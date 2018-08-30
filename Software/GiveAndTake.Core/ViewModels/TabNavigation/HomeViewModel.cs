using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
		private List<PostItemViewModel> _postViewModels;
		private string currentCategory = AppConstants.DefaultCategory;
		private string currentShortFilter = AppConstants.DefaultShortFilter;
		private string currentLocationFilter = AppConstants.DefaultLocationFilter;


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

		public HomeViewModel(IDataModel dataModel)
		{
			_dataModel = dataModel;
			//TODO : Get category to data model

			if (_dataModel.Posts != null)
			{
				//Show Posts
			}
			else
			{
				//Call API to get Posts and display
			}
			UpdatePostViewModels();
			InitCommand();
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
		}

		private async Task ShowCategories()
		{
			//var category = await NavigationService.Navigate<PopupCategoriesViewModel, string, string>(currentCategory);
			//if (category != null)
			//{
			//	currentCategory = category;
			//	UpdatePostViewModels();
			//}

			var defaultCategory = _dataModel.Categories.First(cat => cat.Id == "1");

			await NavigationService.Navigate<PopupCategoriesViewModel, Category>(defaultCategory);
		}

		private async Task ShowShortFilterView()
		{
			var shortFilter = await NavigationService.Navigate<PopupShortFilterViewModel, string, string>(currentShortFilter);
			if (shortFilter != null)
			{
				currentShortFilter = shortFilter;
			}
		}

		private async Task ShowLocationFilterView()
		{
			var locationFilter = await NavigationService.Navigate<PopupLocationFilterViewModel, string, string>(currentLocationFilter);
			if (locationFilter != null)
			{
				currentLocationFilter = locationFilter;
			}
		}

		private string GetFilterParams()
		{
			var parameters = $"sort={GetCurrentSortFilterTag()}";

			if (currentCategory != AppConstants.DefaultCategory)
			{
				parameters += $"&categoryId={GetCurrentCategoryId()}";
			}

			if (currentLocationFilter != AppConstants.DefaultLocationFilter)
			{
				parameters += $"&provinceCityId={GetCurrentProvinceCityId()}";
			}

			return parameters;
		}

		private string GetCurrentSortFilterTag()
		{
			return "desc";
		}

		private string GetCurrentProvinceCityId() => _dataModel.ProvinceCities.FirstOrDefault(p => p.ProvinceCityName == currentLocationFilter)?.Id;

		private string GetCurrentCategoryId() => _dataModel.Categories.FirstOrDefault(c => c.CategoryName == currentCategory)?.Id;

		private bool IsLast(Post post) => _dataModel.Posts.GetPosition(post) + 1 == _dataModel.Posts.Count;
	}
}
