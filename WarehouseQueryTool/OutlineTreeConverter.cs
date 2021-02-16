using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarehouseQueryTool
{
    public static class OutlineTreeConverter
    {
        public static void PopulateTree(Outline[] outlines, TreeView trvPick)
        {
            try
            {
                trvPick.BeginUpdate();

                if (outlines.Length > 0)
                {
                    TreeNode root = (TreeNode)trvPick.Nodes[0];
                    foreach (Outline otn in outlines)
                    {

                        TreeNode outline = new TreeNode(otn.Name);
                        outline.ImageIndex = 0;
                        root.Nodes.Add(outline);

                        foreach (ProcessDefinition pd in otn.ProcessDefinitions)
                        {
                            TreeNode procdef = new TreeNode(pd.Name);
                            procdef.ImageIndex = 2;
                            outline.Nodes.Add(procdef);
                            foreach (ProcessContext pc in pd.Contexts)
                            {
                                TreeNode procctx = new TreeNode(pc.Name);
                                procctx.ImageIndex = 4;
                                procdef.Nodes.Add(procctx);

                                foreach (Parameter pr in pc.Parameters)
                                {
                                    string data_type = GetDataType(pr.DataType);
                                    TreeNode param = new TreeNode(pr.Name + ":" + data_type);
                                    param.Name = pr.Id.ToString();
                                    param.ImageIndex = 1;
                                    procctx.Nodes.Add(param);
                                }
                            }
                        }
                    }
                    trvPick.EndUpdate();
                    trvPick.ExpandAll();

                }
            }
            catch (Exception)
            { throw; }
        }

        public static void AddParamToTree(TreeView trvSel, Outline[] outlines, TreeNode node)
        {
            try
            {
                Parameter param = null;
                for (int i = 0; i < outlines.Length; i++)
                {
                    param = outlines[i].GetParameterById(node.Name);
                    if (param != null)
                    { break; }
                }
                TreeNode root = (TreeNode)trvSel.Nodes[0];
                TreeNode outlinenode = GetNode(root, param, "outline");
                if (outlinenode == null)
                {
                    outlinenode = new TreeNode(param.Outline_Name);
                    outlinenode.ImageIndex = 0;
                    root.Nodes.Add(outlinenode);
                }
                TreeNode processnode = GetNode(outlinenode, param, "process");
                if (processnode == null)
                {
                    processnode = new TreeNode(param.ProcDef_Name);
                    processnode.ImageIndex = 2;
                    outlinenode.Nodes.Add(processnode);
                }
                TreeNode contextnode = GetNode(processnode, param, "context");
                if (contextnode == null)
                {
                    contextnode = new TreeNode(param.ContextName);
                    contextnode.ImageIndex = 4;
                    processnode.Nodes.Add(contextnode);
                }
                TreeNode paramnode = GetNode(contextnode, param, "parameter");
                if (paramnode == null)
                {
                    //int level = param.Level + 1;
                    string data_type = GetDataType(param.DataType);
                    paramnode = new TreeNode(param.Name + ":" + data_type);
                    paramnode.ImageIndex = 1;
                    contextnode.Nodes.Add(paramnode);
                }
                trvSel.ExpandAll();
            }
            catch (Exception)
            { throw; }
        }


        public static TreeNode GetNode(TreeNode parentnode, Parameter param, string property)
        {
            TreeNode tnreturn = null;
            try
            {
                foreach (TreeNode tn in parentnode.Nodes)
                {

                    string data_type = GetDataType(param.DataType);
                    switch (property)
                    {
                        case "outline":
                            if (tn.Text == param.Outline_Name & tn.Level == 1)
                            {
                                tnreturn = tn;
                            }
                            break;
                        case "process":
                            if (tn.Text == param.ProcDef_Name & tn.Level == 2)
                            {
                                tnreturn = tn;
                            }
                            break;
                        case "context":
                            if (tn.Text == param.ContextName & tn.Level == 3)
                            {
                                tnreturn = tn;
                            }
                            break;
                        case "parameter":
                            if (tn.Text == param.Name + ":" + data_type & tn.Level == 4)
                            {
                                tnreturn = tn;
                            }
                            break;
                    }

                }
            }
            catch (Exception)
            { throw; }
            return tnreturn;
        }

        static string GetDataType(string dt)
        {
            string datatype;
            try
            {
                if (dt == "numeric")
                { datatype = "n"; }
                else
                { datatype = "s"; }
            }
            catch (Exception)
            { throw; }
            return datatype;
        }
    }
}
