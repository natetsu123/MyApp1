using FormApp1.DatabaseConnectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormApp1.Entity
{
    interface IEntity<obj, DataBase> : IDisposable
    {
        string TableName { get; }
        void Database(DataBase db);

        bool CreateTable();
        bool DropTable();

        obj[] Select();
        bool Insert(obj entity);
        bool Update(obj entity);
        bool Delete(obj entity);
        bool Truncate();
        
    }
}
