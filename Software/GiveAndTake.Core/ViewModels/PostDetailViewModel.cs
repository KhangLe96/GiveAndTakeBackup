using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;
using I18NPortable;

namespace GiveAndTake.Core.ViewModels
{
	public class PostDetailViewModel : BaseViewModel<Post, bool>
    {
		#region Properties

	    private Post _post;
		private string _categoryName;
	    private string _address;
	    private string _status;
	    private List<Image> _postImages;
	    private int _requestCount;
	    private int _commentCount;
	    private string _categoryBackgroundColor;
	    private string _avatarUrl;
	    private string _userName;
	    private string _createdTime;
	    private string _postTitle;
	    private string _postDescription;
		private bool _isMyPost;
	    private int _postImageIndex;
	    private bool _canNavigateLeft;
	    private bool _canNavigateRight;
	    private string _imageIndexIndicator;

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
			ShowMyRequestListCommand = new MvxCommand(ShowMyRequestList);
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

		private void ShowMyRequestList()
		{
			if (_isMyPost)
			{
				NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage);
			}
			else
			{
				NavigationService.Navigate<PopupCreateRequestViewModel, Post>(_post);
			}
		}


		public override void Prepare(Post post)
		{
			_post = post;
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
		    ImageIndexIndicator = _postImageIndex + 1 + " / " + _postImages.Count;
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
