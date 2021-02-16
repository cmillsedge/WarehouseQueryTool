using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WarehouseQueryTool
{
    public class ProcessDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Outline_Id { get; set; }
        public string Outline_Name { get; set; }
        public List<ProcessContext> Contexts { get; set; }
        public OrclCommand WHConn { get; set; }
        public ProcessDefinition(int id, string name, OrclCommand whconn)
        {
            Id = id;
            Name = name;
            WHConn = whconn;

        }


        public ProcessDefinition()
        {
            Id = -1;
            Name = "";
        }

        public void GetContexts()
        {
            DataTable dt = null;
            try
            {
                int n = 0;
                string query = "SELECT distinct 'Context-' || a.level_no as NAME, a.level_no as LEVELNO, a.parameter_context_id as ID ";
                query = query + " FROM   D_PARAMETERS a, D_PROCESS_VERSIONS c ";
                query = query + " WHERE  a.process_version_id = c.id AND a.usage_mode != 5 ";
                query = query + " AND c.process_definition_id = " + Id.ToString();
                query = query + " ORDER BY  a.level_no ASC";

                dt = WHConn.RunQuery(query);
                List<ProcessContext> contexts = new List<ProcessContext>();

                foreach (DataRow dr in dt.Rows)
                {
                    ProcessContext context = new ProcessContext();
                    context.Outline_Id = Outline_Id;
                    context.Outline_Name = Outline_Name;
                    context.ProcDef_Id = Id;
                    context.ProcDef_Name = Name;
                    context.WHConn = WHConn;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dc.ColumnName == "ID") { context.Id = Int32.Parse(dr[dc].ToString()); }
                        if (dc.ColumnName == "LEVELNO") { context.Level = Int32.Parse(dr[dc].ToString()); }
                        if (dc.ColumnName == "NAME") { context.Name = dr[dc].ToString(); }
                        if (context.Id > 0 & context.Name != "") { context.GetParameters(); }
                    }
                    contexts.Add(context);
                    n += 1;
                }
                Contexts = contexts;
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
