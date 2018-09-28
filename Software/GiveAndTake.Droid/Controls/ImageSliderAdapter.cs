using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using GiveAndTake.Core.Models;
using Java.Net;

namespace GiveAndTake.Droid.Controls
{
	public class ImageSliderAdapter : PagerAdapter
	{
		Context _context;
		List<Image> _imageData;

		public ImageSliderAdapter(Context context, List<Image> imageData)
		{
			_imageData = imageData;
			_context = context;
		}

		public override bool IsViewFromObject(View view, Java.Lang.Object @object)
		{
			return view == ((RelativeLayout)@object);
		}


		public override int Count => _imageData.Count == 0 ? 1 : _imageData.Count;

		public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object objectValue)
		{
			container.RemoveView((RelativeLayout)objectValue);
		}

		public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
		{
			var inflater = _context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
			var view = inflater?.Inflate(Resource.Layout.FragmentImage, null);
			var child = view?.FindViewById<ImageView>(Resource.Id.imgDisplay);
			if (child != null)
			{
				child.Click += (o, e) =>
				{
					//handle touch to view the image
				};
				if (_imageData.Count == 0)
				{
					Bitmap image = BitmapFactory.DecodeResource(_context.Resources, Resource.Drawable.default_post);
					child.SetImageBitmap(image);
				}
				else
				{
					Bitmap image = null;
					Task.Run(() =>
					{
						URL url = new URL(_imageData[position].ResizedImage);
						image = BitmapFactory.DecodeStream(url.OpenConnection().InputStream);
					}).ContinueWith(t =>
					{
						(_context as Activity)?.RunOnUiThread(() => { child.SetImageBitmap(image); });
					});
				}
			}

			container.AddView(view);
			return view;
		}
	}
}