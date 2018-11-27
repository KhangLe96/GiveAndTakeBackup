using GiveAndTake.Core;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.iOS.Controls;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using GiveAndTake.iOS.Views.TableViewCells;
using GiveAndTake.iOS.Views.TableViewSources;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace GiveAndTake.iOS.Views
{
	[MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen,
		ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
	public class UserProfileView : BaseView
	{
		public IMvxCommand LoadMorePostsCommand { get; set; }

		private UIView _profileView;
		private UILabel _userNameLabel;
		private UILabel _userRankLabel;
		private UILabel _rankTitleLabel;
		private UILabel _userSentCountLabel;
		private UILabel _sentCountTitleLabel;
		private UITableView _postsTableView;
		private UIButton _openConversationButton;
		private CustomMvxCachedImageView _avatarView;
		private MvxUIRefreshControl _refreshPostsControl;
		private PostItemTableViewSource<MyPostItemViewCell> _postTableViewSource;

		protected override void InitView()
		{
			HeaderBar.BackButtonIsShown = true;

			_profileView = UIHelper.CreateView(DimensionHelper.ProfileViewHeight, ResolutionHelper.Width, ColorHelper.Line);

			View.Add(_profileView);

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_profileView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Top, 1, ResolutionHelper.StatusHeight + DimensionHelper.HeaderBarHeight),
				NSLayoutConstraint.Create(_profileView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, 0)
			});

			_avatarView = UIHelper.CreateCustomImageView(DimensionHelper.AvatarBigSize, DimensionHelper.AvatarBigSize,
				ImageHelper.DefaultAvatar, DimensionHelper.AvatarBigSize / 2);

			_profileView.Add(_avatarView);

			_profileView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_avatarView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _profileView,
					NSLayoutAttribute.Top, 1, DimensionHelper.AvatarMargin),
				NSLayoutConstraint.Create(_avatarView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _profileView,
					NSLayoutAttribute.Left, 1,  DimensionHelper.MarginBig)
			});

			_userNameLabel = UIHelper.CreateLabel(UIColor.White, DimensionHelper.LargeTextSize, FontType.Bold);

			_profileView.Add(_userNameLabel);

			_profileView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_userNameLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _profileView,
					NSLayoutAttribute.Top, 1, DimensionHelper.MarginBig),
				NSLayoutConstraint.Create(_userNameLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _avatarView,
					NSLayoutAttribute.Right, 1,  DimensionHelper.ProfileMarginLeft)
			});

			_rankTitleLabel = UIHelper.CreateLabel(ColorHelper.TextNormalColor, DimensionHelper.SmallTextSize);

			_profileView.Add(_rankTitleLabel);

			_profileView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_rankTitleLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _userNameLabel,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_rankTitleLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _userNameLabel,
					NSLayoutAttribute.Left, 1,  0)
			});

			_sentCountTitleLabel = UIHelper.CreateLabel(ColorHelper.TextNormalColor, DimensionHelper.SmallTextSize);

			_profileView.Add(_sentCountTitleLabel);

			_profileView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_sentCountTitleLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _rankTitleLabel,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.DefaultMargin),
				NSLayoutConstraint.Create(_sentCountTitleLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _userNameLabel,
					NSLayoutAttribute.Left, 1,  0)
			});

			_userRankLabel = UIHelper.CreateLabel(UIColor.White, DimensionHelper.MediumTextSize);

			_profileView.Add(_userRankLabel);

			_profileView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_userRankLabel, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _rankTitleLabel,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_userRankLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _rankTitleLabel,
					NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal)
			});

			_userSentCountLabel = UIHelper.CreateLabel(UIColor.White, DimensionHelper.MediumTextSize);

			_profileView.Add(_userSentCountLabel);

			_profileView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_userSentCountLabel, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _sentCountTitleLabel,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_userSentCountLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _userRankLabel,
					NSLayoutAttribute.Left, 1, 0)
			});

			_openConversationButton = UIHelper.CreateImageButton(DimensionHelper.PopupButtonHeight,
				DimensionHelper.PopupButtonHeight, ImageHelper.Chat);

			_profileView.Add(_openConversationButton);

			_profileView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_openConversationButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _profileView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.SettingButtonMargin),
				NSLayoutConstraint.Create(_openConversationButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _userNameLabel,
					NSLayoutAttribute.Top, 1, 0)
			});

			_postsTableView = UIHelper.CreateTableView(0, 0);
			_postTableViewSource = new PostItemTableViewSource<MyPostItemViewCell>(_postsTableView)
			{
				LoadMoreEvent = () => LoadMorePostsCommand?.Execute()
			};

			_postsTableView.Source = _postTableViewSource;
			_refreshPostsControl = new MvxUIRefreshControl();
			_postsTableView.RefreshControl = _refreshPostsControl;

			View.Add(_postsTableView);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_postsTableView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _profileView, NSLayoutAttribute.Bottom, 1, DimensionHelper.FilterSize / 2),
				NSLayoutConstraint.Create(_postsTableView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_postsTableView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View, NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_postsTableView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View, NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
			});
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();
			var set = this.CreateBindingSet<UserProfileView, UserProfileViewModel>();

			set.Bind(_avatarView)
				.For(v => v.ImageUrl)
				.To(vm => vm.AvatarUrl);

			set.Bind(_userNameLabel)
				.To(vm => vm.UserName);

			set.Bind(_rankTitleLabel)
				.To(vm => vm.RankTitle);

			set.Bind(_userRankLabel)
				.To(vm => vm.RankType);

			set.Bind(_sentCountTitleLabel)
				.To(vm => vm.SentTitle);

			set.Bind(_userSentCountLabel)
				.To(vm => vm.SentCount);

			set.Bind(_postTableViewSource)
				.To(vm => vm.PostViewModels);

			set.Bind(this)
				.For(v => v.LoadMorePostsCommand)
				.To(vm => vm.LoadMorePostsCommand);

			set.Bind(HeaderBar)
				.For(v => v.BackPressedCommand)
				.To(vm => vm.BackPressedCommand);

			set.Bind(_refreshPostsControl)
				.For(v => v.IsRefreshing)
				.To(vm => vm.IsPostsRefreshing);

			set.Bind(_refreshPostsControl)
				.For(v => v.RefreshCommand)
				.To(vm => vm.RefreshPostsCommand);

			set.Bind(_openConversationButton.Tap())
				.For(v => v.Command)
				.To(vm => vm.OpenConversationCommand);

			set.Apply();
		}
	}
}