using Android.App;
using Android.Content.PM;
using GiveAndTake.Core;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace GiveAndTake.Droid
{
	[Activity(
		Label = "SplashScreen"
		, MainLauncher = true
		, NoHistory = true
		, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashScreen : MvxSplashScreenAppCompatActivity<MvxAppCompatSetup<App>, App>
	{
		public SplashScreen()
			: base(Resource.Layout.SplashScreen)
		{
		}
	}
}