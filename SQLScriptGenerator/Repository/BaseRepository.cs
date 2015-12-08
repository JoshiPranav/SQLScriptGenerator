using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace SQLScriptGenerator.Repository
{
    public abstract class BaseRepository<T> where T : class
    {
        private static SqlConnection _connection;

        public BaseRepository(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        protected abstract T PopulateRecord(SqlDataReader reader);

        protected IEnumerable<T> GetRecords(SqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _connection;
            _connection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                        list.Add(PopulateRecord(reader));
                }
                finally
                {
                   reader.Close();
                }
            }
            finally
            {
                _connection.Close();
            }
            return list;
        }
    }
}
