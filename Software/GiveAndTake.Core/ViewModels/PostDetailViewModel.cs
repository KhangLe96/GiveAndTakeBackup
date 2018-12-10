using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Core.Exceptions;
using GiveAndTake.Core.Helpers;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using I18NPortable;
using MvvmCross.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

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

		public IMvxCommand ShowMyRequestListCommand =>
			_showMyRequestListCommand ?? (_showMyRequestListCommand = new MvxAsyncCommand(ShowMyRequestList));

		public IMvxCommand<int> ShowFullImageCommand =>
			_showFullImageCommand ?? (_showFullImageCommand = new MvxCommand<int>(ShowFullImage));

		public IMvxCommand NavigateLeftCommand =>
			_navigateLeftCommand ?? (_navigateLeftCommand = new MvxCommand(() => PostImageIndex--));

		public IMvxCommand NavigateRightCommand =>
			_navigateRightCommand ?? (_navigateRightCommand = new MvxCommand(() => PostImageIndex++));

		public IMvxCommand<int> UpdateImageIndexCommand =>
			_updateImageIndexCommand ?? (_updateImageIndexCommand = new MvxCommand<int>(index => PostImageIndex = index));

		public IMvxCommand BackPressedCommand =>
			_backPressedCommand ?? (_backPressedCommand = new MvxCommand(() => NavigationService.Close(this, IsLoadInHomeView)));

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
		public bool IsLoadInHomeView
		{
			get => _isLoadInHomeView;
			set => SetProperty(ref _isLoadInHomeView, value);
		}

		public List<ITransformation> AvatarTransformations => new List<ITransformation> { new CircleTransformation() };

		private static readonly List<string> OtherPostOptions = new List<string> { AppConstants.ReportPost };

		private readonly IDataModel _dataModel;

		private IMvxCommand _showGiverProfileCommand;
		private IMvxCommand _showMenuPopupCommand;
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
		private bool _isRequested;
		private string _postId;
		private readonly ILoadingOverlayService _overlay;
		private bool _isBackFromFullImage;
		private string _statusChange;
		private List<string> _myPostOptions;
		private List<string> _otherPostOptions;
		private bool _isLoadFirstTime = true;
		private bool _isLoadInHomeView;
		#endregion

		#region Constructor

		public PostDetailViewModel(IDataModel dataModel, ILoadingOverlayService loadingOverlayService)
		{
			_dataModel = dataModel;
			_overlay = loadingOverlayService;
		}

		private void ShowFullImage(int position)
		{
			PostImageIndex = position;
			NavigationService.Navigate<PostImageViewModel, bool>()
				.ContinueWith(task => PostImageIndex = _dataModel.PostImageIndex);
			_isBackFromFullImage = true;
		}

		private async void ShowMenuView()
		{
			_myPostOptions = GetMyPostOptions();
			_otherPostOptions = IsRequested ? GetOtherPostOptions() : OtherPostOptions;
			var postOptions = _isMyPost ? _myPostOptions : _otherPostOptions;

			var result = await NavigationService.Navigate<PopupExtensionOptionViewModel, List<string>, string>(postOptions);

			if (string.IsNullOrEmpty(result))
			{
				return;
			}

			if (result.Equals(_myPostOptions[0]))
			{
				ChangeStatusOfPost();
			}
			else switch (result)
				{
					case AppConstants.ModifyPost:
						await NavigationService.Navigate<CreatePostViewModel, ViewMode, bool>(ViewMode.EditPost);
						await LoadCurrentPostDataWithOverlay(AppConstants.LoadingDataOverlayTitle);
						break;
					case AppConstants.ViewPostRequests:
						await NavigationService.Navigate<RequestsViewModel, Post, bool>(_post);
						break;
					case AppConstants.DeletePost:
						await DeletePost();
						break;
					case AppConstants.CancelRequest:
						await CancelOldRequest();
						break;
					case AppConstants.ReportPost:
						await NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage);
						break;
				}
		}

		private async Task DeletePost()
		{
			var userConfirmation = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.ConfirmDeletePost);
			if (userConfirmation == RequestStatus.Cancelled)
			{
				return;
			}
			await ChangeStatus(AppConstants.DeletedStatus);
			await Task.Delay(777); //for iOS
			await NavigationService.Close(this, true);
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

		private async Task ShowMyRequestList()
		{
			if (Status == AppConstants.GivedStatus)
			{
				await NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage);
			}
			else
			{
				if (_isMyPost)
				{
					_post.IsMyPost = _isMyPost;
					await NavigationService.Navigate<RequestsViewModel, Post, bool>(_post);
					await LoadCurrentPostDataWithOverlay(AppConstants.LoadingDataOverlayTitle);
				}
				else
				{
					if (IsRequested)
					{
						await ReviewMyRequest();
					}
					else
					{
						await CreateNewRequest();
					}
				}
			}
		}

		private async Task CreateNewRequest()
		{
			var result = await NavigationService.Navigate<PopupCreateRequestViewModel, Post, RequestStatus>(_post);
			if (result == RequestStatus.Submitted)
			{
				while (true)
				{
					try
					{
						await _overlay.ShowOverlay(AppConstants.UpdateOverLayTitle);
						_dataModel.CurrentPost = await ManagementService.GetPostDetail(_postId, _dataModel.LoginResponse.Token);
						IsRequested = _dataModel.CurrentPost.IsRequested;
						CommentCount = _dataModel.CurrentPost.CommentCount;
						RequestCount = _dataModel.CurrentPost.RequestCount;
						break;
					}
					catch (AppException.ApiException)
					{
						await NavigationService.Navigate<PopupWarningViewModel, string, bool>(AppConstants
							.ErrorConnectionMessage);
					}
					finally
					{
						await _overlay.CloseOverlay();
					}
				}
			}
		}

		private async void ChangeStatusOfPost()
		{
			if (_status == AppConstants.GivingStatus)
			{
				if (IsRequested)
				{
					var userConfirmation = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.ConfirmChangeStatusOfPost);
					if (userConfirmation != RequestStatus.Submitted)
					{
						return;
					}
				}
				await ChangeStatus(AppConstants.GivedStatusEN);
			}
			else
			{
				await ChangeStatus(AppConstants.GivingStatusEN);
			}
		}

		private async Task ChangeStatus(string inputStatus)
		{
			await _overlay.ShowOverlay(AppConstants.ProcessingDataOverLayTitle);
			await ManagementService.ChangeStatusOfPost(_postId, inputStatus, _dataModel.LoginResponse.Token);
			if (inputStatus != AppConstants.DeletedStatus)
			{
				await LoadCurrentPostData();
			}
			await _overlay.CloseOverlay();
		}

		private async Task CancelOldRequest()
		{
			var popupResult =
				await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(
					"\nBạn có chắc chắn muốn bỏ yêu cầu ?\n");
			if (popupResult != RequestStatus.Submitted)
			{
				return;
			}

			await _overlay.ShowOverlay(AppConstants.UpdateOverLayTitle);
			try
			{
				await ManagementService.CancelUserRequest(_postId, _dataModel.LoginResponse.Token);
				await LoadCurrentPostData();
			}
			catch (AppException.ApiException)
			{
				await NavigationService.Navigate<PopupWarningViewModel, string, bool>(AppConstants
					.ErrorConnectionMessage);
			}
			finally
			{
				await _overlay.CloseOverlay();
			}
		}
		private async Task ReceiveGift(string requestId)
		{
			await _overlay.ShowOverlay(AppConstants.UpdateOverLayTitle);
			try
			{
				await ManagementService.ChangeStatusOfRequest(requestId, "Received", _dataModel.LoginResponse.Token);
				await LoadCurrentPostData();
			}
			catch (AppException.ApiException)
			{
				await NavigationService.Navigate<PopupWarningViewModel, string, bool>(AppConstants
					.ErrorConnectionMessage);
			}
			finally
			{
				await _overlay.CloseOverlay();
			}
		}
		private async Task ReviewMyRequest()
		{
			await _overlay.ShowOverlay(AppConstants.UpdateOverLayTitle);
			try
			{
				Guid postID = new Guid(_postId);
				var request =
					await ManagementService.GetRequestOfCurrentUserByPostId(postID, _dataModel.LoginResponse.Token);
				await _overlay.CloseOverlay();

				string requestStatus = request.RequestStatus;
				PopupMyRequestStatusResult popupResult = PopupMyRequestStatusResult.Cancelled;
				switch (requestStatus)
				{
					case "Pending":
						popupResult = await NavigationService
							.Navigate<MyRequestPendingViewModel, Request, PopupMyRequestStatusResult>(request);
						break;
					case "Received":
						popupResult = await NavigationService
							.Navigate<MyRequestReceivedViewModel, Request, PopupMyRequestStatusResult>(request);
						break;
					case "Approved":
						popupResult = await NavigationService
							.Navigate<MyRequestApprovedViewModel, Request, PopupMyRequestStatusResult>(request);
						break;
					case "Rejected":
						await CreateNewRequest();
						break;
				}

				switch (popupResult)
				{
					case PopupMyRequestStatusResult.Received:
						await ReceiveGift(request.Id);
						break;
					case PopupMyRequestStatusResult.Removed:
						await CancelOldRequest();
						break;
					case PopupMyRequestStatusResult.Cancelled:
						return;
				}
			}
			catch (AppException.ApiException)
			{
				await NavigationService.Navigate<PopupWarningViewModel, string, bool>(AppConstants
					.ErrorConnectionMessage);
			}
			finally
			{
				await _overlay.CloseOverlay();
			}
		}

		public override void Prepare(Post post)
		{
			_postId = post.PostId;
			PostImages = post.Images;
			_isMyPost = post.IsMyPost;
		}

		public override async void ViewAppearing()
		{
			base.ViewAppearing();

			if (!_isBackFromFullImage && _isLoadFirstTime)
			{
				await LoadCurrentPostDataWithOverlay(AppConstants.LoadingDataOverlayTitle);
			}
			_isBackFromFullImage = false;
			_isLoadFirstTime = false;
		}

		private async Task LoadCurrentPostData()
		{
			_dataModel.CurrentPost = await ManagementService.GetPostDetail(_postId, _dataModel.LoginResponse.Token);
			IsRequested = _dataModel.CurrentPost.IsRequested;
			RequestCount = _dataModel.CurrentPost.RequestCount;
			if (_isMyPost)
			{
				IsRequested = RequestCount != 0;
			}
			CategoryName = _dataModel.CurrentPost.Category.CategoryName;
			AvatarUrl = _dataModel.CurrentPost.User.AvatarUrl;
			UserName = _dataModel.CurrentPost.User.FullName ?? AppConstants.DefaultUserName;
			CreatedTime = TimeHelper.ToTimeAgo(_dataModel.CurrentPost.CreatedTime);
			Address = _dataModel.CurrentPost.ProvinceCity.ProvinceCityName;
			PostDescription = _dataModel.CurrentPost.Description;
			PostTitle = _dataModel.CurrentPost.Title;
			PostImages = _dataModel.CurrentPost.Images;
			CommentCount = _dataModel.CurrentPost.CommentCount;
			Status = _isMyPost ? _dataModel.CurrentPost.PostStatus.Translate() : " ";
			CategoryBackgroundColor = _dataModel.CurrentPost.Category.BackgroundColor;
			PostImageIndex = 0;
			_post = _dataModel.CurrentPost;
			_statusChange = Status == AppConstants.GivingStatus ? AppConstants.GivedStatus : AppConstants.GivingStatus;
		}

		public override Task Initialize()
		{
			_dataModel.PostImages = _dataModel.PostImages;
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

		private async Task LoadCurrentPostDataWithOverlay(string overlayTitle)
		{
			try
			{
				await _overlay.ShowOverlay(overlayTitle);
				await LoadCurrentPostData();
			}
			catch (AppException.ApiException)
			{
				await NavigationService.Navigate<PopupWarningViewModel, string, bool>(AppConstants.ErrorConnectionMessage);
				await Task.Delay(777);//for iphone
				await LoadCurrentPostDataWithOverlay(overlayTitle);
			}
			finally
			{
				await _overlay.CloseOverlay();
			}
		}
		#endregion
	}
}
