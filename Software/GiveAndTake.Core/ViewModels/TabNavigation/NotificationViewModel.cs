using System;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using GiveAndTake.Core.Exceptions;


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

		//public int NotificationCount
		//{
		//	get => _notificationCount;
		//	set
		//	{
		//		//_notificationCount = value;
		//		//RaisePropertyChanged(() => NotificationCount);
		//		SetProperty(ref _notificationCount, value);
		//	}
		//}


		private readonly IDataModel _dataModel;
		private readonly string _token;
		private readonly ILoadingOverlayService _overlay;
		private MvxObservableCollection<NotificationItemViewModel> _notificationItemViewModel;
		private bool _isRefresh;
		private int _notiCount;
		private IMvxCommand _refreshCommand;
		private IMvxCommand _loadMoreCommand;
		//private int _notificationCount;
		public IMvxCommand RefreshCommand => _refreshCommand = _refreshCommand ?? new MvxCommand(OnRefresh);
		public IMvxCommand LoadMoreCommand => _loadMoreCommand = _loadMoreCommand ?? new MvxAsyncCommand(OnLoadMore);

		public NotificationViewModel(IDataModel dataModel, ILoadingOverlayService loadingOverlayService)
		{
			_dataModel = dataModel;
			_token = _dataModel.LoginResponse.Token;
			_overlay = loadingOverlayService;
			//_dataModel.NotificationReceived += OnNotificationReceived;
			//_dataModel.BadgeNotificationUpdated += OnBadgeReceived;

		}

		public override async Task Initialize()
		{
			await base.Initialize();
			await UpdateNotificationViewModels();
			//for iphone when app is destroyed
			if (DataModel.SelectedNotification != null)
			{
				OnItemClicked(DataModel.SelectedNotification);
				DataModel.SelectedNotification = null;
			}
		}

		private void OnBadgeReceived(object sender, int badge)
		{
			//NotificationCount = badge;
			if (badge != 0)
			{
				Task.Run(() => { UpdateNotificationViewModels(); });
			}
		}
		
		public override void ViewCreated()
		{
			base.ViewCreated();
			_dataModel.NotificationReceived += OnNotificationReceived;
			_dataModel.BadgeNotificationUpdated += OnBadgeReceived;
			ManagementService.UpdateSeenNotificationStatus(true, DataModel.LoginResponse.Token);
		}

		public override void ViewDisappearing()
		{
			base.ViewDisappearing();
			DataModel.SelectedNotification = null;						
		}

		public override void ViewDestroy(bool viewFinishing = true)
		{
			base.ViewDestroy(viewFinishing);
			_dataModel.BadgeNotificationUpdated -= OnBadgeReceived;
			_dataModel.NotificationReceived -= OnNotificationReceived;
		}

		public void OnNotificationReceived(object sender, Notification notification)
		{
			if (DataModel.SelectedNotification != null)
			{				
				OnItemClicked(notification);
				Task.Run(() => { UpdateNotificationViewModels(); });
				//_dataModel.SelectedNotification = null;
				ManagementService.UpdateSeenNotificationStatus(true, DataModel.LoginResponse.Token);
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
			await _overlay.ShowOverlay(AppConstants.LoadingDataOverlayTitle);
			await UpdateNotificationViewModels();
			await _overlay.CloseOverlay();
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
			try
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

				case "CancelRequest":
					await HandleCancelRequestType(notification);
					break;

				case "BlockedPost":
					await NavigationService.Navigate<PopupMessageViewModel, string>("Chức năng chưa hoàn thiện!");
					break;

					case "Warning":
						await NavigationService.Navigate<PopupMessageViewModel, string>("Chức năng chưa hoàn thiện!");
						break;
				}
				Task.Run(() => { UpdateNotificationViewModels(); });
			}
			catch (AppException.ApiException)
			{
				await NavigationService.Navigate<PopupWarningViewModel, string, bool>(AppConstants
					.ErrorConnectionMessage);
			}		
		}

		private async Task HandleCancelRequestType(Notification notification)
		{
			await Mvx.Resolve<ILoadingOverlayService>().ShowOverlay(AppConstants.LoadingDataOverlayTitle);
			var post = await ManagementService.GetPostDetail(notification.RelevantId.ToString(), _token);
			await Mvx.Resolve<ILoadingOverlayService>().CloseOverlay();
			await NavigationService.Navigate<RequestsViewModel, Post, bool>(post);
		}

		private async Task HandleIsAcceptedType(Notification notification)
		{
			try
			{
				await _overlay.ShowOverlay(AppConstants.LoadingDataOverlayTitle);
				var response = await ManagementService.GetResponseById(notification.RelevantId, _token);
				await _overlay.CloseOverlay();

				var popupResult =
					await NavigationService.Navigate<ResponseViewModel, Response, PopupRequestDetailResult>(response);
				if (popupResult == PopupRequestDetailResult.ShowPostDetail)
				{
					await _overlay.ShowOverlay(AppConstants.LoadingDataOverlayTitle);
					var post = await ManagementService.GetPostDetail(response.Post.Id.ToString(), _token);
					post.IsMyPost = true;

					await NavigationService.Navigate<PostDetailViewModel, Post>(post);
				}

				await ManagementService.UpdateReadStatus(notification.Id.ToString(), true, _token);
			}
			catch (AppException.ApiException)
			{
				await NavigationService.Navigate<PopupWarningViewModel, string, bool>(AppConstants
					.ErrorConnectionMessage);
			}
			finally
			{
				await _overlay.CloseOverlay();
			}
			
		}

		private async Task HandleRequestType(Notification notification)
		{
			await _overlay.ShowOverlay(AppConstants.LoadingDataOverlayTitle);
			var isProcessed = await ManagementService.CheckIfRequestProcessed(notification.RelevantId, _token);
			var request = await ManagementService.GetRequestById(notification.RelevantId, _token);
			
			if (isProcessed)
			{
                var post = await ManagementService.GetPostDetail(request.Post.PostId, _token);
                await _overlay.CloseOverlay();
                await NavigationService.Navigate<RequestsViewModel, Post, bool>(post);
			}
			else
			{
				await _overlay.CloseOverlay();
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
                        await _overlay.ShowOverlay(AppConstants.LoadingDataOverlayTitle);
                        var post = await ManagementService.GetPostDetail(request.Post.PostId, _token);
                        post.IsMyPost = true;

                        await NavigationService.Navigate<PostDetailViewModel, Post>(post);
                        break;

					default:
						await _overlay.CloseOverlay();
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
				await _overlay.CloseOverlay();
				if (isSaved)
				{
					await NavigationService.Navigate<PopupWarningViewModel, string, bool>(AppConstants.SuccessfulRejectionMessage);
				}
			}
		}

		private async void OnRequestAccepted(Request request)
		{
			var result = await NavigationService.Navigate<PopupResponseViewModel, Request, RequestStatus>(request);
			if (result == RequestStatus.Submitted)
			{
				await NavigationService.Navigate<PopupWarningViewModel, string, bool>(AppConstants.SuccessfulAcceptanceMessage);
			}
		}

		private async Task HandleLikeType(Notification notification)
		{
			await _overlay.ShowOverlay(AppConstants.LoadingDataOverlayTitle);
			_dataModel.CurrentPost = await ManagementService.GetPostDetail(notification.RelevantId.ToString(), _token);
			_dataModel.CurrentPost.IsMyPost = true;
			await _overlay.CloseOverlay();

			await NavigationService.Navigate<PostDetailViewModel, Post, bool>(_dataModel.CurrentPost);

			await ManagementService.UpdateReadStatus(notification.Id.ToString(), true, _token);
		}

		private async void OnRefresh()
		{
			IsRefreshing = true;
			await UpdateNotificationViewModels();
			ManagementService.UpdateSeenNotificationStatus(true, DataModel.LoginResponse.Token);
			IsRefreshing = false;
		}
	}
}
