using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GiveAndTake.Core.Models
{
    public class DataModel : IDataModel
    {
        public ICollection<Category> Categories { get; set; }
    }
}
