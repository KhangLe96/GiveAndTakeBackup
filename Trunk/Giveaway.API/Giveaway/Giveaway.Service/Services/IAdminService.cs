
using Giveaway.Data.Models.Database;

namespace Giveaway.Service.Services
{
	public class AdminService : EntityService<Admin>, IAdminService
	{
	
	}

	public interface IAdminService : IEntityService<Admin>
	{
	}
}
