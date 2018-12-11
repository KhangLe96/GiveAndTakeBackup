using Foundation;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.iOS.Controls;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using System;
using MvvmCross.Plugin.Color;
using UIKit;

namespace GiveAndTake.iOS.Views.TableViewCells
{
	[Register(nameof(MyPostItemViewCell))]
	public class MyPostItemViewCell : MvxTableViewCell
	{
		private UIImageView _imgMultiImages;
		private CustomUIImageView _imgRequest;
		private UIImageView _imgAppreciation;
		private UIImageView _imgExtension;
		private UIImageView _imgComment;
		private CustomMvxCachedImageView _imagePost;
		private UIButton _btnCategory;
		private UILabel _lbPostDate;
		private UILabel _lbSeparator;
		private UILabel _lbPostAddress;
		private UILabel _lbPostTitle;
		private UILabel _lbRequestCount;
		private UILabel _lbAppreciationCount;
		private UILabel _lbCommentCount;
		private UIView _reactionArea;
		private UIView _separatorLine;
		private UIView _optionView;
		private CustomUILabel _postStatus;

		public MyPostItemViewCell(IntPtr handle) : base(handle)
		{
			InitViews();
			CreateBinding();
		}

		private void CreateBinding()
		{
			var set = this.CreateBindingSet<MyPostItemViewCell, PostItemViewModel>();

			set.Bind(ContentView.Tap())
				.For(v => v.Command)
				.To(vm => vm.ShowPostDetailCommand);

			set.Bind(_imagePost)
				.For(v => v.ImageUrl)
				.To(vm => vm.PostImage);

			set.Bind(_imgMultiImages)
				.For("Visibility")
				.To(vm => vm.HasManyPostPhotos)
				.WithConversion("InvertBool");

			set.Bind(_btnCategory)
				.For("Title")
				.To(vm => vm.CategoryName);

			set.Bind(_lbPostDate)
				.To(vm => vm.CreatedTime);

			set.Bind(_postStatus)
				.To(vm => vm.Status);

			set.Bind(_postStatus)
				.For(v => v.TextColor)
				.To(vm => vm.StatusColor)
				.WithConversion(new MvxNativeColorValueConverter());

			set.Bind(_lbPostAddress)
				.To(vm => vm.Address);

			set.Bind(_lbPostTitle)
				.To(vm => vm.PostTitle);

			set.Bind(_lbRequestCount)
				.To(vm => vm.RequestCount);

			set.Bind(_optionView.Tap())
				.For(v => v.Command)
				.To(vm => vm.ShowMenuPopupCommand);

			set.Bind(_separatorLine)
				.For("Visibility")
				.To(vm => vm.IsSeparatorLineShown)
				.WithConversion("InvertBool");

			set.Bind(_btnCategory)
				.For(v => v.BackgroundColor)
				.To(vm => vm.BackgroundColor)
				.WithConversion("StringToUIColor");

			set.Bind(_imgRequest)
				.For(v => v.IsActivated)
				.To(vm => vm.IsRequestIconActivated);

			set.Apply();
		}

		private void InitViews()
		{
			InitPostPhoto();
			InitPostContent();
			InitReactionArea();
			InitSeperatorLine();
		}

