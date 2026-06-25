using System.Windows.Forms;
using HospitalSystem.Models;
using HospitalSystem.DAL;

namespace HospitalSystem.Forms.Doctor
{
    /// <summary>Dialog bác sĩ cập nhật tiền sử bệnh / dị ứng thuốc của bệnh nhân</summary>
    public class PatientMedicalEditDialog : Form
    {
        private BenhNhan _bn;
        private TextBox txtTienSuBenh, txtTienSuBenhGD, txtDiUngThuoc;
        private Button btnOK, btnCancel;

        public PatientMedicalEditDialog(BenhNhan bn)
        {
            _bn = bn;
            Text = $"Cập nhật tiền sử bệnh - {bn.MaBN} ({bn.TenBN})";
            Size = new System.Drawing.Size(500, 310);
            StartPosition = FormStartPosition.CenterParent;

            txtTienSuBenh    = new TextBox { Top = 25, Left = 160, Width = 290, Height = 55,  Multiline = true, Text = bn.TienSuBenh ?? "" };
            txtTienSuBenhGD  = new TextBox { Top = 95, Left = 160, Width = 290, Height = 55,  Multiline = true, Text = bn.TienSuBenhGD ?? "" };
            txtDiUngThuoc    = new TextBox { Top = 165, Left = 160, Width = 290, Height = 55, Multiline = true, Text = bn.DiUngThuoc ?? "" };

            btnOK     = new Button { Text = "Lưu",  Top = 235, Left = 160, Width = 80, DialogResult = DialogResult.OK };
            btnCancel = new Button { Text = "Hủy",  Top = 235, Left = 255, Width = 80, DialogResult = DialogResult.Cancel };

            btnOK.Click += (s, e) =>
            {
                try
                {
                    _bn.TienSuBenh   = txtTienSuBenh.Text.Trim();
                    _bn.TienSuBenhGD = txtTienSuBenhGD.Text.Trim();
                    _bn.DiUngThuoc   = txtDiUngThuoc.Text.Trim();
                    PatientDAL.UpdateMedicalInfo(_bn);
                    MessageBox.Show("Cập nhật tiền sử bệnh thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.None;
                }
            };

            Controls.Add(new Label { Text = "Tiền sử bệnh:",    Top = 30,  Left = 15, AutoSize = true });
            Controls.Add(txtTienSuBenh);
            Controls.Add(new Label { Text = "Tiền sử bệnh GĐ:", Top = 100, Left = 15, AutoSize = true });
            Controls.Add(txtTienSuBenhGD);
            Controls.Add(new Label { Text = "Dị ứng thuốc:",    Top = 170, Left = 15, AutoSize = true });
            Controls.Add(txtDiUngThuoc);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
        }
    }
}
