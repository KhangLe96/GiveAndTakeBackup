using Giveaway.Data.Models.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Giveaway.Service.Services
{
    public interface IReportService : IEntityService<Report>
    {
    }

    public class ReportService : EntityService<Report>, IReportService
    {

    }
}
