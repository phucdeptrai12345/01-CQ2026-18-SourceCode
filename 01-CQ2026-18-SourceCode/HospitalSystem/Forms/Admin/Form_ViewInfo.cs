using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HospitalSystem.Core;

namespace HospitalSystem.Forms.Admin
{
    public partial class Form_ViewInfo : Form
    {
        // Blacklist user/role hệ thống Oracle (đồng bộ với Form_Privileges)
        private const string UserBlacklist =
            "'SYS', 'SYSTEM', 'DBSNMP', 'OUTLN', 'WMSYS', 'CTXSYS', 'XDB', 'ORDDATA', " +
            "'MDSYS', 'ORDPLUGINS', 'SI_INFORMTN_SCHEMA', 'ORDSYS', 'OLAPSYS', 'MDDATA', " +
            "'SPATIAL_WFS_ADMIN_USR', 'SPATIAL_CSW_ADMIN_USR', 'FLOWS_FILES', " +
            "'APEX_PUBLIC_USER', 'ANONYMOUS', 'DVSYS', 'DVF', 'AUDSYS', 'LBACSYS', " +
            "'GSMADMIN_INTERNAL', 'GSMUSER', 'GSMROOTUSER', 'DIP', 'REMOTE_SCHEDULER_AGENT', " +
            "'SYSBACKUP', 'SYSDG', 'SYSKM', 'SYSRAC', 'APPQOSSYS', 'DBSFWUSER', " +
            "'OJVMSYS', 'XS$NULL'";

        private const string RoleBlacklist =
            "'CONNECT', 'RESOURCE', 'DBA', 'SELECT_CATALOG_ROLE', 'EXECUTE_CATALOG_ROLE', " +
            "'DELETE_CATALOG_ROLE', 'EXP_FULL_DATABASE', 'IMP_FULL_DATABASE', " +
            "'DATAPUMP_EXP_FULL_DATABASE', 'DATAPUMP_IMP_FULL_DATABASE', " +
            "'GATHER_SYSTEM_STATISTICS', 'LOGSTDBY_ADMINISTRATOR', 'AQ_ADMINISTRATOR_ROLE', " +
            "'AQ_USER_ROLE', 'SCHEDULER_ADMIN', 'HS_ADMIN_SELECT_ROLE', 'HS_ADMIN_ROLE', " +
            "'GLOBAL_AQ_USER_ROLE', 'OEM_ADVISOR', 'OEM_MONITOR', 'WM_ADMIN_ROLE', " +
            "'JAVAUSERPRIV', 'JAVAIDPRIV', 'JAVASYSPRIV', 'JAVADEBUGPRIV', 'EJBCLIENT', " +
            "'JMXSERVER', 'JAVA_ADMIN', 'JAVA_DEPLOY', 'XDBADMIN', 'XDB_SET_INVOKER', " +
            "'AUTHENTICATEDUSER', 'XDB_WEBSERVICES', 'XDB_WEBSERVICES_WITH_PUBLIC', " +
            "'XDB_WEBSERVICES_OVER_HTTP'";

        public Form_ViewInfo()
        {
            InitializeComponent();
        }

        private void Form_ViewInfo_Load(object sender, EventArgs e)
        {
            dgvUserRole.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPrivs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadUserAndRoles();
            LoadPrivileges();
        }

        public void LoadUserAndRoles()
        {
            try
            {
                // Dùng blacklist thay oracle_maintained='N' để hiển thị đúng user/role vừa tạo
                string sql =
                    $"SELECT username AS \"Tên\", 'USER' AS \"Loại\" FROM DBA_USERS " +
                    $"WHERE username NOT IN ({UserBlacklist}) " +
                    "UNION " +
                    $"SELECT role AS \"Tên\", 'ROLE' AS \"Loại\" FROM DBA_ROLES " +
                    $"WHERE role NOT IN ({RoleBlacklist}) " +
                    "ORDER BY 2, 1";

                dgvUserRole.DataSource = Core.AdminDBHelper.GetDataTable(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        public void LoadPrivileges()
        {
            try
            {
                // Hiển thị tổng quan: object privileges trên schema SYSTEM
                string sql =
                    "SELECT grantee AS \"Người nhận\", table_name AS \"Đối tượng\", " +
                    "privilege AS \"Quyền\", grantable AS \"Cấp lại?\" " +
                    "FROM DBA_TAB_PRIVS WHERE owner = 'SYSTEM' ORDER BY grantee, table_name";

                dgvPrivs.DataSource = Core.AdminDBHelper.GetDataTable(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void LoadPrivilegesOfTarget(string targetName)
        {
            try
            {
                targetName = targetName.ToUpper();

                // Gộp 3 loại quyền: system privilege, role privilege, object privilege
                string sql =
                    $"SELECT 'HỆ THỐNG' AS \"Loại\", privilege AS \"Quyền/Role/Đối tượng\", " +
                    "NULL AS \"Cột\", admin_option AS \"Cấp lại?\", NULL AS \"Người cấp\" " +
                    $"FROM DBA_SYS_PRIVS WHERE grantee = '{targetName}' " +
                    "UNION ALL " +
                    $"SELECT 'ROLE' AS \"Loại\", granted_role AS \"Quyền/Role/Đối tượng\", " +
                    "NULL AS \"Cột\", admin_option AS \"Cấp lại?\", NULL AS \"Người cấp\" " +
                    $"FROM DBA_ROLE_PRIVS WHERE grantee = '{targetName}' " +
                    "UNION ALL " +
                    $"SELECT 'ĐỐI TƯỢNG' AS \"Loại\", owner || '.' || table_name AS \"Quyền/Role/Đối tượng\", " +
                    "NULL AS \"Cột\", grantable AS \"Cấp lại?\", grantor AS \"Người cấp\" " +
                    $"FROM DBA_TAB_PRIVS WHERE grantee = '{targetName}' " +
                    "UNION ALL " +
                    $"SELECT 'CỘT' AS \"Loại\", owner || '.' || table_name AS \"Quyền/Role/Đối tượng\", " +
                    "column_name AS \"Cột\", grantable AS \"Cấp lại?\", grantor AS \"Người cấp\" " +
                    $"FROM DBA_COL_PRIVS WHERE grantee = '{targetName}' " +
                    "ORDER BY 1, 2";

                dgvPrivs.DataSource = Core.AdminDBHelper.GetDataTable(sql);
                lblPrivilegeTitle.Text = "Quyền của: " + targetName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải quyền: " + ex.Message);
            }
        }

        private void dgvUserRole_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu người dùng click vào dòng hợp lệ (không phải tiêu đề)
            if (e.RowIndex >= 0)
            {
                // Lấy giá trị của cột "Tên" ở dòng vừa click
                var cellValue = dgvUserRole.Rows[e.RowIndex].Cells[0].Value;
                string selectedTarget = cellValue?.ToString() ?? string.Empty;

                if (!string.IsNullOrEmpty(selectedTarget))
                {
                    // Gọi hàm load quyền đã viết ở trên
                    LoadPrivilegesOfTarget(selectedTarget);
                }
            }
        }
    }
}
