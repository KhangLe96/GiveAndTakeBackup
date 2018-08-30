using GiveAndTake.Core.ViewModels.Base;
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

			View.Add(_lbProjectName);

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbProjectName, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_lbProjectName, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterY, 1, 0)
			});
		}

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