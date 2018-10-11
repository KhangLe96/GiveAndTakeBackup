using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Binding.Extensions;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels.Popup
{
	//Revew ThanhVo The popup list item just show the list of item (string)
	//What do you expect from popup list, just get selected item, right?
	//So the return value is selected item (string)
	//Where do you handle base on selected item => where you call the popup list item, right?
	//How you init data fro PopupItemViewModels? => you can pass it from Parameter of ViewModel,
	//but MVVMCross does not allow you pass a list, you can convert to json as string => Write json helper to serialize and deserialize object
	public abstract class PopupListViewModel : BaseViewModelResult<bool>
	{
		protected readonly IDataModel DataModel;

		public IMvxAsyncCommand SubmitCommand { get; set; }
		public IMvxAsyncCommand CloseCommand { get; set; }
		public abstract string Title { get; }
		protected abstract string SelectedItem { get; set; }
		protected abstract List<string> PopupItems { get; set; }

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

		protected PopupListViewModel(IDataModel dataModel)
		{
			DataModel = dataModel;
		}

		public override Task Initialize()
		{
			PopupItemViewModels = InitPopupList();
			SubmitCommand = new MvxAsyncCommand(OnCloseCommand);
			CloseCommand = new MvxAsyncCommand(() => NavigationService.Close(this, false));
			return base.Initialize();
		}

		//Review ThanhVo Name of method does not match with command
		protected virtual Task OnCloseCommand()
		{
			return NavigationService.Close(this, true);
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