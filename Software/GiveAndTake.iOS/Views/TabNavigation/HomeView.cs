using CoreAnimation;
using GiveAndTake.Core.ViewModels.TabNavigation;
using GiveAndTake.iOS.Controls;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using GiveAndTake.iOS.Views.TableViewSources;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;

namespace GiveAndTake.iOS.Views.TabNavigation
{
	[MvxTabPresentation(TabIconName = "Images/home_off",
		TabSelectedIconName = "Images/home_on",
		WrapInNavigationController = true)]
	public class HomeView : BaseView
	{
		public IMvxCommand LoadMoreCommand { get; set; }

		public IMvxCommand SearchCommand { get; set; }

		private const int TopColorAlpha = 0;
		private const int VerticalSublayer = 0;
		private const float BottomColorAlpha = 0.8f;

		private UIButton _btnFilter;
		private UIButton _btnSort;
		private UIButton _btnCategory;
		private UISearchBar _searchBar;
	    private UIView _separatorLine;
        private UITableView _postsTableView;
		private PostItemTableViewSource _postTableViewSource;
		private MvxUIRefreshControl _refreshControl;
		private UIButton _newPostButton;
		private PopupItemLabel _searchResult;
		private UIView _gradientView;

		protected override void InitView()
		{
			View.AddGestureRecognizer(new UITapGestureRecognizer(HideKeyboard)
			{
				CancelsTouchesInView = false
			});

			InitFilterBar();
            InitPostsTableView();
			InitNewPostButton();
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			var whiteGradient = new CAGradientLayer
			{
				Frame = _gradientView.Bounds,

				Colors = new[]
				{
					//REVIEW : Don't need to set ColorWithAlpha(0). ColorWithAlpha(0) is UIColor.Transparent
					UIColor.White.ColorWithAlpha(TopColorAlpha).CGColor,
					UIColor.White.ColorWithAlpha(BottomColorAlpha).CGColor
				}
			};
			_gradientView.Layer.InsertSublayer(whiteGradient, VerticalSublayer);
		}

		private void InitFilterBar()
		{
			_btnFilter = UIHelper.CreateImageButton(DimensionHelper.FilterSize, DimensionHelper.FilterSize, ImageHelper.LocationButtonDefault, ImageHelper.LocationButtonSelected);

			View.Add(_btnFilter);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnFilter, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Top, 1, ResolutionHelper.StatusHeight + DimensionHelper.HeaderBarHeight + DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_btnFilter, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Right, 1, -DimensionHelper.MarginNormal)
			});

			_btnSort = UIHelper.CreateImageButton(DimensionHelper.FilterSize, DimensionHelper.FilterSize, ImageHelper.SortButtonDefault, ImageHelper.SortButtonSelected);

			View.Add(_btnSort);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnSort, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _btnFilter,
					NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_btnSort, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _btnFilter,
					NSLayoutAttribute.Left, 1, -DimensionHelper.MarginNormal)
			});

			_btnCategory = UIHelper.CreateImageButton(DimensionHelper.FilterSize, DimensionHelper.FilterSize, ImageHelper.CategoryButtonDefault, ImageHelper.CategoryButtonSelected);
			View.Add(_btnCategory);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnCategory, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _btnFilter,
					NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_btnCategory, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _btnSort,
					NSLayoutAttribute.Left, 1, -DimensionHelper.MarginNormal)
			});

			_searchBar = UIHelper.CreateSearchBar(DimensionHelper.FilterSize, DimensionHelper.FilterSize);
			_searchBar.SearchButtonClicked += OnSearchSubmit;
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
			

			_separatorLine = UIHelper.CreateView(DimensionHelper.MenuSeparatorLineHeight, DimensionHelper.HeaderBarLogoWidth, ColorHelper.GreyLineColor);
			View.Add(_separatorLine);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_separatorLine, NSLayoutAttribute.Width, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Width, 1, 0),
				NSLayoutConstraint.Create(_separatorLine, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _searchBar,
					NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginShort)
			});
		}

		protected override void Dispose(bool disposing)
		{
			_searchBar.SearchButtonClicked -= OnSearchSubmit;
			base.Dispose(disposing);
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
				NSLayoutConstraint.Create(_postsTableView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _separatorLine, NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_postsTableView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_postsTableView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View, NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_postsTableView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View, NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
			});

			_searchResult = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.BigTextSize);
			View.Add(_searchResult);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_searchResult, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_searchResult, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.CenterY, 1, 0)
			});

			_gradientView = UIHelper.CreateView(DimensionHelper.PostCellHeight, ResolutionHelper.Width);
			_gradientView.UserInteractionEnabled = false;

			View.Add(_gradientView);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_gradientView, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_gradientView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Bottom, 1, 0)
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
				.To(vm => vm.PostItemViewModelCollection);

			set.Bind(_searchBar)
				.For(v => v.Text)
				.To(vm => vm.CurrentQueryString);

			set.Bind(this)
				.For(v => v.LoadMoreCommand)
				.To(vm => vm.LoadMoreCommand);

			set.Bind(this)
				.For(v => v.SearchCommand)
				.To(vm => vm.SearchCommand);

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
				.To(vm => vm.ShowLocationFiltersCommand);

			set.Bind(_btnSort.Tap())
				.For(v => v.Command)
				.To(vm => vm.ShowSortFiltersCommand);

			set.Bind(_newPostButton.Tap())
				.For(v => v.Command)
				.To(vm => vm.CreatePostCommand);

			set.Bind(_btnCategory)
				.For(v => v.Selected)
				.To(vm => vm.IsCategoryFilterActivated);

			set.Bind(_btnFilter)
				.For(v => v.Selected)
				.To(vm => vm.IsLocationFilterActivated);

			set.Bind(_btnSort)
				.For(v => v.Selected)
				.To(vm => vm.IsSortFilterActivated);

			set.Bind(_searchResult)
				.For(v => v.Text)
				.To(vm => vm.SearchResultTitle);

			set.Bind(_searchResult)
				.For("Visibility")
				.To(vm => vm.IsSearchResultNull);

			set.Apply();
		}

		private void HideKeyboard() => View.EndEditing(true);

		private void OnSearchSubmit(object sender, EventArgs e)
		{
			_searchBar.ResignFirstResponder();
			SearchCommand.Execute();
		}
	}
}