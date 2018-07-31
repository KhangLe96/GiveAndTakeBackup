using System;
using System.Linq;
using Giveaway.Data.Models.Database;

namespace Giveaway.API.Shared.Services.APIs
{
	public interface IUserService
	{
	    IQueryable<User> GetUsers();
	    bool UpdateUser(User user);
	    User GetUser(Guid id);
	    bool DeleteUser(Guid id);
	}
}
