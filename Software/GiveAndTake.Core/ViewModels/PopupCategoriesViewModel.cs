using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Binding.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Commands;
using System;

namespace GiveAndTake.Core.ViewModels
{
	public class PopupCategoriesViewModel : BaseViewModel
    {
	    public IMvxCommand<CategoryViewModel> ItemClickCommand { get; set; }
	    public IMvxAsyncCommand FindPostCommand { get; set; }
	    private List<CategoryViewModel> _categoryViewModels;
	    private readonly List<Category> _categories;

	    public List<CategoryViewModel> CategoryViewModels
	    {
		    get => _categoryViewModels;
		    set
		    {
			    _categoryViewModels = value;
			    RaisePropertyChanged(() => CategoryViewModels);
		    }
	    }

		public PopupCategoriesViewModel()
		{
			_categories = InitCategories();
			CategoryViewModels = _categories.Select(GenerateCategoryViewModel).ToList();
			ItemClickCommand = new MvxCommand<CategoryViewModel>(OnItemClick);
			FindPostCommand = new MvxAsyncCommand(() => NavigationService.Close(this));
		}

		private void OnItemClick(CategoryViewModel categoryViewModel)
		{
			foreach (var viewModel in _categoryViewModels)
			{
				viewModel.IsSelected = viewModel == categoryViewModel;
			}
		}

	    private CategoryViewModel GenerateCategoryViewModel(Category c) => new CategoryViewModel(c)
	    {
		    IsLastViewInList = IsLastCategory(c),
		    IsSelected = IsDefaultCategory(c)
	    };

	    private bool IsDefaultCategory(Category category) => category.CategoryName == AppConstants.DefaultCategory;

	    private bool IsLastCategory(Category c) => _categories.GetPosition(c) + 1 == _categories.Count;

	    private static List<Category> InitCategories() => new List<Category>
	    {
		    new Category {CategoryName = "Sách"},
		    new Category {CategoryName = "Quần áo"},
		    new Category {CategoryName = "Văn phòng phẩm"},
		    new Category {CategoryName = "Đồ dùng điện tử"},
		    new Category {CategoryName = "Tất cả"}
	    };
    }
}
