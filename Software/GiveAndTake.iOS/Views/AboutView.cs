using CoreGraphics;
using Foundation;
using GiveAndTake.Core;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.iOS.Controls;
using GiveAndTake.iOS.CustomControls;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using System;
using System.Collections.Generic;
using UIKit;

namespace GiveAndTake.iOS.Views
{
	[MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen,
		ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
	public class AboutView : BaseView
	{
		private UIScrollView _scrollView;

		private UIImageView _logoAppImg;
		private UIImageView _contactPhoneImg;
		private UIImageView _logoSiouxImg;

		private UIView _touchFieldBackButton;
		private UIView _touchFieldContactPhone;

		private UIButton _backButton;
		private UILabel _appInfoLabel;
		private UIView _separateLine;
		private UILabel _departmentLabel;
		private UILabel _daNangCityLabel;
		private UILabel _mobileAppLabel;
		private UILabel _appVersionLabel;
		private UILabel _appVersionValue;
		private UILabel _releaseDateLabel;
		private UILabel _releaseDateValue;
		private UILabel _supportContactLabel;
		private UILabel _supportContactValue;
		private UILabel _developedByLabel;

		//Review ThanhVo name should be followed naming conventions _headerBarAbout
		private UIView headerBarAbout;

		private IMvxCommand _contactPhonePressedCommand;

		public IMvxCommand BackPressedCommand { get; set; }
		public IMvxCommand ContactPhonePressedCommand => _contactPhonePressedCommand ?? (_contactPhonePressedCommand = new MvxCommand(ContactPhonePressed));
		//Review Thanh Vo Should hanlde it in the view model.  Use interface injection
		private void ContactPhonePressed()
		{
			var url = new NSUrl("tel:" + AppConstants.SupportContactPhone);
			try
			{
				if (UIApplication.SharedApplication.CanOpenUrl(url))
				{
					UIApplication.SharedApplication.OpenUrl(url);
				}
			}
			catch (Exception ex)
			{
				return;
			}
		}
		protected override void CreateBinding()
		{
			base.CreateBinding();
			var bindingSet = this.CreateBindingSet<AboutView, AboutViewModel>();
			bindingSet.Bind(_appInfoLabel).To(vm => vm.AppInfoLabel);
			bindingSet.Bind(_departmentLabel).To(vm => vm.DepartmentLabel);
			bindingSet.Bind(_daNangCityLabel).To(vm => vm.DaNangCityLabel);
			bindingSet.Bind(_mobileAppLabel).To(vm => vm.MobileAppLabel);
			bindingSet.Bind(_appVersionLabel).To(vm => vm.AppVersionLabel);
			bindingSet.Bind(_appVersionValue).To(vm => vm.AppVersionValue);
			bindingSet.Bind(_releaseDateLabel).To(vm => vm.ReleaseDateLabel);
			bindingSet.Bind(_releaseDateValue).To(vm => vm.ReleaseDateValue);
			bindingSet.Bind(_supportContactLabel).To(vm => vm.SupportContactLabel);
			bindingSet.Bind(_supportContactValue).To(vm => vm.SupportContactValue);
			bindingSet.Bind(_developedByLabel).To(vm => vm.DevelopedBy);

			bindingSet.Bind(this).For(v => v.BackPressedCommand).To(vm => vm.BackPressedCommand);

			bindingSet.Apply();
		}
		protected override void InitView()
		{
			HeaderBar.Hidden = true;
			InitHeaderBar();
			InitContent();
		}
		private void InitHeaderBar()
		{
			//headerBar About
			headerBarAbout = UIHelper.CreateView(DimensionHelper.HeaderBarHeight, ResolutionHelper.Width, UIColor.White);
			View.Add(headerBarAbout);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(headerBarAbout, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Top, 1, ResolutionHelper.StatusHeight),
				NSLayoutConstraint.Create(headerBarAbout, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, 0),
			});
			//backButton
			_touchFieldBackButton = UIHelper.CreateView(50, 60);
			_backButton = UIHelper.CreateImageButton(DimensionHelper.BackButtonHeight, DimensionHelper.BackButtonWidth,
				ImageHelper.BackButton);

			_touchFieldBackButton.AddGestureRecognizer(new UITapGestureRecognizer(() =>
			{
				BackPressedCommand?.Execute();
			}));

			headerBarAbout.AddSubview(_backButton);
			headerBarAbout.AddSubview(_touchFieldBackButton);

			headerBarAbout.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_touchFieldBackButton, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, headerBarAbout,
					NSLayoutAttribute.CenterY,1 , 0),
				NSLayoutConstraint.Create(_touchFieldBackButton, NSLayoutAttribute.Left, NSLayoutRelation.Equal, headerBarAbout,
					NSLayoutAttribute.Left, 1, 0),
				NSLayoutConstraint.Create(_backButton, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, headerBarAbout,
					NSLayoutAttribute.CenterY,1 , 0),
				NSLayoutConstraint.Create(_backButton, NSLayoutAttribute.Left, NSLayoutRelation.Equal, headerBarAbout,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin)
			});
			//text
			_appInfoLabel = UIHelper.CreateLabel(ColorHelper.LightBlue, DimensionHelper.BigTextSize, FontType.Medium);

			headerBarAbout.AddSubview(_appInfoLabel);
			headerBarAbout.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_appInfoLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, headerBarAbout,
					NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_appInfoLabel, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, headerBarAbout,
					NSLayoutAttribute.CenterY, 1, 0),
			});

			_separateLine = UIHelper.CreateView(DimensionHelper.HeaderBarLogoWidth, DimensionHelper.SeperatorHeight, ColorHelper.Gray);

			headerBarAbout.AddSubview(_separateLine);
			headerBarAbout.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_separateLine, NSLayoutAttribute.Width, NSLayoutRelation.Equal, headerBarAbout,
					NSLayoutAttribute.Width,1 , 0),
				NSLayoutConstraint.Create(_separateLine, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null,
					NSLayoutAttribute.NoAttribute, 1 , DimensionHelper.SeparateLineHeaderHeight),
				NSLayoutConstraint.Create(_separateLine, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, headerBarAbout,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin)
			});

		}


		private void InitContent()
		{
			//scroll
			_scrollView = UIHelper.CreateScrollView(0, 0);

			View.Add(_scrollView);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_scrollView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _separateLine,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_scrollView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, 0),
				NSLayoutConstraint.Create(_scrollView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Right, 1, 0),
				NSLayoutConstraint.Create(_scrollView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Bottom, 1, 0),
			});

			_departmentLabel = UIHelper.CreateLabel(ColorHelper.DarkGray, DimensionHelper.BigTextSize, FontType.Medium);
			_scrollView.AddSubview(_departmentLabel);
			_scrollView.AddConstraints(new[]
			{

				NSLayoutConstraint.Create(_departmentLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _scrollView,
					NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_departmentLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _scrollView,
					NSLayoutAttribute.Top, 1, DimensionHelper.MarginBig),
			});

			_daNangCityLabel = UIHelper.CreateLabel(ColorHelper.DarkGray, DimensionHelper.BigTextSize, FontType.Medium);
			_scrollView.AddSubview(_daNangCityLabel);
			_scrollView.AddConstraints(new[]
			{

				NSLayoutConstraint.Create(_daNangCityLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _scrollView,
					NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_daNangCityLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _departmentLabel,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal),
			});

			_logoAppImg = UIHelper.CreateImageView(DimensionHelper.LogoAppAboutWidth, DimensionHelper.LogoAppAboutHeight, UIColor.White);
			_logoAppImg.Image = UIImage.FromFile(ImageHelper.LogoAppWithText);

			_scrollView.AddSubview(_logoAppImg);
			_scrollView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_logoAppImg, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _scrollView,
					NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_logoAppImg, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _daNangCityLabel,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginBig),

			});

			_mobileAppLabel = UIHelper.CreateLabel(ColorHelper.DarkGray, DimensionHelper.BigTextSize, FontType.Medium);
			_scrollView.AddSubview(_mobileAppLabel);
			_scrollView.AddConstraints(new[]
			{

				NSLayoutConstraint.Create(_mobileAppLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _scrollView,
					NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_mobileAppLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _logoAppImg,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginBig),
			});

			_appVersionLabel = UIHelper.CreateLabel(ColorHelper.DarkGray, DimensionHelper.BigTextSize, FontType.Regular);
			_scrollView.AddSubview(_appVersionLabel);
			_scrollView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_appVersionLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _mobileAppLabel,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginBig),
			   NSLayoutConstraint.Create(_appVersionLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _scrollView,
					NSLayoutAttribute.Left, 1, DimensionHelper.MarginNormal),
			});

			_appVersionValue = UIHelper.CreateLabel(ColorHelper.DarkGray, DimensionHelper.BigTextSize, FontType.Regular);
			_scrollView.AddSubview(_appVersionValue);
			_scrollView.AddConstraints(new[]
			{
			   NSLayoutConstraint.Create(_appVersionValue, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _appVersionLabel,
					NSLayoutAttribute.Top, 1, 0),
			   NSLayoutConstraint.Create(_appVersionValue, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _scrollView,
					NSLayoutAttribute.CenterX, 1, DimensionHelper.DefaultMargin),
			});

			_releaseDateLabel = UIHelper.CreateLabel(ColorHelper.DarkGray, DimensionHelper.BigTextSize, FontType.Regular);
			_scrollView.AddSubview(_releaseDateLabel);
			_scrollView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_releaseDateLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _appVersionLabel,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal),
			   NSLayoutConstraint.Create(_releaseDateLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _appVersionLabel,
					NSLayoutAttribute.Left, 1, 0),
			});

			_releaseDateValue = UIHelper.CreateLabel(ColorHelper.DarkGray, DimensionHelper.BigTextSize, FontType.Regular);
			_scrollView.AddSubview(_releaseDateValue);
			_scrollView.AddConstraints(new[]
			{
			   NSLayoutConstraint.Create(_releaseDateValue, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _appVersionValue,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal),
			   NSLayoutConstraint.Create(_releaseDateValue, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _appVersionValue,
					NSLayoutAttribute.Left, 1, 0),
			});

			_supportContactLabel = UIHelper.CreateLabel(ColorHelper.DarkGray, DimensionHelper.BigTextSize, FontType.Regular);
			_scrollView.AddSubview(_supportContactLabel);
			_scrollView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_supportContactLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _releaseDateLabel,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal),
			   NSLayoutConstraint.Create(_supportContactLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _releaseDateLabel,
					NSLayoutAttribute.Left, 1, 0),
			});

			_supportContactValue = UIHelper.CreateLabel(ColorHelper.DarkGray, DimensionHelper.BigTextSize, FontType.Regular);
			_scrollView.AddSubview(_supportContactValue);
			_scrollView.AddConstraints(new[]
			{
			   NSLayoutConstraint.Create(_supportContactValue, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _releaseDateValue,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal),
			   NSLayoutConstraint.Create(_supportContactValue, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _releaseDateValue,
					NSLayoutAttribute.Left, 1, 0),
			});
			//contact phone touch
			_touchFieldContactPhone = UIHelper.CreateView(40, 40);

			_touchFieldContactPhone.AddGestureRecognizer(new UITapGestureRecognizer(() =>
			{
				ContactPhonePressedCommand?.Execute();
			}));

			_contactPhoneImg = UIHelper.CreateImageView(DimensionHelper.ContactPhoneWidth, DimensionHelper.ContactPhoneHeight, UIColor.White);
			_contactPhoneImg.Image = UIImage.FromFile(ImageHelper.ContactPhone);

			_scrollView.AddSubview(_touchFieldContactPhone);
			_scrollView.AddSubview(_contactPhoneImg);
			_scrollView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_contactPhoneImg, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _supportContactValue,
					NSLayoutAttribute.Right, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_contactPhoneImg, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _releaseDateValue,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_touchFieldContactPhone, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _contactPhoneImg,
					NSLayoutAttribute.CenterX,1 , 0),
				NSLayoutConstraint.Create(_touchFieldContactPhone, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _contactPhoneImg,
					NSLayoutAttribute.CenterY, 1, 0)

			});

			_developedByLabel = UIHelper.CreateLabel(ColorHelper.Black, DimensionHelper.BigTextSize, FontType.Regular);
			_scrollView.AddSubview(_developedByLabel);
			_scrollView.AddConstraints(new[]
			{

				NSLayoutConstraint.Create(_developedByLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _scrollView,
					NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_developedByLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _supportContactValue,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginBig),
			});

			_logoSiouxImg = UIHelper.CreateImageView(DimensionHelper.LogoSiouxWidth, DimensionHelper.LogoSiouxHeight, UIColor.White);
			_logoSiouxImg.Image = UIImage.FromFile(ImageHelper.LogoSioux);

			_scrollView.AddSubview(_logoSiouxImg);
			_scrollView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_logoSiouxImg, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _scrollView,
					NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_logoSiouxImg, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _developedByLabel,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal),

			});

			_scrollView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_scrollView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _logoSiouxImg,
					NSLayoutAttribute.Bottom, 1, 0),
			});
		}

	}
}