using System;
using System.Collections.Generic;
using System.Linq;
using Giveaway.API.Shared.Responses;
using Giveaway.Data.Models.Database;

namespace Giveaway.API.Shared.Services.APIs
{
	public interface IUserService
	{
	    bool UpdateUser(User user);
	    User GetUser(Guid id);
	    bool DeleteUser(Guid id);
	    PagingQueryResponse<UserProfileResponse> All(IDictionary<string, string> @params);
	    User Find(Guid userId);
	    bool Update(User user);
	}
}
