using HospitalSystem.BLL;
using HospitalSystem.DAL;

namespace HospitalSystem.Forms;

public partial class LoginForm : Form
{
    private readonly Color _btnNormal = Color.FromArgb(21, 101, 192);
    private readonly Color _btnHover  = Color.FromArgb(25, 118, 210);

    public LoginForm()
    {
        InitializeComponent();
        btnLogin.MouseEnter += (s, e) => { btnLogin.BackColor = _btnHover; btnLogin.Cursor = Cursors.Hand; };
        btnLogin.MouseLeave += (s, e) => { btnLogin.BackColor = _btnNormal; };
    }

    private async void btnLogin_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        btnLogin.Enabled = false;
        btnLogin.Text = "Đang kết nối...";
        try
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text;
            string host = txtHost.Text.Trim();
            string service = txtService.Text.Trim();
            if (string.IsNullOrEmpty(user)) { ShowError("Vui lòng nhập tên đăng nhập."); return; }
            if (string.IsNullOrEmpty(pass)) { ShowError("Vui lòng nhập mật khẩu."); return; }
            if (string.IsNullOrEmpty(host)) { ShowError("Vui lòng nhập host Oracle."); return; }
            if (string.IsNullOrEmpty(service)) { ShowError("Vui lòng nhập service Oracle."); return; }
            if (!int.TryParse(txtPort.Text.Trim(), out int port) || port <= 0)
            {
                ShowError("Port Oracle không hợp lệ.");
                return;
            }

            bool ok = await Task.Run(() => OracleHelper.Connect(user, pass, host, port, service));
            if (!ok) { ShowError("Đăng nhập thất bại. Kiểm tra thông tin kết nối."); return; }

            UserSession? session = await Task.Run(() => AuthService.GetCurrentUser());
            if (session == null) { OracleHelper.Disconnect(); ShowError("Tài khoản không đăng ký trong hệ thống."); return; }

            this.Hide();
            Form dashboard = session.Role == UserRole.DBA
                ? new AdminForm()               // Phân hệ 1
                : new MainForm(session);         // Phân hệ 2
            dashboard.FormClosed += (s, e) => this.Close();
            dashboard.Show();
        }
        catch (Exception ex) { ShowError("Lỗi: " + ex.Message); }
        finally { btnLogin.Enabled = true; btnLogin.Text = "ĐĂNG NHẬP"; }
    }

    private void ShowError(string msg) { lblStatus.Text = msg; lblStatus.ForeColor = Color.FromArgb(239, 83, 80); }
    private void txtPassword_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) btnLogin_Click(sender, e); }
    private void txtUsername_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) txtPassword.Focus(); }
}
