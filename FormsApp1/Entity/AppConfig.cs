using FormsApp1.DatabaseConnectors;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsApp1.Entity
{
    public class AppConfig : IEntity
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
}
