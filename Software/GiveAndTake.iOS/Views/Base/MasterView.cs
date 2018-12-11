using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Ios.Presenters.Attributes;

namespace GiveAndTake.iOS.Views.Base
{
	[MvxRootPresentation]
	public class MasterView : BaseView
	{
		public IMvxAsyncCommand ShowInitialViewModelsCommand { get; set; }
		protected override void InitView() { }
		protected override void CreateBinding()
		{
			base.CreateBinding();
			var set = this.CreateBindingSet<MasterView, MasterViewModel>();

			set.Bind(this)
				.For(v => v.ShowInitialViewModelsCommand)
				.To(vm => vm.ShowInitialViewModelsCommand);

			set.Apply();
		}
		
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			ShowInitialViewModelsCommand.Execute();
		}
	}
}