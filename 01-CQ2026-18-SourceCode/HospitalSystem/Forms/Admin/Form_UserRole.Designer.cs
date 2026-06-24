namespace HospitalSystem.Forms.Admin
{
    partial class Form_UserRole
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabUsers;
        private System.Windows.Forms.TabPage tabRoles;
        private System.Windows.Forms.DataGridView dgvUserList;
        private System.Windows.Forms.DataGridView dgvRoleList;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnCreateUser;
        private System.Windows.Forms.Button btnUpdateUser;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRoleName;
        private System.Windows.Forms.Button btnCreateRole;
        private System.Windows.Forms.Button btnDeleteRole;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;

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
            tabControl1 = new System.Windows.Forms.TabControl();
            tabUsers = new System.Windows.Forms.TabPage();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label1 = new System.Windows.Forms.Label();
            txtUsername = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            txtPassword = new System.Windows.Forms.TextBox();
            btnCreateUser = new System.Windows.Forms.Button();
            btnUpdateUser = new System.Windows.Forms.Button();
            btnDeleteUser = new System.Windows.Forms.Button();
            dgvUserList = new System.Windows.Forms.DataGridView();
            tabRoles = new System.Windows.Forms.TabPage();
            groupBox2 = new System.Windows.Forms.GroupBox();
            label3 = new System.Windows.Forms.Label();
            txtRoleName = new System.Windows.Forms.TextBox();
            btnCreateRole = new System.Windows.Forms.Button();
            btnDeleteRole = new System.Windows.Forms.Button();
            dgvRoleList = new System.Windows.Forms.DataGridView();
            tabControl1.SuspendLayout();
            tabUsers.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUserList).BeginInit();
            tabRoles.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRoleList).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabUsers);
            tabControl1.Controls.Add(tabRoles);
            tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl1.Location = new System.Drawing.Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(800, 450);
            tabControl1.TabIndex = 0;
            // 
            // tabUsers
            // 
            tabUsers.Controls.Add(dgvUserList);
            tabUsers.Controls.Add(groupBox1);
            tabUsers.Location = new System.Drawing.Point(4, 29);
            tabUsers.Name = "tabUsers";
            tabUsers.Padding = new System.Windows.Forms.Padding(3);
            tabUsers.Size = new System.Drawing.Size(792, 417);
            tabUsers.TabIndex = 0;
            tabUsers.Text = "Quản lý User";
            tabUsers.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtUsername);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtPassword);
            groupBox1.Controls.Add(btnCreateUser);
            groupBox1.Controls.Add(btnUpdateUser);
            groupBox1.Controls.Add(btnDeleteUser);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            groupBox1.Location = new System.Drawing.Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(786, 120);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin User";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(20, 30);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(78, 20);
            label1.TabIndex = 0;
            label1.Text = "Username:";
            // 
            // txtUsername
            // 
            txtUsername.Location = new System.Drawing.Point(110, 27);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new System.Drawing.Size(200, 27);
            txtUsername.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(20, 70);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(73, 20);
            label2.TabIndex = 2;
            label2.Text = "Password:";
            // 
            // txtPassword
            // 
            txtPassword.Location = new System.Drawing.Point(110, 67);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new System.Drawing.Size(200, 27);
            txtPassword.TabIndex = 3;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnCreateUser
            // 
            btnCreateUser.Location = new System.Drawing.Point(330, 25);
            btnCreateUser.Name = "btnCreateUser";
            btnCreateUser.Size = new System.Drawing.Size(120, 30);
            btnCreateUser.TabIndex = 4;
            btnCreateUser.Text = "Tạo User";
            btnCreateUser.UseVisualStyleBackColor = true;
            btnCreateUser.Click += btnCreateUser_Click;
            // 
            // btnUpdateUser
            // 
            btnUpdateUser.Location = new System.Drawing.Point(330, 65);
            btnUpdateUser.Name = "btnUpdateUser";
            btnUpdateUser.Size = new System.Drawing.Size(120, 30);
            btnUpdateUser.TabIndex = 5;
            btnUpdateUser.Text = "Sửa Mật khẩu";
            btnUpdateUser.UseVisualStyleBackColor = true;
            btnUpdateUser.Click += btnUpdateUser_Click;
            // 
            // btnDeleteUser
            // 
            btnDeleteUser.BackColor = System.Drawing.Color.MistyRose;
            btnDeleteUser.Location = new System.Drawing.Point(460, 25);
            btnDeleteUser.Name = "btnDeleteUser";
            btnDeleteUser.Size = new System.Drawing.Size(120, 70);
            btnDeleteUser.TabIndex = 6;
            btnDeleteUser.Text = "Xóa User";
            btnDeleteUser.UseVisualStyleBackColor = false;
            btnDeleteUser.Click += btnDeleteUser_Click;
            // 
            // dgvUserList
            // 
            dgvUserList.AllowUserToAddRows = false;
            dgvUserList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvUserList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUserList.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvUserList.Location = new System.Drawing.Point(3, 123);
            dgvUserList.Name = "dgvUserList";
            dgvUserList.ReadOnly = true;
            dgvUserList.RowHeadersWidth = 51;
            dgvUserList.RowTemplate.Height = 29;
            dgvUserList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvUserList.Size = new System.Drawing.Size(786, 291);
            dgvUserList.TabIndex = 7;
            dgvUserList.CellClick += dgvUserList_CellClick;
            // 
            // tabRoles
            // 
            tabRoles.Controls.Add(dgvRoleList);
            tabRoles.Controls.Add(groupBox2);
            tabRoles.Location = new System.Drawing.Point(4, 29);
            tabRoles.Name = "tabRoles";
            tabRoles.Padding = new System.Windows.Forms.Padding(3);
            tabRoles.Size = new System.Drawing.Size(792, 417);
            tabRoles.TabIndex = 1;
            tabRoles.Text = "Quản lý Role";
            tabRoles.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(txtRoleName);
            groupBox2.Controls.Add(btnCreateRole);
            groupBox2.Controls.Add(btnDeleteRole);
            groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            groupBox2.Location = new System.Drawing.Point(3, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(786, 80);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Thông tin Role";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(20, 35);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(84, 20);
            label3.TabIndex = 0;
            label3.Text = "Role Name:";
            // 
            // txtRoleName
            // 
            txtRoleName.Location = new System.Drawing.Point(110, 32);
            txtRoleName.Name = "txtRoleName";
            txtRoleName.Size = new System.Drawing.Size(200, 27);
            txtRoleName.TabIndex = 1;
            // 
            // btnCreateRole
            // 
            btnCreateRole.Location = new System.Drawing.Point(330, 30);
            btnCreateRole.Name = "btnCreateRole";
            btnCreateRole.Size = new System.Drawing.Size(120, 30);
            btnCreateRole.TabIndex = 2;
            btnCreateRole.Text = "Tạo Role";
            btnCreateRole.UseVisualStyleBackColor = true;
            btnCreateRole.Click += btnCreateRole_Click;
            // 
            // btnDeleteRole
            // 
            btnDeleteRole.BackColor = System.Drawing.Color.MistyRose;
            btnDeleteRole.Location = new System.Drawing.Point(460, 30);
            btnDeleteRole.Name = "btnDeleteRole";
            btnDeleteRole.Size = new System.Drawing.Size(120, 30);
            btnDeleteRole.TabIndex = 3;
            btnDeleteRole.Text = "Xóa Role";
            btnDeleteRole.UseVisualStyleBackColor = false;
            btnDeleteRole.Click += btnDeleteRole_Click;
            // 
            // dgvRoleList
            // 
            dgvRoleList.AllowUserToAddRows = false;
            dgvRoleList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvRoleList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRoleList.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvRoleList.Location = new System.Drawing.Point(3, 83);
            dgvRoleList.Name = "dgvRoleList";
            dgvRoleList.ReadOnly = true;
            dgvRoleList.RowHeadersWidth = 51;
            dgvRoleList.RowTemplate.Height = 29;
            dgvRoleList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvRoleList.Size = new System.Drawing.Size(786, 331);
            dgvRoleList.TabIndex = 5;
            dgvRoleList.CellClick += dgvRoleList_CellClick;
            // 
            // Form_UserRole
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(tabControl1);
            Name = "Form_UserRole";
            Text = "Quản lý User và Role";
            Load += Form_UserRole_Load;
            tabControl1.ResumeLayout(false);
            tabUsers.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUserList).EndInit();
            tabRoles.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRoleList).EndInit();
            ResumeLayout(false);

        }
    }
}
