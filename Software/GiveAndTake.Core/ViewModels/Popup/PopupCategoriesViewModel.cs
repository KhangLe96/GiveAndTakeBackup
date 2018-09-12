using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiveAndTake.Core.Models;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class PopupCategoriesViewModel : PopupViewModel
	{
		public override string Title => "Phân loại";
		protected override string SelectedItem { get; set; }
		protected override List<string> PopupItems { get; set; }

		public PopupCategoriesViewModel(IDataModel dataModel) : base(dataModel)
		{
		}

		public override Task Initialize()
		{
			SelectedItem = DataModel.SelectedCategory.CategoryName;

			PopupItems = DataModel
				.Categories
				.Select(c => c.CategoryName)
				.ToList();

			return base.Initialize();
		}

		protected override Task OnCloseCommand()
		{
			DataModel.SelectedCategory = DataModel.Categories.First(c => c.CategoryName == SelectedItem);
			return base.OnCloseCommand();
		}
	}
}
