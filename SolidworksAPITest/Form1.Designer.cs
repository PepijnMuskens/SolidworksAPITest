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
            btnOpenFile = new Button();
            btnUpdate = new Button();
            dataGridViewDrawingValues = new DataGridView();
            btnCompare = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDrawingValues).BeginInit();
            SuspendLayout();
            // 
            // btnOpenFile
            // 
            btnOpenFile.Location = new Point(23, 27);
            btnOpenFile.Margin = new Padding(3, 4, 3, 4);
            btnOpenFile.Name = "btnOpenFile";
            btnOpenFile.Size = new Size(183, 53);
            btnOpenFile.TabIndex = 0;
            btnOpenFile.Text = "Open TXT File";
            btnOpenFile.UseVisualStyleBackColor = true;
            btnOpenFile.Click += btnOpenFile_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(223, 27);
            btnUpdate.Margin = new Padding(3, 4, 3, 4);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(183, 53);
            btnUpdate.TabIndex = 1;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // dataGridViewDrawingValues
            // 
            dataGridViewDrawingValues.AllowUserToAddRows = false;
            dataGridViewDrawingValues.AllowUserToDeleteRows = false;
            dataGridViewDrawingValues.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewDrawingValues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewDrawingValues.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDrawingValues.Location = new Point(23, 107);
            dataGridViewDrawingValues.Margin = new Padding(3, 4, 3, 4);
            dataGridViewDrawingValues.Name = "dataGridViewDrawingValues";
            dataGridViewDrawingValues.ReadOnly = true;
            dataGridViewDrawingValues.RowHeadersWidth = 51;
            dataGridViewDrawingValues.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewDrawingValues.Size = new Size(1074, 533);
            dataGridViewDrawingValues.TabIndex = 2;
            // 
            // btnCompare
            // 
            btnCompare.Location = new Point(424, 27);
            btnCompare.Margin = new Padding(3, 4, 3, 4);
            btnCompare.Name = "btnCompare";
            btnCompare.Size = new Size(183, 53);
            btnCompare.TabIndex = 3;
            btnCompare.Text = "Compare with Excel";
            btnCompare.UseVisualStyleBackColor = true;
            btnCompare.Click += btnCompare_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1120, 680);
            Controls.Add(btnCompare);
            Controls.Add(dataGridViewDrawingValues);
            Controls.Add(btnUpdate);
            Controls.Add(btnOpenFile);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(797, 518);
            Name = "Form1";
            Text = "SolidWorks Deksloof Calculator";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewDrawingValues).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DataGridView dataGridViewDrawingValues;
        private Button btnCompare;
    }
}
