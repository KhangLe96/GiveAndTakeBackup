using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiveAndTake.Core.Models;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class PopupLocationFilterViewModel : PopupViewModel 
	{
		public override string Title => "Lọc theo";
		protected override string SelectedItem { get; set; }
		protected override List<string> PopupItems { get; set; }

		public PopupLocationFilterViewModel(IDataModel dataModel) : base(dataModel)
		{
		}

		public override Task Initialize()
		{
			SelectedItem = DataModel.SelectedProvinceCity.ProvinceCityName;

			PopupItems = DataModel
				.ProvinceCities
				.Select(p => p.ProvinceCityName)
				.ToList();

			return base.Initialize();
		}

		protected override Task OnCloseCommand()
		{
			DataModel.SelectedProvinceCity = DataModel.ProvinceCities.First(city => city.ProvinceCityName == SelectedItem);
			return base.OnCloseCommand();
		}
	}
}