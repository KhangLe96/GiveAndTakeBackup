using GiveAndTake.iOS.Helpers;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views.Popups
{
	[MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext, ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
	public class MyRequestReceivedView : BaseMyRequestDetailView
	{
		protected override void InitView()
		{
			base.InitView();
			InitContentView();
		}
		protected override void CreateBinding()
		{
			base.CreateBinding();
		}
		private void InitContentView()
		{
			_contentView.AddConstraints(new[]
{
				NSLayoutConstraint.Create(_scrollView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Bottom, 1, -DimensionHelper.DefaultMargin)
			});
			_scrollView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_scrollView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _contentScrollView,
					NSLayoutAttribute.Bottom, 1, 0),
			});
		}
	}
}