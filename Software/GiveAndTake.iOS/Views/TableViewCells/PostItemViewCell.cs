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
        private UIImageView imagePost, imgMultiImages, imgAvatar, imgRequest, imgAppeciation, imgExtension, imgComment;
        private UIButton btnCategory;
        private UILabel lbUserName, lbPostDate, lbSeperator, lbPostAddress, lbPostDescription, lbRequestCount, lbAppreciationCount, lbCommentCount;
        private UIView reactionArea;

        public PostItemViewCell(IntPtr handle) : base(handle)
        {
            InitViews();
            CreateBinding();
        }

        private void CreateBinding()
        {
            var set = this.CreateBindingSet<PostItemViewCell, PostItemViewModel>();

            set.Bind(btnCategory)
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
            imagePost = UIHelper.CreateImageView(DimensionHelper.ImagePostSize, DimensionHelper.ImagePostSize);
            imagePost.Image = new UIImage("Images/default_post");
            imagePost.Layer.CornerRadius = 10;
            imagePost.ClipsToBounds = true;

            ContentView.AddSubview(imagePost);

            ContentView.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(imagePost, NSLayoutAttribute.Top, NSLayoutRelation.Equal, ContentView,
                    NSLayoutAttribute.Top, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(imagePost, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView,
                    NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort)
            });
        }

        private void InitMultiImageView()
        {
            imgMultiImages = UIHelper.CreateImageView(DimensionHelper.ImageMultiSize, DimensionHelper.ImageMultiSize);
            imgMultiImages.Image = new UIImage("Images/multiphoto");

            ContentView.AddSubview(imgMultiImages);

            ContentView.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(imgMultiImages, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, imagePost,
                    NSLayoutAttribute.Bottom, 1, - DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(imgMultiImages, NSLayoutAttribute.Right, NSLayoutRelation.Equal, imagePost,
                    NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
            });
        }

        private void InitCategoryButton()
        {
            btnCategory = new UIButton
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            btnCategory.SetTitleColor(UIColor.White, UIControlState.Normal);
            btnCategory.Font = UIHelper.GetFont(FontType.Regular, DimensionHelper.ButtonTextSize);
            btnCategory.BackgroundColor = ColorHelper.ToUIColor("0fbcf9");
            btnCategory.Layer.CornerRadius = DimensionHelper.ButtonCategoryHeight / 2;

            btnCategory.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(btnCategory, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null,
                    NSLayoutAttribute.NoAttribute, 1, DimensionHelper.ButtonCategoryHeight)
            });

            ContentView.AddSubview(btnCategory);

            ContentView.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(btnCategory, NSLayoutAttribute.Top, NSLayoutRelation.Equal, ContentView,
                    NSLayoutAttribute.Top, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(btnCategory, NSLayoutAttribute.Left, NSLayoutRelation.Equal, imagePost,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal)
            });
        }

        private void InitAvatarImageView()
        {
            imgAvatar = UIHelper.CreateImageView(DimensionHelper.ImageAvatarSize, DimensionHelper.ImageAvatarSize);
            imgAvatar.Image = new UIImage("Images/default_avatar");

            ContentView.AddSubview(imgAvatar);

            ContentView.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(imgAvatar, NSLayoutAttribute.Top, NSLayoutRelation.Equal, btnCategory,
                    NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(imgAvatar, NSLayoutAttribute.Left, NSLayoutRelation.Equal, imagePost,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal)
            });
        }

        private void InitUserNameLabel()
        {
            lbUserName = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.MediumTextSize);
            lbUserName.Text = "Quốc Trần";

            ContentView.AddSubview(lbUserName);

            ContentView.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(lbUserName, NSLayoutAttribute.Top, NSLayoutRelation.Equal, btnCategory,
                    NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(lbUserName, NSLayoutAttribute.Left, NSLayoutRelation.Equal, imgAvatar,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal)
            });
        }

        private void InitPostDateLabel()
        {
            lbPostDate = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);
            lbPostDate.Text = "Vài giây trước";

            ContentView.AddSubview(lbPostDate);

            ContentView.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(lbPostDate, NSLayoutAttribute.Top, NSLayoutRelation.Equal, lbUserName,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(lbPostDate, NSLayoutAttribute.Left, NSLayoutRelation.Equal, imgAvatar,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal)
            });
        }

        private void InitSeperatorLabel()
        {
            lbSeperator = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);
            lbSeperator.Text = "-";

            ContentView.AddSubview(lbSeperator);

            ContentView.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(lbSeperator, NSLayoutAttribute.Top, NSLayoutRelation.Equal, lbUserName,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(lbSeperator, NSLayoutAttribute.Left, NSLayoutRelation.Equal, lbPostDate,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginText)
            });
        }

        private void InitPostAddressLabel()
        {
            lbPostAddress = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);
            lbPostAddress.Text = "Đà Nẵng";

            ContentView.AddSubview(lbPostAddress);

            ContentView.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(lbPostAddress, NSLayoutAttribute.Top, NSLayoutRelation.Equal, lbUserName,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(lbPostAddress, NSLayoutAttribute.Left, NSLayoutRelation.Equal, lbSeperator,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginText)
            });
        }

        private void InitPostDescriptionLabel()
        {
            lbPostDescription = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.PostDescriptionTextSize);
            lbPostDescription.Text = "There\'s a girl but I let her get away\r\nIt\'s all my fault \'cause pride got in the way";
            lbPostDescription.Lines = 2;

            ContentView.AddSubview(lbPostDescription);

            ContentView.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(lbPostDescription, NSLayoutAttribute.Top, NSLayoutRelation.Equal, lbPostDate,
                    NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(lbPostDescription, NSLayoutAttribute.Left, NSLayoutRelation.Equal, imagePost,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal),
                NSLayoutConstraint.Create(lbPostDescription, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView,
                    NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
            });
        }

        private void InitReactionArea()
        {
            reactionArea = new UIView
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            ContentView.Add(reactionArea);
            ContentView.AddConstraints(new []
            {
                NSLayoutConstraint.Create(reactionArea, NSLayoutAttribute.Top, NSLayoutRelation.Equal, lbPostDescription, NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(reactionArea, NSLayoutAttribute.Left, NSLayoutRelation.Equal, imagePost, NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal),
                NSLayoutConstraint.Create(reactionArea, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, imagePost, NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(reactionArea, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
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
            imgRequest =
                UIHelper.CreateImageView(DimensionHelper.ButtonRequestHeight, DimensionHelper.ButtonRequestWidth);
            imgRequest.Image = new UIImage("Images/request_off");

            reactionArea.AddSubview(imgRequest);

            reactionArea.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(imgRequest, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, reactionArea,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(imgRequest, NSLayoutAttribute.Left, NSLayoutRelation.Equal, reactionArea,
                    NSLayoutAttribute.Left, 1, 0)
            });
        }

        private void InitRequestCountLabel()
        {
            lbRequestCount = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);
            lbRequestCount.Text = "15";

            reactionArea.AddSubview(lbRequestCount);

            reactionArea.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(lbRequestCount, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, reactionArea,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(lbRequestCount, NSLayoutAttribute.Left, NSLayoutRelation.Equal, imgRequest,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginText)
            });
        }

        private void InitAppeciationIcon()
        {
            imgAppeciation = UIHelper.CreateImageView(DimensionHelper.ButtonSmallHeight, DimensionHelper.ButtonSmallWidth);
            imgAppeciation.Image = new UIImage("Images/heart_off");

            reactionArea.AddSubview(imgAppeciation);

            reactionArea.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(imgAppeciation, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, reactionArea,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(imgAppeciation, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, reactionArea,
                    NSLayoutAttribute.Right, 0.33f, 0)
            });
        }

        private void InitAppeciationCountLabel()
        {
            lbAppreciationCount = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);
            lbAppreciationCount.Text = "20";

            reactionArea.AddSubview(lbAppreciationCount);

            reactionArea.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(lbAppreciationCount, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, reactionArea,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(lbAppreciationCount, NSLayoutAttribute.Left, NSLayoutRelation.Equal, imgAppeciation,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginText)
            });
        }

        private void InitCommentIcon()
        {
            imgComment = UIHelper.CreateImageView(DimensionHelper.ButtonSmallHeight, DimensionHelper.ButtonSmallWidth);
            imgComment.Image = new UIImage("Images/comment");

            reactionArea.AddSubview(imgComment);

            reactionArea.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(imgComment, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, reactionArea,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(imgComment, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, reactionArea,
                    NSLayoutAttribute.Right, 0.66f, 0)
            });
        }

        private void InitCommentCountLabel()
        {
            lbCommentCount = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);
            lbCommentCount.Text = "20";

            reactionArea.AddSubview(lbCommentCount);

            reactionArea.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(lbCommentCount, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, reactionArea,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(lbCommentCount, NSLayoutAttribute.Left, NSLayoutRelation.Equal, imgComment,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginText)
            });
        }

        private void InitExtensionIcon()
        {
            imgExtension = UIHelper.CreateImageView(DimensionHelper.ButtonExtensionHeight, DimensionHelper.ButtonExtensionWidth);
            imgExtension.Image = new UIImage("Images/extension");

            reactionArea.AddSubview(imgExtension);

            reactionArea.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(imgExtension, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, reactionArea,
                    NSLayoutAttribute.Bottom, 1, - DimensionHelper.MarginText),
                NSLayoutConstraint.Create(imgExtension, NSLayoutAttribute.Right, NSLayoutRelation.Equal, reactionArea,
                    NSLayoutAttribute.Right, 1, 0)
            });
        }

        private void InitSeperatorLine()
        {
            var seperator = UIHelper.CreateView(DimensionHelper.SeperatorHeight, 0, ColorHelper.ToUIColor("0fbcf9"));

            ContentView.AddSubview(seperator);

            ContentView.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(seperator, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, ContentView,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(seperator, NSLayoutAttribute.Left, NSLayoutRelation.Equal, imagePost,
                    NSLayoutAttribute.Left, 1, 0),
                NSLayoutConstraint.Create(seperator, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView,
                    NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
            });
        }
    }
}