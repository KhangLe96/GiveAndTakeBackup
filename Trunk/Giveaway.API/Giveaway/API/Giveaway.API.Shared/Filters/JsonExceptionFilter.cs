using Giveaway.API.Shared.Responses;
using Giveaway.Data.EF.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BadRequestException = Giveaway.API.Shared.Exceptions.BadRequestException;
using ConflictException = Giveaway.API.Shared.Exceptions.ConflictException;

namespace Giveaway.API.Shared.Filters
{
    public class JsonExceptionFilter : IExceptionFilter
	{
		private const int BadRequestCode = 400;
		private const int SystemFailureCode = 500;
		private const int ConflictCode = 409;
		private const int NotFoundCode = 404;

		public void OnException(ExceptionContext context)
		{
			int code;
			switch (context.Exception)
			{
				case BadRequestException _:
					code = BadRequestCode;
					break;
				case ConflictException _:
					code = ConflictCode;
					break;
				case DataNotFoundException _:
					code = NotFoundCode;
					break;
				default:
					code = SystemFailureCode;
					break;
			}

			var result = new ObjectResult(new ErrorResponse(context.Exception)) { StatusCode = code };
			context.Result = result;
		}
	}
}
