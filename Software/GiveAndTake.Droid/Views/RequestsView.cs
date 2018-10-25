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

namespace GiveAndTake.Droid.Views
{
    [MvxFragmentPresentation(typeof(MasterViewModel), Resource.Id.content_frame, true)]
    [Register(nameof(RequestsView))]
    public class RequestsView : BaseFragment
    {
        protected override int LayoutId => Resource.Layout.RequestsView;

        public IMvxCommand LoadMoreCommand { get; set; }

        protected override void InitView(View view)
        {
            base.InitView(view);

            var rvRequests = view.FindViewById<MvxRecyclerView>(Resource.Id.rvRequests);
            var layoutManager = new LinearLayoutManager(view.Context);
            rvRequests.AddOnScrollListener(new ScrollListener(layoutManager)
            {
                LoadMoreEvent = LoadMoreEvent
            });
            rvRequests.SetLayoutManager(layoutManager);
        }

        private void LoadMoreEvent()
        {
            LoadMoreCommand?.Execute();
        }

        protected override void CreateBinding()
        {
            base.CreateBinding();
            var bindingSet = this.CreateBindingSet<RequestsView, RequestsViewModel>();

            bindingSet.Bind(this)
                .For(v => v.LoadMoreCommand)
                .To(vm => vm.LoadMoreCommand);

            bindingSet.Apply();
        }
    }
}