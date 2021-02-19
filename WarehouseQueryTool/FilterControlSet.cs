using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseQueryTool
{
    public class FilterControlSet
    {
        public string FieldName { get; set; }
        public string OperatorControl { get; set; }
        public string OperatorValue { get; set; }
        public string FieldControl { get; set; }
        public int FieldValue { get; set; }

        public FilterControlSet()
        {
        }
    }
}
