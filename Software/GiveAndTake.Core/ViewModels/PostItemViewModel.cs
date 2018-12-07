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

	    public string RequestedPostStatus
	    {
		    get => _requestedPostStatus;
		    set => SetProperty(ref _requestedPostStatus, value);
	    }

	    public MvxColor RequestedPostStatusColor
	    {
		    get => _requestedPostStatusColor;
		    set => SetProperty(ref _requestedPostStatusColor, value);
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

	    private static readonly List<string> MyPostOptions = new List<string>
	    {
		    AppConstants.ChangePostStatus,
		    AppConstants.ModifyPost,
		    AppConstants.ViewPostRequests,
		    AppConstants.DeletePost
	    };

		private static readonly List<string> OtherPostOptions = new List<string> { AppConstants.ReportPost };
	    private List<string> _myPostOptions;
	    private List<string> _otherPostOptions;
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
	    private string _requestedPostStatus;
	    private int _requestCount;
	    private int _appreciationCount;
	    private int _commentCount;
	    private bool _hasManyPostPhotos;
	    private bool _isSeparatorLineShown;
	    private bool _isRequested;
	    private string _statusChange;
	    private UserRequest _userRequestResponse;
		private IMvxCommand _showGiverProfileCommand;
	    private IMvxCommand _showPostDetailCommand;
	    private IMvxCommand _showMenuPopupCommand;

	    private readonly IDataModel _dataModel;

		private readonly Post _post;
	    private MvxColor _requestedPostStatusColor;
	    private readonly Action _doReload;
		#endregion

		#region Methods

		public PostItemViewModel(IDataModel dataModel, Post post, Action doReload = null)
	    {
		    _post = post;
		    _dataModel = dataModel;
		    _doReload = doReload;
		    Task.Run(async () => await Init());
	    }

	    private async Task Init()
	    {
		    _userRequestResponse = await ManagementService.CheckUserRequest(_post.PostId, _dataModel.LoginResponse.Token);
		    IsRequested = _userRequestResponse.IsRequested;
		    if (_post.IsMyPost)
		    {
			    _isRequested = RequestCount != 0;
		    }
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
		    //IsRequested = _post.IsRequested;
		    RequestedPostStatus = _post.RequestedPostStatus?.Translate();
		    RequestedPostStatusColor = ColorHelper.GetStatusColor(_post.RequestedPostStatus);

			_postId = _post.PostId;
		    _statusChange = Status == AppConstants.GivingStatus ? AppConstants.GivedStatus : AppConstants.GivingStatus;
		}


        private async Task ShowMenuView()
	    {
		    _myPostOptions = GetMyPostOptions();
		    _otherPostOptions = IsRequested ? GetOtherPostOptions() : OtherPostOptions;
		    var postOptions = _post.IsMyPost ? _myPostOptions : _otherPostOptions;

			var result = await NavigationService.Navigate<PopupExtensionOptionViewModel, List<string>, string>(postOptions);

			if (string.IsNullOrEmpty(result))
			{
				return;
			}

		    if (result.Equals(_myPostOptions[0]))
		    {
				ChangeStatus();
			}

			else switch (result)
			{
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

	    private List<string> GetMyPostOptions() => new List<string>
	    {
		    $"Chuyển trạng thái sang \"{_statusChange}\"",
		    AppConstants.ModifyPost,
		    AppConstants.ViewPostRequests,
		    AppConstants.DeletePost
	    };

	    private List<string> GetOtherPostOptions() => new List<string>()
	    {
			AppConstants.CancelRequest,
			AppConstants.ReportPost,
	    };

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

	    private async void ChangeStatus()
	    {
		    if (_status == AppConstants.GivingStatus)
		    {
			    if (_isRequested)
			    {
				    var userConfirmation = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.ConfirmChangeStatusOfPost);
				    if (userConfirmation != RequestStatus.Submitted)
				    {
					    return;
				    }
			    }
			    await ManagementService.ChangeStatusOfPost(_postId, AppConstants.GivedStatusEN, _dataModel.LoginResponse.Token);
			}
		    else
		    {
			    await ManagementService.ChangeStatusOfPost(_postId, AppConstants.GivingStatusEN, _dataModel.LoginResponse.Token);
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
			RequestCount = Mvx.Resolve<IDataModel>().CurrentPost.RequestCount;
			IsRequested = Mvx.Resolve<IDataModel>().CurrentPost.IsRequested;
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

		#endregion
	}
}