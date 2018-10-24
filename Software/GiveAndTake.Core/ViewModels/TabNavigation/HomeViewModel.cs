using System;
using GiveAndTake.Core.Exceptions;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiveAndTake.Core.Services;

namespace GiveAndTake.Core.ViewModels.TabNavigation
{
	public class HomeViewModel : BaseViewModel
	{
		#region Properties

		public IMvxCommand ShowFilterCommand { get; set; }

		public IMvxCommand ShowShortPostCommand { get; set; }

		public IMvxCommand ShowCategoriesCommand { get; set; }

		public IMvxCommand CreatePostCommand { get; set; }

		public IMvxCommand SearchCommand { get; private set; }

		public IMvxCommand LoadMoreCommand { get; private set; }

		public IMvxCommand RefreshCommand { get; private set; }

		public bool IsRefreshing
		{
			get => _isRefresh;
			set => SetProperty(ref _isRefresh, value);
		}

		public bool IsCategoryFilterActivated
		{
			get => _isCategoryFilterActivated;
			set => SetProperty(ref _isCategoryFilterActivated, value);
		}

		public bool IsSortFilterActivated
		{
			get => _isSortFilterActivated;
			set => SetProperty(ref _isSortFilterActivated, value);
		}

		public bool IsLocationFilterActivated
		{
			get => _isLocationFilterActivated;
			set => SetProperty(ref _isLocationFilterActivated, value);
		}

		public bool IsSearchResultNull
		{
			get => _isSearchResultNull;
			set => SetProperty(ref _isSearchResultNull, value);
		}

		public string CurrentQueryString
		{
			get => _currentQueryString;
			set
			{
				SetProperty(ref _currentQueryString, value);

				if (string.IsNullOrEmpty(value))
				{
					Task.Run(UpdatePostViewModels);
				}
			}
		}

		public MvxObservableCollection<PostItemViewModel> PostViewModels
		{
			get => _postViewModels;
			set => SetProperty(ref _postViewModels, value);
		}

		private bool _isSearchResultNull;
		private bool _isLocationFilterActivated;
		private bool _isSortFilterActivated;
		private bool _isCategoryFilterActivated;
		private bool _isRefresh;
		private string _currentQueryString;
		private readonly IDataModel _dataModel;
		private MvxObservableCollection<PostItemViewModel> _postViewModels;
		private Category _selectedCategory;
		private ProvinceCity _selectedProvinceCity;
		private SortFilter _selectedSortFilter;
		private ILoadingOverlayService _loadingOverlayService;
		#endregion


		public HomeViewModel(IDataModel dataModel, ILoadingOverlayService loadingOverlayService)
		{
			_dataModel = dataModel;
			_loadingOverlayService = loadingOverlayService;
			InitDataModels();
			InitCommand();
		}

		private async void InitDataModels()
		{
			try
			{
				//Review ThanhVo add await 
				_loadingOverlayService.ShowOverlay(AppConstants.LoadingDataOverlayTitle);
				_dataModel.Categories = _dataModel.Categories ?? (await ManagementService.GetCategories()).Categories;
				_dataModel.ProvinceCities = _dataModel.ProvinceCities ?? (await ManagementService.GetProvinceCities()).ProvinceCities;
				_dataModel.SortFilters = _dataModel.SortFilters ?? ManagementService.GetShortFilters();
				_selectedCategory = _selectedCategory ?? _dataModel.Categories.First();
				_selectedProvinceCity = _selectedProvinceCity ?? _dataModel.ProvinceCities.First(p => p.ProvinceCityName == AppConstants.DefaultLocationFilter);
				_selectedSortFilter = _selectedSortFilter ?? _dataModel.SortFilters.First();
				await UpdatePostViewModels();
			}
			catch (AppException.ApiException)
			{
				var result = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.ErrorConnectionMessage);
				if (result == RequestStatus.Submitted)
				{
					InitDataModels();
				}
			}

			await _loadingOverlayService.CloseOverlay();
		}

		private void InitCommand()
		{
			ShowCategoriesCommand = new MvxCommand(ShowCategoriesPopup);
			ShowShortPostCommand = new MvxCommand(ShowSortFiltersPopup);
			ShowFilterCommand = new MvxCommand(ShowLocationFiltersPopup);
			CreatePostCommand = new MvxCommand(ShowNewPostView);
			SearchCommand = new MvxCommand(() => Task.Run(UpdatePostViewModels));
			LoadMoreCommand = new MvxCommand(OnLoadMore);
			RefreshCommand = new MvxCommand(OnRefresh);
		}

		private async Task UpdatePostViewModels()
		{
			try
			{
				_dataModel.ApiPostsResponse = await ManagementService.GetPostList(GetFilterParams());
				PostViewModels = new MvxObservableCollection<PostItemViewModel>(_dataModel.ApiPostsResponse.Posts.Select(GeneratePostViewModels));
				if (PostViewModels.Any())
				{
					PostViewModels.Last().IsSeparatorLineShown = false;
				}
				IsSearchResultNull = PostViewModels.Any();
			}
			catch (AppException.ApiException)
			{
				var result = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.ErrorConnectionMessage);
				if (result == RequestStatus.Submitted)
				{
					await UpdatePostViewModels();
				}
			}
		}

		private async void OnLoadMore()
		{
			try
			{
				_dataModel.ApiPostsResponse = ManagementService.GetPostList($"{GetFilterParams()}&page={_dataModel.ApiPostsResponse.Pagination.Page + 1}").Result;
				if (_dataModel.ApiPostsResponse.Posts.Any())
				{
					PostViewModels.Last().IsSeparatorLineShown = true;
					PostViewModels.AddRange(_dataModel.ApiPostsResponse.Posts.Select(GeneratePostViewModels));
					PostViewModels.Last().IsSeparatorLineShown = false;
				}
			
			}
			catch (AggregateException ex) when (ex.InnerException is AppException.ApiException)
			{
				var result = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.ErrorConnectionMessage);
				if (result == RequestStatus.Submitted)
				{
					OnLoadMore();
				}
			}
		}

		private async void OnRefresh()
		{
			IsRefreshing = true;
			await UpdatePostViewModels();
			await UpdateCategories();
			IsRefreshing = false;
		}

		private PostItemViewModel GeneratePostViewModels(Post post)
		{
			post.IsMyPost = post.User.Id == _dataModel.LoginResponse.Profile.Id;
			return new PostItemViewModel(post);
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
			await UpdatePostViewModels();
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
			await UpdatePostViewModels();
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
			await UpdatePostViewModels();
		}

		private async void ShowNewPostView()
		{
			_dataModel.Categories.RemoveAt(0);

			var result = await NavigationService.Navigate<CreatePostViewModel, bool>();

			await UpdateCategories();
			if (result)
			{
				await UpdatePostViewModels();
			}

		}

		private async Task UpdateCategories()
		{
			try
			{
				_dataModel.Categories = (await ManagementService.GetCategories()).Categories;
			}
			catch (AppException.ApiException)
			{
				var popupResult = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.ErrorConnectionMessage);
				if (popupResult == RequestStatus.Submitted)
				{
					await UpdateCategories();
				}
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
