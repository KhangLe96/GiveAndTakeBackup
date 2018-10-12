using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Binding.Extensions;
using MvvmCross.Commands;
using System.Collections.Generic;
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

		private List<string> _popupItems;
		private List<PopupItemViewModel> _popupItemViewModels;

		public override void Prepare(List<string> popupItems)
		{
			_popupItems = popupItems;
		}

		public override Task Initialize()
		{
			PopupItemViewModels = InitPopupList();
			CloseCommand = new MvxAsyncCommand(() => NavigationService.Close(this, null));
			return base.Initialize();
		}

		private List<PopupItemViewModel> InitPopupList()
		{
			var itemViewModels = new List<PopupItemViewModel>();

			foreach (var itemName in _popupItems)
			{
				var itemViewModel = new PopupItemViewModel(itemName)
				{
					IsLastViewInList = IsLast(itemName)
				};
				itemViewModel.ItemSelected += (sender, args) => { NavigationService.Close(this, itemName); };
				itemViewModels.Add(itemViewModel);
			}

			return itemViewModels;
		}

		private bool IsLast(string name) => _popupItems.GetPosition(name) + 1 == _popupItems.Count;
	}
}