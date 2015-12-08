using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLScriptGenerator.Generators
{
    public class DropViewScriptGenerator : ISqlScriptGenerator
    {
        private string _viewName;

        public DropViewScriptGenerator(string viewName)
        {
            _viewName = viewName;
        }

        public string GenerateScript()
        {
            return String.Format("IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'{0}'))" +
                        "DROP VIEW {0}" +
                        "GO", _viewName);
        }
    }
}
