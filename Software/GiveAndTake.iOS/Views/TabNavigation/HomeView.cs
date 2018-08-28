using GiveAndTake.Core.ViewModels;
using GiveAndTake.Core.ViewModels.TabNavigation;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using GiveAndTake.iOS.Views.TableViewSources;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views.TabNavigation
{
	[MvxTabPresentation(TabIconName = "Images/home_off",
		TabSelectedIconName = "Images/home_on",
		WrapInNavigationController = true)]
	public class HomeView : BaseView
	{
		private UIButton _btnFilter, _btnSort, _btnCategory;
		private UISearchBar _searchBar;
		private UITableView _postsTableView;
		private PostItemTableViewSource _postTableViewSource;

		protected override void InitView()
		{
			InitFilterButton();
			InitSortButton();
			InitCategoryButton();
			InitSearchView();
			InitPostsTableView();
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
					NSLayoutAttribute.Top, 1, ResolutionHelper.StatusHeight + DimensionHelper.HeaderBarHeight),
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
			_postTableViewSource = new PostItemTableViewSource(_postsTableView);
			_postsTableView.Source = _postTableViewSource;
			View.Add(_postsTableView);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_postsTableView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _searchBar, NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_postsTableView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_postsTableView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View, NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_postsTableView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View, NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
			});
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();
			var set = this.CreateBindingSet<HomeView, HomeViewModel>();

			set.Bind(_postTableViewSource)
				.To(vm => vm.PostViewModels);

			set.Apply();
		}
	}
}