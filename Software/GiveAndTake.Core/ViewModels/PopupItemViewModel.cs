using GiveAndTake.Core.ViewModels.Base;

namespace GiveAndTake.Core.ViewModels
{
	public class PopupItemViewModel : BaseViewModel
	{
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
		}
	}
}
