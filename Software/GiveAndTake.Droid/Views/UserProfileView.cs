using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Droid.Views.Base;
using GiveAndTake.Droid.Views.TabNavigation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System;

namespace GiveAndTake.Droid.Views
{
	[MvxFragmentPresentation(typeof(MasterViewModel), Resource.Id.content_frame, true)]
    [Register(nameof(UserProfileView))]
    public class UserProfileView : BaseFragment
    {
	    public IMvxCommand LoadMorePostsCommand { get; set; }

        protected override int LayoutId => Resource.Layout.UserProfileView;

	    protected override void InitView(View view)
	    {
		    base.InitView(view);

		    AddScrollEvent(view.FindViewById<MvxRecyclerView>(Resource.Id.rvPosts), () => LoadMorePostsCommand?.Execute());
	    }

	    protected override void CreateBinding()
	    {
		    base.CreateBinding();
		    var bindingSet = this.CreateBindingSet<UserProfileView, UserProfileViewModel>();

		    bindingSet.Bind(this)
			    .For(v => v.LoadMorePostsCommand)
			    .To(vm => vm.LoadMorePostsCommand);

		    bindingSet.Apply();
	    }

	    private void AddScrollEvent(MvxRecyclerView recyclerView, Action action)
	    {
		    var layoutManager = new LinearLayoutManager(Context);
		    recyclerView.AddOnScrollListener(new ScrollListener(layoutManager)
		    {
			    LoadMoreEvent = action
		    });
		    recyclerView.SetLayoutManager(layoutManager);
	    }
	}
}