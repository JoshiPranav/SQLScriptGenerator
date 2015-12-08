using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLScriptGenerator.Helpers
{
    public interface IFileHelper<T>
    {
        T ReadData();
        void WriteData(T data);
    }
}
