using System;
using Android.Runtime;
using Xamarin.Facebook;
using Object = Java.Lang.Object;

namespace GiveAndTake.Droid.Views
{
    public class FacebookCallback<TResult> : Object, IFacebookCallback where TResult : Object
    {
        public Action HandleCancel { get; set; }
        public Action<FacebookException> HandleError { get; set; }
        public Action<TResult> HandleSuccess { get; set; }

        public void OnCancel()
        {
            var c = HandleCancel;
            c?.Invoke();
        }

        public void OnError(FacebookException error)
        {
            var c = HandleError;
            c?.Invoke(error);
        }

        public void OnSuccess(Object result)
        {
            var c = HandleSuccess;
            c?.Invoke(result.JavaCast<TResult>());
        }
    }
}