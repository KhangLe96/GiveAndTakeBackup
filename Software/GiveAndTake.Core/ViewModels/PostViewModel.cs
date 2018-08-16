using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels
{
    public class PostViewModel : BaseViewModel
    {
        #region Properties

        public Post Post { get; set; }

        private string categoryName;

        public string CategoryName
        {
            get => categoryName;
            set
            {
                categoryName = value;
                RaisePropertyChanged(() => CategoryName);
            }
        }

        private string userName;

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        private string avatarUrl;

        public string AvatarUrl
        {
            get => avatarUrl;
            set
            {
                avatarUrl = value;
                RaisePropertyChanged(() => AvatarUrl);
            }
        }

        private string description;

        public string Description
        {
            get => description;
            set
            {
                description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        private string createdTime;

        public string CreatedTime
        {
            get => createdTime;
            set
            {
                createdTime = value;
                RaisePropertyChanged(() => CreatedTime);
            }
        }

        private string address;

        public string Address
        {
            get => address;
            set
            {
                address = value;
                RaisePropertyChanged(() => Address);
            }
        }

        private string postImage;

        public string PostImage
        {
            get => postImage;
            set
            {
                postImage = value;
                RaisePropertyChanged(() => PostImage);
            }
        }

        private int requestCount;

        public int RequestCount
        {
            get => requestCount;
            set
            {
                requestCount = value;
                RaisePropertyChanged(() => RequestCount);
            }
        }

        private int appreciationCount;

        public int AppreciationCount
        {
            get => appreciationCount;
            set
            {
                appreciationCount = value;
                RaisePropertyChanged(() => AppreciationCount);
            }
        }

        private int commentCount;

        public int CommentCount
        {
            get => commentCount;
            set
            {
                commentCount = value;
                RaisePropertyChanged(() => CommentCount);
            }
        }

        private bool hasManyPostPhotos;

        public bool HasManyPostPhotos
        {
            get => hasManyPostPhotos;
            set
            {
                hasManyPostPhotos = value;
                RaisePropertyChanged(() => HasManyPostPhotos);
            }
        }

        #endregion

        #region Constructor

        public PostViewModel(Post post)
        {
            InitFields(post);
            InitCommand();
        }

        private void InitFields(Post post)
        {
            Post = post;
            CategoryName = post.Category.CategoryName;
            AvatarUrl = post.User.AvatarUrl;
            UserName = post.User.FullName;
            CreatedTime = post.CreatedTime.ToShortDateString();
            Address = post.ProvinceCity.ProvinceCityName;
            Description = post.Description;
            //PostImage = post.Images.FirstOrDefault();
            //HasManyPostPhotos = post.Images.Count > 1;
            HasManyPostPhotos = true;
            AppreciationCount = post.AppreciationCount;
            RequestCount = post.RequestCount;
            CommentCount = post.CommentCount;
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