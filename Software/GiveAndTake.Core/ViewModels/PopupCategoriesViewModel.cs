using GiveAndTake.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveAndTake.Core.ViewModels
{
	public class PopupCategoriesViewModel : PopupViewModel
	{
		public override string Title => "Phân loại";
		protected override string SelectedItem => DataModel.SelectedCategory?.CategoryName ?? AppConstants.DefaultItem;
		protected override List<string> PopupItems => DataModel
			.Categories
			.Select(c => c.CategoryName)
			.Append(AppConstants.DefaultItem)
			.ToList();

		public PopupCategoriesViewModel(IDataModel dataModel) : base(dataModel)
		{
		}

		protected override Task OnCloseCommand()
		{
			DataModel.SelectedCategory = DataModel.Categories.FirstOrDefault(c => c.CategoryName == SelectedItem);
			return base.OnCloseCommand();
		}
	}
}
