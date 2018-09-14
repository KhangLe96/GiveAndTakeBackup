using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GiveAndTake.Core.Helpers
{
	/// <summary>
	/// Event debouncer helps to prevent calling the same event handler too often (like mark Dirty or Invalidate)
	/// </summary>
	public class DebouncerHelper
	{
		private List<CancellationTokenSource> _stepperCancelTokens = new List<CancellationTokenSource>();
		private readonly int _millisecondsToWait;

		public DebouncerHelper(int millisecondsToWait = 300)
		{
			_millisecondsToWait = millisecondsToWait;
		}

		public void Debouce(Action func)
		{
			CancelAllStepperTokens();
			var newTokenSrc = new CancellationTokenSource();
			_stepperCancelTokens.Add(newTokenSrc);
			Task.Delay(_millisecondsToWait, newTokenSrc.Token).ContinueWith(task =>
			{
				if (!newTokenSrc.IsCancellationRequested)
				{
					func();
					CancelAllStepperTokens();
					_stepperCancelTokens = new List<CancellationTokenSource>();
				}
			}, newTokenSrc.Token);
		}

		private void CancelAllStepperTokens()
		{
			foreach (var token in _stepperCancelTokens)
			{
				if (!token.IsCancellationRequested)
				{
					token.Cancel();
				}
			}
		}
	}
}
