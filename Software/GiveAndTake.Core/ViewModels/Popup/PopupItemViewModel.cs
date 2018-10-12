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

		//Review Thanh Vo This is should property of item, when you think about a object, you should think about its charactices
		//In this view, you has bring a list into each item. Instead of last item in view, you can define this item which show the bottom line or not
		private bool _isLastViewInList;
		public bool IsLastViewInList
		{
			get => _isLastViewInList;
			set
			{
				_isLastViewInList = value;
				RaisePropertyChanged(() => IsLastViewInList);
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
			InitCommand();
		}

		private void InitCommand() => _clickCommand = new MvxCommand(OnClickedCommand);

		private void OnClickedCommand() => ItemSelected?.Invoke(this, null);
	}
}
