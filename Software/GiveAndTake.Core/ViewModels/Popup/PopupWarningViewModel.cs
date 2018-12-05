﻿using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class PopupWarningViewModel : BaseViewModel<string, bool>
	{
		public IMvxAsyncCommand CloseCommand { get; set; }

		public string CloseButtonTitle { get; } = AppConstants.SubmitTitle;

		private string _message;
		public string Message
		{
			get => _message;
			set => SetProperty(ref _message, value);
		}
		
		public PopupWarningViewModel()
		{
			CloseCommand = new MvxAsyncCommand(() => NavigationService.Close(this,true));
		}

		public override void Prepare(string message)
		{
			_message = message;
		}
	}
}