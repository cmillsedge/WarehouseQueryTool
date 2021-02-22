using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Drawing;
using System.Reflection;

namespace WarehouseQueryTool
{
    public partial class frmQueryBuilder : Form
    {
        private readonly OrclCommand _whconn;
        private readonly Outline[] outlines;
        public frmQueryBuilder(OrclCommand WHConn)
        {
            _whconn = WHConn;
            InitializeComponent();
            try
            { 
                TreeNode srcroot = new TreeNode("Warehouse");
                TreeNode selroot = new TreeNode("Warehouse");
                srcroot.ImageIndex = 3;
                selroot.ImageIndex = 3;
                trvPick.Nodes.Add(srcroot);
                trvSelect.Nodes.Add(selroot); ;
                outlines = GetOutlines();
                OutlineTreeConverter.PopulateTree(outlines, trvPick);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

        }

        public Outline[] GetOutlines()
        {
            int n = 0;
            Outline[] outlines = null;
            try
            {
                string query = "SELECT a.src_system_id as outline_id, a.name AS outline_name ";
                query += " FROM  d_outlines a";

                DataTable dt = _whconn.RunQuery(query);
                outlines = new Outline[dt.Rows.Count];

                //set up the array
                for (int i = 0; i < outlines.Length; i++) { outlines[i] = new Outline(_whconn); }

                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dc.ColumnName == "OUTLINE_ID") { outlines[n].Id = int.Parse(dr[dc].ToString(), new CultureInfo("en-US")); }
                        if (dc.ColumnName == "OUTLINE_NAME") { outlines[n].Name = dr[dc].ToString(); }
                        if (outlines[n].Id > 0 & outlines[n].Name.Length > 0) { outlines[n].GetProcessDefinitions(); }
                    }
                    n += 1;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            return outlines;

        }

        private void TreeviewItemDrag(object sender,ItemDragEventArgs e)
        {
            // Initiate drag/drop
            TreeNode nod = (TreeNode)e.Item;
            DoDragDrop(e.Item, DragDropEffects.Move);
        }



        private void TreeviewDragEnter(object sender, DragEventArgs e)
        {
            // Set the visual effect
            e.Effect = DragDropEffects.Move;
        }




        // Handle the dragdrop event

        public void TreeviewDragDrop(object sender, DragEventArgs e)
        {
            // e contains the data of the dragged items. See if has a
            // node data structure in it.
            try
            {
                TreeNode node = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                MoveNode( node);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                

        }

        public void MoveNode(TreeNode node)
        {
            // e contains the data of the dragged items. See if has a
            // node data structure in it.
            try
            {
                if (node.Level == 4)
                {
                    OutlineTreeConverter.AddParamToTree(trvSelect, outlines, node);
                }
                else if (node.Level == 3)
                {
                    foreach (TreeNode parameter in node.Nodes)
                    {
                        OutlineTreeConverter.AddParamToTree(trvSelect, outlines, parameter);
                    }
                }
                else if (node.Level == 2)
                {
                    foreach (TreeNode ctx in node.Nodes)
                    {
                        foreach (TreeNode parameter in ctx.Nodes)
                        {
                            OutlineTreeConverter.AddParamToTree(trvSelect, outlines, parameter);
                        }
                    }
                }
                //if we remove then undoing errors becomes difficult
                //trvPick.Nodes.Remove(node);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void btnRunQuery_Click(object sender, EventArgs e)
        {
            try
            {
                List<ProcessDefinition>  pds = TreeProcessConverter.ProcessesFromTree(trvSelect);
                List<String> Queries = new List<String>();
                foreach (ProcessDefinition pd in pds)
                {
                    ProcessSQL ps = new ProcessSQL(pd);
                    Console.WriteLine(ps.Query);
                    Queries.Add(ps.Query);
                }
                using (frmResults frmResults = new frmResults(_whconn, Queries))
                {
                    frmResults.Location = this.Location;
                    frmResults.ShowDialog();
                    this.Hide();
                }
                //frmSel closed re-display logon
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (trvSelect.SelectedNode != null)
            {
                TreeNode node = trvSelect.SelectedNode;
                trvSelect.Nodes.Remove(node);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (trvPick.SelectedNode != null)
            {
                TreeNode node = trvPick.SelectedNode;
                MoveNode(node);
            }
        }

        private void trvSelect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (trvSelect.SelectedNode != null)
                {
                    TreeNode node = trvSelect.SelectedNode;
                    trvSelect.Nodes.Remove(node);
                }
                e.Handled = true;
            }
        }
    }
}
