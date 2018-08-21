using GiveAndTake.Core;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views
{
	[MvxRootPresentation]
	public class PostDetailView : BaseView
	{
		protected override void InitView()
		{
			InitLabel();
		}

		private void InitLabel()
		{
			var label = UiHelper.CreateLabel(UIColor.Black, 24, FontType.Bold);
			label.Text = "Post Detail View";
			View.Add(label);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(label, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(label, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterY, 1, 0)
			});
		}
	}
}