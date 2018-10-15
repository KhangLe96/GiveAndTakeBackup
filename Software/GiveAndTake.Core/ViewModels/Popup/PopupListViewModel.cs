using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Binding.Extensions;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class PopupListViewModel : BaseViewModel<PopupListParam, string>
	{
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

		//Review ThanhVo Select item should be PopupItemViewModel because this is the view model of list of PopupItemViewModel
		private string _selectedItem;
		private List<string> _popupItems;
		private List<PopupItemViewModel> _popupItemViewModels;

		public override void Prepare(PopupListParam popupListParam)
		{
			Title = popupListParam.Title;
			_popupItems = popupListParam.Items;
			_selectedItem = popupListParam.SelectedItem;
		}

		public override Task Initialize()
		{
			PopupItemViewModels = InitPopupList();
			SubmitCommand = new MvxAsyncCommand(() => NavigationService.Close(this, _selectedItem));
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
					IsSelected = itemName == _selectedItem
				};

				//REview ThanhVo Should unregister event when destroy view model
				itemViewModel.ItemSelected += OnItemSelected;
				itemViewModels.Add(itemViewModel);
			}

			//Review ThanhVo itemViewModels.Last() can raise exception if it is empty list
			itemViewModels.Last().IsSeparatorLineShown = false;

			return itemViewModels;
		}

		private void OnItemSelected(object sender, EventArgs e)
		{
			var selectedItemViewModel = sender as PopupItemViewModel;
			foreach (var viewModel in PopupItemViewModels)
			{
				viewModel.IsSelected = viewModel == selectedItemViewModel;
			}
			_selectedItem = selectedItemViewModel?.ItemName;
		}
	}
}