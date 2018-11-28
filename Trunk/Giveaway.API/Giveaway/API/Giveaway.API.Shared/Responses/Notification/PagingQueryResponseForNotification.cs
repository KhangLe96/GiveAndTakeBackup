using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Responses.Notification
{
	[DataContract]
	public class PagingQueryResponseForNotification : PagingQueryResponse<NotificationResponse>
	{
		[DataMember(Name = "numberOfNotiNotSeen")]
		public int NumberOfNotiNotSeen { get; set; }
	}
}
