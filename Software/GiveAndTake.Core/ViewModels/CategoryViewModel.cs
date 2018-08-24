using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;

namespace GiveAndTake.Core.ViewModels
{
	public class CategoryViewModel : BaseViewModel
    {
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
		    set => SetProperty(ref _isLastViewInList, value);
		}

	    public CategoryViewModel(Category category, bool isLast = false)
		{
			Id = category.Id;
			CategoryName = category.CategoryName;
			IsLastViewInList = isLast;
		}
	}
}
