using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLScriptGenerator.Generators
{
    public class StaticSelectClauseGenerator : ISqlScriptGenerator
    {
        public string GenerateScript()
        {
            return String.Format("SELECT ");
        }
    }
}
