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
    public partial class frmLoaiSach : Form
    {
        private QuanLyBanSachDataContext LayDataContext()
        {
            string connStr = Properties.Settings.Default.db_QuanLyBanSachConnectionString;
            return new QuanLyBanSachDataContext(connStr);
        }

        public frmLoaiSach()
        {
            InitializeComponent();

            // Cột trong Designer (colMaLoai, colTenLoai) chưa có DataPropertyName nên lưới trống -> gắn lại bằng code
            LuoiHelper.DatCot(dvgLoaiSach,
                new CotLuoi("colMaLoai", "Mã loại", "ma_loai_sach"),
                new CotLuoi("colTenLoai", "Tên loại sách", "ten_loai_sach"));

            // Bấm vào dòng bất kỳ trên lưới đều chọn được
            dvgLoaiSach.CellClick += dvgLoaiSach_CellClick;

            // ĐÃ MỞ KHÓA: Cho phép người dùng click vào ô mã loại để tự nhập mã mới nếu muốn
            textMaLoai.ReadOnly = false;
            textMaLoai.Enabled = true;

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (var db = LayDataContext())
                {
                    // Lấy dữ liệu từ bảng loại sách
                    var danhSach = db.GetTable<tbl_loai_sach>()
                        .Select(x => new
                        {
                            ma_loai_sach = x.ma_loai_sach,
                            ten_loai_sach = x.ten_loai_sach
                        })
                        .ToList();

                    dvgLoaiSach.DataSource = null;
                    dvgLoaiSach.DataSource = danhSach;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách loại sách: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            textMaLoai.Clear();
            txtTenLoai.Clear();
            txtTenLoai.Focus();
            dvgLoaiSach.ClearSelection();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maLoaiStr = textMaLoai.Text.Trim();
            string tenLoai = txtTenLoai.Text.Trim();

            if (string.IsNullOrEmpty(tenLoai))
            {
                MessageBox.Show("Vui lòng nhập tên loại sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLoai.Focus();
                return;
            }

            try
            {
                using (var db = LayDataContext())
                {
                    var table = db.GetTable<tbl_loai_sach>();

                    tbl_loai_sach loaiMoi = new tbl_loai_sach();
                    loaiMoi.ten_loai_sach = tenLoai;

                    // NẾU BẠN TỰ NHẬP MÃ: Kiểm tra và gán mã do người dùng tự gõ vào ô textMaLoai
                    if (!string.IsNullOrEmpty(maLoaiStr))
                    {
                        if (!int.TryParse(maLoaiStr, out int maLoaiInt))
                        {
                            MessageBox.Show("Mã loại sách phải là dạng số nguyên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textMaLoai.Focus();
                            return;
                        }

                        bool trungMa = table.Any(x => x.ma_loai_sach == maLoaiInt);
                        if (trungMa)
                        {
                            MessageBox.Show("Mã loại sách này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textMaLoai.Focus();
                            return;
                        }

                        loaiMoi.ma_loai_sach = maLoaiInt;
                    }

                    bool daTonTaiTen = table.Any(x => x.ten_loai_sach.ToLower() == tenLoai.ToLower());
                    if (daTonTaiTen)
                    {
                        MessageBox.Show("Tên loại sách này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    table.InsertOnSubmit(loaiMoi);
                    db.SubmitChanges();
                }

                MessageBox.Show("Thêm loại sách mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                btnLamMoi_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm loại sách: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textMaLoai.Text))
            {
                MessageBox.Show("Vui lòng chọn loại sách cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenLoaiMoi = txtTenLoai.Text.Trim();
            if (string.IsNullOrEmpty(tenLoaiMoi))
            {
                MessageBox.Show("Tên loại sách không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLoai.Focus();
                return;
            }

            int maLoai = Convert.ToInt32(textMaLoai.Text);

            try
            {
                using (var db = LayDataContext())
                {
                    var table = db.GetTable<tbl_loai_sach>();
                    var loai = table.FirstOrDefault(x => x.ma_loai_sach == maLoai);
                    if (loai == null)
                    {
                        MessageBox.Show("Không tìm thấy loại sách cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    bool daTonTai = table.Any(x => x.ten_loai_sach.ToLower() == tenLoaiMoi.ToLower() && x.ma_loai_sach != maLoai);
                    if (daTonTai)
                    {
                        MessageBox.Show("Tên loại sách này đã tồn tại ở bản ghi khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    loai.ten_loai_sach = tenLoaiMoi;
                    db.SubmitChanges();
                }

                MessageBox.Show("Cập nhật loại sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                btnLamMoi_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật loại sách: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textMaLoai.Text))
            {
                MessageBox.Show("Vui lòng chọn loại sách cần xóa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maLoai = Convert.ToInt32(textMaLoai.Text);

            var xacNhan = MessageBox.Show("Bạn có chắc chắn muốn xóa loại sách này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (xacNhan == DialogResult.Yes)
            {
                try
                {
                    using (var db = LayDataContext())
                    {
                        // Kiểm tra ràng buộc sách
                        bool coSach = db.GetTable<tbl_sach>().Any(s => s.ma_loai_sach == maLoai);
                        if (coSach)
                        {
                            MessageBox.Show("Không thể xóa loại sách này vì đã có đầu sách thuộc thể loại này trong kho!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        var table = db.GetTable<tbl_loai_sach>();
                        var loai = table.FirstOrDefault(x => x.ma_loai_sach == maLoai);
                        if (loai != null)
                        {
                            table.DeleteOnSubmit(loai);
                            db.SubmitChanges();
                        }
                    }

                    MessageBox.Show("Xóa loại sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    btnLamMoi_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa loại sách: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textMaLoai_TextChanged(object sender, EventArgs e)
        {
            bool dangChonLoaiSach = !string.IsNullOrWhiteSpace(textMaLoai.Text);

            btnThem.Enabled = !dangChonLoaiSach;
            btnSua.Enabled = dangChonLoaiSach;
            btnXoa.Enabled = dangChonLoaiSach;
        }

        private void txtTenLoai_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textMaLoai.Text))
            {
                string tuKhoa = txtTenLoai.Text.Trim().ToLower();

                try
                {
                    using (var db = LayDataContext())
                    {
                        var table = db.GetTable<tbl_loai_sach>();

                        var ketQua = table
                            .Where(x => x.ten_loai_sach.ToLower().Contains(tuKhoa))
                            .Select(x => new
                            {
                                ma_loai_sach = x.ma_loai_sach,
                                ten_loai_sach = x.ten_loai_sach
                            })
                            .ToList();

                        dvgLoaiSach.DataSource = null;
                        dvgLoaiSach.DataSource = ketQua;
                    }
                }
                catch
                {
                    // Bỏ qua lỗi kết nối khi gõ phím nhanh
                }
            }
        }

        private void dvgLoaiSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dvgLoaiSach.Rows[e.RowIndex];
                textMaLoai.Text = row.Cells[0].Value?.ToString() ?? "";
                txtTenLoai.Text = row.Cells[1].Value?.ToString() ?? "";
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e) { }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}