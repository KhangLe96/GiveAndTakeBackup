using System.Collections.Generic;
using System.Reflection;
using GiveAndTake.Core;
using GiveAndTake.Core.Helpers;
using GiveAndTake.Core.Helpers.Interface;
using MvvmCross;
using GiveAndTake.Droid.Helpers;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Core;

namespace GiveAndTake.Droid
{
	public class Setup : MvxAppCompatSetup<App>
	{
		protected override IEnumerable<Assembly> AndroidViewAssemblies =>
			new List<Assembly>(base.AndroidViewAssemblies)
			{
				typeof(MvxRecyclerView).Assembly
			};
		protected override void InitializeFirstChance()
		{
			base.InitializeFirstChance();
			Mvx.LazyConstructAndRegisterSingleton<IUrlHelper, UrlHelper>();
			Mvx.LazyConstructAndRegisterSingleton<ISystemHelper, SystemHelper>();
			Mvx.RegisterType<IDeviceInfo,DeviceInfo>();
		}
	}
}