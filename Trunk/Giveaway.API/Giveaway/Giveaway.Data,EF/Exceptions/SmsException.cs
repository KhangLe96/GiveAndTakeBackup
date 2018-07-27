using System;

namespace Giveaway.Data.EF.Exceptions
{
	public class SmsException : Exception
	{
		public SmsException(string message) : base(message)
		{
		}
	}
}
