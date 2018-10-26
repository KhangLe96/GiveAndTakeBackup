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
using Android.Content.PM;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Object = Java.Lang.Object;
using Result = Android.App.Result;

namespace GiveAndTake.Droid.Views
{
	[MvxActivityPresentation]
    [Activity(Label = "GiveAndTake.Droid.Views", ScreenOrientation = ScreenOrientation.Portrait)]

    public class LoginView : BaseActivity
    {
        private ICallbackManager _callbackManager;

        private AccessTokenTracker accessTokenTracker;
        public static AccessToken accessToken;

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

	    protected override void OnViewModelSet()
	    {
		    base.OnViewModelSet();

			accessToken = AccessToken.CurrentAccessToken;
		    bool isLoggedIn = accessToken != null && !accessToken.IsExpired;
		    if (isLoggedIn)
		    {
				HandleSuccessfulLogin();
		    }
		}

	    protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
	        _callbackManager.OnActivityResult(requestCode, (int)resultCode, data);
			base.OnActivityResult(requestCode, resultCode, data);
        }

        private void InitFacebookButton()
        {
	        _callbackManager = CallbackManagerFactory.Create();

			var loginCallback = new FacebookCallback<LoginResult>
            {
                HandleSuccess = OnLoginSuccess,
                HandleCancel = OnCancelLogin,
                HandleError = OnLoginError
            };

            LoginManager.Instance.RegisterCallback(_callbackManager, loginCallback);

            FindViewById<ImageButton>(Resource.Id.btnFb).Click += delegate
            {
                LoginManager.Instance.LogInWithReadPermissions(this, new[] { "public_profile" , "email" });
            };
        }

	    private void OnLoginSuccess(LoginResult loginResult)
	    {
		    if (Profile.CurrentProfile == null)
		    {
				new MyProfileTracker { HandleLogin = HandleSuccessfulLogin }.StartTracking();
			}
		    else
		    {
			    HandleSuccessfulLogin();
		    }
	    }

	    private void HandleSuccessfulLogin() => LoginCommand.Execute(new BaseUser
	    {
		    FirstName = Profile.CurrentProfile.FirstName,
		    LastName = Profile.CurrentProfile.LastName,
			Name = Profile.CurrentProfile.Name,
		    UserName = Profile.CurrentProfile.Id,
		    AvatarUrl = GetProfilePicture(Profile.CurrentProfile.Id),
		    SocialAccountId = Profile.CurrentProfile.Id
	    });

	    private void OnCancelLogin() { }

        private void OnLoginError(FacebookException loginException) { }

	    private static string GetProfilePicture(string profileId) => $"https://graph.facebook.com/{profileId}/picture?type=small";
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
		public Action HandleLogin { get; set; }
		protected override void OnCurrentProfileChanged(Profile oldProfile, Profile currentProfile)
		{
			StopTracking();
			HandleLogin?.Invoke();
		}
	}
}