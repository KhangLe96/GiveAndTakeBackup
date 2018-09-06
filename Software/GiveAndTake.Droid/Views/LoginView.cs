using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Widget;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System;
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

        private AccessTokenTracker accessTokenTracker;
        public static AccessToken accessToken;

        public IMvxCommand<BaseUser> LoginCommand { get; set; }

        protected override int LayoutId => Resource.Layout.LoginView;

	    protected override void InitView()
	    {
		    _callbackManager = CallbackManagerFactory.Create();
		    accessToken = AccessToken.CurrentAccessToken;
		    bool isLoggedIn = accessToken != null && !accessToken.IsExpired;
		    if (isLoggedIn)
		    {
			    OnLoginSuccess(null);
		    }
		    else
		    {
			    InitFacebookButton();
		    }
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

            LoginManager.Instance.RegisterCallback(_callbackManager, loginCallback);

            FindViewById<ImageButton>(Resource.Id.btnFb).Click += delegate
            {
                LoginManager.Instance.LogInWithReadPermissions(this, new[] { "public_profile" });
            };
        }

        private void OnLoginSuccess(LoginResult loginResult) => new MyProfileTracker { HandleLogin = user => LoginCommand.Execute(user) }.StartTracking();

	    private void OnCancelLogin() { }

        private void OnLoginError(FacebookException loginException) { }
	}


    public class FacebookCallback<TResult> : Object, IFacebookCallback where TResult : Object
    {
        public Action HandleCancel { get; set; }
        public Action<FacebookException> HandleError { get; set; }
        public Action<TResult> HandleSuccess { get; set; }

        public void OnCancel() => HandleCancel?.Invoke();

	    public void OnError(FacebookException error) => HandleError?.Invoke(error);

	    public void OnSuccess(Object result) => HandleSuccess?.Invoke(result.JavaCast<TResult>());
    }

	public class MyProfileTracker : ProfileTracker
	{
		public Action<BaseUser> HandleLogin { get; set; }
		protected override void OnCurrentProfileChanged(Profile oldProfile, Profile currentProfile)
		{
			var userProfile = new BaseUser
			{
				FirstName = currentProfile.FirstName,
				LastName = currentProfile.LastName,
				UserName = currentProfile.Id,
				AvatarUrl = GetProfilePicture(currentProfile.Id),
				SocialAccountId = currentProfile.Id
			};
			HandleLogin?.Invoke(userProfile);
			StopTracking();
		}
		private static string GetProfilePicture(string profileId) => $"https://graph.facebook.com/{profileId}/picture?type=small";
	}
}