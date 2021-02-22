using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseQueryTool
{
    public static class DataColumnExtensions
    {
        public static DataColumn CopyTo(this DataColumn column, DataTable table, bool forceString)
        {

            DataColumn newColumn = null;
           
            if (column == null)
            {
                throw new ArgumentNullException(nameof(column));
            }
            else if (table == null)
            {
                throw new ArgumentNullException(nameof(table));
            }
            else
            {
                if (forceString)
                { newColumn = new DataColumn(column.ColumnName, Type.GetType("System.String"), column.Expression, column.ColumnMapping); }
                else
                { newColumn = new DataColumn(column.ColumnName, column.DataType, column.Expression, column.ColumnMapping); }
                newColumn.AllowDBNull = column.AllowDBNull;
                newColumn.AutoIncrement = column.AutoIncrement;
                newColumn.AutoIncrementSeed = column.AutoIncrementSeed;
                newColumn.AutoIncrementStep = column.AutoIncrementStep;
                newColumn.Caption = column.Caption;
                newColumn.DateTimeMode = column.DateTimeMode;
                newColumn.DefaultValue = column.DefaultValue;
                newColumn.MaxLength = column.MaxLength;
                newColumn.ReadOnly = column.ReadOnly;
                newColumn.Unique = column.Unique;

                table.Columns.Add(newColumn);
            }

            return newColumn;

        }

        public static DataColumn CopyColumnTo(this DataTable sourceTable, string columnName, DataTable destinationTable, bool forceString)
        {


            if (sourceTable == null)
            {
                throw new ArgumentNullException(nameof(sourceTable));
            }
            else
            {
                if (sourceTable.Columns.Contains(columnName))
                {
                    return sourceTable.Columns[columnName].CopyTo(destinationTable, forceString);
                }
                else
                {
                    throw new ArgumentException("The specified column does not exist", "columnName");
                }
            }
        }
    }
}
