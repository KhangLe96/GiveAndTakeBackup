using System;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using GiveAndTake.Core.ViewModels.TabNavigation;
using GiveAndTake.Droid.Helpers;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using GiveAndTake.Core;
using Android.Widget;
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
	    public IMvxCommand CloseSearchBarCommand { get; set; }
		protected override int LayoutId => Resource.Layout.HomeView;

	    private SearchView _searchView;
	    private ImageView _clearButton;

	    protected override void InitView(View view)
	    {
		    base.InitView(view);

		    _searchView = view.FindViewById<SearchView>(Resource.Id.searchView);
		    _searchView.QueryTextSubmit += OnQueryTextSubmit;
		    _searchView.Click += OnSearchViewClicked;

			_clearButton = (ImageView)_searchView.FindViewById(MvvmCross.Droid.Support.V7.AppCompat.Resource.Id.search_close_btn);
			_clearButton.Click += OnClearButtonClicked;

			var rvPosts = view.FindViewById<MvxRecyclerView>(Resource.Id.rvPosts);
			var layoutManager = new LinearLayoutManager(view.Context);
		    rvPosts.AddOnScrollListener(new ScrollListener(layoutManager)
		    {
			    LoadMoreEvent = () => LoadMoreCommand?.Execute()
		    });
			rvPosts.SetLayoutManager(layoutManager);
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

		    bindingSet.Bind(this)
			    .For(v => v.CloseSearchBarCommand)
			    .To(vm => vm.CloseSearchBarCommand);

			bindingSet.Apply();
        }

	    public override void OnPause()
	    {
		    base.OnPause();
			KeyboardHelper.HideKeyboard(_searchView);
	    }

	    protected override void Dispose(bool disposing)
	    {
		    _searchView.QueryTextSubmit -= OnQueryTextSubmit;
		    _searchView.Click -= OnSearchViewClicked;
		    _clearButton.Click -= OnClearButtonClicked;
			base.Dispose(disposing);
		}

	    private void OnClearButtonClicked(object sender, EventArgs e)
	    {
		    _searchView.SetQuery("", false);
		    _searchView.ClearFocus();
		    _clearButton.Visibility = ViewStates.Gone;
		    CloseSearchBarCommand.Execute();
	    }

	    private void OnSearchViewClicked(object sender, EventArgs args)
	    {
		    _searchView.Iconified = false;
	    }

	    private void OnQueryTextSubmit(object sender, SearchView.QueryTextSubmitEventArgs e)
	    {
		    KeyboardHelper.HideKeyboard(sender as View);
		    _searchView.ClearFocus();
		    SearchCommand.Execute();
	    }
    }
}
