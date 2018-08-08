using System.Collections.Generic;
using CoreGraphics;
using Facebook.CoreKit;
using Facebook.LoginKit;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views.Base
{
    [MvxRootPresentation]
    public class LoginView : BaseView
    {
        private UILabel lbLogin, nameLabel;
        private LoginButton loginButton;

        protected override void InitView()
        {
            InitLabelLogin();
            InitLoginFbButton();
            InitLabelUsername();
        }

        protected override void CreateBinding()
        {
            base.CreateBinding();
            var set = this.CreateBindingSet<LoginView, LoginViewModel>();

            set.Bind(lbLogin)
                .For(v => v.Text)
                .To(vm => vm.Login);

            Profile.Notifications.ObserveDidChange((sender, e) =>
            {
                if (e.NewProfile == null)
                    return;

                nameLabel.Text = e.NewProfile.Name;
            });

            loginButton.Completed += HandleLogin;

            loginButton.LoggedOut += (sender, e) =>
            {
                nameLabel.Text = "username";
            };

            set.Apply();
        }

        private void HandleLogin(object sender, LoginButtonCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // handle error
                return;
            }

            if (e.Result.IsCancelled)
            {
                // handle cancel
                return;
            }

            nameLabel.Text = Profile.CurrentProfile.Name;
        }

        private void InitLabelLogin()
        {
            lbLogin = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);

            View.Add(lbLogin);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(lbLogin, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.CenterX, 1, 0),
                NSLayoutConstraint.Create(lbLogin, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.CenterY, 1, 0),
            });
        }

        private void InitLoginFbButton()
        {
            loginButton = new LoginButton
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                LoginBehavior = LoginBehavior.Native,
                ReadPermissions = new[] {"public_profile"}
            };

            loginButton.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(loginButton, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null,
                    NSLayoutAttribute.NoAttribute, 1, 30),
                NSLayoutConstraint.Create(loginButton, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null,
                    NSLayoutAttribute.NoAttribute, 1, 90)
            });

            View.Add(loginButton);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(loginButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, lbLogin,
                    NSLayoutAttribute.Bottom, 1, 20),
                NSLayoutConstraint.Create(loginButton, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.CenterX, 1, 0)
            });
        }

        private void InitLabelUsername()
        {
            nameLabel = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.MediumTextSize);

            View.Add(nameLabel);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(nameLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, loginButton,
                    NSLayoutAttribute.Bottom, 1, 20),
                NSLayoutConstraint.Create(nameLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.CenterX, 1, 0)
            });
        }
    }
}