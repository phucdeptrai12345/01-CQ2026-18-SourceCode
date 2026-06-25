namespace HospitalSystem.Forms;

partial class AdminForm
{
    private System.ComponentModel.IContainer components = null;
    private Panel menuPanel, mainPanel;
    private Label lblTitle;
    private Button btnUserManage, btnViewInfo, btnPrivileges, btnRevoke, btnAuditLog, btnLogout;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        menuPanel = new Panel();
        mainPanel = new Panel();
        lblTitle = new Label();
        btnUserManage = new Button();
        btnViewInfo = new Button();
        btnPrivileges = new Button();
        btnRevoke = new Button();
        btnAuditLog = new Button();
        btnLogout = new Button();
        menuPanel.SuspendLayout();
        SuspendLayout();

        menuPanel.BackColor = Color.FromArgb(30, 30, 58);
        menuPanel.Dock = DockStyle.Left;
        menuPanel.Width = 220;
        menuPanel.Controls.AddRange(new Control[] { btnLogout, btnAuditLog, btnRevoke, btnPrivileges, btnViewInfo, btnUserManage, lblTitle });

        lblTitle.Text = "QUẢN TRỊ\nORACLE DB";
        lblTitle.Font = new Font("Segoe UI", 13f, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(100, 181, 246);
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        lblTitle.Dock = DockStyle.Top;
        lblTitle.Height = 80;

        foreach (var btn in new[] { btnUserManage, btnViewInfo, btnPrivileges, btnRevoke, btnAuditLog })
        {
            btn.Dock = DockStyle.Top;
            btn.Height = 55;
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = Color.FromArgb(40, 40, 70);
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10f);
            btn.FlatAppearance.BorderSize = 0;
        }

        btnUserManage.Text = "  Quản lý User / Role";
        btnUserManage.Click += btnUserManage_Click;
        btnViewInfo.Text = "  Xem thông tin quyền";
        btnViewInfo.Click += btnViewInfo_Click;
        btnPrivileges.Text = "  Cấp quyền";
        btnPrivileges.Click += btnPrivileges_Click;
        btnRevoke.Text = "  Thu hồi quyền";
        btnRevoke.Click += btnRevoke_Click;

        btnAuditLog.Text = "  📋 Nhật ký & Giám sát";
        btnAuditLog.BackColor = Color.FromArgb(21, 101, 60);
        btnAuditLog.Click += btnAuditLog_Click;

        btnLogout.Text = "Đăng xuất";
        btnLogout.Dock = DockStyle.Bottom;
        btnLogout.Height = 50;
        btnLogout.FlatStyle = FlatStyle.Flat;
        btnLogout.BackColor = Color.FromArgb(183, 28, 28);
        btnLogout.ForeColor = Color.White;
        btnLogout.Font = new Font("Segoe UI", 10f);
        btnLogout.FlatAppearance.BorderSize = 0;
        btnLogout.Click += btnLogout_Click;

        mainPanel.Dock = DockStyle.Fill;
        mainPanel.BackColor = Color.FromArgb(245, 245, 250);

        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1280, 720);
        Controls.Add(mainPanel);
        Controls.Add(menuPanel);
        Text = "Phân hệ 1 - Quản trị CSDL Oracle (DBA)";
        StartPosition = FormStartPosition.CenterScreen;

        menuPanel.ResumeLayout(false);
        ResumeLayout(false);
    }
}
