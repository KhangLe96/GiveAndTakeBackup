using System.Collections.Generic;
using System.Linq;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Binding.Extensions;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels
{
	public abstract class PopupViewModel : BaseViewModel<string, string>
	{
		public IMvxCommand<PopupItemViewModel> ItemClickCommand { get; set; }
		public IMvxAsyncCommand ClosePopupCommand { get; set; }
		protected string SelectedItem;
		private readonly List<string> _popupItems;

		public abstract string Title { get; set; }

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

		protected PopupViewModel()
		{
			_popupItems = InitPopupItems();
			ClosePopupCommand = new MvxAsyncCommand(() => NavigationService.Close(this, SelectedItem));
		}

		public override void Prepare(string parameter)
		{
			SelectedItem = parameter;
			PopupItemViewModels = _popupItems.Select(GeneratePopupItemViewModel).ToList();
			ItemClickCommand = new MvxCommand<PopupItemViewModel>(OnItemClick);
		}

		protected void OnItemClick(PopupItemViewModel popupItemViewModel)
		{
			foreach (var viewModel in _popupItemViewModels)
			{
				viewModel.IsSelected = viewModel == popupItemViewModel;
			}
			SelectedItem = popupItemViewModel.ItemName;
		}

		protected abstract List<string> InitPopupItems();

		private PopupItemViewModel GeneratePopupItemViewModel(string name) => new PopupItemViewModel(name)
		{
			IsLastViewInList = IsLast(name),
			IsSelected = GetItemSelectState(name)
		};

		private bool IsLast(string name) => _popupItems.GetPosition(name) + 1 == _popupItems.Count;

		private bool GetItemSelectState(string name) => name == SelectedItem;
	}
}