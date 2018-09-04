using Foundation;
using GiveAndTake.Core.ViewModels;
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
			AddSubview(itemLabel);
			AddConstraints(new []
			{
				NSLayoutConstraint.Create(itemLabel, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.CenterY, 1,0),
				NSLayoutConstraint.Create(itemLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.CenterX, 1, 0)
			});

			seperator = UIHelper.CreateView(DimensionHelper.SeperatorHeight, 0, ColorHelper.BlueColor);
			AddSubview(seperator);
			AddConstraints(new[]
			{
				NSLayoutConstraint.Create(seperator, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(seperator, NSLayoutAttribute.Left, NSLayoutRelation.Equal, this,
					NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(seperator, NSLayoutAttribute.Right, NSLayoutRelation.Equal, this,
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

			bindingSet.Bind(itemLabel).To(vm => vm.ItemName);

			bindingSet.Bind(itemLabel).For(v => v.IsSelected).To(vm => vm.IsSelected).WithConversion("InvertBoolValue");

			bindingSet.Bind(this)
				.For(v => v.ClickCommand)
				.To(vm => vm.ClickCommand);
			
			bindingSet.Apply();
		}
	}
}