using System.Net;

namespace Giveaway.Data.Models
{
	public class ResponseMessage
	{
		public HttpStatusCode StatusCode { get; set; }

		public string ErrorMessage { get; set; }

		public object Data { get; set; }

		public ResponseMessage(HttpStatusCode statusCode, string errorMessage = null, object data = null)
		{
			StatusCode = statusCode;
			ErrorMessage = errorMessage;
			Data = data;
		}
	}
}
