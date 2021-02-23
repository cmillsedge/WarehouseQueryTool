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
    public partial class frmFilter : Form
    {
        //private List<Label> _labels;
        private int _lastGroupPos = 50;
        private int _numFilters = 0;
        public List<FilterControlSet> filters;

        public frmFilter(List<FilterControlSet> filterControlSets, BindingList<TypedColumn> columns)
        {
            InitializeComponent();
            PopulateColumnCombo(columns);
            //_labels = new List<Label>();
            if (filterControlSets != null && filterControlSets.Count > 0)
            { ReloadFilters(filterControlSets); }
        }

        private void ReloadFilters(List<FilterControlSet> filterControlSets)
        {
            foreach (FilterControlSet fcs in filterControlSets)
            {
                _numFilters += 1;
                if (_numFilters > 1)
                { _lastGroupPos += 80; }
                GroupBox gb = new GroupBox();
                gb.Location = new Point(15, _lastGroupPos);
                gb.Size = new Size(890, 75);
                gb.Text = "Filter " + _numFilters;
                gb.Name = "grp" + _numFilters;
                TypedColumn tc = (cmbColumns.SelectedItem as TypedColumn);
                this.Controls.Add(gb);
                FilterFormConverter.ReloadFilterElements(gb, _numFilters, fcs, ttip);
                Button btn = gb.Controls["btnRem" + _numFilters] as Button;
                int curentgroup = _numFilters;
                btn.Click += (send, evt) => { this.Controls.RemoveByKey("grp" + curentgroup); _numFilters -= 1; int n = _numFilters; if (n > 1) { _lastGroupPos -= 80; } else { _lastGroupPos -= 50; } };
            }

        }

        private void btnAddFilter_Click(object sender, EventArgs e)
        {
            try
            {
                _numFilters += 1;
                if (_numFilters > 1)
                { _lastGroupPos += 80; }
                GroupBox gb = new GroupBox();
                gb.Location = new Point(15, _lastGroupPos);
                gb.Size = new Size(890, 75);
                gb.Text = "Filter " + _numFilters;
                gb.Name = "grp" + _numFilters;
                TypedColumn tc = (cmbColumns.SelectedItem as TypedColumn);
                this.Controls.Add(gb);
                FilterFormConverter.AddFilterElements(gb, _numFilters, tc, ttip);
                Button btn = gb.Controls["btnRem" + _numFilters] as Button;
                int curentgroup = _numFilters;
                btn.Click += (send, evt) => { this.Controls.RemoveByKey("grp" + curentgroup); _numFilters -= 1; int n = _numFilters; if (n > 1) { _lastGroupPos -= 80; } else { _lastGroupPos -= 50; } };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void PopulateColumnCombo(BindingList<TypedColumn> columns)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = columns;
            cmbColumns.DataSource = bs;
            cmbColumns.DisplayMember = "FieldName";
            cmbColumns.ValueMember = "DataType";

        }

       

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            //this.Dispose();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            List<GroupBox> grpboxes = new List<GroupBox>();
            foreach(GroupBox grp in this.Controls.OfType<GroupBox>())
            {
                grpboxes.Add(grp); 
            }
            filters = FilterFormConverter.BoxesToFilterControlSets(grpboxes);
            
            this.Close();

        }
    }
}
