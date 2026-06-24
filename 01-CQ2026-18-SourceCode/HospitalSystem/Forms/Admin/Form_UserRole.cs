using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HospitalSystem.Core;

namespace HospitalSystem.Forms.Admin
{
    public partial class Form_UserRole : Form
    {
        public Form_UserRole()
        {
            InitializeComponent();
        }

        private void Form_UserRole_Load(object sender, EventArgs e)
        {
            LoadUserList();
            LoadRoleList();
        }

        // 1. Hàm dùng chung để load dữ liệu lên bảng
        private void LoadUserList()
        {
            try
            {
                // Sử dụng DBA_USERS và lọc bỏ các user hệ thống mặc định cốt lõi
                // Không dùng oracle_maintained = 'N' vì khi dùng _ORACLE_SCRIPT=true, Oracle đánh dấu user mới là 'Y'
                string sql = "SELECT username AS \"Tên User\", created AS \"Ngày tạo\", account_status AS \"Trạng thái\" " +
                             "FROM DBA_USERS " +
                             "WHERE username NOT IN ('SYS', 'SYSTEM', 'DBSNMP', 'OUTLN', 'WMSYS', 'CTXSYS', 'XDB', 'ORDDATA', 'MDSYS', 'ORDPLUGINS', 'SI_INFORMTN_SCHEMA', 'ORDSYS', 'OLAPSYS', 'MDDATA', 'SPATIAL_WFS_ADMIN_USR', 'SPATIAL_CSW_ADMIN_USR', 'FLOWS_FILES', 'APEX_PUBLIC_USER', 'ANONYMOUS', 'DVSYS', 'DVF', 'AUDSYS', 'LBACSYS', 'GSMADMIN_INTERNAL', 'GSMUSER', 'GSMROOTUSER', 'DIP', 'REMOTE_SCHEDULER_AGENT', 'SYSBACKUP', 'SYSDG', 'SYSKM', 'SYSRAC') " +
                             "ORDER BY created DESC";
                
                // Gọi hàm lấy DataTable từ AdminDBHelper
                DataTable dt = AdminDBHelper.GetDataTable(sql); 
                
                // Đổ dữ liệu vào bảng
                dgvUserList.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách User: " + ex.Message);
            }
        }

        private void LoadRoleList()
        {
            try
            {
                // Tương tự User, không dùng oracle_maintained = 'N' để tránh bị ẩn khi dùng _ORACLE_SCRIPT
                string sql = "SELECT role AS \"Tên Role\", common AS \"Chung?\", oracle_maintained as \"Hệ thống?\" " +
                             "FROM DBA_ROLES " +
                             "WHERE role NOT IN ('CONNECT', 'RESOURCE', 'DBA', 'SELECT_CATALOG_ROLE', 'EXECUTE_CATALOG_ROLE', 'DELETE_CATALOG_ROLE', 'EXP_FULL_DATABASE', 'IMP_FULL_DATABASE', 'DATAPUMP_EXP_FULL_DATABASE', 'DATAPUMP_IMP_FULL_DATABASE', 'GATHER_SYSTEM_STATISTICS', 'LOGSTDBY_ADMINISTRATOR', 'AQ_ADMINISTRATOR_ROLE', 'AQ_USER_ROLE', 'SCHEDULER_ADMIN', 'HS_ADMIN_SELECT_ROLE', 'HS_ADMIN_ROLE', 'GLOBAL_AQ_USER_ROLE', 'OEM_ADVISOR', 'OEM_MONITOR', 'WM_ADMIN_ROLE', 'JAVAUSERPRIV', 'JAVAIDPRIV', 'JAVASYSPRIV', 'JAVADEBUGPRIV', 'EJBCLIENT', 'JMXSERVER', 'JAVA_ADMIN', 'JAVA_DEPLOY', 'XDBADMIN', 'XDB_SET_INVOKER', 'AUTHENTICATEDUSER', 'XDB_WEBSERVICES', 'XDB_WEBSERVICES_WITH_PUBLIC', 'XDB_WEBSERVICES_OVER_HTTP') " +
                             "ORDER BY role";
                dgvRoleList.DataSource = AdminDBHelper.GetDataTable(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách Role: " + ex.Message);
            }
        }

        private bool IsValidName(string name)
        {
            // Tên Oracle: Bắt đầu bằng chữ, không có khoảng trắng, không ký tự đặc biệt (trừ _ # $)
            // Cho phép C## ở đầu
            return Regex.IsMatch(name, @"^([a-zA-Z]|C##)[a-zA-Z0-9_$#]*$");
        }

        private static string NormalizeName(string name)
        {
            return name.Trim().ToUpperInvariant();
        }

        private static string QuoteIdentifier(string identifier)
        {
            return "\"" + identifier.Replace("\"", "\"\"") + "\"";
        }

        private static string QuotePassword(string password)
        {
            return "\"" + password.Replace("\"", "\"\"") + "\"";
        }

        // 2. Trong sự kiện Click của nút Tạo User
        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            string user = NormalizeName(txtUsername.Text);
            string pass = txtPassword.Text.Trim();

            // Kiểm tra tính hợp lệ (Validation)
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng không để trống tên hoặc mật khẩu!");
                return;
            }

