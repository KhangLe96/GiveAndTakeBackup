﻿using System.Collections.Generic;
using System.Reflection;
using GiveAndTake.Core;
using GiveAndTake.Core.Helpers;
using GiveAndTake.Droid.Helpers;
using MvvmCross;
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
			Mvx.RegisterType<IDeviceInfo,DeviceInfo>();
		}
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