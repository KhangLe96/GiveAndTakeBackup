﻿using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;
using System.Threading.Tasks;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class PopupMessageViewModel : BaseViewModel<string, RequestStatus>
	{
		public IMvxAsyncCommand SubmitCommand { get; set; }
		public IMvxAsyncCommand CancelCommand { get; set; }

		public string SubmitButtonTitle { get; } = AppConstants.SubmitTitle;
		public string CancelButtonTitle { get; } = AppConstants.CancelTitle;

		private string _message;
		public string Message
		{
			get => _message;
			set => SetProperty(ref _message, value);
		}

		public PopupMessageViewModel()
		{
			SubmitCommand = new MvxAsyncCommand(OnSubmit);
			CancelCommand = new MvxAsyncCommand(OnCancel);
		}

		public Task OnSubmit() => NavigationService.Close(this, RequestStatus.Submitted);

		public Task OnCancel() => NavigationService.Close(this, RequestStatus.Cancelled);

		public override void Prepare(string message)
		{
			_message = message;
		}
	}
}