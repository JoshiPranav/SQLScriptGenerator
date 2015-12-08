using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SQLScriptGenerator.Entities
{
    public class ColumnDefinition
    {
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public int CharacterLimit { get; set; }
        public string IsNullable { get; set; }
        public DataColumn IsSensitive
        {
            get
            {
                return new DataColumn("IsSensitive", System.Type.GetType("System.Boolean"));
            }
        }
    }
}
