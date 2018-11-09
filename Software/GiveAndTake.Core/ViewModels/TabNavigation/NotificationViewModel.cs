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
	public class NotificationViewModel : BaseViewModel<string, bool>
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

		public IMvxCommand RefreshCommand => _refreshCommand = _refreshCommand ?? new MvxCommand(OnRefresh);

		public IMvxCommand LoadMoreCommand => _loadMoreCommand = _loadMoreCommand ?? new MvxAsyncCommand(OnLoadMore);

		private readonly IDataModel _dataModel;
		private MvxObservableCollection<NotificationItemViewModel> _notificationItemViewModel;
		private bool _isRefresh;
		private IMvxCommand _refreshCommand;
		private IMvxCommand _loadMoreCommand;

		public NotificationViewModel(IDataModel dataModel)
		{
			_dataModel = dataModel;
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
			_dataModel.ApiNotificationResponse = await ManagementService.GetNotificationList($"limit={AppConstants.NumberOfRequestPerPage}&page={_dataModel.ApiRequestsResponse.Pagination.Page + 1}");
			if (_dataModel.ApiNotificationResponse.Notifications.Any())
			{
				NotificationItemViewModels.AddRange(_dataModel.ApiNotificationResponse.Notifications.Select(GenerateRequestItem));
			}
		}

		private NotificationItemViewModel GenerateRequestItem(Notification notification)
		{
			var notificationItem = new NotificationItemViewModel(notification)
			{
				//ClickAction = OnItemClicked,
			};
			return notificationItem;
		}

		private async void OnItemClicked(Request request)
		{

		}

		private async void OnRefresh()
		{
			IsRefreshing = true;
			await UpdateNotificationViewModels();
			IsRefreshing = false;
		}

		public async Task UpdateNotificationViewModels()
		{
			_dataModel.ApiNotificationResponse = await ManagementService.GetNotificationList("");
			NotificationItemViewModels = new MvxObservableCollection<NotificationItemViewModel>(_dataModel.ApiNotificationResponse.Notifications.Select(GenerateRequestItem));
		}

		public override void Prepare(string postId)
		{
			//_postId = postId;
			InitNotificationViewModels();
		}

		public async Task UpdateNotificationViewModelOverLay()
		{
			await Mvx.Resolve<ILoadingOverlayService>().ShowOverlay(AppConstants.LoadingDataOverlayTitle);
			await UpdateNotificationViewModels();
			await Mvx.Resolve<ILoadingOverlayService>().CloseOverlay();
		}
	}
}
