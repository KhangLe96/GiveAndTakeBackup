using Giveaway.API.Shared.Requests.Warning;
using Giveaway.API.Shared.Responses.Warning;

namespace Giveaway.API.Shared.Services.APIs
{
	public interface IWarningMessageService
    {
        WarningResponse Create(WarningRequest warningRequest);
    }
}
