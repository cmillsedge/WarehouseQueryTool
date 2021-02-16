namespace WarehouseQueryTool
{
    partial class frmLogOn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogOn));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnLogOn = new System.Windows.Forms.Button();
            this.grpWarehouse = new System.Windows.Forms.GroupBox();
            this.lblWHSid = new System.Windows.Forms.Label();
            this.lblWHConnString = new System.Windows.Forms.Label();
            this.lblWHPort = new System.Windows.Forms.Label();
            this.lblWHSchema = new System.Windows.Forms.Label();
            this.lblWHHost = new System.Windows.Forms.Label();
            this.lblWHPassword = new System.Windows.Forms.Label();
            this.txtWHSid = new System.Windows.Forms.TextBox();
            this.txtWHSchema = new System.Windows.Forms.TextBox();
            this.txtWHPort = new System.Windows.Forms.TextBox();
            this.txtWHPassword = new System.Windows.Forms.TextBox();
            this.txtWHHost = new System.Windows.Forms.TextBox();
            this.grpWarehouse.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(12, 189);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnLogOn
            // 
            this.btnLogOn.Location = new System.Drawing.Point(429, 189);
            this.btnLogOn.Name = "btnLogOn";
            this.btnLogOn.Size = new System.Drawing.Size(75, 23);
            this.btnLogOn.TabIndex = 6;
            this.btnLogOn.Text = "Connect";
            this.btnLogOn.UseVisualStyleBackColor = true;
            this.btnLogOn.Click += new System.EventHandler(this.btnLogOn_Click);
            // 
            // grpWarehouse
            // 
            this.grpWarehouse.Controls.Add(this.lblWHSid);
            this.grpWarehouse.Controls.Add(this.lblWHConnString);
            this.grpWarehouse.Controls.Add(this.lblWHPort);
            this.grpWarehouse.Controls.Add(this.lblWHSchema);
            this.grpWarehouse.Controls.Add(this.lblWHHost);
            this.grpWarehouse.Controls.Add(this.lblWHPassword);
            this.grpWarehouse.Controls.Add(this.txtWHSid);
            this.grpWarehouse.Controls.Add(this.txtWHSchema);
            this.grpWarehouse.Controls.Add(this.txtWHPort);
            this.grpWarehouse.Controls.Add(this.txtWHPassword);
            this.grpWarehouse.Controls.Add(this.txtWHHost);
            this.grpWarehouse.Location = new System.Drawing.Point(12, 21);
            this.grpWarehouse.Name = "grpWarehouse";
            this.grpWarehouse.Size = new System.Drawing.Size(492, 129);
            this.grpWarehouse.TabIndex = 5;
            this.grpWarehouse.TabStop = false;
            this.grpWarehouse.Text = "Warehouse Database";
            // 
            // lblWHSid
            // 
            this.lblWHSid.AutoSize = true;
            this.lblWHSid.Location = new System.Drawing.Point(403, 24);
            this.lblWHSid.Name = "lblWHSid";
            this.lblWHSid.Size = new System.Drawing.Size(25, 13);
            this.lblWHSid.TabIndex = 21;
            this.lblWHSid.Text = "SID";
            // 
            // lblWHConnString
            // 
            this.lblWHConnString.AutoSize = true;
            this.lblWHConnString.Location = new System.Drawing.Point(19, 43);
            this.lblWHConnString.Name = "lblWHConnString";
            this.lblWHConnString.Size = new System.Drawing.Size(91, 13);
            this.lblWHConnString.TabIndex = 11;
            this.lblWHConnString.Text = "Connection String";
            // 
            // lblWHPort
            // 
            this.lblWHPort.AutoSize = true;
            this.lblWHPort.Location = new System.Drawing.Point(322, 24);
            this.lblWHPort.Name = "lblWHPort";
            this.lblWHPort.Size = new System.Drawing.Size(26, 13);
            this.lblWHPort.TabIndex = 20;
            this.lblWHPort.Text = "Port";
            // 
            // lblWHSchema
            // 
            this.lblWHSchema.AutoSize = true;
            this.lblWHSchema.Location = new System.Drawing.Point(19, 72);
            this.lblWHSchema.Name = "lblWHSchema";
            this.lblWHSchema.Size = new System.Drawing.Size(80, 13);
            this.lblWHSchema.TabIndex = 12;
            this.lblWHSchema.Text = "Schema Owner";
            // 
            // lblWHHost
            // 
            this.lblWHHost.AutoSize = true;
            this.lblWHHost.Location = new System.Drawing.Point(229, 24);
            this.lblWHHost.Name = "lblWHHost";
            this.lblWHHost.Size = new System.Drawing.Size(29, 13);
            this.lblWHHost.TabIndex = 19;
            this.lblWHHost.Text = "Host";
            // 
            // lblWHPassword
            // 
            this.lblWHPassword.AutoSize = true;
            this.lblWHPassword.Location = new System.Drawing.Point(19, 102);
            this.lblWHPassword.Name = "lblWHPassword";
            this.lblWHPassword.Size = new System.Drawing.Size(95, 13);
            this.lblWHPassword.TabIndex = 13;
            this.lblWHPassword.Text = "Schema Password";
            // 
            // txtWHSid
            // 
            this.txtWHSid.Location = new System.Drawing.Point(364, 40);
            this.txtWHSid.Name = "txtWHSid";
            this.txtWHSid.Size = new System.Drawing.Size(107, 20);
            this.txtWHSid.TabIndex = 18;
            this.txtWHSid.Text = "ORCLCDB";
            // 
            // txtWHSchema
            // 
            this.txtWHSchema.Location = new System.Drawing.Point(172, 69);
            this.txtWHSchema.Name = "txtWHSchema";
            this.txtWHSchema.Size = new System.Drawing.Size(299, 20);
            this.txtWHSchema.TabIndex = 14;
            this.txtWHSchema.Text = "C##MART";
            // 
            // txtWHPort
            // 
            this.txtWHPort.Location = new System.Drawing.Point(317, 40);
            this.txtWHPort.Name = "txtWHPort";
            this.txtWHPort.Size = new System.Drawing.Size(41, 20);
            this.txtWHPort.TabIndex = 17;
            this.txtWHPort.Text = "1521";
            // 
            // txtWHPassword
            // 
            this.txtWHPassword.Location = new System.Drawing.Point(172, 99);
            this.txtWHPassword.Name = "txtWHPassword";
            this.txtWHPassword.PasswordChar = '*';
            this.txtWHPassword.Size = new System.Drawing.Size(299, 20);
            this.txtWHPassword.TabIndex = 15;
            this.txtWHPassword.Text = "C##MART";
            // 
            // txtWHHost
            // 
            this.txtWHHost.Location = new System.Drawing.Point(172, 40);
            this.txtWHHost.Name = "txtWHHost";
            this.txtWHHost.Size = new System.Drawing.Size(139, 20);
            this.txtWHHost.TabIndex = 16;
            this.txtWHHost.Text = "192.168.1.67";
            // 
            // frmLogOn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 241);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnLogOn);
            this.Controls.Add(this.grpWarehouse);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLogOn";
            this.Text = "Warehouse Query Log On";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogOn_FormClosing);
            this.Load += new System.EventHandler(this.LogOn_Load);
            this.grpWarehouse.ResumeLayout(false);
            this.grpWarehouse.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnLogOn;
        private System.Windows.Forms.GroupBox grpWarehouse;
        private System.Windows.Forms.Label lblWHSid;
        private System.Windows.Forms.Label lblWHConnString;
        private System.Windows.Forms.Label lblWHPort;
        private System.Windows.Forms.Label lblWHSchema;
        private System.Windows.Forms.Label lblWHHost;
        private System.Windows.Forms.Label lblWHPassword;
        private System.Windows.Forms.TextBox txtWHSid;
        private System.Windows.Forms.TextBox txtWHSchema;
        private System.Windows.Forms.TextBox txtWHPort;
        private System.Windows.Forms.TextBox txtWHPassword;
        private System.Windows.Forms.TextBox txtWHHost;
    }
}

