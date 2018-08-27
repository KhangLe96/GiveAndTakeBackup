using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.TabNavigation;
using MvvmCross.Commands;

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

        private string _description;

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
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

	    private bool _isLastViewInList;

	    public bool IsLastViewInList
	    {
		    get => _isLastViewInList;
		    set => SetProperty(ref _isLastViewInList, value);
	    }

		#endregion

		#region Constructor

		public PostItemViewModel(Post post, bool isLast = false)
		{
			_post = post;
			Init();
			InitCommand();
			IsLastViewInList = isLast;
		}

        private void Init()
        {
            CategoryName = _post.Category.CategoryName;
            AvatarUrl = _post.User.AvatarUrl;
            UserName = _post.User.FullName;
            CreatedTime = _post.CreatedTime.ToShortDateString();
            Address = _post.ProvinceCity.ProvinceCityName;
            Description = _post.Description;
            //PostImage = post.Images.FirstOrDefault();
            //HasManyPostPhotos = post.Images.Count > 1;
            HasManyPostPhotos = true;
            AppreciationCount = _post.AppreciationCount;
            RequestCount = _post.RequestCount;
            CommentCount = _post.CommentCount;
        }

        private void InitCommand()
        {
            ShowGiverProfileCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<UserProfileViewModel>());
            ShowPostDetailCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<PostDetailViewModel>());
        }

        #endregion

        #region Methods

        public IMvxAsyncCommand ShowGiverProfileCommand { get; set; }
        public IMvxAsyncCommand ShowPostDetailCommand { get; set; }

        #endregion
    }
}