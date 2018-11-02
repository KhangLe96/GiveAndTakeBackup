using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using GiveAndTake.Core.Exceptions;
using GiveAndTake.Core.Services;
using MvvmCross;

namespace GiveAndTake.Core.ViewModels
{
	public class RequestsViewModel : BaseViewModel<string, bool>
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
	        try
	        {
				_dataModel.ApiRequestsResponse = await ManagementService.GetRequestOfPost(_postId, $"limit=20&page={_dataModel.ApiRequestsResponse.Pagination.Page + 1}");
		        if (_dataModel.ApiRequestsResponse.Requests.Any())
		        {
			        RequestItemViewModels.Last().IsLastViewInList = false;
			        RequestItemViewModels.AddRange(_dataModel.ApiRequestsResponse.Requests.Select(GenerateRequestItem));
			        RequestItemViewModels.Last().IsLastViewInList = true;
		        }
			}
	        catch (AppException.ApiException)
	        {
		        await NavigationService.Navigate<PopupWarningViewModel, string, bool>(AppConstants.ErrorConnectionMessage);
		        await OnLoadMore();
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
			try
			{
				var result = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.RequestRejectingMessage);
				if (result == RequestStatus.Submitted)
				{
					await ManagementService.ChangeStatusOfRequest(request.Id, "Rejected", _dataModel.LoginResponse.Token);
					await UpdateRequestViewModelOverLay();
				}
			
			}
			catch (AppException.ApiException)
			{
				await NavigationService.Navigate<PopupWarningViewModel, string, bool>(AppConstants.ErrorConnectionMessage);
				await OnLoadMore();
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
	        try
	        {
		        _dataModel.ApiRequestsResponse = await ManagementService.GetRequestOfPost(_postId, "");
		        NumberOfRequest = _dataModel.ApiRequestsResponse.Pagination.Totals;
		        RequestItemViewModels = new MvxObservableCollection<RequestItemViewModel>(_dataModel.ApiRequestsResponse.Requests.Select(GenerateRequestItem));
		        if (RequestItemViewModels.Any())
		        {
			        RequestItemViewModels.Last().IsLastViewInList = true;
		        }
			}
	        catch (AppException.ApiException)
	        {
		        await NavigationService.Navigate<PopupWarningViewModel, string, bool>(AppConstants.ErrorConnectionMessage);
		        await OnLoadMore();
	        }        
		}

	    public override void Prepare(string postId)
	    {
		    _postId = postId;
		    InitRequestViewModels();
		}

		public async Task UpdateRequestViewModelOverLay()
		{
			await _overlay.ShowOverlay(AppConstants.LoadingDataOverlayTitle);
			await UpdateRequestViewModels();
			await _overlay.CloseOverlay();
		}
	}
}
