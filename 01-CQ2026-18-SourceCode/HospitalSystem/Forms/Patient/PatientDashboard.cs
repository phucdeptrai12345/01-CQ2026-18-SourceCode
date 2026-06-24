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

    private void LoadMyHsba()
    {
        try
        {
            var list = HsbaDAL.GetMyHsbaAsBenhnhan();
            dgvHsba.DataSource = list;
            _hsbaLoaded = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải hồ sơ bệnh án: {ex.Message}", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadMyPrescriptions()
    {
        try
        {
            var list = PrescriptionDAL.GetMyPrescriptions();
            dgvPrescriptions.DataSource = list;
            _prescLoaded = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải đơn thuốc: {ex.Message}", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
