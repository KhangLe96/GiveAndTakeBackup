using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views.Base
{
	[MvxRootPresentation]
	public class MasterView : BaseView
	{
		private UILabel _lbProjectName;

		protected override void InitView()
		{
			_lbProjectName = UIHelper.CreateLabel(UIColor.Blue, DimensionHelper.MediumTextSize);

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

			set.Bind(_lbProjectName)
				.For(v => v.Text)
				.To(vm => vm.ProjectName);

			set.Apply();
		}
	}
}