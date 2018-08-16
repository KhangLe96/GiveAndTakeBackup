using GiveAndTake.Core;
using System;
using UIKit;

namespace GiveAndTake.iOS.Helpers
{
	public static class UIHelper
	{
		public static UILabel CreateLabel(UIColor textColor, nfloat fontSize, FontType fontType = FontType.Regular)
		{
			var label = new UILabel
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				LineBreakMode = UILineBreakMode.WordWrap,
				TextColor = textColor,
				Font = GetFont(fontType, fontSize)
			};

			return label;
		}

		public static UIView CreateView(nfloat height, nfloat width, UIColor backgroundColor)
		{
			var view = new UIView
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				BackgroundColor = backgroundColor
			};

			view.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(view, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 0 , height),
				NSLayoutConstraint.Create(view, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 0 , width)
			});

			return view;
		}

		public static UIFont GetFont(FontType fontType, nfloat fontSize)
		{
			switch (fontType)
			{
				case FontType.Thin:
					return UIFont.FromName("HelveticaNeue-Thin", fontSize);
				case FontType.Light:
					return UIFont.FromName("HelveticaNeue-Light", fontSize);
				case FontType.LightItalic:
					return UIFont.FromName("HelveticaNeue-LightItalic", fontSize);
				case FontType.Medium:
					return UIFont.FromName("HelveticaNeue-Medium", fontSize);
				case FontType.Bold:
					return UIFont.FromName("HelveticaNeue-Bold", fontSize);
				case FontType.Italic:
					return UIFont.FromName("HelveticaNeue-Italic", fontSize);
				default:
					return UIFont.FromName("HelveticaNeue", fontSize);
			}
		}
	}
}