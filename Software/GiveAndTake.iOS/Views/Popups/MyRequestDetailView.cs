using GiveAndTake.Core;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.Core.ViewModels.Popup;
using GiveAndTake.iOS.Controls;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views.Popups
{
	[MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext, ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
	public class MyRequestDetailView : BaseView
	{
		public string RequestStatus
		{
			get => _requestStatus;
			set => _requestStatus = value;
		}

		private UIView _contentView;
		private UIView _overlayView;
		private UILabel _lbMyRequestPopupTitle;
		private UILabel _lbSentTo;
		private UILabel _lbGiverUserName;
		private UILabel _lbRequestDate;
		private UILabel _lbMyRequestMessage;
		private UIButton _btnReceived;
		private UIButton _btnCancelRequest;
		private UIScrollView _scrollView;
		private string _requestStatus;
		protected override void InitView()
		{
			View.BackgroundColor = UIColor.Clear;

			InitOverlayView();
			InitContentView();
		}
		protected override void CreateBinding()
		{
			var bindingSet = this.CreateBindingSet<MyRequestDetailView, MyRequestDetailViewModel>();

			bindingSet.Bind(_lbMyRequestPopupTitle)
				.For(v => v.Text)
				.To(vm => vm.MyRequestPopupTitle);

			bindingSet.Bind(_lbSentTo)
				.For(v => v.Text)
				.To(vm => vm.SentTo);

			bindingSet.Bind(_lbGiverUserName)
				.For(v => v.Text)
				.To(vm => vm.GiverUserName);

			bindingSet.Bind(_lbRequestDate)
				.For(v => v.Text)
				.To(vm => vm.RequestDate);

			bindingSet.Bind(_lbMyRequestMessage)
				.For(v => v.Text)
				.To(vm => vm.MyRequestMessage);

			bindingSet.Bind(_btnReceived)
				.For("Title")
				.To(vm => vm.BtnReceivedTitle);

			bindingSet.Bind(_btnCancelRequest)
				.For("Title")
				.To(vm => vm.BtnCancelRequestTitle);

			bindingSet.Bind(_btnReceived.Tap())
				.For(v => v.Command)
				.To(vm => vm.ReceivedCommand);

			bindingSet.Bind(_btnCancelRequest.Tap())
				.For(v => v.Command)
				.To(vm => vm.CancelRequestCommand);

			bindingSet.Bind(this)
				.For(v => v.RequestStatus)
				.To(vm => vm.RequestStatus);

			bindingSet.Apply();
		}

		private void InitOverlayView()
		{
			_overlayView = UIHelper.CreateView(0, 0, UIColor.Black.ColorWithAlpha(0.7f));

			View.Add(_overlayView);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_overlayView, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_overlayView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterY, 1, 0),
				NSLayoutConstraint.Create(_overlayView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, View, NSLayoutAttribute.Width, 1, 0),
				NSLayoutConstraint.Create(_overlayView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, View, NSLayoutAttribute.Height, 1, 0),
			});
		}

		private void InitContentView()
		{
			_contentView = UIHelper.CreateView(DimensionHelper.PopupRequestHeight, DimensionHelper.PopupContentWidth, UIColor.White, DimensionHelper.PopupContentRadius);
			_contentView.AddGestureRecognizer(new UITapGestureRecognizer { CancelsTouchesInView = true });

			_overlayView.Add(_contentView);
			_overlayView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_contentView, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _overlayView, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_contentView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _overlayView, NSLayoutAttribute.CenterY, 1, 0)
			});

			_lbMyRequestPopupTitle = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.PopupRequestTitleTextSize);
			_contentView.Add(_lbMyRequestPopupTitle);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbMyRequestPopupTitle, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _contentView, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_lbMyRequestPopupTitle, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _contentView, NSLayoutAttribute.Top, 1, DimensionHelper.DefaultMargin)
			});

			_lbSentTo = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);
			_contentView.AddSubview(_lbSentTo);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbSentTo, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _lbMyRequestPopupTitle,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(_lbSentTo, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin)
			});

			_lbGiverUserName = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.MediumTextSize);
			_contentView.AddSubview(_lbGiverUserName);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbGiverUserName, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _lbMyRequestPopupTitle,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(_lbGiverUserName, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _lbSentTo,
					NSLayoutAttribute.Right, 1, DimensionHelper.DefaultMargin)
			});

			_lbRequestDate = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);
			_contentView.AddSubview(_lbRequestDate);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbRequestDate, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _lbGiverUserName,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_lbRequestDate, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _lbSentTo,
					NSLayoutAttribute.Right, 1, DimensionHelper.DefaultMargin)
			});
			_scrollView = UIHelper.CreateScrollView(0, 0);
			_contentView.AddSubview(_scrollView);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_scrollView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _lbRequestDate,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_scrollView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Left, 1, 0),
				NSLayoutConstraint.Create(_scrollView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Right, 1, 0),
			});
			_lbMyRequestMessage = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.PostDescriptionTextSize);
			_scrollView.Add(_lbMyRequestMessage);
			_scrollView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbMyRequestMessage, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _scrollView,
					NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_lbMyRequestMessage, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _scrollView,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_lbMyRequestMessage, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _scrollView,
					NSLayoutAttribute.Right, 1, -DimensionHelper.DefaultMargin),
			});
			//Pending
			if (RequestStatus == "Pending")
			{
				_btnCancelRequest = UIHelper.CreateAlphaButton(DimensionHelper.PopupRequestButtonWidth,
				DimensionHelper.CreatePostButtonHeight,
				ColorHelper.LightBlue, ColorHelper.DarkBlue, DimensionHelper.MediumTextSize,
				UIColor.White, UIColor.White, ColorHelper.LightBlue, ColorHelper.DarkBlue,
				true, true, FontType.Light);
				_contentView.Add(_btnCancelRequest);
				_contentView.AddConstraints(new[]
				{
				NSLayoutConstraint.Create(_btnCancelRequest, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Bottom, 1, -DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_btnCancelRequest, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin)
				});

				_btnReceived = UIHelper.CreateAlphaButton(DimensionHelper.PopupRequestButtonWidth,
					DimensionHelper.CreatePostButtonHeight,
					UIColor.White, UIColor.White, DimensionHelper.MediumTextSize,
					ColorHelper.LightBlue, ColorHelper.DarkBlue, ColorHelper.LightBlue, ColorHelper.DarkBlue, true, false, FontType.Light);
				_contentView.Add(_btnReceived);
				_contentView.AddConstraints(new[]
				{
				NSLayoutConstraint.Create(_btnReceived, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Bottom, 1, -DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_btnReceived, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Right, 1, -DimensionHelper.DefaultMargin)
				});
				_contentView.AddConstraints(new[]
					{
				NSLayoutConstraint.Create(_scrollView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _btnCancelRequest,
					NSLayoutAttribute.Top, 1, DimensionHelper.DefaultMargin),
				});
				_scrollView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbMyRequestMessage, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _scrollView,
					NSLayoutAttribute.Bottom, 1, 0),
				});
			}

			else if (RequestStatus == "Accepted")
			{
				_btnCancelRequest = UIHelper.CreateAlphaButton(DimensionHelper.PopupRequestButtonWidth,
				DimensionHelper.CreatePostButtonHeight,
				ColorHelper.LightBlue, ColorHelper.DarkBlue, DimensionHelper.MediumTextSize,
				UIColor.White, UIColor.White, ColorHelper.LightBlue, ColorHelper.DarkBlue,
				true, true, FontType.Light);
				_contentView.Add(_btnCancelRequest);
				_contentView.AddConstraints(new[]
				{
				NSLayoutConstraint.Create(_btnCancelRequest, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Bottom, 1, -DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_btnCancelRequest, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin)
				});
				_contentView.AddConstraints(new[]
					{
				NSLayoutConstraint.Create(_scrollView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _btnCancelRequest,
					NSLayoutAttribute.Top, 1, DimensionHelper.DefaultMargin),
				});
				_scrollView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbMyRequestMessage, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _scrollView,
					NSLayoutAttribute.Bottom, 1, 0),
				});
			}
			else if (RequestStatus == "Received")
			{

			}

		}
	}
}