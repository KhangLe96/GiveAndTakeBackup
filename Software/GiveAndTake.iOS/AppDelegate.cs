using Foundation;
using GiveAndTake.Core;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Platforms.Ios.Core;

namespace GiveAndTake.iOS
{
	[Register("AppDelegate")]
	public class AppDelegate : MvxApplicationDelegate<MvxIosSetup<App>, App>
	{
	}
}