﻿using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class PopupWarningViewModel : BaseViewModel<string>
	{
		public IMvxAsyncCommand CloseCommand { get; set; }

		private string _message;
		public string Message
		{
			get => _message;
			set => SetProperty(ref _message, value);
		}

		public PopupWarningViewModel()
		{
			CloseCommand = new MvxAsyncCommand(() => NavigationService.Close(this));
		}

		public override void Prepare(string message)
		{
			_message = message;
		}
	}
}