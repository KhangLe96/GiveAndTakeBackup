using System;
using System.Collections.Generic;
using System.Text;
using Giveaway.Data.Models.Database;

namespace Giveaway.Service.Services
{
    public interface IProviceCityService : IEntityService<ProvinceCity>
    {
    }

    public class ProviceCityService : EntityService<ProvinceCity>, IProviceCityService
    {
    }
}
