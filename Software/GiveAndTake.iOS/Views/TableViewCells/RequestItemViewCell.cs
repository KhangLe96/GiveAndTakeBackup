using System;
using Foundation;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.iOS.Controls;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using UIKit;

namespace GiveAndTake.iOS.Views.TableViewCells
{
	[Register(nameof(RequestItemViewCell))]
	public class RequestItemViewCell : MvxTableViewCell
	{
		private UIView _profileView;

		private CustomMvxCachedImageView _imgAvatar;
		private UILabel _lbUserName;
		private UILabel _lbRequestDate;
		private UILabel _lbMessage;
		private UIButton _btnAccept;
		private UIButton _btnReject;
		private UIView _seperatorLine;

		public RequestItemViewCell(IntPtr handle) : base(handle)
		{
			InitViews();
			CreateBinding();
		}

		private void CreateBinding()
		{
			var set = this.CreateBindingSet<RequestItemViewCell, RequestItemViewModel>();

			set.Bind(_profileView.Tap())
				.For(v => v.Command)
				.To(vm => vm.ClickCommand);

			set.Bind(_imgAvatar)
				.For(v => v.ImageUrl)
				.To(vm => vm.AvatarUrl);

			set.Bind(_lbUserName)
				.For(v => v.Text)
				.To(vm => vm.UserName);

			set.Bind(_lbRequestDate)
				.For(v => v.Text)
				.To(vm => vm.CreatedTime);

			set.Bind(_lbMessage)
				.For(v => v.Text)
				.To(vm => vm.RequestMessage);

			set.Bind(_btnAccept)
				.For("Title")
				.To(vm => vm.Acceptance);

			set.Bind(_btnAccept.Tap())
				.For(v => v.Command)
				.To(vm => vm.AcceptCommand);

			set.Bind(_btnReject)
				.For("Title")
				.To(vm => vm.Rejection);

			set.Bind(_btnReject.Tap())
				.For(v => v.Command)
				.To(vm => vm.RejectCommand);

			set.Bind(_seperatorLine)
				.For("Visibility")
				.To(vm => vm.IsSeperatorShown)
				.WithConversion("InvertBool");

			set.Apply();
		}

		private void InitViews()
		{
			InitProfileView();

			InitAvatarImageView();
			InitUserNameLabel();
			InitRequestDateLabel();
			InitAcceptButton();
			InitRejectButton();
			InitMessageLabel();
			InitSeperatorLine();
		}
		private void InitProfileView()
		{
			_profileView = UIHelper.CreateView(0, 0, UIColor.White);
			ContentView.Add(_profileView);
			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_profileView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_profileView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Left, 1, 0),
				NSLayoutConstraint.Create(_profileView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Right, 1, 0),
			});
		}
		private void InitAvatarImageView()
		{
			_imgAvatar = UIHelper.CreateCustomImageView(DimensionHelper.ImageAvatarSize, DimensionHelper.ImageAvatarSize, ImageHelper.DefaultAvatar, DimensionHelper.ImageAvatarSize / 2);
			_imgAvatar.SetPlaceHolder(ImageHelper.DefaultAvatar, ImageHelper.DefaultAvatar);

			_profileView.AddSubview(_imgAvatar);

			_profileView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_imgAvatar, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _profileView,
					NSLayoutAttribute.Top, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_imgAvatar, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _profileView,
					NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort)
			});
		}

		private void InitUserNameLabel()
		{
			_lbUserName = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.MediumTextSize);

			_profileView.AddSubview(_lbUserName);

			_profileView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbUserName, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _profileView,
					NSLayoutAttribute.Top, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_lbUserName, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imgAvatar,
					NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal)
			});
		}

		private void InitRequestDateLabel()
		{
			_lbRequestDate = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);

			_profileView.AddSubview(_lbRequestDate);

			_profileView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbRequestDate, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _lbUserName,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_lbRequestDate, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imgAvatar,
					NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal)
			});
		}


		private void InitMessageLabel()
		{
			_lbMessage = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.PostDescriptionTextSize);
			_lbMessage.Lines = 2;
			_lbMessage.LineBreakMode = UILineBreakMode.TailTruncation;

			_profileView.AddSubview(_lbMessage);

			_profileView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbMessage, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _lbRequestDate,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_lbMessage, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imgAvatar,
					NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(_lbMessage, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _profileView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_profileView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _lbMessage,
					NSLayoutAttribute.Bottom, 1, 0)
			});
		}

		private void InitAcceptButton()
		{
			_btnAccept = UIHelper.CreateButton(DimensionHelper.RequestActionButtonHeight,
				DimensionHelper.RequestActionButtonWidth,
				ColorHelper.Blue,
				UIColor.White,
				DimensionHelper.ButtonTextSize,
				DimensionHelper.RequestActionButtonHeight / 2);

			ContentView.AddSubview(_btnAccept);

			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnAccept, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _profileView,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_btnAccept, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.DefaultMargin)
			});
		}

		private void InitRejectButton()
		{
			_btnReject = UIHelper.CreateButton(DimensionHelper.RequestActionButtonHeight,
				DimensionHelper.RequestActionButtonWidth,
				UIColor.White,
				ColorHelper.Blue,
				DimensionHelper.ButtonTextSize,
				null,
				DimensionHelper.RequestActionButtonHeight / 2,
				ColorHelper.Blue,
				DimensionHelper.PopupCancelButtonBorder);

			ContentView.AddSubview(_btnReject);

			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnReject, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _btnAccept,
					NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_btnReject, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _btnAccept,
					NSLayoutAttribute.Left, 1, - DimensionHelper.DefaultMargin)
			});
		}
		private void InitSeperatorLine()
		{
			_seperatorLine = UIHelper.CreateView(DimensionHelper.SeperatorHeight, 0, ColorHelper.Blue);

			ContentView.AddSubview(_seperatorLine);

			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_seperatorLine, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _btnAccept,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_seperatorLine, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _profileView,
					NSLayoutAttribute.Left, 1,  DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_seperatorLine, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(ContentView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _seperatorLine,
					NSLayoutAttribute.Bottom, 1, 0)
			});

		}
	}
}