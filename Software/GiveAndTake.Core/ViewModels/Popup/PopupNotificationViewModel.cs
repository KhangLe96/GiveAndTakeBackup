using System;
using System.Collections.Generic;
using System.Text;
using GiveAndTake.Core.ViewModels.Base;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class PopupNotificationViewModel : BaseViewModel<string>
	{
		private string _message;
		public string Message
		{
			get => _message;
			set => SetProperty(ref _message, value);
		}

		public override void Prepare(string message)
		{
			_message = message;
		}
	}
}
