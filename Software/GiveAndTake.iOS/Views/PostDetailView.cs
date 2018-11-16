using CoreGraphics;
using GiveAndTake.Core;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.iOS.CustomControls;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using System.Collections.Generic;
using GiveAndTake.iOS.Controls;
using UIKit;
using Xamarin.iOS.iCarouselBinding;

namespace GiveAndTake.iOS.Views
{
	[MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen,
		ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
	public class PostDetailView : BaseView
	{
		public IMvxCommand<int> ShowFullImageCommand { get; set; }
		public IMvxCommand<int> UpdateImageIndexCommand { get; set; }
		public List<Image> PostImages
		{
			get => _postImages;
			set
			{
				_postImages = value;
				_carouselView.DataSource = new SlideViewDataSource(_postImages,
					new CGRect(0, 0, ResolutionHelper.Width, DimensionHelper.ImageSliderHeight));
				_carouselView.Delegate = new SlideViewDelegate
				{
					OnItemClicked = () => ShowFullImageCommand.Execute((int)_carouselView.CurrentItemIndex),
					OnPageShowed = () => UpdateImageIndexCommand.Execute((int)_carouselView.CurrentItemIndex)
				};
			}
		}

		public int PostImageIndex
		{
			get => _postImageIndex;
			set
			{
				_postImageIndex = value;
				_carouselView?.ScrollToItemAtIndex(value, true);
			}
		}

		public string Status
		{
			get => _status;
			set
			{
				_status = value;
				UpdatePostStatus();
			}
		}

		private string _status;
		public bool IsRequested
		{
			get => _isRequested;
			set
			{
				_isRequested = value;
				InitSetRequestIcon();
			}
		}

		private bool _isRequested;
		private UIButton _btnCategory;
		private UIButton _btnExtension;
		private UIButton _backNavigationButton;
		private UIButton _nextNavigationButton;
		private iCarousel _carouselView;
		private int _postImageIndex;
		private UIImageView _imgRequest;
		private UIImageView _imgLocation;
		private UIImageView _imgComment;
		private CustomMvxCachedImageView _imgAvatar;
		private UILabel _lbCommentCount;
		private UILabel _lbPostStatus;
		private UILabel _lbPostAddress;
		private UILabel _lbRequestCount;
		private UILabel _lbUserName;
		private UILabel _lbPostDate;
		private UILabel _lbPostTitle;
		private UILabel _lbPostDescription;
		private UILabel _lbPageIndex;
		private UIScrollView _scrollView;
		private UIView _contentView;
		private UIView _postInformationView;
		private UIView _imageView;
		private UIView _pageIndexView;
		private UIView _extensionTouchView;
		private UIView _requestTouchView;
		private UIView _commentTouchView;
		private UIView _leftNavigationTouchView;
		private UIView _rightNavigationTouchView;
		private List<Image> _postImages;
		private UIView _extensionView;

		protected override void InitView()
		{
			InitHeader();
			InitScrollContentView();
		}

		protected override void CreateBinding()
		{
			var bindingSet = this.CreateBindingSet<PostDetailView, PostDetailViewModel>();

			bindingSet.Bind(_btnCategory)
				.For("Title")
				.To(vm => vm.CategoryName);

			bindingSet.Bind(_btnCategory)
				.For(v => v.BackgroundColor)
				.To(vm => vm.CategoryBackgroundColor)
				.WithConversion("StringToUIColor");

			bindingSet.Bind(_lbPostAddress)
				.To(vm => vm.Address);

			bindingSet.Bind(this)
				.For(v => v.Status)
				.To(vm => vm.Status);

			bindingSet.Bind(_extensionView.Tap())
				.For(v => v.Command)
				.To(vm => vm.ShowMenuPopupCommand);

			bindingSet.Bind(this)
				.For(v => v.PostImages)
				.To(vm => vm.PostImages);

			bindingSet.Bind(this)
				.For(v => v.PostImageIndex)
				.To(vm => vm.PostImageIndex);

			bindingSet.Bind(this)
				.For(v => v.UpdateImageIndexCommand)
				.To(vm => vm.UpdateImageIndexCommand);

			bindingSet.Bind(this)
				.For(v => v.ShowFullImageCommand)
				.To(vm => vm.ShowFullImageCommand);

			bindingSet.Bind(_backNavigationButton)
				.For("Visibility")
				.To(vm => vm.CanNavigateLeft)
				.WithConversion("InvertBool");

			bindingSet.Bind(_nextNavigationButton)
				.For("Visibility")
				.To(vm => vm.CanNavigateRight)
				.WithConversion("InvertBool");

			bindingSet.Bind(_leftNavigationTouchView)
				.For("Visibility")
				.To(vm => vm.CanNavigateLeft)
				.WithConversion("InvertBool");

			bindingSet.Bind(_rightNavigationTouchView)
				.For("Visibility")
				.To(vm => vm.CanNavigateRight)
				.WithConversion("InvertBool");

			bindingSet.Bind(_leftNavigationTouchView.Tap())
				.For(v => v.Command)
				.To(vm => vm.NavigateLeftCommand);

			bindingSet.Bind(_rightNavigationTouchView.Tap())
				.For(v => v.Command)
				.To(vm => vm.NavigateRightCommand);

			bindingSet.Bind(_lbPageIndex)
				.To(vm => vm.ImageIndexIndicator);

			bindingSet.Bind(_lbRequestCount)
				.To(vm => vm.RequestCount);

			bindingSet.Bind(_requestTouchView.Tap())
				.For(v => v.Command)
				.To(vm => vm.ShowMyRequestListCommand);

			bindingSet.Bind(_lbCommentCount)
				.To(vm => vm.CommentCount);

			bindingSet.Bind(_commentTouchView.Tap())
				.For(v => v.Command)
				.To(vm => vm.ShowPostCommentCommand);

			bindingSet.Bind(_imgAvatar)
				.For(v => v.ImageUrl)
				.To(vm => vm.AvatarUrl);

			bindingSet.Bind(_imgAvatar.Tap())
				.For(v => v.Command)
				.To(vm => vm.ShowGiverProfileCommand);

			bindingSet.Bind(_lbUserName)
				.To(vm => vm.UserName);

			bindingSet.Bind(_lbPostDate)
				.To(vm => vm.CreatedTime);

			bindingSet.Bind(_lbPostTitle)
				.To(vm => vm.PostTitle);

			bindingSet.Bind(_lbPostDescription)
				.To(vm => vm.PostDescription);

			bindingSet.Bind(Header)
				.For(v => v.BackPressedCommand)
				.To(vm => vm.BackPressedCommand);

			bindingSet.Bind(this)
				.For(v => v.IsRequested)
				.To(vm => vm.IsRequested);

			bindingSet.Apply();
		}

		#region InitViews

		private void InitHeader()
		{
			Header.BackButtonIsShown = true;
			_btnCategory = UIHelper.CreateButton(DimensionHelper.ButtonCategoryHeight,
				0,
				ColorHelper.Blue,
				UIColor.White,
				DimensionHelper.ButtonTextSize,
				DimensionHelper.ButtonCategoryHeight / 2);
			View.AddSubview(_btnCategory);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnCategory, NSLayoutAttribute.Top, NSLayoutRelation.Equal, Header,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginObjectPostDetail),
				NSLayoutConstraint.Create(_btnCategory, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, DimensionHelper.MarginObjectPostDetail)
			});

