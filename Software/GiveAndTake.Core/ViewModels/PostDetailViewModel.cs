using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;
using GiveAndTake.Core.Services;
using I18NPortable;
using MvvmCross;

namespace GiveAndTake.Core.ViewModels
{
	public class PostDetailViewModel : BaseViewModel<Post, bool>
    {
		#region Properties

	    private UserRequest _userRequestResponse;
	    private Post _post;
	    private List<Image> _postImages;
	    private int _requestCount;
	    private int _commentCount;
	    private int _postImageIndex;
		private string _postId;
	    private string _categoryName;
	    private string _address;
	    private string _status;
		private string _categoryBackgroundColor;
	    private string _avatarUrl;
	    private string _userName;
	    private string _createdTime;
	    private string _postTitle;
	    private string _postDescription;
	    private string _imageIndexIndicator;
		private bool _isMyPost;
	    private bool _canNavigateLeft;
	    private bool _canNavigateRight;
	    private bool _isRequested;

		public string CategoryName
		{
			get => $"   {_categoryName}   ";
			set => SetProperty(ref _categoryName, value);
		}

		public string Address
		{
			get => _address;
			set => SetProperty(ref _address, value);
		}

		public string Status
		{
			get => _status;
			set => SetProperty(ref _status, value);
		}

		public List<Image> PostImages
		{
			get => _postImages;
			set => SetProperty(ref _postImages, value);
		}

		public int RequestCount
		{
			get => _requestCount;
			set => SetProperty(ref _requestCount, value);
		}

		public int CommentCount
		{
			get => _commentCount;
			set => SetProperty(ref _commentCount, value);
		}

		public string CategoryBackgroundColor
		{
			get => _categoryBackgroundColor;
			set => SetProperty(ref _categoryBackgroundColor, value);
		}

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

		public string CreatedTime
		{
			get => _createdTime;
			set => SetProperty(ref _createdTime, value);
		}

		public string PostTitle
		{
			get => _postTitle;
			set => SetProperty(ref _postTitle, value);
		}

		public string PostDescription
		{
			get => _postDescription;
			set => SetProperty(ref _postDescription, value);
		}

	    public int PostImageIndex
	    {
		    get => _postImageIndex;
		    set
		    {
			    SetProperty(ref _postImageIndex, value);
			    _dataModel.PostImageIndex = value;
			    UpdateImageIndexIndicator();
				UpdateNavigationButtons();
		    }
	    }

	    public bool CanNavigateLeft
	    {
		    get => _canNavigateLeft;
		    set => SetProperty(ref _canNavigateLeft, value);
	    }

	    public bool CanNavigateRight
	    {
		    get => _canNavigateRight;
		    set => SetProperty(ref _canNavigateRight, value);
	    }

	    public string ImageIndexIndicator 
	    {
		    get => _imageIndexIndicator;
		    set => SetProperty(ref _imageIndexIndicator, value);
	    }

	    public bool IsRequested
	    {
		    get => _isRequested;
		    set => SetProperty(ref _isRequested, value);
	    }

		public List<ITransformation> AvatarTransformations => new List<ITransformation> { new CircleTransformation() };

	    private static readonly List<string> MyPostOptions = new List<string>
	    {
		    AppConstants.ChangePostStatus,
		    AppConstants.ModifyPost,
		    AppConstants.ViewPostRequests,
		    AppConstants.DeletePost
	    };

	    private static readonly List<string> OtherPostOptions = new List<string> { AppConstants.ReportPost };

		private readonly IDataModel _dataModel;

	    #endregion

		#region Constructor

		public PostDetailViewModel(IDataModel dataModel)
		{
			_dataModel = dataModel;
			InitCommand();
		}

