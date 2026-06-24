using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using HospitalSystem.Core;

namespace HospitalSystem.Forms.Admin
{
    public partial class Form_Privileges : Form
    {
        // Quyền hợp lệ theo từng loại đối tượng (Tab 1 - cấp quyền toàn đối tượng)
        private readonly Dictionary<string, List<string>> objectPrivileges = new Dictionary<string, List<string>>
        {
            { "TABLE",     new List<string> { "SELECT", "INSERT", "UPDATE", "DELETE", "REFERENCES", "ALTER", "INDEX" } },
            { "VIEW",      new List<string> { "SELECT", "INSERT", "UPDATE", "DELETE" } },
            { "PROCEDURE", new List<string> { "EXECUTE" } },
            { "FUNCTION",  new List<string> { "EXECUTE" } },
        };

        // Blacklist role hệ thống Oracle - dùng chung cho GetGranteeData và LoadAllRoles
        private readonly string roleBlacklist =
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

        // Blacklist user hệ thống Oracle - dùng chung cho GetGranteeData và LoadAllUsers
        private readonly string userBlacklist =
            "'SYS', 'SYSTEM', 'DBSNMP', 'OUTLN', 'WMSYS', 'CTXSYS', 'XDB', 'ORDDATA', " +
            "'MDSYS', 'ORDPLUGINS', 'SI_INFORMTN_SCHEMA', 'ORDSYS', 'OLAPSYS', 'MDDATA', " +
            "'SPATIAL_WFS_ADMIN_USR', 'SPATIAL_CSW_ADMIN_USR', 'FLOWS_FILES', " +
            "'APEX_PUBLIC_USER', 'ANONYMOUS', 'DVSYS', 'DVF', 'AUDSYS', 'LBACSYS', " +
            "'GSMADMIN_INTERNAL', 'GSMUSER', 'GSMROOTUSER', 'DIP', 'REMOTE_SCHEDULER_AGENT', " +
            "'SYSBACKUP', 'SYSDG', 'SYSKM', 'SYSRAC', 'APPQOSSYS', 'DBSFWUSER', " +
            "'OJVMSYS', 'XS$NULL'";

        public Form_Privileges()
        {
            InitializeComponent();
        }

        private void Form_Privileges_Load(object sender, EventArgs e)
        {
            // Tab 1: Cấp quyền trên đối tượng
            LoadGrantees();
            LoadObjectTypes();
            UpdatePrivilegeList();
            LoadObjects();

            // Tab 2: Phân quyền mức cột
            LoadGrantees2();
            LoadTableViewTypes();

            // Tab 3: Cấp Role cho User
            LoadAllRoles();
            LoadAllUsers();
        }

        // 
        //  DÙNG CHUNG
        // 
        private DataTable GetGranteeData()
        {
            string sql = "SELECT username AS NAME, 'USER' AS TYPE FROM DBA_USERS " +
                         "WHERE username NOT IN (" + userBlacklist + ") " +
                         "UNION ALL " +
                         "SELECT role AS NAME, 'ROLE' AS TYPE FROM DBA_ROLES " +
                         "WHERE role NOT IN (" + roleBlacklist + ") " +
                         "ORDER BY TYPE, NAME";
            return AdminDBHelper.GetDataTable(sql);
        }

        // Hàm trả về blacklist owner cho query DBA_OBJECTS
        // Giữ SYSTEM trong danh sách được phép vì DBA thường tạo bảng ở schema SYSTEM
        private string GetOwnerBlacklist()
        {
            return "('SYS', 'WMSYS', 'CTXSYS', 'XDB', 'MDSYS', 'ORDDATA', 'ORDPLUGINS', 'ORDSYS', " +
                   "'OLAPSYS', 'AUDSYS', 'LBACSYS', 'OUTLN', 'DBSNMP', 'APPQOSSYS', 'DBSFWUSER', " +
                   "'OJVMSYS', 'DVSYS', 'DVF', 'GSMADMIN_INTERNAL', 'GSMUSER', 'GSMROOTUSER', " +
                   "'DIP', 'SYSBACKUP', 'SYSDG', 'SYSKM', 'SYSRAC', 'ANONYMOUS', 'XS$NULL')";
        }

