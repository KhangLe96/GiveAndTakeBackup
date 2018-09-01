using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiveAndTake.Core.Models;

namespace GiveAndTake.Core.ViewModels
{
	public class PopupShortFilterViewModel : PopupViewModel
	{
		public override string Title => "Xếp theo";
		protected override string SelectedItem { get; set; }
		protected override List<string> PopupItems { get; set; }

		public PopupShortFilterViewModel(IDataModel dataModel) : base(dataModel)
		{			
		}

		public override Task Initialize()
		{
			SelectedItem = DataModel.SelectedSortFilter?.FilterName ?? AppConstants.DefaultShortFilter;

			PopupItems = DataModel.SortFilters.Select(filter => filter.FilterName).ToList();

			return base.Initialize();
		}

		protected override Task OnCloseCommand()
		{
			DataModel.SelectedSortFilter = DataModel.SortFilters.First(filter => filter.FilterName == SelectedItem);
			return base.OnCloseCommand();
		}
	}
}