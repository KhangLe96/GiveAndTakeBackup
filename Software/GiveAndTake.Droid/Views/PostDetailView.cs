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
		private List<Image> _imagesPost;
		public List<Image> ImagesPost
		{
			get => _imagesPost;
			set
			{
				_imagesPost = value;
				UpdateImageView();
			}
		}

		private ViewPager _imageViewer;
		//REVIEW : rename _currentImage to something like ImageIndex. CurrentImage seems an image name, not int
		private int _currentImage;
		private ImageButton _navigateLeftButton;
		private ImageButton _navigateRightButton;
		//REVIEW : Rename. Why a TextView named ...Image ?
		private TextView _displayCurrentImage;

		private View _view;

		protected override int LayoutId => Resource.Layout.PostDetailView;

		protected override void InitView(View view)
		{
			_view = view;

			InitNavigateLeft();
			InitNavigateRight();

			_navigateLeftButton = _view.FindViewById<ImageButton>(Resource.Id.navigateLeftButton);
			_navigateRightButton = _view.FindViewById<ImageButton>(Resource.Id.navigateRightButton);
			_displayCurrentImage = _view.FindViewById<TextView>(Resource.Id.CurrentImage);

			var viewPager = _view.FindViewById<ViewPager>(Resource.Id.SliderViewPager);
			viewPager.SetClipToPadding(false);

			_imageViewer = view.FindViewById<ViewPager>(Resource.Id.SliderViewPager);
			ImagesPost = new List<Image>();
			_currentImage = _imageViewer.CurrentItem;
			_imageViewer.PageSelected += _imageViewer_PageSelected;
		}

		private void InitNavigationButton()
		{
			_currentImage = _imageViewer.CurrentItem;
			var displayCurrentImage = (_currentImage + 1).ToString();		
			if (ImagesPost.Count == 0 || ImagesPost.Count == 1)
			{
				_displayCurrentImage.Text = displayCurrentImage + "/ 1 ";
				_navigateLeftButton.Enabled = false;
				_navigateRightButton.Enabled = false;
				_navigateLeftButton.Visibility = ViewStates.Invisible;
				_navigateRightButton.Visibility = ViewStates.Invisible;
			}
			else
			{
				var displayTotalImages = ImagesPost.Count.ToString();
				_displayCurrentImage.Text = displayCurrentImage + "/" + displayTotalImages;
				if (_currentImage == 0)
				{
					_navigateLeftButton.Enabled = false;
					_navigateRightButton.Enabled = true;
					_navigateLeftButton.Visibility = ViewStates.Invisible;
					_navigateRightButton.Visibility = ViewStates.Visible;
				}
				else if (_currentImage == ImagesPost.Count - 1)
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
			//REVIEW : Rename the method to ImageViewerOnPageSelected
			InitNavigationButton();
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();

			var bindingSet = this.CreateBindingSet<PostDetailView, PostDetailViewModel>();
			//REVIEW : Try to name variables in View and VM at same.
			bindingSet.Bind(this)
				.For(v => v.ImagesPost)
				.To(vm => vm.PostImage);

			bindingSet.Apply();
		}

		private void InitNavigateLeft()
		{
			var navigateLeftButton = _view.FindViewById<ImageButton>(Resource.Id.navigateLeftButton);
			navigateLeftButton.Click += delegate
			{
				_imageViewer.SetCurrentItem(_currentImage - 1, true);
			};
		}

		private void InitNavigateRight()
		{
			var navigateRightButton = _view.FindViewById<ImageButton>(Resource.Id.navigateRightButton);
			navigateRightButton.Click += delegate
			{
				_imageViewer.SetCurrentItem(_currentImage + 1, true);
			};
		}

		private void UpdateImageView()
		{
			_imageViewer.Adapter = new ImageSliderAdapter(this.Context, ImagesPost);
			InitNavigationButton();
		}
	}
}