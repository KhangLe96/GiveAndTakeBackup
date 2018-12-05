using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Widget;
using GiveAndTake.Core;
using GiveAndTake.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views.Base
{
	[MvxActivityPresentation]
	[Activity]
	public class AboutView : BaseActivity
	{
		protected override int LayoutId => Resource.Layout.AboutView;

		ImageButton btn;
		protected override void InitView()
		{
			btn = FindViewById<ImageButton>(Resource.Id.supportContactPhone);

			btn.Click += OnContactButtonClicked;
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();

			var bindingSet = this.CreateBindingSet<AboutView, AboutViewModel>();

			bindingSet.Apply();
		}

		protected override void OnViewModelSet()
		{
			base.OnViewModelSet();
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