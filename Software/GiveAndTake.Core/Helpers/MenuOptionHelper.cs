using System.Collections.Generic;

namespace GiveAndTake.Core.Helpers
{
	public static class MenuOptionHelper
	{
		public static List<string> GivingPostOptions => new List<string>
		{
			AppConstants.MarkGiven,
			AppConstants.ModifyPost,
			AppConstants.ViewPostRequests,
			AppConstants.DeletePost
		};

		public static List<string> GivenPostOptions => new List<string>
		{
			AppConstants.MarkGiving,
			AppConstants.ModifyPost,
			AppConstants.ViewPostRequests,
			AppConstants.DeletePost
		};

		public static List<string> PendingPostOptions => new List<string>
		{
			AppConstants.CancelRequest,
			AppConstants.ReportPost,
		};

		public static List<string> ApprovedPostOptions => new List<string>
		{
			AppConstants.MarkReceived,
			AppConstants.ReportPost,
		};

		public static List<string> ReceivedPostOptions => new List<string>
		{
			AppConstants.ReportPost,
		};

		public static List<string> GetMenuOptions(string status)
		{
			switch (status)
			{
				case AppConstants.Giving:
					return GivingPostOptions;
				case AppConstants.Gave:
					return GivenPostOptions;
				case AppConstants.Pending:
					return PendingPostOptions;
				case AppConstants.Approved:
					return ApprovedPostOptions;
				case AppConstants.Received:
					return ReceivedPostOptions;
				default:
					return null;
			}
		}
	}
}