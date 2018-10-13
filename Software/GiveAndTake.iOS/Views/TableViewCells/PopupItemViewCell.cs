using Foundation;
using GiveAndTake.Core.ViewModels.Popup;
using GiveAndTake.iOS.Controls;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using System;
using UIKit;

namespace GiveAndTake.iOS.Views.TableViewCells
{
	[Register(nameof(PopupItemViewCell))]
	public class PopupItemViewCell : MvxTableViewCell
	{
		private PopupItemLabel _itemLabel;
		private UIView _separatorLine;

		public PopupItemViewCell(IntPtr handle) : base(handle)
		{
			InitViews();
			CreateBinding();
		}

		private void InitViews()
		{
			BackgroundColor = UIColor.White;

			_itemLabel = UIHelper.CreateLabel(ColorHelper.Default, DimensionHelper.ButtonTextSize);
			AddSubview(_itemLabel);
			AddConstraints(new []
			{
				NSLayoutConstraint.Create(_itemLabel, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.CenterY, 1,0),
				NSLayoutConstraint.Create(_itemLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.CenterX, 1, 0)
			});

			_separatorLine = UIHelper.CreateView(DimensionHelper.SeperatorHeight, 0, ColorHelper.PopupSeparator);
			AddSubview(_separatorLine);
			AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_separatorLine, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_separatorLine, NSLayoutAttribute.Left, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_separatorLine, NSLayoutAttribute.Right, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
			});
		}

		private void CreateBinding()
		{
			var bindingSet = this.CreateBindingSet<PopupItemViewCell, PopupItemViewModel>();

			bindingSet.Bind(_itemLabel)
				.To(vm => vm.ItemName);

			bindingSet.Bind(_itemLabel)
				.For(v => v.IsSelected)
				.To(vm => vm.IsSelected);

			bindingSet.Bind(_separatorLine)
				.For("Visibility")
				.To(vm => vm.IsSeparatorLineShown)
				.WithConversion("InvertBool");

			bindingSet.Bind(this.Tap())
				.For(v => v.Command)
				.To(vm => vm.ClickCommand);

			bindingSet.Apply();
		}
	}
}