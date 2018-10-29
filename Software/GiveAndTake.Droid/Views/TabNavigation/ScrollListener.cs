using System;
using Android.Support.V7.Widget;
using FFImageLoading;

namespace GiveAndTake.Droid.Views.TabNavigation
{
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