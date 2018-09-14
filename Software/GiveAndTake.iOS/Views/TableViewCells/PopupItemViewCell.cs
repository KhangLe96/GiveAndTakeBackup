using Foundation;
using GiveAndTake.Core.ViewModels.Popup;
using GiveAndTake.iOS.Controls;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using System;
using System.Windows.Input;
using UIKit;

namespace GiveAndTake.iOS.Views.TableViewCells
{
	[Register(nameof(PopupItemViewCell))]
	public class PopupItemViewCell : MvxTableViewCell
	{
		public ICommand ClickCommand { get;set; }
		private PopupItemLabel _itemLabel;
		private UIView _seperatorLine;

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

			_seperatorLine = UIHelper.CreateView(DimensionHelper.SeperatorHeight, 0, ColorHelper.Blue);
			AddSubview(_seperatorLine);
			AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_seperatorLine, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_seperatorLine, NSLayoutAttribute.Left, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_seperatorLine, NSLayoutAttribute.Right, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
			});

			AddGestureRecognizer(new UITapGestureRecognizer(() =>
			{
				ClickCommand?.Execute(null);
			}));
		}

		private void CreateBinding()
		{
			var bindingSet = this.CreateBindingSet<PopupItemViewCell, PopupItemViewModel>();

			bindingSet.Bind(_itemLabel)
				.To(vm => vm.ItemName);

			bindingSet.Bind(_itemLabel)
				.For(v => v.IsSelected)
				.To(vm => vm.IsSelected);

			bindingSet.Bind(_seperatorLine)
				.For("Visibility")
				.To(vm => vm.IsLastViewInList);

			bindingSet.Bind(this)
				.For(v => v.ClickCommand)
				.To(vm => vm.ClickCommand);
			
			bindingSet.Apply();
		}
	}
}