using GiveAndTake.Core;
using GiveAndTake.Core.ViewModels;
using GiveAndTake.Core.ViewModels.Popup;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using GiveAndTake.iOS.Views.TableViewSources;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views.Popups
{
	[MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext, ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
	public class PopupView : BaseView
	{
		private UIView popupLine;
		private UIButton btnClose;
		private UITableView popupTableView;
		private PopupItemTableViewSource popupItemTableViewSource;
		private UILabel titleLabel;
		private UIView background;

		protected override void InitView()
		{
			View.BackgroundColor = UIColor.Clear;
			var container = UIHelper.CreateView(0, 0, UIColor.White);
			View.Add(container);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(container, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View, NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(container, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 1, 0),
				NSLayoutConstraint.Create(container, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View, NSLayoutAttribute.Right, 1, 0)
			});

			btnClose = UIHelper.CreateButton(DimensionHelper.PopupButtonHeight, 
				DimensionHelper.PopupButtonWidth,
				ColorHelper.BlueColor, 
				UIColor.White, 
				DimensionHelper.ButtonTextSize, "Xong",
				DimensionHelper.PopupButtonHeight / 2);

			container.Add(btnClose);
			container.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(btnClose, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, container, NSLayoutAttribute.Bottom, 1, -DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(btnClose, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, container, NSLayoutAttribute.CenterX, 1, 0)
			});

			popupTableView = UIHelper.CreateTableView(0, 0);
			popupItemTableViewSource = new PopupItemTableViewSource(popupTableView);
			popupTableView.Source = popupItemTableViewSource;
			container.Add(popupTableView);
			container.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(popupTableView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, btnClose, NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(popupTableView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, container, NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(popupTableView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, container, NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
			});

			titleLabel = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.MediumTextSize, FontType.Bold);
			container.Add(titleLabel);
			container.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(titleLabel, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, popupTableView, NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(titleLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, container, NSLayoutAttribute.CenterX, 1, 0)
			});

			popupLine = UIHelper.CreatePopupLine(DimensionHelper.PopupLineHeight, DimensionHelper.PopupLineWidth, ColorHelper.BlueColor, DimensionHelper.PopupLineHeight / 2);
			container.Add(popupLine);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(popupLine, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, titleLabel, NSLayoutAttribute.Top, 1, -DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(popupLine, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, container, NSLayoutAttribute.CenterX, 1, 0)
			});

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(container, NSLayoutAttribute.Top, NSLayoutRelation.Equal, popupLine, NSLayoutAttribute.Top, 1, - DimensionHelper.MarginNormal)
			});

			background = UIHelper.CreateView(0, 0, UIColor.Black.ColorWithAlpha(0.7f));
			View.Add(background);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(background, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View, NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(background, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, container, NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(background, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 1, 0),
				NSLayoutConstraint.Create(background, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View, NSLayoutAttribute.Right, 1, 0)
			});
		}

		public override void UpdateViewConstraints()
		{
			base.UpdateViewConstraints();
			popupTableView.AddConstraints(new []
			{
				NSLayoutConstraint.Create(popupTableView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 0, popupTableView.ContentSize.Height)
			});
		}

		protected override void CreateBinding()
		{
			var bindingSet = this.CreateBindingSet<PopupView, PopupViewModel>();
			
			bindingSet.Bind(titleLabel)
				.To(vm => vm.Title);

			bindingSet.Bind(btnClose.Tap())
				.For(v => v.Command)
				.To(vm => vm.ClosePopupCommand);

			bindingSet.Bind(popupItemTableViewSource)
				.To(vm => vm.PopupItemViewModels);

			bindingSet.Bind(background.Tap())
				.For(v => v.Command)
				.To(vm => vm.ClosePopupCommand);

			bindingSet.Apply();
		}
	}

	public class PopupCategoriesView : PopupView
	{
	}

	public class PopupLocationFilterView : PopupView
	{
	}

	public class PopupShortFilterView : PopupView
	{
	}
}