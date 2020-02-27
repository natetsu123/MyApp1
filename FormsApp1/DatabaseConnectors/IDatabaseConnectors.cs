using System;
using System.Collections.Generic;

namespace FormsApp1.DatabaseConnectors
{
    interface IDatabaseConnectors<T> : IDisposable
    {
        T ExecuteQuery(string query);
        T ExecuteQuery(string query, Dictionary<string, object> keyValuePairs);
        void ExecuteNonQuery(string query);
        void ExecuteNonQuery(string query, Dictionary<string, object> keyValuePairs);
        void Initialize();
        void BuildSqlConnectionString();
        void Disconnect();
        void BeginTransaction();
        void CommitTransaction();
        void RollBack();
    }
}
