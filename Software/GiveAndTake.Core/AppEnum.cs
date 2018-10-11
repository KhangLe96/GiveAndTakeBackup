namespace GiveAndTake.Core
{
	public enum FontType
	{
		Thin,
		Light,
		Regular,
		Medium,
		Bold,
		Italic,
		LightItalic
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

	public enum RequestStatus
	{
		Submitted,
		Cancelled
	}
}