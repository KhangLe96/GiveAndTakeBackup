using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GiveAndTake.Core.ViewModels.Popup;

namespace GiveAndTake.Core.ViewModels.TabNavigation
{
	public class HomeViewModel : BaseViewModel
	{
		#region Properties

		private readonly IDataModel _dataModel;
		private readonly IManagementService _managementService = Mvx.Resolve<IManagementService>();
		private MvxObservableCollection<PostItemViewModel> _postViewModels;
		private Category _selectedCategory;
		private ProvinceCity _selectedProvinceCity;
		private SortFilter _selectedSortFilter;

		public MvxObservableCollection<PostItemViewModel> PostViewModels
		{
			get => _postViewModels;
			set => SetProperty(ref _postViewModels, value);
		}

		public IMvxCommand ShowFilterCommand { get; set; }

		public IMvxCommand ShowShortPostCommand { get; set; }

		public IMvxCommand ShowCategoriesCommand { get; set; }

		public IMvxCommand CreatePostCommand { get; set; }

		public IMvxCommand SearchCommand { get; private set; }

		public IMvxCommand LoadMoreCommand { get; private set; }

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

		private bool _isSearchResultNull;
		public bool IsSearchResultNull
		{
			get => _isSearchResultNull;
			set => SetProperty(ref _isSearchResultNull, value);
		}

		private string _currentQueryString;
		public string CurrentQueryString
		{
			get => _currentQueryString;
			set
			{
				SetProperty(ref _currentQueryString, value);

				if (string.IsNullOrEmpty(value))
				{
					UpdatePostViewModels();
				}
			}
		}

		private MvxCommand _refreshCommand;
		
		public ICommand RefreshCommand => _refreshCommand = _refreshCommand ?? new MvxCommand(OnRefresh);

		#endregion

		public HomeViewModel(IDataModel dataModel)
		{
			_dataModel = dataModel;
			InitDataModels();
			UpdatePostViewModels();
			InitCommand();
		}

		private void InitDataModels()
		{
			_dataModel.Categories = _dataModel.Categories ?? ManagementService.GetCategories();
			_dataModel.ProvinceCities = _dataModel.ProvinceCities ?? ManagementService.GetProvinceCities();
			_dataModel.SortFilters = _dataModel.SortFilters ?? ManagementService.GetShortFilters();
			_selectedCategory = _selectedCategory ?? _dataModel.Categories.First();
			_selectedProvinceCity = _selectedProvinceCity ?? _dataModel.ProvinceCities.First(p => p.ProvinceCityName == AppConstants.DefaultLocationFilter);
			_selectedSortFilter = _selectedSortFilter ?? _dataModel.SortFilters.First();
		}

		private void InitCommand()
		{
			ShowCategoriesCommand = new MvxCommand(ShowCategoriesPopup);
			ShowShortPostCommand = new MvxCommand(ShowSortFiltersPopup);
			ShowFilterCommand = new MvxCommand(ShowLocationFiltersPopup);
			CreatePostCommand = new MvxCommand(ShowNewPostView);
			SearchCommand = new MvxCommand(UpdatePostViewModels);
			LoadMoreCommand = new MvxCommand(OnLoadMore);
		}

		private void UpdatePostViewModels()
		{
            _dataModel.ApiPostsResponse = _managementService.GetPostList(GetFilterParams());
			PostViewModels = new MvxObservableCollection<PostItemViewModel>(_dataModel.ApiPostsResponse.Posts.Select(GeneratePostViewModels));
			if (PostViewModels.Any())
			{
				PostViewModels.Last().IsSeparatorLineShown = false;
			}
			IsSearchResultNull = PostViewModels.Any();
		}

		private void OnLoadMore()
		{
			_dataModel.ApiPostsResponse = _managementService.GetPostList($"{GetFilterParams()}&page={_dataModel.ApiPostsResponse.Pagination.Page + 1}");
			if (_dataModel.ApiPostsResponse.Posts.Any())
			{
				PostViewModels.Last().IsSeparatorLineShown = true;
				PostViewModels.AddRange(_dataModel.ApiPostsResponse.Posts.Select(GeneratePostViewModels));
				PostViewModels.Last().IsSeparatorLineShown = false;
			}
		}

		private PostItemViewModel GeneratePostViewModels(Post post)
		{
			post.IsMyPost = post.User.Id == _dataModel.LoginResponse.Profile.Id;
			return new PostItemViewModel(post);
		}

		private async void OnRefresh()
		{
			IsRefreshing = true;
			UpdatePostViewModels();
			await Task.Delay(1000);
			IsRefreshing = false;
		}

		private async void ShowCategoriesPopup()
		{
			var result = await NavigationService.Navigate<PopupListViewModel, PopupListParam, string>(new PopupListParam
			{
				Title = AppConstants.PopupCategoriesTitle,
				Items = _dataModel.Categories.Select(c => c.CategoryName).ToList(),
				SelectedItem = _selectedCategory.CategoryName
			});

			if (string.IsNullOrEmpty(result)) return;

			_selectedCategory = _dataModel.Categories.First(c => c.CategoryName == result);
			IsCategoryFilterActivated = _selectedCategory != _dataModel.Categories.First();
			UpdatePostViewModels();
		}

		private async void ShowSortFiltersPopup()
		{
			var result = await NavigationService.Navigate<PopupListViewModel, PopupListParam, string>(new PopupListParam
			{
				Title = AppConstants.PopupSortFiltersTitle,
				Items = _dataModel.SortFilters.Select(s => s.FilterName).ToList(),
				SelectedItem = _selectedSortFilter.FilterName
			});

			if (string.IsNullOrEmpty(result)) return;

			_selectedSortFilter = _dataModel.SortFilters.First(s => s.FilterName == result);
			IsSortFilterActivated = _selectedSortFilter.FilterTag != _dataModel.SortFilters.First().FilterTag;
			UpdatePostViewModels();
		}

		private async void ShowLocationFiltersPopup()
		{
			var result = await NavigationService.Navigate<PopupListViewModel, PopupListParam, string>(new PopupListParam
			{
				Title = AppConstants.PopupLocationFiltersTitle,
				Items = _dataModel.ProvinceCities.Select(c => c.ProvinceCityName).ToList(),
				SelectedItem = _selectedProvinceCity.ProvinceCityName
			});

			if (string.IsNullOrEmpty(result)) return;

			_selectedProvinceCity = _dataModel.ProvinceCities.First(c => c.ProvinceCityName == result);
			IsLocationFilterActivated = _selectedProvinceCity.ProvinceCityName != AppConstants.DefaultLocationFilter;
			UpdatePostViewModels();
		}

		private async void ShowNewPostView()
		{
			_dataModel.Categories.RemoveAt(0);

			var result = await NavigationService.Navigate<CreatePostViewModel, bool>();

			_dataModel.Categories = ManagementService.GetCategories();

			if (result)
			{
				UpdatePostViewModels();
			}
		}

		private string GetFilterParams()
		{
			var parameters = new List<string>{"limit=20"};

			if (!string.IsNullOrEmpty(CurrentQueryString))
			{
				parameters.Add($"keyword={CurrentQueryString}");
			}

			if (_selectedSortFilter != null)
			{
				parameters.Add($"order={_selectedSortFilter.FilterTag}");
			}

			if (_selectedCategory?.Id != _dataModel.Categories.First().Id)
			{
				parameters.Add($"categoryId={_selectedCategory?.Id}");
			}

			if (_selectedProvinceCity != null)
			{
				parameters.Add($"provinceCityId={_selectedProvinceCity.Id}");
			}

			return string.Join("&", parameters);
		}
	}
}
