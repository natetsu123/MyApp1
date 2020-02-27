using System;
using System.Collections.Generic;

namespace FormApp1.DatabaseConnectors
{
    interface IDatabaseConnectors<Reader> : IDisposable
    {
        Reader ExecuteQuery(string query);
        Reader ExecuteQuery(string query, Dictionary<string, object> keyValuePairs);
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
