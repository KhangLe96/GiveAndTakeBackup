using System.Collections.Generic;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.TabNavigation;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views
{
	[MvxFragmentPresentation(typeof(MasterViewModel), Resource.Id.content_frame, true)]
    public class TabNavigation : MvxFragment<TabNavigationViewModel>
    {
        private TabLayout _tabLayout;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle bundle)
        {
            View view = inflater.Inflate(Resource.Layout.TabNavigation, null);
            return view;
        }

        public override void OnViewCreated(View view, Bundle bundle)
        {
            Dictionary<string, int> tabNavigationIcons = DroidConstants.TabNavigationIcons;
            if (bundle == null)
            {
                ViewModel.ShowInitialViewModelsCommand.Execute();
            }
            _tabLayout = view.FindViewById<TabLayout>(Resource.Id.tabLayout);
            for (int index = 0; index < _tabLayout.TabCount; index++)
            {
                var tab = _tabLayout.GetTabAt(index);
                tab.SetIcon(tabNavigationIcons[tab.Text]);
                tab.SetText("");
            }
        }
    }
}

