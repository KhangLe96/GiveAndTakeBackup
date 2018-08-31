using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiveAndTake.Core.Models;

namespace GiveAndTake.Core.ViewModels
{
	public class PopupLocationFilterViewModel : PopupViewModel 
	{
		public override string Title => "Lọc theo";
		protected override string SelectedItem => DataModel.SelectedProvinceCity.ProvinceCityName ?? AppConstants.DefaultItem;
		protected override List<string> PopupItems => DataModel
			.ProvinceCities
			.Select(p => p.ProvinceCityName)
			.Append(AppConstants.DefaultItem)
			.ToList();

		public PopupLocationFilterViewModel(IDataModel dataModel) : base(dataModel)
		{
		}

		protected override Task OnCloseCommand()
		{
			DataModel.SelectedProvinceCity = DataModel.ProvinceCities.FirstOrDefault(city => city.ProvinceCityName == SelectedItem);
			return base.OnCloseCommand();
		}
	}
}