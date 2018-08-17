using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Text;

namespace GiveAndTake.Core.Models
{
    public interface IDataModel
    {
        ICollection<Category> Categories { get; set; }
    }
}
