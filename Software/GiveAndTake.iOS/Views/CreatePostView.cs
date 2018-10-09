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
using CoreGraphics;
using GiveAndTake.Core;
using UIKit;

namespace GiveAndTake.iOS.Views
{
	[MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen,
		ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
	public class CreatePostView : BaseView
	{
		private HeaderBar _headerBar;
		private UIButton _chooseProvinceCityButton;
		private UIButton _chooseCategoryButton;
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
			var bindingSet = this.CreateBindingSet<CreatePostView, CreatePostViewModel>();

			bindingSet.Bind(this)
				.For(v => v.CloseCommand)
				.To(vm => vm.CloseCommand);

			bindingSet.Apply();

			InitHeaderBar();
			InitChooseProvinceCityButton();
			InitChooseCategoryButton();
			InitPostTitleTextField();
			InitPostDescriptionTextView();
			InitChoosePictureButton();
			InitTakePictureButton();
			InitSelectedImageTextView();
			InitCancelButton();
			InitSubmitButton();

			CreateBinding();
		}

		protected override void CreateBinding()
		{
			var bindingSet = this.CreateBindingSet<CreatePostView, CreatePostViewModel>();

			bindingSet.Bind(_btnTakePicture)
				.To(vm => vm.TakePictureCommand);

			bindingSet.Bind(_btnSubmit.Tap())
				.For(v => v.Command)
				.To(vm => vm.SubmitCommand);

			bindingSet.Bind(_postTitleTextField)
				.For("Text")
				.To(vm => vm.PostTitle);

			bindingSet.Bind(_chooseProvinceCityButton.Tap())
				.For(v => v.Command)
				.To(vm => vm.ShowProvinceCityCommand);

			bindingSet.Bind(_chooseProvinceCityButton)
				.For("Title")
				.To(vm => vm.ProvinceCity);

			bindingSet.Bind(_chooseCategoryButton.Tap())
				.For(v => v.Command)
				.To(vm => vm.ShowCategoriesCommand);

			bindingSet.Bind(_chooseCategoryButton)
				.For("Title")
				.To(vm => vm.Category);

			bindingSet.Bind(_btnCancel.Tap())
				.For(v => v.Command)
				.To(vm => vm.CloseCommand);

			bindingSet.Bind(this)
				.For(v => v.ImageCommand)
				.To(vm => vm.ImageCommand);

			bindingSet.Bind(_postTitleTextField)
				.For(v => v.Placeholder)
				.To(vm => vm.PostTitlePlaceHolder);

			bindingSet.Bind(_postDescriptionTextView)
				.For(v => v.Placeholder)
				.To(vm => vm.PostDescriptionPlaceHolder);


			bindingSet.Bind(_selectedImageTextView)
				.For(v => v.AttributedText)
				.To(vm => vm.SelectedImage)
				.WithConversion("StringToAttributedString", _selectedImageTextView);

			bindingSet.Bind(_selectedImageTextView.Tap())
				.For(v => v.Command)
				.To(vm => vm.ShowPhotoCollectionCommand);

			if (_postDescriptionTextView.Text == null)
			{
				bindingSet.Bind(_postDescriptionTextView)
					.For(v => v.Text)
					.To(vm => vm.PostDescription);
			}

			bindingSet.Bind(_btnCancel)
				.For("Title")
				.To(vm => vm.BtnCancelTitle);

			bindingSet.Bind(_btnSubmit)
				.For("Title")
				.To(vm => vm.BtnSubmitTitle);

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
			_chooseProvinceCityButton = UIHelper.CreateButton(DimensionHelper.DropDownButtonHeight, DimensionHelper.DropDownButtonWidth,
				ColorHelper.DefaultEditTextFieldColor, UIColor.Black, DimensionHelper.MediumTextSize, null, DimensionHelper.DropDownButtonHeight / 2, FontType.Light);

			_chooseProvinceCityButton.Layer.BorderColor = ColorHelper.Gray.CGColor;
			_chooseProvinceCityButton.Layer.BorderWidth = 1;
			_chooseProvinceCityButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
			_chooseProvinceCityButton.ContentEdgeInsets = new UIEdgeInsets(10, 15, 10, 10);


			View.Add(_chooseProvinceCityButton);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_chooseProvinceCityButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _headerBar,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_chooseProvinceCityButton, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin)
			});
		}

		private void InitChooseCategoryButton()
		{
			_chooseCategoryButton = UIHelper.CreateButton(DimensionHelper.DropDownButtonHeight, DimensionHelper.DropDownButtonWidth,
				ColorHelper.DefaultEditTextFieldColor, UIColor.Black, DimensionHelper.MediumTextSize, null, DimensionHelper.DropDownButtonHeight / 2, FontType.Light);

			_chooseCategoryButton.Layer.BorderColor = ColorHelper.Gray.CGColor;
			_chooseCategoryButton.Layer.BorderWidth = 1;
			_chooseCategoryButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
			_chooseCategoryButton.ContentEdgeInsets = new UIEdgeInsets(10, 15, 10, 10);

			View.Add(_chooseCategoryButton);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_chooseCategoryButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _headerBar,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_chooseCategoryButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Right, 1, -DimensionHelper.DefaultMargin)
			});
		}

		private void InitPostTitleTextField()
		{
			_postTitleTextField = UIHelper.CreateTextField(DimensionHelper.DropDownButtonHeight, DimensionHelper.CreatePostEditTextWidth,
				ColorHelper.LightGray, ColorHelper.Gray, DimensionHelper.RoundCorner, DimensionHelper.MediumTextSize, FontType.Light);

			UIView paddingView = new UIView(new CGRect(0, 0, 15, _postTitleTextField.Frame.Height));
			_postTitleTextField.LeftView = paddingView;
			_postTitleTextField.LeftViewMode = UITextFieldViewMode.Always;
			_postTitleTextField.ShouldReturn = (textField) => {
				textField.ResignFirstResponder();
				return true;
			};

			View.Add(_postTitleTextField);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_postTitleTextField, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _chooseProvinceCityButton,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_postTitleTextField, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin)
			});
		}

		private void InitPostDescriptionTextView()
		{
			_postDescriptionTextView = UIHelper.CreateTextView(DimensionHelper.PostDescriptionTextViewHeight, DimensionHelper.CreatePostEditTextWidth,
				ColorHelper.LightGray, ColorHelper.Gray, DimensionHelper.RoundCorner, ColorHelper.Gray, DimensionHelper.MediumTextSize, FontType.Light);


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
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal),
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
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(_btnTakePicture, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _btnChoosePicture,
					NSLayoutAttribute.Left, 1, -DimensionHelper.MarginBig)
			});
		}

		private void InitSelectedImageTextView()
		{
			_selectedImageTextView = UIHelper.CreateLabel(ColorHelper.Gray, DimensionHelper.BigTextSize, FontType.Light);

			View.Add(_selectedImageTextView);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_selectedImageTextView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _postDescriptionTextView,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(_selectedImageTextView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin)
			});
		}

		private void InitCancelButton()
		{
			_btnCancel = UIHelper.CreateAlphaButton(DimensionHelper.CreatePostButtonWidth, DimensionHelper.CreatePostButtonHeight,
				ColorHelper.LightBlue, ColorHelper.DarkBlue, DimensionHelper.MediumTextSize,
				UIColor.White, UIColor.White, ColorHelper.LightBlue, ColorHelper.DarkBlue,
				true, true, FontType.Light);

			View.Add(_btnCancel);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnCancel, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Bottom, 1, -DimensionHelper.MarginBig),
				NSLayoutConstraint.Create(_btnCancel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin)
			});
		}

		private void InitSubmitButton()
		{
			_btnSubmit = UIHelper.CreateAlphaButton(DimensionHelper.CreatePostButtonWidth,
				DimensionHelper.CreatePostButtonHeight,
				UIColor.White, UIColor.White, DimensionHelper.MediumTextSize,
				ColorHelper.LightBlue, ColorHelper.DarkBlue, ColorHelper.LightBlue, ColorHelper.DarkBlue, true, false, FontType.Light);

			View.Add(_btnSubmit);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnSubmit, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Bottom, 1, -DimensionHelper.MarginBig),
				NSLayoutConstraint.Create(_btnSubmit, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Right, 1, -DimensionHelper.DefaultMargin)
			});
		}

		private async void HandleSelectImage(object sender, EventArgs e)
		{
			await MediaHelper.OpenGallery();
			var image = MediaHelper.Images;
			if (image != null)
			{
				ImageCommand.Execute(image);
			}

			image?.Clear();
		}
	}
}