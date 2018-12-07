namespace GiveAndTake.Core
{
	public enum FontType
	{
		Light,
		Regular,
		Medium,
		Bold,
	}
	public enum RequestMethod
	{
		Get,
		Put,
		Post,
		Delete
	}

	public enum NetworkStatus
	{
		Success,
		NoWifi,
		Timeout,
		Exception
	}

	public enum PopupListType
	{
		FilterType,
		MenuType
	}

	public enum RequestStatus
	{
		Cancelled,
		Submitted
	}
	public enum MyRequestStatus
	{
		Rejected,
		Pending,
		Approved,
		Received,
	}
	public enum PopupMyRequestStatusResult
	{
		Cancelled,
		Removed,
		Received,
	}
	public enum PopupRequestDetailResult
	{
		Cancelled,
		Accepted,
		Rejected,
		ShowPostDetail
	}

	public enum ListViewItemType
	{
		MyPosts,
		MyRequests
	}

	public enum ViewMode
	{
		CreatePost,
		EditPost,
	}
}