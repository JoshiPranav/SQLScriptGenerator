using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SQLScriptGenerator.Helpers
{
    public class TextFileHelper : IFileHelper<string>
    {
        private string _path;

        public TextFileHelper(string path)
        {
            _path = path;
        }

        public string ReadData()
        {
            if (File.Exists(_path))
            {
                return File.ReadAllText(_path);
            }
            return string.Empty;
        }

        public void WriteData(string data)
        {
            using (StreamWriter sw = new StreamWriter(_path, true))
            {
                sw.Write(data);
            }
        }
    }
}
