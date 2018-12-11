using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Core.Helpers;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using I18NPortable;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

	    public bool IsStatusShown
	    {
		    get => _isStatusShown;
		    set => SetProperty(ref _isStatusShown, value);
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

	    public bool IsRequestIconActivated
	    {
		    get => _isRequestIconActivated;
		    set => SetProperty(ref _isRequestIconActivated, value);
	    }

	    public MvxColor StatusColor
	    {
		    get => _statusColor;
		    set => SetProperty(ref _statusColor, value);
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

	    public Action ShowProfileTab { get; set; }

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
	    private bool _isRequestIconActivated;
	    private bool _isStatusShown;
		private IMvxCommand _showGiverProfileCommand;
	    private IMvxCommand _showPostDetailCommand;
	    private IMvxCommand _showMenuPopupCommand;

	    private readonly IDataModel _dataModel;

		private Post _post;
	    private MvxColor _statusColor;
	    private readonly Action _doReload;

	    #endregion

		#region Methods

		public PostItemViewModel(IDataModel dataModel, Post post, Action doReload = null)
	    {
		    _post = post;
		    _dataModel = dataModel;
		    _doReload = doReload;
		    IsStatusShown = true;
			Task.Run(() => Init());
	    }

		private void Init()
		{
			RequestCount = _post.RequestCount;
			IsRequestIconActivated = _post.IsMyPost ? RequestCount > 0 : _post.IsRequested;
			CategoryName = _post.Category.CategoryName;
			AvatarUrl = _post.User.AvatarUrl;
			UserName = _post.User.FullName ?? AppConstants.DefaultUserName;
			CreatedTime = TimeHelper.ToTimeAgo(_post.CreatedTime);
			Address = _post.ProvinceCity.ProvinceCityName;
			PostTitle = _post.Title;
			PostImage = _post.Images.FirstOrDefault()?.ResizedImage;
			HasManyPostPhotos = _post.Images.Count > 1;
			AppreciationCount = _post.AppreciationCount;
			CommentCount = _post.CommentCount;
			IsSeparatorLineShown = true;
			BackgroundColor = _post.Category.BackgroundColor;
			Status = _post.IsMyPost ? _post.PostStatus.Translate() : _post.RequestedPostStatus?.Translate();
			StatusColor = ColorHelper.GetStatusColor(Status);
			_postId = _post.PostId;
		}


		private async Task ShowMenuView()
        {
	        var postOptions = MenuOptionHelper.GetMenuOptions(Status);

			var result = await NavigationService.Navigate<PopupExtensionOptionViewModel, List<string>, string>(postOptions);

			if (string.IsNullOrEmpty(result))
			{
				return;
			}

			switch (result)
			{
				case AppConstants.MarkGiving:
					await ChangeStatusOfPost(null, AppConstants.GivingStatusEN);
					break;

				case AppConstants.MarkGiven:
					await ChangeStatusOfPost(AppConstants.ConfirmChangeStatusOfPost, AppConstants.GivedStatusEN);
					break;

				case AppConstants.MarkReceived:
					await ChangeStatusOfPost(null, AppConstants.ReceivedStatusEN);
					break;

				case AppConstants.ModifyPost:
					await EditPost();
					break;

				case AppConstants.ViewPostRequests:
					await NavigationService.Navigate<RequestsViewModel, Post, bool>(_post);
					break;

				case AppConstants.DeletePost:
					await DeletePost();
					break;

				case AppConstants.CancelRequest:
					await CancelRequest();
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

	    private async Task EditPost()
	    {
		    _dataModel.CurrentPost = _post;
		    var result = await NavigationService.Navigate<CreatePostViewModel, ViewMode, bool>(ViewMode.EditPost);
		    if (!result)
		    {
			    return;
		    }
		    _doReload?.Invoke();
		}

	    private async Task CancelRequest()
	    {
		    var popupResult =
			    await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.CancelRequestConfrim);
		    if (popupResult != RequestStatus.Submitted)
		    {
			    return;
		    }
		    await ManagementService.CancelUserRequest(_postId, _dataModel.LoginResponse.Token);
		    _doReload?.Invoke();
		}

	    private async Task ShowPostDetailView()
		{
			var result = await NavigationService.Navigate<PostDetailViewModel, Post, bool>(_post);
			_post = _dataModel.CurrentPost;
			Init();
			if (result)
			{
				_doReload?.Invoke();
			}
		}

	    private async Task ShowGiverProfile()
	    {
		    if (_post.IsMyPost)
		    {
			    ShowProfileTab?.Invoke();
			}
		    else
		    {
			    await NavigationService.Navigate<UserProfileViewModel, User>(_post.User);
			}
		}

	    private async Task ChangeStatusOfPost(string warningMessage, string status)
	    {
		    if (!string.IsNullOrEmpty(warningMessage))
		    {
				var userConfirmation = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(warningMessage);
			    if (userConfirmation != RequestStatus.Submitted)
			    {
				    return;
			    }
			}
		    await ManagementService.ChangeStatusOfPost(_postId, status, _dataModel.LoginResponse.Token);
		    _doReload?.Invoke();
	    }

		#endregion
	}
}