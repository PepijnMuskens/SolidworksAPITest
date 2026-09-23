namespace SolidworksAPITest
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.dataGridViewDrawingValues = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDrawingValues)).BeginInit();
            this.SuspendLayout();

            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(20, 20);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(160, 40);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "Open TXT File";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);

            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(195, 20);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(160, 40);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            // 
            // dataGridViewDrawingValues
            // 
            this.dataGridViewDrawingValues.AllowUserToAddRows = false;
            this.dataGridViewDrawingValues.AllowUserToDeleteRows = false;
            this.dataGridViewDrawingValues.Anchor =
                ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom) |
                System.Windows.Forms.AnchorStyles.Left) |
                System.Windows.Forms.AnchorStyles.Right)));

            this.dataGridViewDrawingValues.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;

            this.dataGridViewDrawingValues.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            this.dataGridViewDrawingValues.Location =
                new System.Drawing.Point(20, 80);

            this.dataGridViewDrawingValues.Name =
                "dataGridViewDrawingValues";

            this.dataGridViewDrawingValues.ReadOnly = true;

            this.dataGridViewDrawingValues.RowHeadersWidth = 51;

            this.dataGridViewDrawingValues.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dataGridViewDrawingValues.Size =
                new System.Drawing.Size(940, 400);

            this.dataGridViewDrawingValues.TabIndex = 2;

            // 
            // Form1
            // 
            this.AutoScaleDimensions =
                new System.Drawing.SizeF(7F, 15F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize =
                new System.Drawing.Size(980, 510);

            this.Controls.Add(this.dataGridViewDrawingValues);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnOpenFile);

            this.MinimumSize =
                new System.Drawing.Size(700, 400);

            this.Name = "Form1";
            this.Text = "SolidWorks Deksloof Calculator";

            this.Load +=
                new System.EventHandler(this.Form1_Load);

            ((System.ComponentModel.ISupportInitialize)
                (this.dataGridViewDrawingValues)).EndInit();

            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DataGridView dataGridViewDrawingValues;
    }
}
