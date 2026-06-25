namespace HospitalSystem.Forms.Patient;

partial class PatientDashboard
{
    private System.ComponentModel.IContainer components = null;

    private TabControl tabControl;
    private TabPage tabInfo;
    private TabPage tabHsba;
    private TabPage tabPrescriptions;

    private Label lblTitle;
    private Label lblMaBN, lblMaBNValue;
    private Label lblTenBN, lblTenBNValue;
    private Label lblNgaySinh, lblNgaySinhValue;
    private Label lblCCCD, lblCCCDValue;
    private Label lblPhai, lblPhaiValue;
    private Label lblSoNha;
    private TextBox txtSoNha;
    private Label lblTenDuong;
    private TextBox txtTenDuong;
    private Label lblQuanHuyen;
    private TextBox txtQuanHuyen;
    private Label lblTinhTP;
    private TextBox txtTinhTP;
    private Label lblTienSuBenh;
    private TextBox txtTienSuBenh;
    private Label lblTienSuBenhGD;
    private TextBox txtTienSuBenhGD;
    private Label lblDiUngThuoc;
    private TextBox txtDiUngThuoc;
    private Button btnSave;
    private Panel pnlReadOnly;
    private Panel pnlEditable;
    private Label lblSectionInfo;
    private Label lblSectionEdit;

    private DataGridView dgvHsba;
    private DataGridView dgvPrescriptions;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        // Create all controls
        tabControl        = new TabControl();
        tabInfo           = new TabPage();
        tabHsba           = new TabPage();
        tabPrescriptions  = new TabPage();

        pnlReadOnly       = new Panel();
        pnlEditable       = new Panel();

        lblTitle          = new Label();
        lblSectionInfo    = new Label();
        lblSectionEdit    = new Label();

        lblMaBN           = CreateLabel("Mã BN:",        15, 42);
        lblMaBNValue      = CreateValueLabel("",         130, 42);
        lblTenBN          = CreateLabel("Họ tên:",       15, 77);
        lblTenBNValue     = CreateValueLabel("",         130, 77);
        lblNgaySinh       = CreateLabel("Ngày sinh:",    15, 112);
        lblNgaySinhValue  = CreateValueLabel("",         130, 112);
        lblCCCD           = CreateLabel("CCCD:",         15, 147);
        lblCCCDValue      = CreateValueLabel("",         130, 147);
        lblPhai           = CreateLabel("Phái:",         15, 182);
        lblPhaiValue      = CreateValueLabel("",         130, 182);

        lblSoNha          = CreateFieldLabel("Số nhà:",           15, 48);
        txtSoNha          = CreateEditField(140, 45);
        lblTenDuong       = CreateFieldLabel("Tên đường:",        15, 86);
        txtTenDuong       = CreateEditField(140, 83);
        lblQuanHuyen      = CreateFieldLabel("Quận/Huyện:",       15, 124);
        txtQuanHuyen      = CreateEditField(140, 121);
        lblTinhTP         = CreateFieldLabel("Tỉnh/TP:",          15, 162);
        txtTinhTP         = CreateEditField(140, 159);
        lblTienSuBenh     = CreateFieldLabel("Tiền sử bệnh:",     15, 206);
        txtTienSuBenh     = CreateMultilineField(140, 203, 450);
        lblTienSuBenhGD   = CreateFieldLabel("Tiền sử BN GĐ:",   15, 284);
        txtTienSuBenhGD   = CreateMultilineField(140, 281, 450);
        lblDiUngThuoc     = CreateFieldLabel("Dị ứng thuốc:",     15, 362);
        txtDiUngThuoc     = CreateMultilineField(140, 359, 450);

        btnSave           = new Button();

        dgvHsba           = CreateDGV();
        dgvPrescriptions  = CreateDGV();

        tabControl.SuspendLayout();
        tabInfo.SuspendLayout();
        pnlReadOnly.SuspendLayout();
        pnlEditable.SuspendLayout();
        SuspendLayout();

        // tabControl
        tabControl.Controls.Add(tabInfo);
        tabControl.Controls.Add(tabHsba);
        tabControl.Controls.Add(tabPrescriptions);
        tabControl.Dock = DockStyle.Fill;
        tabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
        tabControl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        tabControl.Padding = new Point(12, 8);
        tabControl.SelectedIndex = 0;
        tabControl.TabIndex = 0;
        tabControl.DrawItem += TabControl_DrawItem;
        tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;

