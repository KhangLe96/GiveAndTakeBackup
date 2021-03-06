﻿using Android.App;
using Android.OS;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;

namespace GiveAndTake.Droid.Views
{
	[MvxActivityPresentation]
	[Activity(Label = "View for MasterViewModel")]
	public class MasterView : MvxActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.MasterView);
		}
	}
}