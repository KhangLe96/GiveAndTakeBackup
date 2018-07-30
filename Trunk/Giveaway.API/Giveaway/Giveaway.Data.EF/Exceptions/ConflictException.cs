using System;

namespace Giveaway.Data.EF.Exceptions
{
	public class ConflictException : Exception
	{
		public ConflictException(string message) : base(message)
		{
		}
	}
}
