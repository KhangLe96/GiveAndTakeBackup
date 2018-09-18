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
	public class PopupPostOptionView : PopupView
	{
		private UIView _popupLine;
		private UILabel _labelDelete;
		private UILabel _labelRequests;
		private UILabel _labelModify;
		private UILabel _labelStatus;
		private UIView _separatorLine1;
		private UIView _separatorLine2;
		private UIView _separatorLine3;

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

			_labelDelete = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.ButtonTextSize);
			_labelDelete.Text = "Xóa";
			ContentView.Add(_labelDelete);
			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_labelDelete, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null,
					NSLayoutAttribute.NoAttribute, 1, DimensionHelper.PopupCellHeight),
				NSLayoutConstraint.Create(_labelDelete, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_labelDelete, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.CenterX, 1, 0)
			});

			_separatorLine1 = UIHelper.CreateView(DimensionHelper.SeperatorHeight, 0, ColorHelper.GreyLineColor);
			ContentView.Add(_separatorLine1);
			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_separatorLine1, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _labelDelete,
					NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_separatorLine1, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_separatorLine1, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
			});

			_labelRequests = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.ButtonTextSize);
			_labelRequests.Text = "Duyệt cho";
			ContentView.Add(_labelRequests);
			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_labelRequests, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null,
					NSLayoutAttribute.NoAttribute, 1, DimensionHelper.PopupCellHeight),
				NSLayoutConstraint.Create(_labelRequests, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _separatorLine1,
					NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_labelRequests, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.CenterX, 1, 0)
			});

			_separatorLine2 = UIHelper.CreateView(DimensionHelper.SeperatorHeight, 0, ColorHelper.GreyLineColor);
			ContentView.Add(_separatorLine2);
			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_separatorLine2, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _labelRequests,
					NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_separatorLine2, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_separatorLine2, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
			});

			_labelModify = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.ButtonTextSize);
			_labelModify.Text = "Chỉnh sửa";
			ContentView.Add(_labelModify);
			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_labelModify, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null,
					NSLayoutAttribute.NoAttribute, 1, DimensionHelper.PopupCellHeight),
				NSLayoutConstraint.Create(_labelModify, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _separatorLine2,
					NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_labelModify, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.CenterX, 1, 0)
			});

			_separatorLine3 = UIHelper.CreateView(DimensionHelper.SeperatorHeight, 0, ColorHelper.GreyLineColor);
			ContentView.Add(_separatorLine3);
			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_separatorLine3, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _labelModify,
					NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_separatorLine3, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_separatorLine3, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
			});

			_labelStatus = UIHelper.CreateLabel(UIColor.Black, DimensionHelper.ButtonTextSize);
			_labelStatus.Text = "Chuyển sang trạng thái \"Đã cho\"";
			ContentView.Add(_labelStatus);
			ContentView.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_labelStatus, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null,
					NSLayoutAttribute.NoAttribute, 1, DimensionHelper.PopupCellHeight),
				NSLayoutConstraint.Create(_labelStatus, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _separatorLine3,
					NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_labelStatus, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, ContentView,
					NSLayoutAttribute.CenterX, 1, 0)
			});

			_popupLine = UIHelper.CreatePopupLine(DimensionHelper.PopupLineHeight, DimensionHelper.PopupLineWidth, ColorHelper.Blue, DimensionHelper.PopupLineHeight / 2);
			ContentView.Add(_popupLine);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_popupLine, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _labelStatus, NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(_popupLine, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.CenterX, 1, 0)
			});

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(ContentView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _popupLine, NSLayoutAttribute.Top, 1, - DimensionHelper.MarginNormal)
			});
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();

			var bindingSet = this.CreateBindingSet<PopupPostOptionView, PopupPostOptionViewModel>();

			bindingSet.Bind(_labelDelete.Tap())
				.For(v => v.Command)
				.To(vm => vm.DeleteCommand);

			bindingSet.Bind(_labelRequests.Tap())
				.For(v => v.Command)
				.To(vm => vm.ViewRequestsCommand);

			bindingSet.Bind(_labelModify.Tap())
				.For(v => v.Command)
				.To(vm => vm.ModifyPostCommand);

			bindingSet.Bind(_labelStatus.Tap())
				.For(v => v.Command)
				.To(vm => vm.ChangeStatusCommand);

			bindingSet.Bind(this)
				.For(v => v.CloseCommand)
				.To(vm => vm.CloseCommand);

			bindingSet.Apply();

		}
	}
}