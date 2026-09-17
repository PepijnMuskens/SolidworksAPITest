namespace SolidworksAPITest
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            button2 = new Button();
            textBoxCreateTussenplaatBovenA = new TextBox();
            textBoxCreateTussenplaatBovenB = new TextBox();
            textBoxCreateTussenplaatBovenC = new TextBox();
            textBoxCreateTussenplaatBovenD = new TextBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(188, 215);
            button1.Name = "button1";
            button1.Size = new Size(119, 92);
            button1.TabIndex = 0;
            button1.Text = "Update";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(57, 215);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 25);
            textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(57, 248);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 25);
            textBox2.TabIndex = 2;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(57, 281);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 25);
            textBox3.TabIndex = 3;
            // 
            // button2
            // 
            button2.Location = new Point(634, 65);
            button2.Name = "button2";
            button2.Size = new Size(119, 118);
            button2.TabIndex = 4;
            button2.Text = "Create Tussenplaat Boven";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBoxCreateTussenplaatBovenA
            // 
            textBoxCreateTussenplaatBovenA.Location = new Point(495, 65);
            textBoxCreateTussenplaatBovenA.Name = "textBoxCreateTussenplaatBovenA";
            textBoxCreateTussenplaatBovenA.Size = new Size(100, 25);
            textBoxCreateTussenplaatBovenA.TabIndex = 5;
            textBoxCreateTussenplaatBovenA.TextChanged += textBoxCreateTussenplaatBovenA_TextChanged;
            // 
            // textBoxCreateTussenplaatBovenB
            // 
            textBoxCreateTussenplaatBovenB.Location = new Point(495, 96);
            textBoxCreateTussenplaatBovenB.Name = "textBoxCreateTussenplaatBovenB";
            textBoxCreateTussenplaatBovenB.Size = new Size(100, 25);
            textBoxCreateTussenplaatBovenB.TabIndex = 6;
            // 
            // textBoxCreateTussenplaatBovenC
            // 
            textBoxCreateTussenplaatBovenC.Location = new Point(495, 127);
            textBoxCreateTussenplaatBovenC.Name = "textBoxCreateTussenplaatBovenC";
            textBoxCreateTussenplaatBovenC.Size = new Size(100, 25);
            textBoxCreateTussenplaatBovenC.TabIndex = 7;
            // 
            // textBoxCreateTussenplaatBovenD
            // 
            textBoxCreateTussenplaatBovenD.Location = new Point(495, 158);
            textBoxCreateTussenplaatBovenD.Name = "textBoxCreateTussenplaatBovenD";
            textBoxCreateTussenplaatBovenD.Size = new Size(100, 25);
            textBoxCreateTussenplaatBovenD.TabIndex = 8;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 510);
            Controls.Add(textBoxCreateTussenplaatBovenD);
            Controls.Add(textBoxCreateTussenplaatBovenC);
            Controls.Add(textBoxCreateTussenplaatBovenB);
            Controls.Add(textBoxCreateTussenplaatBovenA);
            Controls.Add(button2);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button button2;
        private TextBox textBoxCreateTussenplaatBovenA;
        private TextBox textBoxCreateTussenplaatBovenB;
        private TextBox textBoxCreateTussenplaatBovenC;
        private TextBox textBoxCreateTussenplaatBovenD;
    }
}
