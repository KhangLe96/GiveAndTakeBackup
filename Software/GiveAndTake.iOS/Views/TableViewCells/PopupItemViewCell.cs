using Foundation;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.iOS.Controls;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using System;
using UIKit;

namespace GiveAndTake.iOS.Views.TableViewCells
{
	[Register(nameof(PopupItemViewCell))]
	public class PopupItemViewCell : MvxTableViewCell
	{
		private PopupItemLabel itemLabel;
		private UIView seperator;

		public PopupItemViewCell(IntPtr handle) : base(handle)
		{
			InitViews();
			CreateBinding();
		}

		private void InitViews()
		{
			itemLabel = UIHelper.CreateLabel(ColorHelper.Default, DimensionHelper.ButtonTextSize);
			ContentView.AddSubview(itemLabel);
			ContentView.AddConstraints(new []
			{
				NSLayoutConstraint.Create(itemLabel, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.CenterY, 1,0),
				NSLayoutConstraint.Create(itemLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.CenterX, 1, 0)
			});

			seperator = UIHelper.CreateView(DimensionHelper.SeperatorHeight, 0, ColorHelper.BlueColor);
			ContentView.AddSubview(seperator);
			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(seperator, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(seperator, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(seperator, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
			});
		}

		private void CreateBinding()
		{
			var bindingSet = this.CreateBindingSet<PopupItemViewCell, PopupItemViewModel>();

			bindingSet.Bind(itemLabel).To(vm => vm.ItemName);

			bindingSet.Bind(itemLabel).For(v => v.IsSelected).To(vm => vm.IsSelected);

			//bindingSet.Bind(seperator).For(v => v.BindVisibility()).To(vm => !vm.IsLastViewInList);
			
			bindingSet.Apply();
		}
	}
}