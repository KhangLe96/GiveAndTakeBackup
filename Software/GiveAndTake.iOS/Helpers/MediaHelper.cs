using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ELCImagePicker;
using Foundation;
using GiveAndTake.Core;
using GiveAndTake.Core.Helpers;
using UIKit;

namespace GiveAndTake.iOS.Helpers
{
	public static class MediaHelper
	{
		public static List<byte[]> images;
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
						images = new List<byte[]>();

						var items = t.Result as List<AssetResult>;
						foreach (var item in items)
						{
							var path = Save(item.Image, item.Name);
							byte[] bytes = System.IO.File.ReadAllBytes(path);
							images.Add(bytes);
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
			string jpgFilename = System.IO.Path.Combine(documentsDirectory, name); // hardcoded filename, overwritten each time
			NSData imgData = image.AsJPEG();
			NSError err = null;
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