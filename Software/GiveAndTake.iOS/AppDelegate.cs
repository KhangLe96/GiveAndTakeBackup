using Foundation;
using GiveAndTake.Core;
using MvvmCross.Platforms.Ios.Core;

namespace GiveAndTake.iOS
{
	[Register("AppDelegate")]
	public class AppDelegate : MvxApplicationDelegate<MvxIosSetup<App>, App>
	{
	}
}