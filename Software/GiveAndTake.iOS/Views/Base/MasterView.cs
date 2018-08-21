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
        private UIView _headerBar;
        private UIImageView _logoView;

        protected override void InitView()
        {
            InitHeaderBar();
            InitContainerView();
            InitLabel();
        }

        private void InitHeaderBar()
        {
			_headerBar = new UIView { TranslatesAutoresizingMaskIntoConstraints = false };
            _logoView = new UIImageView { TranslatesAutoresizingMaskIntoConstraints = false, Image = UIImage.FromFile("Images/Top_logo.png") };

            View.Add(_headerBar); 
			View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_headerBar, NSLayoutAttribute.Width, NSLayoutRelation.Equal, View, NSLayoutAttribute.Width, 1, 0),
                NSLayoutConstraint.Create(_headerBar, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 100),
                NSLayoutConstraint.Create(_headerBar, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View, NSLayoutAttribute.Top, 1, 0),
            });

			_headerBar.Add(_logoView);
			_headerBar.AddConstraints(new[]
            {
                //NSLayoutConstraint.Create(logoView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 220),
                NSLayoutConstraint.Create(_logoView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 30),
                NSLayoutConstraint.Create(_logoView, NSLayoutAttribute.Left , NSLayoutRelation.Equal, _headerBar, NSLayoutAttribute.Left, 1, 100),
                NSLayoutConstraint.Create(_logoView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _headerBar, NSLayoutAttribute.Right, 1, -100),
                NSLayoutConstraint.Create(_logoView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _headerBar, NSLayoutAttribute.Top, 1, 40),
                NSLayoutConstraint.Create(_logoView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _headerBar, NSLayoutAttribute.Bottom, 1, -40),
            });
        }

        private void InitContainerView()
        {
            ContainerView = new UIView { TranslatesAutoresizingMaskIntoConstraints = false };
            View.Add(ContainerView);
            View.AddConstraints(new []
            {
                NSLayoutConstraint.Create(ContainerView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, View, NSLayoutAttribute.Width, 1, 0),
                NSLayoutConstraint.Create(ContainerView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _headerBar, NSLayoutAttribute.Bottom, 1, 0),
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