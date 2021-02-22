using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace WarehouseQueryTool
{
    public static class FilterFormConverter
    {
        public static void AddFilterElements(GroupBox box, int numfilters, TypedColumn tc, ToolTip ttip)
        {

            //label for datatype
            Label lblType = new Label();
            lblType.Text = tc.DataType;
            lblType.Name = "lblType" + numfilters;
            lblType.Size = new Size(1, 1);
            lblType.Location = new Point(1, 1);
            box.Controls.Add(lblType);
            //label for column
            Label lblColumn = new Label();
            lblColumn.Text = tc.FieldName;
            lblColumn.Name = "lblCoumn" + numfilters;
            lblColumn.Size = new Size(300, 13);
            ttip.SetToolTip(lblColumn, tc.FieldName);
            lblColumn.Location = new Point(16, 42);
            box.Controls.Add(lblColumn);
            //label for operator
            Label lblOper = new Label();
            lblOper.Text = "Operator";
            lblOper.Name = "lblOper" + numfilters;
            lblOper.Size = new Size(48, 13);
            lblOper.Location = new Point(399, 21);
            box.Controls.Add(lblOper);
            //label for filter value
            Label lblValue = new Label();
            lblValue.Text = "Filter Value";
            lblValue.Name = "lblValue" + numfilters;
            lblValue.Size = new Size(59, 13);
            lblValue.Location = new Point(605, 21);
            box.Controls.Add(lblValue);
            //Combo for filter operator
            BindingList<string> operators = GetOperators(tc);
            ComboBox cmbOper = new ComboBox();
            cmbOper.DataSource = operators;
            cmbOper.Name = "cmbOper" + numfilters;
            cmbOper.Size = new Size(150, 20);
            cmbOper.Location = new Point(344, 37);
            box.Controls.Add(cmbOper);
            //textbox for filter value
            TextBox txtValue = new TextBox();
            txtValue.Text = "<Enter Filter Value>";
            txtValue.Name = "txtValue" + numfilters;
            txtValue.Size = new Size(270, 20);
            txtValue.Location = new Point(499, 37);
            box.Controls.Add(txtValue);
            //button for removal
            Button btnRem = new Button();
            btnRem.Text = "Remove Filter";
            btnRem.Name = "btnRem" + numfilters;
            btnRem.Size = new Size(100, 20);
            btnRem.Location = new Point(780, 37);
            box.Controls.Add(btnRem);
        }

        public static BindingList<string> GetOperators(TypedColumn tc)
        {
            BindingList<string> operators = new BindingList<string>();

            switch (tc.DataType)
            {
                case "System.String":
                    operators.Add("EQUALS");
                    operators.Add("LIKE");
                    operators.Add("STARTS WITH");
                    operators.Add("ENDS WITH");
                    break;
                case "System.Decimal":
                    operators.Add("EQUALS");
                    operators.Add("GREATER THAN");
                    operators.Add("LESS THAN");
                    break;
                case "System.DateTime":
                    operators.Add("ON");
                    operators.Add("BEFORE");
                    operators.Add("AFTER");
                    break;
                default:
                    operators.Add("EQUALS");
                    operators.Add("LIKE");
                    operators.Add("STARTS WITH");
                    operators.Add("ENDS WITH");
                    break;
            }
            return operators;
        }

        public static List<FilterControlSet> BoxesToFilterControlSets(List<GroupBox> groupBoxes )
        {
            List<FilterControlSet> filterControlSets = new List<FilterControlSet>();
            try
            {
                foreach (GroupBox gb in groupBoxes)
                {
                    int boxNumber = GetBoxNumber(gb);
                    TextBox filterValText = GetFilterTextBox(boxNumber, gb);
                    ComboBox filterOpCombo = GetFilterCombo( boxNumber, gb);
                    Label filterLabel = GetFilterLabel( boxNumber, gb);
                    Label typeLabel = GetDataTypeLabel(boxNumber, gb);
                    FilterControlSet fcs = new FilterControlSet();
                    fcs.FieldControl = filterValText.Name;
                    fcs.FieldName = filterLabel.Text;
                    fcs.FieldValue = filterValText.Text;
                    fcs.OperatorControl = filterOpCombo.Name;
                    fcs.OperatorValue = filterOpCombo.SelectedItem.ToString();
                    fcs.DataType = typeLabel.Text;
                    filterControlSets.Add(fcs);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return filterControlSets;
        }

        public static int GetBoxNumber(GroupBox groupBox)
        {
            int boxNumber = 0;
            try
            {
                string boxName = groupBox.Name;
                boxName = boxName.Substring(3);
                boxNumber = int.Parse(boxName, new CultureInfo("en-US"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return boxNumber;
        }

        public static ComboBox GetFilterCombo(int controlNum, GroupBox groupBox)
        {
            ComboBox cmb = null;
            try
            {
                Control[] ctls = groupBox.Controls.Find("cmbOper" + controlNum, true);
                cmb = (ComboBox)ctls[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cmb;
        }

        public static Label GetFilterLabel(int controlNum, GroupBox groupBox)
        {
            Label lbl = null;
            try
            {
                Control[] ctls = groupBox.Controls.Find("lblCoumn" + controlNum, true);
                lbl = (Label)ctls[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lbl;
        }

        public static Label GetDataTypeLabel(int controlNum, GroupBox groupBox)
        {
            Label lbl = null;
            try
            {
                Control[] ctls = groupBox.Controls.Find("lblType" + controlNum, true);
                lbl = (Label)ctls[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lbl;
        }

        public static TextBox GetFilterTextBox(int controlNum, GroupBox groupBox)
        {
            TextBox txt = null;
            try
            {
                Control[] ctls = groupBox.Controls.Find("txtValue" + controlNum, true);
                txt = (TextBox)ctls[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return txt;
        }
    }
}
