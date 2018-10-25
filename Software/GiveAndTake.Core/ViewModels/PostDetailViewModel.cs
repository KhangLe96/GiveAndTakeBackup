﻿using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using I18NPortable;
using MvvmCross.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GiveAndTake.Core.ViewModels
{
	public class PostDetailViewModel : BaseViewModel<Post, bool>
    {
		#region Properties

	    public IMvxCommand ShowGiverProfileCommand =>
		    _showGiverProfileCommand ?? (_showGiverProfileCommand = new MvxCommand(() =>
			    NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage)));
		
		public IMvxCommand ShowMenuPopupCommand =>
			_showMenuPopupCommand ?? (_showMenuPopupCommand = new MvxCommand(ShowMenuView));

	    public IMvxCommand ShowPostCommentCommand =>
		    _showPostCommentCommand ?? (_showPostCommentCommand = new MvxCommand(() =>
			    NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage)));

		public IMvxCommand ShowMyRequestListCommand =>
			_showMyRequestListCommand ?? (_showMyRequestListCommand = new MvxCommand(ShowMyRequestList));

		public IMvxCommand<int> ShowFullImageCommand =>
			_showFullImageCommand ?? (_showFullImageCommand = new MvxCommand<int>(ShowFullImage));

		public IMvxCommand NavigateLeftCommand =>
			_navigateLeftCommand ?? (_navigateLeftCommand = new MvxCommand(() => PostImageIndex--));

		public IMvxCommand NavigateRightCommand =>
			_navigateRightCommand ?? (_navigateRightCommand = new MvxCommand(() => PostImageIndex++));

		public IMvxCommand<int> UpdateImageIndexCommand =>
			_updateImageIndexCommand ?? (_updateImageIndexCommand = new MvxCommand<int>(index => PostImageIndex = index));

		public IMvxCommand BackPressedCommand =>
			_backPressedCommand ?? (_backPressedCommand = new MvxCommand(() => NavigationService.Close(this, true)));

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

	    private IMvxCommand _showGiverProfileCommand;
	    private IMvxCommand _showMenuPopupCommand;
	    private IMvxCommand _showPostCommentCommand;
	    private IMvxCommand _showMyRequestListCommand;
	    private IMvxCommand _navigateLeftCommand;
	    private IMvxCommand _navigateRightCommand;
	    private IMvxCommand _backPressedCommand;
	    private IMvxCommand<int> _showFullImageCommand;
	    private IMvxCommand<int> _updateImageIndexCommand;
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
	    private int _requestCount;
	    private int _commentCount;
	    private int _postImageIndex;
	    private bool _canNavigateLeft;
	    private bool _canNavigateRight;
	    private bool _isMyPost;
	    private List<Image> _postImages;
	    private Post _post;

	    #endregion

		#region Constructor

		public PostDetailViewModel(IDataModel dataModel)
		{
			_dataModel = dataModel;
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
	}
}
