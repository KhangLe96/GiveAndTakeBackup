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
using MvvmCross;

namespace GiveAndTake.Core.ViewModels.TabNavigation
{
	public class HomeViewModel : BaseViewModel
	{
		#region Properties

		public string SearchResultTitle => AppConstants.SearchResultNullTitle;

		public IMvxCommand ShowLocationFiltersCommand =>
			_showLocationFiltersCommand ?? (_showLocationFiltersCommand = new MvxAsyncCommand(ShowLocationFiltersPopup));

		public IMvxCommand ShowSortFiltersCommand =>
			_showSortFiltersCommand ?? (_showSortFiltersCommand = new MvxAsyncCommand(ShowSortFiltersPopup));

		public IMvxCommand ShowCategoriesCommand =>
			_showCategoriesCommand ?? (_showCategoriesCommand = new MvxAsyncCommand(ShowCategoriesPopup));

		public IMvxCommand CreatePostCommand =>
			_createPostCommand ?? (_createPostCommand = new MvxAsyncCommand(ShowNewPostView));

		public IMvxCommand SearchCommand =>
			_searchCommand ?? (_searchCommand = new MvxAsyncCommand(OnSearching));
		public IMvxCommand CloseSearchBarCommand =>
			_searchCommand ?? (_searchCommand = new MvxAsyncCommand(InitDataModels));



		public IMvxCommand LoadMoreCommand =>
			_loadMoreCommand ?? (_loadMoreCommand = new MvxAsyncCommand(OnLoadMore));

		public IMvxCommand RefreshCommand =>
			_refreshCommand ?? (_refreshCommand = new MvxAsyncCommand(OnRefresh));


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
			set => SetProperty(ref _currentQueryString, value);		
		}

		public MvxObservableCollection<PostItemViewModel> PostItemViewModelCollection
		{
			get => _postItemViewModelCollection;
			set => SetProperty(ref _postItemViewModelCollection, value);
		}

		private bool _isSearchResultNull;
		private bool _isLocationFilterActivated;
		private bool _isSortFilterActivated;
		private bool _isCategoryFilterActivated;
		private bool _isRefresh;
		private string _currentQueryString;
		private readonly IDataModel _dataModel;
		private MvxObservableCollection<PostItemViewModel> _postItemViewModelCollection;
		private Category _selectedCategory;
		private ProvinceCity _selectedProvinceCity;
		private SortFilter _selectedSortFilter;
		private IMvxCommand _showLocationFiltersCommand;
		private IMvxCommand _showSortFiltersCommand;
		private IMvxCommand _showCategoriesCommand;
		private IMvxCommand _createPostCommand;
		private IMvxCommand _searchCommand;
		private IMvxCommand _loadMoreCommand;
		private IMvxCommand _refreshCommand;

		#endregion

		public HomeViewModel(IDataModel dataModel)
		{
			_dataModel = dataModel;

			//REVIEW : InitDataModels could be run earlier ? Can we run it in Setup ?
			Task.Run(async () => await InitDataModels());
		}

		private async Task InitDataModels() 
		{
			try
			{
				if (_dataModel.ApiPostsResponse == null)
				{
					await Mvx.Resolve<ILoadingOverlayService>().ShowOverlay(AppConstants.LoadingDataOverlayTitle);
					_dataModel.Categories = _dataModel.Categories ?? (await ManagementService.GetCategories()).Categories;
					_dataModel.ProvinceCities = _dataModel.ProvinceCities ?? (await ManagementService.GetProvinceCities()).ProvinceCities;
					_dataModel.SortFilters = _dataModel.SortFilters ?? ManagementService.GetShortFilters();
					_selectedCategory = _selectedCategory ?? _dataModel.Categories.First();
					_selectedProvinceCity = _selectedProvinceCity ?? _dataModel.ProvinceCities.First(p => p.ProvinceCityName == AppConstants.DefaultLocationFilter);
					_selectedSortFilter = _selectedSortFilter ?? _dataModel.SortFilters.First();				
					await UpdatePostViewModels();
					await Mvx.Resolve<ILoadingOverlayService>().CloseOverlay();
				}				
			}
			catch (AppException.ApiException)
			{
				var result = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.ErrorConnectionMessage);
				if (result == RequestStatus.Submitted)
				{
					await InitDataModels();
				}
			}			
		}

		private async Task UpdatePostViewModels()
		{
			try
			{
				_dataModel.ApiPostsResponse = await ManagementService.GetPostList(GetFilterParams());
				PostItemViewModelCollection = new MvxObservableCollection<PostItemViewModel>(_dataModel.ApiPostsResponse.Posts.Select(GeneratePostViewModels));
				if (PostItemViewModelCollection.Any())
				{
					PostItemViewModelCollection.Last().IsSeparatorLineShown = false;
				}
				IsSearchResultNull = PostItemViewModelCollection.Any();
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

		private async Task OnLoadMore() 
		{
			try
			{
				_dataModel.ApiPostsResponse = await ManagementService.GetPostList($"{GetFilterParams()}&page={_dataModel.ApiPostsResponse.Pagination.Page + 1}");
				if (_dataModel.ApiPostsResponse.Posts.Any())
				{
					PostItemViewModelCollection.Last().IsSeparatorLineShown = true;
					PostItemViewModelCollection.AddRange(_dataModel.ApiPostsResponse.Posts.Select(GeneratePostViewModels));
					PostItemViewModelCollection.Last().IsSeparatorLineShown = false;
				}
			
			}
			catch (AppException.ApiException)
			{
				var result = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.ErrorConnectionMessage);
				if (result == RequestStatus.Submitted)
				{
					await OnLoadMore();
				}
			}
		}

		private async Task OnRefresh()
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
		
		private async Task ShowCategoriesPopup() 
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

		private async Task ShowSortFiltersPopup() 
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

		private async Task ShowLocationFiltersPopup()
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

		private async Task ShowNewPostView() 
		{
			_dataModel.Categories.RemoveAt(0);

			var result = await NavigationService.Navigate<CreatePostViewModel, bool>();
			await Mvx.Resolve<ILoadingOverlayService>().ShowOverlay(AppConstants.LoadingDataOverlayTitle);
			await UpdateCategories();
			if (result)
			{				
				await UpdatePostViewModels();				
			}
			await Mvx.Resolve<ILoadingOverlayService>().CloseOverlay();
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
		private async Task OnSearching()
		{
			await Mvx.Resolve<ILoadingOverlayService>().ShowOverlay(AppConstants.LoadingDataOverlayTitle);
			await UpdatePostViewModels();
			await Mvx.Resolve<ILoadingOverlayService>().CloseOverlay();
		}

		public override void ViewDestroy(bool viewFinishing = true)
		{
			base.ViewDestroy(viewFinishing);
			_dataModel.ApiPostsResponse = null;
		}


		private string GetFilterParams()
		{
			var parameters = new List<string>();

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
