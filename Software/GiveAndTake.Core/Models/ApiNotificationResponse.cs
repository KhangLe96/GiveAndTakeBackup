using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GiveAndTake.Core.Models
{
	[DataContract]
	public class ApiNotificationResponse
	{
		[DataMember(Name = "results")]
		public List<Notification> Notifications { get; set; }

		[DataMember(Name = "pagination")]
		public Pagination Pagination { get; set; }
	}
}
