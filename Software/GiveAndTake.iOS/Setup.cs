using GiveAndTake.Core;
using GiveAndTake.Core.Helpers.Interface;
using GiveAndTake.iOS.Helpers;
using MvvmCross;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.ViewModels;
using GiveAndTake.Core.Helpers;
using SystemHelper = GiveAndTake.iOS.Helpers.SystemHelper;

namespace GiveAndTake.iOS
{
	public class Setup : MvxIosSetup<App>
	{
		protected override void InitializeFirstChance()
		{
			base.InitializeFirstChance();
			Mvx.LazyConstructAndRegisterSingleton<IUrlHelper, UrlHelper>();
			Mvx.LazyConstructAndRegisterSingleton<ISystemHelper, SystemHelper>();
			Mvx.RegisterType<IDeviceInfo, DeviceInfo>();
		}

		protected override IMvxApplication CreateApp()
		{
			return new App();
		}
	}
}