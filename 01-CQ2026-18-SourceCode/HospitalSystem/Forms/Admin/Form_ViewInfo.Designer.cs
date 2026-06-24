namespace HospitalSystem.Forms.Admin
{
    partial class Form_ViewInfo
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvUserRole;
        private System.Windows.Forms.DataGridView dgvPrivs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPrivilegeTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvUserRole = new System.Windows.Forms.DataGridView();
            dgvPrivs = new System.Windows.Forms.DataGridView();
            label1 = new System.Windows.Forms.Label();
            lblPrivilegeTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)dgvUserRole).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPrivs).BeginInit();
            SuspendLayout();
            // 
            // dgvUserRole
            // 
            dgvUserRole.AllowUserToAddRows = false;
            dgvUserRole.AllowUserToDeleteRows = false;
            dgvUserRole.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUserRole.Location = new System.Drawing.Point(12, 40);
            dgvUserRole.Name = "dgvUserRole";
            dgvUserRole.ReadOnly = true;
            dgvUserRole.RowHeadersWidth = 51;
            dgvUserRole.RowTemplate.Height = 29;
            dgvUserRole.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvUserRole.Size = new System.Drawing.Size(300, 390);
            dgvUserRole.TabIndex = 0;
            dgvUserRole.CellClick += dgvUserRole_CellClick;
            // 
            // dgvPrivs
            // 
            dgvPrivs.AllowUserToAddRows = false;
            dgvPrivs.AllowUserToDeleteRows = false;
            dgvPrivs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPrivs.Location = new System.Drawing.Point(330, 40);
            dgvPrivs.Name = "dgvPrivs";
            dgvPrivs.ReadOnly = true;
            dgvPrivs.RowHeadersWidth = 51;
            dgvPrivs.RowTemplate.Height = 29;
            dgvPrivs.Size = new System.Drawing.Size(450, 390);
            dgvPrivs.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(200, 23);
            label1.TabIndex = 2;
            label1.Text = "Danh sách User và Role";
            // 
            // lblPrivilegeTitle
            // 
            lblPrivilegeTitle.AutoSize = true;
            lblPrivilegeTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblPrivilegeTitle.Location = new System.Drawing.Point(330, 9);
            lblPrivilegeTitle.Name = "lblPrivilegeTitle";
            lblPrivilegeTitle.Size = new System.Drawing.Size(262, 23);
            lblPrivilegeTitle.TabIndex = 3;
            lblPrivilegeTitle.Text = "Quyền (Privileges) của đối tượng";
            // 
            // Form_ViewInfo
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(lblPrivilegeTitle);
            Controls.Add(label1);
            Controls.Add(dgvPrivs);
            Controls.Add(dgvUserRole);
            Name = "Form_ViewInfo";
            Text = "Xem Thông Tin User/Role và Quyền";
            Load += Form_ViewInfo_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUserRole).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPrivs).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
