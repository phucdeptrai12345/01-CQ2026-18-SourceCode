namespace HospitalSystem.Forms.Doctor;

partial class DoctorDashboard
{
    private System.ComponentModel.IContainer components = null;

    private TabControl tabControl;
    private TabPage tabHsba;
    private TabPage tabServices;
    private TabPage tabPrescriptions;

    private DataGridView dgvHsba;
    private DataGridView dgvServices;
    private DataGridView dgvPrescriptions;

    private Panel pnlHsbaToolbar;
    private Button btnViewDetail;
    private Button btnUpdateDiagnosis;
    private Button btnUpdatePatientMedical;
    private Label lblHsbaCount;

    private Panel pnlPresToolbar;
    private Button btnAddPrescription;
    private Button btnEditPrescription;
    private Button btnDeletePrescription;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        tabControl = new TabControl();
        tabHsba = new TabPage();
        pnlHsbaToolbar = new Panel();
        btnViewDetail = new Button();
        btnUpdateDiagnosis = new Button();
        btnUpdatePatientMedical = new Button();
        lblHsbaCount = new Label();
        dgvHsba = CreateDGV();
        tabServices = new TabPage();
        dgvServices = CreateDGV();
        tabPrescriptions = new TabPage();
        pnlPresToolbar = new Panel();
        btnAddPrescription = new Button();
        btnEditPrescription = new Button();
        btnDeletePrescription = new Button();
        dgvPrescriptions = CreateDGV();
        tabControl.SuspendLayout();
        tabHsba.SuspendLayout();
        pnlHsbaToolbar.SuspendLayout();
        tabPrescriptions.SuspendLayout();
        pnlPresToolbar.SuspendLayout();
        SuspendLayout();
        // 
        // tabControl
        // 
        tabControl.Controls.Add(tabHsba);
        tabControl.Controls.Add(tabServices);
        tabControl.Controls.Add(tabPrescriptions);
        tabControl.Dock = DockStyle.Fill;
        tabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
        tabControl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        tabControl.Location = new Point(0, 0);
        tabControl.Name = "tabControl";
        tabControl.Padding = new Point(12, 8);
        tabControl.SelectedIndex = 0;
        tabControl.Size = new Size(1110, 686);
        tabControl.TabIndex = 0;
        tabControl.DrawItem += TabControl_DrawItem;
        //
        // dgvHsba
        //
        dgvHsba.Dock = DockStyle.Fill;
        dgvHsba.Name = "dgvHsba";
        dgvHsba.SelectionChanged += dgvHsba_SelectionChanged;
        //
        // dgvServices
        //
        dgvServices.Dock = DockStyle.Fill;
        dgvServices.Name = "dgvServices";
        //
        // dgvPrescriptions
        //
        dgvPrescriptions.Dock = DockStyle.Fill;
        dgvPrescriptions.Name = "dgvPrescriptions";
        //
        // tabHsba
        //
        tabHsba.BackColor = Color.FromArgb(15, 15, 26);
        tabHsba.Controls.Add(dgvHsba);
        tabHsba.Controls.Add(pnlHsbaToolbar);
        tabHsba.Location = new Point(4, 36);
        tabHsba.Name = "tabHsba";
        tabHsba.Size = new Size(1102, 646);
        tabHsba.TabIndex = 0;
        tabHsba.Text = "  📁  HSBA của tôi  ";
        // 
        // pnlHsbaToolbar
        // 
        pnlHsbaToolbar.BackColor = Color.FromArgb(20, 20, 40);
        pnlHsbaToolbar.Controls.Add(btnViewDetail);
        pnlHsbaToolbar.Controls.Add(btnUpdateDiagnosis);
        pnlHsbaToolbar.Controls.Add(btnUpdatePatientMedical);
        pnlHsbaToolbar.Controls.Add(lblHsbaCount);
        pnlHsbaToolbar.Dock = DockStyle.Top;
        pnlHsbaToolbar.Location = new Point(0, 0);
        pnlHsbaToolbar.Name = "pnlHsbaToolbar";
        pnlHsbaToolbar.Padding = new Padding(10);
        pnlHsbaToolbar.Size = new Size(1102, 50);
        pnlHsbaToolbar.TabIndex = 0;
        // 
        // btnViewDetail
        // 
        btnViewDetail.BackColor = Color.FromArgb(21, 101, 192);
        btnViewDetail.FlatAppearance.BorderSize = 0;
        btnViewDetail.FlatStyle = FlatStyle.Flat;
        btnViewDetail.ForeColor = Color.White;
        btnViewDetail.Location = new Point(10, 0);
        btnViewDetail.Name = "btnViewDetail";
        btnViewDetail.Size = new Size(140, 32);
        btnViewDetail.TabIndex = 0;
        btnViewDetail.Text = "🔍  Xem chi tiết";
        btnViewDetail.UseVisualStyleBackColor = false;
        btnViewDetail.Click += btnViewDetail_Click;
        // 
        // btnUpdateDiagnosis
        // 
        btnUpdateDiagnosis.BackColor = Color.FromArgb(46, 125, 50);
        btnUpdateDiagnosis.FlatAppearance.BorderSize = 0;
        btnUpdateDiagnosis.FlatStyle = FlatStyle.Flat;
        btnUpdateDiagnosis.ForeColor = Color.White;
        btnUpdateDiagnosis.Location = new Point(160, 0);
        btnUpdateDiagnosis.Name = "btnUpdateDiagnosis";
        btnUpdateDiagnosis.Size = new Size(140, 32);
        btnUpdateDiagnosis.TabIndex = 1;
        btnUpdateDiagnosis.Text = "✏️  Cập nhật CĐ";
        btnUpdateDiagnosis.UseVisualStyleBackColor = false;
        btnUpdateDiagnosis.Click += btnUpdateDiagnosis_Click;
        // 
        // btnUpdatePatientMedical
        // 
        btnUpdatePatientMedical.BackColor = Color.FromArgb(100, 60, 140);
        btnUpdatePatientMedical.FlatAppearance.BorderSize = 0;
        btnUpdatePatientMedical.FlatStyle = FlatStyle.Flat;
        btnUpdatePatientMedical.ForeColor = Color.White;
        btnUpdatePatientMedical.Location = new Point(310, 0);
        btnUpdatePatientMedical.Name = "btnUpdatePatientMedical";
        btnUpdatePatientMedical.Size = new Size(140, 32);
        btnUpdatePatientMedical.TabIndex = 2;
        btnUpdatePatientMedical.Text = "\U0001fa7a  Tiền sử BN";
        btnUpdatePatientMedical.UseVisualStyleBackColor = false;
        btnUpdatePatientMedical.Click += btnUpdatePatientMedical_Click;
        // 
        // lblHsbaCount
        // 
        lblHsbaCount.Dock = DockStyle.Right;
        lblHsbaCount.ForeColor = Color.FromArgb(150, 200, 255);
        lblHsbaCount.Location = new Point(872, 10);
        lblHsbaCount.Name = "lblHsbaCount";
        lblHsbaCount.Size = new Size(220, 30);
        lblHsbaCount.TabIndex = 3;
        lblHsbaCount.Text = "Đang tải...";
        lblHsbaCount.TextAlign = ContentAlignment.MiddleRight;
        // 
        // tabServices
        // 
        tabServices.BackColor = Color.FromArgb(15, 15, 26);
        tabServices.Controls.Add(dgvServices);
        tabServices.Location = new Point(4, 36);
        tabServices.Name = "tabServices";
        tabServices.Size = new Size(1102, 646);
        tabServices.TabIndex = 1;
        tabServices.Text = "  🔬  Dịch vụ chẩn đoán  ";
        // 
        // tabPrescriptions
        // 
        tabPrescriptions.BackColor = Color.FromArgb(15, 15, 26);
        tabPrescriptions.Controls.Add(dgvPrescriptions);
        tabPrescriptions.Controls.Add(pnlPresToolbar);
        tabPrescriptions.Location = new Point(4, 36);
        tabPrescriptions.Name = "tabPrescriptions";
        tabPrescriptions.Size = new Size(1102, 646);
        tabPrescriptions.TabIndex = 2;
        tabPrescriptions.Text = "  💊  Đơn thuốc  ";
        // 
        // pnlPresToolbar
        // 
        pnlPresToolbar.BackColor = Color.FromArgb(20, 20, 40);
        pnlPresToolbar.Controls.Add(btnAddPrescription);
        pnlPresToolbar.Controls.Add(btnEditPrescription);
        pnlPresToolbar.Controls.Add(btnDeletePrescription);
        pnlPresToolbar.Dock = DockStyle.Top;
        pnlPresToolbar.Location = new Point(0, 0);
        pnlPresToolbar.Name = "pnlPresToolbar";
        pnlPresToolbar.Padding = new Padding(10);
        pnlPresToolbar.Size = new Size(1102, 50);
        pnlPresToolbar.TabIndex = 0;
        // 
        // btnAddPrescription
        // 
        btnAddPrescription.BackColor = Color.FromArgb(21, 101, 192);
        btnAddPrescription.FlatAppearance.BorderSize = 0;
        btnAddPrescription.FlatStyle = FlatStyle.Flat;
        btnAddPrescription.ForeColor = Color.White;
        btnAddPrescription.Location = new Point(10, 0);
        btnAddPrescription.Name = "btnAddPrescription";
        btnAddPrescription.Size = new Size(120, 32);
        btnAddPrescription.TabIndex = 0;
        btnAddPrescription.Text = "➕  Thêm thuốc";
        btnAddPrescription.UseVisualStyleBackColor = false;
        btnAddPrescription.Click += btnAddPrescription_Click;
        // 
        // btnEditPrescription
        // 
        btnEditPrescription.BackColor = Color.FromArgb(46, 125, 50);
        btnEditPrescription.FlatAppearance.BorderSize = 0;
        btnEditPrescription.FlatStyle = FlatStyle.Flat;
        btnEditPrescription.ForeColor = Color.White;
        btnEditPrescription.Location = new Point(140, 0);
        btnEditPrescription.Name = "btnEditPrescription";
        btnEditPrescription.Size = new Size(120, 32);
        btnEditPrescription.TabIndex = 1;
        btnEditPrescription.Text = "✏️  Sửa liều";
        btnEditPrescription.UseVisualStyleBackColor = false;
        btnEditPrescription.Click += btnEditPrescription_Click;
        // 
        // btnDeletePrescription
        // 
        btnDeletePrescription.BackColor = Color.FromArgb(183, 28, 28);
        btnDeletePrescription.FlatAppearance.BorderSize = 0;
        btnDeletePrescription.FlatStyle = FlatStyle.Flat;
        btnDeletePrescription.ForeColor = Color.White;
        btnDeletePrescription.Location = new Point(270, 0);
        btnDeletePrescription.Name = "btnDeletePrescription";
        btnDeletePrescription.Size = new Size(120, 32);
        btnDeletePrescription.TabIndex = 2;
        btnDeletePrescription.Text = "🗑️  Xóa thuốc";
        btnDeletePrescription.UseVisualStyleBackColor = false;
        btnDeletePrescription.Click += btnDeletePrescription_Click;
        // 
        // DoctorDashboard
        // 
        BackColor = Color.FromArgb(15, 15, 26);
        Controls.Add(tabControl);
        Font = new Font("Segoe UI", 9F);
        Name = "DoctorDashboard";
        Size = new Size(1110, 686);
        tabControl.ResumeLayout(false);
        tabHsba.ResumeLayout(false);
        pnlHsbaToolbar.ResumeLayout(false);
        tabPrescriptions.ResumeLayout(false);
        pnlPresToolbar.ResumeLayout(false);
        ResumeLayout(false);
    }

    private static Button CreateBtn(string text, Color color, int left)
    {
        var btn = new Button { Text = text, Width = 120, Height = 32, Top = 9, Left = left, BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9f, FontStyle.Bold), Cursor = Cursors.Hand };
        btn.FlatAppearance.BorderSize = 0;
        return btn;
    }

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
