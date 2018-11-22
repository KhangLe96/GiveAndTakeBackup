using System;
using CoreAnimation;
using Facebook.LoginKit;
using GiveAndTake.Core;
using GiveAndTake.Core.ViewModels.TabNavigation;
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
using MvvmCross.ViewModels;
using UIKit;

namespace GiveAndTake.iOS.Views.TabNavigation
{
	[MvxTabPresentation(TabIconName = "Images/avt_off",
		TabSelectedIconName = "Images/avt_on",
		WrapInNavigationController = true)]
	public class ProfileView : BaseView
	{
		public IMvxCommand LoadMorePostsCommand { get; set; }
		public IMvxCommand LoadMoreRequestedPostsCommand { get; set; }

		public IMvxInteraction LogoutFacebook
		{
			get => _logoutFacebook;
			set
			{
				if (_logoutFacebook != null)
					_logoutFacebook.Requested -= OnLogoutFacebook;

				_logoutFacebook = value;
				_logoutFacebook.Requested += OnLogoutFacebook;
			}
		}

		private UIView _profileView;
		private CustomMvxCachedImageView _avatarView;
		private UILabel _userNameLabel;
		private UILabel _rankTitleLabel;
		private UILabel _sentCountTitleLabel;
		private UILabel _userRankLabel;
		private UILabel _userSentCountLabel;
		private UIButton _profileSettingButton;
		private UITableView _postsTableView;
		private PostItemTableViewSource<MyPostItemViewCell> _postTableViewSource;
		private MvxUIRefreshControl _refreshPostsControl;
		private UITableView _requestedPostsTableView;
		private PostItemTableViewSource<PostItemViewCell> _requestedPostsTableViewSource;
		private MvxUIRefreshControl _refreshRequestedPostsControl;
		private CustomUIButton _myPostsButton;
		private CustomUIButton _myRequestedPostsButton;
		private IMvxInteraction _logoutFacebook;

		protected override void InitView()
		{
			_profileView = UIHelper.CreateView(DimensionHelper.ProfileViewHeight, ResolutionHelper.Width, ColorHelper.Line);

			View.Add(_profileView);

			View.AddConstraints(new []
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

			_profileSettingButton = UIHelper.CreateImageButton(DimensionHelper.PopupButtonHeight,
				DimensionHelper.PopupButtonHeight, ImageHelper.Setting);

			_profileView.Add(_profileSettingButton);

			_profileView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_profileSettingButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _profileView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.SettingButtonMargin),
				NSLayoutConstraint.Create(_profileSettingButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _userNameLabel,
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

			_requestedPostsTableView = UIHelper.CreateTableView(0, 0);
			_requestedPostsTableViewSource = new PostItemTableViewSource<PostItemViewCell>(_requestedPostsTableView)
			{
				LoadMoreEvent = () => LoadMoreRequestedPostsCommand?.Execute()
			};

			_requestedPostsTableView.Source = _requestedPostsTableViewSource;
			_refreshRequestedPostsControl = new MvxUIRefreshControl();
			_requestedPostsTableView.RefreshControl = _refreshRequestedPostsControl;

			View.Add(_requestedPostsTableView);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_requestedPostsTableView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _profileView, NSLayoutAttribute.Bottom, 1, DimensionHelper.FilterSize / 2),
				NSLayoutConstraint.Create(_requestedPostsTableView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_requestedPostsTableView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View, NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_requestedPostsTableView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View, NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
			});

			_myPostsButton = UIHelper.CreateButton(DimensionHelper.FilterSize, 0, DimensionHelper.MediumTextSize, ColorHelper.ColorPrimary, DimensionHelper.BorderWidth)
				.SetRoundedCorners((int)DimensionHelper.FilterSize / 2, CACornerMask.MinXMaxYCorner | CACornerMask.MinXMinYCorner);
				
			View.Add(_myPostsButton);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_myPostsButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _profileView, NSLayoutAttribute.Bottom, 1, - DimensionHelper.FilterSize / 2),
				NSLayoutConstraint.Create(_myPostsButton, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_myPostsButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterX, 1, 0)
			});

			_myRequestedPostsButton = UIHelper.CreateButton(DimensionHelper.FilterSize, 0, DimensionHelper.MediumTextSize, ColorHelper.ColorPrimary, DimensionHelper.BorderWidth)
				.SetRoundedCorners((int)DimensionHelper.FilterSize / 2, CACornerMask.MaxXMinYCorner | CACornerMask.MaxXMaxYCorner);

			View.Add(_myRequestedPostsButton);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_myRequestedPostsButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _profileView, NSLayoutAttribute.Bottom, 1, - DimensionHelper.FilterSize / 2),
				NSLayoutConstraint.Create(_myRequestedPostsButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View, NSLayoutAttribute.Right, 1, -DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_myRequestedPostsButton, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterX, 1, 0)
			});

		}

		protected override void CreateBinding()
		{
			base.CreateBinding();
			var set = this.CreateBindingSet<ProfileView, ProfileViewModel>();

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

			set.Bind(_refreshPostsControl)
				.For(v => v.IsRefreshing)
				.To(vm => vm.IsPostsRefreshing);

			set.Bind(_refreshPostsControl)
				.For(v => v.RefreshCommand)
				.To(vm => vm.RefreshPostsCommand);

			set.Bind(_postsTableView)
				.For("Visibility")
				.To(vm => vm.IsPostsList)
				.WithConversion("InvertBool");

			set.Bind(_requestedPostsTableViewSource)
				.To(vm => vm.RequestedPostViewModels);

			set.Bind(this)
				.For(v => v.LoadMoreRequestedPostsCommand)
				.To(vm => vm.LoadMoreRequestedPostsCommand);

			set.Bind(_refreshRequestedPostsControl)
				.For(v => v.IsRefreshing)
				.To(vm => vm.IsRequestedPostsRefreshing);

			set.Bind(_refreshRequestedPostsControl)
				.For(v => v.RefreshCommand)
				.To(vm => vm.RefreshRequestedPostsCommand);

			set.Bind(_requestedPostsTableView)
				.For("Visibility")
				.To(vm => vm.IsPostsList);

			set.Bind(_myPostsButton)
				.For("Title")
				.To(vm => vm.LeftButtonTitle);

			set.Bind(_myPostsButton.Tap())
				.For(v => v.Command)
				.To(vm => vm.ShowMyPostsCommand);

			set.Bind(_myPostsButton)
				.For(v => v.Activated)
				.To(vm => vm.IsPostsList);

			set.Bind(_myRequestedPostsButton)
				.For("Title")
				.To(vm => vm.RightButtonTitle);

			set.Bind(_myRequestedPostsButton.Tap())
				.For(v => v.Command)
				.To(vm => vm.ShowMyRequestsCommand);

			set.Bind(_myRequestedPostsButton)
				.For(v => v.Activated)
				.To(vm => vm.IsPostsList)
				.WithConversion("InvertBool");

			set.Bind(_profileSettingButton.Tap())
				.For(v => v.Command)
				.To(vm => vm.ShowMenuPopupCommand);

			set.Bind(this)
				.For(view => view.LogoutFacebook)
				.To(viewModel => viewModel.LogoutFacebook)
				.OneWay();


			set.Apply();
		}

		private void OnLogoutFacebook(object sender, EventArgs e)
		{
			new LoginManager().LogOut();
		}
	}
}