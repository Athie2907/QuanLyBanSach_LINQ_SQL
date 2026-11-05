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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;

            if (tenDangNhap == "" || matKhau == "")
            {
                lblThongBao.Text = "Vui lòng nhập đầy đủ thông tin!";
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
                        lblThongBao.Text = "Tên đăng nhập không tồn tại!";
                        return;
                    }

                    if (!taiKhoan.trang_thai)
                    {
                        lblThongBao.Text = "Tài khoản đã bị khóa!";
                        return;
                    }

                    bool hopLe = PasswordHelper.KiemTraMatKhau(
                        matKhau,
                        taiKhoan.salt,
                        taiKhoan.mat_khau_hash
                    );

                    if (!hopLe)
                    {
                        lblThongBao.Text = "Mật khẩu không chính xác!";
                        return;
                    }
                }

                MessageBox.Show(
                    "Đăng nhập thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                this.Hide();

                using (FormGiaoDien frm = new FormGiaoDien())
                {
                    frm.TenDangNhapHienTai = tenDangNhap;
                    frm.ShowDialog();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng nhập: " + ex.Message);
            }
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
