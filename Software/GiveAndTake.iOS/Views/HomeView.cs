using GiveAndTake.Core;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views
{
    [MvxRootPresentation]
    public class HomeView : BaseView
    {
        private UIButton btnFilter, btnSort, btnCategory;
        private UISearchBar searchBar;

        protected override void InitView()
        {
            InitFilterButton();
            InitSortButton();
            InitCategoryButton();
            InitSearchView();
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
                    NSLayoutAttribute.Right, 1, -DimensionHelper.MarginShort)
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
                    NSLayoutAttribute.Left, 1, -DimensionHelper.MarginShort)
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
                    NSLayoutAttribute.Left, 1, -DimensionHelper.MarginShort)
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
                    NSLayoutAttribute.Left, 1, -DimensionHelper.MarginShort),
                NSLayoutConstraint.Create(searchBar, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
                NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort)
            });
        }
    }
}