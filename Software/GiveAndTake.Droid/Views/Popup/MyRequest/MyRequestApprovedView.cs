﻿using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.Droid.Helpers;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views
{
	[MvxDialogFragmentPresentation]
	[Register(nameof(MyRequestApprovedView))]
	public class MyRequestApprovedView : MvxDialogFragment<MyRequestApprovedViewModel>
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView(inflater, container, savedInstanceState);
			var view = this.BindingInflate(Resource.Layout.MyRequestApprovedView, null);
			return view;
		}

		public override void OnStart()
		{
			base.OnStart();
			Dialog.Window.SetLayout(DimensionHelper.ScreenWidth - (int)DimensionHelper.FromDimensionId(Resource.Dimension.margin_normal) * 4, ViewGroup.LayoutParams.WrapContent);
			Dialog.Window.SetBackgroundDrawable(new ColorDrawable(Color.Transparent));
		}
	}
}