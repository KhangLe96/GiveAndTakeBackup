using System.Diagnostics;
using System.Reflection;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels;
using I18NPortable;
using MvvmCross;
using MvvmCross.ViewModels;

namespace GiveAndTake.Core
{
	public class App : MvxApplication
	{
		public App()
		{
			var currentAssembly = GetType().GetTypeInfo().Assembly;

			I18N
				.Current
				.SetLogger(text => Debug.WriteLine(text))
				.SetNotFoundSymbol("$")
				.SetFallbackLocale("vi")
				.Init(currentAssembly);
		}

		public override void Initialize()
		{
			base.Initialize();

			RegisterHelpers();

			RegisterServices();

			RegisterAppStart<LoginViewModel>();
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
			Mvx.RegisterType<ILoadingOverlayService, LoadingOverlayService>();
		}
	}
}
