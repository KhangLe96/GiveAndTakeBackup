﻿using System.Collections.Generic;
using System.IO;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views
{
	//Review ThanhVo Why is type of is MasterViewModel?
	[MvxFragmentPresentation(typeof(MasterViewModel), Resource.Id.content_frame, true)]
	[Register(nameof(CreatePostView))]
	public class CreatePostView : BaseFragment
	{
		protected override int LayoutId => Resource.Layout.CreatePostView;
		public IMvxCommand<List<byte[]>> ImageCommand { get; set; }
		public IMvxCommand SubmitCommand { get; set; }
		private View _view;

		private ImageButton _choosePictureButton;

		protected override void InitView(View view)
		{
			_view = view;
			InitChoosePicture();
			InitSubmit();

			var tvImageSelected = _view.FindViewById<TextView>(Resource.Id.tvSelectedImage);
			tvImageSelected.TextChanged += OnTextViewImageSelectedTextChanged;
		}

		private void OnTextViewImageSelectedTextChanged(object sender, TextChangedEventArgs e)
		{
			var textView = sender as TextView;
			if (!string.IsNullOrEmpty(textView?.Text))
			{
				textView.PaintFlags = PaintFlags.UnderlineText;
			}
			else
			{
				if (textView != null) textView.PaintFlags = PaintFlags.LinearText;
			}
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();

			var bindingSet = this.CreateBindingSet<CreatePostView, CreatePostViewModel>();

			bindingSet.Bind(this)
				.For(v => v.ImageCommand)
				.To(vm => vm.ImageCommand);

			bindingSet.Bind(this)
				.For(v => v.SubmitCommand)
				.To(vm => vm.SubmitCommand);

			bindingSet.Apply();
		}

		private void InitSubmit()
		{
			Button submitButton = _view.FindViewById<Button>(Resource.Id.Submit);
			submitButton.Click += delegate
			{
				SubmitCommand.Execute(null);
			};
		}

		private void InitChoosePicture()
		{
			_choosePictureButton = _view.FindViewById<ImageButton>(Resource.Id.ChoosePicture);
			_choosePictureButton.Click += ChoosePictureButton_Click;
		}

		public override void OnDestroyView()
		{
			base.OnDestroyView();
			_choosePictureButton.Click -= ChoosePictureButton_Click;
		}

		private void ChoosePictureButton_Click(object sender, System.EventArgs e)
		{
			Intent intent = new Intent();
			intent.SetType("image/*");
			intent.PutExtra(Intent.ExtraAllowMultiple, true);
			intent.SetAction(Intent.ActionGetContent);
			StartActivityForResult(Intent.CreateChooser(intent, "Select Picture"), 9001);
		}

		public override void OnActivityResult(int requestCode, int resultCode, Intent data)
		{
			List<byte[]> image = new List<byte[]>();
			base.OnActivityResult(requestCode, resultCode, data);
			if (requestCode == 9001)
			{
				if (data?.ClipData != null)
				{
					var imageCount = data.ClipData.ItemCount;
					for (int i = 0; i < imageCount; i++)
					{
						var selectedImage = data.ClipData.GetItemAt(i).Uri;
						var imageInByte = ConvertUriToByte(selectedImage);
						image.Add(imageInByte);
					}

					ImageCommand.Execute(image);
				}
				else
				{
					if (data != null)
					{
						var selectedImage = Android.Net.Uri.Parse(data.DataString);
						var imageInByte = ConvertUriToByte(selectedImage);
						image.Add(imageInByte);
						ImageCommand.Execute(image);
					}
				}
			}
		}

		public byte[] ConvertUriToByte(Android.Net.Uri uri)
		{
			Stream stream = Activity.ContentResolver.OpenInputStream(uri);
			byte[] byteArray;

			using (var memoryStream = new MemoryStream())
			{
				stream.CopyTo(memoryStream);
				byteArray = memoryStream.ToArray();
			}
			return byteArray;
		}
	}
}