using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross;
using MvvmCross.Binding.Extensions;
using MvvmCross.Commands;

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
			_dataModel.SelectedSortFilter = _dataModel.SortFilters.First();
		}

		private void UpdatePostViewModels()
		{
			_dataModel.Posts = _managementService.GetPostList(GetFilterParams());
			PostViewModels = _dataModel.Posts.Select(post => new PostItemViewModel(post, IsLast(post))).ToList();
		}

		private void InitCommand()
		{
			// TODO: fix bug
			//ShowCategoriesCommand = new MvxAsyncCommand(ShowCategories);
			//ShowShortPostCommand = new MvxAsyncCommand(ShowShortFilterView);
			//ShowFilterCommand = new MvxAsyncCommand(ShowLocationFilterView);
			CreatePostCommand = new MvxAsyncCommand(() => NavigationService.Navigate<CreatePostViewModel>());
			SearchCommand = new MvxCommand(OnSearchSubmit);
		}

		private void OnSearchSubmit()
		{
			UpdatePostViewModels();
		}

		private async Task ShowCategories()
		{
			await NavigationService.Navigate<PopupCategoriesViewModel>();
			if (_dataModel.SelectedCategory != null)
			{
				UpdatePostViewModels();
			}
		}

		private async Task ShowShortFilterView()
		{
			await NavigationService.Navigate<PopupShortFilterViewModel>();
			if (_dataModel.SelectedSortFilter != null)
			{
				UpdatePostViewModels();
			}
		}

		private async Task ShowLocationFilterView()
		{
			await NavigationService.Navigate<PopupLocationFilterViewModel>();
			if (_dataModel.SelectedProvinceCity != null)
			{
				UpdatePostViewModels();
			}
		}

		private string GetFilterParams()
		{
			var parameters = $"order={_dataModel.SelectedSortFilter.FilterTag}";

			if (!string.IsNullOrEmpty(_currentQueryString))
			{
				parameters += $"&title={_currentQueryString}";
			}

			if (_dataModel.SelectedCategory != null)
			{
				parameters += $"&categoryId={_dataModel.SelectedCategory.Id}";
			}

			if (_dataModel.SelectedProvinceCity != null)
			{
				parameters += $"&provinceCityId={_dataModel.SelectedProvinceCity.Id}";
			}

			return parameters;
		}

		private bool IsLast(Post post) => _dataModel.Posts.GetPosition(post) + 1 == _dataModel.Posts.Count;
	}
}
