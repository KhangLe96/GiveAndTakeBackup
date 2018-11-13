using GiveAndTake.Core.ViewModels;
using GiveAndTake.iOS.CustomControls;
using GiveAndTake.iOS.Helpers;
using GiveAndTake.iOS.Views.Base;
using GiveAndTake.iOS.Views.TableViewSources;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace GiveAndTake.iOS.Views
{
	[MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen,
		ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
	public class RequestsView : BaseView
	{
		private HeaderBar _headerBar;
		private UIView _titleArea;
		private UILabel _title;
		private UIButton _btnRequestNumber;
		private UITableView _requestsTableView;
		private RequestItemTableViewSource _requestTableViewSource;
		private MvxUIRefreshControl _refreshControl;

		public IMvxCommand LoadMoreCommand { get; set; }
		public IMvxCommand BackPressedCommand { get; set; }

		protected override void InitView()
		{
			InitHeaderBar();
			InitTitleArea();
			InitRequestsTableView();
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();

			//Review ThanhVo should be bindingSet name
			var set = this.CreateBindingSet<RequestsView, RequestsViewModel>();

			set.Bind(this)
				.For(v => v.BackPressedCommand)
				.To(vm => vm.BackPressedCommand);

			set.Bind(_btnRequestNumber)
				.For("Title")
				.To(vm => vm.NumberOfRequest);

			set.Bind(_requestTableViewSource)
				.To(vm => vm.RequestItemViewModels);

			set.Bind(this)
				.For(v => v.LoadMoreCommand)
				.To(vm => vm.LoadMoreCommand);


			set.Bind(_refreshControl)
				.For(v => v.IsRefreshing)
				.To(vm => vm.IsRefreshing);

			set.Bind(_refreshControl)
				.For(v => v.RefreshCommand)
				.To(vm => vm.RefreshCommand);

			set.Apply();
		}

		private void InitHeaderBar()
		{
			_headerBar = UIHelper.CreateHeaderBar(ResolutionHelper.Width, DimensionHelper.HeaderBarHeight,
				UIColor.White, true);

			//Review ThanhVo OnBackPressed just action, so = is enough. But should make the method for easy maintainance
			_headerBar.OnBackPressed += () => BackPressedCommand?.Execute();

			View.Add(_headerBar);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_headerBar, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Top, 1, ResolutionHelper.StatusHeight),
				NSLayoutConstraint.Create(_headerBar, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, 0)
			});
		}

		private void InitTitleArea()
		{
			_titleArea = UIHelper.CreateView(DimensionHelper.RequestTitleAreaHeight, ResolutionHelper.Width,
				ColorHelper.Blue);

			View.Add(_titleArea);
			View.AddConstraints(new []
			{
				NSLayoutConstraint.Create(_titleArea, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _headerBar,
					NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_titleArea, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
					NSLayoutAttribute.Left, 1, 0)
			});

			_title = UIHelper.CreateLabel(UIColor.White, DimensionHelper.RequestTitleTextSize);
			//Review ThanhVo Should bind from view model
			_title.Text = "Danh sách yêu cầu";
			_titleArea.Add(_title);
			_titleArea.AddConstraints(new []
			{
				NSLayoutConstraint.Create(_title, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _titleArea,
					NSLayoutAttribute.Top, 1, DimensionHelper.RequestTitleMarignTop),
				NSLayoutConstraint.Create(_title, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _titleArea,
					NSLayoutAttribute.Left, 1, DimensionHelper.RequestTitleMarignLeft)
			});

			_btnRequestNumber = UIHelper.CreateButton(
				DimensionHelper.ButtonCategoryHeight,
				0,
				UIColor.White,
				ColorHelper.Blue,
				DimensionHelper.RequestNumberTextSize,
				DimensionHelper.ButtonCategoryHeight / 2);
			//_btnRequestNumber.SetTitle("10", UIControlState.Normal);

			_titleArea.Add(_btnRequestNumber);
			_titleArea.AddConstraints(new []
			{
				NSLayoutConstraint.Create(_btnRequestNumber, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _titleArea,
					NSLayoutAttribute.Top, 1, DimensionHelper.RequestTitleMarignTop),
				NSLayoutConstraint.Create(_btnRequestNumber, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _title,
					NSLayoutAttribute.Right, 1, DimensionHelper.MarginNormal)
			});
		}

		private void InitRequestsTableView()
		{
			_requestsTableView = UIHelper.CreateTableView(0, 0);
			_requestTableViewSource = new RequestItemTableViewSource(_requestsTableView)
			{
				//Review ThanhVo should make the method for easy maintainance
				LoadMoreEvent = () => LoadMoreCommand?.Execute()
			};

			_requestsTableView.Source = _requestTableViewSource;
			_refreshControl = new MvxUIRefreshControl();
			_requestsTableView.RefreshControl = _refreshControl;

			View.Add(_requestsTableView);
			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_requestsTableView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _titleArea, NSLayoutAttribute.Bottom, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_requestsTableView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 1, DimensionHelper.MarginShort),
				NSLayoutConstraint.Create(_requestsTableView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View, NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(_requestsTableView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View, NSLayoutAttribute.Right, 1, - DimensionHelper.MarginShort)
			});
		}
	}
}