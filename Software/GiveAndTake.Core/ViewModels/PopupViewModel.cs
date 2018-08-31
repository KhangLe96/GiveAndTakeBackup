using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Binding.Extensions;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GiveAndTake.Core.Models;

namespace GiveAndTake.Core.ViewModels
{
	public abstract class PopupViewModel : BaseViewModel
	{
		protected readonly IDataModel DataModel;

		public IMvxAsyncCommand ClosePopupCommand { get; set; }
		public abstract string Title { get; }
		protected virtual string SelectedItem { get; set; }
		protected abstract List<string> PopupItems { get; }

		private List<PopupItemViewModel> _popupItemViewModels;
		public List<PopupItemViewModel> PopupItemViewModels
		{
			get => _popupItemViewModels;
			set
			{
				_popupItemViewModels = value;
				RaisePropertyChanged(() => PopupItemViewModels);
			}
		}

		protected PopupViewModel(IDataModel dataModel)
		{
			DataModel = dataModel;
			PopupItemViewModels = InitPopupList();
			ClosePopupCommand = new MvxAsyncCommand(OnCloseCommand);
		}

		protected virtual Task OnCloseCommand()
		{
			return NavigationService.Close(this);
		}

		private List<PopupItemViewModel> InitPopupList()
		{
			var itemViewModels = new List<PopupItemViewModel>();

			foreach (var itemName in PopupItems)
			{
				var itemViewModel = new PopupItemViewModel(itemName)
				{
					IsLastViewInList = IsLast(itemName),
					IsSelected = itemName == SelectedItem
				};
				itemViewModel.ItemSelected += OnItemSelected;
				itemViewModels.Add(itemViewModel);
			}

			return itemViewModels;
		}

		private bool IsLast(string name) => PopupItems.GetPosition(name) + 1 == PopupItems.Count;

		protected void OnItemSelected(object sender, EventArgs e)
		{
			var selectedItemViewModel = sender as PopupItemViewModel;
			foreach (var viewModel in PopupItemViewModels)
			{
				viewModel.IsSelected = viewModel == selectedItemViewModel;
			}
			SelectedItem = selectedItemViewModel?.ItemName;
		}
	}
}