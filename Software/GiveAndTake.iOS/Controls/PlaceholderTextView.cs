using CoreGraphics;
using System;
using UIKit;

namespace GiveAndTake.iOS.Controls
{
	public class PlaceholderTextView : UITextView
	{
		public UIColor PlaceholderTextColor { get; set; }

		public UIColor ActualTextColor { get; set; }

		private string _placeholder;

		public string Placeholder
		{
			get { return _placeholder; }
			set
			{
				_placeholder = value;

				if (string.IsNullOrEmpty(base.Text))
				{
					base.Text = value;
					TextColor = PlaceholderTextColor;
				}
			}
		}


		private bool _isNoteTextFocused;

		public bool IsNoteTextFocused
		{
			get { return _isNoteTextFocused; }
			set
			{
				_isNoteTextFocused = value;
				TextColor = value ? ActualTextColor : PlaceholderTextColor;

				if (value)
				{
					BecomeFirstResponder();
				}
			}
		}

		public override string Text
		{
			get { return string.Equals(base.Text, _placeholder) ? null : base.Text; }
			set
			{
				base.Text = string.IsNullOrEmpty(value) ? _placeholder : value;
				TextColor = string.IsNullOrEmpty(value) ? PlaceholderTextColor : ActualTextColor;
			}
		}

		public PlaceholderTextView()
		{
			Initialize();
		}

		public PlaceholderTextView(CGRect frame)
			: base(frame)
		{
			Initialize();
		}

		public PlaceholderTextView(IntPtr handle)
			: base(handle)
		{
			Initialize();
		}

		private void Initialize()
		{
			ShouldBeginEditing = t =>
			{
				if (string.IsNullOrEmpty(Text))
					base.Text = string.Empty;

				TextColor = ActualTextColor;
				return true;
			};

			ShouldEndEditing = t =>
			{
				TextColor = string.IsNullOrWhiteSpace(Text) ? PlaceholderTextColor : ActualTextColor;

				if (string.IsNullOrWhiteSpace(Text))
				{
					base.Text = _placeholder;
				}

				return true;
			};
		}

		public void ResetState()
		{
			Text = Placeholder;
			TextColor = PlaceholderTextColor;
		}
	}
}