using System;

namespace GiveAndTake.Core.Exceptions
{
	public class AppException
	{
		public class ApiException : Exception
		{
			public ApiException(string message) : base(message) { }
		}
	}
	
}
