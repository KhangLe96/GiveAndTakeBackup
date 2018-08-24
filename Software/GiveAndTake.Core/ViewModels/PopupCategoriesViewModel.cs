using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Binding.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace GiveAndTake.Core.ViewModels
{
	public class PopupCategoriesViewModel : BaseViewModel
    {
	    private List<CategoryViewModel> _categoryViewModels;

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
			var categories = InitCategories();
			CategoryViewModels = categories.Select(c => new CategoryViewModel(c, categories.GetPosition(c) + 1 == categories.Count)).ToList();
		}

	    private static List<Category> InitCategories()
	    {
		    return new List<Category>
		    {
			    new Category {CategoryName = "Sách"},
			    new Category {CategoryName = "Quần áo"},
			    new Category {CategoryName = "Văn phòng phẩm"},
			    new Category {CategoryName = "Đồ dùng điện tử"},
			    new Category {CategoryName = "Tất cả"}
		    };
	    }
	}
}
