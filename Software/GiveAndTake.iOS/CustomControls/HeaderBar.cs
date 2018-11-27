using GiveAndTake.iOS.Helpers;
using System;
using MvvmCross.Commands;
using UIKit;

namespace GiveAndTake.iOS.CustomControls
{
	public class HeaderBar : UIView
	{
		private UIImageView _logoImage;
		private UIView _separateLine;
		private UIButton _backButton;
		private UIView _touchField;
		private bool _backButtonIsShown;
		public IMvxCommand BackPressedCommand { get; set; }
		public bool BackButtonIsShown
		{
			get => _backButtonIsShown;
			set
			{
				_backButtonIsShown = value;
				_backButton.Hidden = !value;
				_touchField.Hidden = !value;
			}
		}

		public HeaderBar()
		{
			InitView();
			InitBackButton();
			BackButtonIsShown = false;
		}

		private void InitView()
		{
			_logoImage = UIHelper.CreateImageView(DimensionHelper.HeaderBarLogoWidth,
				DimensionHelper.HeaderBarLogoHeight, UIColor.White);
			_logoImage.Image = UIImage.FromFile(ImageHelper.TopLogo);

			_separateLine = UIHelper.CreateView(DimensionHelper.HeaderBarLogoWidth,
				DimensionHelper.SeperatorHeight, ColorHelper.Gray);

			AddSubview(_logoImage);
			AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_logoImage, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_logoImage, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.CenterY, 1, 0),
				NSLayoutConstraint.Create(_logoImage, NSLayoutAttribute.Height, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.Height, 1, -DimensionHelper.HeaderBarLogoHeight),
			});

			AddSubview(_separateLine);
			AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_separateLine, NSLayoutAttribute.Width, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.Width,1 , 0),
				NSLayoutConstraint.Create(_separateLine, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null,
					NSLayoutAttribute.NoAttribute, 1 , DimensionHelper.SeparateLineHeaderHeight),
				NSLayoutConstraint.Create(_separateLine, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.Bottom, 1, 0)
			});
		}

		private void InitBackButton()
		{
			_touchField = UIHelper.CreateView(50, 60);
			_backButton = UIHelper.CreateImageButton(DimensionHelper.BackButtonHeight, DimensionHelper.BackButtonWidth,
				ImageHelper.BackButton);

			_touchField.AddGestureRecognizer(new UITapGestureRecognizer(() =>
			{
				BackPressedCommand?.Execute();
			}));
			
			AddSubview(_backButton);
			AddSubview(_touchField);

			AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_touchField, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.CenterY,1 , 0),
				NSLayoutConstraint.Create(_touchField, NSLayoutAttribute.Left, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.Left, 1, 0),
				NSLayoutConstraint.Create(_backButton, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.CenterY,1 , 0),
				NSLayoutConstraint.Create(_backButton, NSLayoutAttribute.Left, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin)
			});
		}
	}
}