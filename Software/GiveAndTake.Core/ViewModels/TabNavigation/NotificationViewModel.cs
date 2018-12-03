﻿using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross;

namespace GiveAndTake.Core.ViewModels.TabNavigation
{
	public class NotificationViewModel : BaseViewModel
	{
		public bool IsRefreshing
		{
			get => _isRefresh;
			set => SetProperty(ref _isRefresh, value);
		}

		public MvxObservableCollection<NotificationItemViewModel> NotificationItemViewModels
		{
			get => _notificationItemViewModel;
			set => SetProperty(ref _notificationItemViewModel, value);
		}
	
		private readonly IDataModel _dataModel;
		private readonly string _token;
		private readonly ILoadingOverlayService _loadingOverlayService;
		private MvxObservableCollection<NotificationItemViewModel> _notificationItemViewModel;
		private bool _isRefresh;

		private IMvxCommand _refreshCommand;
		private IMvxCommand _loadMoreCommand;
		public IMvxCommand RefreshCommand => _refreshCommand = _refreshCommand ?? new MvxCommand(OnRefresh);
		public IMvxCommand LoadMoreCommand => _loadMoreCommand = _loadMoreCommand ?? new MvxAsyncCommand(OnLoadMore);

		public NotificationViewModel(IDataModel dataModel, ILoadingOverlayService loadingOverlayService)
		{
			_dataModel = dataModel;
			_token = _dataModel.LoginResponse.Token;
			_loadingOverlayService = loadingOverlayService;
		}

		public override async Task Initialize()
		{
			await base.Initialize();
			await UpdateNotificationViewModels();
		}

		private async void InitNotificationViewModels()
		{
			await UpdateNotificationViewModelOverLay();
		}

		private async Task OnLoadMore()
		{
			_dataModel.ApiNotificationResponse = await ManagementService.GetNotificationList($"limit={AppConstants.NumberOfRequestPerPage}&page={_dataModel.ApiNotificationResponse.Pagination.Page + 1}", _token);
			if (_dataModel.ApiNotificationResponse.Notifications.Any())
			{
				NotificationItemViewModels.AddRange(
					_dataModel.ApiNotificationResponse.Notifications.Select(GenerateNotificationItem));
			}
		}

		private NotificationItemViewModel GenerateNotificationItem(Notification notification)
		{
			var notificationItem = new NotificationItemViewModel(notification)
			{
				ClickAction = OnItemClicked,
			};
			return notificationItem;
		}

		private async void OnItemClicked(Notification notification)
		{
			switch (notification.Type)
			{
				case "Like":
					await HandleLikeType(notification);
					break;

				case "Comment":
					await NavigationService.Navigate<PopupMessageViewModel, string>("Chức năng chưa hoàn thiện!");
					break;

				case "Request":
					await HandleRequestType(notification);
					break;

				case "IsAccepted":
					await NavigationService.Navigate<PopupMessageViewModel, string>("Chức năng chưa hoàn thiện!");
					break;

				case "IsRejected":
					await HandleLikeType(notification);
					break;

				case "BlockedPost":
					await NavigationService.Navigate<PopupMessageViewModel, string>("Chức năng chưa hoàn thiện!");
					break;

				case "Warning":
					await NavigationService.Navigate<PopupMessageViewModel, string>("Chức năng chưa hoàn thiện!");
					break;
			}
		}

		private async Task HandleRequestType(Notification notification)
		{
			await Mvx.Resolve<ILoadingOverlayService>().ShowOverlay(AppConstants.LoadingDataOverlayTitle);
			var isProcessed = await ManagementService.CheckIfRequestProcessed(notification.RelevantId, _token);
			var request = await ManagementService.GetRequestById(notification.RelevantId, _token);
			
			if (isProcessed)
			{
                var post = await ManagementService.GetPostDetail(request.Post.PostId, _dataModel.LoginResponse.Token);
                await Mvx.Resolve<ILoadingOverlayService>().CloseOverlay();
                await NavigationService.Navigate<RequestsViewModel, Post, bool>(post);
			}
			else
			{
				var popupResult = await NavigationService.Navigate<RequestDetailViewModel, Request, PopupRequestDetailResult>(request);
				switch (popupResult)
				{
					case PopupRequestDetailResult.Rejected:
						OnRequestRejected(request);
						break;

					case PopupRequestDetailResult.Accepted:
						OnRequestAccepted(request);
						break;

                    case PopupRequestDetailResult.ShowPostDetail:
                        await Mvx.Resolve<ILoadingOverlayService>().ShowOverlay(AppConstants.LoadingDataOverlayTitle);
                        var post = await ManagementService.GetPostDetail(request.Post.PostId, _dataModel.LoginResponse.Token);
                        post.IsMyPost = true;

                        await NavigationService.Navigate<PostDetailViewModel, Post>(post);
                        break;

					default:
						await Mvx.Resolve<ILoadingOverlayService>().CloseOverlay();
						break;
                }
			}

			await ManagementService.UpdateReadStatus(notification.Id.ToString(), true, _token);
		}

		private async void OnRequestRejected(Request request)
		{
			var result = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.RequestRejectingMessage);

			if (result == RequestStatus.Submitted)
			{
				var isSaved = await ManagementService.ChangeStatusOfRequest(request.Id, "Rejected", _token);
				await Mvx.Resolve<ILoadingOverlayService>().CloseOverlay();
				if (isSaved)
				{
					await NavigationService.Navigate<PopupNotificationViewModel, string>(AppConstants.SuccessfulRejectionMessage);
				}
			}
		}

		private async void OnRequestAccepted(Request request)
		{
			var result = await NavigationService.Navigate<PopupResponseViewModel, Request, RequestStatus>(request);
			if (result == RequestStatus.Submitted)
			{
				await NavigationService.Navigate<PopupNotificationViewModel, string>(AppConstants.SuccessfulAcceptanceMessage);
			}
		}

		private async Task HandleLikeType(Notification notification)
		{
			await Mvx.Resolve<ILoadingOverlayService>().ShowOverlay(AppConstants.LoadingDataOverlayTitle);
			_dataModel.CurrentPost = await ManagementService.GetPostDetail(notification.RelevantId.ToString(), _dataModel.LoginResponse.Token);
			_dataModel.CurrentPost.IsMyPost = true;
			await Mvx.Resolve<ILoadingOverlayService>().CloseOverlay();

			await NavigationService.Navigate<PostDetailViewModel, Post, bool>(_dataModel.CurrentPost);

			await ManagementService.UpdateReadStatus(notification.Id.ToString(), true, _token);
		}

		private async void OnRefresh()
		{
			IsRefreshing = true;
			await UpdateNotificationViewModels();
			IsRefreshing = false;
		}

		public async Task UpdateNotificationViewModels()
		{
			_dataModel.ApiNotificationResponse = await ManagementService.GetNotificationList($"limit=20", _token);
			NotificationItemViewModels = new MvxObservableCollection<NotificationItemViewModel>(_dataModel.ApiNotificationResponse.Notifications.Select(GenerateNotificationItem));
		}

		public async Task UpdateNotificationViewModelOverLay()
		{
			await _loadingOverlayService.ShowOverlay(AppConstants.LoadingDataOverlayTitle);
			await UpdateNotificationViewModels();
			await _loadingOverlayService.CloseOverlay();
		}
	}
}
