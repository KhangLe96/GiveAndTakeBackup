//using Facebook.CoreKit;
//using Facebook.LoginKit;
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
        private UIImageView _logoImage;
        private UIButton _customedLoginFacebookButton;
        private UIButton _customedLoginGoogleButton;
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
            _logoImage = new UIImageView { TranslatesAutoresizingMaskIntoConstraints = false, Image = UIImage.FromFile("Images/LoginLogo.png")};

            _logoImage.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_logoImage, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null,
                    NSLayoutAttribute.NoAttribute, 1, 150),
                NSLayoutConstraint.Create(_logoImage, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null,
                    NSLayoutAttribute.NoAttribute, 1, 200)
            });

            View.Add(_logoImage);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_logoImage, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.Top, 1, 130),
                NSLayoutConstraint.Create(_logoImage, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.CenterX, 1, 0)
            });
        }

        private void InitLoginFbButton()
        {
            _customedLoginFacebookButton = new UIButton { TranslatesAutoresizingMaskIntoConstraints = false };

            _customedLoginFacebookButton.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_customedLoginFacebookButton, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null,
                    NSLayoutAttribute.NoAttribute, 1, 40),
                NSLayoutConstraint.Create(_customedLoginFacebookButton, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null,
                    NSLayoutAttribute.NoAttribute, 1, 100)
            });

            _customedLoginFacebookButton.SetBackgroundImage(new UIImage("Images/facebook_button"), UIControlState.Normal);

            View.Add(_customedLoginFacebookButton);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_customedLoginFacebookButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _logoImage,
                    NSLayoutAttribute.Bottom, 1, 20),
                NSLayoutConstraint.Create(_customedLoginFacebookButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.CenterX, 1, -15)
            });

            //customedLoginFacebookButton.TouchUpInside += HandleLoginFacebook;
        }

        private void InitLoginGoogleButton()
        {
            _customedLoginGoogleButton = new UIButton { TranslatesAutoresizingMaskIntoConstraints = false };

            _customedLoginGoogleButton.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_customedLoginGoogleButton, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null,
                    NSLayoutAttribute.NoAttribute, 1, 0),
                NSLayoutConstraint.Create(_customedLoginGoogleButton, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null,
                    NSLayoutAttribute.NoAttribute, 1, 0)
            });

            _customedLoginGoogleButton.SetBackgroundImage(new UIImage("Images/google_button"), UIControlState.Normal);

            View.Add(_customedLoginGoogleButton);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_customedLoginGoogleButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _logoImage,
                    NSLayoutAttribute.Bottom, 1, 20),
                NSLayoutConstraint.Create(_customedLoginGoogleButton, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
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