using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLScriptGenerator.Generators
{
    public class CreateViewScriptGenerator : ISqlScriptGenerator
    {
        private string _viewName;

        public CreateViewScriptGenerator(string viewName)
        {
            _viewName = viewName;
        }

        public string GenerateScript()
        {
            return String.Format("CREATE VIEW {0} AS", _viewName);
        }
    }
}
