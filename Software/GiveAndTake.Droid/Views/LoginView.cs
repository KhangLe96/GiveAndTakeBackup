using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using GiveAndTake.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Xamarin.Facebook.Login.Widget;

namespace GiveAndTake.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(
        Label = "GiveAndTake.Droid.Views",
        LaunchMode = LaunchMode.SingleTop
    )]
    public class LoginView : MvxAppCompatActivity<LoginViewModel>
    {
        private ICallbackManager callbackManager;

        private readonly List<string> permissions = new List<string> {"public_profile"};
        private TextView tvName;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.LoginView);

            var loginButton = FindViewById<LoginButton>(Resource.Id.login_button);
            tvName = FindViewById<TextView>(Resource.Id.tvName);

            var loginCallback = new FacebookCallback<LoginResult>
            {
                HandleSuccess = OnLoginSuccess,
                HandleCancel = OnCancelLogin,
                HandleError = OnLoginError
            };

            callbackManager = CallbackManagerFactory.Create();

            loginButton.RegisterCallback(callbackManager, loginCallback);

            LoginManager.Instance.LogInWithReadPermissions(this, permissions);
            LoginManager.Instance.RegisterCallback(callbackManager, loginCallback);
            
        }

        private void OnLoginSuccess(LoginResult loginResult)
        {
            tvName.Text = Profile.CurrentProfile.Name;
        }

        private void OnCancelLogin()
        {
        }

        private void OnLoginError(FacebookException loginException)
        {
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            callbackManager.OnActivityResult(requestCode, (int)resultCode, data);
        }
    }
}