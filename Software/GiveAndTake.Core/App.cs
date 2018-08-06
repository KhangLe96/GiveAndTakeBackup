using GiveAndTake.Core.ViewModels;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace GiveAndTake.Core
{
	public class App : MvxApplication
	{
		public override void Initialize()
		{
			CreatableTypes()
				.EndingWith("Service")
				.AsInterfaces()
				.RegisterAsLazySingleton();

			RegisterAppStart<MasterViewModel>();
		}
	}
}