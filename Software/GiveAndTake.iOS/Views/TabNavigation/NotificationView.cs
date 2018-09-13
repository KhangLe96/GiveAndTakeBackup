﻿using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views.TabNavigation
{
	[MvxTabPresentation(TabIconName = "Images/notification_off",
		TabSelectedIconName = "Images/notification_on",
		WrapInNavigationController = true)]
	public class NotificationView : BaseView
	{
		protected override void InitView()
		{
			UILabel testLabel = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.MediumTextSize);
			testLabel.Text = "Notification View";
			View.Add(testLabel);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(testLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(testLabel, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.CenterY, 1, 0)
			});
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			NavigationController?.SetNavigationBarHidden(true, animated);
		}
	}
}