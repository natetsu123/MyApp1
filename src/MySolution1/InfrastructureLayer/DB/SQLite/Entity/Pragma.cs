using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.DB.SQLite.Entity
{
    public class Pragm
    {
        public int Cid { get; set; }
        public string Name { get; set; } 
        public string Type { get; set; }
        public string Notnull { get; set; }
        public string Dflt_value { get; set; }
        public int Pk { get; set; }
    }
}
