using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiveAndTake.Core.Models;

namespace GiveAndTake.Core.ViewModels
{
	public class PopupShortFilterViewModel : PopupViewModel
	{
		public override string Title => "Xếp theo";
		protected override string SelectedItem => DataModel.SelectedSortFilter.FilterName ?? AppConstants.DefaultShortFilter;
		protected override List<string> PopupItems =>DataModel.SortFilters.Select(filter => filter.FilterName).ToList();

		public PopupShortFilterViewModel(IDataModel dataModel) : base(dataModel)
		{
		}

		protected override Task OnCloseCommand()
		{
			DataModel.SelectedSortFilter = DataModel.SortFilters.First(filter => filter.FilterName == SelectedItem);
			return base.OnCloseCommand();
		}
	}
}