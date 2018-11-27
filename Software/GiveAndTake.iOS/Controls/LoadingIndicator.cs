using System;
using System.Threading;
using System.Threading.Tasks;
using CoreAnimation;
using CoreGraphics;
using GiveAndTake.iOS.Helpers;
using UIKit;

namespace GiveAndTake.iOS.Controls
{
	public class LoadingIndicator : UIView
	{
		private const int Duration = 600;
		private const int Max = 100;

		private CancellationTokenSource _loadingCancel;

		private UIColor _color;
		private nfloat _lineWidth;

		private bool _isCleaning;
		private bool _disposed;

		public nfloat CircleSize { get; set; }
		public LoadingIndicator(nfloat size, UIColor color, nfloat lineWidth = default(nfloat))
		{
			CircleSize = size;
			_color = color;
			_lineWidth = lineWidth == default(nfloat) ? DimensionHelper.LoadingLineWidth : lineWidth;
			InitControl();
		}

		private void InitControl()
		{
			TranslatesAutoresizingMaskIntoConstraints = false;
			BackgroundColor = UIColor.Clear;

			if (CircleSize > 0)
			{
				AddConstraint(NSLayoutConstraint.Create(this, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, CircleSize));
				AddConstraint(NSLayoutConstraint.Create(this, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, CircleSize));
			}
		}

		public async void StartLoadingAnimation()
		{
			_loadingCancel?.Cancel();
			_loadingCancel = new CancellationTokenSource();

			var token = _loadingCancel.Token;

			var percent = 1;

			while (!token.IsCancellationRequested)
			{
				try
				{
					Hidden = false;

					await Task.Delay(Duration / Max, token);

					if (token.IsCancellationRequested)
					{
						break;
					}

					if (percent > Max)
					{
						percent = 1;
					}

					if (token.IsCancellationRequested)
					{
						break;
					}
					Fill(percent, token);
					percent++;
				}
				catch (OperationCanceledException)
				{
				}
			}

			if (!_disposed)
			{
				Hidden = true;
			}
		}

		public void StopLoadingAnimation()
		{
			_loadingCancel?.Cancel();
			_loadingCancel = null;

			Layer.RemoveAllAnimations();
			Layer.Sublayers = null;
		}

		private void Fill(int percent, CancellationToken token)
		{
			token.ThrowIfCancellationRequested();

			double startAngle;
			double endAngle;

			if (_isCleaning)
			{
				startAngle = percent * 2 * Math.PI / Max - 0.5 * Math.PI;
				endAngle = 1.5 * Math.PI;
			}
			else
			{
				startAngle = -0.5 * Math.PI;
				endAngle = (percent * 2 * Math.PI / Max) - 0.5 * Math.PI;
			}

			token.ThrowIfCancellationRequested();

			if (percent == Max)
			{
				_isCleaning = !_isCleaning;
			}

			token.ThrowIfCancellationRequested();

			DrawArc(new nfloat(startAngle), new nfloat(endAngle), token);
		}

		/// <summary>
		/// Draw circle from startAngle to endAngle with clockwise direction
		/// </summary>
		protected void DrawArc(nfloat startAngle, nfloat endAngle, CancellationToken token)
		{

			token.ThrowIfCancellationRequested();

			var path = new UIBezierPath();
			var center = new CGPoint(CircleSize / 2, CircleSize / 2);

			path.AddArc(center, CircleSize / 2 - _lineWidth / 2f, startAngle, endAngle, true);

			if (token.IsCancellationRequested)
			{
				return;
			}

			var layer = new CAShapeLayer
			{
				Path = path.CGPath,
				FillColor = UIColor.Clear.CGColor,
				StrokeColor = _color.CGColor,
				LineCap = CAShapeLayer.CapRound,
				LineWidth = _lineWidth,
				Frame = new CGRect(0, 0, CircleSize, CircleSize)
			};

			token.ThrowIfCancellationRequested();

			if (_isCleaning)
			{
				Layer.Sublayers = null;
			}

			Layer.AddSublayer(layer);
		}

		public void SetLineColor(UIColor color)
		{
			_color = color;
		}

		public void SetLineWidth(float value)
		{
			_lineWidth = value;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_disposed = true;

				StopLoadingAnimation();
			}

			base.Dispose(disposing);
		}
	}
}