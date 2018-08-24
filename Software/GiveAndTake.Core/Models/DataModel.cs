using System.Collections.Generic;

namespace GiveAndTake.Core.Models
{
    public class DataModel : IDataModel
    {
        public ICollection<Category> Categories { get; set; }
    }
}
