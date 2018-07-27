using Giveaway.Data.EF.Exceptions;
using System.Linq;
using System.Text.RegularExpressions;

namespace Giveaway.Data.EF.Helpers
{
	public static class ValidationHelper
	{
		private static readonly string[] PhoneNumberRegex = {
			@"^[0-9]{10,11}$",
			@"^\+[0-9]{2}\s+[0-9]{2}[0-9]{8}$",
			@"^[0-9]{3}-[0-9]{4}-[0-9]{4}$",
		};
		
		public static void CheckRequest(object request)
		{
			if (request == null)
			{
				throw new BadRequestException("Thông tin truyền trống");
			}
		}

		public static bool IsPhoneNumberValid(string number)
		{
			if (string.IsNullOrEmpty(number))
			{
				return false;
			}

			bool isValid = Regex.Match(number, MakeCombinedPattern()).Success;

			return isValid;
		}

		public static bool IsUsernameFormatValid(string userName)
		{
			return Regex.IsMatch(userName, Const.UsernamePattern);
		}

		public static bool IsValidEmail(string email)
		{
			try
			{
				var addr = new System.Net.Mail.MailAddress(email);
				return addr.Address == email;
			}
			catch
			{
				return false;
			}
		}


		private static string MakeCombinedPattern()
		{
			return string.Join("|", PhoneNumberRegex
				.Select(item => "(" + item + ")"));
		}
	}
}
