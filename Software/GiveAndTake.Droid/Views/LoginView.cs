using Android.App;
using Android.Content;
using Android.Widget;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System.Collections.Generic;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Xamarin.Facebook.Login.Widget;

namespace GiveAndTake.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "GiveAndTake.Droid.Views")]

    public class LoginView : BaseActivity
    {
        private ICallbackManager callbackManager;

        private readonly List<string> permissions = new List<string> { "public_profile" };

        public IMvxCommand<BaseUser> LoginCommand { get; set; }

        protected override int LayoutId => Resource.Layout.LoginView;

        protected override void InitView()
        {
            InitFacebookButton();
        }

        protected override void CreateBinding()
        {
            base.CreateBinding();

            var bindingSet = this.CreateBindingSet<LoginView, LoginViewModel>();

            bindingSet.Bind(this)
                .For(v => v.LoginCommand)
                .To(vm => vm.LoginCommand);

            bindingSet.Apply();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            callbackManager.OnActivityResult(requestCode, (int)resultCode, data);
        }

        private void InitFacebookButton()
        {
            var btnFbDefault = FindViewById<LoginButton>(Resource.Id.btnFbDefault);

            var loginCallback = new FacebookCallback<LoginResult>
            {
                HandleSuccess = OnLoginSuccess,
                HandleCancel = OnCancelLogin,
                HandleError = OnLoginError
            };

            callbackManager = CallbackManagerFactory.Create();

            btnFbDefault.RegisterCallback(callbackManager, loginCallback);

            LoginManager.Instance.LogInWithReadPermissions(this, permissions);
            LoginManager.Instance.RegisterCallback(callbackManager, loginCallback);

            var btnFacebookLogin = FindViewById<Button>(Resource.Id.btnFb);
            btnFacebookLogin.Click += delegate { btnFbDefault.PerformClick(); };
        }

        private void OnLoginSuccess(LoginResult loginResult)
        {
            var profile = Profile.CurrentProfile;
            var userProfile = new BaseUser
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                UserName = profile.Id,
                AvatarUrl = GetProfilePicture(profile.Id),
                SocialAccountId = profile.Id
            };
            LoginCommand.Execute(userProfile);
        }

        private string GetProfilePicture(string profileId) => $"https://graph.facebook.com/{profileId}/picture?type=small";

        private void OnCancelLogin()
        {
        }

        private void OnLoginError(FacebookException loginException)
        {
        }
    }
}