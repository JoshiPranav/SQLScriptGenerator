using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQLScriptGenerator.Repository;
using System.Configuration;
using SQLScriptGenerator.Entities;
using SQLScriptGenerator.Helpers;
using SQLScriptGenerator.Generators;

namespace SQLScriptGenerator
{
    public partial class Form1 : Form
    {
        private readonly ITableRepository _tableRepository;
        private readonly IColumnRepository _columnRepository;
        private readonly IFileHelper<string> _fileHelper;
        private readonly ISqlViewGenerator _sqlViewGenerator;

        public Form1()
        {
            InitializeComponent();
            _tableRepository = new TableRepository(ConfigurationManager.ConnectionStrings["VDBD_DFATPASSConnectionString"].ConnectionString);
            _columnRepository = new ColumnRepository(ConfigurationManager.ConnectionStrings["VDBD_DFATPASSConnectionString"].ConnectionString);
            _fileHelper = new TextFileHelper(ConfigurationManager.AppSettings["SensitiveFieldsFilePath"]);
            _sqlViewGenerator = new SqlViewGenerator();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var tableNameList = _tableRepository.GetTableList();
            cboTables.DataSource = tableNameList;
            cboTables.ValueMember = "TableName";
            cboTables.DisplayMember = "TableName";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedTable = (TableDefinition) cboTables.SelectedItem;
            var columnList = _columnRepository.GetColumnListWithDetails(selectedTable.TableName);

            var storedSensitiveFieldData = _fileHelper.ReadData();
            var storedSensitiveFieldList = storedSensitiveFieldData.Split(',').ToList();

            var table = new DataTable();
            table.Columns.Add("ColumnName", typeof(string));
            table.Columns.Add("DataType", typeof(string));
            table.Columns.Add("CharacterLimit", typeof(int));
            table.Columns.Add("IsNullable", typeof(string));
            table.Columns.Add(new DataColumn("IsSensitive", typeof(bool)));

            foreach (var entity in columnList)
            {
                var row = table.NewRow();
                row["ColumnName"] = entity.ColumnName;
                row["DataType"] = entity.DataType;
                row["CharacterLimit"] = entity.CharacterLimit;
                row["IsNullable"] = entity.IsNullable;
                if (storedSensitiveFieldList.Contains(entity.ColumnName))
                {
                    row["IsSensitive"] = true;
                }
                table.Rows.Add(row);
            }

            grdTableColumns.ReadOnly = false;
            
            grdTableColumns.DataSource = table;
        }

        

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            List<string> sensitiveFieldsList = new List<string>();

            var sbSelectStatement = new StringBuilder(string.Empty);

            sbSelectStatement.Append("SELECT ");
            
            foreach (var row in grdTableColumns.Rows)
            {
                var dgRow = (System.Windows.Forms.DataGridViewRow)(row);
                
                var columnName = dgRow.Cells[0].EditedFormattedValue.ToString();
                    var columnDataType = dgRow.Cells[1].EditedFormattedValue.ToString().ToUpper();
                var isSensitiveColumn = dgRow.Cells[4];
                var isSensitive = Convert.ToBoolean(isSensitiveColumn.EditedFormattedValue);
                if (isSensitive)
                {
                    sbSelectStatement.Append(GetColumnSelectClause(columnDataType, columnName));
                    sensitiveFieldsList.Add(columnName);
                }
                else
                {
                    sbSelectStatement.Append(columnName);
                }
                sbSelectStatement.Append(", ");
            }
            txtResult.Text = sbSelectStatement.ToString();


            var txtSize = TextRenderer.MeasureText(txtResult.Text, txtResult.Font, txtResult.ClientRectangle.Size, TextFormatFlags.WordBreak);
            txtResult.Height = Convert.ToInt32(txtSize.Height+50);

            if (sensitiveFieldsList.Count > 0)
            {
                var sensitiveFieldString = new StringBuilder(String.Join(",", sensitiveFieldsList));
                sensitiveFieldString.Append(",");
                _fileHelper.WriteData(sensitiveFieldString.ToString());
            }
        }

        private string GetColumnSelectClause(string columnDataType, string columnName)
        {
            string selectFieldValue = string.Empty;

            switch (columnDataType)
            { 
                case "VARCHAR":
                    return "'HIDDEN " + columnName + "' as " + columnName;
                case "NVARCHAR":
                    return "'HIDDEN " + columnName + "' as " + columnName;
                case "BOOLEAN":
                    return "0" + " as " + columnName;
                case "INTEGER":
                    return "0" + " as " + columnName;
                case "BIGINT":
                    return "0" + " as " + columnName;
                case "DECIMAL":
                    return "0" + " as " + columnName;
                case "DATE":
                    return DateTime.MinValue.ToShortDateString() + " as " + columnName;
                case "DATETIME":
                    return DateTime.MinValue.ToShortDateString() + " as " + columnName;
                default:
                    throw new Exception(String.Format("Unexpected data type for column {0}, cannot return default for sql data type: {1}", columnName, columnDataType));
            }
        }
    }
}
