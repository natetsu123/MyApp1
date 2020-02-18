using InfrastructureLayer.DB.SQLite.Entity;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.DB.SQLite.Model
{
    /// <summary>
    /// AppSetting Model
    /// </summary>
    public class ParameterModel : BaseClass.CModel
    {
        #region ===== 変数 =====
        /// <summary>
        /// データベース接続文字列
        /// </summary>
        private string connectionString;
        #endregion

        #region ===== コンストラクタ =====
        /// <summary>
        /// AppSetting Model コンストラクタ
        /// </summary>
        /// <param name="arg_dbFileName"></param>
        public ParameterModel(string arg_dbFileName)
        {
            connectionString = "Data Source = " + arg_dbFileName + ".db";

        }
        #endregion



        /// <summary>
        /// Paramter テーブルの作成
        /// </summary>
        /// <returns>True:成功／False:失敗</returns>
        public override bool CreateTable()
        {
            //SQL
            var sql = new System.Text.StringBuilder();
            sql.Append("CREATE TABLE IF NOT EXISTS Parameter ( ");
            sql.Append(" id INTEGER NOT NULL, ");
            sql.Append(" key TEXT NOT NULL, ");
            sql.Append(" value TEXT NOT NULL, ");
            sql.Append(" description TEXT, ");
            sql.Append(" PRIMARY KEY( id )) ");

            //実行
            using (var db = new Manager(connectionString))
            {
                try
                {
                    db.ExecuteNonQuery(sql.ToString());
                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Paramter テーブルの削除
        /// </summary>
        /// <returns>True:成功／False:失敗</returns>
        public override bool DropTable()
        {
            //SQL
            var sql = new System.Text.StringBuilder();
            sql.Append("DROP TABLE IF EXISTS Parameter");
            
            //実行
            using (var db = new Manager(connectionString))
            {
                try
                {
                    db.ExecuteNonQuery(sql.ToString());
                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Parameter テーブルの取得
        /// </summary>
        /// <param name="whereStatement">検索条件</param>
        /// <returns>True:成功／False:失敗</returns>
        public override object[] Select(string whereStatement)
        {
            //SQL
            var sql = new System.Text.StringBuilder();
            sql.Append("SELECT id, key, value, description From Parameter WHERE 1=1 ");
            if (whereStatement != "") sql.Append(whereStatement);
            sql.Append(" Order by id ");

            //実行
            using (var db = new Manager(connectionString))
            {
                try
                {
                    Entity.Parameter[] p = new Entity.Parameter[] { };
                    int iCount;
                    iCount = 0;
                    using (var reader = db.ExecuteQuery(sql.ToString()))
                    {
                        while (reader.Read())
                        {
                            Array.Resize(ref p, iCount);
                            
                            p[iCount].Id = (int)reader["id"];
                            p[iCount].Key = reader["key"].ToString();
                            p[iCount].Value = reader["valuie"].ToString();
                            p[iCount].Description = reader["description"].ToString();
                            iCount += 1;
                        }
                    }
                    return p;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return null;
                }
            }
        }

        /// <summary>
        /// Parameter テーブルの挿入
        /// </summary>
        /// <param name="p">Parameter Entity</param>
        /// <returns>True:成功／False:失敗</returns>
        public bool Insert(Parameter p)
        {
            // 変数
            var ret = true;
            var param = EntityToDictionary(p);

            // SQL
            var sql = new System.Text.StringBuilder();
            sql.Append("INSERT INTO Parameter (key, value, description ) VALUES( @Key, @Value, @Description ) ");

            // 実行
            using ( var db = new Manager(connectionString))
            {
                try
                {
                    db.ExecuteNonQuery(sql.ToString(),param);
                    ret = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    ret = false;
                }
            }
            return ret;
        }


        /// <summary>
        /// Parameter Entity を Dictionary 形式へ変換
        /// </summary>
        /// <param name="p">Parameter Entity</param>
        /// <returns>変換後の Dictionary </returns>
        public Dictionary<string, object> EntityToDictionary(Parameter p)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@Key", p.Key);
            dict.Add("@Value", p.Value);
            dict.Add("@Description", p.Description);
            return dict;
        }
    }
}
