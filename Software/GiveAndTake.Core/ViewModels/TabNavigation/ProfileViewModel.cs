using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Core.Exceptions;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

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
			set => SetProperty(ref _postViewModels, value);
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

		private string _avatarUrl;
		private string _userName;
		private string _rankType;
		private string _sentCount;
		private bool _isPostsList;
		private bool _isPostsRefresh;
		private bool _isRequestedPostsRefresh;
		private bool _isSearchResultNull;
		private readonly IDataModel _dataModel;
		private IMvxCommand _showMyPostsCommand;
		private IMvxCommand _showMyRequestsCommand;
		private IMvxCommand _loadMorePostsCommand;
		private IMvxCommand _loadMoreRequestedPostsCommand;
		private IMvxCommand _refreshPostsCommand;
		private IMvxCommand _refreshRequestedPostsCommand;
		private MvxObservableCollection<PostItemViewModel> _postViewModels;
		private MvxObservableCollection<PostItemViewModel> _requestedPostViewModels;

		public ProfileViewModel(IDataModel dataModel)
		{
			_dataModel = dataModel;
			AvatarUrl = _dataModel.LoginResponse.Profile.AvatarUrl;
			UserName = _dataModel.LoginResponse.Profile.FullName.ToUpper();
			RankType = AppConstants.Member;
			SentCount = _dataModel.LoginResponse.Profile.SentCount + " " + AppConstants.Times;
			IsPostsList = true;
		}

		public override Task Initialize() => UpdateMyPostViewModels();

		private void ShowMyPosts() => IsPostsList = true;

		private async Task ShowMyRequests()
		{
			IsPostsList = false;
			if (RequestedPostViewModels == null)
			{
				await UpdateMyRequestedPostViewModels();
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
				_dataModel.ApiPostsResponse = await ManagementService.GetMyPostList(_dataModel.LoginResponse.Profile.Id, $"page={_dataModel.ApiPostsResponse.Pagination.Page + 1}", _dataModel.LoginResponse.Token);
				if (_dataModel.ApiPostsResponse.Posts.Any())
				{
					PostViewModels.Last().IsSeparatorLineShown = true;
					PostViewModels.AddRange(_dataModel.ApiPostsResponse.Posts.Select(GeneratePostViewModels));
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
				PostViewModels = new MvxObservableCollection<PostItemViewModel>(_dataModel.ApiMyPostsResponse.Posts.Select(GeneratePostViewModels));
				if (PostViewModels.Any())
				{
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
				RequestedPostViewModels = new MvxObservableCollection<PostItemViewModel>(_dataModel.ApiMyRequestedPostResponse.Posts.Select(GeneratePostViewModels));
				if (RequestedPostViewModels.Any())
				{
					RequestedPostViewModels.Last().IsSeparatorLineShown = false;
				}

				IsSearchResultNull = PostViewModels.Any();
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

		private PostItemViewModel GeneratePostViewModels(Post post)
		{
			post.IsMyPost = false;
			return new PostItemViewModel(post);
		}
	}
}
