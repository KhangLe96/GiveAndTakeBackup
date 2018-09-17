using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross;
using MvvmCross.Binding.Extensions;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GiveAndTake.Core.ViewModels.TabNavigation
{
	public class HomeViewModel : BaseViewModel
	{
		private readonly IDataModel _dataModel;
		private readonly IManagementService _managementService = Mvx.Resolve<IManagementService>();

		public string CurrentQueryString { get; set; }

		private MvxObservableCollection<PostItemViewModel> _postViewModels;

		public MvxObservableCollection<PostItemViewModel> PostViewModels
		{
			get => _postViewModels;
			set => SetProperty(ref _postViewModels, value);
		}

		public IMvxAsyncCommand ShowFilterCommand { get; set; }

		public IMvxAsyncCommand ShowShortPostCommand { get; set; }

		public IMvxAsyncCommand ShowCategoriesCommand { get; set; }

		public IMvxAsyncCommand CreatePostCommand { get; set; }

		public ICommand SearchCommand { get; private set; }

		public ICommand LoadMoreCommand { get; private set; }

		private bool _isRefresh;

		public bool IsRefreshing
		{
			get => _isRefresh;
			set => SetProperty(ref _isRefresh, value);
		}

		private bool _isCategoryFilterActivated;
		public bool IsCategoryFilterActivated
		{
			get => _isCategoryFilterActivated;
			set => SetProperty(ref _isCategoryFilterActivated, value);
		}

		private bool _isSortFilterActivated;
		public bool IsSortFilterActivated
		{
			get => _isSortFilterActivated;
			set => SetProperty(ref _isSortFilterActivated, value);
		}

		private bool _isLocationFilterActivated;
		public bool IsLocationFilterActivated
		{
			get => _isLocationFilterActivated;
			set => SetProperty(ref _isLocationFilterActivated, value);
		}

		private MvxCommand _refreshCommand;
		public ICommand RefreshCommand => _refreshCommand = _refreshCommand ?? new MvxCommand(OnRefresh);


		public HomeViewModel(IDataModel dataModel)
		{
			_dataModel = dataModel;
			InitDataModels();
			UpdatePostViewModels();
			InitCommand();
		}

		private void InitDataModels()
		{
			_dataModel.Categories = ManagementService.GetCategories();
			_dataModel.ProvinceCities = ManagementService.GetProvinceCities();
			_dataModel.SortFilters = ManagementService.GetShortFilters();
			_dataModel.SelectedCategory = _dataModel.Categories.First(c => c.CategoryName == AppConstants.DefaultItem);
			_dataModel.SelectedProvinceCity = _dataModel.ProvinceCities.First(p => p.ProvinceCityName == AppConstants.DefaultLocationFilter);
			_dataModel.SelectedSortFilter = _dataModel.SortFilters.First(filter => filter.FilterName == AppConstants.DefaultShortFilter);
		}

		private void InitCommand()
		{
			ShowCategoriesCommand = new MvxAsyncCommand(ShowViewResult<PopupCategoriesViewModel>);
			ShowShortPostCommand = new MvxAsyncCommand(ShowViewResult<PopupShortFilterViewModel>);
			ShowFilterCommand = new MvxAsyncCommand(ShowViewResult<PopupLocationFilterViewModel>);
			CreatePostCommand = new MvxAsyncCommand(ShowViewResult<CreatePostViewModel>);
			SearchCommand = new MvxCommand(UpdatePostViewModels);
			LoadMoreCommand = new MvxCommand(OnLoadMore);
		}

		private void UpdatePostViewModels()
		{
		    var  a = _managementService.GetPostList(GetFilterParams());
            _dataModel.ApiPostsResponse = _managementService.GetPostList(GetFilterParams());
			PostViewModels = new MvxObservableCollection<PostItemViewModel>(_dataModel.ApiPostsResponse.Posts.Select(post => new PostItemViewModel(post, IsLast(post))));
		}

		private void OnLoadMore()
		{
			_dataModel.ApiPostsResponse = _managementService.GetPostList($"{GetFilterParams()}&page={_dataModel.ApiPostsResponse.Pagination.Page + 1}");
			if (_dataModel.ApiPostsResponse.Posts.Any())
			{
				PostViewModels.Last().IsLastViewInList = false;
				PostViewModels.AddRange(_dataModel.ApiPostsResponse.Posts.Select(GeneratePostViewModels));
			}
		}

		private PostItemViewModel GeneratePostViewModels(Post post)
		{
			post.IsMyPost = post.User.Id == _dataModel.LoginResponse.Profile.Id;
			post.IsLast = IsLast(post);
			return new PostItemViewModel(post);
		}

		private async void OnRefresh()
		{
			IsRefreshing = true;
			UpdatePostViewModels();
			await Task.Delay(1000);
			IsRefreshing = false;
		}

		private async Task ShowViewResult<T>() where T : BaseViewModelResult<bool>
		{
			var result = await NavigationService.Navigate<T, bool>();
			if (!result) return;
			UpdatePostViewModels();
			IsCategoryFilterActivated = _dataModel.SelectedCategory.CategoryName != AppConstants.DefaultItem;
			IsLocationFilterActivated =  _dataModel.SelectedProvinceCity.ProvinceCityName != AppConstants.DefaultLocationFilter;
			IsSortFilterActivated = _dataModel.SelectedSortFilter.FilterName != AppConstants.DefaultShortFilter;
		}

		private string GetFilterParams()
		{
			var parameters = new List<string>();

			if (!string.IsNullOrEmpty(CurrentQueryString))
			{
				parameters.Add($"title={CurrentQueryString}");
			}

			if (_dataModel.SelectedSortFilter != null)
			{
				parameters.Add($"order={_dataModel.SelectedSortFilter.FilterTag}");
			}

			if (_dataModel.SelectedCategory?.CategoryName != AppConstants.DefaultItem )
			{
				parameters.Add($"categoryId={_dataModel.SelectedCategory?.Id}");
			}

			if (_dataModel.SelectedProvinceCity != null)
			{
				parameters.Add($"provinceCityId={_dataModel.SelectedProvinceCity.Id}");
			}

			return string.Join("&", parameters);
		}

		private bool IsLast(Post post) => _dataModel.ApiPostsResponse.Posts.GetPosition(post) + 1 == _dataModel.ApiPostsResponse.Posts.Count;
	}
}