		private void InitPostPhoto()
		{
			_imagePost = UIHelper.CreateCustomImageView(DimensionHelper.ImagePostSize, DimensionHelper.ImagePostSize, ImageHelper.DefaultPost, DimensionHelper.PostPhotoCornerRadius);
			_imagePost.SetPlaceHolder(ImageHelper.DefaultPost, ImageHelper.DefaultPost);

			ContentView.AddSubview(_imagePost);

			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_imagePost, NSLayoutAttribute.Top, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Top, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_imagePost, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort)
			});

			_imgMultiImages = UIHelper.CreateImageView(DimensionHelper.ImageMultiSize, DimensionHelper.ImageMultiSize, ImageHelper.Multiphoto);

			ContentView.AddSubview(_imgMultiImages);

			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_imgMultiImages, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _imagePost,
					NSLayoutAttribute.Bottom, 1, - DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_imgMultiImages, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _imagePost,
					NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
			});
		}

		private void InitPostContent()
		{
			_btnCategory = UIHelper.CreateButton(DimensionHelper.ButtonCategoryHeight,
				0,
				ColorHelper.Blue,
				UIColor.White,
				DimensionHelper.ButtonTextSize,
				DimensionHelper.ButtonCategoryHeight / 2);

			ContentView.AddSubview(_btnCategory);

			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnCategory, NSLayoutAttribute.Top, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Top, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_btnCategory, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imagePost,
					NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal)
			});

			_postStatus = UIHelper.CreateCustomLabel(ColorHelper.Green, DimensionHelper.MediumTextSize);

			ContentView.AddSubview(_postStatus);

			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_postStatus, NSLayoutAttribute.Top, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Top, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_postStatus, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
			});

			_lbPostDate = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);

			ContentView.AddSubview(_lbPostDate);

			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbPostDate, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _btnCategory,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(_lbPostDate, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _btnCategory,
					NSLayoutAttribute.Left, 1, 0)
			});

			_lbSeparator = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);
			_lbSeparator.Text = "-";

			ContentView.AddSubview(_lbSeparator);

			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbSeparator, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _lbPostDate,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_lbSeparator, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _lbPostDate,
					NSLayoutAttribute.Right, 1, DimensionHelper.MarginText)
			});

			_lbPostAddress = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);

			ContentView.AddSubview(_lbPostAddress);

			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbPostAddress, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _lbPostDate,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_lbPostAddress, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _lbSeparator,
					NSLayoutAttribute.Right, 1, DimensionHelper.MarginText)
			});

			_lbPostTitle = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.PostDescriptionTextSize);
			_lbPostTitle.Lines = 3;
			_lbPostTitle.LineBreakMode = UILineBreakMode.TailTruncation;

			ContentView.AddSubview(_lbPostTitle);

			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbPostTitle, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _lbPostDate,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_lbPostTitle, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imagePost,
					NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(_lbPostTitle, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
			});
		}


		private void InitReactionArea()
		{
			_reactionArea = new UIView
			{
				TranslatesAutoresizingMaskIntoConstraints = false
			};
			ContentView.Add(_reactionArea);
			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_reactionArea, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _lbPostTitle, NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_reactionArea, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imagePost, NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(_reactionArea, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _imagePost, NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_reactionArea, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
			});

			_imgRequest =
				UIHelper.CreateImageView(DimensionHelper.ButtonRequestHeight, DimensionHelper.ButtonRequestWidth, ImageHelper.RequestOff, ImageHelper.RequestOn);

			_reactionArea.AddSubview(_imgRequest);

			_reactionArea.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_imgRequest, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _reactionArea,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_imgRequest, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _reactionArea,
					NSLayoutAttribute.Left, 1, 0)
			});

			_lbRequestCount = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);

			_reactionArea.AddSubview(_lbRequestCount);

			_reactionArea.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbRequestCount, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _reactionArea,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_lbRequestCount, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imgRequest,
					NSLayoutAttribute.Right, 1, DimensionHelper.MarginText)
			});

			_optionView = UIHelper.CreateView(0, DimensionHelper.ButtonExtensionWidth * 4);

			ContentView.AddSubview(_optionView);

			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_optionView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _reactionArea,
					NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_optionView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_optionView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Right, 1, 0)
			});

			_imgExtension = UIHelper.CreateImageView(DimensionHelper.ButtonExtensionHeight, DimensionHelper.ButtonExtensionWidth, ImageHelper.Extension);

			_optionView.AddSubview(_imgExtension);

			_optionView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_imgExtension, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _optionView,
					NSLayoutAttribute.Bottom, 1, - DimensionHelper.MarginText - DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_imgExtension, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _optionView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
			});
		}

		private void AppreciationAndComment()
		{
			_imgAppreciation = UIHelper.CreateImageView(DimensionHelper.ButtonSmallHeight, DimensionHelper.ButtonSmallWidth,
				ImageHelper.HeartOff);

			_reactionArea.AddSubview(_imgAppreciation);

			_reactionArea.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_imgAppreciation, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _reactionArea,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_imgAppreciation, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _reactionArea,
					NSLayoutAttribute.Right, 0.33f, 0)
			});

			_lbAppreciationCount = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);

			_reactionArea.AddSubview(_lbAppreciationCount);

			_reactionArea.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbAppreciationCount, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _reactionArea,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_lbAppreciationCount, NSLayoutAttribute.Left, NSLayoutRelation.Equal,
					_imgAppreciation,
					NSLayoutAttribute.Right, 1, DimensionHelper.MarginText)
			});

			_imgComment = UIHelper.CreateImageView(DimensionHelper.ButtonSmallHeight, DimensionHelper.ButtonSmallWidth,
				ImageHelper.CommentIcon);

			_reactionArea.AddSubview(_imgComment);

			_reactionArea.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_imgComment, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _reactionArea,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_imgComment, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _reactionArea,
					NSLayoutAttribute.Right, 0.66f, 0)
			});

			_lbCommentCount = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);

			_reactionArea.AddSubview(_lbCommentCount);

			_reactionArea.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbCommentCount, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _reactionArea,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_lbCommentCount, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imgComment,
					NSLayoutAttribute.Right, 1, DimensionHelper.MarginText)
			});
		}

		private void InitSeperatorLine()
		{
			_separatorLine = UIHelper.CreateView(DimensionHelper.SeperatorHeight, 0, ColorHelper.Blue);

			ContentView.AddSubview(_separatorLine);

			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_separatorLine, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_separatorLine, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imagePost,
					NSLayoutAttribute.Left, 1, 0),
				NSLayoutConstraint.Create(_separatorLine, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
			});
		}
	}
}