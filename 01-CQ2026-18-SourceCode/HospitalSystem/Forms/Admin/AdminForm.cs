using HospitalSystem.Forms.Admin;

namespace HospitalSystem.Forms;

public partial class AdminForm : Form
{
    public AdminForm() { InitializeComponent(); }

    private void btnAuditLog_Click(object sender, EventArgs e) => LoadPanel(new Form_AuditLog());
    private void btnUserManage_Click(object sender, EventArgs e) => LoadPanel(new Form_UserRole());
    private void btnViewInfo_Click(object sender, EventArgs e) => LoadPanel(new Form_ViewInfo());
    private void btnPrivileges_Click(object sender, EventArgs e) => LoadPanel(new Form_Privileges());
    private void btnRevoke_Click(object sender, EventArgs e) => LoadPanel(new Form_Revoke());

    private void LoadPanel(Form frm)
    {
        mainPanel.Controls.Clear();
        frm.TopLevel = false;
        frm.FormBorderStyle = FormBorderStyle.None;
        frm.Dock = DockStyle.Fill;
        mainPanel.Controls.Add(frm);
        frm.Show();
    }

    private void btnLogout_Click(object sender, EventArgs e)
    {
        HospitalSystem.BLL.AuthService.Logout();
        this.Hide();
        var login = new LoginForm();
        login.FormClosed += (s, e2) => this.Close();
        login.Show();
    }
}
