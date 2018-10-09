using Android.App;
using Android.Support.V4.View;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.Droid.Controls;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System.Collections.Generic;

namespace GiveAndTake.Droid.Views
{
	[MvxActivityPresentation]
	[Activity(Label = "PostImageView", Theme = "@style/MainTheme.Base.Fullscreen")]
	public class PostImageView : BaseActivity
	{
		#region Properties

		public IMvxCommand<int> UpdateImageIndexCommand { get; set; }
		protected override int LayoutId => Resource.Layout.PostImageView;

		public List<Image> PostImages
		{
			get => _postImages;
			set
			{
				_postImages = value;
				_imageViewer.Adapter = new ImageSliderAdapter(this, PostImages);
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

		private List<Image> _postImages;
		private int _postImageIndex;
		private ViewPager _imageViewer;

		#endregion

		protected override void InitView()
		{
			_imageViewer = FindViewById<ViewPager>(Resource.Id.SliderViewPager);

			_imageViewer.SetClipToPadding(false);

			_imageViewer.PageSelected += (sender, args) => UpdateImageIndexCommand?.Execute(_imageViewer.CurrentItem);
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();

			var bindingSet = this.CreateBindingSet<PostImageView, PostImageViewModel>();

			bindingSet.Bind(this)
				.For(v => v.PostImages)
				.To(vm => vm.PostImages);

			bindingSet.Bind(this)
				.For(v => v.PostImageIndex)
				.To(vm => vm.PostImageIndex);

			bindingSet.Bind(this)
				.For(v => v.UpdateImageIndexCommand)
				.To(vm => vm.UpdateImageIndexCommand);

			bindingSet.Apply();
		}
	}
}