using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseQueryTool
{
    class ContextSQL
    {
        public string Name { get; set; }
        public string ColumnSQL { get; set; }
        public string ViewSQL { get; set; }
        public string ProcessName { get; set; }
        public int Level { get; set; }
        public int Levels { get; set; }
        //public ParameterSQL[] ParametersSQL { get; set; }
        public List<ParameterSQL> ParametersSQL { get; set; }
        public string[] Columns { get; set; }

        public ContextSQL(ProcessContext context, int ProcessLevels)
        {
            Name = context.Name;
            ProcessName = context.ProcDef_Name;
            Level = context.Level;
            Levels = ProcessLevels;
            ConvertToParameterSQL(context.Parameters);
            GetColumns(context.Parameters);
            GetContextSQL();
        }

        public ContextSQL()
        {
        }

        public void ConvertToParameterSQL(List<Parameter> Params)
        {

            try
            {
                int p = Params.Count();
                List<ParameterSQL> pSQLS = new List<ParameterSQL>();
                for (int i = 0; i < p; i++)
                {
                    ParameterSQL pSQL = new ParameterSQL(Params[i]);
                    pSQLS.Add(pSQL);
                }
                ParametersSQL = pSQLS;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void GetColumns(List<Parameter> Params)
        {

            try
            {
                int p = Params.Count();
                if (Level == 0 || Levels == 1)
                {
                    p += 4;
                }

                string[] cols = new string[p];
                string col = "";
                for (int i = 0; i < p; i++)
                {
                    if (Level == 0 || Levels == 1)
                    {
                    switch (i)
                        {
                            case 0:
                                col = "context_" + Level + "." + (char)34 + "Expt_Name" + (char)34;
                                break;
                            case 1:
                                col = "context_" + Level + "." + (char)34 + "Process_Ver_Name" + (char)34;
                                break;
                            case 2:
                                col = "context_" + Level + "." + (char)34 + "Method_Name" + (char)34;
                                break;
                            case 3:
                                col = "context_" + Level + "." + (char)34 + "Lot_Name" + (char)34;
                                break;
                            default:
                                //col = "context_" + Level + "." + (char)34 + Params[i - 3].Name + (char)34;
                                col = "context_" + Level + "." + (char)34 + Params[i - 4].Name + (char)34;
                                break;
                        }
                    }
                    else
                    {
                       col = "context_" + Level + "." + (char)34 + Params[i].Name + (char)34;
                    }
                    
                    cols[i] = col;
                }
                Columns = cols;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void GetContextSQL()
        {

            string builder;
            try
            {
                //non parameter columns
                builder = "(SELECT b.name AS \"Process_Ver_Name\", d.name AS \"Expt_Name\", i.name as \"Method_Name\", d.id AS \"Exp_Id\", h.name AS \"Lot_Name\", e.id AS \"Task_Context_Id\",";
                builder += "e.label AS \"Task_Label\", e.parent_id AS \"Task_Ctx_Parent\", ";
                foreach(ParameterSQL prs in ParametersSQL)
                {
                    //parameter columns
                    builder += prs.PivotClause;
                }
                builder = builder.TrimEnd(',');
                //tables
                builder += " FROM f_results a, d_process_versions b, d_parameters  c, d_tasks d, d_task_contexts e, d_process_definitions g, s_lots h, d_outline_methods i ";
                //joins
                builder += " WHERE a.parameter_id = c.id AND c.process_version_id = b.id AND a.task_id = d.id AND a.lot_id = h.id AND e.task_id = d.id ";
                builder += " AND d.id = a.task_id AND a.task_context_id = e.id AND b.process_definition_id = g.id AND a.outline_method_id = i.id(+) ";
                //filters
                builder += " AND g.name = '" + ProcessName + "' AND c.level_no = " + Level + " ";
                //group by 
                builder += "  GROUP BY b.name, d.name, i.name, d.id, h.name, e.id, e.label, e.parent_id)";
                //alias 
                builder += " context_" + Level + ",";
                ViewSQL = builder;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }



    }
}
