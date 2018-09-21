﻿using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using GiveAndTake.Core.ViewModels.TabNavigation;
using GiveAndTake.Droid.Helpers;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System;
using SearchView = Android.Widget.SearchView;

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
	    public IMvxCommand LoadMoreCommand { get; set; }
	    protected override int LayoutId => Resource.Layout.HomeView;
	    private SearchView _searchView;

	    protected override void InitView(View view)
	    {
		    base.InitView(view);

		    _searchView = view.FindViewById<SearchView>(Resource.Id.searchView);
		    _searchView.QueryTextSubmit += OnQueryTextSubmit;
		    _searchView.Close += OnClose;
		    _searchView.QueryTextChange += OnQueryTextChanged;
			_searchView.Click += (sender, args) => _searchView.Iconified = false;
			//_searchView.ClearFocus();

			var rvPosts = view.FindViewById<MvxRecyclerView>(Resource.Id.rvPosts);
			var layoutManager = new LinearLayoutManager(view.Context);
		    rvPosts.AddOnScrollListener(new ScrollListener(layoutManager)
		    {
			    LoadMoreEvent = () => LoadMoreCommand?.Execute()
		    });
			rvPosts.SetLayoutManager(layoutManager);
	    }

	    private void OnQueryTextChanged(object sender, SearchView.QueryTextChangeEventArgs e)
	    {
		    if (string.IsNullOrEmpty(e.NewText))
		    {
			    OnClose(this,null);
		    }
	    }

	    private void OnClose(object sender, SearchView.CloseEventArgs e)
	    {
			SearchCommand.Execute();
		    KeyboardHelper.HideKeyboard(sender as View);
			_searchView.ClearFocus();
		}

	    private void OnQueryTextSubmit(object sender, SearchView.QueryTextSubmitEventArgs e)
	    {
		    SearchCommand.Execute();
			KeyboardHelper.HideKeyboard(sender as View);
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

	public class ScrollListener : RecyclerView.OnScrollListener
	{
		public Action LoadMoreEvent { get; set; }

		private readonly LinearLayoutManager _layoutManager;

		public ScrollListener(LinearLayoutManager layoutManager)
		{
			_layoutManager = layoutManager;
		}

		public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
		{
			base.OnScrolled(recyclerView, dx, dy);

			var visibleItemCount = recyclerView.ChildCount;
			var totalItemCount = recyclerView.GetAdapter().ItemCount;
			var pastVisibleItems = _layoutManager.FindFirstVisibleItemPosition();

			if (visibleItemCount + pastVisibleItems >= totalItemCount)
			{
				LoadMoreEvent?.Invoke();
			}
		}
	}
}