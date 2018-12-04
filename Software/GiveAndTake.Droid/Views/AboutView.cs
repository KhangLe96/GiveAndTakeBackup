using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Views;
using Android.Widget;
using GiveAndTake.Core;

using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views.Base
{
	//Review ThanhVo why use activity?
	[MvxActivityPresentation]
	[Activity(Label = "View for AboutView", WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.Portrait)]
	public class AboutView : BaseActivity
	{
		protected override int LayoutId => Resource.Layout.AboutView;

		ImageButton btn;

		protected override void InitView()
		{
			btn = FindViewById<ImageButton>(Resource.Id.supportContactPhone);

			btn.Click += OnContactButtonClicked;

		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			btn.Click -= OnContactButtonClicked;
		}

		//Review ThanhVo Handle it in the view model
		private void OnContactButtonClicked(object sender, EventArgs e)
		{
			var hasTelephony = PackageManager.HasSystemFeature(PackageManager.FeatureTelephony);
			if (hasTelephony)
			{
				var uri = Android.Net.Uri.Parse("tel:"+ AppConstants.SupportContactPhone);
				var intent = new Intent(Intent.ActionView, uri);
				StartActivity(intent);
			}
		}
	}
}