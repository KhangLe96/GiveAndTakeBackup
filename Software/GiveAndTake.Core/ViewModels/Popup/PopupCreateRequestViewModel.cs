using System;
using System.Collections.Generic;
using System.Text;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels.Popup
{
	public class PopupCreateRequestViewModel : BaseViewModel<Post, bool>
	{
		#region Properties

		private string _avatarUrl;
		private string _userName;

		public string PopupTitle { get; set; } = "Thông tin trao đổi";
		public string SendTo { get; set; } = "Gửi đến:";
		public string PopupInputInformationPlaceHolder { get; set; } = "Thông tin trao đổi ...";
		public string BtnSubmitTitle { get; set; } = "Gửi";
		public string BtnCancelTitle { get; set; } = "Hủy";

		public string AvatarUrl
		{
			get => _avatarUrl;
			set => SetProperty(ref _avatarUrl, value);
		}

		public string UserName
		{
			get => _userName;
			set => SetProperty(ref _userName, value);
		}

		#endregion

		#region Constructor

		public override void Prepare(Post post)
		{
			AvatarUrl = post.User.AvatarUrl;
			UserName = post.User.FullName ?? AppConstants.DefaultUserName;
		}

		private void InitCommand()
		{
			CloseCommand = new MvxAsyncCommand(() => NavigationService.Close(this, false));
		}

		#endregion

		#region Methods

		public IMvxAsyncCommand CloseCommand { get; set; }

		#endregion
	}
}
