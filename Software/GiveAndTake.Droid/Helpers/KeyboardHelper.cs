using Android.App;
using Android.Content;
using Android.Views;
using Android.Views.InputMethods;

namespace GiveAndTake.Droid.Helpers
{
	public static class KeyboardHelper
	{
		public static void HideKeyboard(View view)
		{
			if (view == null)
			{
				return;
			}

			var inputMethodManager = (InputMethodManager)Android.App.Application.Context.GetSystemService(Context.InputMethodService);
			inputMethodManager.HideSoftInputFromWindow(view.WindowToken, 0);
		}

		public static void ShowKeyboard(View view)
		{
			if (view == null)
			{
				return;
			}

			var inputMethodManager = (InputMethodManager)Android.App.Application.Context.GetSystemService(Context.InputMethodService);
			inputMethodManager.ShowSoftInput(view, ShowFlags.Implicit);
		}

		public static bool IsPhoneLocked()
		{
			var keyboardManager = (KeyguardManager)Android.App.Application.Context.GetSystemService(Context.KeyguardService);

			return keyboardManager.InKeyguardRestrictedInputMode();
		}
	}
}