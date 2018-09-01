using GiveAndTake.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveAndTake.Core.ViewModels
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
			SelectedItem = DataModel.SelectedCategory?.CategoryName ?? AppConstants.DefaultItem;

			PopupItems = DataModel
				.Categories
				.Select(c => c.CategoryName)
				.Append(AppConstants.DefaultItem)
				.ToList();

			return base.Initialize();
		}

		protected override Task OnCloseCommand()
		{
			DataModel.SelectedCategory = DataModel.Categories.FirstOrDefault(c => c.CategoryName == SelectedItem);
			return base.OnCloseCommand();
		}
	}
}
