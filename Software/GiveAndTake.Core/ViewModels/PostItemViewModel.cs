using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using GiveAndTake.Core.ViewModels.TabNavigation;
using MvvmCross.Commands;
using MvvmCross.UI;

namespace GiveAndTake.Core.ViewModels
{
    public class PostItemViewModel : BaseViewModel
    {
        #region Properties

        private readonly Post _post;
		private IDataModel _dataModel;

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

		#endregion

		#region Constructor

		public PostItemViewModel(Post post, bool isLast = false) 
		{
			_post = post;
			Init();
			InitCommand();
		}

	    private void Init()
	    {
		    CategoryName = _post.Category.CategoryName;
		    AvatarUrl = _post.User.AvatarUrl ?? AppConstants.DefaultUrl;
		    UserName = _post.User.FullName ?? AppConstants.DefaultUserName;
		    CreatedTime = _post.CreatedTime.ToString("dd.MM.yyyy");
		    Address = _post.ProvinceCity.ProvinceCityName;
		    Description = _post.Description;
		    PostImage = _post.Images.FirstOrDefault()?.ResizedImage ?? AppConstants.DefaultUrl;
			HasManyPostPhotos = _post.Images.Count > 1;
		    AppreciationCount = _post.AppreciationCount;
		    RequestCount = _post.RequestCount;
		    CommentCount = _post.CommentCount;
		    IsLastViewInList = false;
	        BackgroundColor = _post.Category.BackgroundColor;
	    }

	    private void InitCommand()
        {
            ShowGiverProfileCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.DefaultWarningMessage));
            ShowPostDetailCommand = new MvxAsyncCommand(ShowPostDetailView);
			ShowMenuPopupCommand = new MvxAsyncCommand(ShowMenuView);
        }

	    private async Task ShowMenuView()
	    {
		    if (_post.IsMyPost)
		    {
			    await NavigationService.Navigate<PopupPostOptionViewModel>();
		    }
		    else
		    {
				await NavigationService.Navigate<PopupReportViewModel>();
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

        #endregion

        #region Utils

        private int ConvertHexToDec(string hex)
        {
            string color = hex.TrimStart('#');
            string R = color.Substring(2, 2);
            string G = color.Substring(4, 2);
            string B = color.Substring(6, 2);

            int decValue = int.Parse(B + G + R, System.Globalization.NumberStyles.HexNumber);
            return decValue;
        }

        #endregion
    }
}