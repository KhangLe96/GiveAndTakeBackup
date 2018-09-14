using System;
using System.Windows.Input;
using CoreGraphics;
using GiveAndTake.iOS.Helpers;
using UIKit;

namespace GiveAndTake.iOS.Controls
{
	public class CustomTextField : UITextField
	{
		public UIEdgeInsets EdgeInsets { get; set; }
		public event EventHandler OnFocus;

		public ICommand ReturnCommand { get; set; }

		public override bool Enabled
		{
			get => base.Enabled;
			set
			{
				base.Enabled = value;
				BackgroundColor = value ? _backgroundColor : ColorHelper.LightGray;
			}
		}

		private readonly UIColor _backgroundColor;

		public CustomTextField(UIColor backgroundColor, UIColor borderColor)
		{
			_backgroundColor = backgroundColor;
			BackgroundColor = backgroundColor;
			Layer.BorderColor = borderColor.CGColor;

			EdgeInsets = UIEdgeInsets.Zero;
			ShouldReturn += TextFieldShouldReturn;
			ShouldBeginEditing += BeginEditing;
			SpellCheckingType = UITextSpellCheckingType.No;
			AutocorrectionType = UITextAutocorrectionType.No;
			AutocapitalizationType = UITextAutocapitalizationType.None;
		}

		public override CGRect EditingRect(CGRect forBounds)
		{
			return base.EditingRect(InsetRect(forBounds, EdgeInsets));
		}

		public override CGRect TextRect(CGRect forBounds)
		{
			return base.TextRect(InsetRect(forBounds, EdgeInsets));
		}

		private bool BeginEditing(UITextField textfield)
		{
			OnFocus?.Invoke(textfield, null);
			return true;
		}

		private CGRect InsetRect(CGRect rect, UIEdgeInsets insets)
		{
			return new CGRect(rect.X + insets.Left,
				rect.Y + insets.Top,
				rect.Width - insets.Left - insets.Right,
				rect.Height - insets.Top - insets.Bottom);
		}

		private bool TextFieldShouldReturn(UITextField textfield)
		{
			ReturnCommand?.Execute(null);
			this.EndEditing(true);
			return true;
		}
	}
}
