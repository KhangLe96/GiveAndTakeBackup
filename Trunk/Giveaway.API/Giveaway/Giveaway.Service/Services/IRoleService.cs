using Giveaway.Data.Models.Database;

namespace Giveaway.Service.Services
{
    public interface IRoleService : IEntityService<Role>
    {
        
    }

    public class RoleService : EntityService<Role>, IRoleService
    {

    }
}