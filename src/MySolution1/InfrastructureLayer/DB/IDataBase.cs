using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.DB
{
    public interface IDataBase
    {
        void Close();
        void BeginTransaction();
        void CommitTransaction();
        void RollBack();
        List<string> GetTables();
        List<string> GetColumns(string table_name);
        List<string> GetPrimaryKey(string table_name);
        Dictionary<string, string> GetColumnInfo(string table_name, string column_name);
    }
}