		private void InitCommand()
		{
			CloseCommand = new MvxAsyncCommand(() => NavigationService.Close(this, false));
			ShowMenuPopupCommand = new MvxCommand(ShowMenuView);
			ShowPostCommentCommand = new MvxCommand(async () =>
				await NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage));
			ShowMyRequestListCommand = new MvxAsyncCommand(ShowMyRequestList);
			ShowFullImageCommand = new MvxCommand<int>(ShowFullImage);
			NavigateLeftCommand = new MvxCommand(() => PostImageIndex--);
			NavigateRightCommand = new MvxCommand(() => PostImageIndex++);
			UpdateImageIndexCommand = new MvxCommand<int>(index => PostImageIndex = index);
			ShowGiverProfileCommand = new MvxAsyncCommand(async () => 
				await NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage));
			BackPressedCommand = new MvxCommand(() => NavigationService.Close(this, true));
		}

		private void ShowFullImage(int position)
		{
			PostImageIndex = position;
			NavigationService.Navigate<PostImageViewModel, bool>().ContinueWith(task => PostImageIndex = _dataModel.PostImageIndex);
		}

		private async void ShowMenuView()
		{
			var postOptions = _isMyPost ? AppConstants.MyPostOptions : AppConstants.OtherPostOptions;

			var result = await NavigationService.Navigate<PopupExtensionOptionViewModel, List<string>, string>(postOptions);

			if (string.IsNullOrEmpty(result)) return;

			switch (result)
			{
				case AppConstants.ChangePostStatus:
					await NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage);
					break;

				case AppConstants.ModifyPost:
					await NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage);
					break;

				case AppConstants.ViewPostRequests:
					await NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage);
					break;

				case AppConstants.DeletePost:
					await NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage);
					break;
				case AppConstants.ReportPost:
					await NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage);
					break;
			}
		}

	    private async void CheckUserRequest()
	    {
		    var managementService = Mvx.Resolve<IManagementService>();
		    _userRequestResponse = await managementService.CheckUserRequest(_postId, _dataModel.LoginResponse.Token);
		    IsRequested = _userRequestResponse.IsRequested;
	    }

		private async Task ShowMyRequestList()
		{
			if (_isMyPost)
			{
				await NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage);
			}
			else
			{
				if (IsRequested)
				{
					var popupResult = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>("\nBạn có chắc chắn muốn bỏ yêu cầu ?\n");
					if (popupResult != RequestStatus.Submitted) return;
					var managementService = Mvx.Resolve<IManagementService>();
					await managementService.CancelUserRequest(_postId, _dataModel.LoginResponse.Token);
					UpdateDataModel();
				}
				else
				{
					var result = await NavigationService.Navigate<PopupCreateRequestViewModel, Post, RequestStatus>(_post);
					if (result == RequestStatus.Submitted)
					{
						UpdateDataModel();
					}
				}
			}
		}

		public override void Prepare(Post post)
		{
			_postId = post.PostId;
			CategoryName = post.Category.CategoryName;
			AvatarUrl = post.User.AvatarUrl;
			UserName = post.User.FullName ?? AppConstants.DefaultUserName;
			CreatedTime = post.CreatedTime.ToString("dd.MM.yyyy");
			Address = post.ProvinceCity.ProvinceCityName;
			PostDescription = post.Description;
			PostTitle = post.Title;
			PostImages = post.Images;
			RequestCount = post.RequestCount;
			CommentCount = post.CommentCount;
			Status = post.IsMyPost ? post.PostStatus.Translate() : " ";
			CategoryBackgroundColor = post.Category.BackgroundColor;
			_isMyPost = post.IsMyPost;
			PostImageIndex = 0;
			_post = post;

			CheckUserRequest();
		}

	    private async void UpdateDataModel()
	    {
		    var managementService = Mvx.Resolve<IManagementService>();
		    _dataModel.CurrentPost = await managementService.GetPostDetail(_postId);
		    CategoryName = _dataModel.CurrentPost.Category.CategoryName;
		    AvatarUrl = _dataModel.CurrentPost.User.AvatarUrl;
		    UserName = _dataModel.CurrentPost.User.FullName ?? AppConstants.DefaultUserName;
		    CreatedTime = _dataModel.CurrentPost.CreatedTime.ToString("dd.MM.yyyy");
		    Address = _dataModel.CurrentPost.ProvinceCity.ProvinceCityName;
		    PostDescription = _dataModel.CurrentPost.Description;
		    PostTitle = _dataModel.CurrentPost.Title;
		    PostImages = _dataModel.CurrentPost.Images;
		    RequestCount = _dataModel.CurrentPost.RequestCount;
		    CommentCount = _dataModel.CurrentPost.CommentCount;
		    Status = _dataModel.CurrentPost.IsMyPost ? _dataModel.CurrentPost.PostStatus.Translate() : " ";
		    CategoryBackgroundColor = _dataModel.CurrentPost.Category.BackgroundColor;
		    _isMyPost = _dataModel.CurrentPost.IsMyPost;
		    PostImageIndex = 0;
		    _post = _dataModel.CurrentPost;

		    CheckUserRequest();
		}

	    public override Task Initialize()
	    {
		    _dataModel.PostImages = PostImages;
		    _dataModel.PostImageIndex = 0;
		    return base.Initialize();
	    }

	    private void UpdateNavigationButtons()
	    {
		    CanNavigateLeft = PostImages.Count > 1 && PostImageIndex > 0;
		    CanNavigateRight = PostImages.Count > 1 && PostImageIndex < PostImages.Count - 1;
	    }

	    private void UpdateImageIndexIndicator()
	    {
		    var totalImage = _postImages.Count == 0 ? 1 : PostImages.Count;
		    ImageIndexIndicator = _postImageIndex + 1 + " / " + totalImage;
	    }
		#endregion

		#region Methods

	    public IMvxAsyncCommand ShowGiverProfileCommand { get; set; }
		public IMvxAsyncCommand CloseCommand { get; set; }
		public IMvxCommand ShowMenuPopupCommand { get; set; }
		public IMvxCommand ShowPostCommentCommand { get; set; }
		public IMvxCommand ShowMyRequestListCommand { get; set; }
		public IMvxCommand<int> ShowFullImageCommand { get; set; }
		public IMvxCommand NavigateLeftCommand { get; set; }
	    public IMvxCommand NavigateRightCommand { get; set; }
	    public IMvxCommand<int> UpdateImageIndexCommand { get; set; }
	    public IMvxCommand BackPressedCommand { get; set; }

	    #endregion
	}
}
