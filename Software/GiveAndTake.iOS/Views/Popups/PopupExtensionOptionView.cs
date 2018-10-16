using GiveAndTake.Core.ViewModels.Popup;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.TableViewSources;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views.Popups
{
	[MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext)]
	public class PopupExtensionOptionView : PopupView
	{
		private UIView _popupLine;
		private UITableView _popupTableView;
		private PopupItemTableViewSource _popupItemTableViewSource;
		
		protected override void InitView()
		{
			base.InitView();

			_popupTableView = UIHelper.CreateTableView(0, 0);
			_popupItemTableViewSource = new PopupItemTableViewSource(_popupTableView);
			_popupTableView.Source = _popupItemTableViewSource;

			ContentView.Add(_popupTableView);

			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_popupTableView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Bottom, 1, - DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_popupTableView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_popupTableView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
			});

			_popupLine = UIHelper.CreatePopupLine(DimensionHelper.PopupLineHeight, DimensionHelper.PopupLineWidth, ColorHelper.Blue, DimensionHelper.PopupLineHeight / 2);

			ContentView.Add(_popupLine);

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_popupLine, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _popupTableView, NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_popupLine, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(ContentView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _popupLine, NSLayoutAttribute.Top, 1, - DimensionHelper.MarginNormal)
			});

		}

		public override void UpdateViewConstraints()
		{
			base.UpdateViewConstraints();
			_popupTableView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_popupTableView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 0, _popupTableView.ContentSize.Height)
			});
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();

			var bindingSet = this.CreateBindingSet<PopupExtensionOptionView, PopupExtensionOptionViewModel>();

			bindingSet.Bind(_popupItemTableViewSource)
				.To(vm => vm.PopupItemViewModels);

			bindingSet.Apply();
		}
	}
}