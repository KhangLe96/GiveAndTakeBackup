using System.Collections.Generic;
using CoreGraphics;
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
        private UILabel lbLogin;
        private LoginButton loginButton;
        private readonly List<string> readPermissions = new List<string> { "public_profile" };

        protected override void InitView()
        {
            lbLogin = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.MediumTextSize);
            loginButton = new LoginButton(new CGRect(80, 20, 220, 46))
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                LoginBehavior = LoginBehavior.Native,
                ReadPermissions = readPermissions.ToArray()
            };

            View.Add(lbLogin);
            View.Add(loginButton);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(lbLogin, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterX, 1, 0),
                NSLayoutConstraint.Create(lbLogin, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterY, 1, 0),
                NSLayoutConstraint.Create(loginButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, lbLogin, NSLayoutAttribute.Bottom, 1, 20),
                NSLayoutConstraint.Create(loginButton, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterX, 1, 0)
            });
         }

        protected override void CreateBinding()

        {
            base.CreateBinding();
            var set = this.CreateBindingSet<LoginView, LoginViewModel>();

            set.Bind(lbLogin)
                .For(v => v.Text)
                .To(vm => vm.Login);

            set.Bind(loginButton);

            set.Apply();
        }
    }
}