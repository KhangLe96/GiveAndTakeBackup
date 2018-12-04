﻿using System;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Linq;
using System.Threading.Tasks;


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

		public int NotificationCount
		{
			get => _notificationCount;
			set => SetProperty(ref _notificationCount, value);
		}

		private readonly IDataModel _dataModel;
		private readonly string _token;
		private readonly ILoadingOverlayService _loadingOverlayService;
		private MvxObservableCollection<NotificationItemViewModel> _notificationItemViewModel;
		private bool _isRefresh;
		private int _notiCount;

		private IMvxCommand _refreshCommand;
		private IMvxCommand _loadMoreCommand;
		private int _notificationCount;
		public IMvxCommand RefreshCommand => _refreshCommand = _refreshCommand ?? new MvxCommand(OnRefresh);
		public IMvxCommand LoadMoreCommand => _loadMoreCommand = _loadMoreCommand ?? new MvxAsyncCommand(OnLoadMore);

		public NotificationViewModel(IDataModel dataModel, ILoadingOverlayService loadingOverlayService)
		{
			_dataModel = dataModel;
			_token = _dataModel.LoginResponse.Token;
			_loadingOverlayService = loadingOverlayService;
			_dataModel.NotificationReceived += OnNotificationReceived;
			_dataModel.BadgeNotificationUpdated += OnBadgeReceived;
		}

		public override async Task Initialize()
		{
			await base.Initialize();
			await UpdateNotificationViewModels();
		}

		private void OnBadgeReceived(object sender, int badge)
		{
			NotificationCount = badge;
		}

		public override void ViewCreated()
		{
			base.ViewCreated();
			_dataModel.NotificationReceived += OnNotificationReceived;
			_dataModel.BadgeNotificationUpdated += OnBadgeReceived;
		}

		public override void ViewDestroy(bool viewFinishing = true)
		{
			base.ViewDestroy(viewFinishing);
			_dataModel.NotificationReceived -= OnNotificationReceived;
			_dataModel.BadgeNotificationUpdated -= OnBadgeReceived;
		}

		public void OnNotificationReceived(object sender, Notification notification)
		{
			if (DataModel.SelectedNotification != null)
			{
				OnItemClicked(notification);
				_dataModel.SelectedNotification = null;
			}			
		}

		public async Task UpdateNotificationViewModels()
		{
			_dataModel.ApiNotificationResponse = await ManagementService.GetNotificationList($"limit=20", _token);
			_notiCount = _dataModel.ApiNotificationResponse.NumberOfNotiNotSeen;
			NotificationItemViewModels = new MvxObservableCollection<NotificationItemViewModel>(_dataModel.ApiNotificationResponse.Notifications.Select(GenerateNotificationItem));
		}

		public async Task UpdateNotificationViewModelOverLay()
		{
			await _loadingOverlayService.ShowOverlay(AppConstants.LoadingDataOverlayTitle);
			await UpdateNotificationViewModels();
			await _loadingOverlayService.CloseOverlay();
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
					await HandleIsAcceptedType(notification);
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

		private async Task HandleIsAcceptedType(Notification notification)
		{
			await Mvx.Resolve<ILoadingOverlayService>().ShowOverlay(AppConstants.LoadingDataOverlayTitle);
			var response = await ManagementService.GetResponseById(notification.RelevantId, _token);
			await Mvx.Resolve<ILoadingOverlayService>().CloseOverlay();

			var popupResult = await NavigationService.Navigate<ReponseViewModel, Response, PopupRequestDetailResult>(response);
			if (popupResult == PopupRequestDetailResult.ShowPostDetail)
			{
				await Mvx.Resolve<ILoadingOverlayService>().ShowOverlay(AppConstants.LoadingDataOverlayTitle);
				var post = await ManagementService.GetPostDetail(response.Post.Id.ToString());
				post.IsMyPost = true;

				await NavigationService.Navigate<PostDetailViewModel, Post>(post);
			}

			await ManagementService.UpdateReadStatus(notification.Id.ToString(), true, _token);
		}

		private async Task HandleRequestType(Notification notification)
		{
			await Mvx.Resolve<ILoadingOverlayService>().ShowOverlay(AppConstants.LoadingDataOverlayTitle);
			var isProcessed = await ManagementService.CheckIfRequestProcessed(notification.RelevantId, _token);
			var request = await ManagementService.GetRequestById(notification.RelevantId, _token);
			
			if (isProcessed)
			{
                var post = await ManagementService.GetPostDetail(request.Post.PostId);
                await Mvx.Resolve<ILoadingOverlayService>().CloseOverlay();
                await NavigationService.Navigate<RequestsViewModel, Post, bool>(post);
			}
			else
			{
				await Mvx.Resolve<ILoadingOverlayService>().CloseOverlay();
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
						var post = await ManagementService.GetPostDetail(request.Post.PostId);
						post.IsMyPost = true;
						await NavigationService.Navigate<PostDetailViewModel, Post>(post);
						break;
					default:
						await Mvx.Resolve<ILoadingOverlayService>().CloseOverlay();
						break;
				}
			}

			ManagementService.UpdateReadStatus(notification.Id.ToString(), true, _token);
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
			_dataModel.CurrentPost = await ManagementService.GetPostDetail(notification.RelevantId.ToString());
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
	}
}
