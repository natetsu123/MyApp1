using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.DB.SQLite
{
    /// <summary>
    /// データベース操作管理クラス
    /// </summary>
    internal class Manager : IDisposable
    {
        private SQLiteConnection sqlConnection;
        private SQLiteTransaction sqlTransaction;


        #region === コンストラクタ／デストラクタ ===
        /// <summary>
        /// コンストラクタ（ＤＢ接続）
        /// </summary>
        /// <param name="connectionString">データソース</param>
        public Manager(string connectionString)
        {
            sqlConnection = new SQLiteConnection(connectionString);
            sqlConnection.Open();

        }
        public void Dispose()
        {
            this.Close();
            this.sqlConnection.Dispose();
        }
        #endregion 


        #region === 切断／トランザクション（開始／コミット／ロールバック）===



        /// <summary>
        /// 切断
        /// </summary>
        public void Close()
        {
            this.sqlConnection.Close();
            this.sqlConnection.Dispose();
        }


        /// <summary>
        /// トランザクション　開始
        /// </summary>
        public void BeginTransaction()
        {
            this.sqlTransaction = this.sqlConnection.BeginTransaction();
        }


        /// <summary>
        /// トランザクション　コミット
        /// </summary>
        public void CommitTransaction()
        {
            if (this.sqlTransaction.Connection != null)
            {
                this.sqlTransaction.Commit();
                this.sqlConnection.Dispose();
            }
        }


        /// <summary>
        /// トランザクション　ロールバック
        /// </summary>
        public void RollBack()
        {
            if (this.sqlTransaction.Connection != null)
            {
                this.sqlTransaction.Rollback();
                this.sqlConnection.Dispose();
            }
        }
        #endregion


        #region === クエリ実行（アウトプット取得）===

        /// <summary>
        /// クエリ実行（アウトプットあり）
        /// </summary>
        /// <param name="query">SQL文</param>
        /// <returns></returns>
        public SQLiteDataReader ExecuteQuery(string query)
        {
            return this.ExecuteQuery(query, new Dictionary<string, object>());
        }

        /// <summary>
        /// クエリ実行（アウトプットあり）
        /// </summary>
        /// <param name="query">SQL文</param>
        /// <param name="paramDict">SQLパラメータ</param>
        /// <returns></returns>
        public SQLiteDataReader ExecuteQuery(string query, Dictionary<string, object> paramDict)
        {
            SQLiteDataReader reader;
            using (var cmd = new SQLiteCommand())
            {
                cmd.Connection = this.sqlConnection;
                cmd.Transaction = this.sqlTransaction;

                cmd.CommandText = query;
                foreach (KeyValuePair<string, object> item in paramDict)
                {
                    cmd.Parameters.Add(new SQLiteParameter(item.Key, item.Value));
                }
                reader = cmd.ExecuteReader();
            }
            return reader;
        }


        #endregion


        #region === クエリ実行（アウトプットなし）===

        /// <summary>
        /// クエリ実行（アウトプットなし）
        /// </summary>
        /// <param name="query">SQL文</param>
        public void ExecuteNonQuery(string query)
        {
            this.ExecuteNonQuery(query, new Dictionary<string, object>());
        }

        /// <summary>
        /// クエリ実行（アウトプットなし）
        /// </summary>
        /// <param name="query">SQL文</param>
        /// <param name="paramDict">SQLパラメータ</param>
        public void ExecuteNonQuery(string query, Dictionary<string, object> paramDict)
        {
            using (var cmd = new SQLiteCommand())
            {
                cmd.Connection = this.sqlConnection;
                cmd.Transaction = this.sqlTransaction;

                cmd.CommandText = query;
                foreach (KeyValuePair<string, object> item in paramDict)
                {
                    cmd.Parameters.Add(new SQLiteParameter(item.Key, item.Value));
                }
                cmd.ExecuteNonQuery();
            }
        }

        #endregion


    }
}
