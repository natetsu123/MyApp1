using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.DB
{
    interface ISQLite
    {
        SQLiteDataReader ExecuteQuery(string query);
        SQLiteDataReader ExecuteQuery(string query, Dictionary<string ,object> keyValues);
        void ExecuteNonQuery(string query);
        void ExecuteNonQuery(string query, Dictionary<string, object> keyValues);

    }
}
