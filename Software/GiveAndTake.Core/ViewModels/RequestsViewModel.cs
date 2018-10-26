using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace GiveAndTake.Core.ViewModels
{
	public class RequestsViewModel : BaseViewModel<string>
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

		private readonly IDataModel _dataModel;
	    private MvxObservableCollection<RequestItemViewModel> _requestItemViewModels;
		private int _numberOfRequest;
	    private bool _isRefresh;
		private IMvxCommand _refreshCommand;
	    private IMvxCommand _loadMoreCommand;
	    private string _postId;

	    public RequestsViewModel(IDataModel dataModel)
        {
            _dataModel = dataModel;
        }

	    private async void InitRequestViewModels() => await UpdateRequestViewModels();


	    private async Task OnLoadMore()
        {
            _dataModel.ApiRequestsResponse = await ManagementService.GetRequestOfPost(_postId, $"limit=20&page={_dataModel.ApiRequestsResponse.Pagination.Page + 1}");
            if (_dataModel.ApiRequestsResponse.Requests.Any())
            {
                RequestItemViewModels.Last().IsLastViewInList = false;
                RequestItemViewModels.AddRange(_dataModel.ApiRequestsResponse.Requests.Select(GenerateRequestItem));
                RequestItemViewModels.Last().IsLastViewInList = true;
            }
        }

	    private RequestItemViewModel GenerateRequestItem(Request request)
	    {
		    var requestItem = new RequestItemViewModel(request)
		    {
				OnClicked = OnItemClicked,
				OnAccepted = OnRequestAccepted,
				OnRejected = OnRequestRejected
		    };

			return requestItem;
	    }

	    private async void OnRequestRejected(Request request)
	    {
			var result = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.RequestRejectingMessage);
		    if (result == RequestStatus.Submitted)
		    {
			    await ManagementService.ChangeStatusOfRequest(request.Id, "Rejected", _dataModel.LoginResponse.Token);
		    }
		}

	    private async void OnRequestAccepted(Request request)
	    {
		    var result = await NavigationService.Navigate<PopupResponseViewModel, Request, RequestStatus>(request);
		    if (result == RequestStatus.Submitted)
		    {
			    await UpdateRequestViewModels();
		    }
		}

	    private async void OnItemClicked(Request request)
	    {
		    var popupResult = await NavigationService.Navigate<RequestDetailViewModel, Request, PopupRequestDetailResult>(request);
		    switch (popupResult)
		    {
			    case PopupRequestDetailResult.Rejected:
				    OnRequestRejected(request);
					break;
			    case PopupRequestDetailResult.Accepted:
				    OnRequestAccepted(request);
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
	        _dataModel.ApiRequestsResponse = await ManagementService.GetRequestOfPost(_postId, "");
	        NumberOfRequest = _dataModel.ApiRequestsResponse.Pagination.Totals;
			RequestItemViewModels = new MvxObservableCollection<RequestItemViewModel>(_dataModel.ApiRequestsResponse.Requests.Select(GenerateRequestItem));
            if (RequestItemViewModels.Any())
            {
                RequestItemViewModels.Last().IsLastViewInList = true;
            }
		}

	    public override void Prepare(string postId)
	    {
		    _postId = postId;
		    InitRequestViewModels();
		}
    }
}
