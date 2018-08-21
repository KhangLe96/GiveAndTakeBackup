using GiveAndTake.Core;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views
{
    [MvxRootPresentation]
    public class UserProfileView : BaseView
    {
        protected override void InitView()
        {
            InitLabel();
        }

        private void InitLabel()
        {
            var label = UIHelper.CreateLabel(UIColor.Black, 24, FontType.Bold);
            label.Text = "User Profile View";
            View.Add(label);
            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(label, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterX, 1, 0),
                NSLayoutConstraint.Create(label, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterY, 1, 0)
            });
        }
    }
}