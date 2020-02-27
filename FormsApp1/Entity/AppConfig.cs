using FormApp1.DatabaseConnectors;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormApp1.Entity
{
    public class AppConfig : IEntityData
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }

        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>
            {
                { "@Id", this.Id },
                { "@Key", this.Key },
                { "@Value", this.Value },
                { "@Description", this.Description }
            };
            return keyValues;
        }

        public string ToStringData()
        {
            var text = new StringBuilder();
            text.AppendFormat("Id = {0}", Id);
            text.AppendFormat(", Key = {0}", Key);
            text.AppendFormat(", Value = {0}", Value);
            text.AppendFormat(", Description = {0}", Description);
            return text.ToString();
        }
    }


    public class AppConfigEntity : IEntity<AppConfig, SqliteDatabaseInfo>, IDisposable
    {

        readonly App.Logger Logger = new App.Logger(System.Environment.CurrentDirectory, Encoding.UTF8);

        internal SqliteDatabaseConnector DbConnector { get; set; }
        public string TableName { get => "AppConfig"; }


        public AppConfigEntity(SqliteDatabaseInfo db)
        {
            this.Database(db);

        }
        public void Database(SqliteDatabaseInfo db)
        {
            DbConnector = new SqliteDatabaseConnector(db.Path, db.Filename);
        }
        public void Dispose()
        {
            this.Dispose();
        }


        public bool CreateTable()
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("CREATE TABLE IF NOT EXISTS " + TableName + "( Id INTEGER NOT NULL, Key TEXT NOT NULL, Value TEXT NOT NULL, Description TEXT, PRIMARY KEY(Id) ) ");
            try
            {
                DbConnector.ExecuteNonQuery(sql.ToString());
                Logger.Debug("Succecc Create Table " + TableName);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
        }
        public bool DropTable()
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("DROP TALBE IF EXISTS AppConfig");
            try
            {
                DbConnector.ExecuteNonQuery(sql.ToString());
                Logger.Debug("Succecc Drop Table AppConfig");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
        }


        public AppConfig[] Select()
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("SELECT Id, Key, Value, Description FROM " + TableName);
            sql.Append(" ORDER BY Id");
            try
            {
                var reader = DbConnector.ExecuteQuery(sql.ToString());
                Logger.Debug(sql.ToString());
                AppConfig[] entity = new AppConfig[reader.FieldCount];
                int iCount = 0;
                while (reader.Read())
                {
                    entity[iCount] = new AppConfig();
                    entity[iCount].Id = reader.GetInt32(0);
                    entity[iCount].Key = reader.GetString(1);
                    entity[iCount].Value = reader.GetString(2);
                    entity[iCount].Description = reader.GetString(3);
                }
                return entity;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message + " " + sql.ToString());
                throw new Exception(ex.Message);
            }
        }
        public bool Insert(AppConfig entity)
        {
            var sql = new System.Text.StringBuilder();
            bool ret;
            sql.Append("INSERT INTO " + TableName + " (Key, Value, Description) VALUES ( @Key, @Value, @Description ) ");
            try
            {
                DbConnector.ExecuteNonQuery(sql.ToString(), entity.ToDictionary());
                Logger.Debug(sql.ToString() + " " + entity.ToStringData());
                ret = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message + " " + sql.ToString() + " " + entity.ToStringData());
                throw new Exception(ex.Message);
            }
            return ret;

        }
        public bool Delete(AppConfig entity)
        {
            var sql = new System.Text.StringBuilder();
            bool ret;
            sql.Append("DELETE FROM " + TableName + " WHERE Id = @Id ");
            try
            {
                DbConnector.ExecuteNonQuery(sql.ToString(), entity.ToDictionary());
                Logger.Debug(sql.ToString() + " " + entity.ToStringData());
                ret = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message + " " + sql.ToString() + " " + entity.ToStringData());
                throw new Exception(ex.Message);
            }
            return ret;
        }

        public bool Update(AppConfig entity)
        {
            var sql = new System.Text.StringBuilder();
            bool ret;
            sql.Append("UPDATE " + TableName);
            sql.Append(" SET Key = @Key, Value = @Value, Description = @Description ");
            sql.Append(" WHERE Id = @Id ");
            try
            {
                DbConnector.ExecuteNonQuery(sql.ToString(), entity.ToDictionary());
                Logger.Debug(sql.ToString() + " " + entity.ToStringData());
                ret = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message + " " + sql.ToString() + " " + entity.ToStringData());
                throw new Exception(ex.Message);
            }
            return ret;
        }
        public bool Truncate()
        {
            var sql = new System.Text.StringBuilder();
            bool ret;
            sql.Append("DELETE FROM " + TableName);
            try
            {
                DbConnector.ExecuteNonQuery(sql.ToString());
                ret = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message + " " + sql.ToString());
                throw new Exception(ex.Message);
            }
            return ret;
        }
    }
}
