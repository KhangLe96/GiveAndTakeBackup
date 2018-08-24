using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;

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
			CategoryViewModels = categories.Select(c => new CategoryViewModel(c)).ToList();
		}

	    private static List<Category> InitCategories()
	    {
		    return new List<Category>
		    {
			    new Category {CategoryName = "Đà Nẵng"},
			    new Category {CategoryName = "TP Hồ Chí Minh"},
			    new Category {CategoryName = "Tất cả"}
		    };
	    }
	}
}
