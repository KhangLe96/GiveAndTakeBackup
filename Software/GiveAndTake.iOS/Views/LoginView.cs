using System;
//using Facebook.CoreKit;
//using Facebook.LoginKit;
using Foundation;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.iOS.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views
{
    [MvxRootPresentation]
    public class LoginView : BaseView
    {
        private UIImageView logoImage;
        private UIButton customedLoginFacebookButton;
        private UIButton customedLoginGoogleButton;
        public IMvxCommand<User> LoginCommand { get; set; }

        protected override void InitView()
        {
            InitBackground();
            InitLogoImage();
            InitLoginFbButton();
            InitLoginGoogleButton();
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
            customedLoginFacebookButton = new UIButton { TranslatesAutoresizingMaskIntoConstraints = false };

            customedLoginFacebookButton.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(customedLoginFacebookButton, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null,
                    NSLayoutAttribute.NoAttribute, 1, 40),
                NSLayoutConstraint.Create(customedLoginFacebookButton, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null,
                    NSLayoutAttribute.NoAttribute, 1, 100)
            });

            customedLoginFacebookButton.SetBackgroundImage(new UIImage("Images/facebook_button"), UIControlState.Normal);

            View.Add(customedLoginFacebookButton);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(customedLoginFacebookButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, logoImage,
                    NSLayoutAttribute.Bottom, 1, 20),
                NSLayoutConstraint.Create(customedLoginFacebookButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.CenterX, 1, -15)
            });

            //customedLoginFacebookButton.TouchUpInside += HandleLoginFacebook;
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

            customedLoginGoogleButton.SetBackgroundImage(new UIImage("Images/google_button"), UIControlState.Normal);

            View.Add(customedLoginGoogleButton);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(customedLoginGoogleButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, logoImage,
                    NSLayoutAttribute.Bottom, 1, 20),
                NSLayoutConstraint.Create(customedLoginGoogleButton, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.CenterX, 1, 15)
            });
        }

        //private void HandleLoginFacebook(object sender, EventArgs e)
        //{
        //    // integrate with default login facebook button
        //    var manager = new LoginManager();
        //    manager.LogInWithReadPermissions(new[] { "public_profile" }, this, HandleLoginWithFacebook);
        //}

        //private void HandleLoginWithFacebook(LoginManagerLoginResult result, NSError error)
        //{
        //    if (error != null)
        //    {
        //        // handle error
        //        return;
        //    }

        //    if (result.IsCancelled)
        //    {
        //        // handle cancel
        //        return;
        //    }

        //    var profile = Profile.CurrentProfile;
        //    var userProfile = new User
        //    {
        //        FirstName = profile.FirstName,
        //        LastName = profile.LastName,
        //        UserName = profile.Name,
        //        AvatarUrl = GetProfilePicture(profile.UserID),
        //        SocialAccountId = profile.UserID
        //    };
        //    LoginCommand.Execute(userProfile);
        //}

        private string GetProfilePicture(string profileId) => $"https://graph.facebook.com/{profileId}/picture?type=small";
    }
}