            if (!IsValidName(user))
            {
                MessageBox.Show("Tên User không hợp lệ! (Phải bắt đầu bằng chữ, không chứa khoảng trắng)");
                return;
            }

            // Thực thi lệnh thông qua DBHelper
            string sql = $"CREATE USER {QuoteIdentifier(user)} IDENTIFIED BY {QuotePassword(pass)}";
            
            try 
            {
                AdminDBHelper.ExecuteNonQuery(sql);
                MessageBox.Show("Tạo User thành công!");
                
                // QUAN TRỌNG: Gọi hàm này để cập nhật lại bảng ngay lập tức
                LoadUserList(); 
                
                // Xóa trống TextBox sau khi tạo để tiện nhập user tiếp theo
                txtUsername.Clear();
                txtPassword.Clear();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            string user = NormalizeName(txtUsername.Text);
            string pass = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng chọn User và nhập Password mới!");
                return;
            }

            string sql = $"ALTER USER {QuoteIdentifier(user)} IDENTIFIED BY {QuotePassword(pass)}";
            try
            {
                AdminDBHelper.ExecuteNonQuery(sql);
                MessageBox.Show($"Đã đổi mật khẩu cho User: {user}");
                txtPassword.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            string user = NormalizeName(txtUsername.Text);
            if (string.IsNullOrEmpty(user)) return;

            var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa User '{user}' không?\n(Thêm CASCADE để xóa sạch các object liên quan)", 
                                        "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                string sql = $"DROP USER {QuoteIdentifier(user)} CASCADE";
                try
                {
                    AdminDBHelper.ExecuteNonQuery(sql);
                    MessageBox.Show("Xóa User thành công!");
                    LoadUserList();
                    txtUsername.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnCreateRole_Click(object sender, EventArgs e)
        {
            string role = NormalizeName(txtRoleName.Text);
            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Vui lòng nhập tên Role!");
                return;
            }

            if (!IsValidName(role))
            {
                MessageBox.Show("Tên Role không hợp lệ! (Phải bắt đầu bằng chữ, không chứa khoảng trắng)");
                return;
            }

            string sql = $"CREATE ROLE {QuoteIdentifier(role)}";
            try
            {
                AdminDBHelper.ExecuteNonQuery(sql);
                MessageBox.Show($"Tạo Role {role} thành công!");
                LoadRoleList();
                txtRoleName.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnDeleteRole_Click(object sender, EventArgs e)
        {
            string role = NormalizeName(txtRoleName.Text);
            if (string.IsNullOrEmpty(role)) return;

            var result = MessageBox.Show($"Xác nhận xóa Role '{role}'?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string sql = $"DROP ROLE {QuoteIdentifier(role)}";
                try
                {
                    AdminDBHelper.ExecuteNonQuery(sql);
                    MessageBox.Show("Xóa Role thành công!");
                    LoadRoleList();
                    txtRoleName.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void dgvUserList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtUsername.Text = dgvUserList.Rows[e.RowIndex].Cells[0].Value?.ToString() ?? "";
            }
        }

        private void dgvRoleList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtRoleName.Text = dgvRoleList.Rows[e.RowIndex].Cells[0].Value?.ToString() ?? "";
            }
        }
    }
}
