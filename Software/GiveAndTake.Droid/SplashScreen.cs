using System;
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
	[Activity( MainLauncher = true, 
		NoHistory = true, 
		ScreenOrientation = ScreenOrientation.Portrait,
		Theme = "@style/SplashTheme")]
	public class SplashScreen : MvxSplashScreenAppCompatActivity<Setup, App>
	{
		private Core.Models.Notification _clickedNotification;
		private int _badgeValue;

		public SplashScreen()
			: base(Resource.Layout.SplashScreen)
		{
		}
		protected override void OnResume()
		{
			base.OnResume();

			var notificationData = Intent?.Extras?.GetString("NotificationModelData");
			_badgeValue = (int) Intent?.Extras?.GetInt("BadgeData");
			bool isDataModelInitialized = Mvx.CanResolve<IDataModel>();

			if (notificationData != null)
			{
				
				var notification = JsonHelper.Deserialize<Core.Models.Notification>(notificationData);
				if (isDataModelInitialized)
				{
					var dataModel = Mvx.Resolve<IDataModel>();
					dataModel.Badge = _badgeValue;
					if (dataModel.IsLoggedIn)
					{
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
				if (isDataModelInitialized && Mvx.Resolve<IDataModel>().IsLoggedIn)
				{
					Finish();
				}
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (Mvx.CanResolve<IDataModel>())
			{				
				if (_clickedNotification != null)
				{
					var dataModel = Mvx.Resolve<IDataModel>();
					dataModel.SelectedNotification = _clickedNotification;
					dataModel.Badge = _badgeValue;
					dataModel.RaiseNotificationReceived(_clickedNotification);
					dataModel.RaiseBadgeUpdated(_badgeValue);
				}
			}
		}
	}
}