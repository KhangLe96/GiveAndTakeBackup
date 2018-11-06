using Giveaway.Data.Models.Database;

namespace Giveaway.Service.Services
{
	public interface INotificationService : IEntityService<Notification>
	{
	}

	public class NotificationService : EntityService<Notification>, INotificationService
	{

	}
}
