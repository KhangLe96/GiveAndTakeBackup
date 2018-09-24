using Facebook.CoreKit;
using Facebook.LoginKit;
using Foundation;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.iOS.Controls;
using GiveAndTake.iOS.Helpers;
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
        private UIImageView _contentView;
        private PopupItemLabel _loginTitle;
        public MvxCommand<BaseUser> LoginCommand { get; set; }

        protected override void InitView()
        {
            InitBackground();
            InitContent();
        }
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Profile.Notifications.ObserveDidChange((sender, e) => {

                if (e.NewProfile == null)
                    return;
                
                var facebookProfile = e.NewProfile;
                LoginCommand?.Execute(GetUserProfile(facebookProfile));
            });
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
            _contentView = UIHelper.CreateImageView(ResolutionHelper.Width, ResolutionHelper.Height, UIColor.White, ImageHelper.LoginBackground);
            _contentView.UserInteractionEnabled = true;

            View.Add(_contentView);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_contentView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.CenterY, 1, 0),
                NSLayoutConstraint.Create(_contentView, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.CenterX, 1, 0)
            });
        }

        private void InitContent()
        {
            _logoImage = UIHelper.CreateImageView(DimensionHelper.LoginLogoWidth, DimensionHelper.LoginLogoHeight, UIColor.White, ImageHelper.LoginLogo);
            _loginTitle = UIHelper.CreateLabel(ColorHelper.Blue, DimensionHelper.LoginTitleTextSize);
            _customedLoginFacebookButton = UIHelper.CreateImageButton(DimensionHelper.LoginButtonHeight, DimensionHelper.LoginButtonWidth, ImageHelper.FacebookButton);
            _customedLoginGoogleButton = UIHelper.CreateImageButton(DimensionHelper.LoginButtonHeight, DimensionHelper.LoginButtonWidth, ImageHelper.GoogleButton);

            _loginTitle.Text = "Đăng nhập với tài khoản";

            _contentView.AddSubviews(_logoImage, _loginTitle, _customedLoginFacebookButton);

            _contentView.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_loginTitle, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _contentView,
                    NSLayoutAttribute.CenterY, 1, - DimensionHelper.MarginNormal),
                NSLayoutConstraint.Create(_loginTitle, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _contentView,
                    NSLayoutAttribute.CenterX, 1, 0),

                NSLayoutConstraint.Create(_logoImage, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _loginTitle,
                    NSLayoutAttribute.Top, 1, - DimensionHelper.MarginNormal),
                NSLayoutConstraint.Create(_logoImage, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _contentView,
                    NSLayoutAttribute.CenterX, 1, 0),

                NSLayoutConstraint.Create(_customedLoginFacebookButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _contentView,
                    NSLayoutAttribute.CenterY, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(_customedLoginFacebookButton, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _contentView,
                    NSLayoutAttribute.CenterX, 1, 0),
            });

            _customedLoginFacebookButton.AddGestureRecognizer(new UITapGestureRecognizer(LoginToFacebook));

        }

        private void LoginToFacebook()
        {
            // integrate with default login facebook button
            var manager = new LoginManager();
            manager.LogInWithReadPermissions(new[] { "public_profile", "email" }, this, HandleLoginResult);
        }

        private void HandleLoginResult(LoginManagerLoginResult result, NSError error)
        {
            if (error != null)
            {
                // handle error
                return;
            }

            if (result.IsCancelled)
            {
                // handle cancel
                return;
            }

            Profile.LoadCurrentProfile((facebookProfile, profileError) =>
            {
                if (facebookProfile != null)
                {
                    LoginCommand?.Execute(GetUserProfile(facebookProfile));
                }
            });
        }

        private User GetUserProfile(Profile facebookProfile)
        {
            var userProfile = new User
            {
                FirstName = facebookProfile.FirstName,
                LastName = facebookProfile.LastName,
	            Name = facebookProfile.Name,
                UserName = facebookProfile.Name,
                AvatarUrl = GetProfilePicture(facebookProfile.UserID),
                SocialAccountId = facebookProfile.UserID
            };
            return userProfile;
        }

        private string GetProfilePicture(string profileId) => $"https://graph.facebook.com/{profileId}/picture?type=small";
    }
}
