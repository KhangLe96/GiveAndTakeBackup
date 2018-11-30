using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Core.Exceptions;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveAndTake.Core.ViewModels
{
	public class UserProfileViewModel : BaseViewModel<User>
    {
		public string RankTitle => AppConstants.RankTitle;

		public string SentTitle => AppConstants.SentTitle;

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

		public MvxObservableCollection<PostItemViewModel> PostViewModels
		{
			get => _postViewModels;
			set
			{
				SetProperty(ref _postViewModels, value);
				SentCount = _apiPostsResponse.Pagination.Totals + " " + AppConstants.Times;
			}
		}

	    public IMvxCommand RefreshPostsCommand =>
		    _refreshPostsCommand ?? (_refreshPostsCommand = new MvxAsyncCommand(OnRefreshPosts));

	    public IMvxCommand LoadMorePostsCommand =>
			_loadMorePostsCommand ?? (_loadMorePostsCommand = new MvxAsyncCommand(OnLoadMorePosts));

	    public IMvxCommand OpenConversationCommand =>
			_openConversationCommand ?? (_openConversationCommand = new MvxAsyncCommand(OpenConversation));

	    public IMvxCommand BackPressedCommand =>
		    _backPressedCommand ?? (_backPressedCommand = new MvxAsyncCommand(OnBackPressed));

	    private User _user;
		private string _userName;
		private string _rankType;
		private string _avatarUrl;
		private string _sentCount;
		private bool _isPostsRefresh;
		private bool _isSearchResultNull;
	    private readonly IDataModel _dataModel;
	    private ApiPostsResponse _apiPostsResponse;
		private IMvxCommand _refreshPostsCommand;
		private IMvxCommand _loadMorePostsCommand;
		private IMvxCommand _openConversationCommand;
		private IMvxCommand _backPressedCommand;
		private MvxObservableCollection<PostItemViewModel> _postViewModels;

	    public UserProfileViewModel(IDataModel dataModel)
	    {
		    _dataModel = dataModel;
	    }

	    public override void Prepare(User user)
	    {
		    _user = user;
			AvatarUrl = user.AvatarUrl;
		    UserName = user.FullName.ToUpper();
		    RankType = AppConstants.Member;
		}

	    public override Task Initialize()
	    {
			//TODO: Add loading indicator here
		    return UpdatePostsViewModels();
	    }

	    private async Task OnRefreshPosts()
		{
			IsPostsRefreshing = true;
			await UpdatePostsViewModels();
			IsPostsRefreshing = false;
		}

		private async Task OnLoadMorePosts()
		{
			try
			{
				_apiPostsResponse = await ManagementService.GetMyPostList(_user.Id, $"page={_apiPostsResponse.Pagination.Page + 1}", _dataModel.LoginResponse.Token);

				if (_apiPostsResponse.Posts.Any())
				{
					PostViewModels.Last().IsSeparatorLineShown = true;
					PostViewModels.AddRange(_apiPostsResponse.Posts.Select(GeneratePostViewModels));
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

		private async Task UpdatePostsViewModels()
		{
			try
			{
				_apiPostsResponse = await ManagementService.GetMyPostList(_user.Id, null, _dataModel.LoginResponse.Token);
				if (_apiPostsResponse.Posts.Any())
				{
					PostViewModels = new MvxObservableCollection<PostItemViewModel>(_apiPostsResponse.Posts.Select(GeneratePostViewModels));
					PostViewModels.Last().IsSeparatorLineShown = false;
				}

				IsSearchResultNull = PostViewModels.Any();
			}
			catch (AppException.ApiException)
			{
				var result = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.ErrorConnectionMessage);
				if (result == RequestStatus.Submitted)
				{
					await UpdatePostsViewModels();
				}
			}
		}

		private PostItemViewModel GeneratePostViewModels(Post post)
		{
			post.IsMyPost = false;
			return new PostItemViewModel(_dataModel, post);
		}

		private async Task OpenConversation()
		{
			//TODO: close current view and navigate to conversation view
			await NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage);
		}

	    private Task OnBackPressed() => NavigationService.Close(this);
    }
}
