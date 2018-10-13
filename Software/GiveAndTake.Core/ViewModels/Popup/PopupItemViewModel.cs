using System;
using System.Windows.Input;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class PopupItemViewModel : BaseViewModel
	{
		public EventHandler ItemSelected { get; set; }
		private MvxCommand _clickCommand;
		public ICommand ClickCommand => _clickCommand;

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
			set
			{
				_isSeparatorLineShown = value;
				RaisePropertyChanged(() => IsSeparatorLineShown);
			}
		}

		private bool _isSelected;
		public bool IsSelected
		{
			get => _isSelected;
			set
			{
				_isSelected = value;
				RaisePropertyChanged(() => IsSelected);
			}
		}

		public PopupItemViewModel(string name)
		{
			ItemName = name;
			IsSelected = true;
			IsSeparatorLineShown = true;
			InitCommand();
		}

		private void InitCommand() => _clickCommand = new MvxCommand(OnClickedCommand);

		private void OnClickedCommand() => ItemSelected?.Invoke(this, null);
	}
}
