using HospitalSystem.DAL;
using HospitalSystem.Models;

namespace HospitalSystem.Forms.Doctor;

/// <summary>Dashboard cho Bác sĩ</summary>
public partial class DoctorDashboard : UserControl
{
    public DoctorDashboard()
    {
        InitializeComponent();
        LoadData();
    }

    public void SelectTab(int index)
    {
        if (index >= 0 && index < tabControl.TabCount)
            tabControl.SelectedIndex = index;
    }

    private void LoadData()
    {
        LoadMyHsba();
        LoadMyServices();
    }

    private void LoadMyHsba()
    {
        try
        {
            var list = HsbaDAL.GetMyHsba();
            dgvHsba.DataSource = list;
            lblHsbaCount.Text = $"Hồ sơ của tôi: {list.Count}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải HSBA: {ex.Message}", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadMyServices()
    {
        try
        {
            if (dgvHsba.SelectedRows.Count > 0)
            {
                var hsba = (Hsba)dgvHsba.SelectedRows[0].DataBoundItem;
                var services = HsbaDvDAL.GetServicesByHsba(hsba.MaHSBA);
                dgvServices.DataSource = services;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải dịch vụ: {ex.Message}", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadPrescriptions()
    {
        try
        {
            if (dgvHsba.SelectedRows.Count > 0)
            {
                var hsba = (Hsba)dgvHsba.SelectedRows[0].DataBoundItem;
                var presc = PrescriptionDAL.GetByHsba(hsba.MaHSBA);
                dgvPrescriptions.DataSource = presc;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải đơn thuốc: {ex.Message}", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnViewDetail_Click(object sender, EventArgs e)
    {
        if (dgvHsba.SelectedRows.Count == 0)
        {
            MessageBox.Show("Vui lòng chọn một hồ sơ.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        var hsba = (Hsba)dgvHsba.SelectedRows[0].DataBoundItem;
        LoadMyServices();
        LoadPrescriptions();
        tabControl.SelectedIndex = 1;
    }

    private void btnUpdateDiagnosis_Click(object sender, EventArgs e)
    {
        if (dgvHsba.SelectedRows.Count == 0)
        {
            MessageBox.Show("Vui lòng chọn một hồ sơ để cập nhật.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        var hsba = (Hsba)dgvHsba.SelectedRows[0].DataBoundItem;
        using var dlg = new DiagnosisEditDialog(hsba);
        if (dlg.ShowDialog() == DialogResult.OK)
            LoadMyHsba();
    }

    private void btnAddPrescription_Click(object sender, EventArgs e)
    {
        if (dgvHsba.SelectedRows.Count == 0)
        {
            MessageBox.Show("Vui lòng chọn một hồ sơ bệnh án trước.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        var hsba = (Hsba)dgvHsba.SelectedRows[0].DataBoundItem;
        using var dlg = new PrescriptionEditDialog(hsba.MaHSBA);
        if (dlg.ShowDialog() == DialogResult.OK)
            LoadPrescriptions();
    }

    private void btnEditPrescription_Click(object sender, EventArgs e)
    {
        if (dgvPrescriptions.SelectedRows.Count == 0)
        {
            MessageBox.Show("Vui lòng chọn đơn thuốc cần sửa.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        var dt = (DonThuoc)dgvPrescriptions.SelectedRows[0].DataBoundItem;
        string newLieu = dt.LieuDung ?? "";
        using var dlg = new Form { Text = "Sửa liều dùng", Size = new System.Drawing.Size(350, 160), StartPosition = FormStartPosition.CenterParent };
        var txt = new TextBox { Left = 120, Top = 20, Width = 180, Text = newLieu };
        var ok = new Button { Text = "Lưu", Left = 120, Top = 60, DialogResult = DialogResult.OK };
        var cancel = new Button { Text = "Hủy", Left = 215, Top = 60, DialogResult = DialogResult.Cancel };
        dlg.Controls.AddRange(new Control[] { new Label { Text = "Liều dùng:", Left = 20, Top = 23, AutoSize = true }, txt, ok, cancel });
        dlg.AcceptButton = ok; dlg.CancelButton = cancel;
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            try
            {
                dt.LieuDung = txt.Text.Trim();
                PrescriptionDAL.Update(dt);
                MessageBox.Show("Cập nhật liều dùng thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPrescriptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void btnDeletePrescription_Click(object sender, EventArgs e)
    {
        if (dgvPrescriptions.SelectedRows.Count == 0)
        {
            MessageBox.Show("Vui lòng chọn đơn thuốc cần xóa.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        var dt = (DonThuoc)dgvPrescriptions.SelectedRows[0].DataBoundItem;
        if (MessageBox.Show($"Xóa thuốc '{dt.TenThuoc}'?", "Xác nhận xóa",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
        try
        {
            PrescriptionDAL.Delete(dt.MaHSBA, dt.NgayDT ?? DateTime.Now, dt.TenThuoc ?? "");
            MessageBox.Show("Đã xóa đơn thuốc.", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadPrescriptions();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnUpdatePatientMedical_Click(object sender, EventArgs e)
    {
        if (dgvHsba.SelectedRows.Count == 0)
        {
            MessageBox.Show("Vui lòng chọn một hồ sơ bệnh án trước.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        var hsba = (Hsba)dgvHsba.SelectedRows[0].DataBoundItem;
        var bn = PatientDAL.GetPatientById(hsba.MaBN);
        if (bn == null)
        {
            MessageBox.Show("Không tìm thấy thông tin bệnh nhân.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        using var dlg = new PatientMedicalEditDialog(bn);
        dlg.ShowDialog();
    }

    private void dgvHsba_SelectionChanged(object sender, EventArgs e)
    {
        LoadMyServices();
        LoadPrescriptions();
    }
}
