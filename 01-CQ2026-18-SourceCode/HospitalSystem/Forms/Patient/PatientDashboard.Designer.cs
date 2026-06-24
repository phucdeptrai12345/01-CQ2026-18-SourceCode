namespace HospitalSystem.Forms.Patient;

partial class PatientDashboard
{
    private System.ComponentModel.IContainer components = null;

    // Tab control
    private TabControl tabControl;
    private TabPage tabInfo;
    private TabPage tabHsba;
    private TabPage tabPrescriptions;

    // Tab 1: Thông tin cá nhân
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

    // Tab 2: Hồ sơ bệnh án
    private DataGridView dgvHsba;

    // Tab 3: Đơn thuốc
    private DataGridView dgvPrescriptions;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        tabControl = new TabControl();
        tabInfo = new TabPage();
        pnlEditable = new Panel();
        lblSectionEdit = new Label();
        btnSave = new Button();
        pnlReadOnly = new Panel();
        lblSectionInfo = new Label();
        lblTitle = new Label();
        tabHsba = new TabPage();
        tabPrescriptions = new TabPage();
        tabControl.SuspendLayout();
        tabInfo.SuspendLayout();
        pnlEditable.SuspendLayout();
        pnlReadOnly.SuspendLayout();
        SuspendLayout();
        // 
        // tabControl
        // 
        tabControl.Controls.Add(tabInfo);
        tabControl.Controls.Add(tabHsba);
        tabControl.Controls.Add(tabPrescriptions);
        tabControl.Location = new Point(0, 0);
        tabControl.Name = "tabControl";
        tabControl.SelectedIndex = 0;
        tabControl.Size = new Size(200, 100);
        tabControl.TabIndex = 0;
        tabControl.DrawItem += TabControl_DrawItem;
        tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
        // 
        // tabInfo
        // 
        tabInfo.Controls.Add(pnlEditable);
        tabInfo.Controls.Add(pnlReadOnly);
        tabInfo.Controls.Add(lblTitle);
        tabInfo.Location = new Point(4, 24);
        tabInfo.Name = "tabInfo";
        tabInfo.Size = new Size(192, 72);
        tabInfo.TabIndex = 0;
        // 
        // pnlEditable
        // 
        pnlEditable.Controls.Add(lblSectionEdit);
        pnlEditable.Controls.Add(btnSave);
        pnlEditable.Location = new Point(0, 0);
        pnlEditable.Name = "pnlEditable";
        pnlEditable.Size = new Size(200, 100);
        pnlEditable.TabIndex = 0;
        // 
        // lblSectionEdit
        // 
        lblSectionEdit.Location = new Point(0, 0);
        lblSectionEdit.Name = "lblSectionEdit";
        lblSectionEdit.Size = new Size(100, 23);
        lblSectionEdit.TabIndex = 0;
        // 
        // btnSave
        // 
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 118, 210);
        btnSave.Location = new Point(0, 0);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(75, 23);
        btnSave.TabIndex = 1;
        btnSave.Click += btnSave_Click;
        // 
        // pnlReadOnly
        // 
        pnlReadOnly.Controls.Add(lblSectionInfo);
        pnlReadOnly.Location = new Point(0, 0);
        pnlReadOnly.Name = "pnlReadOnly";
        pnlReadOnly.Size = new Size(200, 100);
        pnlReadOnly.TabIndex = 1;
        // 
        // lblSectionInfo
        // 
        lblSectionInfo.Location = new Point(0, 0);
        lblSectionInfo.Name = "lblSectionInfo";
        lblSectionInfo.Size = new Size(100, 23);
        lblSectionInfo.TabIndex = 0;
        // 
        // lblTitle
        // 
        lblTitle.Location = new Point(0, 0);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(100, 23);
        lblTitle.TabIndex = 2;
        // 
        // tabHsba
        // 
        tabHsba.Location = new Point(4, 24);
        tabHsba.Name = "tabHsba";
        tabHsba.Size = new Size(192, 72);
        tabHsba.TabIndex = 1;
        // 
        // tabPrescriptions
        // 
        tabPrescriptions.Location = new Point(4, 24);
        tabPrescriptions.Name = "tabPrescriptions";
        tabPrescriptions.Size = new Size(192, 72);
        tabPrescriptions.TabIndex = 2;
        // 
        // PatientDashboard
        // 
        BackColor = Color.FromArgb(15, 15, 26);
        Controls.Add(tabControl);
        Font = new Font("Segoe UI", 9F);
        Name = "PatientDashboard";
        Size = new Size(709, 686);
        tabControl.ResumeLayout(false);
        tabInfo.ResumeLayout(false);
        pnlEditable.ResumeLayout(false);
        pnlReadOnly.ResumeLayout(false);
        ResumeLayout(false);
    }

    private static Label CreateLabel(string text, int left, int top, bool bold = false)
        => new Label { Text = text, Font = new Font("Segoe UI", 9f, bold ? FontStyle.Bold : FontStyle.Regular), ForeColor = Color.FromArgb(150, 180, 220), AutoSize = false, Width = 95, Height = 25, Left = left, Top = top, TextAlign = ContentAlignment.MiddleRight };

    private static Label CreateValueLabel(string text, int left, int top)
        => new Label { Text = text, Font = new Font("Segoe UI", 9f, FontStyle.Bold), ForeColor = Color.White, AutoSize = false, Width = 200, Height = 25, Left = left, Top = top, TextAlign = ContentAlignment.MiddleLeft };

    private static Label CreateFieldLabel(string text, int left, int top)
        => new Label { Text = text, Font = new Font("Segoe UI", 9f, FontStyle.Bold), ForeColor = Color.FromArgb(120, 180, 250), AutoSize = false, Width = 115, Height = 28, Left = left, Top = top, TextAlign = ContentAlignment.MiddleLeft };

    private static TextBox CreateEditField(int left, int top)
        => new TextBox { Width = 260, Height = 30, Left = left, Top = top, BackColor = Color.FromArgb(30, 30, 55), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 10f) };

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
