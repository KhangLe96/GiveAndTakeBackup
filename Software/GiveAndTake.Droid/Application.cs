using System;
using Android.App;
using Android.Runtime;
using GiveAndTake.Core;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace GiveAndTake.Droid
{
	[Application]
	public class Application : MvxAppCompatApplication<Setup, App>
	{
		public Application(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}
	}
}