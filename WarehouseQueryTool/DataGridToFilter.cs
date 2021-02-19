using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace WarehouseQueryTool
{
    public static class DataGridToFilter
    {
        public static DataTable SetRowOperators (DataTable dt)
        {
            DataTable dtops = dt;
            foreach (DataColumn dc in dt.Columns)
            {
                dt.Rows[0][dc.ColumnName] = " LIKE ";
            }
            return dtops;
        }
    }
}
