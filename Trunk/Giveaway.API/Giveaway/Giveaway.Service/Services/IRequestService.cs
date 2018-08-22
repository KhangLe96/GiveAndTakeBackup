using Giveaway.Data.Models.Database;

namespace Giveaway.Service.Services
{
    public interface IRequestService : IEntityService<Request>
    {
    }

    public class RequestService : EntityService<Request>, IRequestService
    {

    }
}
