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
        public string FieldValue { get; set; }
        public string DataType { get; set; }

        public FilterControlSet()
        {
        }
        
        public string ControlSetToString()
        {
            string filter = "";
            string field = FieldName.Replace(" ", Environment.NewLine);


            switch (DataType)
            {
                case "System.String":
                    filter = GetStringFilter();
                    break;
                case "System.Decimal":
                    filter = GetNumberFilter();
                    break;
                case "System.DateTime":
                    filter = GetDateFilter();
                    break;
                default:
                    filter = GetStringFilter();
                    break;
            }

            return filter;
        }

        public string GetStringFilter()
        {

            string filter;
            switch (OperatorValue)
            {
                case "EQUALS":
                    filter = string.Format("[{0}] = '{1}'", FieldName, FieldValue);
                    break;
                case "LIKE":
                    filter = string.Format("[{0}] LIKE '%{1}%'", FieldName, FieldValue);
                    break;
                case "STARTS WITH":
                    filter = string.Format("[{0}] LIKE '{1}%'", FieldName, FieldValue);
                    break;
                case "ENDS WITH":
                    filter = string.Format("[{0}] LIKE '%{1}'", FieldName, FieldValue);
                    break;
                default:
                    filter = string.Format("[{0}] = '{1}'", FieldName, FieldValue);
                    break;
            }
            return filter;
        }

        public string GetNumberFilter()
        {

            string filter;
            switch (OperatorValue)
            {
                case "EQUALS":
                    filter = string.Format("[{0}] = {1}", FieldName, FieldValue);
                    break;
                case "GREATER THAN":
                    filter = string.Format("[{0}] > {1}", FieldName, FieldValue);
                    break;
                case "LESS THAN":
                    filter = string.Format("[{0}] < {1}", FieldName, FieldValue);
                    break;
                default:
                    filter = string.Format("[{0}] = {1}", FieldName, FieldValue);
                    break;
            }
            return filter;
        }

        public string GetDateFilter()
        {

            string filter;
            switch (OperatorValue)
            {
                case "ON":
                    filter = string.Format("[{0}] = {1}", FieldName, FieldValue);
                    break;
                case "AFTER":
                    filter = string.Format("[{0}] > {1}", FieldName, FieldValue);
                    break;
                case "BEFORE":
                    filter = string.Format("[{0}] < {1}", FieldName, FieldValue);
                    break;
                default:
                    filter = string.Format("[{0}] = {1}", FieldName, FieldValue);
                    break;
            }
            return filter;
        }

    }
}