        private static string QuoteIdentifier(string identifier)
        {
            return "\"" + identifier.Replace("\"", "\"\"") + "\"";
        }

        private static string SqlString(string value)
        {
            return "'" + value.Replace("'", "''") + "'";
        }

        private static (string Owner, string Name) SplitOwnerObject(string objFullName)
        {
            int dotIndex = objFullName.IndexOf('.');
            if (dotIndex < 0)
                throw new ArgumentException("Đối tượng phải có dạng OWNER.OBJECT_NAME.");

            return (objFullName.Substring(0, dotIndex).Trim(), objFullName.Substring(dotIndex + 1).Trim());
        }

        private static string BuildQualifiedObjectName(string objFullName)
        {
            var (owner, name) = SplitOwnerObject(objFullName);
            return QuoteIdentifier(owner) + "." + QuoteIdentifier(name);
        }

        private static string BuildColumnList(IEnumerable<string> columns)
        {
            var quoted = new List<string>();
            foreach (string column in columns)
                quoted.Add(QuoteIdentifier(column));
            return string.Join(", ", quoted);
        }

        private static string BuildColumnSelectViewName(string objFullName, string grantee, IEnumerable<string> columns)
        {
            string seed = objFullName + "|" + grantee + "|" + string.Join(",", columns);
            byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(seed));
            return "VW_COL_" + Convert.ToHexString(hash).Substring(0, 20);
        }

        //
        //  TAB 1: CẤP QUYỀN TRÊN ĐỐI TƯỢNG (toàn bộ bảng/view/proc/func)
        // 

        private void LoadGrantees()
        {
            try
            {
                DataTable dt = GetGranteeData();
                cboGrantee.Items.Clear();
                foreach (DataRow row in dt.Rows)
                    cboGrantee.Items.Add(row["NAME"].ToString() + " (" + row["TYPE"].ToString() + ")");
                if (cboGrantee.Items.Count > 0)
                    cboGrantee.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách Grantee: " + ex.Message);
            }
        }

        private void LoadObjectTypes()
        {
            cboObjectType.Items.Clear();
            foreach (var key in objectPrivileges.Keys)
                cboObjectType.Items.Add(key);
            cboObjectType.SelectedIndex = 0;
        }

