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

			var data = Intent?.Extras?.GetString("giveandtake");
			bool isDataModelInitialized = Mvx.CanResolve<IDataModel>();
			
			if (data != null)
			{
				try
				{
					var notification = JsonHelper.Deserialize<Core.Models.Notification>(data);
					if (isDataModelInitialized)
					{
						var dataModel = Mvx.Resolve<IDataModel>();

						if (dataModel.IsLoggedIn)
						{
							a = true;
							_clickedNotification = notification;
							Finish();
						}

						// TODO: clear extra 'giveandtake'
						Intent.Extras.Clear();
					}
					else
					{
						_clickedNotification = notification;
					}
				}
				catch (Exception e)
				{
					Toast.MakeText(this, e.Message, ToastLength.Long).Show();
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

				if (a)
				{
					dataModel.RaiseNotificationReceived(_clickedNotification);
				}
			}
		}
	}
}