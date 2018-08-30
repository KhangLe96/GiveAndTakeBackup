using System;
using System.Collections.Generic;
using System.Text;
using Giveaway.Data.Models.Database;

namespace Giveaway.Service.Services
{
    public interface IProvinceCityService : IEntityService<ProvinceCity>
    {
    }

    public class ProvinceCityService : EntityService<ProvinceCity>, IProvinceCityService
    {
    }
}
