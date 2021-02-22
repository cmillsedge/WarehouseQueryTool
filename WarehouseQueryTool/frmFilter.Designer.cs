
namespace WarehouseQueryTool
{
    partial class frmFilter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFilter));
            this.btnClose = new System.Windows.Forms.Button();
            this.lblAddFilter = new System.Windows.Forms.Label();
            this.cmbColumns = new System.Windows.Forms.ComboBox();
            this.btnAddFilter = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.ttip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(707, 608);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 29);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblAddFilter
            // 
            this.lblAddFilter.AutoSize = true;
            this.lblAddFilter.Location = new System.Drawing.Point(12, 18);
            this.lblAddFilter.Name = "lblAddFilter";
            this.lblAddFilter.Size = new System.Drawing.Size(112, 13);
            this.lblAddFilter.TabIndex = 2;
            this.lblAddFilter.Text = "Select Column to Filter";
            // 
            // cmbColumns
            // 
            this.cmbColumns.FormattingEnabled = true;
            this.cmbColumns.Location = new System.Drawing.Point(140, 12);
            this.cmbColumns.Name = "cmbColumns";
            this.cmbColumns.Size = new System.Drawing.Size(297, 21);
            this.cmbColumns.TabIndex = 3;
            // 
            // btnAddFilter
            // 
            this.btnAddFilter.Location = new System.Drawing.Point(457, 10);
            this.btnAddFilter.Name = "btnAddFilter";
            this.btnAddFilter.Size = new System.Drawing.Size(92, 23);
            this.btnAddFilter.TabIndex = 4;
            this.btnAddFilter.Text = "Add Filter";
            this.btnAddFilter.UseVisualStyleBackColor = true;
            this.btnAddFilter.Click += new System.EventHandler(this.btnAddFilter_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(803, 608);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(81, 29);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "Apply Filter";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // frmFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 649);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnAddFilter);
            this.Controls.Add(this.cmbColumns);
            this.Controls.Add(this.lblAddFilter);
            this.Controls.Add(this.btnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFilter";
            this.RightToLeftLayout = true;
            this.Text = "Result Filter Definition";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblAddFilter;
        private System.Windows.Forms.ComboBox cmbColumns;
        private System.Windows.Forms.Button btnAddFilter;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ToolTip ttip;
    }
}