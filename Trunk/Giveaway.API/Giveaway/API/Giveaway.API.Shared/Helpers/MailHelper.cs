using Giveaway.Util.Utils;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Giveaway.API.Shared.Helpers
{
    public static class MailHelper
	{
		private const string Host = "smtp.gmail.com";
		private const int Port = 587;

		public static Task Send(string subject, string body, string toEmail = null, string pathAttach = "", string ccEmail = "")
		{
			// UI doesn't to want for sending email time
			return Task.Run(() =>
			{
				try
				{
					MailMessage mail = new MailMessage();
					Attachment data = null;

					if (!string.IsNullOrEmpty(pathAttach) && File.Exists(pathAttach))
					{
						data = new Attachment(pathAttach, MediaTypeNames.Application.Octet);
						mail.Attachments.Add(data);
					}

					var configuration = ServiceProviderHelper.Current.GetService<IConfigurationRoot>();
					var mailAccount = configuration.GetSection("Mail");
					var fromEmail = mailAccount["Account"];

					mail.Subject = subject;
					mail.From = new MailAddress(fromEmail);

					if (string.IsNullOrEmpty(toEmail))
					{
						toEmail = mailAccount["Support"];
					}

					var emails = toEmail.Replace(',', ';').Split(';');

					foreach (var email in emails)
					{
						mail.To.Add(email);
					}

					mail.Body = body;

					if (!string.IsNullOrEmpty(ccEmail))
					{
						mail.CC.Add(ccEmail);
					}

					SmtpClient smtp = new SmtpClient(Host, Port) {EnableSsl = true};

					NetworkCredential netCre = new NetworkCredential(fromEmail, mailAccount["Pasword"]);
					smtp.Credentials = netCre;
					smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
					smtp.Send(mail);

					if (!string.IsNullOrEmpty(pathAttach) && File.Exists(pathAttach))
					{
						data?.Dispose();
						File.Delete(pathAttach);
					}
				}
				catch
				{
					// ignored
				}
			});
		}
	}
}
