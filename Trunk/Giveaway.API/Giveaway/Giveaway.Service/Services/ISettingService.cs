
using Giveaway.Data.Models.Database;

namespace Giveaway.Service.Services
{
	public class SettingService : EntityService<Setting>, ISettingService
	{
		
	}

	public interface ISettingService : IEntityService<Setting>
	{
	}
}
