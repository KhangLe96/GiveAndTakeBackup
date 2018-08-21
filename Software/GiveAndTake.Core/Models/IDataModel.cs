using System.Collections.Generic;

namespace GiveAndTake.Core.Models
{
    public interface IDataModel
    {
        ICollection<Category> Categories { get; set; }
    }
}
