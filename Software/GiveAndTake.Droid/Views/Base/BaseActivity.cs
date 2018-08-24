using Android.App;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace GiveAndTake.Droid.Views.Base
{
    [Activity]
    public abstract class BaseActivity : MvxAppCompatActivity
    {
        protected abstract int LayoutId { get; }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            if (ViewModel != null)
            {
                SetContentView(LayoutId);
                InitView();
                CreateBinding();
            }
        }

        protected abstract void InitView();

        protected virtual void CreateBinding()
        {
        }
    }
}