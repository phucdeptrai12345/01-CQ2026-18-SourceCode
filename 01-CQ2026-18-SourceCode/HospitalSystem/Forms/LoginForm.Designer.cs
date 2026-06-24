namespace HospitalSystem.Forms;

partial class LoginForm
{
    private System.ComponentModel.IContainer components = null;

    // Controls
    private Panel pnlBackground;
    private Panel pnlCenter;
    private Label lblIcon;
    private Label lblTitle;
    private Label lblSubTitle;
    private Label lblHost;
    private TextBox txtHost;
    private Label lblPort;
    private TextBox txtPort;
    private Label lblService;
    private TextBox txtService;
    private Label lblUsername;
    private TextBox txtUsername;
    private Label lblPassword;
    private TextBox txtPassword;
    private Button btnLogin;
    private Label lblStatus;
    private Panel pnlSeparator;
    private Label lblVersion;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        // === Form chính ===
        this.Text = "Đăng nhập - Hệ thống Quản lý Y tế Bệnh viện";
        this.Size = new Size(600, 820); // Mở rộng form
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.BackColor = Color.FromArgb(18, 18, 30);
        this.Font = new Font("Segoe UI", 9f);

        // === Panel nền chính (toàn bộ form) ===
        pnlBackground = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.FromArgb(18, 18, 30),
            Padding = new Padding(40)
        };

        // === Panel trung tâm bo tròn ===
        pnlCenter = new Panel
        {
            BackColor = Color.FromArgb(30, 30, 58),
            Padding = new Padding(40),
            Width = 500, // Mở rộng panel
            Height = 690,
            Left = 40,
            Top = 35
        };

        // === Icon bệnh viện ===
        lblIcon = new Label
        {
            Text = "🏥",
            Font = new Font("Segoe UI", 40f), // To hơn
            ForeColor = Color.FromArgb(100, 181, 246),
            AutoSize = false,
            Width = 420,
            Height = 65,
            Left = 40,
            Top = 40,
            TextAlign = ContentAlignment.MiddleCenter
        };

        // === Tiêu đề chính ===
        lblTitle = new Label
        {
            Text = "QUẢN LÝ Y TẾ",
            Font = new Font("Segoe UI", 24f, FontStyle.Bold), // To hơn
            ForeColor = Color.FromArgb(100, 181, 246),
            AutoSize = false,
            Width = 420,
            Height = 50,
            Left = 40,
            Top = 110,
            TextAlign = ContentAlignment.MiddleCenter
        };

        // === Tiêu đề phụ ===
        lblSubTitle = new Label
        {
            Text = "Hệ thống quản lý dữ liệu bệnh viện",
            Font = new Font("Segoe UI", 11f),
            ForeColor = Color.FromArgb(150, 150, 180),
            AutoSize = false,
            Width = 420,
            Height = 25,
            Left = 40,
            Top = 160,
            TextAlign = ContentAlignment.MiddleCenter
        };

        // === Separator ===
        pnlSeparator = new Panel
        {
            BackColor = Color.FromArgb(60, 60, 100),
            Width = 360,
            Height = 1,
            Left = 70,
            Top = 200
        };

        // === Oracle connection fields ===
        lblHost = new Label
        {
            Text = "HOST",
            Font = new Font("Segoe UI", 9f, FontStyle.Bold),
            ForeColor = Color.FromArgb(120, 180, 250),
            AutoSize = false,
            Width = 190,
            Height = 20,
            Left = 40,
            Top = 220
        };

        txtHost = new TextBox
        {
            Text = "localhost",
            Width = 190,
            Height = 34,
            Left = 40,
            Top = 242,
            BackColor = Color.FromArgb(40, 40, 70),
            ForeColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            Font = new Font("Segoe UI", 11f)
        };

        lblPort = new Label
        {
            Text = "PORT",
            Font = new Font("Segoe UI", 9f, FontStyle.Bold),
            ForeColor = Color.FromArgb(120, 180, 250),
            AutoSize = false,
            Width = 85,
            Height = 20,
            Left = 245,
            Top = 220
        };

        txtPort = new TextBox
        {
            Text = "1521",
            Width = 85,
            Height = 34,
            Left = 245,
            Top = 242,
            BackColor = Color.FromArgb(40, 40, 70),
            ForeColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            Font = new Font("Segoe UI", 11f)
        };

        lblService = new Label
        {
            Text = "SERVICE",
            Font = new Font("Segoe UI", 9f, FontStyle.Bold),
            ForeColor = Color.FromArgb(120, 180, 250),
            AutoSize = false,
            Width = 115,
            Height = 20,
            Left = 345,
            Top = 220
        };

        txtService = new TextBox
        {
            Text = "XEPDB1",
            Width = 115,
            Height = 34,
            Left = 345,
            Top = 242,
            BackColor = Color.FromArgb(40, 40, 70),
            ForeColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            Font = new Font("Segoe UI", 11f)
        };

        // === Label Username ===
        lblUsername = new Label
        {
            Text = "TÊN ĐĂNG NHẬP",
            Font = new Font("Segoe UI", 9f, FontStyle.Bold),
            ForeColor = Color.FromArgb(120, 180, 250),
            AutoSize = false,
            Width = 420,
            Height = 20,
            Left = 40,
            Top = 305
        };

        // === TextBox Username ===
        txtUsername = new TextBox
        {
            Width = 420,
            Height = 45, // To hơn
            Left = 40,
            Top = 330,
            BackColor = Color.FromArgb(40, 40, 70),
            ForeColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            Font = new Font("Segoe UI", 13f),
            Padding = new Padding(8)
        };
        txtUsername.KeyDown += new KeyEventHandler(txtUsername_KeyDown);

        // === Label Password ===
        lblPassword = new Label
        {
            Text = "MẬT KHẨU",
            Font = new Font("Segoe UI", 9f, FontStyle.Bold),
            ForeColor = Color.FromArgb(120, 180, 250),
            AutoSize = false,
            Width = 420,
            Height = 20,
            Left = 40,
            Top = 395
        };

        // === TextBox Password ===
        txtPassword = new TextBox
        {
            Width = 420,
            Height = 45,
            Left = 40,
            Top = 420,
            BackColor = Color.FromArgb(40, 40, 70),
            ForeColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            Font = new Font("Segoe UI", 13f),
            PasswordChar = '●',
            Padding = new Padding(8)
        };
        txtPassword.KeyDown += new KeyEventHandler(txtPassword_KeyDown);

        // === Button Đăng nhập ===
        btnLogin = new Button
        {
            Text = "ĐĂNG NHẬP",
            Width = 420,
            Height = 55, // Nút to hơn
            Left = 40,
            Top = 505,
            BackColor = Color.FromArgb(21, 101, 192),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 13f, FontStyle.Bold),
            Cursor = Cursors.Hand
        };
        btnLogin.FlatAppearance.BorderSize = 0;
        btnLogin.Click += new EventHandler(btnLogin_Click);

        // === Label Status (lỗi) ===
        lblStatus = new Label
        {
            Text = "",
            Font = new Font("Segoe UI", 10f),
            ForeColor = Color.FromArgb(239, 83, 80),
            AutoSize = false,
            Width = 420,
            Height = 40,
            Left = 40,
            Top = 570,
            TextAlign = ContentAlignment.MiddleCenter
        };

        // === Label Version ===
        lblVersion = new Label
        {
            Text = "PhanHe2 v1.0 | Oracle 21c XE | .NET 8",
            Font = new Font("Segoe UI", 9f),
            ForeColor = Color.FromArgb(80, 80, 110),
            AutoSize = false,
            Width = 420,
            Height = 20,
            Left = 40,
            Top = 625,
            TextAlign = ContentAlignment.MiddleCenter
        };

        // Thêm controls vào pnlCenter
        pnlCenter.Controls.AddRange(new Control[]
        {
            lblIcon, lblTitle, lblSubTitle, pnlSeparator,
            lblHost, txtHost, lblPort, txtPort, lblService, txtService,
            lblUsername, txtUsername,
            lblPassword, txtPassword,
            btnLogin, lblStatus, lblVersion
        });

        // Thêm pnlCenter vào pnlBackground
        pnlBackground.Controls.Add(pnlCenter);

        // Thêm pnlBackground vào Form
        this.Controls.Add(pnlBackground);
    }
}
