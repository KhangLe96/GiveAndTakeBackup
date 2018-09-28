using System.Collections.Generic;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels
{
	public class PostDetailViewModel : BaseViewModel<Post>
    {
		#region Properties

		private string _categoryName;
	    private string _address;
	    private string _status;
		//REVIEW: Rename to _postImages cause It is a List
	    private List<Image> _postImage;
	    private int _requestCount;
	    private int _commentCount;
	    private string _categoryBackgroundColor;
	    private string _avatarUrl;
	    private string _userName;
	    private string _createdTime;
	    private string _postTitle;
	    private string _postDescription;
	    private int _currentPage;

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

	    public List<Image> PostImage
	    {
		    get => _postImage;
		    set => SetProperty(ref _postImage, value);
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

	    public int CurrentPage
	    {
		    get => _currentPage;
		    set => SetProperty(ref _currentPage, value);
	    }

		public List<ITransformation> AvatarTransformations => new List<ITransformation> { new CircleTransformation() };

		#endregion

		#region Constructor

		public PostDetailViewModel()
	    {
			InitCommand();
	    }

	    private void InitCommand()
	    {
			//   ShowMenuPopupCommand = new MvxAsyncCommand(ShowMenuView);
			//ShowPostCommentCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<PopupWarningViewModel>(AppConstants.DefaultWarningMessage));
		}

		//private async Task ShowMenuView()
		//{
		// if (_post.IsMyPost)
		// {
		//  await NavigationService.Navigate<PopupPostOptionViewModel>();
		// }
		// else
		// {
		//  await NavigationService.Navigate<PopupReportViewModel>();
		// }
		//}


		public override void Prepare(Post post)
		{
			CategoryName = post.Category.CategoryName;
			AvatarUrl = post.User.AvatarUrl;
			UserName = post.User.FullName ?? AppConstants.DefaultUserName;
			CreatedTime = post.CreatedTime.ToString("dd.MM.yyyy");
			Address = post.ProvinceCity.ProvinceCityName;
			PostDescription = post.Description;
			PostTitle = post.Title;
			PostImage = post.Images;
			RequestCount = post.RequestCount;
			CommentCount = post.CommentCount;
			Status = post.PostStatus;
			CategoryBackgroundColor = post.Category.BackgroundColor;
		}

		#endregion

		#region Methods
		//REVIEW : Consider to user MvxCommand instead of MvxAsyncCommand except in needed cases
		public IMvxAsyncCommand ShowMenuPopupCommand { get; set; }
	    public IMvxAsyncCommand ShowPostCommentCommand { get; set; }
		#endregion
	}
}
