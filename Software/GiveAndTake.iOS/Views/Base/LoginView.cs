using Facebook.CoreKit;
using Facebook.LoginKit;
using GiveAndTake.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using System;
using GiveAndTake.Core.Models;
using MvvmCross.Commands;
using UIKit;

namespace GiveAndTake.iOS.Views.Base
{
    [MvxRootPresentation]
    public class LoginView : BaseView
    {
        private LoginButton defaultLoginButton;
        private UIImageView logoImage;
        private UIButton customedLoginFacebookButton;
        private UIButton customedLoginGoogleButton;
        public IMvxCommand<UserProfile> LoginCommand { get; set; }

        protected override void InitView()
        {
            InitBackground();
            InitLogoImage();
            InitLoginFbButton();
            InitLoginGoogleButton();
            InitDefaultLoginFacebookButton();
        }

        protected override void CreateBinding()
        {
            base.CreateBinding();

            var set = this.CreateBindingSet<LoginView, LoginViewModel>();

            set.Bind(this)
                .For(v => v.LoginCommand)
                .To(vm => vm.LoginCommand);

            set.Apply();
        }

        private void InitBackground()
        {
            View.BackgroundColor = UIColor.FromRGB(15, 188, 249);
        }

        private void InitLogoImage()
        {
            logoImage = new UIImageView { TranslatesAutoresizingMaskIntoConstraints = false, Image = UIImage.FromFile("Images/LoginLogo.png")};

            logoImage.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(logoImage, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null,
                    NSLayoutAttribute.NoAttribute, 1, 150),
                NSLayoutConstraint.Create(logoImage, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null,
                    NSLayoutAttribute.NoAttribute, 1, 200)
            });

            View.Add(logoImage);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(logoImage, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.Top, 1, 130),
                NSLayoutConstraint.Create(logoImage, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.CenterX, 1, 0)
            });
        }

        private void InitLoginFbButton()
        {
            InitDefaultLoginFacebookButton();

            customedLoginFacebookButton = new UIButton { TranslatesAutoresizingMaskIntoConstraints = false };

            customedLoginFacebookButton.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(customedLoginFacebookButton, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null,
                    NSLayoutAttribute.NoAttribute, 1, 40),
                NSLayoutConstraint.Create(customedLoginFacebookButton, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null,
                    NSLayoutAttribute.NoAttribute, 1, 100)
            });

            customedLoginFacebookButton.Layer.CornerRadius = 20;
            customedLoginFacebookButton.BackgroundColor = UIColor.FromRGB(35, 143, 205);
            customedLoginFacebookButton.SetTitle("f", UIControlState.Normal);
            customedLoginFacebookButton.SetTitleColor(UIColor.White, UIControlState.Normal);

            View.Add(customedLoginFacebookButton);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(customedLoginFacebookButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, logoImage,
                    NSLayoutAttribute.Bottom, 1, 20),
                NSLayoutConstraint.Create(customedLoginFacebookButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.CenterX, 1, -15)
            });

            customedLoginFacebookButton.TouchUpInside += HandleLoginFacebook;
        }

        private void InitLoginGoogleButton()
        {
            customedLoginGoogleButton = new UIButton() { TranslatesAutoresizingMaskIntoConstraints = false };

            customedLoginGoogleButton.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(customedLoginGoogleButton, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null,
                    NSLayoutAttribute.NoAttribute, 1, 40),
                NSLayoutConstraint.Create(customedLoginGoogleButton, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null,
                    NSLayoutAttribute.NoAttribute, 1, 100)
            });

            customedLoginGoogleButton.Layer.CornerRadius = 20;
            customedLoginGoogleButton.BackgroundColor = UIColor.FromRGB(176, 93, 89);
            customedLoginGoogleButton.SetTitle("G+", UIControlState.Normal);
            customedLoginGoogleButton.SetTitleColor(UIColor.White, UIControlState.Normal);

            View.Add(customedLoginGoogleButton);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(customedLoginGoogleButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, logoImage,
                    NSLayoutAttribute.Bottom, 1, 20),
                NSLayoutConstraint.Create(customedLoginGoogleButton, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.CenterX, 1, 15)
            });
        }

        private void HandleLoginFacebook(object sender, EventArgs e)
        {
            // integrate with default login facebook button
        }

        private void InitDefaultLoginFacebookButton()
        {
            defaultLoginButton = new LoginButton
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                LoginBehavior = LoginBehavior.Native,
                ReadPermissions = new[] { "public_profile" },
            };

            defaultLoginButton.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(defaultLoginButton, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null,
                    NSLayoutAttribute.NoAttribute, 1, 30),
                NSLayoutConstraint.Create(defaultLoginButton, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null,
                    NSLayoutAttribute.NoAttribute, 1, 90)
            });

            View.Add(defaultLoginButton);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(defaultLoginButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.Bottom, 1, 20),
                NSLayoutConstraint.Create(defaultLoginButton, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.CenterX, 1, 0)
            });

            Profile.Notifications.ObserveDidChange(HandleProfileChanged);

            defaultLoginButton.Completed += HandleDefaultLogin;

            defaultLoginButton.LoggedOut += HandleLogout;
        }

        private void HandleDefaultLogin(object sender, LoginButtonCompletedEventArgs e)
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

            var profile = Profile.CurrentProfile;
            var userProfile = new UserProfile
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                UserName = profile.Name,
                ImageUrl = GetProfilePicture(profile.UserID),
                SocialAccountId = profile.UserID
            };
            LoginCommand.Execute(userProfile);
        }

        private string GetProfilePicture(string profileId) => $"https://graph.facebook.com/{profileId}/picture?type=small";

        private void HandleLogout(object sender, EventArgs e)
        {

        }

        private void HandleProfileChanged(object sender, ProfileDidChangeEventArgs e)
        {

        }
    }
}