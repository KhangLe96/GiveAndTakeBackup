using Android.Graphics;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using GiveAndTake.Core;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Droid.Controls;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System.Collections.Generic;

namespace GiveAndTake.Droid.Views
{
	[MvxFragmentPresentation(typeof(MasterViewModel), Resource.Id.content_frame, true)]
	[Register(nameof(PostDetailView))]
	public class PostDetailView : BaseFragment
	{

		#region Properties

		public IMvxCommand<int> ShowFullImageCommand { get; set; }
		public IMvxCommand<int> UpdateImageIndexCommand { get; set; }
		protected override int LayoutId => Resource.Layout.PostDetailView;

		public List<Image> PostImages
		{
			get => _postImages;
			set
			{
				_postImages = value;
				_imageViewer.Adapter = new ImageSliderAdapter(Context, PostImages)
				{
					HandleItemSelected = () => ShowFullImageCommand?.Execute(_postImageIndex)
				};
			}
		}

		public string Status
		{
			get => _status;
			set
			{
				_status = value;
				// REVIEW[KHOA]: hard-coded color
				_tvStatus.SetTextColor(_status == AppConstants.GivingStatus ? Color.ParseColor("#2CB273") : Color.DarkRed);
			}
		}

		public bool IsRequested
		{
			get => _isRequested;
			set
			{
				_isRequested = value;
				InitSetRequestIcon();
			}
		}

		public int PostImageIndex
		{
			get => _postImageIndex;
			set
			{
				_postImageIndex = value;
				_imageViewer.SetCurrentItem(value, true);
			}
		}

		private ImageButton _requestButton;
		private ViewPager _imageViewer;
		private TextView _tvStatus;
		private string _status;
		private List<Image> _postImages;
		private int _postImageIndex;
		private bool _isRequested;

		#endregion

		protected override void InitView(View view)
		{
			_tvStatus = view.FindViewById<TextView>(Resource.Id.tvStatus);
			_imageViewer = view.FindViewById<ViewPager>(Resource.Id.SliderViewPager);
			_requestButton = view.FindViewById<ImageButton>(Resource.Id.requestImageButton);

			_imageViewer.SetClipToPadding(false);

			// REVIEW[KHOA]: unsubscribe event when view is destroyed
			_imageViewer.PageSelected += (sender, args) => UpdateImageIndexCommand?.Execute(_imageViewer.CurrentItem);

		}

		private void InitSetRequestIcon()
		{
			_requestButton.SetBackgroundResource(IsRequested
				? Resource.Drawable.request_on
				: Resource.Drawable.request_off);
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();

			var bindingSet = this.CreateBindingSet<PostDetailView, PostDetailViewModel>();

			bindingSet.Bind(this)
				.For(v => v.PostImages)
				.To(vm => vm.PostImages);

			bindingSet.Bind(this)
				.For(v => v.Status)
				.To(vm => vm.Status);

			bindingSet.Bind(this)
				.For(v => v.PostImageIndex)
				.To(vm => vm.PostImageIndex);

			bindingSet.Bind(this)
				.For(v => v.ShowFullImageCommand)
				.To(vm => vm.ShowFullImageCommand);

			bindingSet.Bind(this)
				.For(v => v.UpdateImageIndexCommand)
				.To(vm => vm.UpdateImageIndexCommand);

			bindingSet.Bind(this)
				.For(v => v.IsRequested)
				.To(vm => vm.IsRequested);

			bindingSet.Apply();
		}
	}
}