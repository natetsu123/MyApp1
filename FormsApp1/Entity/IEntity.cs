using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsApp1.Entity
{
    public interface IEntity
    {
        Dictionary<string, object> ToDictionary();
        string ToStringData();
    }
}
