using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Droid.Helpers;
using GiveAndTake.Droid.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System.Collections.Generic;
using System.IO;


namespace GiveAndTake.Droid.Views
{
	[MvxFragmentPresentation(typeof(MasterViewModel), Resource.Id.content_frame, true)]
	[Register(nameof(CreatePostView))]
	public class CreatePostView : BaseFragment
	{
		protected override int LayoutId => Resource.Layout.CreatePostView;
		public IMvxCommand<List<byte[]>> ImageCommand { get; set; }
		public IMvxCommand BackPressedCommand { get; set; }

		private View _view;

		private const int ChoosePictureCode = 9001;
		private ImageButton _choosePictureButton;
		private Button _submitButton;
		private EditText _title;
		private EditText _postDescriptionEditText;
		private TextView _tvImageSelected;

		protected override void InitView(View view)
		{
			_view = view;
			InitChoosePicture();

			_title = _view.FindViewById<EditText>(Resource.Id.Title);
			_title.FocusChange += OnEditTextFocusChange;

			_postDescriptionEditText = _view.FindViewById<EditText>(Resource.Id.PostDescription);
			_postDescriptionEditText.FocusChange += OnEditTextFocusChange;
			_postDescriptionEditText.SetOnTouchListener(new MyTouchListener()
			{
				TouchViewId = _postDescriptionEditText.Id
			});

			_tvImageSelected = _view.FindViewById<TextView>(Resource.Id.tvSelectedImage);
			_tvImageSelected.TextChanged += OnTextViewImageSelectedTextChanged;

			Activity.Window.SetSoftInputMode(SoftInput.AdjustResize);

		}

		private void OnEditTextFocusChange(object sender, View.FocusChangeEventArgs e)
		{
			var editText = sender as EditText;
			if (!editText.HasFocus)
			{
				KeyboardHelper.HideKeyboard(editText);
			}
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
				.For(v => v.BackPressedCommand)
				.To(vm => vm.BackPressedCommand);

			bindingSet.Apply();
		}

		protected override void HandleActivityCommandFromFragment()
		{
			((MasterView)Activity).BackPressedFromCreatePostCommand = BackPressedCommand;
		}

		public override void OnDestroyView()
		{
			base.OnDestroyView();
			_choosePictureButton.Click -= ChoosePicture;
			_title.FocusChange -= OnEditTextFocusChange;
			_postDescriptionEditText.FocusChange -= OnEditTextFocusChange;
			_tvImageSelected.TextChanged -= OnTextViewImageSelectedTextChanged;
			((MasterView)Activity).BackPressedFromCreatePostCommand = null;
		}


		private void InitChoosePicture()
		{
			_choosePictureButton = _view.FindViewById<ImageButton>(Resource.Id.ChoosePicture);
			_choosePictureButton.Click += ChoosePicture;
		}

		private void ChoosePicture(object sender, System.EventArgs e)
		{
			Intent intent = new Intent();
			intent.SetType("image/*");
			intent.PutExtra(Intent.ExtraAllowMultiple, true);
			intent.SetAction(Intent.ActionGetContent);
			StartActivityForResult(Intent.CreateChooser(intent, "Select Picture"), ChoosePictureCode);
		}

		public override void OnActivityResult(int requestCode, int resultCode, Intent data)
		{
			List<byte[]> image = new List<byte[]>();
			base.OnActivityResult(requestCode, resultCode, data);
			if (requestCode == ChoosePictureCode)
			{
				if (data?.ClipData != null)
				{
					var imageCount = data.ClipData.ItemCount;
					for (int i = 0; i < imageCount; i++)
					{
						var selectedImage = data.ClipData.GetItemAt(i).Uri;
						var imageInByte = ConvertUriToByte(selectedImage);
						image.Add(ImageHelper.ResizeImage(imageInByte, 600, 600));
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

		private byte[] ConvertUriToByte(Android.Net.Uri uri)
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

	public class MyTouchListener : Java.Lang.Object, View.IOnTouchListener
	{
		public int TouchViewId { get; set; }

		public bool OnTouch(View v, MotionEvent e)
		{
			if (v.Id == TouchViewId)
			{
				v.Parent.RequestDisallowInterceptTouchEvent(true);
				switch (e.Action & MotionEventActions.Mask)
				{
					case MotionEventActions.Up:
						v.Parent.RequestDisallowInterceptTouchEvent(false);
						break;
				}
			}

			return false;
		}
	}
}
