using GiveAndTake.Core.Helpers.Interface;

namespace GiveAndTake.Core.Helpers
{
	public class EmailHelper : IEmailHelper
	{
		private const string DefaultFeedbackEmail = AppConstants.DefaultFeedbackEmail;
		public const string FeedbackSubject = "Góp ý cho ứng dụng \"Cho và Nhận\"";
		public const string FormatEmailUrl = "mailto:{0}?subject={1}&body={2}";

		private readonly IUrlHelper _urlHelper;

		public EmailHelper(IUrlHelper urlHelper)
		{
			_urlHelper = urlHelper;
		}

		public void OpenFeedbackEmail()
		{
			var subject = EncodeUrl(string.Format(FeedbackSubject));
			var body = EncodeUrl(string.Empty);
			var url = string.Format(FormatEmailUrl, DefaultFeedbackEmail, subject, body);

			_urlHelper.OpenUrl(url);
		}

		private string EncodeUrl(string url)
		{
			var urlEncode = System.Net.WebUtility.UrlEncode(url);

			return urlEncode?.Replace("+", "%20") ?? url;
		}
	}
}
