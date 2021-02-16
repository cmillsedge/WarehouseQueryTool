using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseQueryTool
{
    class ProcessSQL
    {
        public string Name { get; set; }
        public string Query { get; set; }
        public List<ContextSQL> ContextsSQL { get; set; }
        public ProcessSQL(ProcessDefinition pdef)
        {
            Name = pdef.Name;
            ConvertToContextSQL(pdef.Contexts);
            GetQuerySQL();
        }

        public ProcessSQL()
        {
        }

        public void ConvertToContextSQL(List<ProcessContext> contexts)
        {

            try
            {
                int p = contexts.Count();
                List<ContextSQL> cSQLS = new List<ContextSQL>();
                for (int i = 0; i < p; i++)
                {
                    ContextSQL cSQL = new ContextSQL(contexts[i]);
                    cSQLS.Add(cSQL);
                }
                ContextsSQL = cSQLS;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void GetQuerySQL()
        {

            string outercols = "";
            string inlineviews = "";
            string whereclause = "";
            int loop = 1;
            try
            {
                foreach (ContextSQL cSQL in ContextsSQL)
                {

                    inlineviews += cSQL.ViewSQL;
                    if (loop > 1)
                    { whereclause = " context_" + (cSQL.Level - 1) + ".\"Task_Context_Id\" = context_" + (cSQL.Level) + ".\"Task_Ctx_Parent\" (+) AND"; }
                    
                    foreach (string col in cSQL.Columns)
                    {
                        outercols += col + ",";
                    }
                    loop += 1;
                    
                }
                //string clean up the ends
                outercols = outercols.TrimEnd(',');
                inlineviews = inlineviews.TrimEnd(',');
                //  context_1.task_context_id = context_2.child1_task_ctx_parent;
                if (ContextsSQL.Count() > 1)
                {
                    //remove trailing AND
                    int pos = whereclause.LastIndexOf(" AND", StringComparison.CurrentCulture);
                    whereclause = whereclause.Substring(0, pos);
                    Query = "SELECT " + outercols + " FROM " + inlineviews + " WHERE " + whereclause; 
                }
                else
                { Query = "SELECT " + outercols + " FROM " + inlineviews; }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
