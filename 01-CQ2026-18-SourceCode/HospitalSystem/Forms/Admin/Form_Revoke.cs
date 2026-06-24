using HospitalSystem.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HospitalSystem.Forms.Admin
{
    public partial class Form_Revoke : Form
    {
        public Form_Revoke()
        {
            InitializeComponent();
        }

        private void Form_Revoke1_Load(object sender, EventArgs e)
        {
            // Khi form mở, clear DataGridView để không hiển thị dữ liệu cũ
            dataGridView1.DataSource = null;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        private void btnViewPrivileges_Click(object sender, EventArgs e)
        {
            string user = NormalizeTargetName();
            if (string.IsNullOrEmpty(user))
            {
                return;
            }

            try
            {
                DataTable dt = LoadPrivilegeData(user);
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy quyền nào của user/role này.");
                    dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải quyền: " + ex.Message);
            }
        }

        private void btnRevoke_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select at least one privilege to revoke.");
                return;
            }

            string user = NormalizeTargetName();
            if (string.IsNullOrEmpty(user))
                return;

            try
            {
                var errors = new List<string>();
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (row.IsNewRow) continue;

                    string revokeSql = BuildRevokeSql(row, user);
                    try
                    {
                        AdminDBHelper.ExecuteNonQuery(revokeSql);
                    }
                    catch (Exception ex)
                    {
                        errors.Add(revokeSql + "\n  " + ex.Message);
                    }
                }

                if (errors.Count == 0)
                    MessageBox.Show("Đã thu hồi các quyền đã chọn.");
                else
                    MessageBox.Show("Một số quyền chưa thu hồi được:\n" + string.Join("\n\n", errors));

                dataGridView1.DataSource = LoadPrivilegeData(user);
                dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thu hồi quyền: " + ex.Message);
            }
        }

        private string NormalizeTargetName()
        {
            string target = textBox1.Text.Trim().ToUpperInvariant();
            if (string.IsNullOrEmpty(target))
            {
                MessageBox.Show("Vui lòng nhập tên user/role.");
                return string.Empty;
            }

            if (!Regex.IsMatch(target, @"^[A-Z][A-Z0-9_$#]*$"))
            {
                MessageBox.Show("Tên user/role không hợp lệ. Chỉ hỗ trợ Oracle identifier thông thường.");
                return string.Empty;
            }

            textBox1.Text = target;
            return target;
        }

        private static DataTable LoadPrivilegeData(string grantee)
        {
            string sql = $@"
                SELECT 'SYSTEM' AS PRIVILEGE_TYPE,
                       privilege AS PRIVILEGE,
                       CAST(NULL AS VARCHAR2(128)) AS OWNER,
                       CAST(NULL AS VARCHAR2(128)) AS OBJECT_NAME,
                       CAST(NULL AS VARCHAR2(128)) AS GRANTABLE
                FROM dba_sys_privs
                WHERE grantee = '{grantee}'
                UNION ALL
                SELECT 'ROLE' AS PRIVILEGE_TYPE,
                       granted_role AS PRIVILEGE,
                       CAST(NULL AS VARCHAR2(128)) AS OWNER,
                       CAST(NULL AS VARCHAR2(128)) AS OBJECT_NAME,
                       admin_option AS GRANTABLE
                FROM dba_role_privs
                WHERE grantee = '{grantee}'
                UNION ALL
                SELECT 'OBJECT' AS PRIVILEGE_TYPE,
                       privilege AS PRIVILEGE,
                       owner AS OWNER,
                       table_name AS OBJECT_NAME,
                       grantable AS GRANTABLE
                FROM dba_tab_privs
                WHERE grantee = '{grantee}'
                ORDER BY PRIVILEGE_TYPE, OWNER, OBJECT_NAME, PRIVILEGE";

            return AdminDBHelper.GetDataTable(sql);
        }

        private static string BuildRevokeSql(DataGridViewRow row, string grantee)
        {
            string type = Convert.ToString(row.Cells["PRIVILEGE_TYPE"].Value) ?? "";
            string privilege = Convert.ToString(row.Cells["PRIVILEGE"].Value) ?? "";
            string owner = Convert.ToString(row.Cells["OWNER"].Value) ?? "";
            string objectName = Convert.ToString(row.Cells["OBJECT_NAME"].Value) ?? "";
            string quotedGrantee = QuoteIdentifier(grantee);

            return type switch
            {
                "ROLE" => "REVOKE " + QuoteIdentifier(privilege) + " FROM " + quotedGrantee,
                "OBJECT" => "REVOKE " + privilege + " ON " + QuoteIdentifier(owner) + "." + QuoteIdentifier(objectName) + " FROM " + quotedGrantee,
                _ => "REVOKE " + privilege + " FROM " + quotedGrantee
            };
        }

        private static string QuoteIdentifier(string identifier)
        {
            return "\"" + identifier.Replace("\"", "\"\"") + "\"";
        }
    }
}
