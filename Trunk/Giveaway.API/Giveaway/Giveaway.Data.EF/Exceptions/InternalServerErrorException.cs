using System;

namespace Giveaway.Data.EF.Exceptions
{
	public class InternalServerErrorException : Exception
	{
		public InternalServerErrorException(string message) : base(message)
		{
		}
	}
}
