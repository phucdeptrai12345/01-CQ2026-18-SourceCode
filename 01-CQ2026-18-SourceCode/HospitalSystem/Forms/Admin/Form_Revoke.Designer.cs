namespace HospitalSystem.Forms.Admin
{
    partial class Form_Revoke
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
            label1 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            dataGridView1 = new DataGridView();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(94, 47);
            label1.Name = "label1";
            label1.Size = new Size(117, 25);
            label1.TabIndex = 0;
            label1.Text = "Tên user/role:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(236, 44);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(511, 31);
            textBox1.TabIndex = 1;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.Info;
            button1.Location = new Point(797, 44);
            button1.Name = "button1";
            button1.Size = new Size(120, 34);
            button1.TabIndex = 2;
            button1.Text = "Xem quyền";
            button1.UseVisualStyleBackColor = false;
            button1.Click += btnViewPrivileges_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(71, 114);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(880, 294);
            dataGridView1.TabIndex = 3;
            // 
            // button2
            // 
            button2.BackColor = Color.Red;
            button2.ForeColor = Color.White;
            button2.Location = new Point(364, 439);
            button2.Name = "button2";
            button2.Size = new Size(239, 42);
            button2.TabIndex = 4;
            button2.Text = "Thu hồi quyền";
            button2.UseVisualStyleBackColor = false;
            button2.Click += btnRevoke_Click;
            // 
            // Form_Revoke1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1007, 518);
            Controls.Add(button2);
            Controls.Add(dataGridView1);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Name = "Form_Revoke1";
            Text = "Thu hồi quyền";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Button button1;
        private DataGridView dataGridView1;
        private Button button2;
    }
}