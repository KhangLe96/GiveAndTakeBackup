using GiveAndTake.Core;
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
			Mvx.LazyConstructAndRegisterSingleton<ISystemHelper, SystemHelper>();
		}

		protected override IMvxApplication CreateApp()
		{
			return new App();
		}
	}
}