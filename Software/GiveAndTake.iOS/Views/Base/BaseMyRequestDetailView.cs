using GiveAndTake.Core.ViewModels;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views.Popups
{
	[MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext, ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
	public class BaseMyRequestDetailView : BaseView
	{
		protected UIView _contentView;
		protected UIView _contentScrollView;
		protected UIScrollView _scrollView;
		private UIButton _deletePhotoButton;
		private UILabel _lbMyRequestMessage;
		private UIView _overlayView;
		private UILabel _lbMyRequestPopupTitle;
		private UILabel _lbSentTo;
		private UILabel _lbGiverUserName;
		private UILabel _lbRequestDate;
		protected override void InitView()
		{
			View.BackgroundColor = UIColor.Clear;
			InitOverlayView();
			InitBaseContentView();
		}
		protected override void CreateBinding()
		{
			var bindingSet = this.CreateBindingSet<BaseMyRequestDetailView, BaseMyRequestDetailViewModel>();

			bindingSet.Bind(_lbMyRequestPopupTitle)
				.For(v => v.Text)
				.To(vm => vm.MyRequestPopupTitle);

			bindingSet.Bind(_deletePhotoButton.Tap())
				.For(v => v.Command)
				.To(vm => vm.CloseCommand);

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

			bindingSet.Apply();
		}

		protected void InitOverlayView()
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

		protected void InitBaseContentView()
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
			_deletePhotoButton = UIHelper.CreateImageButton(DimensionHelper.DeletePhotoButtonWidth,
				DimensionHelper.DeletePhotoButtonWidth, ImageHelper.DeleteRequestDetailButton);
			_contentView.AddSubview(_deletePhotoButton);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_deletePhotoButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Top, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_deletePhotoButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.DefaultMargin)
			});
			_lbSentTo = UIHelper.CreateLabel(UIColor.DarkGray, DimensionHelper.MediumTextSize);
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

			_lbRequestDate = UIHelper.CreateLabel(UIColor.DarkGray, DimensionHelper.SmallTextSize);
			_contentView.AddSubview(_lbRequestDate);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbRequestDate, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _lbGiverUserName,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_lbRequestDate, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _lbSentTo,
					NSLayoutAttribute.Left, 1, 0)
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
			_contentScrollView = UIHelper.CreateView(0, DimensionHelper.PopupContentWidth, UIColor.White, 0);
			_scrollView.AddSubview(_contentScrollView);
			_scrollView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_contentScrollView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _scrollView,
					NSLayoutAttribute.Top, 1, 0),

			});
			_lbMyRequestMessage = UIHelper.CreateLabel(UIColor.DarkGray, DimensionHelper.PostDescriptionTextSize);
			_contentScrollView.Add(_lbMyRequestMessage);
			_contentScrollView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbMyRequestMessage, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _contentScrollView,
					NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_lbMyRequestMessage, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _contentScrollView,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_lbMyRequestMessage, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _contentScrollView,
					NSLayoutAttribute.Right, 1, -DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_contentScrollView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _lbMyRequestMessage,
					NSLayoutAttribute.Bottom, 1, 0),
			});
		}
	}
}