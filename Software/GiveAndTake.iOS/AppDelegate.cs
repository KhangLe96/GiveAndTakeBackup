using Foundation;
using GiveAndTake.Core;
using MvvmCross.Platforms.Ios.Core;
using Facebook.CoreKit;
using Facebook.LoginKit;

namespace GiveAndTake.iOS
{
	[Register("AppDelegate")]
	public class AppDelegate : MvxApplicationDelegate<MvxIosSetup<App>, App>
	{

	}
}