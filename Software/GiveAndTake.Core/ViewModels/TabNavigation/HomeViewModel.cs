using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross;
using MvvmCross.Binding.Extensions;
using MvvmCross.Commands;
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

		private List<PostItemViewModel> _postViewModels;

		public List<PostItemViewModel> PostViewModels
		{
			get => _postViewModels;
			set
			{
				_postViewModels = value;
				RaisePropertyChanged(() => PostViewModels);
			}
		}

		public IMvxAsyncCommand ShowFilterCommand { get; set; }

		public IMvxAsyncCommand ShowShortPostCommand { get; set; }

		public IMvxAsyncCommand ShowCategoriesCommand { get; set; }

		public IMvxAsyncCommand CreatePostCommand { get; set; }

		public ICommand SearchCommand { get; private set; }

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

		private void UpdatePostViewModels()
		{
			_dataModel.Posts = _managementService.GetPostList(GetFilterParams());
			PostViewModels = _dataModel.Posts.Select(post => new PostItemViewModel(post, IsLast(post))).ToList();
		}

		private void InitCommand()
		{
			ShowCategoriesCommand = new MvxAsyncCommand(ShowCategories);
			ShowShortPostCommand = new MvxAsyncCommand(ShowShortFilterView);
			ShowFilterCommand = new MvxAsyncCommand(ShowLocationFilterView);
			CreatePostCommand = new MvxAsyncCommand(() => NavigationService.Navigate<CreatePostViewModel>());
			SearchCommand = new MvxCommand(OnSearchSubmit);
		}

		private void OnSearchSubmit()
		{
			UpdatePostViewModels();
		}

		private async Task ShowCategories()
		{
			var result = await NavigationService.Navigate<PopupCategoriesViewModel, bool>();
			if (result)
			{
				UpdatePostViewModels();
			}
		}

		private async Task ShowShortFilterView()
		{
			var result = await NavigationService.Navigate<PopupShortFilterViewModel, bool>();
			if (result)
			{
				UpdatePostViewModels();
			}
		}

		private async Task ShowLocationFilterView()
		{
			var result = await NavigationService.Navigate<PopupLocationFilterViewModel, bool>();
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

			if (_dataModel.SelectedCategory != null)
			{
				parameters.Add($"categoryId={_dataModel.SelectedCategory.Id}");
			}

			if (_dataModel.SelectedProvinceCity != null)
			{
				parameters.Add($"provinceCityId={_dataModel.SelectedProvinceCity.Id}");
			}

			return string.Join("&", parameters);
		}

		private bool IsLast(Post post) => _dataModel.Posts.GetPosition(post) + 1 == _dataModel.Posts.Count;
	}
}
