using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace GiveAndTake.Core.ViewModels
{
	public class RequestsViewModel : BaseViewModel<Post, bool>
	{
		public string Title => "Danh sách yêu cầu";

		public bool IsRefreshing
		{
			get => _isRefresh;
			set => SetProperty(ref _isRefresh, value);
		}

		public int NumberOfRequest
		{
			get => _numberOfRequest;
			set => SetProperty(ref _numberOfRequest, value);
		}

		public MvxObservableCollection<RequestItemViewModel> RequestItemViewModels
		{
			get => _requestItemViewModels;
			set => SetProperty(ref _requestItemViewModels, value);
		}

		public IMvxCommand RefreshCommand => _refreshCommand = _refreshCommand ?? new MvxCommand(OnRefresh);
		public IMvxCommand LoadMoreCommand => _loadMoreCommand = _loadMoreCommand ?? new MvxAsyncCommand(OnLoadMore);

		public IMvxCommand BackPressedCommand =>
			_backPressedCommand = _backPressedCommand ?? new MvxAsyncCommand(() => NavigationService.Close(this, true));

		private readonly IDataModel _dataModel;
		private MvxObservableCollection<RequestItemViewModel> _requestItemViewModels;
		private int _numberOfRequest;
		private bool _isRefresh;
		private IMvxCommand _refreshCommand;
		private IMvxCommand _loadMoreCommand;
		private IMvxCommand _backPressedCommand;
		private string _postId;
		private Post _post;
		private readonly ILoadingOverlayService _overlay;

		public RequestsViewModel(IDataModel dataModel, ILoadingOverlayService loadingOverlayService)
		{
			_dataModel = dataModel;
			_overlay = loadingOverlayService;
		}

		private async void InitRequestViewModels()
		{			
			await UpdateRequestViewModelOverLay();			
		}

		private async Task OnLoadMore()
        {
            _dataModel.ApiRequestsResponse = await ManagementService.GetRequestOfPost(_postId, $"limit={AppConstants.NumberOfRequestPerPage}&page={_dataModel.ApiRequestsResponse.Pagination.Page + 1}", _dataModel.LoginResponse.Token);
            if (_dataModel.ApiRequestsResponse.Requests.Any())
            {
                RequestItemViewModels.Last().IsSeperatorShown = true;
                RequestItemViewModels.AddRange(_dataModel.ApiRequestsResponse.Requests.Select(GenerateRequestItem));
                RequestItemViewModels.Last().IsSeperatorShown = false;
            }
        }

	    private RequestItemViewModel GenerateRequestItem(Request request)
	    {
		    var requestItem = new RequestItemViewModel(request)
		    {
				ClickAction = OnItemClicked,
				AcceptAction = OnRequestAccepted,
				RejectAction = OnRequestRejected
		    };
			return requestItem;
	    }

	    private async void OnRequestRejected(Request request)
	    {
			var result = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.RequestRejectingMessage);
		    if (result == RequestStatus.Submitted)
		    {
				await ManagementService.ChangeStatusOfRequest(request.Id, "Rejected", _dataModel.LoginResponse.Token);
			    await UpdateRequestViewModelOverLay();
			}
		}

	    private async void OnRequestAccepted(Request request)
	    {
		    var result = await NavigationService.Navigate<PopupResponseViewModel, Request, RequestStatus>(request);
		    if (result == RequestStatus.Submitted)
		    {
				await UpdateRequestViewModelOverLay();
			}
		}

	    private async void OnItemClicked(Request request)
	    {
		    request.Post = _post;
		    var popupResult = await NavigationService.Navigate<RequestDetailViewModel, Request, PopupRequestDetailResult>(request);
		    switch (popupResult)
		    {
			    case PopupRequestDetailResult.Rejected:
				    OnRequestRejected(request);
					break;
			    case PopupRequestDetailResult.Accepted:
				    OnRequestAccepted(request);
				    break;
			    case PopupRequestDetailResult.ShowPostDetail:
				    await NavigationService.Navigate<PostDetailViewModel, Post>(_post);
				    break;
			}
	    }

	    private async void OnRefresh()
        {
            IsRefreshing = true;
            await UpdateRequestViewModels();
            IsRefreshing = false;
        }

        public async Task UpdateRequestViewModels()
        {
			_dataModel.ApiRequestsResponse = await ManagementService.GetRequestOfPost(_postId, $"limit={AppConstants.NumberOfRequestPerPage}", _dataModel.LoginResponse.Token);	       
			NumberOfRequest = _dataModel.ApiRequestsResponse.Pagination.Totals;
			RequestItemViewModels = new MvxObservableCollection<RequestItemViewModel>(_dataModel.ApiRequestsResponse.Requests.Select(GenerateRequestItem));
            if (RequestItemViewModels.Any())
            {
                RequestItemViewModels.Last().IsSeperatorShown = false;
            }	        
		}

	    public override void Prepare(Post post)
	    {
		    _postId = post.PostId;
		    _post = post;
			
		    InitRequestViewModels();
		}

		public async Task UpdateRequestViewModelOverLay()
		{
			//await _overlay.ShowOverlay(AppConstants.LoadingDataOverlayTitle);
			await UpdateRequestViewModels();
			await _overlay.CloseOverlay();
		}
	}
}