        // tabInfo
        tabInfo.BackColor = Color.FromArgb(15, 15, 26);
        tabInfo.Controls.Add(pnlEditable);
        tabInfo.Controls.Add(pnlReadOnly);
        tabInfo.Controls.Add(lblTitle);
        tabInfo.Name = "tabInfo";
        tabInfo.TabIndex = 0;
        tabInfo.Text = "  \U0001f9d1  Thông tin cá nhân  ";

        // lblTitle
        lblTitle.Text = "THÔNG TIN BỆNH NHÂN";
        lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(100, 180, 255);
        lblTitle.Dock = DockStyle.Top;
        lblTitle.Height = 50;
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        lblTitle.Name = "lblTitle";
        lblTitle.TabIndex = 0;

        // pnlReadOnly
        pnlReadOnly.BackColor = Color.FromArgb(20, 20, 40);
        pnlReadOnly.Dock = DockStyle.Top;
        pnlReadOnly.Height = 220;
        pnlReadOnly.Name = "pnlReadOnly";
        pnlReadOnly.Padding = new Padding(10);
        pnlReadOnly.TabIndex = 1;
        pnlReadOnly.Controls.Add(lblSectionInfo);
        pnlReadOnly.Controls.Add(lblMaBN);    pnlReadOnly.Controls.Add(lblMaBNValue);
        pnlReadOnly.Controls.Add(lblTenBN);   pnlReadOnly.Controls.Add(lblTenBNValue);
        pnlReadOnly.Controls.Add(lblNgaySinh); pnlReadOnly.Controls.Add(lblNgaySinhValue);
        pnlReadOnly.Controls.Add(lblCCCD);    pnlReadOnly.Controls.Add(lblCCCDValue);
        pnlReadOnly.Controls.Add(lblPhai);    pnlReadOnly.Controls.Add(lblPhaiValue);

        // lblSectionInfo
        lblSectionInfo.Text = "Thông tin định danh (chỉ đọc)";
        lblSectionInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblSectionInfo.ForeColor = Color.FromArgb(100, 180, 255);
        lblSectionInfo.Location = new Point(15, 8);
        lblSectionInfo.AutoSize = true;
        lblSectionInfo.Name = "lblSectionInfo";
        lblSectionInfo.TabIndex = 0;

        // pnlEditable
        pnlEditable.AutoScroll = true;
        pnlEditable.AutoScrollMinSize = new Size(0, 500);
        pnlEditable.BackColor = Color.FromArgb(15, 15, 26);
        pnlEditable.Dock = DockStyle.Fill;
        pnlEditable.Name = "pnlEditable";
        pnlEditable.Padding = new Padding(10);
        pnlEditable.TabIndex = 2;
        pnlEditable.Controls.Add(lblSectionEdit);
        pnlEditable.Controls.Add(lblSoNha);      pnlEditable.Controls.Add(txtSoNha);
        pnlEditable.Controls.Add(lblTenDuong);   pnlEditable.Controls.Add(txtTenDuong);
        pnlEditable.Controls.Add(lblQuanHuyen);  pnlEditable.Controls.Add(txtQuanHuyen);
        pnlEditable.Controls.Add(lblTinhTP);     pnlEditable.Controls.Add(txtTinhTP);
        pnlEditable.Controls.Add(lblTienSuBenh); pnlEditable.Controls.Add(txtTienSuBenh);
        pnlEditable.Controls.Add(lblTienSuBenhGD); pnlEditable.Controls.Add(txtTienSuBenhGD);
        pnlEditable.Controls.Add(lblDiUngThuoc); pnlEditable.Controls.Add(txtDiUngThuoc);
        pnlEditable.Controls.Add(btnSave);

        // lblSectionEdit
        lblSectionEdit.Text = "Địa chỉ & Tiền sử bệnh (có thể chỉnh sửa)";
        lblSectionEdit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblSectionEdit.ForeColor = Color.FromArgb(100, 180, 255);
        lblSectionEdit.Location = new Point(15, 8);
        lblSectionEdit.AutoSize = true;
        lblSectionEdit.Name = "lblSectionEdit";
        lblSectionEdit.TabIndex = 0;

