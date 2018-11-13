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
			_dataModel.ApiNotificationResponse = await ManagementService.GetNotificationList($"limit={AppConstants.NumberOfRequestPerPage}&page={_dataModel.ApiNotificationResponse.Pagination.Page + 1}", _dataModel.LoginResponse.Token);
			if (_dataModel.ApiNotificationResponse.Notifications.Any())
			{
				await GenerateNotificationItemViewModels();
			}
		}

		private async Task GenerateNotificationItemViewModels()
		{
			foreach (var notification in _dataModel.ApiNotificationResponse.Notifications)
			{
				_dataModel.CurrentPost = await ManagementService.GetPostDetail(notification.RelevantId.ToString());
				var postUrl = _dataModel.CurrentPost.Images.Count != 0 ? _dataModel.CurrentPost.Images.ElementAt(0).ResizedImage : null;
				var avatarUrl = _dataModel.LoginResponse.Profile.AvatarUrl;

				NotificationItemViewModels.Add(GenerateNotificationItem(notification, avatarUrl, postUrl));
			}
		}

		private NotificationItemViewModel GenerateNotificationItem(Notification notification, string avatarUrl, string postUrl)
		{
			var notificationItem = new NotificationItemViewModel(notification, avatarUrl, postUrl)
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
			_dataModel.ApiNotificationResponse = await ManagementService.GetNotificationList("", _dataModel.LoginResponse.Token);
			NotificationItemViewModels = new MvxObservableCollection<NotificationItemViewModel>();
			await GenerateNotificationItemViewModels();
		}

		public async Task UpdateNotificationViewModelOverLay()
		{
			await Mvx.Resolve<ILoadingOverlayService>().ShowOverlay(AppConstants.LoadingDataOverlayTitle);
			await UpdateNotificationViewModels();
			await Mvx.Resolve<ILoadingOverlayService>().CloseOverlay();
		}
	}
}
