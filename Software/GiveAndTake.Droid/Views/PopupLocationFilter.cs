﻿using Android.OS;
using Android.Runtime;
using Android.Views;
using GiveAndTake.Core.ViewModels;
using MvvmCross.Droid.Support.Design;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GiveAndTake.Droid.Views
{
	[MvxDialogFragmentPresentation]
	[Register(nameof(PopupLocationFilter))]
	public class PopupLocationFilter : MvxBottomSheetDialogFragment<PopupLocationFilterViewModel>
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView(inflater, container, savedInstanceState);

			var view = this.BindingInflate(Resource.Layout.PopupView, null);

			return view;
		}
	}
}