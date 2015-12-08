using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SQLScriptGenerator.Helpers
{
    public static class DataReaderExtensions
    {
        public static T CheckNullAndReturnValue<T>(this SqlDataReader reader, int ordinal)
        {
            if (reader.IsDBNull(ordinal))
            {
                return default(T);
            }

            return (T)reader.GetValue(ordinal);
        }
    }
}
