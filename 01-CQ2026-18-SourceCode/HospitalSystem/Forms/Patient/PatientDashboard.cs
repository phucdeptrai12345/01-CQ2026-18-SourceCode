using HospitalSystem.DAL;
using HospitalSystem.Models;

namespace HospitalSystem.Forms.Patient;

/// <summary>Dashboard cho Bệnh nhân - xem thông tin cá nhân, HSBA, đơn thuốc</summary>
public partial class PatientDashboard : UserControl
{
    private BenhNhan? _currentPatient;
    private bool _hsbaLoaded = false;
    private bool _prescLoaded = false;

    public PatientDashboard()
    {
        InitializeComponent();
        LoadMyInfo();
        // Attach designer-friendly DrawItem handler if not present
        // (The actual handler is implemented below to avoid lambda in Designer file)
        // Ensure tabControl draw handler exists
    }

    private void LoadMyInfo()
    {
        try
        {
            _currentPatient = PatientDAL.GetMyInfo();
            if (_currentPatient == null)
            {
                MessageBox.Show("Không tìm thấy thông tin bệnh nhân.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DisplayInfo(_currentPatient);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải thông tin: {ex.Message}", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void DisplayInfo(BenhNhan bn)
    {
        lblMaBNValue.Text    = bn.MaBN;
        lblTenBNValue.Text   = bn.TenBN;
        lblNgaySinhValue.Text = bn.NgaySinh?.ToString("dd/MM/yyyy") ?? "";
        lblCCCDValue.Text    = bn.CCCD;
        lblPhaiValue.Text    = bn.Phai;
        txtSoNha.Text        = bn.SoNha;
        txtTenDuong.Text     = bn.TenDuong;
        txtQuanHuyen.Text    = bn.QuanHuyen;
        txtTinhTP.Text       = bn.TinhTP;
        txtTienSuBenh.Text   = bn.TienSuBenh;
        txtTienSuBenhGD.Text = bn.TienSuBenhGD;
        txtDiUngThuoc.Text   = bn.DiUngThuoc;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (_currentPatient == null) return;
        try
        {
            _currentPatient.SoNha        = txtSoNha.Text.Trim();
            _currentPatient.TenDuong     = txtTenDuong.Text.Trim();
            _currentPatient.QuanHuyen    = txtQuanHuyen.Text.Trim();
            _currentPatient.TinhTP       = txtTinhTP.Text.Trim();
            _currentPatient.TienSuBenh   = txtTienSuBenh.Text.Trim();
            _currentPatient.TienSuBenhGD = txtTienSuBenhGD.Text.Trim();
            _currentPatient.DiUngThuoc   = txtDiUngThuoc.Text.Trim();
            PatientDAL.UpdateMyProfile(_currentPatient);
            MessageBox.Show("Cập nhật thông tin thành công!", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi lưu: {ex.Message}", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tabControl.SelectedTab == tabHsba && !_hsbaLoaded)
            LoadMyHsba();
        else if (tabControl.SelectedTab == tabPrescriptions && !_prescLoaded)
            LoadMyPrescriptions();
    }

    private void TabControl_DrawItem(object sender, DrawItemEventArgs e)
    {
        var g = e.Graphics;
        var tab = tabControl.TabPages[e.Index];
        var bounds = tabControl.GetTabRect(e.Index);
        bool selected = e.Index == tabControl.SelectedIndex;
        using var bgBrush = new System.Drawing.SolidBrush(selected
            ? Color.FromArgb(21, 101, 192)
            : Color.FromArgb(30, 30, 58));
        g.FillRectangle(bgBrush, bounds);
        TextRenderer.DrawText(g, tab.Text, e.Font ?? tabControl.Font, bounds,
            selected ? Color.White : Color.FromArgb(160, 160, 200),
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
    }

    private void LoadMyHsba()
    {
        try
        {
            var list = HsbaDAL.GetMyHsbaAsBenhnhan();
            dgvHsba.DataSource = list;
            _hsbaLoaded = true;
            SetHsbaHeaders();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải hồ sơ bệnh án: {ex.Message}", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void SetHsbaHeaders()
    {
        var map = new Dictionary<string, string>
        {
            ["MaHSBA"]  = "Mã HSBA",
            ["MaBN"]    = "Mã BN",
            ["Ngay"]    = "Ngày khám",
            ["ChanDoan"]= "Chẩn đoán",
            ["DieuTri"] = "Điều trị",
            ["MaBS"]    = "Bác sĩ",
            ["MaKhoa"]  = "Khoa",
            ["KetLuan"] = "Kết luận"
        };
        foreach (DataGridViewColumn col in dgvHsba.Columns)
            if (map.TryGetValue(col.Name, out var header)) col.HeaderText = header;
        if (dgvHsba.Columns["MaBN"] != null) dgvHsba.Columns["MaBN"].Visible = false;
    }

    private void LoadMyPrescriptions()
    {
        try
        {
            var list = PrescriptionDAL.GetMyPrescriptions();
            dgvPrescriptions.DataSource = list;
            _prescLoaded = true;
            SetPrescriptionHeaders();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải đơn thuốc: {ex.Message}", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void SetPrescriptionHeaders()
    {
        var map = new Dictionary<string, string>
        {
            ["MaHSBA"]  = "Mã HSBA",
            ["NgayDT"]  = "Ngày kê",
            ["TenThuoc"]= "Tên thuốc",
            ["LieuDung"]= "Liều dùng"
        };
        foreach (DataGridViewColumn col in dgvPrescriptions.Columns)
            if (map.TryGetValue(col.Name, out var header)) col.HeaderText = header;
    }
}
