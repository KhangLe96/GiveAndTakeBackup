using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Droid.Views.TabNavigation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views.Base
{
	[MvxActivityPresentation]
	[Activity(Label = "View for HomeViewModel", WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.Portrait)]
	public class MasterView : BaseActivity
	{
		protected override int LayoutId => Resource.Layout.MasterView;
		public IMvxAsyncCommand ShowInitialViewModelsCommand { get; set; }

		public IMvxCommand BackPressedFromCreatePostCommand { get; set; }
		public IMvxCommand BackPressedFromHomeViewSearchedCommand { get; set; }
		public IMvxCommand BackPressedFromPostDetailCommand { get; set; }
		public bool IsHomeScreen = true;
		protected override void InitView()
		{		
		}

		protected override void CreateBinding()
		{
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
			if (BackPressedFromCreatePostCommand != null)
			{
				BackPressedFromCreatePostCommand.Execute();				
			} else if (BackPressedFromHomeViewSearchedCommand != null)
			{
				BackPressedFromHomeViewSearchedCommand.Execute();
				BackPressedFromHomeViewSearchedCommand = null;
			}
			else if (BackPressedFromPostDetailCommand != null)
			{
				BackPressedFromPostDetailCommand.Execute();
				BackPressedFromPostDetailCommand = null;
			}
			else
			{
				if (IsHomeScreen)
				{
					MoveTaskToBack(true);
				}
				else
				{
					base.OnBackPressed();
				}				
		}
		}


	}
}