using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
					UpdatePostViewModels();
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

		#endregion

		public HomeViewModel(IDataModel dataModel)
		{
			_dataModel = dataModel;
			InitDataModels();
			InitCommand();
		}

		private async void InitDataModels()
		{
			try
			{
				_dataModel.Categories = _dataModel.Categories ?? ManagementService.GetCategories();
				_dataModel.ProvinceCities = _dataModel.ProvinceCities ?? ManagementService.GetProvinceCities();
				_dataModel.SortFilters = _dataModel.SortFilters ?? ManagementService.GetShortFilters();
				_dataModel.SelectedCategory = _dataModel.SelectedCategory ?? _dataModel.Categories.First();
				_dataModel.SelectedProvinceCity = _dataModel.SelectedProvinceCity ?? _dataModel.ProvinceCities.First(p => p.ProvinceCityName == AppConstants.DefaultLocationFilter);
				_dataModel.SelectedSortFilter = _dataModel.SelectedSortFilter ?? _dataModel.SortFilters.First();
				UpdatePostViewModels();
			}
			catch (Exception)
			{
				var result = await NavigationService.Navigate<PopupMessageViewModel, string, bool>(AppConstants.ErrorConnectionMessage);
				if (result)
				{
					InitDataModels();
				}
			}
		}

		private void InitCommand()
		{
			ShowCategoriesCommand = new MvxCommand(ShowViewResult<PopupCategoriesViewModel>);
			ShowShortPostCommand = new MvxCommand(ShowViewResult<PopupShortFilterViewModel>);
			ShowFilterCommand = new MvxCommand(ShowViewResult<PopupLocationFilterViewModel>);
			CreatePostCommand = new MvxCommand(ShowNewPostView);
			SearchCommand = new MvxCommand(UpdatePostViewModels);
			LoadMoreCommand = new MvxCommand(OnLoadMore);
			RefreshCommand = new MvxCommand(OnRefresh);
		}

		private async void UpdatePostViewModels()
		{
			try
			{
				var t = DateTimeOffset.Now;

				_dataModel.ApiPostsResponse = ManagementService.GetPostList(GetFilterParams());
				PostViewModels = new MvxObservableCollection<PostItemViewModel>(_dataModel.ApiPostsResponse.Posts.Select(GeneratePostViewModels));
				if (PostViewModels.Any())
				{
					PostViewModels.Last().IsLastViewInList = true;
				}
				IsSearchResultNull = PostViewModels.Any();

				Console.WriteLine($"UpdatePostViewModels lasted {(DateTimeOffset.Now - t).Milliseconds}");
			}
			catch (Exception )
			{
				var result = await NavigationService.Navigate<PopupMessageViewModel, string, bool>(AppConstants.ErrorConnectionMessage);
				if (result)
				{
					UpdatePostViewModels();
				}
			}
		}

		private async void OnLoadMore()
		{
			try
			{
				_dataModel.ApiPostsResponse = ManagementService.GetPostList($"{GetFilterParams()}&page={_dataModel.ApiPostsResponse.Pagination.Page + 1}");
				if (_dataModel.ApiPostsResponse.Posts.Any())
				{
					PostViewModels.Last().IsLastViewInList = false;
					PostViewModels.AddRange(_dataModel.ApiPostsResponse.Posts.Select(GeneratePostViewModels));
					PostViewModels.Last().IsLastViewInList = true;
				}
			}
			catch (Exception)
			{
				var result = await NavigationService.Navigate<PopupMessageViewModel, string, bool>(AppConstants.ErrorConnectionMessage);
				if (result)
				{
					OnLoadMore();
				}
			}
		}

		private void OnRefresh()
		{
			IsRefreshing = true;
			Task.Run(() => UpdatePostViewModels())
				.ContinueWith(task => Task.Delay(1000))
				.ContinueWith(task => { IsRefreshing = false; });
		}

		private PostItemViewModel GeneratePostViewModels(Post post)
		{
			post.IsMyPost = post.User.Id == _dataModel.LoginResponse.Profile.Id;
			return new PostItemViewModel(post);
		}

		private async void ShowViewResult<T>() where T : BaseViewModelResult<bool>
		{
			var result = await NavigationService.Navigate<T, bool>();
			if (!result) return;
			UpdatePostViewModels();
			IsCategoryFilterActivated = _dataModel.SelectedCategory != _dataModel.Categories.First();
			IsLocationFilterActivated =  _dataModel.SelectedProvinceCity.ProvinceCityName != AppConstants.DefaultLocationFilter;
			IsSortFilterActivated = _dataModel.SelectedSortFilter.FilterTag != _dataModel.SortFilters.First().FilterTag;
		}

		private async void ShowNewPostView()
		{
			var categoryFilter = _dataModel.SelectedCategory;
			var locationFilter = _dataModel.SelectedProvinceCity;
			_dataModel.Categories.RemoveAt(0);

			var result = await NavigationService.Navigate<CreatePostViewModel, bool>();

			await Task.Run(() =>
			{
				_dataModel.Categories = ManagementService.GetCategories();
				_dataModel.SelectedCategory = categoryFilter;
				_dataModel.SelectedProvinceCity = locationFilter;
				if (result)
				{
					UpdatePostViewModels();
				}
			});
		}

		private string GetFilterParams()
		{
			var parameters = new List<string>{"limit=20"};

			if (!string.IsNullOrEmpty(CurrentQueryString))
			{
				parameters.Add($"keyword={CurrentQueryString}");
			}

			if (_dataModel.SelectedSortFilter != null)
			{
				parameters.Add($"order={_dataModel.SelectedSortFilter.FilterTag}");
			}

			if (_dataModel.SelectedCategory?.Id != _dataModel.Categories.First().Id)
			{
				parameters.Add($"categoryId={_dataModel.SelectedCategory?.Id}");
			}

			if (_dataModel.SelectedProvinceCity != null)
			{
				parameters.Add($"provinceCityId={_dataModel.SelectedProvinceCity.Id}");
			}

			return string.Join("&", parameters);
		}
	}
}
