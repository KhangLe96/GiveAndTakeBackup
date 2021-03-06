﻿using GiveAndTake.Core.ViewModels;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace GiveAndTake.Core
{
	public class App : MvxApplication
	{
		public override void Initialize()
		{
			base.Initialize();

			RegisterAppStart<MasterViewModel>();
		}

		protected void RegisterHelpers()
		{
			Mvx.LazyConstructAndRegisterSingleton<IDataModel, DataModel>();
			var dataModel = new DataModel();
			Mvx.RegisterSingleton(dataModel);
		}

		protected void RegisterServices()
		{
			Mvx.RegisterType<IManagementService, ManagementService>();
		}
	}
}