using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using GiveAndTake.Core;
using GiveAndTake.Core.ViewModels.TabNavigation;
using GiveAndTake.Droid.Helpers;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using SearchView = Android.Support.V7.Widget.SearchView;

namespace GiveAndTake.Droid.Views.TabNavigation
{
	[MvxTabLayoutPresentation(TabLayoutResourceId = Resource.Id.tabLayout,
        Title = AppConstants.HomeTab,
        ViewPagerResourceId = Resource.Id.viewPager,
        FragmentHostViewType = typeof(TabNavigationView))]
    [Register(nameof(HomeView))]
    public class HomeView : BaseFragment
    {
	    public IMvxCommand SearchCommand { get; set; }
	    public IMvxCommand LoadMoreCommand { get; set; }
	    protected override int LayoutId => Resource.Layout.HomeView;

	    private SearchView _searchView;
		
	    protected override void InitView(View view)
	    {
		    base.InitView(view);

		    _searchView = view.FindViewById<SearchView>(Resource.Id.searchView);
		    _searchView.QueryTextSubmit += OnQueryTextSubmit;
			_searchView.Click += (sender, args) => _searchView.Iconified = false;
		    ImageView closeButtonSearchView = (ImageView) _searchView.FindViewById(MvvmCross.Droid.Support.V7.AppCompat.Resource.Id.search_close_btn);
		    closeButtonSearchView.Click += delegate
		    {
				_searchView.SetQuery("",false);
				_searchView.ClearFocus();
			    closeButtonSearchView.Visibility = ViewStates.Gone;
		    };
			var rvPosts = view.FindViewById<MvxRecyclerView>(Resource.Id.rvPosts);
			var layoutManager = new LinearLayoutManager(view.Context);
		    rvPosts.AddOnScrollListener(new ScrollListener(layoutManager)
		    {
			    LoadMoreEvent = () => LoadMoreCommand?.Execute()
		    });
			rvPosts.SetLayoutManager(layoutManager);
		}

	    private void OnQueryTextSubmit(object sender, SearchView.QueryTextSubmitEventArgs e)
	    {
			KeyboardHelper.HideKeyboard(sender as View);
		    _searchView.ClearFocus();
		    SearchCommand.Execute();
		}

        protected override void CreateBinding()
        {
            base.CreateBinding();
            var bindingSet = this.CreateBindingSet<HomeView, HomeViewModel>();

            bindingSet.Bind(this)
                .For(v => v.SearchCommand)
                .To(vm => vm.SearchCommand);

            bindingSet.Bind(this)
                .For(v => v.LoadMoreCommand)
                .To(vm => vm.LoadMoreCommand);

            bindingSet.Apply();
        }

        public override void OnPause()
        {
            base.OnPause();
            KeyboardHelper.HideKeyboard(_searchView);
        }
    }
}
