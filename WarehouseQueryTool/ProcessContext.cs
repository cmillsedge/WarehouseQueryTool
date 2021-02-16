using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WarehouseQueryTool
{
    public class ProcessContext
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public int Outline_Id { get; set; }
        public string Outline_Name { get; set; }
        public int ProcDef_Id { get; set; }
        public string ProcDef_Name { get; set; }
        public List<Parameter> Parameters  { get; set; }
    public OrclCommand WHConn { get; set; }
        public ProcessContext(int id, string name, OrclCommand whconn)
        {
            Id = id;
            Name = name;
            WHConn = whconn;


        }

        public ProcessContext()
        {
            Id = -1;
        }

        public void GetParameters()
        {
            DataTable dt = null;
            try
            {
                int n = 0;
                string query = "SELECT row_number() over (order by a.level_no, a.name, a.data_type_name) AS ID, ";
                query = query + " a.level_no, a.name as parameter_name, a.data_type_name as data_type ";
                query = query + " FROM   D_PARAMETERS a, D_PROCESS_VERSIONS c ";
                query = query + " WHERE  a.process_version_id = c.id AND a.usage_mode != 5 ";
                query = query + " AND c.process_definition_id = " + ProcDef_Id.ToString();
                query = query + " AND a.level_no = " + Level.ToString();
                query = query + " ORDER BY  a.level_no, a.name ";

                dt = WHConn.RunQuery(query);
                List<Parameter> parameters = new List<Parameter>();
                //set up the array
                foreach (DataRow dr in dt.Rows)
                {
                    Parameter parameter = new Parameter();
                    parameter.Outline_Id = Outline_Id;
                    parameter.ProcDef_Id = ProcDef_Id;
                    parameter.Outline_Name = Outline_Name;
                    parameter.ProcDef_Name = ProcDef_Name;
                    parameter.ContextName = Name;
                    parameter.WHConn = WHConn;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dc.ColumnName == "ID") { parameter.Id = ProcDef_Id + "-" + Level + "-" + dr[dc].ToString(); }
                        if (dc.ColumnName == "LEVEL_NO") { parameter.ContextLevel = Int32.Parse(dr[dc].ToString()); }
                        if (dc.ColumnName == "PARAMETER_NAME") { parameter.Name = dr[dc].ToString(); }
                        if (dc.ColumnName == "DATA_TYPE") { parameter.DataType = dr[dc].ToString(); }
                    }
                    parameters.Add(parameter);
                    n += 1;
                }
                Parameters = parameters;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dt.Dispose();
            }
        }
    }
}
