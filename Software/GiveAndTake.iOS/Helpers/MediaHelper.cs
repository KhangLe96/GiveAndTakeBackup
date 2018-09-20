using System;
using System.Collections.Generic;
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
							Images.Add(bytes);
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
	}
}