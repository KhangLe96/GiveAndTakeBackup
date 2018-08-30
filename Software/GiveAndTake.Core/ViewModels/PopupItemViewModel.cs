using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;
using System;
using System.Windows.Input;
using GiveAndTake.Core.Models;

namespace GiveAndTake.Core.ViewModels
{
	public class PopupItemViewModel : BaseViewModel
	{
		public EventHandler ItemSelected { get; set; }
		private MvxCommand _clickCommand;

		public ICommand ClickCommand => _clickCommand;

		private string _itemId;

		public string ItemId
		{
			get => _itemId;
			set => SetProperty(ref _itemId, value);
		}

		private string _itemName;

		public string ItemName
		{
			get => _itemName;
			set => SetProperty(ref _itemName, value);
		}

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

			InitCommand();
		}

		public PopupItemViewModel(Category category)
		{
			ItemName = category.CategoryName;

			InitCommand();
		}

		private void InitCommand()
		{
			_clickCommand = new MvxCommand(OnClickedCommand);
		}

		private void OnClickedCommand()
		{
			ItemSelected?.Invoke(this, null);
		}
	}
}
