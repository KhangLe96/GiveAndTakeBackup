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

		public MvxObservableCollection<PostItemViewModel> PostViewModels
		{
			get => _postViewModels;
			set => SetProperty(ref _postViewModels, value);
		}

		public IMvxCommand ShowMyPostsCommand =>
			_showMyPostsCommand ?? (_showMyPostsCommand = new MvxCommand(ShowMyPosts));

		public IMvxCommand ShowMyRequestsCommand =>
			_showMyRequestsCommand ?? (_showMyRequestsCommand = new MvxCommand(ShowMyRequests));

		private string _avatarUrl;
		private string _userName;
		private string _rankType;
		private string _sentCount;
		private bool _isPostsList;
		private bool _isSearchResultNull;
		private readonly IDataModel _dataModel;
		private IMvxCommand _showMyPostsCommand;
		private IMvxCommand _showMyRequestsCommand;
		private MvxObservableCollection<PostItemViewModel> _postViewModels;

		public ProfileViewModel(IDataModel dataModel)
		{
			_dataModel = dataModel;
			AvatarUrl = _dataModel.LoginResponse.Profile.AvatarUrl;
			UserName = _dataModel.LoginResponse.Profile.FullName;
			RankType = AppConstants.Member;
			SentCount = _dataModel.LoginResponse.Profile.SentCount + AppConstants.Times;
			IsPostsList = true;
		}

		public override Task Initialize() => UpdatePostViewModels();

		private void ShowMyPosts() => IsPostsList = true;

		private void ShowMyRequests() => IsPostsList = false;

		private async Task UpdatePostViewModels()
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
					await UpdatePostViewModels();
				}
			}
		}

		private PostItemViewModel GeneratePostViewModels(Post post)
		{
			post.IsMyPost = post.User.Id == _dataModel.LoginResponse.Profile.Id;
			return new PostItemViewModel(post);
		}
	}
}