			_imgLocation = UIHelper.CreateImageView(DimensionHelper.LocationLogoHeight,
				DimensionHelper.LocationLogoWidth, ImageHelper.LocationLogo);
			View.AddSubview(_imgLocation);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_imgLocation, NSLayoutAttribute.Top, NSLayoutRelation.Equal, Header,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginObjectPostDetail + 2),
				NSLayoutConstraint.Create(_imgLocation, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _btnCategory,
					NSLayoutAttribute.Right, 1, DimensionHelper.DefaultMargin)
			});

			_lbPostAddress = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.MediumTextSize, FontType.Light);
			View.AddSubview(_lbPostAddress);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbPostAddress, NSLayoutAttribute.Top, NSLayoutRelation.Equal, Header,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginObjectPostDetail),
				NSLayoutConstraint.Create(_lbPostAddress, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imgLocation,
					NSLayoutAttribute.Right, 1, DimensionHelper.MarginShort)
			});

			_btnExtension = UIHelper.CreateImageButton(DimensionHelper.ExtensionButtonHeight,
				DimensionHelper.ExtensionButtonWidth, ImageHelper.Extension);
			View.AddSubview(_btnExtension);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnExtension, NSLayoutAttribute.Top, NSLayoutRelation.Equal, Header,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.ExtensionButtonMarginTop),
				NSLayoutConstraint.Create(_btnExtension, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Right, 1, -DimensionHelper.MarginShort)
			});

			_extensionTouchView = UIHelper.CreateView(DimensionHelper.PostDetailExtensionTouchFieldHeight,
				DimensionHelper.PostDetailExtensionTouchFieldWidth, null);
			View.AddSubview(_extensionTouchView);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_extensionTouchView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, Header,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_extensionTouchView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Right, 1, 0)
			});

			_lbPostStatus = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.PostDetailStatusTextSize, FontType.Bold);
			View.AddSubview(_lbPostStatus);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbPostStatus, NSLayoutAttribute.Top, NSLayoutRelation.Equal, Header,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginObjectPostDetail),
				NSLayoutConstraint.Create(_lbPostStatus, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _btnExtension,
					NSLayoutAttribute.Left, 1, -DimensionHelper.DefaultMargin)
			});

			_extensionView = UIHelper.CreateView(0, 0);
			View.AddSubview(_extensionView);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_extensionView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, Header,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_extensionView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Right, 1, 0),
				NSLayoutConstraint.Create(_extensionView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _btnCategory,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginObjectPostDetail),
				NSLayoutConstraint.Create(_extensionView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _lbPostStatus,
					NSLayoutAttribute.Right, 1, 0)
			});
		}

		private void InitScrollContentView()
		{
			_scrollView = UIHelper.CreateScrollView(0, ResolutionHelper.Width);

			View.AddSubview(_scrollView);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_scrollView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _btnCategory,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginObjectPostDetail),
				NSLayoutConstraint.Create(_scrollView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, 0),
				NSLayoutConstraint.Create(_scrollView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Right, 1, 0),
				NSLayoutConstraint.Create(_scrollView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Bottom, 1, 0),
			});
	
			_contentView = UIHelper.CreateView(0, ResolutionHelper.Width);
			_scrollView.AddSubview(_contentView);
			_scrollView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_contentView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _scrollView,
					NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_contentView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _scrollView,
					NSLayoutAttribute.Left, 1, 0),
				NSLayoutConstraint.Create(_contentView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _scrollView,
					NSLayoutAttribute.Right, 1, 0),
			});
		
			_imageView = UIHelper.CreateView(DimensionHelper.ImageSliderHeight, ResolutionHelper.Width,
				UIColor.Black);
			_contentView.AddSubview(_imageView);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_imageView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_imageView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Left, 1, 0)
			});

			_carouselView = UIHelper.CreateSlideView(DimensionHelper.ImageSliderHeight, ResolutionHelper.Width);
			_imageView.AddSubview(_carouselView);
			_imageView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_carouselView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.CenterY, 1, 0)
			});

			_backNavigationButton = UIHelper.CreateImageButton(DimensionHelper.NavigationHeight,
				DimensionHelper.NavigationWidth, ImageHelper.BackNavigationButton);
			_contentView.AddSubview(_backNavigationButton);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_backNavigationButton, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.CenterY, 1, 0),
				NSLayoutConstraint.Create(_backNavigationButton, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.Left, 1, DimensionHelper.MarginNormal)
			});

			_leftNavigationTouchView = UIHelper.CreateView(DimensionHelper.PostDetailExtensionTouchFieldHeight,
				DimensionHelper.PostDetailExtensionTouchFieldWidth, null);
			_contentView.AddSubview(_leftNavigationTouchView);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_leftNavigationTouchView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.CenterY, 1, 0),
				NSLayoutConstraint.Create(_leftNavigationTouchView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.Left, 1, 0)
			});

			_nextNavigationButton = UIHelper.CreateImageButton(DimensionHelper.NavigationHeight,
				DimensionHelper.NavigationWidth, ImageHelper.NextNavigationButton);
			_contentView.AddSubview(_nextNavigationButton);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_nextNavigationButton, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.CenterY, 1, 0),
				NSLayoutConstraint.Create(_nextNavigationButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.MarginNormal)
			});

			_rightNavigationTouchView = UIHelper.CreateView(DimensionHelper.PostDetailExtensionTouchFieldHeight,
				DimensionHelper.PostDetailExtensionTouchFieldWidth, null);
			_contentView.AddSubview(_rightNavigationTouchView);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_rightNavigationTouchView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.CenterY, 1, 0),
				NSLayoutConstraint.Create(_rightNavigationTouchView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.Right, 1, 0)
			});

			_pageIndexView = UIHelper.CreateView(DimensionHelper.PostDetailImageIndexHeight, DimensionHelper.PostDetailImageIndexWidth, UIColor.Black.ColorWithAlpha((float)0.5), 5);
			_contentView.AddSubview(_pageIndexView);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_pageIndexView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.Top, 1, DimensionHelper.MarginObjectPostDetail),
				NSLayoutConstraint.Create(_pageIndexView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.MarginNormal)
			});

			_lbPageIndex = UIHelper.CreateLabel(UIColor.White, DimensionHelper.BigTextSize, FontType.Light);
			_pageIndexView.AddSubview(_lbPageIndex);
			_pageIndexView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbPageIndex, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _pageIndexView,
					NSLayoutAttribute.CenterY, 1, 0),
				NSLayoutConstraint.Create(_lbPageIndex, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _pageIndexView,
					NSLayoutAttribute.CenterX, 1, 0)
			});

			_lbRequestCount = UIHelper.CreateLabel(ColorHelper.Gray, DimensionHelper.PostDetailBigTextSize, FontType.Light);
			_contentView.AddSubview(_lbRequestCount);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbRequestCount, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginObjectPostDetail),
				NSLayoutConstraint.Create(_lbRequestCount, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.MarginObjectPostDetail)
			});
		
			_imgRequest = UIHelper.CreateImageView(DimensionHelper.PostDetailRequestListLogoHeight, DimensionHelper.PostDetailRequestListLogoWidth, ImageHelper.RequestOff);
			_contentView.AddSubview(_imgRequest);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_imgRequest, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.PostDetailRequestListLogoMarginTop),
				NSLayoutConstraint.Create(_imgRequest, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _lbRequestCount,
					NSLayoutAttribute.Left, 1, -DimensionHelper.PostDetailSmallMargin)
			});

			_requestTouchView = UIHelper.CreateView(DimensionHelper.PostDetailRequestTouchFieldHeight,
				DimensionHelper.PostDetailRequestTouchFieldWidth, null);
			_contentView.AddSubview(_requestTouchView);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_requestTouchView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_requestTouchView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Right, 1, 0)
			});

			_lbCommentCount = UIHelper.CreateLabel(ColorHelper.Gray, DimensionHelper.PostDetailBigTextSize, FontType.Light);
			_contentView.AddSubview(_lbCommentCount);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbCommentCount, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginObjectPostDetail),
				NSLayoutConstraint.Create(_lbCommentCount, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _imgRequest,
					NSLayoutAttribute.Left, 1, -DimensionHelper.PostDetailBigMargin)
			});
		
			_imgComment = UIHelper.CreateImageView(DimensionHelper.PostDetailCommentLogoSize, DimensionHelper.PostDetailCommentLogoSize, ImageHelper.CommentIcon);
			_contentView.AddSubview(_imgComment);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_imgComment, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.PostDetailCommentLogoMarginTop),
				NSLayoutConstraint.Create(_imgComment, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _lbCommentCount,
					NSLayoutAttribute.Left, 1, -DimensionHelper.DefaultMargin)
			});

			_commentTouchView = UIHelper.CreateView(DimensionHelper.PostDetailRequestTouchFieldHeight,
				DimensionHelper.PostDetailRequestTouchFieldWidth, null);
			_contentView.AddSubview(_commentTouchView);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_commentTouchView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _imageView,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_commentTouchView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _requestTouchView,
					NSLayoutAttribute.Left, 1, 0)
			});


			_postInformationView = UIHelper.CreateView(0, DimensionHelper.PostDetailContentViewWidth, ColorHelper.LightGray,
				DimensionHelper.RoundCorner);
			_postInformationView.Layer.BorderColor = ColorHelper.Gray.CGColor;
			_postInformationView.Layer.BorderWidth = 1;
			_contentView.AddSubview(_postInformationView);
			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_postInformationView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _imgComment,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginObjectPostDetail),
				NSLayoutConstraint.Create(_postInformationView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _contentView,
					NSLayoutAttribute.Left, 1, DimensionHelper.MarginObjectPostDetail)
			});
		
			_imgAvatar = UIHelper.CreateCustomImageView(DimensionHelper.PostDetailAvatarSize, DimensionHelper.PostDetailAvatarSize, ImageHelper.DefaultAvatar, DimensionHelper.PostDetailAvatarSize / 2);
			_postInformationView.AddSubview(_imgAvatar);
			_postInformationView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_imgAvatar, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _postInformationView,
					NSLayoutAttribute.Top, 1, DimensionHelper.MarginObjectPostDetail),
				NSLayoutConstraint.Create(_imgAvatar, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _postInformationView,
					NSLayoutAttribute.Left, 1, DimensionHelper.MarginObjectPostDetail)
			});

			_lbUserName = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.PostDetailNormalTextSize);
			_postInformationView.AddSubview(_lbUserName);
			_postInformationView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbUserName, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _postInformationView,
					NSLayoutAttribute.Top, 1, DimensionHelper.PostDetailBigMargin),
				NSLayoutConstraint.Create(_lbUserName, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imgAvatar,
					NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal)
			});
		
			_lbPostDate = UIHelper.CreateLabel(UIColor.Gray, DimensionHelper.PostDetailSmallTextSize, FontType.Light);
			_postInformationView.AddSubview(_lbPostDate);
			_postInformationView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbPostDate, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _lbUserName,
					NSLayoutAttribute.Bottom, 1, 1),
				NSLayoutConstraint.Create(_lbPostDate, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imgAvatar,
					NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal)
			});
	
			_lbPostTitle = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.PostDetailNormalTextSize, FontType.Bold);
			_postInformationView.AddSubview(_lbPostTitle);
			_postInformationView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbPostTitle, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _imgAvatar,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginObjectPostDetail),
				NSLayoutConstraint.Create(_lbPostTitle, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _postInformationView,
					NSLayoutAttribute.Left, 1, DimensionHelper.MarginObjectPostDetail),
				NSLayoutConstraint.Create(_lbPostTitle, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _postInformationView,
					NSLayoutAttribute.Right, 1, -DimensionHelper.MarginObjectPostDetail)
			});
		
			_lbPostDescription = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.PostDetailNormalTextSize);
			_postInformationView.AddSubview(_lbPostDescription);
			_postInformationView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbPostDescription, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _lbPostTitle,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_lbPostDescription, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _postInformationView,
					NSLayoutAttribute.Left, 1, DimensionHelper.MarginObjectPostDetail),
				NSLayoutConstraint.Create(_lbPostDescription, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _postInformationView,
					NSLayoutAttribute.Right, 1, -DimensionHelper.MarginObjectPostDetail),
				NSLayoutConstraint.Create(_postInformationView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _lbPostDescription,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginObjectPostDetail)
			});

			_contentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_contentView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _postInformationView,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginObjectPostDetail)
			});
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			_scrollView.ContentSize = _contentView.Frame.Size;
		}

		private void UpdatePostStatus()
		{
			_lbPostStatus.AttributedText = UIHelper.CreateAttributedString(_status, _status == AppConstants.GivingStatus ? ColorHelper.Green : ColorHelper.DarkRed, false);
		}


		private void InitSetRequestIcon()
		{
			_imgRequest.Image = UIImage.FromBundle(IsRequested ? ImageHelper.RequestOn : ImageHelper.RequestOff);
		}

		#endregion
	}
}