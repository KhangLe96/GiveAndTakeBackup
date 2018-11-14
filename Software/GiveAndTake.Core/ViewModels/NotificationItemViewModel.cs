using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GiveAndTake.Core.Helpers;
using MvvmCross.Commands;
using MvvmCross.UI;

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

		public MvxColor BackgroundColor
		{
			get => _backgroundColor;
			set => SetProperty(ref _backgroundColor, value);
		}

		private readonly Notification _notification;
		private IMvxCommand _clickCommand;
		private string _avatarUrl;
		private string _postUrl;
		private string _createdTime;
		private string _message;
		private MvxColor _backgroundColor;

		#endregion

		#region Constructor

		public NotificationItemViewModel(Notification notification)
		{
			_notification = notification;
			Init();
		}

		private void Init()
		{
			Message = _notification.Message;
			CreatedTime = TimeHelper.ToTimeAgo(_notification.CreatedTime);
			BackgroundColor = _notification.IsRead ? MvxColor.ParseHexString("#FFFFFF") : MvxColor.ParseHexString("#d2f9ff");
			AvatarUrl = _notification.AvatarUrl;
			PostUrl = _notification.PostUrl;
		}

		private void HandleOnClicked()
		{
			ClickAction?.Invoke(_notification);
		}

		#endregion
	}
}
