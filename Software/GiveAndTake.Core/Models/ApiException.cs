using System;

namespace GiveAndTake.Core.Models
{
	//Review ThanhVo Don't put it in Model. Create another folder Exception with class AppException.cs and put ApiException in this class
	public class ApiException : Exception
	{
		public ApiException(string message) : base(message) { }
	}
}
