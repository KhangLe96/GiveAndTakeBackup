using System;
using System.Collections.Generic;
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
					IsLastViewInList = IsLast(itemName),
					IsSelected = itemName == _selectedItem
				};
				itemViewModel.ItemSelected += OnItemSelected;
				itemViewModels.Add(itemViewModel);
			}

			return itemViewModels;
		}

		private bool IsLast(string name) => _popupItems.GetPosition(name) + 1 == _popupItems.Count;

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