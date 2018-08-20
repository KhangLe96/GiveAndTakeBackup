using System;
using Foundation;
using GiveAndTake.Core;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace GiveAndTake.iOS.Views.TableViewCells
{
    [Register("PostItemViewCell")]
    public class PostItemViewCell : MvxTableViewCell
    {
        private UIImageView _imagePost, _imgMultiImages, _imgAvatar, _imgRequest, _imgAppeciation, _imgExtension, _imgComment;
        private UIButton _btnCategory;
        private UILabel _lbUserName, _lbPostDate, _lbSeperator, _lbPostAddress, _lbPostDescription, _lbRequestCount, _lbAppreciationCount, _lbCommentCount;
        private UIView _reactionArea;

        public PostItemViewCell(IntPtr handle) : base(handle)
        {
            InitViews();
            CreateBinding();
        }

        private void CreateBinding()
        {
            var set = this.CreateBindingSet<PostItemViewCell, PostItemViewModel>();

            set.Bind(_btnCategory)
                .For("Title")
                .To(vm => vm.CategoryName);

            set.Apply();
        }
        
        private void InitViews()
        {
            InitPostPhoto();
            InitMultiImageView();
            InitCategoryButton();
            InitAvatarImageView();
            InitUserNameLabel();
            InitPostDateLabel();
            InitSeperatorLabel();
            InitPostAddressLabel();
            InitPostDescriptionLabel();
            InitReactionArea();
            InitSeperatorLine();
        }

        private void InitPostPhoto()
        {
            _imagePost = UiHelper.CreateImageView(DimensionHelper.ImagePostSize, DimensionHelper.ImagePostSize);
            _imagePost.Image = new UIImage("Images/default_post");
            _imagePost.Layer.CornerRadius = 10;
            _imagePost.ClipsToBounds = true;

            ContentView.AddSubview(_imagePost);

            ContentView.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_imagePost, NSLayoutAttribute.Top, NSLayoutRelation.Equal, ContentView,
                    NSLayoutAttribute.Top, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(_imagePost, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView,
                    NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort)
            });
        }

        private void InitMultiImageView()
        {
            _imgMultiImages = UiHelper.CreateImageView(DimensionHelper.ImageMultiSize, DimensionHelper.ImageMultiSize);
            _imgMultiImages.Image = new UIImage("Images/multiphoto");

            ContentView.AddSubview(_imgMultiImages);

            ContentView.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_imgMultiImages, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _imagePost,
                    NSLayoutAttribute.Bottom, 1, - DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(_imgMultiImages, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _imagePost,
                    NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
            });
        }

        private void InitCategoryButton()
        {
            _btnCategory = new UIButton
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            _btnCategory.SetTitleColor(UIColor.White, UIControlState.Normal);
            _btnCategory.Font = UiHelper.GetFont(FontType.Regular, DimensionHelper.ButtonTextSize);
            _btnCategory.BackgroundColor = ColorHelper.ToUiColor("0fbcf9");
            _btnCategory.Layer.CornerRadius = DimensionHelper.ButtonCategoryHeight / 2;

            _btnCategory.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_btnCategory, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null,
                    NSLayoutAttribute.NoAttribute, 1, DimensionHelper.ButtonCategoryHeight)
            });

            ContentView.AddSubview(_btnCategory);

            ContentView.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_btnCategory, NSLayoutAttribute.Top, NSLayoutRelation.Equal, ContentView,
                    NSLayoutAttribute.Top, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(_btnCategory, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imagePost,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal)
            });
        }

        private void InitAvatarImageView()
        {
            _imgAvatar = UiHelper.CreateImageView(DimensionHelper.ImageAvatarSize, DimensionHelper.ImageAvatarSize);
            _imgAvatar.Image = new UIImage("Images/default_avatar");

            ContentView.AddSubview(_imgAvatar);

            ContentView.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_imgAvatar, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _btnCategory,
                    NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(_imgAvatar, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imagePost,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal)
            });
        }

        private void InitUserNameLabel()
        {
            _lbUserName = UiHelper.CreateLabel(UIColor.Black, DimensionHelper.MediumTextSize);
            _lbUserName.Text = "Quốc Trần";

            ContentView.AddSubview(_lbUserName);

            ContentView.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_lbUserName, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _btnCategory,
                    NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(_lbUserName, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imgAvatar,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal)
            });
        }

        private void InitPostDateLabel()
        {
            _lbPostDate = UiHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);
            _lbPostDate.Text = "Vài giây trước";

            ContentView.AddSubview(_lbPostDate);

            ContentView.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_lbPostDate, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _lbUserName,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(_lbPostDate, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imgAvatar,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal)
            });
        }

        private void InitSeperatorLabel()
        {
            _lbSeperator = UiHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);
            _lbSeperator.Text = "-";

            ContentView.AddSubview(_lbSeperator);

            ContentView.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_lbSeperator, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _lbUserName,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(_lbSeperator, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _lbPostDate,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginText)
            });
        }

        private void InitPostAddressLabel()
        {
            _lbPostAddress = UiHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);
            _lbPostAddress.Text = "Đà Nẵng";

            ContentView.AddSubview(_lbPostAddress);

            ContentView.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_lbPostAddress, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _lbUserName,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(_lbPostAddress, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _lbSeperator,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginText)
            });
        }

        private void InitPostDescriptionLabel()
        {
            _lbPostDescription = UiHelper.CreateLabel(UIColor.Black, DimensionHelper.PostDescriptionTextSize);
            _lbPostDescription.Text = "There\'s a girl but I let her get away\r\nIt\'s all my fault \'cause pride got in the way";
            _lbPostDescription.Lines = 2;

            ContentView.AddSubview(_lbPostDescription);

            ContentView.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_lbPostDescription, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _lbPostDate,
                    NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(_lbPostDescription, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imagePost,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal),
                NSLayoutConstraint.Create(_lbPostDescription, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView,
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
            ContentView.AddConstraints(new []
            {
                NSLayoutConstraint.Create(_reactionArea, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _lbPostDescription, NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(_reactionArea, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imagePost, NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal),
                NSLayoutConstraint.Create(_reactionArea, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _imagePost, NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(_reactionArea, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
            });

            InitRequestIcon();
            InitRequestCountLabel();
            InitAppeciationIcon();
            InitAppeciationCountLabel();
            InitCommentIcon();
            InitCommentCountLabel();
            InitExtensionIcon();
        }

        private void InitRequestIcon()
        {
            _imgRequest =
                UiHelper.CreateImageView(DimensionHelper.ButtonRequestHeight, DimensionHelper.ButtonRequestWidth);
            _imgRequest.Image = new UIImage("Images/request_off");

            _reactionArea.AddSubview(_imgRequest);

            _reactionArea.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_imgRequest, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _reactionArea,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(_imgRequest, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _reactionArea,
                    NSLayoutAttribute.Left, 1, 0)
            });
        }

        private void InitRequestCountLabel()
        {
            _lbRequestCount = UiHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);
            _lbRequestCount.Text = "15";

            _reactionArea.AddSubview(_lbRequestCount);

            _reactionArea.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_lbRequestCount, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _reactionArea,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(_lbRequestCount, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imgRequest,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginText)
            });
        }

        private void InitAppeciationIcon()
        {
            _imgAppeciation = UiHelper.CreateImageView(DimensionHelper.ButtonSmallHeight, DimensionHelper.ButtonSmallWidth);
            _imgAppeciation.Image = new UIImage("Images/heart_off");

            _reactionArea.AddSubview(_imgAppeciation);

            _reactionArea.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_imgAppeciation, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _reactionArea,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(_imgAppeciation, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _reactionArea,
                    NSLayoutAttribute.Right, 0.33f, 0)
            });
        }

        private void InitAppeciationCountLabel()
        {
            _lbAppreciationCount = UiHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);
            _lbAppreciationCount.Text = "20";

            _reactionArea.AddSubview(_lbAppreciationCount);

            _reactionArea.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_lbAppreciationCount, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _reactionArea,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(_lbAppreciationCount, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imgAppeciation,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginText)
            });
        }

        private void InitCommentIcon()
        {
            _imgComment = UiHelper.CreateImageView(DimensionHelper.ButtonSmallHeight, DimensionHelper.ButtonSmallWidth);
            _imgComment.Image = new UIImage("Images/comment");

            _reactionArea.AddSubview(_imgComment);

            _reactionArea.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_imgComment, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _reactionArea,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(_imgComment, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _reactionArea,
                    NSLayoutAttribute.Right, 0.66f, 0)
            });
        }

        private void InitCommentCountLabel()
        {
            _lbCommentCount = UiHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);
            _lbCommentCount.Text = "20";

            _reactionArea.AddSubview(_lbCommentCount);

            _reactionArea.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_lbCommentCount, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _reactionArea,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(_lbCommentCount, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imgComment,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginText)
            });
        }

        private void InitExtensionIcon()
        {
            _imgExtension = UiHelper.CreateImageView(DimensionHelper.ButtonExtensionHeight, DimensionHelper.ButtonExtensionWidth);
            _imgExtension.Image = new UIImage("Images/extension");

            _reactionArea.AddSubview(_imgExtension);

            _reactionArea.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(_imgExtension, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _reactionArea,
                    NSLayoutAttribute.Bottom, 1, - DimensionHelper.MarginText),
                NSLayoutConstraint.Create(_imgExtension, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _reactionArea,
                    NSLayoutAttribute.Right, 1, 0)
            });
        }

        private void InitSeperatorLine()
        {
            var seperator = UiHelper.CreateView(DimensionHelper.SeperatorHeight, 0, ColorHelper.ToUiColor("0fbcf9"));

            ContentView.AddSubview(seperator);

            ContentView.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(seperator, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, ContentView,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(seperator, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _imagePost,
                    NSLayoutAttribute.Left, 1, 0),
                NSLayoutConstraint.Create(seperator, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView,
                    NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
            });
        }
    }
}