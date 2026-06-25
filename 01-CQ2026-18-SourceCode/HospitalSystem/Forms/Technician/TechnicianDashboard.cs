using HospitalSystem.DAL;
using HospitalSystem.Models;

namespace HospitalSystem.Forms.Technician;

/// <summary>Dashboard cho Kỹ thuật viên</summary>
public partial class TechnicianDashboard : UserControl
{
    public TechnicianDashboard()
    {
        InitializeComponent();
        LoadMyServices();
    }

    private void LoadMyServices()
    {
        try
        {
            var list = HsbaDvDAL.GetMyServices();
            dgvServices.DataSource = list;
            SetHsbaDvHeaders(dgvServices);
            lblCount.Text = $"Dịch vụ được giao: {list.Count}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static void SetHsbaDvHeaders(DataGridView dgv)
    {
        var map = new Dictionary<string, string>
        {
            ["MaHSBA"] = "Mã HSBA",
            ["LoaiDV"]  = "Loại dịch vụ",
            ["NgayDV"]  = "Ngày DV",
            ["MaKTV"]   = "Mã KTV",
            ["KetQua"]  = "Kết quả"
        };
        foreach (DataGridViewColumn col in dgv.Columns)
            if (map.TryGetValue(col.Name, out var h)) col.HeaderText = h;
    }

    private void btnUpdateResult_Click(object sender, EventArgs e)
    {
        if (dgvServices.SelectedRows.Count == 0)
        {
            MessageBox.Show("Vui lòng chọn một dịch vụ để cập nhật kết quả.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        var dv = (HsbaDv)dgvServices.SelectedRows[0].DataBoundItem;
        using var dlg = new UpdateResultDialog(dv);
        if (dlg.ShowDialog() == DialogResult.OK)
            LoadMyServices();
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        LoadMyServices();
    }
}
