
using Giveaway.Data.Models.Database;

namespace Giveaway.Service.Services
{
	public class SuperAdminService : EntityService<SuperAdmin>, ISuperAdminService
	{
	
	}

	public interface ISuperAdminService : IEntityService<SuperAdmin>
	{
	}
}
