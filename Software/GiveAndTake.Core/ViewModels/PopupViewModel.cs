using System;
using System.Collections.Generic;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Binding.Extensions;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels
{
	public abstract class PopupViewModel : BaseViewModel
	{
		private readonly IDataModel _dataModel;
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

		protected PopupViewModel(IDataModel dataModel)
		{
			_dataModel = dataModel;
			//TODO : Get Categories to viewmodel
			ClosePopupCommand = new MvxAsyncCommand(() => NavigationService.Close(this, SelectedItem));
		}

		public override void Prepare(string parameter)
		{
			SelectedItem = parameter;
			ItemClickCommand = new MvxCommand<PopupItemViewModel>(OnItemClick);
			InitCategoriesList();
		}

		private void InitCategoriesList()
		{
			var categoryItemViewModels = new List<PopupItemViewModel>();

			foreach (var category in _dataModel.Categories)
			{
				var categoryItemViewModel = new PopupItemViewModel(category);
				categoryItemViewModel.ItemSelected += OnCategorySelected;
				categoryItemViewModels.Add(categoryItemViewModel);
			}

			PopupItemViewModels = categoryItemViewModels;
		}

		private void OnCategorySelected(object sender, EventArgs e)
		{
			var selectedCategory = sender as PopupItemViewModel;
			//TODO Update data model with selected category
		}

		protected void OnItemClick(PopupItemViewModel popupItemViewModel)
		{
			foreach (var viewModel in _popupItemViewModels)
			{
				viewModel.IsSelected = viewModel == popupItemViewModel;
			}
			SelectedItem = popupItemViewModel.ItemName;
		}

		private PopupItemViewModel GeneratePopupItemViewModel(string name) => new PopupItemViewModel(name)
		{
			IsLastViewInList = IsLast(name),
			IsSelected = GetItemSelectState(name)
		};

		private bool IsLast(string name) => _popupItems.GetPosition(name) + 1 == _popupItems.Count;

		private bool GetItemSelectState(string name) => name == SelectedItem;
	}
}