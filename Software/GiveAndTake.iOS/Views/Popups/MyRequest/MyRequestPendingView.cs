using GiveAndTake.Core;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views.Popups
{
	[MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext, ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
	public class MyRequestPendingView : BaseMyRequestDetailView
	{
		private UIButton _btnRemoveRequest;

		protected override void InitView()
		{
			base.InitView();
			InitContentView();
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();
			var bindingSet = this.CreateBindingSet<MyRequestPendingView, MyRequestPendingViewModel>();

			bindingSet.Bind(_btnRemoveRequest)
				.For("Title")
				.To(vm => vm.BtnRemoveRequestTitle);

			bindingSet.Bind(_btnRemoveRequest.Tap())
				.For(v => v.Command)
				.To(vm => vm.RemoveRequestCommand);
			bindingSet.Apply();
		}

		private void InitContentView()
		{
			_btnRemoveRequest = UIHelper.CreateAlphaButton(DimensionHelper.PopupRequestButtonWidth,
					DimensionHelper.PopupRequestButtonHeight,
					ColorHelper.LightBlue, ColorHelper.DarkBlue, DimensionHelper.MediumTextSize,
					UIColor.White, UIColor.White, ColorHelper.LightBlue, ColorHelper.DarkBlue,
					true, true, FontType.Light);
			_contentView.Add(_btnRemoveRequest);
			_contentView.AddConstraints(new[]
				{
				NSLayoutConstraint.Create(_btnRemoveRequest, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Bottom, 1, -DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_btnRemoveRequest, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.CenterX, 1, 0)
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