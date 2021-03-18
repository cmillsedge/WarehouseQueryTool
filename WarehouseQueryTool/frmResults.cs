using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WarehouseQueryTool
{
    public partial class frmResults : Form
    {
        private OrclCommand _whconn;
        private DataTable _mainTable;
        private BindingList<TypedColumn> _columns;
        private List<FilterControlSet> _filterControlSets;
        public frmResults(OrclCommand WHConn, List<String> Queries)
        {
            _whconn = WHConn;
            _filterControlSets = new List<FilterControlSet>();
            InitializeComponent();
            PopulateGrid(Queries);
            PopulateCols();
            dgvResults.AutoResizeColumns();
            dgvResults.AllowUserToAddRows = false;
        }

        public void PopulateGrid(List<String> Queries)
        {
            try
            {
                _mainTable = new DataTable();
                foreach (string query in Queries)
                {
                    DataTable dt = RunQuery(query);
                    string process = dt.Rows[0]["Process_Ver_Name"].ToString();
                    AddColumns(dt, process);
                    AddRows(dt, process);
                    dt.Dispose();
                }
                
                dgvResults.DataSource = _mainTable;
                dgvResults.Columns[0].Frozen = true;
                dgvResults.Columns[1].Frozen = true;
                dgvResults.Columns[2].Frozen = true;
                dgvResults.Columns[3].Frozen = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public void PopulateCols()
        {
            try
            {
                _columns = new BindingList<TypedColumn>();
                for (int i = 0; i < _mainTable.Columns.Count; i++)
                {
                    TypedColumn tc = new TypedColumn();
                    tc.FieldName = _mainTable.Columns[i].ColumnName.Replace(Environment.NewLine, " | ");
                    tc.DataType = _mainTable.Columns[i].DataType.ToString();
                    _columns.Add(tc);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public DataTable RunQuery(string Query)
        {

            try
            {
                DataTable dt = _whconn.RunQuery(Query);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                DataTable dt2 = new DataTable();
                return dt2;
            }
        }

        public void AddColumns(DataTable dt, string process)
        {

            try
            {
                //DataColumn df = new DataColumn("Filter Operations", System.Type.GetType("System.String"));
                foreach (DataColumn dc in dt.Columns)
                {
                    dc.ColumnName = dc.ColumnName.TrimStart(' ').TrimEnd(' ');
                    if(!(dc.ColumnName == "Expt_Name" || dc.ColumnName == "Process_Ver_Name" || dc.ColumnName == "Method_Name" || dc.ColumnName == "Lot_Name"))
                    { 
                        dc.ColumnName = process + Environment.NewLine + dc.ColumnName;
                    }
                    if(!(_mainTable.Columns.Contains(dc.ColumnName)))
                    {
                        dc.CopyTo(_mainTable, false);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddRows(DataTable dt, string process)
        {

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow drnew = _mainTable.NewRow();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        drnew.BeginEdit();
                        drnew[dc.ColumnName] = dr[dc.ColumnName];
                        drnew.EndEdit();
                    }
                    _mainTable.Rows.Add(drnew);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnDefinition_Click(object sender, EventArgs e)
        {
            using (frmFilter frmFilter = new frmFilter(_filterControlSets, _columns))
            {
                frmFilter.Location = this.Location;
                frmFilter.ShowDialog();
                _filterControlSets = frmFilter.filters;
            }
            ApplyFilter();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            string filter = String.Empty;
            if (_filterControlSets != null)
            {
                for (int i = 0; i < _filterControlSets.Count; i++)
                {
                    if (i == 0)
                    {
                        filter += _filterControlSets[i].ControlSetToString();
                    }
                    else
                    {
                        filter += " AND " + _filterControlSets[i].ControlSetToString();
                    }
                }
                (dgvResults.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                (dgvResults.DataSource as DataTable).DefaultView.RowFilter = filter;
            }
        }

        

        private void btnClear_Click(object sender, EventArgs e)
        {
            string rowFilter = string.Empty;
            (dgvResults.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            //this.Dispose();
        }

        
        private void CopyAlltoClipboard()
        {
            dgvResults.SelectAll();
            DataObject dataObj = dgvResults.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occurred while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Tab Text File (*.tsv)|*.tsv";
            sfd.FileName = "WarehouseQuery-" + System.DateTime.Now.ToString().Replace('/', '-').Replace(':', '-') + ".tsv";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                dgvResults.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
                // Copy DataGridView results to clipboard
                CopyAlltoClipboard();

                File.WriteAllText(sfd.FileName, Clipboard.GetText());

                // Clear Clipboard and DataGridView selection
                Clipboard.Clear();
                dgvResults.ClearSelection();

                // Open the newly saved excel file
                if (File.Exists(sfd.FileName))
                    System.Diagnostics.Process.Start(sfd.FileName);
            }
        }

        
    }
}
