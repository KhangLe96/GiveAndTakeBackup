using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Views;

namespace GiveAndTake.Droid.Views.Base
{    
	[Activity]

	public abstract class BaseActivity :MvxActivity
	{
		protected abstract int LayoutId { get; }
		
		protected override void OnViewModelSet()
		{

			base.OnViewModelSet();

			if (ViewModel != null)
			{

				SetContentView(LayoutId);
				InitView();
				CreateBinding();
			}
		}

		protected abstract void InitView();

		protected virtual void CreateBinding() { }
	}
}