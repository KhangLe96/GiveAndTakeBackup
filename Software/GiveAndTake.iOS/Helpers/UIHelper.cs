using GiveAndTake.Core;
using System;
using GiveAndTake.iOS.Controls;
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

	    public static CustomUIImage CreateCustomImageView(nfloat height, nfloat width, string imagePath)
	    {
	        var view = new CustomUIImage
            {
	            TranslatesAutoresizingMaskIntoConstraints = false,
                Image = new UIImage(imagePath)
	        };

	        view.AddConstraints(new[]
	        {
	            NSLayoutConstraint.Create(view, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 0 , height),
	            NSLayoutConstraint.Create(view, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 0 , width)
	        });

            return view;
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

	        searchBar.AddConstraints(new[]
	        {
	            NSLayoutConstraint.Create(searchBar, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 0 , height),
	            NSLayoutConstraint.Create(searchBar, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 0 , width)
	        });

	        return searchBar;
	    }

        public static UIButton CreateImageButton(nfloat height, nfloat width, string imagePath)
	    {
	        var button = new UIButton
	        {
	            TranslatesAutoresizingMaskIntoConstraints = false
	        };

	        button.AddConstraints(new[]
	        {
	            NSLayoutConstraint.Create(button, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 0 , height),
	            NSLayoutConstraint.Create(button, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 0 , width)
	        });

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

	        if (width > 0)
	        {
	            tableView.AddConstraint(NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Width,
	                NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, width));
	        }

	        if (height > 0)
	        {
	            tableView.AddConstraint(NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Height,
	                NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, height));
	        }

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
	}
}