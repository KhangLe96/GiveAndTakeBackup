using CoreAnimation;
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
using UIKit;

namespace GiveAndTake.iOS.Views
{
	[MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen,
		ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
	public class CreatePostView : BaseView
	{
		private HeaderBar _headerBar;
		private UITextField _chooseProvinceCityField;
		private UITextField _chooseCategoryField;
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
			InitChooseProvinceCityField();
			InitChooseCategoryField();
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

			bindingSet.Bind(_chooseProvinceCityField.Tap())
				.For(v => v.Command)
				.To(vm => vm.ShowProvinceCityCommand);

			bindingSet.Bind(_chooseProvinceCityField)
				.For(v => v.Placeholder)
				.To(vm => vm.ProvinceCityPlaceHolder);

			bindingSet.Bind(_chooseProvinceCityField)
				.For(v => v.Text)
				.To(vm => vm.ProvinceCity);

			bindingSet.Bind(_chooseCategoryField.Tap())
				.For(v => v.Command)
				.To(vm => vm.ShowCategoriesCommand);

			bindingSet.Bind(_chooseCategoryField)
				.For(v => v.Placeholder)
				.To(vm => vm.CategoryPlaceHolder);

			bindingSet.Bind(_chooseCategoryField)
				.For(v => v.Text)
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
				.WithConversion("SelectedImage", _selectedImageTextView);

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

		private void InitChooseProvinceCityField()
		{
			
			_chooseProvinceCityField = UIHelper.CreateTextField(DimensionHelper.DropDownFieldHeight,
				DimensionHelper.DropDownFieldWidth,
				ColorHelper.LightGray, ColorHelper.Gray, DimensionHelper.RoundCorner);

			_chooseProvinceCityField.UserInteractionEnabled = false;
			_chooseProvinceCityField.Font = UIFont.SystemFontOfSize(DimensionHelper.MediumTextSize);
			_chooseProvinceCityField.Layer.BorderWidth = 1;
			UIView paddingView = new UIView(new CGRect(0, 0, 15, _chooseProvinceCityField.Frame.Height));
			_chooseProvinceCityField.LeftView = paddingView;
			_chooseProvinceCityField.LeftViewMode = UITextFieldViewMode.Always;

			View.Add(_chooseProvinceCityField);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_chooseProvinceCityField, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _headerBar,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_chooseProvinceCityField, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin)
			});
		}

		private void InitChooseCategoryField()
		{
			_chooseCategoryField = UIHelper.CreateTextField(DimensionHelper.DropDownFieldHeight,
				DimensionHelper.DropDownFieldWidth,
				ColorHelper.LightGray, ColorHelper.Gray, DimensionHelper.RoundCorner);

			_chooseCategoryField.UserInteractionEnabled = false;
			_chooseCategoryField.Font = UIFont.SystemFontOfSize(DimensionHelper.MediumTextSize);
			_chooseCategoryField.Layer.BorderWidth = 1;
			UIView paddingView = new UIView(new CGRect(0, 0, 15, _chooseCategoryField.Frame.Height));
			_chooseCategoryField.LeftView = paddingView;
			_chooseCategoryField.LeftViewMode = UITextFieldViewMode.Always;

			View.Add(_chooseCategoryField);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_chooseCategoryField, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _headerBar,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_chooseCategoryField, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Right, 1, -DimensionHelper.DefaultMargin)
			});
		}

		private void InitPostTitleTextField()
		{
			_postTitleTextField = UIHelper.CreateTextField(DimensionHelper.DropDownFieldHeight, DimensionHelper.CreatePostEditTextWidth,
				ColorHelper.LightGray, ColorHelper.Gray, DimensionHelper.RoundCorner);
			
			_postTitleTextField.Font = UIFont.SystemFontOfSize(DimensionHelper.MediumTextSize);
			_postTitleTextField.Layer.SublayerTransform = CATransform3D.MakeTranslation(15, 0, 0);
			_postTitleTextField.ShouldReturn = (textField) => {
				textField.ResignFirstResponder();
				return true;
			};

			View.Add(_postTitleTextField);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_postTitleTextField, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _chooseProvinceCityField,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_postTitleTextField, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, DimensionHelper.DefaultMargin)
			});
		}

		private void InitPostDescriptionTextView()
		{
			_postDescriptionTextView = UIHelper.CreateTextView(DimensionHelper.PostDescriptionTextViewHeight, DimensionHelper.CreatePostEditTextWidth,
				ColorHelper.LightGray, ColorHelper.Gray, DimensionHelper.RoundCorner, ColorHelper.Gray, DimensionHelper.MediumTextSize);


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
			_selectedImageTextView = UIHelper.CreateLabel(ColorHelper.Gray, DimensionHelper.BigTextSize);

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
				true, true);

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
			_btnSubmit = UIHelper.CreateAlphaButton(DimensionHelper.CreatePostButtonWidth, DimensionHelper.CreatePostButtonHeight,
				UIColor.White, UIColor.White, DimensionHelper.MediumTextSize,
				ColorHelper.LightBlue, ColorHelper.DarkBlue, ColorHelper.LightBlue, ColorHelper.DarkBlue);

			View.Add(_btnSubmit);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnSubmit, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Bottom, 1, -DimensionHelper.MarginBig),
				NSLayoutConstraint.Create(_btnSubmit, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Right, 1, -DimensionHelper.DefaultMargin)
			});
		}

		async void HandleSelectImage(object sender, EventArgs e)
		{
			await MediaHelper.OpenGallery();
			var image = MediaHelper.Images;
			if (image != null)
			{
				ImageCommand.Execute(image);
			}
		}
	}
}