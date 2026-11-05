using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanSach_LINQ_SQL
{
    public partial class frmDoiMatKhau : Form
    {
        private string tenDangNhap;
        public frmDoiMatKhau(string tenDangNhap)
        {
            InitializeComponent();
            this.tenDangNhap = tenDangNhap;
        }

        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        private void lblmatKhauCu_Click(object sender, EventArgs e)
        {

        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            bool hien = chkHienMatKhau.Checked;

            txtMatKhauCu.UseSystemPasswordChar = !hien;
            txtMatKhauMoi.UseSystemPasswordChar = !hien;
            txtXacNhanMatKhau.UseSystemPasswordChar = !hien;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this .Close();
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string matKhauCu = txtMatKhauCu.Text;
            string matKhauMoi = txtMatKhauMoi.Text;
            string xacNhan = txtXacNhanMatKhau.Text;

            if (matKhauCu == "" || matKhauMoi == "" || xacNhan == "")
            {
                lblThongBao.Text = "Vui lòng nhập đầy đủ thông tin!";
                return;
            }

            if (matKhauMoi.Length < 6)
            {
                lblThongBao.Text = "Mật khẩu mới phải có ít nhất 6 ký tự!";
                return;
            }

            if (matKhauMoi != xacNhan)
            {
                lblThongBao.Text = "Mật khẩu xác nhận không khớp!";
                return;
            }

            if (matKhauCu == matKhauMoi)
            {
                lblThongBao.Text = "Mật khẩu mới phải khác mật khẩu cũ!";
                return;
            }

            try
            {
                using (QuanLyBanSachDataContext db =
                    new QuanLyBanSachDataContext())
                {
                    var taiKhoan = db.tbl_nguoi_dungs
                        .FirstOrDefault(x => x.ten_dang_nhap == tenDangNhap);

                    if (taiKhoan == null)
                    {
                        lblThongBao.Text = "Không tìm thấy tài khoản!";
                        return;
                    }

                    bool hopLe = PasswordHelper.KiemTraMatKhau(
                        matKhauCu,
                        taiKhoan.salt,
                        taiKhoan.mat_khau_hash
                    );

                    if (!hopLe)
                    {
                        lblThongBao.Text = "Mật khẩu hiện tại không chính xác!";
                        return;
                    }

                    string saltMoi = PasswordHelper.TaoSalt();

                    taiKhoan.salt = saltMoi;
                    taiKhoan.mat_khau_hash =
                        PasswordHelper.MaHoaMatKhau(matKhauMoi, saltMoi);

                    db.SubmitChanges();
                }

                MessageBox.Show(
                    "Đổi mật khẩu thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đổi mật khẩu: " + ex.Message);
            }
    }
    }
}
