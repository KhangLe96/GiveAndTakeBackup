using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross;
using MvvmCross.Binding.Extensions;
using MvvmCross.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross.ViewModels;

namespace GiveAndTake.Core.ViewModels.TabNavigation
{
	public class HomeViewModel : BaseViewModel
	{
		private readonly IDataModel _dataModel;
		private readonly IManagementService _managementService = Mvx.Resolve<IManagementService>();

		private string _currentQueryString;
		public string CurrentQueryString
		{
			get => _currentQueryString;
			set
			{
				_currentQueryString = value;
				RaisePropertyChanged(() => PostViewModels);
			}
		}

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
			_dataModel.ApiPostsResponse = _managementService.GetPostList(GetFilterParams());
			PostViewModels = new MvxObservableCollection<PostItemViewModel>(_dataModel.ApiPostsResponse.Posts.Select(post => new PostItemViewModel(post, IsLast(post))));
		}

		private void OnLoadMore()
		{
			_dataModel.ApiPostsResponse = _managementService.GetPostList($"{GetFilterParams()}&page={_dataModel.ApiPostsResponse.Pagination.Page + 1}");
			if (_dataModel.ApiPostsResponse.Posts.Any())
			{
				PostViewModels.AddRange(_dataModel.ApiPostsResponse.Posts.Select(post => new PostItemViewModel(post)));
			}
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
			if (result)
			{
				UpdatePostViewModels();
			}
		}

		private string GetFilterParams()
		{
			var parameters = new List<string>();

			if (!string.IsNullOrEmpty(_currentQueryString))
			{
				parameters.Add($"title={_currentQueryString}");
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
