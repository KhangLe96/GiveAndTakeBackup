using System;
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
using Android.Runtime;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Object = Java.Lang.Object;
using Result = Android.App.Result;

namespace GiveAndTake.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "GiveAndTake.Droid.Views")]

    public class LoginView : BaseActivity
    {
        private ICallbackManager _callbackManager;

        private readonly List<string> _permissions = new List<string> { "public_profile" };

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
            _callbackManager.OnActivityResult(requestCode, (int)resultCode, data);
        }

        private void InitFacebookButton()
        {
            var loginCallback = new FacebookCallback<LoginResult>
            {
                HandleSuccess = OnLoginSuccess,
                HandleCancel = OnCancelLogin,
                HandleError = OnLoginError
            };

            _callbackManager = CallbackManagerFactory.Create();

            LoginManager.Instance.RegisterCallback(_callbackManager, loginCallback);

            FindViewById<ImageButton>(Resource.Id.btnFb).Click += delegate
            {
                LoginManager.Instance.LogInWithReadPermissions(this, _permissions);
            };
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

        private void OnCancelLogin()
        {
        }

        private void OnLoginError(FacebookException loginException)
        {
        }

        private string GetProfilePicture(string profileId) => $"https://graph.facebook.com/{profileId}/picture?type=small";
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