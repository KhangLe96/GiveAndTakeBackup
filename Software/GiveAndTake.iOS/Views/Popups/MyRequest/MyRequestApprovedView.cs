using GiveAndTake.Core;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.iOS.Controls;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views.Popups
{
	[MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext, ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
	public class MyRequestApprovedView : BaseMyRequestDetailView
	{
		private UIButton _btnReceived;
		private AlphaUiButton _btnRemoveRequest;

		protected override void InitView()
		{
			base.InitView();
			InitContentView();
		}
		protected override void CreateBinding()
		{
			base.CreateBinding();
			var bindingSet = this.CreateBindingSet<MyRequestApprovedView, MyRequestApprovedViewModel>();
			bindingSet.Bind(_btnReceived)
				.For("Title")
				.To(vm => vm.BtnReceivedTitle);

			bindingSet.Bind(_btnRemoveRequest)
				.For("Title")
				.To(vm => vm.BtnRemoveRequestTitle);

			bindingSet.Bind(_btnReceived.Tap())
				.For(v => v.Command)
				.To(vm => vm.ReceivedCommand);

			bindingSet.Bind(_btnRemoveRequest.Tap())
				.For(v => v.Command)
				.To(vm => vm.RemoveRequestCommand);
			bindingSet.Apply();
		}
		private void InitContentView()
		{
			_btnRemoveRequest = UIHelper.CreateAlphaButton(DimensionHelper.PopupRequestButtonWidth,
					DimensionHelper.CreatePostButtonHeight,
					ColorHelper.LightBlue, ColorHelper.DarkBlue, DimensionHelper.MediumTextSize,
					UIColor.White, UIColor.White, ColorHelper.LightBlue, ColorHelper.DarkBlue,
					true, true, FontType.Light);
			_contentView.Add(_btnRemoveRequest);
			_contentView.AddConstraints(new[]
				{
				NSLayoutConstraint.Create(_btnRemoveRequest, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Bottom, 1, -DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_btnRemoveRequest, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _contentView,
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
				NSLayoutConstraint.Create(_scrollView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _btnRemoveRequest,
					NSLayoutAttribute.Top, 1, -DimensionHelper.DefaultMargin)
			});
			_scrollView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_scrollView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _contentScrollView,
					NSLayoutAttribute.Bottom, 1, 0),
			});
		}
	}
}