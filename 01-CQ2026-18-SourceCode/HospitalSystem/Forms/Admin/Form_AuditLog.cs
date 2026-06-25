using System.Data;
using HospitalSystem.Core;

namespace HospitalSystem.Forms.Admin;

public class Form_AuditLog : Form
{
    private TabControl tabControl;
    private TabPage tabAudit, tabVpd, tabBackup;
    private DataGridView dgvAudit, dgvVpd, dgvBackup;
    private Button btnRefreshAudit, btnRefreshVpd, btnRefreshBackup;

    public Form_AuditLog()
    {
        InitUI();
        LoadAll();
    }

    private void InitUI()
    {
        Text = "Nhật ký & Giám sát hệ thống";
        BackColor = Color.FromArgb(245, 245, 250);

        tabControl = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10f, FontStyle.Bold) };

        tabAudit = new TabPage("  📋  Nhật ký Audit  ");
        tabVpd   = new TabPage("  🔒  VPD Policies  ");
        tabBackup = new TabPage("  💾  Backup Jobs  ");

        dgvAudit  = CreateDGV();
        dgvVpd    = CreateDGV();
        dgvBackup = CreateDGV();

        btnRefreshAudit  = CreateRefreshBtn();
        btnRefreshVpd    = CreateRefreshBtn();
        btnRefreshBackup = CreateRefreshBtn();

        btnRefreshAudit.Click  += (s, e) => LoadAuditLog();
        btnRefreshVpd.Click    += (s, e) => LoadVpdPolicies();
        btnRefreshBackup.Click += (s, e) => LoadBackupJobs();

        tabAudit.Controls.Add(dgvAudit);
        tabAudit.Controls.Add(btnRefreshAudit);

        tabVpd.Controls.Add(dgvVpd);
        tabVpd.Controls.Add(btnRefreshVpd);

        tabBackup.Controls.Add(dgvBackup);
        tabBackup.Controls.Add(btnRefreshBackup);

        tabControl.TabPages.Add(tabAudit);
        tabControl.TabPages.Add(tabVpd);
        tabControl.TabPages.Add(tabBackup);

        Controls.Add(tabControl);
    }

    private static DataGridView CreateDGV()
    {
        var dgv = new DataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AllowUserToAddRows = false,
            RowHeadersVisible = false,
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.None,
            Font = new Font("Segoe UI", 9f)
        };
        dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 33, 80);
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
        dgv.EnableHeadersVisualStyles = false;
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 244, 255);
        dgv.RowTemplate.Height = 28;
        return dgv;
    }

    private static Button CreateRefreshBtn() => new Button
    {
        Text = "↻  Làm mới",
        Dock = DockStyle.Bottom,
        Height = 36,
        BackColor = Color.FromArgb(21, 101, 192),
        ForeColor = Color.White,
        FlatStyle = FlatStyle.Flat,
        Font = new Font("Segoe UI", 9f, FontStyle.Bold)
    };

    private void LoadAll()
    {
        LoadAuditLog();
        LoadVpdPolicies();
        LoadBackupJobs();
    }

    private void LoadAuditLog()
    {
        try
        {
            string sql =
                "SELECT LOG_ID AS \"#\", TABLE_NAME AS \"Bảng\", ACTION AS \"Thao tác\", " +
                "COLUMN_NAME AS \"Cột\", " +
                "SUBSTR(OLD_VAL,1,60) AS \"Giá trị cũ\", " +
                "SUBSTR(NEW_VAL,1,60) AS \"Giá trị mới\", " +
                "USER_NAME AS \"Người dùng\", " +
                "TO_CHAR(CHANGED_AT,'DD/MM/YYYY HH24:MI:SS') AS \"Thời gian\", " +
                "OS_USER AS \"OS User\", HOST_NAME AS \"Host\" " +
                "FROM AUDIT_LOG ORDER BY CHANGED_AT DESC";
            dgvAudit.DataSource = AdminDBHelper.GetDataTable(sql);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi tải Audit Log: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadVpdPolicies()
    {
        try
        {
            string sql =
                "SELECT OBJECT_NAME AS \"Bảng/View\", POLICY_NAME AS \"Tên Policy\", " +
                "FUNCTION AS \"Hàm VPD\", " +
                "SEL AS \"SELECT\", INS AS \"INSERT\", UPD AS \"UPDATE\", DEL AS \"DELETE\", " +
                "ENABLE AS \"Bật?\" " +
                "FROM DBA_POLICIES WHERE OBJECT_OWNER = 'SYSTEM' ORDER BY OBJECT_NAME";
            dgvVpd.DataSource = AdminDBHelper.GetDataTable(sql);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi tải VPD Policies: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadBackupJobs()
    {
        try
        {
            string sql =
                "SELECT JOB_NAME AS \"Tên Job\", JOB_TYPE AS \"Loại\", STATE AS \"Trạng thái\", " +
                "ENABLED AS \"Bật?\", " +
                "TO_CHAR(NEXT_RUN_DATE,'DD/MM/YYYY HH24:MI') AS \"Lần chạy tiếp\", " +
                "REPEAT_INTERVAL AS \"Lịch lặp\" " +
                "FROM DBA_SCHEDULER_JOBS " +
                "WHERE JOB_NAME IN ('JOB_DAILY_BACKUP','JOB_WEEKLY_BACKUP') " +
                "ORDER BY JOB_NAME";
            dgvBackup.DataSource = AdminDBHelper.GetDataTable(sql);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi tải Backup Jobs: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
