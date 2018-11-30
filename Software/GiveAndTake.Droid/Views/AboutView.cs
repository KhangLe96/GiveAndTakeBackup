using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Droid.Views.TabNavigation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views.Base
{
	[MvxActivityPresentation]
	[Activity(Label = "View for AboutView", WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.Portrait)]
	public class AboutView : BaseActivity
	{
		protected override int LayoutId => Resource.Layout.AboutView;
		protected override void InitView()
		{
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();
			var bindingSet = this.CreateBindingSet<AboutView, AboutViewModel>();

			//bindingSet.Bind(this)
			//	.For(v => v.ShowInitialViewModelsCommand)
			//	.To(vm => vm.ShowInitialViewModelsCommand);

			bindingSet.Apply();
		}

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

		}

	}
}