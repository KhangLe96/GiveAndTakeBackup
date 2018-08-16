using Giveaway.API.Shared.Requests.Warning;
using Giveaway.API.Shared.Responses.Warning;
using System;
using System.Collections.Generic;
using System.Text;

namespace Giveaway.API.Shared.Services.APIs
{
    public interface IWarningMessageService
    {
        WarningResponse Create(WarningRequest warningRequest);
    }
}