        // btnSave
        btnSave.Text = "Lưu thay đổi";
        btnSave.BackColor = Color.FromArgb(21, 101, 192);
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.ForeColor = Color.White;
        btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnSave.Location = new Point(140, 435);
        btnSave.Size = new Size(160, 36);
        btnSave.Name = "btnSave";
        btnSave.TabIndex = 20;
        btnSave.UseVisualStyleBackColor = false;
        btnSave.Click += btnSave_Click;

        // tabHsba
        tabHsba.BackColor = Color.FromArgb(15, 15, 26);
        tabHsba.Controls.Add(dgvHsba);
        tabHsba.Name = "tabHsba";
        tabHsba.TabIndex = 1;
        tabHsba.Text = "  \U0001f4c1  Hồ sơ bệnh án  ";
        dgvHsba.Dock = DockStyle.Fill;
        dgvHsba.Name = "dgvHsba";

        // tabPrescriptions
        tabPrescriptions.BackColor = Color.FromArgb(15, 15, 26);
        tabPrescriptions.Controls.Add(dgvPrescriptions);
        tabPrescriptions.Name = "tabPrescriptions";
        tabPrescriptions.TabIndex = 2;
        tabPrescriptions.Text = "  \U0001f48a  Đơn thuốc  ";
        dgvPrescriptions.Dock = DockStyle.Fill;
        dgvPrescriptions.Name = "dgvPrescriptions";

        // PatientDashboard
        BackColor = Color.FromArgb(15, 15, 26);
        Controls.Add(tabControl);
        Font = new Font("Segoe UI", 9F);
        Name = "PatientDashboard";
        Size = new Size(1100, 686);

        tabControl.ResumeLayout(false);
        tabInfo.ResumeLayout(false);
        pnlReadOnly.ResumeLayout(false);
        pnlReadOnly.PerformLayout();
        pnlEditable.ResumeLayout(false);
        pnlEditable.PerformLayout();
        ResumeLayout(false);
    }

    private static Label CreateLabel(string text, int left, int top, bool bold = false)
        => new Label { Text = text, Font = new Font("Segoe UI", 9f, bold ? FontStyle.Bold : FontStyle.Regular), ForeColor = Color.FromArgb(150, 180, 220), AutoSize = false, Width = 110, Height = 25, Left = left, Top = top, TextAlign = ContentAlignment.MiddleRight };

    private static Label CreateValueLabel(string text, int left, int top)
        => new Label { Text = text, Font = new Font("Segoe UI", 9f, FontStyle.Bold), ForeColor = Color.White, AutoSize = false, Width = 280, Height = 25, Left = left, Top = top, TextAlign = ContentAlignment.MiddleLeft };

    private static Label CreateFieldLabel(string text, int left, int top)
        => new Label { Text = text, Font = new Font("Segoe UI", 9f, FontStyle.Bold), ForeColor = Color.FromArgb(120, 180, 250), AutoSize = false, Width = 120, Height = 28, Left = left, Top = top, TextAlign = ContentAlignment.MiddleLeft };

    private static TextBox CreateEditField(int left, int top)
        => new TextBox { Width = 280, Height = 30, Left = left, Top = top, BackColor = Color.FromArgb(30, 30, 55), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 10f) };

    private static TextBox CreateMultilineField(int left, int top, int width)
        => new TextBox { Width = width, Height = 60, Left = left, Top = top, BackColor = Color.FromArgb(30, 30, 55), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 10f), Multiline = true, ScrollBars = ScrollBars.Vertical };

    private static DataGridView CreateDGV()
    {
        var dgv = new DataGridView
        {
            BackgroundColor = Color.FromArgb(18, 18, 35), ForeColor = Color.White, GridColor = Color.FromArgb(50, 50, 80),
            BorderStyle = BorderStyle.None, SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            RowHeadersVisible = false, AllowUserToAddRows = false, AllowUserToDeleteRows = false
        };
        dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 33, 62);
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(100, 181, 246);
        dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
        dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(22, 33, 62);
        dgv.ColumnHeadersHeight = 35;
        dgv.DefaultCellStyle.BackColor = Color.FromArgb(18, 18, 35);
        dgv.DefaultCellStyle.ForeColor = Color.White;
        dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(30, 80, 160);
        dgv.DefaultCellStyle.SelectionForeColor = Color.White;
        dgv.DefaultCellStyle.Padding = new Padding(3);
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(22, 22, 42);
        dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;
        dgv.RowTemplate.Height = 30;
        dgv.EnableHeadersVisualStyles = false;
        return dgv;
    }
}
