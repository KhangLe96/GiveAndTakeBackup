using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Core.Exceptions;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveAndTake.Core.ViewModels.TabNavigation
{
	public class ProfileViewModel : BaseViewModel
	{
		public string RankTitle => AppConstants.RankTitle;

		public string SentTitle => AppConstants.SentTitle;

		public string LeftButtonTitle => AppConstants.MyPostsTitle;

		public string RightButtonTitle => AppConstants.MyRequestsTitle;

		public List<ITransformation> AvatarTransformations => new List<ITransformation> { new CircleTransformation() };


		public string AvatarUrl
		{
			get => _avatarUrl;
			set => SetProperty(ref _avatarUrl, value);
		}

		public string UserName
		{
			get => _userName;
			set => SetProperty(ref _userName, value);
		}

		public string RankType
		{
			get => _rankType;
			set => SetProperty(ref _rankType, value);
		}

		public string SentCount
		{
			get => _sentCount;
			set => SetProperty(ref _sentCount, value);
		}

		public bool IsPostsList
		{
			get => _isPostsList;
			set => SetProperty(ref _isPostsList, value);
		}

		public bool IsSearchResultNull
		{
			get => _isSearchResultNull;
			set => SetProperty(ref _isSearchResultNull, value);
		}

		public bool IsPostsRefreshing
		{
			get => _isPostsRefresh;
			set => SetProperty(ref _isPostsRefresh, value);
		}

		public bool IsRequestedPostsRefreshing
		{
			get => _isRequestedPostsRefresh;
			set => SetProperty(ref _isRequestedPostsRefresh, value);
		}

		public MvxObservableCollection<PostItemViewModel> PostViewModels
		{
			get => _postViewModels;
			set
			{
				SetProperty(ref _postViewModels, value);
				SentCount = _dataModel.ApiMyPostsResponse.Pagination.Totals + " " + AppConstants.Times;
			}
		}

		public MvxObservableCollection<PostItemViewModel> RequestedPostViewModels
		{
			get => _requestedPostViewModels;
			set => SetProperty(ref _requestedPostViewModels, value);
		}

		public IMvxCommand ShowMyPostsCommand =>
			_showMyPostsCommand ?? (_showMyPostsCommand = new MvxCommand(ShowMyPosts));

		public IMvxCommand ShowMyRequestsCommand =>
			_showMyRequestsCommand ?? (_showMyRequestsCommand = new MvxAsyncCommand(ShowMyRequests));

		public IMvxCommand LoadMorePostsCommand =>
			_loadMorePostsCommand ?? (_loadMorePostsCommand = new MvxAsyncCommand(OnLoadMorePosts));

		public IMvxCommand LoadMoreRequestedPostsCommand =>
			_loadMoreRequestedPostsCommand ?? (_loadMoreRequestedPostsCommand = new MvxAsyncCommand(OnLoadMoreRequestedPosts));

		public IMvxCommand RefreshPostsCommand =>
			_refreshPostsCommand ?? (_refreshPostsCommand = new MvxAsyncCommand(OnRefreshPosts));

		public IMvxCommand RefreshRequestedPostsCommand =>
			_refreshRequestedPostsCommand ?? (_refreshRequestedPostsCommand = new MvxAsyncCommand(OnRefreshRequestedPosts));

		public IMvxCommand ShowMenuPopupCommand =>
			_showMenuPopupCommand ?? (_showMenuPopupCommand = new MvxAsyncCommand(ShowMenuSettingView));

		public IMvxInteraction LogoutFacebook =>
			_logoutFacebook ?? (_logoutFacebook = new MvxInteraction());

		public IMvxCommand CreatePostCommand =>
			_createPostCommand ?? (_createPostCommand = new MvxAsyncCommand(ShowNewPostView));

		private string _avatarUrl;
		private string _userName;
		private string _rankType;
		private string _sentCount;
		private bool _isPostsList;
		private bool _isPostsRefresh;
		private bool _isRequestedPostsRefresh;
		private bool _isSearchResultNull;
		private readonly IDataModel _dataModel;
		private readonly ILoadingOverlayService _overlayService;
		private IMvxCommand _showMyPostsCommand;
		private IMvxCommand _showMyRequestsCommand;
		private IMvxCommand _loadMorePostsCommand;
		private IMvxCommand _loadMoreRequestedPostsCommand;
		private IMvxCommand _refreshPostsCommand;
		private IMvxCommand _refreshRequestedPostsCommand;
		private IMvxCommand _showMenuPopupCommand;
		private MvxInteraction _logoutFacebook;
		private IMvxCommand _createPostCommand;
		private MvxObservableCollection<PostItemViewModel> _postViewModels;
		private MvxObservableCollection<PostItemViewModel> _requestedPostViewModels;
		private Post _post;

		private static readonly List<string> MenuSettingOptions = new List<string> 
		{
			AppConstants.SendFeedback,
            AppConstants.About,
			AppConstants.LogOut
		};

		public ProfileViewModel(IDataModel dataModel, ILoadingOverlayService overlayService)
		{
			_dataModel = dataModel;
			_overlayService = overlayService;
			AvatarUrl = _dataModel.LoginResponse.Profile.AvatarUrl;
			UserName = _dataModel.LoginResponse.Profile.FullName.ToUpper();
			RankType = AppConstants.Member;
			IsPostsList = true;
		}

		public override async Task Initialize()
		{
			await base.Initialize();
			await UpdateMyPostViewModels();
		}

		private void ShowMyPosts()
		{
			if (IsPostsList)
			{
				return;
			}

			IsPostsList = true;
			_post.IsMyPost = IsPostsList;
		}

		private async Task ShowMyRequests()
		{
			if (!IsPostsList)
			{
				return;
			}

			IsPostsList = false;
			_post.IsMyPost = IsPostsList;

			if (RequestedPostViewModels == null)
			{
				await _overlayService.ShowOverlay(AppConstants.LoadingDataOverlayTitle);
				await UpdateMyRequestedPostViewModels();
				await _overlayService.CloseOverlay();
			}
		}

		private async Task OnRefreshPosts()
		{
			IsPostsRefreshing = true;
			await UpdateMyPostViewModels();
			IsPostsRefreshing = false;
		}

		private async Task OnRefreshRequestedPosts()
		{
			IsRequestedPostsRefreshing = true;
			await UpdateMyRequestedPostViewModels();
			IsRequestedPostsRefreshing = false;
		}

		private async Task OnLoadMorePosts()
		{
			try
			{
				_dataModel.ApiMyPostsResponse = await ManagementService.GetMyPostList(_dataModel.LoginResponse.Profile.Id, $"page={_dataModel.ApiMyPostsResponse.Pagination.Page + 1}", _dataModel.LoginResponse.Token);

				if (_dataModel.ApiMyPostsResponse.Posts.Any())
				{
					PostViewModels.Last().IsSeparatorLineShown = true;
					PostViewModels.AddRange(_dataModel.ApiMyPostsResponse.Posts.Select(GeneratePostViewModels));
					PostViewModels.Last().IsSeparatorLineShown = false;
				}
			}
			catch (AggregateException) 
			{
				var result = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.ErrorConnectionMessage);
				if (result == RequestStatus.Submitted)
				{
					await OnLoadMorePosts();
				}
			}
		}

		private async Task OnLoadMoreRequestedPosts()
		{
			try
			{
				_dataModel.ApiMyRequestedPostResponse = await ManagementService.GetMyRequestedPosts($"page={_dataModel.ApiMyRequestedPostResponse.Pagination.Page + 1}", _dataModel.LoginResponse.Token);

				if (_dataModel.ApiMyRequestedPostResponse.Posts.Any())
				{
					RequestedPostViewModels.Last().IsSeparatorLineShown = true;
					RequestedPostViewModels.AddRange(_dataModel.ApiMyRequestedPostResponse.Posts.Select(GeneratePostViewModels));
					RequestedPostViewModels.Last().IsSeparatorLineShown = false;
				}
			}
			catch (AggregateException) 
			{
				var result = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.ErrorConnectionMessage);
				if (result == RequestStatus.Submitted)
				{
					await OnLoadMoreRequestedPosts();
				}
			}
		}

		private async Task UpdateMyPostViewModels()
		{
			try
			{
				_dataModel.ApiMyPostsResponse = await ManagementService.GetMyPostList(_dataModel.LoginResponse.Profile.Id, null, _dataModel.LoginResponse.Token);
				PostViewModels = new MvxObservableCollection<PostItemViewModel>();
				if (_dataModel.ApiMyPostsResponse.Posts.Any())
				{
					PostViewModels.AddRange(_dataModel.ApiMyPostsResponse.Posts.Select(GeneratePostViewModels));
					PostViewModels.Last().IsSeparatorLineShown = false;
				}

				IsSearchResultNull = PostViewModels.Any();
			}
			catch (AppException.ApiException)
			{
				var result = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.ErrorConnectionMessage);
				if (result == RequestStatus.Submitted)
				{
					await UpdateMyPostViewModels();
				}
			}
		}

		private async Task UpdateMyRequestedPostViewModels()
		{
			try
			{
				_dataModel.ApiMyRequestedPostResponse = await ManagementService.GetMyRequestedPosts(null, _dataModel.LoginResponse.Token);
				RequestedPostViewModels = new MvxObservableCollection<PostItemViewModel>();
				if (_dataModel.ApiMyRequestedPostResponse.Posts.Any())
				{
					RequestedPostViewModels.AddRange(_dataModel.ApiMyRequestedPostResponse.Posts.Select(GeneratePostViewModels));
					RequestedPostViewModels.Last().IsSeparatorLineShown = false;
				}

				IsSearchResultNull = RequestedPostViewModels.Any();
			}
			catch (AppException.ApiException)
			{
				var result = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.ErrorConnectionMessage);
				if (result == RequestStatus.Submitted)
				{
					await UpdateMyRequestedPostViewModels();
				}
			}
		}

		private async void ReloadData()
		{
			await UpdateProfileViewModelWithOverlay();
		}

		private async Task ShowNewPostView()
		{
			if (_dataModel.Categories != null)
			{
				try
				{
					_dataModel.Categories.RemoveAt(0);
					var result = await NavigationService.Navigate<CreatePostViewModel, bool>();
					_dataModel.Categories = (await ManagementService.GetCategories()).Categories;
					if (result)
					{
						await UpdateProfileViewModelWithOverlay();
					}
				}
				catch (AppException.ApiException)
				{
					await NavigationService.Navigate<PopupWarningViewModel, string, bool>(AppConstants
						.ErrorConnectionMessage);
				}
			}
		}

		private async Task UpdateProfileViewModelWithOverlay()
		{
			try
			{
				await _overlayService.ShowOverlay(AppConstants.LoadingDataOverlayTitle);
				if (_post.IsMyPost)
				{
					await UpdateMyPostViewModels();
				}
				else
				{
					await UpdateMyRequestedPostViewModels();
				}
			}
			catch (AppException.ApiException)
			{
				await NavigationService.Navigate<PopupWarningViewModel, string, bool>(AppConstants.ErrorConnectionMessage);
			}
			finally
			{
				await _overlayService.CloseOverlay();
			}
		}

		private PostItemViewModel GeneratePostViewModels(Post post)
		{
			post.IsMyPost = IsPostsList;
			_post = post;
			return new PostItemViewModel(_dataModel, post, ReloadData);
		}

		private async Task ShowMenuSettingView()
		{
			var result = await NavigationService.Navigate<PopupExtensionOptionViewModel, List<string>, string>(MenuSettingOptions);

			if (string.IsNullOrEmpty(result)) return;

			switch (result)
			{
				case AppConstants.SendFeedback:
					await NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage);
					break;
                case AppConstants.About:
                    await NavigationService.Navigate<AboutViewModel>();
                    break;
                case AppConstants.LogOut:
					_logoutFacebook.Raise();
	                _dataModel.ApiPostsResponse = null;
					await Task.WhenAll(
						ManagementService.Logout(_dataModel.LoginResponse.Token),
						NavigationService.Navigate<LoginViewModel>());
	                _dataModel.LoginResponse = null;
					break;
			}
		}

	}
}
