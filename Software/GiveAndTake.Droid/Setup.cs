using System.Collections.Generic;
using System.Reflection;
using GiveAndTake.Core;
using GiveAndTake.Core.Helpers;
using GiveAndTake.Droid.Helpers;
using MvvmCross;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;

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
			Mvx.LazyConstructAndRegisterSingleton<ISystemHelper, SystemHelper>();
		}
	}
}