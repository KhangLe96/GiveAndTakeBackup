using GiveAndTake.Core;
using GiveAndTake.iOS.Controls;
using GiveAndTake.iOS.CustomControls;
using System;
using Foundation;
using CoreAnimation;
using UIKit;
using Xamarin.iOS.iCarouselBinding;

namespace GiveAndTake.iOS.Helpers
{
	public static class UIHelper
	{
		public static UIImage ConvertViewToImage(UIView view)
		{
			UIGraphics.BeginImageContext(view.Bounds.Size);
			view.Layer.RenderInContext(UIGraphics.GetCurrentContext());
			var image = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			return image;
		}

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

		public static CustomUILabel CreateCustomLabel(UIColor textColor, nfloat fontSize, FontType fontType = FontType.Regular)
		{
			var label = new CustomUILabel
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				LineBreakMode = UILineBreakMode.WordWrap,
				TextColor = textColor,
				Font = GetFont(fontType, fontSize),
				Lines = 0
			};

			return label;
		}

		public static HeaderBar CreateHeaderBar(nfloat width, nfloat height, UIColor backgroundColor, bool isShowBackButton=false)
		{
			var headerBar = new HeaderBar(isShowBackButton)
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

		public static PlaceholderTextView CreateTextView(nfloat height, nfloat width, UIColor backgroundColor, UIColor borderColor, 
			nfloat cornerRadius, UIColor placeHolderColor, nfloat textSize, FontType fontType = FontType.Regular)
		{
			var textView = new PlaceholderTextView
			{
				PlaceholderTextColor = placeHolderColor,
				TranslatesAutoresizingMaskIntoConstraints = false,
			};
			textView.Font = GetFont(fontType, textSize);
			textView.BackgroundColor = backgroundColor;
			textView.Layer.BorderColor = borderColor.CGColor;
			textView.Layer.BorderWidth = 1;
			textView.Layer.CornerRadius = cornerRadius;
			textView.Layer.MasksToBounds = true;
			textView.TextContainerInset = new UIEdgeInsets(10, 10, 10, 10);
			textView.TextAlignment = UITextAlignment.Left;
			AddWidthHeight(height, width, textView);

			return textView;
		}

		public static UITextField CreateTextField(nfloat height, nfloat width, 
			UIColor backgroundColor, UIColor borderColor, 
			nfloat cornerRadius, nfloat textSize, FontType fontType = FontType.Regular)
		{
			var textField = new UITextField
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
			};
			textField.Layer.BorderColor = borderColor.CGColor;
			textField.Layer.BorderWidth = 1;
			textField.Font = GetFont(fontType, textSize);
			textField.BackgroundColor = backgroundColor;
			textField.Layer.CornerRadius = cornerRadius;
			textField.Layer.MasksToBounds = true;
			textField.TextAlignment = UITextAlignment.Left;

			AddWidthHeight(height, width, textField);

			return textField;
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

		public static CustomMvxCachedImageView CreateCustomImageView(nfloat height, nfloat width, string defaultImagePath, nfloat cornerRadius = default(nfloat))
		{
			var imageView = new CustomMvxCachedImageView
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				DefaultImage = new UIImage(defaultImagePath)
			};
			imageView.Layer.CornerRadius = cornerRadius;
			imageView.ClipsToBounds = true;

			AddWidthHeight(height, width, imageView);

			return imageView;
		}

		public static CustomMvxCachedImageView SetPlaceHolder(this CustomMvxCachedImageView customMvxCachedImageView, string loadingPlaceHolder, string errorPlaceHolder)
		{
			customMvxCachedImageView.LoadingPlaceholderImagePath = $"res:{loadingPlaceHolder}";
			customMvxCachedImageView.ErrorPlaceholderImagePath = $"res:{errorPlaceHolder}";
			return customMvxCachedImageView;
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
			searchBar.Layer.BorderColor = ColorHelper.Blue.CGColor;
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

		public static UIScrollView CreateScrollView(nfloat height, nfloat width)
		{
			var scrollView = new UIScrollView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				ScrollEnabled = true
			};

			AddWidthHeight(height, width, scrollView);

			return scrollView;
		}

		public static UIButton CreateButton(nfloat height, nfloat width, UIColor backgroundColor, UIColor textColor, nfloat textSize, nfloat cornerRadius, FontType fontType = FontType.Regular)
		{
			var button = new UIButton
			{
				TranslatesAutoresizingMaskIntoConstraints = false
			};
			button.SetTitleColor(textColor, UIControlState.Normal);
			button.Font = GetFont(fontType, textSize);
			button.BackgroundColor = backgroundColor;
			button.Layer.CornerRadius = cornerRadius;
			button.Layer.MasksToBounds = true;

			AddWidthHeight(height, width, button);

			return button;
		}

		public static UIButton CreateButton(nfloat height, nfloat width, 
			UIColor backgroundColor, UIColor textColor, 
			nfloat textSize, string title, nfloat cornerRadius, 
			UIColor borderColor, nfloat borderWidth, FontType fontType = FontType.Regular)
		{
			var button = CreateButton(height, width, backgroundColor, textColor, textSize, cornerRadius, fontType);

			button.Layer.BorderWidth = borderWidth;
			button.Layer.BorderColor = borderColor.CGColor;

			return button;
		}

		public static CustomUIButton CreateButton(nfloat height, nfloat width, nfloat textSize, UIColor borderColor, nfloat borderWidth, FontType fontType = FontType.Regular)
		{
			var button = new CustomUIButton
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Font = GetFont(fontType, textSize)
			};

			button.Layer.BorderWidth = borderWidth;
			button.Layer.BorderColor = borderColor.CGColor;

			AddWidthHeight(height, width, button);

			return button;
		}

		public static CustomUIButton SetRoundedCorners(this CustomUIButton button, int cornerRadius, CACornerMask cornerMask)
		{
			button.Layer.CornerRadius = cornerRadius;
			button.Layer.MaskedCorners = cornerMask;
			button.Layer.MasksToBounds = true;
			return button;
		}

		public static UIFont GetFont(FontType fontType, nfloat fontSize)
		{
			switch (fontType)
			{
				case FontType.Light:
					return UIFont.FromName("SanFranciscoDisplay-Light", fontSize);
				case FontType.Medium:
					return UIFont.FromName("SanFranciscoDisplay-Medium", fontSize);
				case FontType.Bold:
					return UIFont.FromName("SanFranciscoDisplay-Bold", fontSize);
				default:
					return UIFont.FromName("SanFranciscoDisplay-Regular", fontSize);
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

		public static iCarousel CreateSlideView(nfloat height, nfloat width)
		{
			var view = new iCarousel
			{
				ContentMode = UIViewContentMode.Center,
				Type = iCarouselType.Linear,
				CenterItemWhenSelected = true,
				TranslatesAutoresizingMaskIntoConstraints = false,
				PagingEnabled = true
			};

			AddWidthHeight(height, width, view);

			return view;
		}

		public static NSAttributedString CreateAttributedString(string text, UIColor color, bool underlineOrNot)
		{
			if (underlineOrNot)
			{
				var attrStr = new NSAttributedString(text, foregroundColor: color, underlineStyle: NSUnderlineStyle.Single);
				return attrStr;
			}
			else
			{
				var attrStr = new NSAttributedString(text, foregroundColor: color);
				return attrStr;
			}
		}
	}
}