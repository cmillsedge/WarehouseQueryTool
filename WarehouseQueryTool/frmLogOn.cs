using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess;

namespace WarehouseQueryTool
{
    public partial class frmLogOn : Form
    {
        public frmLogOn()
        {
            InitializeComponent();
        }

        private void btnLogOn_Click(object sender, EventArgs e)
        {
            OrclCommand WHConn = null;
            DBProperties WHDatabase = null;
            try
            {
                WHDatabase = new DBProperties(txtWHSchema.Text, txtWHPassword.Text, txtWHHost.Text, txtWHPort.Text, txtWHSid.Text);
                WHConn = new OrclCommand(WHDatabase);
                //Open main form here
                using (frmQueryBuilder frmQuery = new frmQueryBuilder(WHConn))
                {
                    frmQuery.Location = this.Location;
                    frmQuery.ShowDialog();
                    this.Hide();
                }
                //frmSel closed re-display logon
                this.Show();

                //Close connections
                WHConn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void LogOn_Load(object sender, EventArgs e)
        {
            try
            {
                DBProperties WHDatabase = DBSerializer.DeSerializeDB("WarehouseDB.whm");

                txtWHHost.Text = WHDatabase.Host;
                txtWHPort.Text = WHDatabase.Port;
                txtWHSchema.Text = WHDatabase.Schema;
                txtWHSid.Text = WHDatabase.SID;
                txtWHPassword.Text = "";
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void LogOn_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DBProperties WHDatabase = new DBProperties(txtWHSchema.Text, txtWHPassword.Text, txtWHHost.Text, txtWHPort.Text, txtWHSid.Text);
                DBSerializer.SerializeDB(WHDatabase, "WarehouseDB.whm");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
