using System.Collections.Generic;
using System.Reflection;
using GiveAndTake.Core;
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

		//public override MvxLogProviderType GetDefaultLogProviderType()
		//	=> MvxLogProviderType.Serilog;

		//protected override IMvxLogProvider CreateLogProvider()
		//{
		//	Log.Logger = new LoggerConfiguration()
		//		.MinimumLevel.Debug()
		//		.WriteTo.AndroidLog()
		//		.CreateLogger();
		//	return base.CreateLogProvider();
		//}

		//protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
		//{
		//	registry.RegisterCustomBindingFactory<BinaryEdit>(
		//		"MyCount",
		//		(arg) => new BinaryEditTargetBinding(arg));

		//	base.FillTargetFactories(registry);
		//}
	}
}