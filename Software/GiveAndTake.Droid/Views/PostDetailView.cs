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
using Android.OS;
using GiveAndTake.Droid.Helpers;

namespace GiveAndTake.Droid.Views
{
	[MvxFragmentPresentation(typeof(MasterViewModel), Resource.Id.content_frame, true)]
	[Register(nameof(PostDetailView))]
	public class PostDetailView : BaseFragment
	{

		#region Properties

		public IMvxCommand<int> ShowFullImageCommand { get; set; }
		public IMvxCommand<int> UpdateImageIndexCommand { get; set; }
		public IMvxCommand BackPressedCommand { get; set; }
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
				_tvStatus.SetTextColor(_status == AppConstants.GivingStatus ? ColorHelper.Green : Color.DarkRed);
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
		public bool IsLoadInHomeView
		{
			get => _isLoadInHomeView;
			set
			{
				_isLoadInHomeView = value;
				if (IsLoadInHomeView)
				{
					((MasterView)Activity).BackPressedFromPostDetailCommand = BackPressedCommand;
				}
				else
				{
					((MasterView)Activity).BackPressedFromPostDetailCommand = null;
				}
			}
		}

		private ImageButton _requestButton;
		private ViewPager _imageViewer;
		private TextView _tvStatus;
		private string _status;
		private List<Image> _postImages;
		private int _postImageIndex;
		private bool _isRequested;
		private bool _isLoadInHomeView;
		#endregion

		protected override void InitView(View view)
		{
			_tvStatus = view.FindViewById<TextView>(Resource.Id.tvStatus);
			_imageViewer = view.FindViewById<ViewPager>(Resource.Id.SliderViewPager);
			_requestButton = view.FindViewById<ImageButton>(Resource.Id.requestImageButton);

			_imageViewer.SetClipToPadding(false);
			_imageViewer.PageSelected += OnPageSelected;
			
		}


		private void InitSetRequestIcon()
		{
			_requestButton.SetBackgroundResource(IsRequested
				? Resource.Drawable.request_on
				: Resource.Drawable.request_off);
		}

		public override void OnDestroyView()
		{
			base.OnDestroyView();
			_imageViewer.PageSelected -= OnPageSelected;
		}

		private void OnPageSelected(object sender, System.EventArgs e)
		{
			UpdateImageIndexCommand?.Execute(_imageViewer.CurrentItem);
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

			bindingSet.Bind(this)
				.For(v => v.IsLoadInHomeView)
				.To(vm => vm.IsLoadInHomeView);

			bindingSet.Bind(this)
				.For(v => v.BackPressedCommand)
				.To(vm => vm.BackPressedCommand);

			bindingSet.Apply();
		}
	}
}