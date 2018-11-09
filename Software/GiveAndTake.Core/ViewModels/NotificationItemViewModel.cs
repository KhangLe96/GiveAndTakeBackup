using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels
{
	public class NotificationItemViewModel : BaseViewModel
	{
		#region Properties

		public Action<Notification> ClickAction { get; set; }

		public IMvxCommand ClickCommand => _clickCommand ?? (_clickCommand = new MvxCommand(HandleOnClicked));

		public List<ITransformation> AvatarTransformations => new List<ITransformation> { new CircleTransformation() };
		public List<ITransformation> PostTransformations => new List<ITransformation> { new CornersTransformation(5, CornerTransformType.AllRounded) };

		public string AvatarUrl
		{
			get => _avatarUrl;
			set => SetProperty(ref _avatarUrl, value);
		}

		public string PostUrl
		{
			get => _postUrl;
			set => SetProperty(ref _postUrl, value);
		}

		public string CreatedTime
		{
			get => _createdTime;
			set => SetProperty(ref _createdTime, value);
		}
		public string Message
		{
			get => _message;
			set => SetProperty(ref _message, value);
		}

		private readonly Notification _notification;
		private IMvxCommand _clickCommand;
		private string _avatarUrl;
		private string _postUrl;
		private string _createdTime;
		private string _message;

		#endregion

		#region Constructor

		public NotificationItemViewModel(Notification notification)
		{
			_notification = notification;
		}

		private void HandleOnClicked()
		{
			ClickAction?.Invoke(_notification);
		}

		#endregion
	}
}
