using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels
{
	public class UserProfileViewModel : BaseViewModel
    {
	    public string RankTitle => AppConstants.RankTitle;

	    public string SentTitle => AppConstants.SentTitle;

	    public string LeftButtonTitle => AppConstants.MyPostsTitle;

	    public string RightButtonTitle => AppConstants.MyRequestsTitle;

	    public string AvatarUrl
	    {
		    get => _avatarUrl;
		    set => SetProperty(ref _avatarUrl, value);
	    }

	    public string UserName
	    {
		    get => _userName;
		    set => SetProperty(ref _userName, value);
		}

	    public string RankType
	    {
		    get => _rankType;
		    set => SetProperty(ref _rankType, value);
		}

	    public string SentCount
	    {
		    get => _sentCount;
		    set => SetProperty(ref _sentCount, value);
		}

	    public bool IsPostsList
	    {
		    get => _isPostsList;
		    set => SetProperty(ref _isPostsList, value);
		}
		
	    public IMvxCommand ShowMyPostsCommand =>
		    _showMyPostsCommand ?? (_showMyPostsCommand = new MvxCommand(ShowMyPosts));

	    public IMvxCommand ShowMyRequestsCommand =>
			_showMyRequestsCommand ?? (_showMyRequestsCommand = new MvxCommand(ShowMyRequests));

	    private string _avatarUrl;
	    private string _userName;
	    private string _rankType;
	    private string _sentCount;
	    private bool _isPostsList;
	    private IDataModel _dataModel;
	    private IMvxCommand _showMyPostsCommand;
	    private IMvxCommand _showMyRequestsCommand;

	    public UserProfileViewModel(IDataModel dataModel)
	    {
		    _dataModel = dataModel;
		    AvatarUrl = _dataModel.LoginResponse.Profile.AvatarUrl;
		    UserName = _dataModel.LoginResponse.Profile.FullName;
		    RankType = AppConstants.Member;
		    SentCount = _dataModel.LoginResponse.Profile.SentCount + AppConstants.Times;
		    IsPostsList = true;
	    }

	    private void ShowMyPosts()
	    {
		    IsPostsList = true;
	    }

	    private void ShowMyRequests()
	    {
		    IsPostsList = false;
	    }
	}
}
