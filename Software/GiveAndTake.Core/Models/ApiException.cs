using System;

namespace GiveAndTake.Core.Models
{
	public class ApiException : Exception
	{
		public ApiException(string message) : base(message) { }
	}
}
