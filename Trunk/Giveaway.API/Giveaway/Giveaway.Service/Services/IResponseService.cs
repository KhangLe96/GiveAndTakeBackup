using System;
using System.Collections.Generic;
using System.Text;
using Giveaway.Data.Models.Database;

namespace Giveaway.Service.Services
{
	public interface IResponseService : IEntityService<Response>
	{
	}

	public class ResponseService : EntityService<Response>, IResponseService
	{

	}
}
