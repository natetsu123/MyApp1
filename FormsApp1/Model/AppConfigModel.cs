using FormsApp1.DatabaseConnectors;
using FormsApp1.Entity;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsApp1.Model
{
    public class AppConfigModel : IModel<AppConfig, SqliteDatabaseInfo>, IDisposable
    {
        readonly App.Logger Logger = new App.Logger(System.Environment.CurrentDirectory, Encoding.UTF8);

        internal SqliteDatabaseConnector DbConnector { get; set; }
        public string TableName { get => "AppConfig"; }


        public AppConfigModel(SqliteDatabaseInfo db)
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public bool Update(AppConfig entity)
        {
            throw new NotImplementedException();
        }
        public bool Truncate()
        {
            throw new NotImplementedException();
        }


    }
}
