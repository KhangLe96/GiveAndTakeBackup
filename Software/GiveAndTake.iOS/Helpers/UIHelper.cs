using GiveAndTake.Core;
using System;
using FFImageLoading.Cross;
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

		public static MvxCachedImageView CreateCustomImageView(nfloat height, nfloat width, string imagePath, nfloat cornerRadius = default(nfloat))
		{
			var imageView = new MvxCachedImageView
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Image = new UIImage(imagePath)
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

		public static UIButton CreateImageButton(nfloat height, nfloat width, string imagePath)
		{
			var button = new UIButton
			{
				TranslatesAutoresizingMaskIntoConstraints = false
			};

			AddWidthHeight(height, width, button);

			button.SetBackgroundImage(new UIImage(imagePath), UIControlState.Normal);
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
	}
}