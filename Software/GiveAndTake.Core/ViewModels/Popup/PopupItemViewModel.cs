using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;
using System;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class PopupItemViewModel : BaseViewModel
	{
		public Action<PopupItemViewModel> ItemSelected { get; set; }
		public IMvxCommand ClickCommand { get; set; }

		private string _itemName;
		public string ItemName
		{
			get => _itemName;
			set => SetProperty(ref _itemName, value);
		}

		
		private bool _isSeparatorLineShown;
		public bool IsSeparatorLineShown
		{
			get => _isSeparatorLineShown;
			set => SetProperty(ref _isSeparatorLineShown, value);
		}

		private bool _isSelected;
		public bool IsSelected
		{
			get => _isSelected;
			set => SetProperty(ref _isSelected, value);
		}

		public PopupItemViewModel(string name)
		{
			ItemName = name;
			IsSelected = true;
			IsSeparatorLineShown = true;
			InitCommand();
		}

		private void InitCommand() => ClickCommand = new MvxCommand(OnClickedCommand);

		private void OnClickedCommand()
		{
			IsSelected = true;
			ItemSelected?.Invoke(this);
		}
	}
}
