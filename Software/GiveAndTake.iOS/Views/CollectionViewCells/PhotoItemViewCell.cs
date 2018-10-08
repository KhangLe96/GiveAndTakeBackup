using System;
using CoreGraphics;
using Foundation;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using UIKit;

namespace GiveAndTake.iOS.Views.CollectionViewCells
{
	public class PhotoItemViewCell : MvxCollectionViewCell
	{
		private UIImageView _photoImageView;
		private UIButton _deletePhotoButton;

		private string _imageBase64Data;
		public string ImageBase64Data
		{
			get => _imageBase64Data;
			set
			{
				_imageBase64Data = value;
				//Review Thanh Vo use var https://intellitect.com/when-to-use-and-not-use-var-in-c/
				NSData decodedData = new NSData(_imageBase64Data, NSDataBase64DecodingOptions.None);
				UIImage decodedImage = new UIImage(decodedData);
				_photoImageView.Image = decodedImage;
			}
		}

		//Review Thanh Vo Delete if it is unused
		public IMvxCommand DeleteAPhotoCommand { get; set; }

		public PhotoItemViewCell(IntPtr handle) : base(handle)
		{

			InitView();
			CreateBinding();
		}

		private void InitView()
		{
			ContentView.Layer.BorderColor = UIColor.Clear.CGColor;
			ContentView.Layer.BorderWidth = 1.0f;
			ContentView.Layer.CornerRadius = 10.0f;
			ContentView.Layer.MasksToBounds = true;
			ContentView.BackgroundColor = UIColor.White;
			_photoImageView = new UIImageView(new CGRect(0, 0, ContentView.Frame.Width, ContentView.Frame.Height));
			_deletePhotoButton = UIHelper.CreateImageButton(DimensionHelper.DeletePhotoButtonWidth,
				DimensionHelper.DeletePhotoButtonWidth, ImageHelper.DeletePhotoButton);

			ContentView.AddSubview(_photoImageView);
			ContentView.AddSubview(_deletePhotoButton);
			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_photoImageView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Left, 1, 0),
				NSLayoutConstraint.Create(_photoImageView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_photoImageView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Right, 1, 0),
				NSLayoutConstraint.Create(_photoImageView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Bottom, 1, 0),

				NSLayoutConstraint.Create(_deletePhotoButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Right, 1, 0),
				NSLayoutConstraint.Create(_deletePhotoButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Top, 1, 0),
			});
		}

		private void CreateBinding()
		{
			var bindingSet = this.CreateBindingSet<PhotoItemViewCell, PhotoTemplateViewModel>();

			bindingSet.Bind(this)
				.For(v => v.ImageBase64Data)
				.To(vm => vm.ImageBase64Data);

			bindingSet.Bind(_deletePhotoButton.Tap())
				.For(v => v.Command)
				.To(vm => vm.DeleteAPhotoCommand);

			bindingSet.Apply();
		}
	}
}