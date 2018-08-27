using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Binding.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Commands;
using System;
using MvvmCross.ViewModels;

namespace GiveAndTake.Core.ViewModels
{
	public class PopupCategoriesViewModel : BaseViewModel<Category, Category>
	{
	    public IMvxCommand<CategoryViewModel> ItemClickCommand { get; set; }
	    public IMvxAsyncCommand FindPostCommand { get; set; }
	    private List<CategoryViewModel> _categoryViewModels;
	    private readonly List<Category> _categories;
	    private Category selectedCategory;

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
		}

	    public override void Prepare(Category category)
	    {
		    selectedCategory = category ?? _categories.FirstOrDefault(c => c.CategoryName == AppConstants.DefaultCategory);
		    CategoryViewModels = _categories.Select(GenerateCategoryViewModel).ToList();
		    ItemClickCommand = new MvxCommand<CategoryViewModel>(OnItemClick);
		    FindPostCommand = new MvxAsyncCommand(() => NavigationService.Close(this, selectedCategory));
		}

		private void OnItemClick(CategoryViewModel categoryViewModel)
		{
			foreach (var viewModel in _categoryViewModels)
			{
				viewModel.IsSelected = viewModel == categoryViewModel;
			}

			selectedCategory = categoryViewModel.Category;
		}

	    private CategoryViewModel GenerateCategoryViewModel(Category c) => new CategoryViewModel(c)
	    {
		    IsLastViewInList = IsLastCategory(c),
		    IsSelected = GetCategorySelectState(c)
	    };

	    private bool GetCategorySelectState(Category category) => category.CategoryName == selectedCategory.CategoryName;

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
