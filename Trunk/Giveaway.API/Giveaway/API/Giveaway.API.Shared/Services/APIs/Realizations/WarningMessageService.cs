using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Giveaway.API.Shared.Requests.Warning;
using Giveaway.API.Shared.Responses.Warning;
using Giveaway.Data.EF.Exceptions;
using Giveaway.Data.Models.Database;
using static Giveaway.Data.EF.Const;

namespace Giveaway.API.Shared.Services.APIs.Realizations
{
    public class WarningMessageService : IWarningMessageService
    {
        private readonly Service.Services.IWarningMessageService _warningMessageService;
        public WarningMessageService(Service.Services.IWarningMessageService warningMessageService)
        {
            _warningMessageService = warningMessageService;
        }

        public WarningResponse Create(WarningRequest warningRequest)
        {
            var warning = Mapper.Map<WarningMessage>(warningRequest);
            var result = _warningMessageService.Create(warning, out var isSaved);
            if(!isSaved)
            {
                throw new InternalServerErrorException(Error.InternalServerError);
            }

            var warningResponse = Mapper.Map<WarningResponse>(result);

            return warningResponse;
        }
    }
}
