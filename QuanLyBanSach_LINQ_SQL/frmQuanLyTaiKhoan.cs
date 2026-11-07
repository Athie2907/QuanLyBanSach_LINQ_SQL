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
    public partial class frmQuanLyTaiKhoan : Form
    {
        private int maNguoiDungDangChon = 0;

        // Hàm đọc trực tiếp chuỗi kết nối từ App.config
        private QuanLyBanSachDataContext LayDataContext()
        {
            string connStr = Properties.Settings.Default.db_QuanLyBanSachConnectionString;
            return new QuanLyBanSachDataContext(connStr);
        }

        public frmQuanLyTaiKhoan()
        {
            InitializeComponent();
            dgTaiKhoan.AutoGenerateColumns = false;

            dgTaiKhoan.Columns["colMaNguoiDung"].DataPropertyName = "MaNguoiDung";
            dgTaiKhoan.Columns["colTenDangNhap"].DataPropertyName = "TenDangNhap";
            dgTaiKhoan.Columns["colHoTen"].DataPropertyName = "HoTen";
            dgTaiKhoan.Columns["colTenPhanQuyen"].DataPropertyName = "TenPhanQuyen";
            dgTaiKhoan.Columns["colTrangThai"].DataPropertyName = "TrangThai";

            LoadTaiKhoan();
            LoadPhanQuyen();
            LamMoi();
        }

        private void LoadTaiKhoan()
        {
            try
            {
                using (QuanLyBanSachDataContext db = LayDataContext())
                {
                    var danhSach = (from nd in db.tbl_nguoi_dungs
                                    join pq in db.tbl_phan_quyens
                                    on nd.ma_phan_quyen equals pq.ma_phan_quyen
                                    select new
                                    {
                                        MaNguoiDung = nd.ma_nguoi_dung,
                                        TenDangNhap = nd.ten_dang_nhap,
                                        HoTen = nd.ho_ten,
                                        TenPhanQuyen = pq.ten_phan_quyen,
                                        TrangThai = nd.trang_thai
                                            ? "Đang hoạt động"
                                            : "Đã khóa"
                                    }).ToList();

                    dgTaiKhoan.DataSource = null;
                    dgTaiKhoan.DataSource = danhSach;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải tài khoản: " + ex.Message);
            }
        }

        private void LoadPhanQuyen()
        {
            try
            {
                using (QuanLyBanSachDataContext db = LayDataContext())
                {
                    var danhSach = db.tbl_phan_quyens
                        .Select(x => new
                        {
                            x.ma_phan_quyen,
                            x.ten_phan_quyen
                        })
                        .ToList();

                    cbPhanQuyen.DataSource = danhSach;
                    cbPhanQuyen.DisplayMember = "ten_phan_quyen";
                    cbPhanQuyen.ValueMember = "ma_phan_quyen";
                    cbPhanQuyen.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải nhóm quyền: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;
            string hoTen = txtHoTen.Text.Trim();

            if (tenDangNhap == "" || matKhau == "" || hoTen == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (matKhau.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!");
                return;
            }

            if (cbPhanQuyen.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhóm quyền!");
                return;
            }

            try
            {
                using (QuanLyBanSachDataContext db = LayDataContext())
                {
                    // Kiểm tra tên đăng nhập đã tồn tại
                    bool daTonTai = db.tbl_nguoi_dungs
                        .Any(x => x.ten_dang_nhap == tenDangNhap);

                    if (daTonTai)
                    {
                        MessageBox.Show("Tên đăng nhập đã tồn tại!");
                        return;
                    }

                    // Tạo Salt và mã hóa mật khẩu
                    string salt = PasswordHelper.TaoSalt();

                    string matKhauHash =
                        PasswordHelper.MaHoaMatKhau(matKhau, salt);

                    // Tạo tài khoản mới
                    tbl_nguoi_dung taiKhoan = new tbl_nguoi_dung
                    {
                        ten_dang_nhap = tenDangNhap,
                        mat_khau_hash = matKhauHash,
                        salt = salt,
                        ho_ten = hoTen,
                        ma_phan_quyen = Convert.ToInt32(
                            cbPhanQuyen.SelectedValue),
                        trang_thai = chkTrangThai.Checked,
                        ngay_tao = DateTime.Now
                    };

                    db.tbl_nguoi_dungs.InsertOnSubmit(taiKhoan);
                    db.SubmitChanges();
                }

                MessageBox.Show("Thêm tài khoản thành công!");

                LoadTaiKhoan();
                LamMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm tài khoản: " + ex.Message);
            }
        }

        private void LamMoi()
        {
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            txtHoTen.Clear();

            cbPhanQuyen.SelectedIndex = -1;
            chkTrangThai.Checked = true;

            txtTenDangNhap.Enabled = true;
            txtMatKhau.Enabled = true;

            dgTaiKhoan.ClearSelection();
        }

        private void dgTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgTaiKhoan.Rows[e.RowIndex];

            maNguoiDungDangChon = Convert.ToInt32(
                row.Cells["colMaNguoiDung"].Value
            );

            txtTenDangNhap.Text =
                row.Cells["colTenDangNhap"].Value?.ToString() ?? "";

            txtHoTen.Text =
                row.Cells["colHoTen"].Value?.ToString() ?? "";

            cbPhanQuyen.Text =
                row.Cells["colTenPhanQuyen"].Value?.ToString() ?? "";

            string trangThai =
                row.Cells["colTrangThai"].Value?.ToString() ?? "";

            chkTrangThai.Checked = trangThai == "Đang hoạt động";

            txtMatKhau.Clear();

            // Không cho sửa tên đăng nhập và mật khẩu tại đây
            txtTenDangNhap.Enabled = false;
            txtMatKhau.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (maNguoiDungDangChon == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần sửa!");
                return;
            }

            string hoTen = txtHoTen.Text.Trim();

            if (hoTen == "")
            {
                MessageBox.Show("Vui lòng nhập họ tên!");
                return;
            }

            if (cbPhanQuyen.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhóm quyền!");
                return;
            }

            try
            {
                using (QuanLyBanSachDataContext db = LayDataContext())
                {
                    var taiKhoan = db.tbl_nguoi_dungs
                        .FirstOrDefault(x =>
                            x.ma_nguoi_dung == maNguoiDungDangChon);

                    if (taiKhoan == null)
                    {
                        MessageBox.Show("Không tìm thấy tài khoản!");
                        return;
                    }

                    taiKhoan.ho_ten = hoTen;

                    taiKhoan.ma_phan_quyen =
                        Convert.ToInt32(cbPhanQuyen.SelectedValue);

                    taiKhoan.trang_thai = chkTrangThai.Checked;

                    db.SubmitChanges();
                }

                MessageBox.Show("Cập nhật tài khoản thành công!");

                LoadTaiKhoan();
                LamMoi();
                maNguoiDungDangChon = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa tài khoản: " + ex.Message);
            }
        }

        private void btnKhoa_Click(object sender, EventArgs e)
        {
            if (maNguoiDungDangChon == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần khóa hoặc mở khóa!");
                return;
            }

            try
            {
                using (QuanLyBanSachDataContext db = LayDataContext())
                {
                    var taiKhoan = db.tbl_nguoi_dungs
                        .FirstOrDefault(x =>
                            x.ma_nguoi_dung == maNguoiDungDangChon);

                    if (taiKhoan == null)
                    {
                        MessageBox.Show("Không tìm thấy tài khoản!");
                        return;
                    }

                    // Không cho phép tự khóa tài khoản admin đang đăng nhập
                    if (taiKhoan.ten_dang_nhap == "admin")
                    {
                        MessageBox.Show("Không thể khóa tài khoản quản trị viên!");
                        return;
                    }

                    taiKhoan.trang_thai = !taiKhoan.trang_thai;

                    db.SubmitChanges();

                    string thongBao = taiKhoan.trang_thai
                        ? "Mở khóa tài khoản thành công!"
                        : "Khóa tài khoản thành công!";

                    MessageBox.Show(thongBao);
                }

                LoadTaiKhoan();
                LamMoi();
                maNguoiDungDangChon = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khóa/mở khóa tài khoản: " + ex.Message);
            }
        }
    }
}