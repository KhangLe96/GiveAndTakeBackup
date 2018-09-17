using System.Windows.Input;
using GiveAndTake.Core.ViewModels.Popup;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views.Popups
{
	[MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext)]
	public class PopupReportView : PopupView
	{
		private UIView _popupLine;
		private UILabel _labelReport;

		public override ICommand CloseCommand { get; set; }

		protected override void InitView()
		{
			base.InitView();

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(ContentView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View, NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(ContentView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 1, 0),
				NSLayoutConstraint.Create(ContentView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View, NSLayoutAttribute.Right, 1, 0)
			});

			_labelReport = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.ButtonTextSize);
			_labelReport.Text = "Báo cáo";
			ContentView.Add(_labelReport);
			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_labelReport, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null,
					NSLayoutAttribute.NoAttribute, 1, DimensionHelper.PopupCellHeight),
				NSLayoutConstraint.Create(_labelReport, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_labelReport, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.CenterX, 1, 0)
			});

			_popupLine = UIHelper.CreatePopupLine(DimensionHelper.PopupLineHeight, DimensionHelper.PopupLineWidth, ColorHelper.Blue, DimensionHelper.PopupLineHeight / 2);
			ContentView.Add(_popupLine);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_popupLine, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _labelReport, NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_popupLine, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.CenterX, 1, 0)
			});

			var swipeGesture = new UISwipeGestureRecognizer(() => CloseCommand?.Execute(null))
			{
				Direction = UISwipeGestureRecognizerDirection.Down
			};
			ContentView.AddGestureRecognizer(swipeGesture);

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(ContentView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _popupLine, NSLayoutAttribute.Top, 1, - DimensionHelper.MarginNormal)
			});
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();

			var bindingSet = this.CreateBindingSet<PopupReportView, PopupReportViewModel>();

			bindingSet.Bind(this)
				.For(v => v.CloseCommand)
				.To(vm => vm.CloseCommand);

			bindingSet.Bind(_labelReport.Tap())
				.For(v => v.Command)
				.To(vm => vm.ReportCommand);

			bindingSet.Apply();
		}
	}
}