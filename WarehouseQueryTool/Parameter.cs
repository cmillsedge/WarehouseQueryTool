using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WarehouseQueryTool
{
    public class Parameter
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int ContextLevel { get; set; }
        public string ContextName { get; set; }
        public string DataType { get; set; }
        public int Outline_Id { get; set; }
        public string Outline_Name { get; set; }
        public int ProcDef_Id { get; set; }
        public string ProcDef_Name { get; set; }
        public OrclCommand WHConn { get; set; }

        public Parameter(string id, string name, string data_type, OrclCommand whconn)
        {
            Id = id;
            Name = name;
            DataType = data_type;
            WHConn = whconn;

        }

        public Parameter()
        {
            Id = "-1";
            Name = "";
        }



    }
}
