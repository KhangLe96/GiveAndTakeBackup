using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.Base;
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

	

		private readonly IDataModel _dataModel;
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
			_dataModel.ApiNotificationResponse = await ManagementService.GetNotificationList($"limit={AppConstants.NumberOfRequestPerPage}&page={_dataModel.ApiNotificationResponse.Pagination.Page + 1}", _dataModel.LoginResponse.Token);
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
					_dataModel.CurrentPost = await ManagementService.GetPostDetail(notification.RelevantId.ToString());
					await NavigationService.Navigate<PostDetailViewModel, Post, bool>(_dataModel.CurrentPost);
					await ManagementService.UpdateReadStatus(notification.Id.ToString(), true, _dataModel.LoginResponse.Token);

					break;
			}
		}

		private async void OnRefresh()
		{
			IsRefreshing = true;
			await UpdateNotificationViewModels();
			IsRefreshing = false;
		}

		public async Task UpdateNotificationViewModels()
		{
			_dataModel.ApiNotificationResponse = await ManagementService.GetNotificationList($"limit=20", _dataModel.LoginResponse.Token);
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
