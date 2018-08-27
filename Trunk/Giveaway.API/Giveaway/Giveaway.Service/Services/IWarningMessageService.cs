using Giveaway.Data.Models.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Giveaway.Service.Services
{
    public interface IWarningMessageService : IEntityService<WarningMessage>
    {

    }

    public class WarningMessageService : EntityService<WarningMessage>, IWarningMessageService
    {

    }
}
