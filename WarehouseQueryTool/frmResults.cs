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
using Excel = Microsoft.Office.Interop.Excel;

namespace WarehouseQueryTool
{
    public partial class frmResults : Form
    {
        private OrclCommand _whconn;
        private DataTable _mainTable;
        public frmResults(OrclCommand WHConn, List<String> Queries)
        {
            _whconn = WHConn;
            InitializeComponent();
            PopulateGrid(Queries);
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
                foreach(DataColumn dc in dt.Columns)
                {
                    if(!(dc.ColumnName == "Expt_Name" || dc.ColumnName == "Process_Ver_Name" || dc.ColumnName == "Method_Name" || dc.ColumnName == "Lot_Name"))
                    { 
                        dc.ColumnName = process + Environment.NewLine + dc.ColumnName;
                    }
                    if(!(_mainTable.Columns.Contains(dc.ColumnName)))
                    {
                        dc.CopyTo(_mainTable);
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

        private void btnFilter_Click(object sender, EventArgs e)
        {
            string rowFilter = "";
            if (txtExpt.Text != "")
            {
                rowFilter += string.Format("[{0}] LIKE '%{1}%'", "Expt_Name", txtExpt.Text);
            }
            if (txtProcess.Text != "")
            {
                if (rowFilter.Length == 0)
                {
                    rowFilter += string.Format("[{0}] LIKE '%{1}%'", "Process_Ver_Name", txtProcess.Text);
                }
                else
                {
                    rowFilter += string.Format("AND [{0}] LIKE '%{1}%'", "Process_Ver_Name", txtProcess.Text);
                }
            }
            if (txtMethod.Text != "")
            {
                if (rowFilter.Length == 0)
                {
                    rowFilter += string.Format("[{0}] LIKE '%{1}%'", "Method_Name", txtMethod.Text);
                }
                else
                {
                    rowFilter += string.Format("AND [{0}] LIKE '%{1}%'", "Method_Name", txtMethod.Text);
                }
            }

            //rowFilter += string.Format(" OR [{0}] = '{1}'", columnName, additionalFilterValue);
            (dgvResults.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            string rowFilter = string.Empty;
            (dgvResults.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        
        private void copyAlltoClipboard()
        {
             dgvResults.SelectAll();
            DataObject dataObj = dgvResults.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void releaseObject(object obj)
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
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "WarehouseQuery-" + System.DateTime.Now.ToString().Replace('/', '-').Replace(':', '-') + ".xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                dgvResults.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
                // Copy DataGridView results to clipboard
                copyAlltoClipboard();

                object misValue = System.Reflection.Missing.Value;
                Excel.Application xlexcel = new Excel.Application();

                xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                Excel.Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
                Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                // Paste clipboard results to worksheet range
                Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[1, 1];
                CR.Select();
                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                //For some reason column A is always blank in the worksheet. ¯\_(ツ)_ /¯
                // Delete blank column A and select cell A1
                Excel.Range delRng = xlWorkSheet.get_Range("A:A").Cells;
                delRng.Delete(Type.Missing);
                xlWorkSheet.get_Range("A1").Select();

                // Save the excel file under the captured location from the SaveFileDialog
                xlWorkBook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlexcel.DisplayAlerts = true;
                xlWorkBook.Close(true, misValue, misValue);
                xlexcel.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlexcel);

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
