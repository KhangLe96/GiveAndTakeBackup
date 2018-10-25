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

namespace GiveAndTake.Core.ViewModels
{
	public class PostItemViewModel : BaseViewModel
    {
        #region Properties

        private readonly Post _post;

	    private string _categoryName;

        public string CategoryName 
        {
            get => $"   {_categoryName}   ";
            set => SetProperty(ref _categoryName, value);
        }

        private string _userName;

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        private string _avatarUrl;

        public string AvatarUrl
        {
            get => _avatarUrl;
            set
            {
                _avatarUrl = value;
                RaisePropertyChanged(() => AvatarUrl);
            }
        }

        private string _postTitle;

        public string PostTitle
        {
            get => _postTitle;
            set
            {
                _postTitle = value;
                RaisePropertyChanged(() => PostTitle);
            }
        }

        private string _createdTime;

        public string CreatedTime
        {
            get => _createdTime;
            set
            {
                _createdTime = value;
                RaisePropertyChanged(() => CreatedTime);
            }
        }

        private string _address;

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                RaisePropertyChanged(() => Address);
            }
        }

        private string _postImage;

        public string PostImage
        {
            get => _postImage;
            set
            {
                _postImage = value;
                RaisePropertyChanged(() => PostImage);
            }
        }

        private int _requestCount;

        public int RequestCount
        {
            get => _requestCount;
            set
            {
                _requestCount = value;
                RaisePropertyChanged(() => RequestCount);
            }
        }

        private int _appreciationCount;

        public int AppreciationCount
        {
            get => _appreciationCount;
            set
            {
                _appreciationCount = value;
                RaisePropertyChanged(() => AppreciationCount);
            }
        }

        private int _commentCount;

        public int CommentCount
        {
            get => _commentCount;
            set
            {
                _commentCount = value;
                RaisePropertyChanged(() => CommentCount);
            }
        }

        private bool _hasManyPostPhotos;

        public bool HasManyPostPhotos
        {
            get => _hasManyPostPhotos;
            set
            {
                _hasManyPostPhotos = value;
                RaisePropertyChanged(() => HasManyPostPhotos);
            }
        }

	    private bool _isSeparatorLineShown;

	    public bool IsSeparatorLineShown
	    {
		    get => _isSeparatorLineShown;
		    set => SetProperty(ref _isSeparatorLineShown, value);
	    }

        private string _backgroundColor;

        public string BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                RaisePropertyChanged(() => BackgroundColor);
            }
        }

	    public List<ITransformation> PostTransformations => new List<ITransformation> { new CornersTransformation(5 , CornerTransformType.AllRounded) };

	    public List<ITransformation> AvatarTransformations => new List<ITransformation> { new CircleTransformation() };

	    public double DownsampleWidth => 200d;

	    private static readonly List<string> MyPostOptions = new List<string>
	    {
		    AppConstants.ChangePostStatus,
		    AppConstants.ModifyPost,
		    AppConstants.ViewPostRequests,
		    AppConstants.DeletePost
	    };

	    private static readonly List<string> OtherPostOptions = new List<string> { AppConstants.ReportPost };

		#endregion

		#region Constructor

		public PostItemViewModel(Post post) 
		{
			_post = post;
			Init();
			InitCommand();
		}

	    private void Init()
	    {
		    CategoryName = _post.Category.CategoryName;
		    AvatarUrl = _post.User.AvatarUrl;
		    UserName = _post.User.FullName ?? AppConstants.DefaultUserName;
		    CreatedTime = _post.CreatedTime.ToString("dd.MM.yyyy");
		    Address = _post.ProvinceCity.ProvinceCityName;
		    PostTitle = _post.Title;
		    PostImage = _post.Images.FirstOrDefault()?.ResizedImage.Replace("192.168.51.137:8089", "api.chovanhan.asia");
			HasManyPostPhotos = _post.Images.Count > 1;
		    AppreciationCount = _post.AppreciationCount;
		    RequestCount = _post.RequestCount;
		    CommentCount = _post.CommentCount;
		    IsSeparatorLineShown = true;
	        BackgroundColor = _post.Category.BackgroundColor;
	    }

	    private void InitCommand()
        {
            ShowGiverProfileCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage));
            ShowPostDetailCommand = new MvxAsyncCommand(ShowPostDetailView);
			ShowMenuPopupCommand = new MvxAsyncCommand(ShowMenuView);
            ShowRequestListCommand = new MvxAsyncCommand(ShowRequestListView);
        }

        private async Task ShowRequestListView()
        {
            await NavigationService.Navigate<RequestsViewModel>();
        }

        private async Task ShowMenuView()
	    {
			var postOptions = _post.IsMyPost ? AppConstants.MyPostOptions : AppConstants.OtherPostOptions;

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

	    private async Task ShowPostDetailView()
	    {
		    await NavigationService.Navigate<PostDetailViewModel, Post>(_post);
	    }


		#endregion

		#region Methods

		public IMvxAsyncCommand ShowGiverProfileCommand { get; set; }
        public ICommand ShowPostDetailCommand { get; set; }
        public IMvxAsyncCommand ShowMenuPopupCommand { get; set; }
        public ICommand ShowRequestListCommand { get; set; }

        #endregion
    }
}