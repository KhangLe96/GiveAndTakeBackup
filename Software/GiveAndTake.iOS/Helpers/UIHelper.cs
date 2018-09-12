using GiveAndTake.Core;
using System;
using CoreFoundation;
using FFImageLoading.Cross;
using GiveAndTake.iOS.Controls;
using GiveAndTake.iOS.CustomControls;
using System;
using UIKit;

namespace GiveAndTake.iOS.Helpers
{
	public static class UIHelper
	{
		public static PopupItemLabel CreateLabel(UIColor textColor, nfloat fontSize, FontType fontType = FontType.Regular)
		{
			var label = new PopupItemLabel
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				LineBreakMode = UILineBreakMode.WordWrap,
				TextColor = textColor,
				Font = GetFont(fontType, fontSize),
				Lines = 0
			};

			return label;
		}

		public static HeaderBar CreateHeaderBar(nfloat width, nfloat height, UIColor backgroundColor)
		{
			var headerBar = new HeaderBar
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				BackgroundColor = backgroundColor,
			};
			AddWidthHeight(height, width, headerBar);
			return headerBar;
		}

		public static UIImageView CreateImageView(nfloat width, nfloat height, UIColor backgroundColor)
		{
			var imageView = new UIImageView
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				BackgroundColor = backgroundColor
			};
			AddWidthHeight(height, width, imageView);
			return imageView;
		}

		public static UIImageView CreateImageView(nfloat width, nfloat height, UIColor backgroundColor, string imagePath)
		{
			var imageView = CreateImageView(width, height, backgroundColor);
			imageView.Image = new UIImage(imagePath);
			return imageView;
		}

		public static UIView CreateView(nfloat height, nfloat width)
		{
			var view = new UIView { TranslatesAutoresizingMaskIntoConstraints = false };

			AddWidthHeight(height, width, view);

			return view;
		}

		public static UIView CreateView(nfloat height, nfloat width, UIColor backgroundColor)
		{
			var view = CreateView(height, width);

			view.BackgroundColor = backgroundColor;

			return view;
		}

		public static PlaceholderTextView CreateEditTextView(nfloat height, nfloat width, UIColor backgroundColor, UIColor borderColor, 
			nfloat cornerRadius, string placeHolder, UIColor placeHolderColor, nfloat textSize, FontType fontType = FontType.Regular)
		{
			var editText = new PlaceholderTextView
			{
				PlaceholderTextColor = placeHolderColor,
				Placeholder = placeHolder,
				Font = GetFont(fontType, textSize),
				TranslatesAutoresizingMaskIntoConstraints = false,
			};
			editText.BackgroundColor = backgroundColor;
			editText.Layer.BorderColor = borderColor.CGColor;
			editText.Layer.BorderWidth = 1;
			editText.Layer.CornerRadius = cornerRadius;
			editText.Layer.MasksToBounds = true;
			editText.TextContainerInset = new UIEdgeInsets(10, 10, 10, 10);
			editText.TextAlignment = UITextAlignment.Left;
			AddWidthHeight(height, width, editText);

			return editText;
		}

		public static UITextField CreateEditTextField(nfloat height, nfloat width, 
			UIColor backgroundColor, UIColor borderColor, 
			nfloat cornerRadius)
		{
			var editText = new UITextField
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
			};
			editText.Layer.BorderColor = borderColor.CGColor;
			editText.Layer.BorderWidth = 1;
			editText.Font = GetFont(FontType.Regular, DimensionHelper.ButtonTextSize);
			editText.BackgroundColor = backgroundColor;
			editText.Layer.CornerRadius = cornerRadius;
			editText.Layer.MasksToBounds = true;
			editText.TextAlignment = UITextAlignment.Left;

			AddWidthHeight(height, width, editText);

			return editText;
		}

		public static UIView CreateView(nfloat height, nfloat width, UIColor backgroundColor, nfloat cornerRadius)
		{
			var view = CreateView(height, width, backgroundColor);

			view.Layer.CornerRadius = cornerRadius;
			view.ClipsToBounds = true;

			return view;
		}

		public static UIImageView CreateImageView(nfloat height, nfloat width, string imagePath, nfloat cornerRadius = default(nfloat))
		{
			var imageView = new UIImageView
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Image = new UIImage(imagePath)
			};
			imageView.Layer.CornerRadius = cornerRadius;
			imageView.ClipsToBounds = true;

			AddWidthHeight(height, width, imageView);

			return imageView;
		}

		public static CustomMvxCachedImageView CreateCustomImageView(nfloat height, nfloat width, string imagePath, nfloat cornerRadius = default(nfloat))
		{
			var imageView = new CustomMvxCachedImageView
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				DefaultImage = new UIImage(imagePath)
			};
			imageView.Layer.CornerRadius = cornerRadius;
			imageView.ClipsToBounds = true;

			AddWidthHeight(height, width, imageView);

			return imageView;
		}

		public static UISearchBar CreateSearchBar(nfloat height, nfloat width)
		{
			var searchBar = new UISearchBar
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				BackgroundImage = new UIImage()
			};

			searchBar.Layer.CornerRadius = DimensionHelper.FilterSize / 2;
			searchBar.Layer.MasksToBounds = true;
			searchBar.Layer.BorderColor = ColorHelper.BlueColor.CGColor;
			searchBar.Layer.BorderWidth = DimensionHelper.SeperatorHeight;

			AddWidthHeight(height, width, searchBar);

			return searchBar;
		}

		public static AlphaUiButton CreateAlphaButton(nfloat width, nfloat height,
			UIColor textColor, UIColor textTouchColor, nfloat fontSize,
			UIColor backgroundColor, UIColor touchBackgroundColor, UIColor borderColor, UIColor touchBorderColor,
			bool isRoundCorner = true, bool isBorderEnabled = false,
			FontType fontType = FontType.Regular)
		{
			var button = new AlphaUiButton(backgroundColor, touchBackgroundColor, borderColor, touchBorderColor)
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				ExclusiveTouch = true
			};

			if (width > 0)
			{
				button.AddConstraint(NSLayoutConstraint.Create(button, NSLayoutAttribute.Width,
					NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, width));
			}

			if (height > 0)
			{
				button.AddConstraint(NSLayoutConstraint.Create(button, NSLayoutAttribute.Height,
					NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, height));
			}

			if (isRoundCorner)
			{
				button.Layer.CornerRadius = height / 2;
			}

			if (isBorderEnabled)
			{
				button.Layer.BorderWidth = 1;
				button.Layer.BorderColor = UIColor.Gray.CGColor;
			}

			button.BackgroundColor = backgroundColor;
			button.Layer.BorderColor = borderColor.CGColor;
			button.SetTitleColor(textColor, UIControlState.Normal);
			button.SetTitleColor(textTouchColor, UIControlState.Highlighted);
			button.Font = GetFont(fontType, fontSize);
			return button;
		}

		public static UIButton CreateImageButton(nfloat height, nfloat width, string imagePath)
		{
			var button = new UIButton
			{
				TranslatesAutoresizingMaskIntoConstraints = false
			};

			AddWidthHeight(height, width, button);

			button.SetBackgroundImage(new UIImage(imagePath), UIControlState.Normal);
			button.SetBackgroundImage(new UIImage(imagePath), UIControlState.Highlighted);

			return button;
		}

		public static UIButton CreateImageButton(nfloat height, nfloat width, string defaultImagePath, string selectedImagePath)
		{
			var button = CreateImageButton(height, width, defaultImagePath);

			button.SetBackgroundImage(new UIImage(selectedImagePath), UIControlState.Selected);

			return button;
		}

		public static UITableView CreateTableView(nfloat width, nfloat height)
		{
			var tableView = new UITableView
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				ScrollEnabled = true,
				SeparatorColor = UIColor.Clear
			};

			AddWidthHeight(height, width, tableView);

			return tableView;
		}

		public static UIButton CreateButton(nfloat height, nfloat width, UIColor backgroundColor, UIColor textColor, nfloat textSize, string title, nfloat cornerRadius)
		{
			var button = new UIButton
			{
				TranslatesAutoresizingMaskIntoConstraints = false
			};
			button.SetTitleColor(textColor, UIControlState.Normal);
			button.Font = GetFont(FontType.Regular, textSize);
			button.SetTitle(title, UIControlState.Normal);

			button.BackgroundColor = backgroundColor;
			button.Layer.CornerRadius = cornerRadius;
			button.Layer.MasksToBounds = true;

			AddWidthHeight(height, width, button);

			return button;
		}

		public static UIButton CreateButton(nfloat height, nfloat width, UIColor backgroundColor, UIColor textColor, nfloat textSize, string title, nfloat cornerRadius, UIColor borderColor, nfloat borderWidth)
		{
			var button = CreateButton(height, width, backgroundColor, textColor, textSize, title, cornerRadius);

			button.Layer.BorderWidth = borderWidth;
			button.Layer.BorderColor = borderColor.CGColor;

			return button;
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

		private static void AddWidthHeight(nfloat height, nfloat width, UIView view)
		{
			if (width != 0)
			{
				view.AddConstraints(new[]
				{
					NSLayoutConstraint.Create(view, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null,
						NSLayoutAttribute.NoAttribute, 0, width)
				});
			}

			if (height != 0)
			{
				view.AddConstraints(new[]
				{
					NSLayoutConstraint.Create(view, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null,
						NSLayoutAttribute.NoAttribute, 0, height)
				});
			}
		}

		public static UIView CreatePopupLine(nfloat height, nfloat width, UIColor color, nfloat cornerRadius)
		{
			var line = CreateView(height, width, color);
			line.Layer.CornerRadius = cornerRadius;
			line.Layer.MasksToBounds = true;
			return line;
		}
	}
}