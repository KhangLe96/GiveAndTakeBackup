﻿using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Widget;
using GiveAndTake.Core;
using GiveAndTake.Core.Helpers;
using GiveAndTake.Core.Models;
using MvvmCross;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Views;

namespace GiveAndTake.Droid
{
	[Activity(
		 MainLauncher = true
		, NoHistory = true
		, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashScreen : MvxSplashScreenAppCompatActivity<Setup, App>
	{
		private Core.Models.Notification _clickedNotification;

		public SplashScreen()
			: base(Resource.Layout.SplashScreen)
		{
		}

		private bool a;
		protected override void OnResume()
		{
			base.OnResume();

			var notificationData = Intent?.Extras?.GetString("GiveAndTakeNotification");
			bool isDataModelInitialized = Mvx.CanResolve<IDataModel>();

			if (notificationData != null)
			{
				var notification = JsonHelper.Deserialize<Core.Models.Notification>(notificationData);
				if (isDataModelInitialized)
				{
					var dataModel = Mvx.Resolve<IDataModel>();

					if (dataModel.IsLoggedIn)
					{
						a = true;
						_clickedNotification = notification;
						Finish();
					}
					Intent.Extras.Clear();
				}
				else
				{
					_clickedNotification = notification;
				}
			}
			else
			{
				if (Mvx.CanResolve<IDataModel>() && Mvx.Resolve<IDataModel>().IsLoggedIn)
				{
					Finish();
				}
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (_clickedNotification != null)
			{
				var dataModel = Mvx.Resolve<IDataModel>();
				dataModel.SelectedNotification = _clickedNotification;
				dataModel.RaiseNotificationReceived(_clickedNotification);
			}
		}
	}
}