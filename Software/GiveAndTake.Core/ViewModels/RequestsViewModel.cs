using System;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GiveAndTake.Core.ViewModels
{
    public class RequestsViewModel : BaseViewModel
    {
        private readonly IDataModel _dataModel;
        private readonly IManagementService _managementService = Mvx.Resolve<IManagementService>();

        private int _numberOfRequest;
        public int NumberOfRequest
        {
            get => _numberOfRequest;
            set => SetProperty(ref _numberOfRequest, value);
        }

        public string Title => "Danh sách yêu cầu";

        private MvxObservableCollection<RequestItemViewModel> _requestItemViewModels;
        public MvxObservableCollection<RequestItemViewModel> RequestItemViewModels
        {
            get => _requestItemViewModels;
            set => SetProperty(ref _requestItemViewModels, value);
        }

        private bool _isRefresh;
        public bool IsRefreshing
        {
            get => _isRefresh;
            set => SetProperty(ref _isRefresh, value);
        }

        public ICommand LoadMoreCommand { get; private set; }

        private MvxCommand _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand = _refreshCommand ?? new MvxCommand(OnRefresh);

        public RequestsViewModel(IDataModel dataModel)
        {
            _dataModel = dataModel;
            UpdateRequestViewModels();
            InitCommand();
        }

        private void InitCommand()
        {
            LoadMoreCommand = new MvxCommand(OnLoadMore);
        }

        private void OnLoadMore()
        {
			//Review ThanhVo What is 20 here? Should define it as constant, so the reader can know its meaning
            _dataModel.ApiRequestsResponse = _managementService.GetRequestOfPost("", $"limit=20&page={_dataModel.ApiRequestsResponse.Pagination.Page + 1}");
            if (_dataModel.ApiRequestsResponse.Requests.Any())
            {
                RequestItemViewModels.Last().IsLastViewInList = false;
                RequestItemViewModels.AddRange(_dataModel.ApiRequestsResponse.Requests.Select(GenerateRequestItem));
                RequestItemViewModels.Last().IsLastViewInList = true;
            }
        }

	    private RequestItemViewModel GenerateRequestItem(Request request)
	    {
		    var requestItem = new RequestItemViewModel(request, _dataModel);
			//Review ThanhVo register action like this requestItem.ReloadRequestList = OnRefresh;
			requestItem.ReloadRequestList += new Action(OnRefresh);

			return requestItem;
	    }

	    private async void OnRefresh()
        {
            IsRefreshing = true;
            UpdateRequestViewModels();
            await Task.Delay(1000);
            IsRefreshing = false;
        }

        public void UpdateRequestViewModels()
        {
			//Review ThanhVo At least, you have to pass post Id for request, because you only load request of this post, not all
			var result = _managementService.GetRequestOfPost("", "");
	        NumberOfRequest = result.Pagination.Totals;
			RequestItemViewModels = new MvxObservableCollection<RequestItemViewModel>(result.Requests.Select(GenerateRequestItem));
            if (RequestItemViewModels.Any())
            {
                RequestItemViewModels.Last().IsLastViewInList = true;
            }
		}
    }
}
