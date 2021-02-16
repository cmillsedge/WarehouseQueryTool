using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Data;

namespace WarehouseQueryTool
{
    public class Outline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProcessDefinition> ProcessDefinitions { get; set; }
        public OrclCommand WHConn { get; set; }

        public Outline(int id , string name, OrclCommand whconn)
        {
            Id = id;
            Name = name;
            WHConn = whconn;
            GetProcessDefinitions();

        }

        public Outline (OrclCommand whconn)
        {
            Id = - 1;
            Name = "";
            WHConn = whconn;
        }

        public void GetProcessDefinitions()
        {
            DataTable dt = null;
            try
            {
                int n = 0;
                string query = "SELECT b.id as process_id, b.name AS process_name ";
                query = query + " FROM   d_process_definitions b";
                query = query + " WHERE  b.outline_id = " + Id.ToString();

                dt = WHConn.RunQuery(query);
                List<ProcessDefinition> pdefs = new List<ProcessDefinition>();

                foreach (DataRow dr in dt.Rows)
                {
                    ProcessDefinition pdef = new ProcessDefinition();
                    pdef.Outline_Id = Id;
                    pdef.Outline_Name = Name;
                    pdef.WHConn = WHConn;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dc.ColumnName == "PROCESS_ID") { pdef.Id = Int32.Parse(dr[dc].ToString()); }
                        if (dc.ColumnName == "PROCESS_NAME") { pdef.Name = dr[dc].ToString(); }
                        if (pdef.Id > 0 & pdef.Name != "") { pdef.GetContexts(); }
                    }
                    pdefs.Add(pdef);
                    n += 1;
                }
                ProcessDefinitions = pdefs;
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

        public Parameter GetParameterById (string uniqueid)
        {

            Parameter param = null;
            try
            {
                int procpos = uniqueid.IndexOf("-");
                int levelpos = uniqueid.LastIndexOf("-");
                int procdefid = Int32.Parse(uniqueid.Substring(0, procpos));
                int levelid = Int32.Parse(uniqueid.Substring((procpos + 1), 1));

                foreach (ProcessDefinition pd in this.ProcessDefinitions)
                {
                    if (pd.Id == procdefid)
                    {
                        foreach (ProcessContext pc in pd.Contexts)
                        {
                            if (pc.Level == levelid)
                            {
                                foreach (Parameter pr in pc.Parameters)
                                {
                                    if (pr.Id == uniqueid)
                                    {
                                        param = pr;
                                        break;

                                    }
                                }
                            }
                        }
                    }
                }
                return param;
            }
            catch (Exception)
            { 
                return param;
                throw;
            }

        }

        

    }
}
