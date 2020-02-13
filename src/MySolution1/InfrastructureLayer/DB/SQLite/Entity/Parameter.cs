using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.DB.SQLite.Entity
{
    /// <summary>
    /// SQLite Parameter レコードクラス
    /// </summary>
    public class Parameter
    {
        #region ===== プロパティ =====

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 一意の Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 設定値
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 設定値の説明
        /// </summary>
        public string Description { get; set; }

        #endregion
    }
}
