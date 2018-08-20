using Foundation;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using GiveAndTake.iOS.Views.TableViewCells;
using GiveAndTake.iOS.Views.TableViewSources;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views
{
    [MvxRootPresentation]
    public class HomeView : BaseView
    {
        private UIButton btnFilter, btnSort, btnCategory;
        private UISearchBar searchBar;
        private UITableView postsTableView;
        private PostItemTableViewSource _postTableViewSource;

        protected override void InitView()
        {
            InitFilterButton();
            InitSortButton();
            InitCategoryButton();
            InitSearchView();
            InitPostsTableView();
        }

        private void InitFilterButton()
        {
            btnFilter = UIHelper.CreateImageButton(DimensionHelper.FilterSize, DimensionHelper.FilterSize, "Images/filter_button");

            View.Add(btnFilter);
            View.AddConstraints(new []
            {
                NSLayoutConstraint.Create(btnFilter, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.Top, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(btnFilter, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.Right, 1, -DimensionHelper.MarginNormal)
            });
        }

        private void InitSortButton()
        {
            btnSort = UIHelper.CreateImageButton(DimensionHelper.FilterSize, DimensionHelper.FilterSize, "Images/sort_button");

            View.Add(btnSort);
            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(btnSort, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.Top, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(btnSort, NSLayoutAttribute.Right, NSLayoutRelation.Equal, btnFilter,
                    NSLayoutAttribute.Left, 1, -DimensionHelper.MarginNormal)
            });
        }

        private void InitCategoryButton()
        {
            btnCategory = UIHelper.CreateImageButton(DimensionHelper.FilterSize, DimensionHelper.FilterSize, "Images/category_button");
            View.Add(btnCategory);
            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(btnCategory, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.Top, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(btnCategory, NSLayoutAttribute.Right, NSLayoutRelation.Equal, btnSort,
                    NSLayoutAttribute.Left, 1, -DimensionHelper.MarginNormal)
            });
        }

        private void InitSearchView()
        {
            searchBar = UIHelper.CreateSearchBar(DimensionHelper.FilterSize, DimensionHelper.FilterSize);
            View.Add(searchBar);
            View.AddConstraints(new[]
            {
                NSLayoutConstraint.Create(searchBar, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
                    NSLayoutAttribute.Top, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(searchBar, NSLayoutAttribute.Right, NSLayoutRelation.Equal, btnCategory,
                    NSLayoutAttribute.Left, 1, -DimensionHelper.MarginNormal),
                NSLayoutConstraint.Create(searchBar, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
                NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort)
            });
        }

        private void InitPostsTableView()
        {
            postsTableView = UIHelper.CreateTableView(0, 0);
            _postTableViewSource = new PostItemTableViewSource(postsTableView);
            postsTableView.Source = _postTableViewSource;
            View.Add(postsTableView);
            View.AddConstraints(new []
            {
                NSLayoutConstraint.Create(postsTableView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, searchBar, NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(postsTableView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(postsTableView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View, NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(postsTableView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View, NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
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