using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
