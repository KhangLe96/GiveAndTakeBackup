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
		Submitted,
		Cancelled
	}
}