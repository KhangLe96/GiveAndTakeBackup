using GiveAndTake.Core;
using GiveAndTake.Core.Helpers.Interface;
using GiveAndTake.iOS.Helpers;
using MvvmCross;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.ViewModels;

namespace GiveAndTake.iOS
{
	public class Setup : MvxIosSetup<App>
	{
		protected override void InitializeFirstChance()
		{
			//Register interface
			//exp: Mvx.LazyConstructAndRegisterSingleton<IZipHelper, ZipHelper>();
			base.InitializeFirstChance();
			Mvx.LazyConstructAndRegisterSingleton<IUrlHelper, UrlHelper>();
		}

		protected override IMvxApplication CreateApp()
		{
			return new App();
		}
	}
}