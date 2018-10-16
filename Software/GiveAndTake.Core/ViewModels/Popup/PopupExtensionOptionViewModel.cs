using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class PopupExtensionOptionViewModel : BaseViewModel<List<string>, string>
	{
		public IMvxAsyncCommand CloseCommand { get; set; }

		public List<PopupItemViewModel> PopupItemViewModels
		{
			get => _popupItemViewModels;
			set
			{
				_popupItemViewModels = value;
				RaisePropertyChanged(() => PopupItemViewModels);
			}
		}

		private List<PopupItemViewModel> _popupItemViewModels;

		public override void Prepare(List<string> popupItems)
		{
			PopupItemViewModels = InitPopupList(popupItems);
		}

		public override Task Initialize()
		{
			CloseCommand = new MvxAsyncCommand(() => NavigationService.Close(this, null));
			return base.Initialize();
		}

		private List<PopupItemViewModel> InitPopupList(List<string> popupItems)
		{
			var itemViewModels = new List<PopupItemViewModel>();

			foreach (var itemName in popupItems)
			{
				var itemViewModel = new PopupItemViewModel(itemName)
				{
					ItemSelected = item => NavigationService.Close(this, item.ItemName)
				};
				itemViewModels.Add(itemViewModel);
			}

			if (itemViewModels.Any())
			{
				itemViewModels.Last().IsSeparatorLineShown = false;
			}

			return itemViewModels;
		}
	}
}