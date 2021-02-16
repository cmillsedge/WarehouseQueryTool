namespace WarehouseQueryTool
{
    partial class frmQueryBuilder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQueryBuilder));
            this.trvPick = new System.Windows.Forms.TreeView();
            this.imgTree = new System.Windows.Forms.ImageList(this.components);
            this.trvSelect = new System.Windows.Forms.TreeView();
            this.lblPick = new System.Windows.Forms.Label();
            this.lblSelected = new System.Windows.Forms.Label();
            this.btnRunQuery = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // trvPick
            // 
            this.trvPick.AllowDrop = true;
            this.trvPick.ImageIndex = 0;
            this.trvPick.ImageList = this.imgTree;
            this.trvPick.Location = new System.Drawing.Point(12, 41);
            this.trvPick.Name = "trvPick";
            this.trvPick.SelectedImageIndex = 0;
            this.trvPick.Size = new System.Drawing.Size(490, 601);
            this.trvPick.TabIndex = 1;
            this.trvPick.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeviewItemDrag);
            // 
            // imgTree
            // 
            this.imgTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTree.ImageStream")));
            this.imgTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imgTree.Images.SetKeyName(0, "outline.png");
            this.imgTree.Images.SetKeyName(1, "parameter.png");
            this.imgTree.Images.SetKeyName(2, "process_definition.png");
            this.imgTree.Images.SetKeyName(3, "admin.png");
            this.imgTree.Images.SetKeyName(4, "list.png");
            // 
            // trvSelect
            // 
            this.trvSelect.AllowDrop = true;
            this.trvSelect.ImageIndex = 0;
            this.trvSelect.ImageList = this.imgTree;
            this.trvSelect.Location = new System.Drawing.Point(544, 41);
            this.trvSelect.Name = "trvSelect";
            this.trvSelect.SelectedImageIndex = 0;
            this.trvSelect.Size = new System.Drawing.Size(490, 601);
            this.trvSelect.TabIndex = 2;
            this.trvSelect.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeviewDragDrop);
            this.trvSelect.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeviewDragEnter);
            this.trvSelect.KeyDown += new System.Windows.Forms.KeyEventHandler(this.trvSelect_KeyDown);
            // 
            // lblPick
            // 
            this.lblPick.AutoSize = true;
            this.lblPick.Location = new System.Drawing.Point(12, 25);
            this.lblPick.Name = "lblPick";
            this.lblPick.Size = new System.Drawing.Size(147, 13);
            this.lblPick.TabIndex = 3;
            this.lblPick.Text = "Available Process Parameters";
            // 
            // lblSelected
            // 
            this.lblSelected.AutoSize = true;
            this.lblSelected.Location = new System.Drawing.Point(541, 25);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(146, 13);
            this.lblSelected.TabIndex = 4;
            this.lblSelected.Text = "Selected Process Parameters";
            // 
            // btnRunQuery
            // 
            this.btnRunQuery.Location = new System.Drawing.Point(959, 663);
            this.btnRunQuery.Name = "btnRunQuery";
            this.btnRunQuery.Size = new System.Drawing.Size(75, 23);
            this.btnRunQuery.TabIndex = 5;
            this.btnRunQuery.Text = "Run Query";
            this.btnRunQuery.UseVisualStyleBackColor = true;
            this.btnRunQuery.Click += new System.EventHandler(this.btnRunQuery_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(508, 309);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(29, 28);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.Location = new System.Drawing.Point(1040, 309);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(29, 28);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // frmQueryBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 698);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnRunQuery);
            this.Controls.Add(this.lblSelected);
            this.Controls.Add(this.lblPick);
            this.Controls.Add(this.trvSelect);
            this.Controls.Add(this.trvPick);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmQueryBuilder";
            this.Text = "Warehouse Query Builder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TreeView trvPick;
        private System.Windows.Forms.ImageList imgTree;
        private System.Windows.Forms.TreeView trvSelect;
        private System.Windows.Forms.Label lblPick;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.Button btnRunQuery;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
    }
}