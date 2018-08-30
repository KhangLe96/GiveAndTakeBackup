using System.Collections.Generic;
using System.Linq;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using MvvmCross;
using MvvmCross.ViewModels;

namespace GiveAndTake.Core.ViewModels
{
	public class PopupCategoriesViewModel : PopupViewModel, IMvxViewModel<Category>
	{
		private readonly IManagementService _managementService = Mvx.Resolve<IManagementService>();

		public override string Title { get; set; } = "Phân loại";

		public PopupCategoriesViewModel(IDataModel dataModel) : base(dataModel)
		{

		}
		public void Prepare(Category category)
		{
		}

		protected override List<string> InitPopupItems()
		{
			DataModel.Categories = _managementService.GetCategories();
			return DataModel.Categories.Select(c => c.CategoryName).ToList();
		}
	}
}
