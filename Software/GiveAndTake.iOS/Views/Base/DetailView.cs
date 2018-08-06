using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Binding.BindingContext;

namespace GiveAndTake.iOS.Views.Base
{
	public abstract class DetailView : BaseView

	{
		protected override void CreateBinding()
		{
			base.CreateBinding();
			var bindingSet = this.CreateBindingSet<DetailView, DetailViewModel>();

			//Insert binding here

			bindingSet.Apply();
		}
	}
}