        private void cboObjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePrivilegeList();
            LoadObjects();
        }

        // 2. Cập nhật danh sách quyền theo loại đối tượng đang chọn
        private void UpdatePrivilegeList()
        {
            string objType = cboObjectType.SelectedItem?.ToString() ?? "TABLE";
            clbPrivileges.Items.Clear();
            if (objectPrivileges.ContainsKey(objType))
                foreach (var priv in objectPrivileges[objType])
                    clbPrivileges.Items.Add(priv);
        }

        private void LoadObjects()
        {
            try
            {
                string objType = cboObjectType.SelectedItem?.ToString() ?? "TABLE";
                // Lọc bỏ các schema hệ thống thuần túy, giữ lại SYSTEM vì DBA thường tạo bảng ở đây
                string sql = "SELECT owner || '.' || object_name AS OBJ_FULLNAME " +
                             "FROM DBA_OBJECTS " +
                             "WHERE object_type = '" + objType + "' " +
                             "AND owner NOT IN " + GetOwnerBlacklist() + " " +
                             "ORDER BY owner, object_name";

                DataTable dt = AdminDBHelper.GetDataTable(sql);
                lstObjects.Items.Clear();
                foreach (DataRow row in dt.Rows)
                    lstObjects.Items.Add((row["OBJ_FULLNAME"].ToString() ?? "").Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách đối tượng: " + ex.Message);
            }
        }

        private void lstObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tab 1 không cần load cột - chỉ cấp quyền toàn đối tượng
        }

        private void clbPrivileges_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Tab 1 cấp quyền toàn đối tượng
        }

        private void btnGrant_Click(object sender, EventArgs e)
        {
            // Kiểm tra đầu vào
            if (cboGrantee.SelectedItem == null)
            { MessageBox.Show("Vui lòng chọn Grantee!"); return; }
            if (lstObjects.SelectedItem == null)
            { MessageBox.Show("Vui lòng chọn đối tượng dữ liệu!"); return; }
            if (clbPrivileges.CheckedItems.Count == 0)
            { MessageBox.Show("Vui lòng chọn ít nhất một quyền!"); return; }

            // Lấy tên Grantee (bỏ phần " (USER)" hoặc " (ROLE)" phía sau)
            string granteeFull = cboGrantee.SelectedItem.ToString() ?? "";
            string grantee = granteeFull.Substring(0, granteeFull.LastIndexOf('(')).Trim();
            string objFullName = BuildQualifiedObjectName((lstObjects.SelectedItem.ToString() ?? "").Trim());
            string grantOption = chkGrantOption.Checked ? " WITH GRANT OPTION" : "";
            string quotedGrantee = QuoteIdentifier(grantee);

            var errors = new List<string>();
            foreach (var item in clbPrivileges.CheckedItems)
            {
                string priv = (item.ToString() ?? "").Trim();
                string sql = "GRANT " + priv + " ON " + objFullName + " TO " + quotedGrantee + grantOption;
                try { AdminDBHelper.ExecuteNonQuery(sql); }
                catch (Exception ex) { errors.Add(priv + ": " + ex.Message); }
            }

            if (errors.Count == 0)
                MessageBox.Show("Cấp quyền thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Một số quyền không cấp được:\n" + string.Join("\n", errors), "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //
        //  TAB 2: PHÂN QUYỀN MỨC CỘT
        //

        private void LoadGrantees2()
        {
            try
            {
                DataTable dt = GetGranteeData();
                cboGrantee2.Items.Clear();
                foreach (DataRow row in dt.Rows)
                    cboGrantee2.Items.Add(row["NAME"].ToString() + " (" + row["TYPE"].ToString() + ")");
                if (cboGrantee2.Items.Count > 0)
                    cboGrantee2.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải Grantee: " + ex.Message);
            }
        }

        private void LoadTableViewTypes()
        {
            // Tab 2 chỉ dùng TABLE và VIEW vì chỉ chúng mới có cột
            cboObjectType2.Items.Clear();
            cboObjectType2.Items.Add("TABLE");
            cboObjectType2.Items.Add("VIEW");
            cboObjectType2.SelectedIndex = 0;
        }

        private void cboObjectType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadObjects2();
            lstColumns.Items.Clear();
            clbPrivileges2.Items.Clear();
            string objType = cboObjectType2.SelectedItem?.ToString() ?? "TABLE";

            if (objType == "TABLE")
            {
                clbPrivileges2.Items.Add("SELECT");
                clbPrivileges2.Items.Add("UPDATE");
                clbPrivileges2.Items.Add("REFERENCES");
            }
            else // VIEW
            {
                clbPrivileges2.Items.Add("SELECT");
                clbPrivileges2.Items.Add("UPDATE");
            }
        }

        private void LoadObjects2()
        {
            try
            {
                string objType = cboObjectType2.SelectedItem?.ToString() ?? "TABLE";
                string sql = "SELECT owner || '.' || object_name AS OBJ_FULLNAME " +
                             "FROM DBA_OBJECTS " +
                             "WHERE object_type = '" + objType + "' " +
                             "AND owner NOT IN " + GetOwnerBlacklist() + " " +
                             "ORDER BY owner, object_name";

                DataTable dt = AdminDBHelper.GetDataTable(sql);
                lstObjects2.Items.Clear();
                foreach (DataRow row in dt.Rows)
                    lstObjects2.Items.Add((row["OBJ_FULLNAME"].ToString() ?? "").Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải đối tượng: " + ex.Message);
            }
        }

        private void lstObjects2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadColumnsForSelectedObject();
        }

        // 3. Load danh sách cột khi chọn TABLE hoặc VIEW
        private void LoadColumnsForSelectedObject()
        {
            lstColumns.Items.Clear();
            string selected = (lstObjects2.SelectedItem?.ToString() ?? "").Trim();
            if (string.IsNullOrEmpty(selected)) return;

            int dotIndex = selected.IndexOf('.');
            if (dotIndex < 0) return;
            string owner = selected.Substring(0, dotIndex).Trim();
            string objName = selected.Substring(dotIndex + 1).Trim();

            try
            {
                string sql = "SELECT column_name FROM DBA_TAB_COLUMNS " +
                             "WHERE owner = " + SqlString(owner) + " AND table_name = " + SqlString(objName) + " " +
                             "ORDER BY column_id";

                DataTable dt = AdminDBHelper.GetDataTable(sql);
                foreach (DataRow row in dt.Rows)
                    lstColumns.Items.Add((row["COLUMN_NAME"].ToString() ?? "").Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách cột: " + ex.Message);
            }
        }

        private void btnGrantCol_Click(object sender, EventArgs e)
        {
            // Kiểm tra đầu vào
            if (cboGrantee2.SelectedItem == null)
            { MessageBox.Show("Vui lòng chọn Grantee!"); return; }
            if (lstObjects2.SelectedItem == null)
            { MessageBox.Show("Vui lòng chọn bảng / view!"); return; }
            if (clbPrivileges2.CheckedItems.Count == 0)
            { MessageBox.Show("Vui lòng chọn quyền!"); return; }

            // Lấy tên Grantee (bỏ phần " (USER)" hoặc " (ROLE)" phía sau)
            string granteeFull = cboGrantee2.SelectedItem.ToString() ?? "";
            string grantee = granteeFull.Substring(0, granteeFull.LastIndexOf('(')).Trim();
            string objFullName = (lstObjects2.SelectedItem.ToString() ?? "").Trim();
            string qualifiedObjName = BuildQualifiedObjectName(objFullName);
            string grantOption = chkGrantOption2.Checked ? " WITH GRANT OPTION" : "";
            string quotedGrantee = QuoteIdentifier(grantee);

            // Lấy danh sách cột được tick (checked)
            var selectedColumns = new List<string>();
            foreach (var col in lstColumns.CheckedItems)
                selectedColumns.Add((col.ToString() ?? "").Trim());

            var errors = new List<string>();
            foreach (var item in clbPrivileges2.CheckedItems)
            {
                string priv = (item.ToString() ?? "").Trim();
                try
                {
                    if (selectedColumns.Count == 0)
                    {
                        string sql = "GRANT " + priv + " ON " + qualifiedObjName + " TO " + quotedGrantee + grantOption;
                        AdminDBHelper.ExecuteNonQuery(sql);
                    }
                    else if (priv == "SELECT")
                    {
                        string columnList = BuildColumnList(selectedColumns);
                        string viewName = BuildColumnSelectViewName(objFullName, grantee, selectedColumns);
                        string createViewSql = "CREATE OR REPLACE VIEW " + QuoteIdentifier(viewName) +
                                               " AS SELECT " + columnList + " FROM " + qualifiedObjName;
                        string grantViewSql = "GRANT SELECT ON " + QuoteIdentifier(viewName) + " TO " + quotedGrantee + grantOption;

                        AdminDBHelper.ExecuteNonQuery(createViewSql);
                        AdminDBHelper.ExecuteNonQuery(grantViewSql);
                    }
                    else if (priv == "UPDATE" || priv == "REFERENCES")
                    {
                        string columnList = BuildColumnList(selectedColumns);
                        string sql = "GRANT " + priv + " (" + columnList + ") ON " + qualifiedObjName + " TO " + quotedGrantee + grantOption;
                        AdminDBHelper.ExecuteNonQuery(sql);
                    }
                    else
                    {
                        errors.Add(priv + ": Quyền này không hỗ trợ phân quyền mức cột. Bỏ chọn cột để cấp toàn đối tượng.");
                    }
                }
                catch (Exception ex)
                {
                    errors.Add(priv + ": " + ex.Message);
                }
            }

            if (errors.Count == 0)
                MessageBox.Show("Phân quyền mức cột thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Một số quyền không cấp được:\n" + string.Join("\n", errors), "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //
        //  TAB 3: CẤP ROLE CHO USER
        //

        private void LoadAllRoles()
        {
            try
            {
                // Sử dụng DBA_ROLES và lọc bỏ các role hệ thống mặc định cốt lõi
                string sql = "SELECT role FROM DBA_ROLES " +
                             "WHERE role NOT IN (" + roleBlacklist + ") " +
                             "ORDER BY role";

                DataTable dt = AdminDBHelper.GetDataTable(sql);
                clbRoles.Items.Clear();
                foreach (DataRow row in dt.Rows)
                    clbRoles.Items.Add((row["ROLE"].ToString() ?? "").Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách Role: " + ex.Message);
            }
        }

        private void LoadAllUsers()
        {
            try
            {
                // Sử dụng DBA_USERS và lọc bỏ các user hệ thống mặc định cốt lõi
                // Không dùng oracle_maintained = 'N' vì khi dùng _ORACLE_SCRIPT=true, Oracle đánh dấu user mới là 'Y'
                string sql = "SELECT username FROM DBA_USERS " +
                             "WHERE username NOT IN (" + userBlacklist + ") " +
                             "ORDER BY username";

                DataTable dt = AdminDBHelper.GetDataTable(sql);
                cboTargetUser.Items.Clear();
                foreach (DataRow row in dt.Rows)
                    cboTargetUser.Items.Add((row["USERNAME"].ToString() ?? "").Trim());

                if (cboTargetUser.Items.Count > 0)
                    cboTargetUser.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách User: " + ex.Message);
            }
        }

        private void cboTargetUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTargetUser.SelectedItem == null) return;
            string user = (cboTargetUser.SelectedItem.ToString() ?? "").Trim();
            try
            {
                // Gọi hàm này để cập nhật lại bảng ngay lập tức
                string sql = "SELECT granted_role AS \"Role đã cấp\", " +
                             "admin_option AS \"Admin Option\", " +
                             "default_role AS \"Default?\" " +
                             "FROM DBA_ROLE_PRIVS " +
                             "WHERE grantee = '" + user + "' " +
                             "ORDER BY granted_role";

                dgvCurrentRoles.DataSource = AdminDBHelper.GetDataTable(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnGrantRole_Click(object sender, EventArgs e)
        {
            if (cboTargetUser.SelectedItem == null)
            { MessageBox.Show("Vui lòng chọn User!"); return; }
            if (clbRoles.CheckedItems.Count == 0)
            { MessageBox.Show("Vui lòng chọn ít nhất một Role!"); return; }

            string targetUser = (cboTargetUser.SelectedItem.ToString() ?? "").Trim();
            string adminOption = chkAdminOption.Checked ? " WITH ADMIN OPTION" : "";
            string quotedTargetUser = QuoteIdentifier(targetUser);

            var errors = new List<string>();
            foreach (var item in clbRoles.CheckedItems)
            {
                string role = (item.ToString() ?? "").Trim();
                string sql = "GRANT " + QuoteIdentifier(role) + " TO " + quotedTargetUser + adminOption;
                try { AdminDBHelper.ExecuteNonQuery(sql); }
                catch (Exception ex) { errors.Add(role + ": " + ex.Message); }
            }

            if (errors.Count == 0)
            {
                MessageBox.Show("Cấp Role thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // QUAN TRỌNG: Làm mới bảng Role hiện tại ngay lập tức
                cboTargetUser_SelectedIndexChanged(sender, e);
            }
            else
                MessageBox.Show("Một số Role không cấp được:\n" + string.Join("\n", errors), "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dgvCurrentRoles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Placeholder - có thể mở rộng sau
        }
    }
}
