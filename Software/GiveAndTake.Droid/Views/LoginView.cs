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
using Android.Gms.Common;
using Android.OS;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;
namespace GiveAndTake.Droid.Views
{
	[MvxActivityPresentation]
    [Activity(Label = "GiveAndTake.Droid.Views", ScreenOrientation = ScreenOrientation.Portrait)]

    public class LoginView : BaseActivity
    {
	    private static AccessToken accessToken;
        private ICallbackManager _callbackManager;
	    private ImageButton _btnFacebookLogin;
	    static readonly string TAG = "MainActivity";

		public IMvxCommand<BaseUser> LoginCommand { get; set; }
	    public string FireBaseToken { get; set; }
		protected override int LayoutId => Resource.Layout.LoginView;

	    

	    protected override void InitView()
	    {
		    _callbackManager = CallbackManagerFactory.Create();

		    var loginCallback = new FacebookCallback<LoginResult>
		    {
			    HandleSuccess = OnLoginSuccess
		    };

		    LoginManager.Instance.RegisterCallback(_callbackManager, loginCallback);

		    _btnFacebookLogin = FindViewById<ImageButton>(Resource.Id.btnFb);
		    _btnFacebookLogin.Click += OnLoginButtonClicked;			
		}

	    protected override void CreateBinding()
        {
            base.CreateBinding();

            var bindingSet = this.CreateBindingSet<LoginView, LoginViewModel>();

            bindingSet.Bind(this)
                .For(v => v.LoginCommand)
                .To(vm => vm.LoginCommand);

	        bindingSet.Bind(this)
		        .For(v => v.FireBaseToken)
		        .To(vm => vm.FireBaseToken);

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

	    protected override void Dispose(bool disposing)
	    {
			_btnFacebookLogin.Click -= OnLoginButtonClicked;
			base.Dispose(disposing);
	    }

	    private void OnLoginButtonClicked(object sender, EventArgs e) => 
		    LoginManager.Instance.LogInWithReadPermissions(this, new[] {"public_profile", "email"});

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

	    private void HandleSuccessfulLogin()
	    {
		    LoginCommand.Execute(new BaseUser
		    {
			    FirstName = Profile.CurrentProfile.FirstName,
			    LastName = Profile.CurrentProfile.LastName,
			    Name = Profile.CurrentProfile.Name,
			    UserName = Profile.CurrentProfile.Id,
			    AvatarUrl = GetProfilePicture(Profile.CurrentProfile.Id),
			    SocialAccountId = Profile.CurrentProfile.Id
		    });
		    
			var result = IsPlayServicesAvailable();
		    if (result)
		    {
			    FireBaseToken = FirebaseInstanceId.Instance.Token;						
			    Log.Debug(TAG, "InstanceID token: " + FirebaseInstanceId.Instance.Token);
			}		   
		}

	    private static string GetProfilePicture(string profileId) => $"https://graph.facebook.com/{profileId}/picture?type=large";
	   
	    public bool IsPlayServicesAvailable()
	    {
		    int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
		    return resultCode == ConnectionResult.Success;
	    }
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