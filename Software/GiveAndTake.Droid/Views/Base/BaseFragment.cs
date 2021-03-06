﻿using System;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using GiveAndTake.Droid.Helpers;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Views.Fragments;

namespace GiveAndTake.Droid.Views.Base
{
	public abstract class BaseFragment : MvxFragment
	{
		public event EventHandler BackPressed;

		protected abstract int LayoutId { get; }

		private LayoutInflater _inflater;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView(inflater, container, savedInstanceState);

			_inflater = inflater;

			var view = this.BindingInflate(LayoutId, null);
			view.SetBackgroundColor(Color.White);

			var layoutParameters = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);

			view.LayoutParameters = layoutParameters;
			view.FocusChange += ViewOnFocusChange;

			InitView(view);
			CreateBinding();

			return view;
		}

		protected virtual void InitView(View view)
		{
		}

		protected virtual void CreateBinding()
		{
		}

		public override void OnViewModelSet()
		{
			base.OnViewModelSet();

			if (ViewModel != null)
			{
				//Register Popup 
			}
		}

		private void ViewOnFocusChange(object sender, View.FocusChangeEventArgs focusChangeEventArgs)
		{
			if (focusChangeEventArgs.HasFocus)
			{
				KeyboardHelper.HideKeyboard(sender as View);
			}
		}
	}
}