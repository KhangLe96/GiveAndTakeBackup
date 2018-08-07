using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using GiveAndTake.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Xamarin.Facebook.Login.Widget;
using Object = Java.Lang.Object;

namespace GiveAndTake.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(
        Label = "GiveAndTake.Droid.Views",
        LaunchMode = LaunchMode.SingleTop
    )]
    public class LoginView : MvxAppCompatActivity<LoginViewModel>
    {
        private ICallbackManager _callbackManager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.LoginView);

            _callbackManager = CallbackManagerFactory.Create();
            var loginButton = FindViewById<LoginButton>(Resource.Id.login_button);

            var loginCallback = new FacebookCallback<LoginResult>()
            {
                HandleSuccess = OnLoginSuccess,
                HandleCancel = OnCancelLogin,
                HandleError = OnLoginError
            };

            loginButton.RegisterCallback(_callbackManager, loginCallback);
            LoginManager.Instance.LogInWithReadPermissions(this , new List<string>
            {
                "public_profile"
            });
            LoginManager.Instance.RegisterCallback(_callbackManager, loginCallback);
        }

        private void OnLoginSuccess(LoginResult loginResult)
        {
            var profile = Profile.CurrentProfile;
            var tvName = FindViewById<TextView>(Resource.Id.tvName);
            tvName.Text = profile.Name;
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
            _callbackManager.OnActivityResult(requestCode, (int) resultCode, data);
        }
    }


   public class FacebookCallback<TResult> : Object, IFacebookCallback where TResult : Object
    {
        public Action HandleCancel { get; set; }
        public Action<FacebookException> HandleError { get; set; }
        public Action<TResult> HandleSuccess { get; set; }

        public void OnCancel()
        {
            var c = HandleCancel;
            c?.Invoke();
        }

        public void OnError(FacebookException error)
        {
            var c = HandleError;
            c?.Invoke(error);
        }

        public void OnSuccess(Object result)
        {
            var c = HandleSuccess;
            c?.Invoke(result.JavaCast<TResult>());
        }
    }
}