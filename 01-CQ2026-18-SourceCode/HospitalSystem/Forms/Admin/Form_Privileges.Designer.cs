namespace HospitalSystem.Forms.Admin
{
    partial class Form_Privileges
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabObjectPriv;
        private System.Windows.Forms.TabPage tabColumnPriv;
        private System.Windows.Forms.TabPage tabGrantRole;

        // Tab 1 - Cấp quyền trên đối tượng
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboGrantee;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboObjectType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstObjects;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox clbPrivileges;
        private System.Windows.Forms.CheckBox chkGrantOption;
        private System.Windows.Forms.Button btnGrant;

        // Tab 2 - Phân quyền mức cột
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboGrantee2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboObjectType2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListBox lstObjects2;
        private System.Windows.Forms.GroupBox grpColumns;
        private System.Windows.Forms.Label lblColumnHint;
        private System.Windows.Forms.CheckedListBox lstColumns;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckedListBox clbPrivileges2;
        private System.Windows.Forms.CheckBox chkGrantOption2;
        private System.Windows.Forms.Button btnGrantCol;

        // Tab 3 - Cấp Role cho User
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboTargetUser;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckedListBox clbRoles;
        private System.Windows.Forms.CheckBox chkAdminOption;
        private System.Windows.Forms.Button btnGrantRole;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvCurrentRoles;

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
            tabObjectPriv = new System.Windows.Forms.TabPage();
            tabColumnPriv = new System.Windows.Forms.TabPage();
            tabGrantRole = new System.Windows.Forms.TabPage();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label1 = new System.Windows.Forms.Label();
            cboGrantee = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            cboObjectType = new System.Windows.Forms.ComboBox();
            label3 = new System.Windows.Forms.Label();
            lstObjects = new System.Windows.Forms.ListBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            clbPrivileges = new System.Windows.Forms.CheckedListBox();
            chkGrantOption = new System.Windows.Forms.CheckBox();
            btnGrant = new System.Windows.Forms.Button();
            groupBox4 = new System.Windows.Forms.GroupBox();
            label7 = new System.Windows.Forms.Label();
            cboGrantee2 = new System.Windows.Forms.ComboBox();
            label8 = new System.Windows.Forms.Label();
            cboObjectType2 = new System.Windows.Forms.ComboBox();
            label9 = new System.Windows.Forms.Label();
            lstObjects2 = new System.Windows.Forms.ListBox();
            grpColumns = new System.Windows.Forms.GroupBox();
            lblColumnHint = new System.Windows.Forms.Label();
            lstColumns = new System.Windows.Forms.CheckedListBox();
            groupBox5 = new System.Windows.Forms.GroupBox();
            clbPrivileges2 = new System.Windows.Forms.CheckedListBox();
            chkGrantOption2 = new System.Windows.Forms.CheckBox();
            btnGrantCol = new System.Windows.Forms.Button();
            groupBox3 = new System.Windows.Forms.GroupBox();
            label4 = new System.Windows.Forms.Label();
            cboTargetUser = new System.Windows.Forms.ComboBox();
            label5 = new System.Windows.Forms.Label();
            clbRoles = new System.Windows.Forms.CheckedListBox();
            chkAdminOption = new System.Windows.Forms.CheckBox();
            btnGrantRole = new System.Windows.Forms.Button();
            label6 = new System.Windows.Forms.Label();
            dgvCurrentRoles = new System.Windows.Forms.DataGridView();
            tabControl1.SuspendLayout();
            tabObjectPriv.SuspendLayout();
            tabColumnPriv.SuspendLayout();
            tabGrantRole.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            grpColumns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCurrentRoles).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabObjectPriv);
            tabControl1.Controls.Add(tabColumnPriv);
            tabControl1.Controls.Add(tabGrantRole);
            tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl1.Location = new System.Drawing.Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(800, 450);
            tabControl1.TabIndex = 0;
            // 
            // tabObjectPriv
            // 
            tabObjectPriv.Controls.Add(btnGrant);
            tabObjectPriv.Controls.Add(chkGrantOption);
            tabObjectPriv.Controls.Add(groupBox2);
            tabObjectPriv.Controls.Add(groupBox1);
            tabObjectPriv.Location = new System.Drawing.Point(4, 29);
            tabObjectPriv.Name = "tabObjectPriv";
            tabObjectPriv.Padding = new System.Windows.Forms.Padding(3);
            tabObjectPriv.Size = new System.Drawing.Size(792, 417);
            tabObjectPriv.TabIndex = 0;
            tabObjectPriv.Text = "Cấp quyền";
            tabObjectPriv.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(cboGrantee);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(cboObjectType);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(lstObjects);
            groupBox1.Location = new System.Drawing.Point(6, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(430, 370);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin cấp quyền";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(10, 25);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(130, 20);
            label1.TabIndex = 0;
            label1.Text = "Grantee (User/Role):";
            // 
            // cboGrantee
            // 
            cboGrantee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboGrantee.Location = new System.Drawing.Point(10, 45);
            cboGrantee.Name = "cboGrantee";
            cboGrantee.Size = new System.Drawing.Size(405, 28);
            cboGrantee.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(10, 82);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(105, 20);
            label2.TabIndex = 2;
            label2.Text = "Loại đối tượng:";
            // 
            // cboObjectType
            // 
            cboObjectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboObjectType.Location = new System.Drawing.Point(10, 102);
            cboObjectType.Name = "cboObjectType";
            cboObjectType.Size = new System.Drawing.Size(405, 28);
            cboObjectType.TabIndex = 3;
            cboObjectType.SelectedIndexChanged += cboObjectType_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(10, 140);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(90, 20);
            label3.TabIndex = 4;
            label3.Text = "Chọn đối tượng:";
            // 
            // lstObjects
            // 
            lstObjects.Location = new System.Drawing.Point(10, 160);
            lstObjects.Name = "lstObjects";
            lstObjects.Size = new System.Drawing.Size(405, 196);
            lstObjects.TabIndex = 5;
            lstObjects.SelectedIndexChanged += lstObjects_SelectedIndexChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(clbPrivileges);
            groupBox2.Location = new System.Drawing.Point(445, 6);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(340, 370);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Chọn quyền";
            // 
            // clbPrivileges
            // 
            clbPrivileges.CheckOnClick = true;
            clbPrivileges.Location = new System.Drawing.Point(6, 22);
            clbPrivileges.Name = "clbPrivileges";
            clbPrivileges.Size = new System.Drawing.Size(325, 338);
            clbPrivileges.TabIndex = 0;
            clbPrivileges.ItemCheck += clbPrivileges_ItemCheck;
            // 
            // chkGrantOption
            // 
            chkGrantOption.AutoSize = true;
            chkGrantOption.Location = new System.Drawing.Point(6, 385);
            chkGrantOption.Name = "chkGrantOption";
            chkGrantOption.Size = new System.Drawing.Size(490, 24);
            chkGrantOption.TabIndex = 3;
            chkGrantOption.Text = "WITH GRANT OPTION  (người nhận được phép cấp lại quyền này)";
            chkGrantOption.UseVisualStyleBackColor = true;
            // 
            // btnGrant
            // 
            btnGrant.BackColor = System.Drawing.Color.MistyRose;
            btnGrant.Location = new System.Drawing.Point(654, 382);
            btnGrant.Name = "btnGrant";
            btnGrant.Size = new System.Drawing.Size(130, 30);
            btnGrant.TabIndex = 4;
            btnGrant.Text = "GRANT";
            btnGrant.UseVisualStyleBackColor = false;
            btnGrant.Click += btnGrant_Click;
            // 
            // tabColumnPriv
            // 
            tabColumnPriv.Controls.Add(btnGrantCol);
            tabColumnPriv.Controls.Add(chkGrantOption2);
            tabColumnPriv.Controls.Add(grpColumns);
            tabColumnPriv.Controls.Add(groupBox5);
            tabColumnPriv.Controls.Add(groupBox4);
            tabColumnPriv.Location = new System.Drawing.Point(4, 29);
            tabColumnPriv.Name = "tabColumnPriv";
            tabColumnPriv.Padding = new System.Windows.Forms.Padding(3);
            tabColumnPriv.Size = new System.Drawing.Size(792, 417);
            tabColumnPriv.TabIndex = 1;
            tabColumnPriv.Text = "Phân quyền mức cột";
            tabColumnPriv.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label7);
            groupBox4.Controls.Add(cboGrantee2);
            groupBox4.Controls.Add(label8);
            groupBox4.Controls.Add(cboObjectType2);
            groupBox4.Controls.Add(label9);
            groupBox4.Controls.Add(lstObjects2);
            groupBox4.Location = new System.Drawing.Point(6, 6);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(250, 370);
            groupBox4.TabIndex = 0;
            groupBox4.TabStop = false;
            groupBox4.Text = "Thông tin";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(8, 25);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(130, 20);
            label7.TabIndex = 0;
            label7.Text = "Grantee (User/Role):";
            // 
            // cboGrantee2
            // 
            cboGrantee2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboGrantee2.Location = new System.Drawing.Point(8, 45);
            cboGrantee2.Name = "cboGrantee2";
            cboGrantee2.Size = new System.Drawing.Size(230, 28);
            cboGrantee2.TabIndex = 1;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(8, 82);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(105, 20);
            label8.TabIndex = 2;
            label8.Text = "Chọn bảng/view:";
            // 
            // cboObjectType2
            // 
            cboObjectType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboObjectType2.Location = new System.Drawing.Point(8, 102);
            cboObjectType2.Name = "cboObjectType2";
            cboObjectType2.Size = new System.Drawing.Size(230, 28);
            cboObjectType2.TabIndex = 3;
            cboObjectType2.SelectedIndexChanged += cboObjectType2_SelectedIndexChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(8, 140);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(90, 20);
            label9.TabIndex = 4;
            label9.Text = "Chọn đối tượng:";
            // 
            // lstObjects2
            // 
            lstObjects2.Location = new System.Drawing.Point(8, 160);
            lstObjects2.Name = "lstObjects2";
            lstObjects2.Size = new System.Drawing.Size(230, 196);
            lstObjects2.TabIndex = 5;
            lstObjects2.SelectedIndexChanged += lstObjects2_SelectedIndexChanged;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(clbPrivileges2);
            groupBox5.Location = new System.Drawing.Point(265, 6);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new System.Drawing.Size(160, 370);
            groupBox5.TabIndex = 1;
            groupBox5.TabStop = false;
            groupBox5.Text = "Chọn quyền";
            // 
            // clbPrivileges2
            // 
            clbPrivileges2.CheckOnClick = true;
            clbPrivileges2.Location = new System.Drawing.Point(6, 22);
            clbPrivileges2.Name = "clbPrivileges2";
            clbPrivileges2.Size = new System.Drawing.Size(145, 338);
            clbPrivileges2.TabIndex = 0;
            // 
            // grpColumns
            // 
            grpColumns.Controls.Add(lblColumnHint);
            grpColumns.Controls.Add(lstColumns);
            grpColumns.Location = new System.Drawing.Point(434, 6);
            grpColumns.Name = "grpColumns";
            grpColumns.Size = new System.Drawing.Size(350, 370);
            grpColumns.TabIndex = 2;
            grpColumns.TabStop = false;
            grpColumns.Text = "Chọn cột (SELECT / UPDATE)";
            grpColumns.Visible = true;
            // 
            // lblColumnHint
            // 
            lblColumnHint.Location = new System.Drawing.Point(6, 22);
            lblColumnHint.Name = "lblColumnHint";
            lblColumnHint.Size = new System.Drawing.Size(335, 40);
            lblColumnHint.TabIndex = 0;
            lblColumnHint.Text = "Không chọn cột = cấp toàn bảng.\r\nChọn cột = phân quyền mức cột.";
            lblColumnHint.ForeColor = System.Drawing.Color.DarkBlue;
            lblColumnHint.Visible = true;
            // 
            // lstColumns
            // 
            lstColumns.CheckOnClick = true;
            lstColumns.Location = new System.Drawing.Point(6, 68);
            lstColumns.Name = "lstColumns";
            lstColumns.Size = new System.Drawing.Size(335, 292);
            lstColumns.TabIndex = 1;
            // 
            // chkGrantOption2
            // 
            chkGrantOption2.AutoSize = true;
            chkGrantOption2.Location = new System.Drawing.Point(6, 385);
            chkGrantOption2.Name = "chkGrantOption2";
            chkGrantOption2.Size = new System.Drawing.Size(490, 24);
            chkGrantOption2.TabIndex = 3;
            chkGrantOption2.Text = "WITH GRANT OPTION  (người nhận được phép cấp lại quyền này)";
            chkGrantOption2.UseVisualStyleBackColor = true;
            // 
            // btnGrantCol
            // 
            btnGrantCol.BackColor = System.Drawing.Color.MistyRose;
            btnGrantCol.Location = new System.Drawing.Point(654, 382);
            btnGrantCol.Name = "btnGrantCol";
            btnGrantCol.Size = new System.Drawing.Size(130, 30);
            btnGrantCol.TabIndex = 4;
            btnGrantCol.Text = "GRANT";
            btnGrantCol.UseVisualStyleBackColor = false;
            btnGrantCol.Click += btnGrantCol_Click;
            // 
            // tabGrantRole
            // 
            tabGrantRole.Controls.Add(groupBox3);
            tabGrantRole.Location = new System.Drawing.Point(4, 29);
            tabGrantRole.Name = "tabGrantRole";
            tabGrantRole.Padding = new System.Windows.Forms.Padding(3);
            tabGrantRole.Size = new System.Drawing.Size(792, 417);
            tabGrantRole.TabIndex = 2;
            tabGrantRole.Text = "Cấp Role cho User";
            tabGrantRole.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(cboTargetUser);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(clbRoles);
            groupBox3.Controls.Add(chkAdminOption);
            groupBox3.Controls.Add(btnGrantRole);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(dgvCurrentRoles);
            groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox3.Location = new System.Drawing.Point(3, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(786, 411);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "Cấp Role cho User";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(10, 28);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(90, 20);
            label4.TabIndex = 0;
            label4.Text = "User nhận Role:";
            // 
            // cboTargetUser
            // 
            cboTargetUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboTargetUser.Location = new System.Drawing.Point(10, 50);
            cboTargetUser.Name = "cboTargetUser";
            cboTargetUser.Size = new System.Drawing.Size(250, 28);
            cboTargetUser.TabIndex = 1;
            cboTargetUser.SelectedIndexChanged += cboTargetUser_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(10, 88);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(65, 20);
            label5.TabIndex = 2;
            label5.Text = "Chọn Role:";
            // 
            // clbRoles
            // 
            clbRoles.CheckOnClick = true;
            clbRoles.Location = new System.Drawing.Point(10, 108);
            clbRoles.Name = "clbRoles";
            clbRoles.Size = new System.Drawing.Size(250, 230);
            clbRoles.TabIndex = 3;
            // 
            // chkAdminOption
            // 
            chkAdminOption.AutoSize = true;
            chkAdminOption.Location = new System.Drawing.Point(10, 350);
            chkAdminOption.Name = "chkAdminOption";
            chkAdminOption.Size = new System.Drawing.Size(350, 24);
            chkAdminOption.TabIndex = 4;
            chkAdminOption.Text = "WITH ADMIN OPTION  (user được phép cấp lại role này)";
            chkAdminOption.UseVisualStyleBackColor = true;
            // 
            // btnGrantRole
            // 
            btnGrantRole.BackColor = System.Drawing.Color.MistyRose;
            btnGrantRole.Location = new System.Drawing.Point(370, 346);
            btnGrantRole.Name = "btnGrantRole";
            btnGrantRole.Size = new System.Drawing.Size(130, 30);
            btnGrantRole.TabIndex = 5;
            btnGrantRole.Text = "GRANT ROLE";
            btnGrantRole.UseVisualStyleBackColor = false;
            btnGrantRole.Click += btnGrantRole_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label6.Location = new System.Drawing.Point(275, 28);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(145, 20);
            label6.TabIndex = 6;
            label6.Text = "Role hiện tại của User:";
            // 
            // dgvCurrentRoles
            // 
            dgvCurrentRoles.AllowUserToAddRows = false;
            dgvCurrentRoles.AllowUserToDeleteRows = false;
            dgvCurrentRoles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvCurrentRoles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCurrentRoles.Location = new System.Drawing.Point(275, 50);
            dgvCurrentRoles.Name = "dgvCurrentRoles";
            dgvCurrentRoles.ReadOnly = true;
            dgvCurrentRoles.RowHeadersWidth = 51;
            dgvCurrentRoles.RowTemplate.Height = 29;
            dgvCurrentRoles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvCurrentRoles.Size = new System.Drawing.Size(500, 290);
            dgvCurrentRoles.TabIndex = 7;
            dgvCurrentRoles.CellClick += dgvCurrentRoles_CellClick;
            // 
            // Form_Privileges
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(tabControl1);
            Name = "Form_Privileges";
            Text = "Cấp quyền (Grant Privileges)";
            Load += Form_Privileges_Load;
            tabControl1.ResumeLayout(false);
            tabObjectPriv.ResumeLayout(false);
            tabObjectPriv.PerformLayout();
            tabColumnPriv.ResumeLayout(false);
            tabColumnPriv.PerformLayout();
            tabGrantRole.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            grpColumns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCurrentRoles).EndInit();
            ResumeLayout(false);
        }
    }
}