using Android.Runtime;
using Android.Views;
using Android.Widget;
using GiveAndTake.Core.ViewModels.TabNavigation;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views.TabNavigation
{
	[MvxTabLayoutPresentation(TabLayoutResourceId = Resource.Id.tabLayout,
        Title = "Home",
        ViewPagerResourceId = Resource.Id.viewPager,
        FragmentHostViewType = typeof(TabNavigationView))]
    [Register(nameof(HomeView))]
    public class HomeView : BaseFragment
    {
	    public IMvxCommand SearchCommand { get; set; }
	    protected override int LayoutId => Resource.Layout.HomeView;
	    private SearchView searchView;

	    protected override void InitView(View view)
	    {
		    base.InitView(view);
		    searchView = view.FindViewById<SearchView>(Resource.Id.searchView);
		    searchView.QueryTextSubmit += OnQueryTextSubmit;
	    }

	    private void OnQueryTextSubmit(object sender, SearchView.QueryTextSubmitEventArgs e)
	    {
		    SearchCommand.Execute();
			searchView.OnActionViewCollapsed();
	    }

	    protected override void CreateBinding()
	    {
		    base.CreateBinding();
		    var bindingSet = this.CreateBindingSet<HomeView, HomeViewModel>();

		    bindingSet.Bind(this)
			    .For(v => v.SearchCommand)
			    .To(vm => vm.SearchCommand);

			bindingSet.Apply();
	    }
    }
}