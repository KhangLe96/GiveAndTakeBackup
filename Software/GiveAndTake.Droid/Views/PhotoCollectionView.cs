using Android.Support.V7.Widget;
using Android.Views;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Commands;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views
{
	//Review ThanhVo Why is type of is MasterViewModel?
	[MvxFragmentPresentation(typeof(MasterViewModel), Resource.Id.content_frame, true)]
	public class PhotoCollectionView : BaseFragment
	{
		public IMvxAsyncCommand BackPressedCommand { get; set; }

		protected override int LayoutId => Resource.Layout.PhotoCollectionView;
		protected override void InitView(View view)
		{
			base.InitView(view);
			var rvPostCollection = view.FindViewById<MvxRecyclerView>(Resource.Id.rvPhotoCollection);
			rvPostCollection.SetLayoutManager(new GridLayoutManager(Activity, 2));
		}
	}
}