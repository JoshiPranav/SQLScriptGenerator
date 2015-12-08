using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLScriptGenerator.Entities;
using System.Data.SqlClient;
using SQLScriptGenerator.Helpers;

namespace SQLScriptGenerator.Repository
{
    public class ColumnRepository : BaseRepository<ColumnDefinition>, IColumnRepository
    {
        public ColumnRepository(string connectionString)
            : base(connectionString)
        {
        }

        public IEnumerable<ColumnDefinition> GetColumnListWithDetails(string tableName)
        {
            using (var command = new SqlCommand("select COLUMN_NAME as ColumnName, DATA_TYPE as DataType, CHARACTER_MAXIMUM_LENGTH as CharacterLimit, IS_NULLABLE as IsNullable  from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '" + tableName + "'"))
            {
                return GetRecords(command);
            }
        }

        protected override ColumnDefinition PopulateRecord(SqlDataReader reader)
        {
            return new ColumnDefinition
            {
                ColumnName = reader.GetString(0),
                DataType = reader.GetString(1),
                CharacterLimit  = reader.CheckNullAndReturnValue<Int32>(2), //reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                IsNullable = reader.IsDBNull(3) ? string.Empty : reader.GetString(3)
            };
        }
    }
}
