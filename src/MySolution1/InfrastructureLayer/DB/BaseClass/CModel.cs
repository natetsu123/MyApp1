using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.DB.BaseClass
{
    /// <summary>
    /// データベースアクセス Model の基底クラス
    /// </summary>
    public  class CModel
    {
        /// <summary>
        /// テーブル作成
        /// </summary>
        /// <returns></returns>
        public  virtual bool CreateTable() { return false; }

        /// <summary>
        /// テーブル削除
        /// </summary>
        /// <returns></returns>
        public virtual bool DropTable() { return false; }

        /// <summary>
        /// データ取得
        /// </summary>
        /// <param name="paramDict">検索条件</param>
        /// <returns>Entityクラス</returns>
        public virtual object[] Select(string whereStatement) { return null; }

        /// <summary>
        /// データ挿入
        /// </summary>
        /// <param name="obj">Entityクラス</param>
        /// <returns>True:成功／False:失敗</returns>
        public virtual bool Insert(object obj) { return false; }

        /// <summary>
        /// データ更新
        /// </summary>
        /// <param name="obj">Entityクラス</param>
        /// <returns>True:成功／False:失敗</returns>
        public virtual bool Update(object obj) { return false; }

        /// <summary>
        /// データ削除
        /// </summary>
        /// <param name="obj">Entityクラス</param>
        /// <returns>True:成功／False:失敗</returns>
        public virtual bool Delete(object obj) { return false; }

        /// <summary>
        /// データ一括削除
        /// </summary>
        /// <returns>True:成功／False:失敗</returns>
        public virtual bool Truncate() { return false; }
    }
}
