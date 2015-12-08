using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLScriptGenerator.Generators
{
    public class SqlViewGenerator : ISqlViewGenerator
    {
        public string CreateViewScript(string viewName)
        {
            return String.Format("CREATE VIEW {0} AS", viewName);
        }

        public string DropViewScript(string viewName)
        {
            return String.Format("IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'{0}'))" +
                    "DROP VIEW {0}" +
                    "GO", viewName);
        }
    }
}
