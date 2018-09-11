﻿using GiveAndTake.Core.ViewModels.TabNavigation;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using GiveAndTake.iOS.Views.TableViewSources;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace GiveAndTake.iOS.Views.TabNavigation
{
	[MvxTabPresentation(TabIconName = "Images/home_off",
		TabSelectedIconName = "Images/home_on",
		WrapInNavigationController = true)]
	public class HomeView : BaseView
	{
		private UIButton _btnFilter;
		private UIButton _btnSort;
		private UIButton _btnCategory;
		private UISearchBar _searchBar;
		private UITableView _postsTableView;
		private PostItemTableViewSource _postTableViewSource;
		private MvxUIRefreshControl _refreshControl;
		private UIButton _newPostButton;

		public IMvxCommand LoadMoreCommand { get; set; }

		protected override void InitView()
		{
			InitFilterButton();
			InitSortButton();
			InitCategoryButton();
			InitSearchView();
			InitPostsTableView();
			InitNewPostButton();
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			NavigationController?.SetNavigationBarHidden(true, animated);
		}

		private void InitFilterButton()
		{
			_btnFilter = UIHelper.CreateImageButton(DimensionHelper.FilterSize, DimensionHelper.FilterSize, "Images/filter_button");

			View.Add(_btnFilter);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnFilter, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Top, 1, ResolutionHelper.StatusHeight + DimensionHelper.HeaderBarHeight + DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_btnFilter, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Right, 1, -DimensionHelper.MarginNormal)
			});
		}

		private void InitSortButton()
		{
			_btnSort = UIHelper.CreateImageButton(DimensionHelper.FilterSize, DimensionHelper.FilterSize, "Images/sort_button");

			View.Add(_btnSort);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnSort, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _btnFilter,
					NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_btnSort, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _btnFilter,
					NSLayoutAttribute.Left, 1, -DimensionHelper.MarginNormal)
			});
		}

		private void InitCategoryButton()
		{
			_btnCategory = UIHelper.CreateImageButton(DimensionHelper.FilterSize, DimensionHelper.FilterSize, "Images/category_button");
			View.Add(_btnCategory);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnCategory, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _btnFilter,
					NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_btnCategory, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _btnSort,
					NSLayoutAttribute.Left, 1, -DimensionHelper.MarginNormal)
			});
		}

		private void InitSearchView()
		{
			_searchBar = UIHelper.CreateSearchBar(DimensionHelper.FilterSize, DimensionHelper.FilterSize);
			View.Add(_searchBar);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_searchBar, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _btnFilter,
					NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_searchBar, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _btnCategory,
					NSLayoutAttribute.Left, 1, -DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(_searchBar, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
				NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort)
			});
		}

		private void InitPostsTableView()
		{
			_postsTableView = UIHelper.CreateTableView(0, 0);
			_postTableViewSource = new PostItemTableViewSource(_postsTableView)
			{
				LoadMoreEvent = () => LoadMoreCommand?.Execute()
			};

			_postsTableView.Source = _postTableViewSource;
			_refreshControl = new MvxUIRefreshControl();
			_postsTableView.RefreshControl = _refreshControl;

			View.Add(_postsTableView);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_postsTableView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _searchBar, NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_postsTableView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_postsTableView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View, NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_postsTableView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View, NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
			});
		}

		private void InitNewPostButton()
		{
			_newPostButton = UIHelper.CreateImageButton(DimensionHelper.NewPostSize, DimensionHelper.NewPostSize, ImageHelper.NewPost);

			View.Add(_newPostButton);

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_newPostButton, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Bottom, 1, -DimensionHelper.MarginNormal - NavigationController.NavigationBar.Frame.Size.Height),
				NSLayoutConstraint.Create(_newPostButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Right, 1, -DimensionHelper.MarginNormal)
			});
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();
			var set = this.CreateBindingSet<HomeView, HomeViewModel>();

			set.Bind(_postTableViewSource)
				.To(vm => vm.PostViewModels);

			set.Bind(this)
				.For(v => v.LoadMoreCommand)
				.To(vm => vm.LoadMoreCommand);

			set.Bind(_refreshControl)
				.For(v => v.IsRefreshing)
				.To(vm => vm.IsRefreshing);

			set.Bind(_refreshControl)
				.For(v => v.RefreshCommand)
				.To(vm => vm.RefreshCommand);

			set.Bind(_btnCategory.Tap())
				.For(v => v.Command)
				.To(vm => vm.ShowCategoriesCommand);

			set.Bind(_btnFilter.Tap())
				.For(v => v.Command)
				.To(vm => vm.ShowFilterCommand);

			set.Bind(_btnSort.Tap())
				.For(v => v.Command)
				.To(vm => vm.ShowShortPostCommand);

			set.Bind(_newPostButton.Tap())
				.For(v => v.Command)
				.To(vm => vm.CreatePostCommand);

			set.Apply();
		}
	}
}