using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using UIKit;
using GiveAndTake.iOS.Helpers;

namespace GiveAndTake.iOS.Views.Base
{
    [MvxRootPresentation]
    public class MasterView : BaseView
    {
        private UIView headerBar;
        private UIImageView logoView;

        protected override void InitView()
        {
            InitHeaderBar();
        }

        private void InitHeaderBar()
        {
			headerBar = new UIView { TranslatesAutoresizingMaskIntoConstraints = false };
            logoView = new UIImageView { TranslatesAutoresizingMaskIntoConstraints = false, Image = UIImage.FromFile("Images/Top_logo.png") };

            View.Add(headerBar); 
			View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(headerBar, NSLayoutAttribute.Width, NSLayoutRelation.Equal, View, NSLayoutAttribute.Width, 1, 0),
                NSLayoutConstraint.Create(headerBar, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 100),
                NSLayoutConstraint.Create(headerBar, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View, NSLayoutAttribute.Top, 1, 0),
            });

			headerBar.Add(logoView);
			headerBar.AddConstraints(new[]
            {
                //NSLayoutConstraint.Create(logoView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 220),
                NSLayoutConstraint.Create(logoView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 30),
                NSLayoutConstraint.Create(logoView, NSLayoutAttribute.Left , NSLayoutRelation.Equal, headerBar, NSLayoutAttribute.Left, 1, 100),
                NSLayoutConstraint.Create(logoView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, headerBar, NSLayoutAttribute.Right, 1, -100),
                NSLayoutConstraint.Create(logoView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, headerBar, NSLayoutAttribute.Top, 1, 40),
                NSLayoutConstraint.Create(logoView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, headerBar, NSLayoutAttribute.Bottom, 1, -40),
            });
        }
    }
}