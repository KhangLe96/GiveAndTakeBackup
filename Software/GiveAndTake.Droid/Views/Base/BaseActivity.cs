using Android.App;
using Android.Content.PM;
using Android.OS;
using HockeyApp.Android;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace GiveAndTake.Droid.Views.Base
{
	[Activity(Label = "BaseActivity", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public abstract class BaseActivity : MvxAppCompatActivity
    {
	    public const string HockeyAppid = "9f427f51ef0e48b4b86952c0f02fa65a";

		protected abstract int LayoutId { get; }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            if (ViewModel != null)
            {
                SetContentView(LayoutId);
                InitView();
                CreateBinding();
            }
        }

        protected abstract void InitView();

        protected virtual void CreateBinding()
        {
        }

	    protected override void OnCreate(Bundle bundle)
	    {
		    base.OnCreate(bundle);

		    CrashManager.Register(this, HockeyAppid);
		}

	    protected override void OnResume()
	    {
		    base.OnResume();

		    Tracking.StartUsage(this);
		}

	    protected override void OnPause()
	    {
		    Tracking.StopUsage(this);

		    base.OnPause();
	    }
	}
}