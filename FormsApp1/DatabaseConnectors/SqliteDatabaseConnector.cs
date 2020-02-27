using System;
using System.Data.SQLite;
using System.Collections.Generic;

namespace FormApp1.DatabaseConnectors
{
    public class SqliteDatabaseConnector : IDatabaseConnectors<SQLiteDataReader>, IDisposable
    {
        private SQLiteConnection _SQLiteConnection;
        private SQLiteTransaction _SQLiteTransaction;
        private string _dbConnectionString = "";
        private readonly string _path;
        private readonly string _fileName;


        public SqliteDatabaseConnector(string path, string fileName)
        {
            _path = path;
            _fileName = fileName;
            Initialize();
        }
        public void Initialize()
        {
            BuildSqlConnectionString();
            _SQLiteConnection = new SQLiteConnection(_dbConnectionString);
            _SQLiteConnection.Open();
        }
        public void BuildSqlConnectionString()
        {
            string fName = (_fileName.Length == 0) ? "default" : _fileName;
            if(_path.Length == 0)
                _dbConnectionString = $"Data Source ={fName}.db";
            else
                _dbConnectionString = $"Data Source ={_path}/{fName}.db";
            
        }
        public void Dispose()
        {
            this.Disconnect();
            this._SQLiteConnection.Dispose();
            this._SQLiteTransaction.Dispose();
        }
        public void Disconnect()
        {
            _SQLiteConnection.Close();
        }


        public void BeginTransaction()
        {
            this._SQLiteTransaction = this._SQLiteConnection.BeginTransaction();
        }
        public void CommitTransaction()
        {
            if (this._SQLiteTransaction.Connection != null)
            {
                this._SQLiteTransaction.Commit();
                this.Dispose();
            }
        }
        public void RollBack()
        {
            if (this._SQLiteTransaction.Connection != null)
            {
                this._SQLiteTransaction.Rollback();
                this.Dispose();
            }
        }


        public void ExecuteNonQuery(string query)
        {
            this.ExecuteNonQuery(query, new Dictionary<string, object>());
        }
        public void ExecuteNonQuery(string query, Dictionary<string, object> keyValuePairs)
        {
            using(var cmd = new SQLiteCommand())
            {
                cmd.Connection = _SQLiteConnection;
                cmd.Transaction = _SQLiteTransaction;
                foreach (KeyValuePair<string, object> item in keyValuePairs)
                {
                    if ( query.IndexOf(item.Key) > 0 ) 
                        cmd.Parameters.Add(new SQLiteParameter(item.Key, item.Value));
                }
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
            }
        }
        public SQLiteDataReader ExecuteQuery(string query)
        {
            return this.ExecuteQuery(query, new Dictionary<string, object>());
        }
        public SQLiteDataReader ExecuteQuery(string query, Dictionary<string, object> keyValuePairs)
        {
            SQLiteDataReader reader;
            using (var cmd = new SQLiteCommand())
            {
                cmd.Connection = this._SQLiteConnection;
                cmd.Transaction = this._SQLiteTransaction;
                foreach (KeyValuePair<string, object> item in keyValuePairs)
                {
                    if (query.IndexOf(item.Key) > 0)
                        cmd.Parameters.Add(new SQLiteParameter(item.Key, item.Value));
                }
                cmd.CommandText = query;
                reader = cmd.ExecuteReader();
            }
            return reader;
        }

    }
}
