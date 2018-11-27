using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using ELCImagePicker;
using Foundation;
using UIKit;

namespace GiveAndTake.iOS.Helpers
{
	public static class MediaHelper
	{
		public static List<byte[]> Images;
		public static async Task OpenGallery()
		{
			var picker = ELCImagePickerViewController.Create(15);
			picker.MaximumImagesCount = 15;

			var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;
			while (topController.PresentedViewController != null)
			{
				topController = topController.PresentedViewController;
			}
			topController.PresentViewController(picker, true, null);

			await picker.Completion.ContinueWith(t =>
			{
				picker.BeginInvokeOnMainThread(() =>
				{
					//dismiss the picker
					picker.DismissViewController(true, null);

					if (t.IsCanceled || t.Exception != null)
					{
					}
					else
					{
						Images = new List<byte[]>();

						var items = t.Result;
						foreach (var item in items)
						{
							var path = Save(item.Image, item.Name);
							var bytes = File.ReadAllBytes(path);
							Images.Add(ResizeImage(bytes, 600, 600));
							//CleanPath(path);
						}
					}
				});
			});
		}

		static string Save(UIImage image, string name)
		{
			var documentsDirectory = Environment.GetFolderPath
				(Environment.SpecialFolder.Personal);
			string jpgFilename = Path.Combine(documentsDirectory, name); // hardcoded filename, overwritten each time
			NSData imgData = image.AsJPEG();
			NSError err;
			if (imgData.Save(jpgFilename, false, out err))
			{
				return jpgFilename;
			}
			else
			{
				Console.WriteLine("NOT saved as " + jpgFilename + " because" + err.LocalizedDescription);
				return null;
			}
		}

		static void ClearFiles(List<string> filePaths)
		{
			var documentsDirectory = Environment.GetFolderPath
				(Environment.SpecialFolder.Personal);

			if (Directory.Exists(documentsDirectory))
			{
				foreach (var p in filePaths)
				{
					File.Delete(p);
				}
			}
		}

		public static byte[] ResizeImage(byte[] imageData, float width, float height)
		{

			var originalImage = new UIImage(Foundation.NSData.FromArray(imageData));

			var originalHeight = originalImage.Size.Height;
			var originalWidth = originalImage.Size.Width;

			nfloat newHeight;
			nfloat newWidth;

			if (originalHeight > originalWidth)
			{
				newHeight = height;
				var ratio = originalHeight / height;
				newWidth = originalWidth / ratio;
			}
			else
			{
				newWidth = width;
				var ratio = originalWidth / width;
				newHeight = originalHeight / ratio;
			}

			width = (float)newWidth;
			height = (float)newHeight;

			UIGraphics.BeginImageContext(new SizeF(width, height));
			originalImage.Draw(new RectangleF(0, 0, width, height));
			var resizedImage = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();

			var bytesImage = resizedImage.AsJPEG().ToArray();
			resizedImage.Dispose();
			return bytesImage;
		}
	}
}