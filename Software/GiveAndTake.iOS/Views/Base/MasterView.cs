using GiveAndTake.Core;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views.Base
{
    [MvxRootPresentation]
    public class MasterView : BaseView
    {
        public UIView ContainerView { get; set; }
        private UIView headerBar;
        private UIImageView logoView;

        protected override void InitView()
        {
            InitHeaderBar();
            InitContainerView();
            InitLabel();
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

        private void InitContainerView()
        {
            ContainerView = new UIView { TranslatesAutoresizingMaskIntoConstraints = false };
            View.Add(ContainerView);
            View.AddConstraints(new []
            {
                NSLayoutConstraint.Create(ContainerView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, View, NSLayoutAttribute.Width, 1, 0),
                NSLayoutConstraint.Create(ContainerView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, headerBar, NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(ContainerView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View, NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(ContainerView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 1, 0),
            });
        }

        private void InitLabel()
        {
            var label = UIHelper.CreateLabel(UIColor.Black, 24, FontType.Bold);
            label.Text = "Master View";
            ContainerView.Add(label);
            ContainerView.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(label, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, ContainerView, NSLayoutAttribute.CenterX, 1, 0),
                NSLayoutConstraint.Create(label, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, ContainerView, NSLayoutAttribute.CenterY, 1, 0)
            });
        }
    }
}