using Android.Content;
using Android.Content.PM;
using Android.Net;
using GiveAndTake.Core.Helpers.Interface;

namespace GiveAndTake.Droid.Helpers
{
	public class UrlHelper : IUrlHelper
	{
		public void OpenUrl(string url)
		{
			var browserIntent = new Intent(Intent.ActionView);
			browserIntent.AddFlags(ActivityFlags.NewTask);
			browserIntent.AddFlags(ActivityFlags.ClearWhenTaskReset);
			browserIntent.SetData(Uri.Parse(url));
			Android.App.Application.Context.StartActivity(browserIntent);
		}
	}
}
