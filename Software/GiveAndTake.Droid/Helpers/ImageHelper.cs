using Android.Graphics;
using System.IO;

namespace GiveAndTake.Droid.Helpers
{
	public static class ImageHelper
	{
		public static byte[] ResizeImage(byte[] imageData, float width, float height)
		{
			// Load the bitmap 
			var options = new BitmapFactory.Options { InPurgeable = true }; 
			var originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length, options);

			float newHeight;
			float newWidth;

			var originalHeight = originalImage.Height;
			var originalWidth = originalImage.Width;

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

			var resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int) newWidth, (int) newHeight, true);

			originalImage.Recycle();

			using (var ms = new MemoryStream())
			{
				resizedImage.Compress(Bitmap.CompressFormat.Png, 100, ms);

				resizedImage.Recycle();

				return ms.ToArray();
			}

		}
	}
}