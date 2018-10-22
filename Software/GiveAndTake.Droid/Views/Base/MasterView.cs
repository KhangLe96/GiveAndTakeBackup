using System;
using Android.App;
using Android.OS;
using Android.Views;
using GiveAndTake.Core.ViewModels.Base;

using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views.Base
{
	[MvxActivityPresentation]
	[Activity(Label = "View for HomeViewModel", WindowSoftInputMode = SoftInput.AdjustPan)]
	public class MasterView : BaseActivity
	{
		protected override int LayoutId => Resource.Layout.MasterView;
		public IMvxAsyncCommand ShowInitialViewModelsCommand { get; set; }

		public IMvxCommand BackPressedCommand { get; set; }

		
		protected override void InitView()
		{		
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();
			base.CreateBinding();
			var bindingSet = this.CreateBindingSet<MasterView, MasterViewModel>();
			bindingSet.Bind(this)
				.For(v => v.ShowInitialViewModelsCommand)
				.To(vm => vm.ShowInitialViewModelsCommand);
			bindingSet.Apply();
		}

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			if (bundle == null)
			{
				ShowInitialViewModelsCommand.Execute();
			}
		}
		public override void OnBackPressed()
		{
			if (BackPressedCommand == null)
			{
				base.OnBackPressed();
			}
			else
			{
				BackPressedCommand.Execute();
			}
		}


	}
}