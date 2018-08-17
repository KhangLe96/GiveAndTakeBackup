using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using System;
using UIKit;

namespace GiveAndTake.iOS.Views
{
    [MvxRootPresentation]
    public class PostView : BaseView
    {
        private UIImageView imagePost, imgMultiImages, imgAvatar, imgRequest, imgAppeciation, imgExtension, imgComment;
        private UIButton btnCategory;
        private UILabel lbUserName, lbPostDate, lbSeperator, lbPostAddress, lbPostDescription, lbRequestCount, lbAppreciationCount, lbCommentCount;

        protected override void InitView()
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
            InitRequestIcon();
            InitRequestCountLabel();
            InitExtensionIcon();
            //InitAppeciationIcon();
            //InitAppeciationCountLabel();
            //InitCommentIcon();
            //InitCommentCountLabel();
        }

        private void InitPostPhoto()
        {
            imagePost = UIHelper.CreateImageView(DimensionHelper.ImagePostSize, DimensionHelper.ImagePostSize);
            imagePost.Image = new UIImage("Images/default_post");
            imagePost.Layer.CornerRadius = 10;
            imagePost.ClipsToBounds = true;

            View.Add(imagePost);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(imagePost, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.Top, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(imagePost, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort)
            });
        }

        private void InitMultiImageView()
        {
            imgMultiImages = UIHelper.CreateImageView(DimensionHelper.ImageMultiSize, DimensionHelper.ImageMultiSize);
            imgMultiImages.Image = new UIImage("Images/multiphoto");

            View.Add(imgMultiImages);

            View.AddConstraints(new[]
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

            btnCategory.SetTitle("   Sách   ", UIControlState.Normal);
            btnCategory.SetTitleColor(UIColor.White, UIControlState.Normal);
            btnCategory.BackgroundColor = ColorHelper.ToUIColor("0fbcf9");
            btnCategory.Layer.CornerRadius = DimensionHelper.ButtonCategoryHeight / 2;

            btnCategory.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(btnCategory, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null,
                    NSLayoutAttribute.NoAttribute, 1, DimensionHelper.ButtonCategoryHeight)
            });

            View.Add(btnCategory);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(btnCategory, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.Top, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(btnCategory, NSLayoutAttribute.Left, NSLayoutRelation.Equal, imagePost,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginShort)
            });
        }

        private void InitAvatarImageView()
        {
            imgAvatar = UIHelper.CreateImageView(DimensionHelper.ImageAvatarSize, DimensionHelper.ImageAvatarSize);
            imgAvatar.Image = new UIImage("Images/default_avatar");

            View.Add(imgAvatar);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(imgAvatar, NSLayoutAttribute.Top, NSLayoutRelation.Equal, btnCategory,
                    NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(imgAvatar, NSLayoutAttribute.Left, NSLayoutRelation.Equal, imagePost,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginShort)
            });
        }

        private void InitUserNameLabel()
        {
            lbUserName = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.MediumTextSize);
            lbUserName.Text = "Quốc Trần";

            View.Add(lbUserName);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(lbUserName, NSLayoutAttribute.Top, NSLayoutRelation.Equal, btnCategory,
                    NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(lbUserName, NSLayoutAttribute.Left, NSLayoutRelation.Equal, imgAvatar,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginShort)
            });
        }

        private void InitPostDateLabel()
        {
            lbPostDate = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);
            lbPostDate.Text = "Vài giây trước";

            View.Add(lbPostDate);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(lbPostDate, NSLayoutAttribute.Top, NSLayoutRelation.Equal, lbUserName,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(lbPostDate, NSLayoutAttribute.Left, NSLayoutRelation.Equal, imgAvatar,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginShort)
            });
        }

        private void InitSeperatorLabel()
        {
            lbSeperator = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);
            lbSeperator.Text = "-";

            View.Add(lbSeperator);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(lbSeperator, NSLayoutAttribute.Top, NSLayoutRelation.Equal, lbUserName,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(lbSeperator, NSLayoutAttribute.Left, NSLayoutRelation.Equal, lbPostDate,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginShort)
            });
        }

        private void InitPostAddressLabel()
        {
            lbPostAddress = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);
            lbPostAddress.Text = "Đà Nẵng";

            View.Add(lbPostAddress);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(lbPostAddress, NSLayoutAttribute.Top, NSLayoutRelation.Equal, lbUserName,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(lbPostAddress, NSLayoutAttribute.Left, NSLayoutRelation.Equal, lbSeperator,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginShort)
            });
        }

        private void InitPostDescriptionLabel()
        {
            lbPostDescription = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.PostDescriptionTextSize);
            lbPostDescription.Text = "There\'s a girl but I let her get away\r\nIt\'s all my fault \'cause pride got in the way";
            lbPostDescription.Lines = 2;

            View.Add(lbPostDescription);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(lbPostDescription, NSLayoutAttribute.Top, NSLayoutRelation.Equal, lbPostDate,
                    NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(lbPostDescription, NSLayoutAttribute.Left, NSLayoutRelation.Equal, imagePost,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(lbPostDescription, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
            });
        }

        private void InitRequestIcon()
        {
            imgRequest =
                UIHelper.CreateImageView(DimensionHelper.ButtonRequestHeight, DimensionHelper.ButtonSmallWidth);
            imgRequest.Image = new UIImage("Images/request_off");

            View.Add(imgRequest);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(imgRequest, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, imagePost,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(imgRequest, NSLayoutAttribute.Left, NSLayoutRelation.Equal, imagePost,
                    NSLayoutAttribute.Right, 1, DimensionHelper.MarginShort)
            });
        }

        private void InitRequestCountLabel()
        {
            lbRequestCount = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);
            lbRequestCount.Text = "15";

            View.Add(lbRequestCount);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(lbRequestCount, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, imagePost,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(lbRequestCount, NSLayoutAttribute.Left, NSLayoutRelation.Equal, imgRequest,
                    NSLayoutAttribute.Right, 1, 0)
            });
        }

        private void InitExtensionIcon()
        {
            imgExtension = UIHelper.CreateImageView(DimensionHelper.ButtonExtensionHeight, DimensionHelper.ButtonExtensionWidth);
            imgExtension.Image = new UIImage("Images/extension");

            View.Add(imgExtension);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(imgExtension, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, imagePost,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(imgExtension, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
            });
        }

        private void InitAppeciationIcon()
        {
            imgAppeciation = UIHelper.CreateImageView(DimensionHelper.ButtonSmallHeight, DimensionHelper.ButtonSmallWidth);
            imgAppeciation.Image = new UIImage("Images/heart_off");

            View.Add(imgAppeciation);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(imgAppeciation, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, imagePost,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(imgAppeciation, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, imgExtension,
                    NSLayoutAttribute.CenterX, 1, GetAppreciationImageX())
            });
        }

        private void InitAppeciationCountLabel()
        {
            lbAppreciationCount = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);
            lbAppreciationCount.Text = "20";

            View.Add(lbAppreciationCount);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(lbAppreciationCount, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, imagePost,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(lbAppreciationCount, NSLayoutAttribute.Left, NSLayoutRelation.Equal, imgAppeciation,
                    NSLayoutAttribute.Right, 1, 0)
            });
        }

        private void InitCommentIcon()
        {
            imgComment = UIHelper.CreateImageView(DimensionHelper.ButtonSmallHeight, DimensionHelper.ButtonSmallWidth);
            imgComment.Image = new UIImage("Images/comment");

            View.Add(imgComment);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(imgComment, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, imagePost,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(imgComment, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, null,
                    NSLayoutAttribute.NoAttribute, 1, GetcommentImageX())
            });
        }

        private void InitCommentCountLabel()
        {
            lbCommentCount = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.SmallTextSize);
            lbCommentCount.Text = "20";

            View.Add(lbCommentCount);

            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(lbCommentCount, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, imagePost,
                    NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(lbCommentCount, NSLayoutAttribute.Left, NSLayoutRelation.Equal, imgAppeciation,
                    NSLayoutAttribute.Right, 1, 0)
            });
        }

        private nfloat GetAppreciationImageX()
        {
            return (imgExtension.Center.X - imgRequest.Center.X) / 3;
        }

        private nfloat GetcommentImageX()
        {
            return (imgExtension.Center.X + imgRequest.Center.X * 2) / 3;
        }
    }
}