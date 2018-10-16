using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class PopupListViewModel : BaseViewModel<PopupListParam, string>
	{
		protected readonly IDataModel DataModel;
		public string SubmitButtonTitle { get; } = AppConstants.Done;
		public IMvxAsyncCommand SubmitCommand { get; set; }
		public IMvxAsyncCommand CloseCommand { get; set; }
		public string Title { get; private set; }

		public List<PopupItemViewModel> PopupItemViewModels
		{
			get => _popupItemViewModels;
			set
			{
				_popupItemViewModels = value;
				RaisePropertyChanged(() => PopupItemViewModels);
			}
		}

		private PopupItemViewModel _selectedPopupItem;
		private List<PopupItemViewModel> _popupItemViewModels;

		public override void Prepare(PopupListParam popupListParam)
		{
			Title = popupListParam.Title;
			PopupItemViewModels = InitPopupList(popupListParam.Items, popupListParam.SelectedItem);
		}

		public override Task Initialize()
		{
			SubmitCommand = new MvxAsyncCommand(() => NavigationService.Close(this, _selectedPopupItem?.ItemName));
			CloseCommand = new MvxAsyncCommand(() => NavigationService.Close(this, null));
			return base.Initialize();
		}

		private List<PopupItemViewModel> InitPopupList(List<string> popupItems, string selectedItem)
		{
			var itemViewModels = new List<PopupItemViewModel>();

			foreach (var itemName in popupItems)
			{
				var itemViewModel = new PopupItemViewModel(itemName)
				{
					IsSelected = itemName == selectedItem,
					ItemSelected = OnItemSelected
				};


				if (itemName == selectedItem)
				{
					_selectedPopupItem = itemViewModel;
				}

				itemViewModels.Add(itemViewModel);
			}

			if (itemViewModels.Any())
			{
				itemViewModels.Last().IsSeparatorLineShown = false;
			}

			return itemViewModels;
		}

		private void OnItemSelected(PopupItemViewModel selectedPopupItemViewModel)
		{
			_selectedPopupItem.IsSelected = false;
			_selectedPopupItem = selectedPopupItemViewModel;
		}
	}
}