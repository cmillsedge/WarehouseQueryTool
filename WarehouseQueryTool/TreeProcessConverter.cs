using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarehouseQueryTool
{
    public static class TreeProcessConverter
    {
        public static List<ProcessDefinition> ProcessesFromTree(TreeView trvSelect)
        {
            try
            {
                
                List<ProcessDefinition> pds = new List<ProcessDefinition>();
                foreach (TreeNode outline in trvSelect.Nodes[0].Nodes)
                {
                    foreach (TreeNode process in outline.Nodes)
                    {
                        int nctx = 0;
                        ProcessDefinition pd = new ProcessDefinition();
                        List<ProcessContext> pcs = new List<ProcessContext>();
                        pd.Name = process.Text;
                        pd.Contexts = pcs;
                        pds.Add(pd);
                        foreach (TreeNode context in process.Nodes)
                        {
                            int nprm = 0;
                            nctx = pds.Count - 1;
                            ProcessContext pc = new ProcessContext();
                            int pos = context.Text.LastIndexOf("-", StringComparison.CurrentCulture);
                            pc.Level = Int32.Parse(context.Text.Substring(pos + 1));
                            pc.Name = "Context";
                            pc.ProcDef_Name = pd.Name;
                            List<Parameter> prms = new List<Parameter>();
                            pc.Parameters = prms;
                            pds[nctx].Contexts.Add(pc);
                            foreach (TreeNode parameter in context.Nodes)
                            {
                                nprm = pds[nctx].Contexts.Count - 1;
                                Parameter prm = new Parameter();
                                prm.ContextLevel = pds[nctx].Contexts[nprm].Level;
                                int posprm = parameter.Text.LastIndexOf(":", StringComparison.CurrentCulture);
                                prm.Name = parameter.Text.Substring(0, posprm);
                                prm.DataType = parameter.Text.Substring(posprm + 1);
                                pds[nctx].Contexts[nprm].Parameters.Add(prm);
                            }
                        }

                    }   
                }
                return pds;
            }
            catch (Exception)
            { throw; }
        }

    }   
}
