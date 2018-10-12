using System;
using System.Collections.Generic;
using System.Diagnostics;
using Android.Provider;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Droid.Controls;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views
{
	[MvxFragmentPresentation(typeof(MasterViewModel), Resource.Id.content_frame, true)]
	[Register(nameof(PostDetailView))]
	public class PostDetailView : BaseFragment
	{
		private List<Image> _postImages;
		public List<Image> PostImages
		{
			get => _postImages;
			set
			{
				_postImages = value;
				UpdateImageView();
			}
		}

		private ViewPager _imageViewer;
		private int _imageIndex;
		private ImageButton _navigateLeftButton;
		private ImageButton _navigateRightButton;
		private TextView _displayCurrentImageTextView;

		private View _view;

		protected override int LayoutId => Resource.Layout.PostDetailView;

		protected override void InitView(View view)
		{
			_view = view;

			InitNavigateLeft();
			InitNavigateRight();

			_navigateLeftButton = _view.FindViewById<ImageButton>(Resource.Id.navigateLeftButton);
			_navigateRightButton = _view.FindViewById<ImageButton>(Resource.Id.navigateRightButton);
			_displayCurrentImageTextView = _view.FindViewById<TextView>(Resource.Id.CurrentImage);

			var viewPager = _view.FindViewById<ViewPager>(Resource.Id.SliderViewPager);
			viewPager.SetClipToPadding(false);

			_imageViewer = view.FindViewById<ViewPager>(Resource.Id.SliderViewPager);
			PostImages = new List<Image>();
			_imageIndex = _imageViewer.CurrentItem;
			_imageViewer.PageSelected += _imageViewer_PageSelected;
		}

		private void ImageViewerOnPageSelected()
		{
			_imageIndex = _imageViewer.CurrentItem;
			var displayCurrentImage = (_imageIndex + 1).ToString();		
			if (PostImages.Count == 0 || PostImages.Count == 1)
			{
				_displayCurrentImageTextView.Text = displayCurrentImage + "/ 1 ";
				_navigateLeftButton.Enabled = false;
				_navigateRightButton.Enabled = false;
				_navigateLeftButton.Visibility = ViewStates.Invisible;
				_navigateRightButton.Visibility = ViewStates.Invisible;
			}
			else
			{
				var displayTotalImages = PostImages.Count.ToString();
				_displayCurrentImageTextView.Text = displayCurrentImage + "/" + displayTotalImages;
				if (_imageIndex == 0)
				{
					_navigateLeftButton.Enabled = false;
					_navigateRightButton.Enabled = true;
					_navigateLeftButton.Visibility = ViewStates.Invisible;
					_navigateRightButton.Visibility = ViewStates.Visible;
				}
				else if (_imageIndex == PostImages.Count - 1)
				{
					_navigateLeftButton.Enabled = true;
					_navigateRightButton.Enabled = false;
					_navigateLeftButton.Visibility = ViewStates.Visible;
					_navigateRightButton.Visibility = ViewStates.Invisible;
				}
				else
				{
					_navigateLeftButton.Enabled = true;
					_navigateRightButton.Enabled = true;
					_navigateLeftButton.Visibility = ViewStates.Visible;
					_navigateRightButton.Visibility = ViewStates.Visible;
				}
			}
		}

		private void _imageViewer_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
		{
			ImageViewerOnPageSelected();
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();

			var bindingSet = this.CreateBindingSet<PostDetailView, PostDetailViewModel>();
			
			bindingSet.Bind(this)
				.For(v => v.PostImages)
				.To(vm => vm.PostImages);

			bindingSet.Apply();
		}

		private void InitNavigateLeft()
		{
			var navigateLeftButton = _view.FindViewById<ImageButton>(Resource.Id.navigateLeftButton);
			navigateLeftButton.Click += delegate
			{
				_imageViewer.SetCurrentItem(_imageIndex - 1, true);
			};
		}

		private void InitNavigateRight()
		{
			var navigateRightButton = _view.FindViewById<ImageButton>(Resource.Id.navigateRightButton);
			navigateRightButton.Click += delegate
			{
				_imageViewer.SetCurrentItem(_imageIndex + 1, true);
			};
		}

		private void UpdateImageView()
		{
			_imageViewer.Adapter = new ImageSliderAdapter(this.Context, PostImages);
			ImageViewerOnPageSelected();
		}
	}
}