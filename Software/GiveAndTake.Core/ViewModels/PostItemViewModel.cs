using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GiveAndTake.Core.Helpers;
using I18NPortable;
using MvvmCross;

namespace GiveAndTake.Core.ViewModels
{
	public class PostItemViewModel : BaseViewModel
    {
        #region Properties

	    public string CategoryName 
	    {
		    get => $"   {_categoryName}   ";
		    set => SetProperty(ref _categoryName, value);
	    }

	    public string UserName
	    {
		    get => _userName;
			set => SetProperty(ref _userName, value);
		}

	    public string AvatarUrl
	    {
		    get => _avatarUrl;
			set => SetProperty(ref _avatarUrl, value);
		}

	    public string PostTitle
	    {
		    get => _postTitle;
			set => SetProperty(ref _postTitle, value);
		}

	    public string CreatedTime
	    {
		    get => _createdTime;
			set => SetProperty(ref _createdTime, value);
		}


	    public string Address
	    {
		    get => _address;
			set => SetProperty(ref _address, value);
		}

		public string PostImage
	    {
		    get => _postImage;
			set => SetProperty(ref _postImage, value);
		}

		public int RequestCount
	    {
		    get => _requestCount;
			set => SetProperty(ref _requestCount, value);
		}

		public int AppreciationCount
	    {
		    get => _appreciationCount;
			set => SetProperty(ref _appreciationCount, value);
		}

		public int CommentCount
	    {
		    get => _commentCount;
			set => SetProperty(ref _commentCount, value);
		}

		public bool HasManyPostPhotos
	    {
		    get => _hasManyPostPhotos;
			set => SetProperty(ref _hasManyPostPhotos, value);
		}

		public bool IsSeparatorLineShown
	    {
		    get => _isSeparatorLineShown;
		    set => SetProperty(ref _isSeparatorLineShown, value);
	    }

	    public string BackgroundColor
	    {
		    get => _backgroundColor;
			set => SetProperty(ref _backgroundColor, value);
		}

		public string Status
	    {
		    get => _status;
		    set => SetProperty(ref _status, value);
	    }

	    public bool IsRequested
	    {
		    get => _isRequested;
		    set => SetProperty(ref _isRequested, value);
	    }


		public List<ITransformation> PostTransformations => 
		    new List<ITransformation> { new CornersTransformation(5 , CornerTransformType.AllRounded) };

	    public List<ITransformation> AvatarTransformations => 
		    new List<ITransformation> { new CircleTransformation() };

	    public IMvxCommand ShowGiverProfileCommand =>
		    _showGiverProfileCommand ?? (_showGiverProfileCommand = new MvxAsyncCommand(ShowGiverProfile));

	    public IMvxCommand ShowPostDetailCommand =>
		    _showPostDetailCommand ?? (_showPostDetailCommand = new MvxAsyncCommand(ShowPostDetailView));

	    public IMvxCommand ShowMenuPopupCommand =>
		    _showMenuPopupCommand ?? (_showMenuPopupCommand = new MvxAsyncCommand(ShowMenuView));
		


	    private static readonly List<string> MyPostOptions = new List<string>
	    {
		    AppConstants.ChangePostStatus,
		    AppConstants.ModifyPost,
		    AppConstants.ViewPostRequests,
		    AppConstants.DeletePost
	    };


	    private static readonly List<string> OtherPostOptions = new List<string> { AppConstants.ReportPost };

	    private string _postId;
	    private string _categoryName;
	    private string _userName;
	    private string _avatarUrl;
	    private string _postTitle;
	    private string _createdTime;
	    private string _address;
	    private string _postImage;
	    private string _backgroundColor;
	    private string _status;
	    private int _requestCount;
	    private int _appreciationCount;
	    private int _commentCount;
	    private bool _hasManyPostPhotos;
	    private bool _isSeparatorLineShown;
	    private bool _isRequested;
	    private IMvxCommand _showGiverProfileCommand;
	    private IMvxCommand _showPostDetailCommand;
	    private IMvxCommand _showMenuPopupCommand;
		

	    private readonly Post _post;
	    private readonly IDataModel _dataModel;

		#endregion

	    private readonly Action _doReload;
		#region Methods

		public PostItemViewModel(IDataModel dataModel, Post post, Action doReload = null) 
		{
			_dataModel = dataModel;
			_post = post;
			_doReload = doReload;
			Init();
		}

	    private void Init()
	    {
		    CategoryName = _post.Category.CategoryName;
		    AvatarUrl = _post.User.AvatarUrl;
		    UserName = _post.User.FullName ?? AppConstants.DefaultUserName;
		    CreatedTime = TimeHelper.ToTimeAgo(_post.CreatedTime);
		    Address = _post.ProvinceCity.ProvinceCityName;
		    PostTitle = _post.Title;
		    PostImage = _post.Images.FirstOrDefault()?.ResizedImage;
			HasManyPostPhotos = _post.Images.Count > 1;
		    AppreciationCount = _post.AppreciationCount;
		    RequestCount = _post.RequestCount;
		    CommentCount = _post.CommentCount;
		    IsSeparatorLineShown = true;
	        BackgroundColor = _post.Category.BackgroundColor;
		    Status = _post.PostStatus.Translate();
		    IsRequested = _post.IsRequested;
		    _postId = _post.PostId;
		}

	    

        private async Task ShowMenuView()
	    {
			var postOptions = _post.IsMyPost ? MyPostOptions : OtherPostOptions;

			var result = await NavigationService.Navigate<PopupExtensionOptionViewModel, List<string>, string>(postOptions);

			if (string.IsNullOrEmpty(result)) return;

			switch (result)
			{
				case AppConstants.ChangePostStatus:
					await ManagementService.ChangeStatusOfPost(_postId, AppConstants.GivedStatusEN, _dataModel.LoginResponse.Token);
					break;

				case AppConstants.ModifyPost:
					_dataModel.CurrentPost = _post;
					await NavigationService.Navigate<CreatePostViewModel, ViewMode, bool>(ViewMode.EditPost);
					break;

				case AppConstants.ViewPostRequests:
					await NavigationService.Navigate<RequestsViewModel, Post, bool>(_post);
					break;

				case AppConstants.DeletePost:
					await DeletePost();
					break;

				case AppConstants.ReportPost:
					await NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage);
					break;
			}
		}

	    private async Task DeletePost()
	    {
			var userConfirmation = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.ConfirmDeletePost);
		    if (userConfirmation != RequestStatus.Submitted)
		    {
			    return;
		    }
		    await ManagementService.ChangeStatusOfPost(_postId, AppConstants.DeletedStatus, _dataModel.LoginResponse.Token);
		    _doReload?.Invoke();
		}

	    private async Task ShowPostDetailView()
		{
			var result = await NavigationService.Navigate<PostDetailViewModel, Post, bool>(_post);
			RequestCount = Mvx.Resolve<IDataModel>().CurrentPost.RequestCount;
			if (result)
			{
				_doReload?.Invoke();
			}
		}

		private async Task ShowGiverProfile() =>
		    await NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage);

	    #endregion
    }
}