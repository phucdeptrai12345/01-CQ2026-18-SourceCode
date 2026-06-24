namespace HospitalSystem.Forms.Technician;

partial class TechnicianDashboard
{
    private System.ComponentModel.IContainer components = null;
    private DataGridView dgvServices;
    private Panel pnlToolbar;
    private Button btnUpdateResult;
    private Button btnRefresh;
    private Label lblTitle;
    private Label lblCount;
    private Label lblInfo;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        lblTitle = new Label();
        pnlToolbar = new Panel();
        btnUpdateResult = new Button();
        btnRefresh = new Button();
        lblCount = new Label();
        lblInfo = new Label();
        dgvServices = new DataGridView();
        pnlToolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvServices).BeginInit();
        SuspendLayout();
        // 
        // lblTitle
        // 
        lblTitle.Text = "Dịch vụ của tôi";
        lblTitle.Dock = DockStyle.Top;
        lblTitle.Height = 44;
        lblTitle.ForeColor = Color.FromArgb(200, 200, 255);
        lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitle.Padding = new Padding(12, 8, 0, 0);
        // 
        // pnlToolbar
        // 
        pnlToolbar.Dock = DockStyle.Top;
        pnlToolbar.Height = 50;
        pnlToolbar.BackColor = Color.FromArgb(20, 20, 40);
        pnlToolbar.Padding = new Padding(10);
        // 
        // btnUpdateResult
        // 
        btnUpdateResult.Text = "Cập nhật kết quả";
        btnUpdateResult.Width = 140;
        btnUpdateResult.Height = 32;
        btnUpdateResult.Left = 10;
        btnUpdateResult.FlatStyle = FlatStyle.Flat;
        btnUpdateResult.FlatAppearance.BorderSize = 0;
        btnUpdateResult.BackColor = Color.FromArgb(21, 101, 192);
        btnUpdateResult.ForeColor = Color.White;
        btnUpdateResult.Click += btnUpdateResult_Click;
        // 
        // btnRefresh
        // 
        btnRefresh.Text = "Làm mới";
        btnRefresh.Width = 100;
        btnRefresh.Height = 32;
        btnRefresh.Left = 160;
        btnRefresh.FlatStyle = FlatStyle.Flat;
        btnRefresh.FlatAppearance.BorderSize = 0;
        btnRefresh.BackColor = Color.FromArgb(46, 125, 50);
        btnRefresh.ForeColor = Color.White;
        btnRefresh.Click += btnRefresh_Click;
        // 
        // lblCount
        // 
        lblCount.Text = "Đang tải...";
        lblCount.AutoSize = false;
        lblCount.Width = 220;
        lblCount.TextAlign = ContentAlignment.MiddleRight;
        lblCount.ForeColor = Color.FromArgb(150, 200, 255);
        lblCount.Dock = DockStyle.Right;
        // 
        pnlToolbar.Controls.AddRange(new Control[] { btnUpdateResult, btnRefresh, lblCount });
        // 
        // lblInfo
        // 
        lblInfo.Text = "";
        lblInfo.Dock = DockStyle.Bottom;
        lblInfo.Height = 24;
        lblInfo.ForeColor = Color.FromArgb(180, 180, 200);
        lblInfo.Padding = new Padding(10, 4, 0, 0);
        // 
        // dgvServices
        // 
        dataGridViewCellStyle1.BackColor = Color.FromArgb(22, 22, 42);
        dataGridViewCellStyle1.ForeColor = Color.White;
        dgvServices.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = Color.FromArgb(22, 33, 62);
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dataGridViewCellStyle2.ForeColor = Color.FromArgb(100, 181, 246);
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(22, 33, 62);
        dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
        dgvServices.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        dgvServices.ColumnHeadersHeight = 35;
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = Color.FromArgb(18, 18, 35);
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle3.ForeColor = Color.White;
        dataGridViewCellStyle3.Padding = new Padding(3);
        dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(30, 80, 160);
        dataGridViewCellStyle3.SelectionForeColor = Color.White;
        dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
        dgvServices.DefaultCellStyle = dataGridViewCellStyle3;
        dgvServices.EnableHeadersVisualStyles = false;
        dgvServices.Dock = DockStyle.Fill;
        dgvServices.Name = "dgvServices";
        dgvServices.RowTemplate.Height = 30;
        dgvServices.TabIndex = 0;
        // 
        // TechnicianDashboard
        // 
        BackColor = Color.FromArgb(15, 15, 26);
        Controls.Add(dgvServices);
        Controls.Add(pnlToolbar);
        Controls.Add(lblTitle);
        Controls.Add(lblInfo);
        Font = new Font("Segoe UI", 9F);
        Name = "TechnicianDashboard";
        Size = new Size(1110, 686);
        pnlToolbar.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvServices).EndInit();
        ResumeLayout(false);
    }
}
