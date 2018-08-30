using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels;
using MvvmCross;
using MvvmCross.ViewModels;

namespace GiveAndTake.Core
{
	public class App : MvxApplication
	{
		public override void Initialize()
		{
			base.Initialize();

			RegisterHelpers();

			RegisterServices();

			RegisterAppStart<CreatePostViewModel>();
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
