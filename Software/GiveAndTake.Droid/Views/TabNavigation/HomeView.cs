﻿using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using FFImageLoading;
using GiveAndTake.Core.ViewModels.TabNavigation;
using GiveAndTake.Droid.Helpers;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System;
using GiveAndTake.Core;
using SearchView = Android.Widget.SearchView;
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

	public class ScrollListener : RecyclerView.OnScrollListener
	{
		public Action LoadMoreEvent { get; set; }

		private readonly LinearLayoutManager _layoutManager;

		private bool _isLoading;

		public ScrollListener(LinearLayoutManager layoutManager)
		{
			_layoutManager = layoutManager;
			_isLoading = false;
		}

		public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
		{
			base.OnScrolled(recyclerView, dx, dy);

			var visibleItemCount = recyclerView.ChildCount;
			var totalItemCount = recyclerView.GetAdapter().ItemCount;
			var pastVisibleItems = _layoutManager.FindFirstVisibleItemPosition();

			if (!_isLoading && visibleItemCount + pastVisibleItems >= totalItemCount - 5)
			{
				_isLoading = true;
				LoadMoreEvent?.BeginInvoke(result => _isLoading = false, null);
			}
		}

		public override void OnScrollStateChanged(RecyclerView recyclerView, int newState)
		{
			base.OnScrollStateChanged(recyclerView, newState);

			switch (newState)
			{
				case RecyclerView.ScrollStateDragging:
					ImageService.Instance.SetPauseWork(true);
					break;

				case RecyclerView.ScrollStateIdle:
					ImageService.Instance.SetPauseWork(false);
					break;
			}
		}
	}
}