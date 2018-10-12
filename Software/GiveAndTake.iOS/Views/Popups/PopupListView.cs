using System.Windows.Input;
using GiveAndTake.Core;
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
	[MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext, ModalTransitionStyle = UIModalTransitionStyle.CoverVertical)]
	public class PopupListView : BaseView
	{
		private UIView _popupLine;
		private UIButton _btnClose;
		private UITableView _popupTableView;
		private PopupItemTableViewSource _popupItemTableViewSource;
		private UILabel _titleLabel;
		private UIView _background;
		public ICommand CloseCommand { get; set; }

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

			_btnClose = UIHelper.CreateButton(DimensionHelper.PopupButtonHeight, 
				DimensionHelper.PopupButtonWidth,
				ColorHelper.Blue, 
				UIColor.White, 
				DimensionHelper.ButtonTextSize, 
				DimensionHelper.PopupButtonHeight / 2);

			container.Add(_btnClose);
			container.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_btnClose, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, container, NSLayoutAttribute.Bottom, 1, -DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(_btnClose, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, container, NSLayoutAttribute.CenterX, 1, 0)
			});

			_popupTableView = UIHelper.CreateTableView(0, 0);
			_popupItemTableViewSource = new PopupItemTableViewSource(_popupTableView);
			_popupTableView.Source = _popupItemTableViewSource;
			container.Add(_popupTableView);
			container.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_popupTableView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _btnClose, NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_popupTableView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, container, NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_popupTableView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, container, NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
			});

			_titleLabel = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.MediumTextSize, FontType.Bold);
			container.Add(_titleLabel);
			container.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_titleLabel, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _popupTableView, NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_titleLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, container, NSLayoutAttribute.CenterX, 1, 0)
			});

			_popupLine = UIHelper.CreatePopupLine(DimensionHelper.PopupLineHeight, DimensionHelper.PopupLineWidth, ColorHelper.Blue, DimensionHelper.PopupLineHeight / 2);
			container.Add(_popupLine);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_popupLine, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _titleLabel, NSLayoutAttribute.Top, 1, -DimensionHelper.MarginNormal),
				NSLayoutConstraint.Create(_popupLine, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, container, NSLayoutAttribute.CenterX, 1, 0)
			});

			var swipeGesture = new UISwipeGestureRecognizer(() => CloseCommand?.Execute(null))
			{
				Direction = UISwipeGestureRecognizerDirection.Down
			};
			container.AddGestureRecognizer(swipeGesture);

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(container, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _popupLine, NSLayoutAttribute.Top, 1, - DimensionHelper.MarginNormal)
			});

			_background = UIHelper.CreateView(0, 0, UIColor.Clear);
			View.Add(_background);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_background, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View, NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_background, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, container, NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_background, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 1, 0),
				NSLayoutConstraint.Create(_background, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View, NSLayoutAttribute.Right, 1, 0)
			});
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			_background.BackgroundColor = UIColor.Black.ColorWithAlpha(0.7f);
		}

		public override void ViewWillUnload()
		{
			base.ViewWillUnload();
			_background.BackgroundColor = UIColor.Clear;
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			_background.BackgroundColor = UIColor.Clear;
		}

		public override void UpdateViewConstraints()
		{
			base.UpdateViewConstraints();
			_popupTableView.AddConstraints(new []
			{
				NSLayoutConstraint.Create(_popupTableView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 0, _popupTableView.ContentSize.Height)
			});
		}

		protected override void CreateBinding()
		{
			var bindingSet = this.CreateBindingSet<PopupListView, PopupListViewModel>();
			
			bindingSet.Bind(_titleLabel)
				.To(vm => vm.Title);

			bindingSet.Bind(_btnClose.Tap())
				.For(v => v.Command)
				.To(vm => vm.SubmitCommand);

			bindingSet.Bind(_popupItemTableViewSource)
				.To(vm => vm.PopupItemViewModels);

			bindingSet.Bind(_background.Tap())
				.For(v => v.Command)
				.To(vm => vm.CloseCommand);

			bindingSet.Bind(_btnClose)
				.For("Title")
				.To(vm => vm.SubmitButtonTitle);

			bindingSet.Bind(this)
				.For(v => v.CloseCommand)
				.To(vm => vm.CloseCommand);

			bindingSet.Apply();
		}
	}
}