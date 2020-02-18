using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.DB
{
    interface IEntity
    {
        string TableName();
        bool CreateTable();
        bool DropTable();
        bool Truncate();
        bool Delete<T>(T entity);
        bool Insert<T>(T entity);
        bool Update<T>(T entity);
        T[] Select<T>();

        List<string> GetEntityProperties<T>(T entity);
    }
}
