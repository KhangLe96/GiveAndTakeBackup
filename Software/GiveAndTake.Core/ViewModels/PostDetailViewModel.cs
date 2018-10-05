using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross.Commands;
using System.Collections.Generic;
using I18NPortable;

namespace GiveAndTake.Core.ViewModels
{
	public class PostDetailViewModel : BaseViewModel<Post, bool>
    {
		#region Properties

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

		public List<ITransformation> AvatarTransformations => new List<ITransformation> { new CircleTransformation() };

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
		}

		private void ShowFullImage(int position)
		{
			_dataModel.PostImages = PostImages;
			_dataModel.PostImageIndex = position;
			NavigationService.Navigate<PostImageViewModel>();
		}

		private void ShowMenuView()
		{
			if (_isMyPost)
			{
				NavigationService.Navigate<PopupPostOptionViewModel>();
			}
			else
			{
				NavigationService.Navigate<PopupReportViewModel>();
			}
		}

		private void ShowMyRequestList()
		{
			if (_isMyPost)
			{
				NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage);
			}
		}


		public override void Prepare(Post post)
		{
			CategoryName = post.Category.CategoryName;
			AvatarUrl = post.User.AvatarUrl ?? AppConstants.DefaultAvatar;
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
		}

		#endregion

		#region Methods

	    public IMvxAsyncCommand CloseCommand { get; set; }
		public IMvxCommand ShowMenuPopupCommand { get; set; }
		public IMvxCommand ShowPostCommentCommand { get; set; }
		public IMvxCommand ShowMyRequestListCommand { get; set; }
		public IMvxCommand<int> ShowFullImageCommand { get; set; }

		#endregion
	}
}
