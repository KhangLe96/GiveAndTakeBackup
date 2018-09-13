using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreAnimation;
using Foundation;
using GiveAndTake.Core.Helpers;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.iOS.Controls;
using GiveAndTake.iOS.CustomControls;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views
{
	[MvxModalPresentation]
	public class CreatePostView : BaseView
	{
		private HeaderBar _headerBar;
		private UIButton _btnChooseProvinceCity;
		private UIButton _btnChooseCategory;
		private UITextField _postTitleTextField;
		private PlaceholderTextView _postDescriptionTextView;
		private UIButton _btnTakePicture;
		private UIButton _btnChoosePicture;
		private UIButton _btnCancel;
		private UIButton _btnSubmit;
		private UILabel _selectedImageTextView;

		public IMvxCommand<List<byte[]>> ImageCommand { get; set; }
		public IMvxAsyncCommand CloseCommand { get; set; }

		protected override void InitView()
		{
			ResolutionHelper.InitStaticVariable();
			DimensionHelper.InitStaticVariable();
			ImageHelper.InitStaticVariable();

			var bindingSet = this.CreateBindingSet<CreatePostView, CreatePostViewModel>();

			bindingSet.Bind(this)
				.For(v => v.CloseCommand)
				.To(vm => vm.CloseCommand);

			bindingSet.Apply();

			InitHeaderBar();
			InitChooseProvinceCityButton();
			InitChooseCategoryButton();
			InitPostTitleEditText();
			InitPostDescriptionEditText();
			InitChoosePictureButton();
			InitTakePictureButton();
			InitSelectedImageTextView();
			InitCancelButton();
			InitSubmitButton();
		}

		protected override void CreateBinding()
		{
			var bindingSet = this.CreateBindingSet<CreatePostView, CreatePostViewModel>();

			bindingSet.Bind(_btnTakePicture.Tap())
				.For(v => v.Command)
				.To(vm => vm.TakePictureCommand);

			bindingSet.Bind(_btnChooseProvinceCity.TitleLabel)
				.For(v => v.Text)
				.To(vm => vm.ProvinceCity);

			bindingSet.Bind(_btnChooseCategory.TitleLabel)
				.For(v => v.Text)
				.To(vm => vm.Category);

			bindingSet.Bind(_btnSubmit.Tap())
				.For(v => v.Command)
				.To(vm => vm.SubmitCommand);

			bindingSet.Bind(_postTitleTextField)
				.For(v => v.Text)
				.To(vm => vm.PostTitle);

			bindingSet.Bind(_postDescriptionTextView)
				.For(v => v.Text)
				.To(vm => vm.PostDescription);

			bindingSet.Bind(_btnChooseProvinceCity.Tap())
				.For(v => v.Command)
				.To(vm => vm.ShowProvinceCityCommand);

			bindingSet.Bind(_btnChooseCategory.Tap())
				.For(v => v.Command)
				.To(vm => vm.ShowCategoriesCommand);

			bindingSet.Bind(_btnCancel.Tap())
				.For(v => v.Command)
				.To(vm => vm.CloseCommand);

			bindingSet.Bind(this)
				.For(v => v.ImageCommand)
				.To(vm => vm.ImageCommand);

			bindingSet.Bind(_selectedImageTextView)
				.For(v => v.Text)
				.To(vm => vm.SelectedImage);

			bindingSet.Bind(_selectedImageTextView.Tap())
				.For(v => v.Command)
				.To(vm => vm.ShowPhotoCollectionCommand);

			bindingSet.Apply();
		}

		private void InitHeaderBar()
		{
			_headerBar = UIHelper.CreateHeaderBar(ResolutionHelper.Width, DimensionHelper.HeaderBarHeight,
				UIColor.White, true);
			_headerBar.BackPressedCommand = CloseCommand;
			View.Add(_headerBar);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_headerBar, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Top, 1, ResolutionHelper.StatusHeight),
				NSLayoutConstraint.Create(_headerBar, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, 0),
			});
		}

		private void InitChooseProvinceCityButton()
		{
			_btnChooseProvinceCity = UIHelper.CreateButton(DimensionHelper.DropDownButtonHeight, DimensionHelper.DropDownButtonWidth,
				ColorHelper.DefaultEditTextFieldColor, ColorHelper.PlaceHolderTextColor, DimensionHelper.MediumTextSize, "Tỉnh/Thành phố", DimensionHelper.FilterSize / 2);

			_btnChooseProvinceCity.Layer.BorderColor = ColorHelper.PlaceHolderTextColor.CGColor;
			_btnChooseProvinceCity.Layer.BorderWidth = 1;
			_btnChooseProvinceCity.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
			_btnChooseProvinceCity.ContentEdgeInsets = new UIEdgeInsets(10, 15, 10, 10);

			View.Add(_btnChooseProvinceCity);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnChooseProvinceCity, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _headerBar,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_btnChooseProvinceCity, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin)
			});
		}

		private void InitChooseCategoryButton()
		{
			_btnChooseCategory = UIHelper.CreateButton(DimensionHelper.DropDownButtonHeight, DimensionHelper.DropDownButtonWidth,
				ColorHelper.DefaultEditTextFieldColor, ColorHelper.PlaceHolderTextColor, DimensionHelper.MediumTextSize, "Loại ...", DimensionHelper.FilterSize / 2);

			_btnChooseCategory.Layer.BorderColor = ColorHelper.PlaceHolderTextColor.CGColor;
			_btnChooseCategory.Layer.BorderWidth = 1;
			_btnChooseCategory.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
			_btnChooseCategory.ContentEdgeInsets = new UIEdgeInsets(10, 15, 10, 10);

			View.Add(_btnChooseCategory);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnChooseCategory, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _headerBar,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_btnChooseCategory, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Right, 1, -DimensionHelper.DefaultMargin)
			});
		}

		private void InitPostTitleEditText()
		{
			_postTitleTextField = UIHelper.CreateEditTextField(DimensionHelper.DropDownButtonHeight, DimensionHelper.CreatePostEditTextWidth,
				ColorHelper.DefaultEditTextFieldColor, ColorHelper.PlaceHolderTextColor, DimensionHelper.FilterSize / 2);
			_postTitleTextField.Placeholder = "Tiêu đề";
			_postTitleTextField.Font = UIFont.SystemFontOfSize(DimensionHelper.MediumTextSize);
			_postTitleTextField.Layer.SublayerTransform = CATransform3D.MakeTranslation(15, 0, 0);

			View.Add(_postTitleTextField);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_postTitleTextField, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _btnChooseProvinceCity,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_postTitleTextField, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin)
			});
		}

		private void InitPostDescriptionEditText()
		{
			_postDescriptionTextView = UIHelper.CreateEditTextView(DimensionHelper.PostDescriptionEditTextHeight, DimensionHelper.CreatePostEditTextWidth,
				ColorHelper.DefaultEditTextFieldColor, ColorHelper.PlaceHolderTextColor, DimensionHelper.FilterSize / 2, "Mô tả", ColorHelper.PlaceHolderTextColor, DimensionHelper.MediumTextSize);


			View.Add(_postDescriptionTextView);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_postDescriptionTextView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _postTitleTextField,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_postDescriptionTextView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin)
			});
		}

		private void InitChoosePictureButton()
		{
			_btnChoosePicture = UIHelper.CreateImageButton(DimensionHelper.PictureButtonHeight, DimensionHelper.PictureButtonWidth, ImageHelper.ChoosePictureButton);
			_btnChoosePicture.TouchUpInside += HandleSelectImage;

			View.Add(_btnChoosePicture);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnChoosePicture, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _postDescriptionTextView,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_btnChoosePicture, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Right, 1, -DimensionHelper.DefaultMargin)
			});
		}

		private void InitTakePictureButton()
		{
			_btnTakePicture = UIHelper.CreateImageButton(DimensionHelper.PictureButtonHeight, DimensionHelper.PictureButtonWidth, ImageHelper.TakePictureButton);

			View.Add(_btnTakePicture);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnTakePicture, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _postDescriptionTextView,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_btnTakePicture, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _btnChoosePicture,
					NSLayoutAttribute.Left, 1, -DimensionHelper.MarginBig)
			});
		}

		private void InitSelectedImageTextView()
		{
			_selectedImageTextView = UIHelper.CreateLabel(ColorHelper.PlaceHolderTextColor, DimensionHelper.MediumTextSize);
			var selectedImageTextViewText = "Đã chọn 0 hình";

			var normalAttributedTitle = new NSAttributedString(selectedImageTextViewText, underlineStyle: NSUnderlineStyle.Single);
			_selectedImageTextView.AttributedText = normalAttributedTitle;

			View.Add(_selectedImageTextView);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_selectedImageTextView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _postDescriptionTextView,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_selectedImageTextView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin)
			});
		}

		private void InitCancelButton()
		{
			_btnCancel = UIHelper.CreateAlphaButton(DimensionHelper.CreatePostButtonWidth, DimensionHelper.CreatePostButtonHeight,
				ColorHelper.PrimaryColor, ColorHelper.SecondaryColor, DimensionHelper.MediumTextSize,
				UIColor.White, UIColor.White, ColorHelper.PrimaryColor, ColorHelper.SecondaryColor,
				true, true);

			_btnCancel.SetTitle("Hủy", UIControlState.Normal);

			View.Add(_btnCancel);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnCancel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _btnTakePicture,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_btnCancel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin)
			});
		}

		private void InitSubmitButton()
		{
			_btnSubmit = UIHelper.CreateAlphaButton(DimensionHelper.CreatePostButtonWidth, DimensionHelper.CreatePostButtonHeight,
				UIColor.White, UIColor.White, DimensionHelper.MediumTextSize,
				ColorHelper.PrimaryColor, ColorHelper.SecondaryColor, ColorHelper.PrimaryColor, ColorHelper.SecondaryColor);

			_btnSubmit.SetTitle("Đăng", UIControlState.Normal);

			View.Add(_btnSubmit);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnSubmit, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _btnTakePicture,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_btnSubmit, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Right, 1, -DimensionHelper.DefaultMargin)
			});
		}

		async void HandleSelectImage(object sender, EventArgs e)
		{
			await MediaHelper.OpenGallery();
			var image = MediaHelper.images;
			ImageCommand.Execute(image);
		}
	}
}