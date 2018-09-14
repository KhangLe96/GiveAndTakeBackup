using System.Windows.Input;
using Foundation;
using GiveAndTake.iOS.Helpers;
using UIKit;

namespace GiveAndTake.iOS.Controls
{
	public class CustomTextView : UIView
	{
		private bool _isError;
		public UILabel LbPlaceHolder;

		private UITextView _textView;

		public UITextView TextView
		{
			get => _textView;
			set => _textView = value;
		}

		public CustomTextView()
		{
			TranslatesAutoresizingMaskIntoConstraints = false;
			BackgroundColor = UIColor.Clear;
			Layer.BorderColor = ColorHelper.Gray.CGColor;
			Layer.BorderWidth = DimensionHelper.BorderWidth;
			Layer.CornerRadius = DimensionHelper.RoundCorner;

			InitElements();
			InitView();
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			if (LbPlaceHolder != null)
			{
				LbPlaceHolder.Hidden = _textView.Text?.Length > 0;
			}
		}

		public bool IsEditing => TextView.Focused;

		public ICommand ClickCommand { get; set; }

		public string PlaceHolder
		{
			get => LbPlaceHolder.Text;
			set
			{
				LbPlaceHolder.Hidden = TextView.Text.Length > 0;
				LbPlaceHolder.Text = value;
			}
		}

		public bool IsError
		{
			get => _isError;
			set
			{
				_isError = value;
				if (value)
				{
					LbPlaceHolder.TextColor = UIColor.Red;
					TextView.ResignFirstResponder();
				}
				else
				{
					LbPlaceHolder.TextColor = ColorHelper.Gray;
				}
			}
		}

		private void InitView()
		{
			Add(TextView);
			Add(LbPlaceHolder);
			AddConstraints(new[]
			{
				NSLayoutConstraint.Create(LbPlaceHolder, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.Top, 1, DimensionHelper.TextPadding),
				NSLayoutConstraint.Create(LbPlaceHolder, NSLayoutAttribute.Left, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.Left, 1, DimensionHelper.TextPadding),
				NSLayoutConstraint.Create(TextView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.Left, 1, 0),
				NSLayoutConstraint.Create(TextView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.Right, 1, 0),
				NSLayoutConstraint.Create(TextView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(TextView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.Bottom, 1, 0)
			});
		}

		private void InitElements()
		{
			TextView = new UITextView
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Editable = true,
				AutocorrectionType = UITextAutocorrectionType.No,
				//Font = GetFont(FontType.Regular, DimensionHelper.MediumTextSize),
				ShowsHorizontalScrollIndicator = false,
				TextContainerInset = new UIEdgeInsets(DimensionHelper.TextPadding, DimensionHelper.TextPadding, 0, DimensionHelper.TextPadding)
			};
			TextView.TextContainer.LineFragmentPadding = 0;
			TextView.ShouldChangeText += OnTextViewShouldChangeText;
			TextView.ShouldBeginEditing += OnTextViewShouldBeginEditing;

			LbPlaceHolder = UIHelper.CreateLabel(ColorHelper.Gray, DimensionHelper.MediumTextSize);
			LbPlaceHolder.BackgroundColor = UIColor.Clear;
			LbPlaceHolder.UserInteractionEnabled = false;
		}

		private bool OnTextViewShouldBeginEditing(UITextView textview)
		{
			ClickCommand?.Execute(null);
			return true;
		}

		private bool OnTextViewShouldChangeText(UITextView textview, NSRange range, string text)
		{
			var adder = 1;
			if (text.Length == 0)
			{
				adder = -1;
			}
			LbPlaceHolder.Hidden = TextView.Text.Length + adder > 0;
			return true;
		}
	}
}
