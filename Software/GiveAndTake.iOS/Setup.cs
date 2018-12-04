using GiveAndTake.Core;
using GiveAndTake.Core.Helpers;
using GiveAndTake.iOS.Helpers;
using MvvmCross;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.ViewModels;

namespace GiveAndTake.iOS
{
	public class Setup : MvxIosSetup<App>
	{
		protected override IMvxApplication CreateApp()
		{
			return new App();
		}
		protected override void InitializeFirstChance()
		{
			base.InitializeFirstChance();
			Mvx.RegisterType<IDeviceInfo, DeviceInfo>();
		}
	}
}