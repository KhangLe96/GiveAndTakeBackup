using GiveAndTake.Core;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.ViewModels;

namespace GiveAndTake.iOS
{
	public class Setup : MvxIosSetup
	{
		protected override void InitializeFirstChance()
		{
			//Register interface
			//exp: Mvx.LazyConstructAndRegisterSingleton<IZipHelper, ZipHelper>();
		}

		protected override IMvxApplication CreateApp()
		{
			return new App();
		}
	}
}