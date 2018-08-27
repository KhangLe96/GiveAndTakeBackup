using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;

namespace GiveAndTake.Core.ViewModels
{
	public class CategoryViewModel : BaseViewModel
    {
		public Category Category { get; set; }
		private string _id;

	    public string Id
	    {
		    get => _id;
		    set => SetProperty(ref _id, value);
	    }

	    private string _categoryName;

	    public string CategoryName
	    {
		    get => _categoryName;
		    set => SetProperty(ref _categoryName, value);
		}

	    private bool _isLastViewInList;

	    public bool IsLastViewInList
	    {
		    get => _isLastViewInList;
			set
			{
				_isLastViewInList = value;
				RaisePropertyChanged(() => IsLastViewInList);
			}
		}

	    private bool _isSelected;

	    public bool IsSelected
	    {
		    get => _isSelected;
		    set
		    {
			    _isSelected = value; 
			    RaisePropertyChanged(() => IsSelected);
		    }
	    }

	    public CategoryViewModel(Category category)
	    {
		    Category = category;
			Id = category.Id;
			CategoryName = category.CategoryName;
		}
	}
}
