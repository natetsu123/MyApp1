using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormApp1.Entity
{
    public interface IEntityData
    {
        Dictionary<string, object> ToDictionary();
        string ToStringData();
    }
}
