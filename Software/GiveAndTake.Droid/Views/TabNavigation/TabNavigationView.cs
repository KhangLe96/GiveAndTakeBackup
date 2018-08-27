using System;
using System.Collections.Generic;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.TabNavigation;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views.TabNavigation
{
	[MvxFragmentPresentation(typeof(MasterViewModel), Resource.Id.content_frame, true)]
	public class TabNavigationView : BaseFragment
	{
		private static readonly Dictionary<string, int> TabTitleIconsDictionary = new Dictionary<string, int>(){
			{"Home",Resource.Drawable.tab_navigation_icon_home},
			{"Notification",Resource.Drawable.tab_navigation_icon_notification},
			{"Conversation",Resource.Drawable.tab_navigation_icon_conversation},
			{"Profile",Resource.Drawable.tab_navigation_icon_profile},
		};
		public IMvxAsyncCommand ShowInitialViewModelsCommand { get; set; }
		protected override int LayoutId => Resource.Layout.TabNavigation;

		protected override void CreateBinding()
		{
			base.CreateBinding();
			var bindingSet = this.CreateBindingSet<TabNavigationView, TabNavigationViewModel>();
			bindingSet.Bind(this)
				.For(v => v.ShowInitialViewModelsCommand)
				.To(vm => vm.ShowInitialViewModelsCommand);
			bindingSet.Apply();
		}

		public override void OnViewCreated(View view, Bundle bundle)
		{
			if (bundle == null)
			{
				ShowInitialViewModelsCommand.Execute();
			}
			TabLayout tabLayout = view.FindViewById<TabLayout>(Resource.Id.tabLayout);
			for (int index = 0; index < tabLayout.TabCount; index++)
			{
				var tab = tabLayout.GetTabAt(index);
				tab.SetIcon(TabTitleIconsDictionary[tab.Text]);
				tab.SetText("");
			}
		}
	}
}

