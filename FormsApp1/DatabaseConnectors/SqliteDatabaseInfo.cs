using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormApp1.DatabaseConnectors
{
    public class SqliteDatabaseInfo 
    {
        public string Path { get; set; }
        public string Filename { get; set; }
    }

    public enum SqliteColumnDataTypeEnum
    {
        INTEGER = 0,
        TEXT,
        BLOB,
        REAL,
        NUMERIC
    }

    public class SqliteColumnInfo
    {
        public string ColumnName { get; set; }
        public SqliteColumnDataTypeEnum Type { get; set; }
        public Boolean NotNull { get; set; }
        public Boolean PrimaryKey { get; set; }
        public Boolean AutoIncrement { get; set; }
        public Boolean Unique { get; set; }
        public String Defult { get; set; }
    }
}
