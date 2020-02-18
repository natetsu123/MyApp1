using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace InfrastructureLayer.DB
{
    public class SQLiteDataAccess : IDataBase, ISQLite, IDisposable
    {
        private SQLiteConnection SQLiteConnection;
        private SQLiteTransaction SQLiteTransaction;

        public SQLiteDataAccess(string connectionString)
        {
            SQLiteConnection = new SQLiteConnection(connectionString);
            SQLiteConnection.Open();
        }
        public void Dispose()
        {
            this.Close();
            this.SQLiteConnection.Dispose();
        }
                       
        public void BeginTransaction()
        {
            this.SQLiteTransaction = this.SQLiteConnection.BeginTransaction();
        }
        public void CommitTransaction()
        {
            if (this.SQLiteTransaction.Connection != null)
            {
                this.SQLiteTransaction.Commit();
                this.SQLiteConnection.Dispose();
            }
        }
        public void Close()
        {
            this.SQLiteConnection.Close();
            this.SQLiteConnection.Dispose();
        }
        public void RollBack()
        {
            if (this.SQLiteTransaction.Connection != null)
            {
                this.SQLiteTransaction.Rollback();
                this.SQLiteConnection.Dispose();
            }
        }
        
        public Dictionary<string, string> GetColumnInfo(string table_name, string column_name)
        {
            Dictionary<string, string> ret_data = new Dictionary<string, string>();
            var sql = new System.Text.StringBuilder();
            sql.Append(" SELECT * FROM pragma_table_info(@table_name);");
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@table_name", table_name);
            try
            {
                SQLiteDataReader reader = ExecuteQuery(sql.ToString(), param);
                while (reader.Read())
                {
                    var cname = reader["name"].ToString();
                    if (cname == column_name)
                    {
                        ret_data.Add("cid", reader["cid"].ToString());
                        ret_data.Add("name", reader["name"].ToString());
                        ret_data.Add("type", reader["type"].ToString());
                        ret_data.Add("notnull", reader["notnull"].ToString());
                        ret_data.Add("dflt_value", reader["dflt_value"].ToString());
                        ret_data.Add("pk", reader["pk"].ToString());
                        break;
                    }
                }
                return ret_data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                return new Dictionary<string, string>();
            }
        }
        public List<string> GetColumns(string table_name)
        {
            List<string> ret_data = new List<string>();
            var sql = new System.Text.StringBuilder();
            sql.Append(" SELECT * FROM pragma_table_info(@table_name);");
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@table_name", table_name);
            try
            {
                SQLiteDataReader reader = ExecuteQuery(sql.ToString(), param);
                while (reader.Read())
                {
                    ret_data.Add((string)reader["name".ToString()]);
                }
                return ret_data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                return new List<string> { };
            }
        }
        public List<string> GetPrimaryKey(string table_name)
        {
            List<string> ret_data = new List<string>();
            var sql = new System.Text.StringBuilder();
            sql.Append(" SELECT * FROM pragma_table_info(@table_name);");
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@table_name", table_name);
            try
            {
                SQLiteDataReader reader = ExecuteQuery(sql.ToString(), param);
                while (reader.Read())
                {
                    if (reader["pk"].ToString() == "1")
                    {
                        ret_data.Add(reader["name"].ToString());
                    }
                }
                return ret_data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                return new List<string>();
            }
        }
        public List<string> GetTables()
        {
            List<string> ret_data = new List<string>();
            var sql = new System.Text.StringBuilder();
            sql.Append(" SELECT tbl_name FROM sqlite_master WHERE type='table' order by 1 ");
            try
            {
                SQLiteDataReader reader = ExecuteQuery(sql.ToString());
                while (reader.Read())
                {
                    ret_data.Add(reader["tbl_name"].ToString());
                }
                return ret_data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                return new List<string> { };
            }
        }
        
        public SQLiteDataReader ExecuteQuery(string query)
        {
            return this.ExecuteQuery(query, new Dictionary<string, object>());
        }
        public SQLiteDataReader ExecuteQuery(string query, Dictionary<string, object> keyValues)
        {
            SQLiteDataReader reader;
            using (var cmd = new SQLiteCommand())
            {
                cmd.Connection = this.SQLiteConnection;
                cmd.Transaction = this.SQLiteTransaction;

                cmd.CommandText = query;
                foreach (KeyValuePair<string, object> item in keyValues)
                {
                    cmd.Parameters.Add(new SQLiteParameter(item.Key, item.Value));
                }
                reader = cmd.ExecuteReader();
            }
            return reader;
        }
        public void ExecuteNonQuery(string query)
        {
            this.ExecuteNonQuery(query, new Dictionary<string, object>());
        }
        public void ExecuteNonQuery(string query, Dictionary<string, object> keyValues)
        {
            using (var cmd = new SQLiteCommand())
            {
                cmd.Connection = this.SQLiteConnection;
                cmd.Transaction = this.SQLiteTransaction;

                cmd.CommandText = query;
                foreach (KeyValuePair<string, object> item in keyValues)
                {
                    cmd.Parameters.Add(new SQLiteParameter(item.Key, item.Value));
                }
                cmd.ExecuteNonQuery();
            }
        }


    }
}
