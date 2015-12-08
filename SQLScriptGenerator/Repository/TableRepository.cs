using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SQLScriptGenerator.Entities;

namespace SQLScriptGenerator.Repository
{
    public class TableRepository :  BaseRepository<TableDefinition>, ITableRepository
    {
        public TableRepository(string connectionString)
            : base(connectionString)
        {
        }

        public IEnumerable<TableDefinition> GetTableList()
        {
            using (var command = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.Tables"))
            {
                return GetRecords(command);
            }
        }

        protected override TableDefinition PopulateRecord(SqlDataReader reader)
        {
            return new TableDefinition
            {
                TableName = reader.GetString(0)
            };
        }
    }
}
