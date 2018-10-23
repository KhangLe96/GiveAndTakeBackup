using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace GiveAndTake.Core.ViewModels
{
	public class RequestsViewModel : BaseViewModel
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
		    set
		    {
			    SetProperty(ref _requestItemViewModels, value);
			    Task.Run(async () => { await UpdateRequestViewModels(); });
		    }
	    }

	    public IMvxCommand RefreshCommand => _refreshCommand = _refreshCommand ?? new MvxCommand(OnRefresh);

	    public IMvxCommand LoadMoreCommand => _loadMoreCommand = _loadMoreCommand ?? new MvxCommand(OnLoadMore);

		private readonly IDataModel _dataModel;
	    private MvxObservableCollection<RequestItemViewModel> _requestItemViewModels;
		private int _numberOfRequest;
	    private bool _isRefresh;
		private IMvxCommand _refreshCommand;
	    private IMvxCommand _loadMoreCommand;

        public RequestsViewModel(IDataModel dataModel)
        {
            _dataModel = dataModel;
        }

        private async void OnLoadMore()
        {
            _dataModel.ApiRequestsResponse = await ManagementService.GetRequestOfPost("", $"limit=20&page={_dataModel.ApiRequestsResponse.Pagination.Page + 1}");
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

	    private void OnRequestAccepted(Request request)
	    {
	    }

	    private void OnItemClicked(Request request)
	    {
	    }

	    private async void OnRefresh()
        {
            IsRefreshing = true;
            await UpdateRequestViewModels();
            IsRefreshing = false;
        }

        public async Task UpdateRequestViewModels()
        {
			var result = await ManagementService.GetRequestOfPost("", "");
	        NumberOfRequest = result.Pagination.Totals;
			RequestItemViewModels = new MvxObservableCollection<RequestItemViewModel>(result.Requests.Select(GenerateRequestItem));
            if (RequestItemViewModels.Any())
            {
                RequestItemViewModels.Last().IsLastViewInList = true;
            }
		}
    }
}
