using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross;
using System;

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

		public override void ViewCreated()
		{
			base.ViewCreated();
			DataModel.NotificationReceived += OnNotificationReceived;
		}

		public override void ViewDestroy(bool viewFinishing = true)
		{
			base.ViewDestroy(viewFinishing);
			//DataModel.NotificationReceived -= OnNotificationReceived;
		}

		private async void OnNotificationReceived(object sender, Notification notification)
		{
			OnItemClicked(notification);
			//var result = await NavigationService.Navigate<PopupWarningResponseViewModel, string, bool>(AppConstants.ErrorMessage);		
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
					break;

				case "Request":
					
					await HandleRequestType(notification);
					break;

				case "IsAccepted":
					break;

				case "IsRejected":
					await HandleLikeType(notification);
					break;

				case "BlockedPost":
					break;

				case "Warning":
					break;
			}
		}

		private async Task HandleRequestType(Notification notification)
		{
			await Mvx.Resolve<ILoadingOverlayService>().ShowOverlay(AppConstants.LoadingDataOverlayTitle);
			var isProcessed = await ManagementService.CheckIfRequestProcessed(notification.RelevantId, _token);
			var request = await ManagementService.GetRequestById(notification.RelevantId, _token);
			await Mvx.Resolve<ILoadingOverlayService>().CloseOverlay();
			if (isProcessed)
			{
				await NavigationService.Navigate<RequestsViewModel, string, bool>(request.PostId);
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
				}
			}
		}

		private async void OnRequestRejected(Request request)
		{
			var result = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.RequestRejectingMessage);
			if (result == RequestStatus.Submitted)
			{
				var isSaved = await ManagementService.ChangeStatusOfRequest(request.Id, "Rejected", _token);
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
