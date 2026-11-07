using System;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyBanSach_LINQ_SQL
{
    public partial class frmSach : Form
    {
        private int? maSachDangChon = null;

        private QuanLyBanSachDataContext LayDataContext()
        {
            return new QuanLyBanSachDataContext(Properties.Settings.Default.db_QuanLyBanSachConnectionString);
        }

        public frmSach()
        {
            InitializeComponent();

            LuoiHelper.DatCot(dvgSach,
                new CotLuoi("colMa", "Mã", "ma_sach"),
                new CotLuoi("colTen", "Tên sách", "ten_sach"),
                new CotLuoi("colLoai", "Loại sách", "ten_loai_sach"),
                new CotLuoi("colSL", "SL", "so_luong", "N0", true),
                new CotLuoi("colGia", "Giá", "gia_ban", "N0", true));

            // Bấm vào dòng nào cũng chọn được
            dvgSach.CellClick += dataGridView1_CellContentClick;
        }

        private void frmSach_Load(object sender, EventArgs e)
        {
            
            numSoLuong.Maximum = 100000;
            numGiaBan.Maximum = 1000000000;
            numGiaBan.ThousandsSeparator = true;

            LoadLoaiSach();
            LoadSach();
        }

        private void LoadLoaiSach()
        {
            try
            {
                using (var db = LayDataContext())
                {
                    var ds = db.tbl_loai_saches.OrderBy(x => x.ten_loai_sach).ToList();
                    cbLoaiSach.DataSource = ds;
                    cbLoaiSach.DisplayMember = "ten_loai_sach";
                    cbLoaiSach.ValueMember = "ma_loai_sach";
                    cbLoaiSach.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải loại sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tải danh sách sách, có lọc theo ô tìm kiếm (tên sách hoặc tác giả)
        private void LoadSach()
        {
            try
            {
                // Lấy chữ trong ô tìm kiếm, nếu null thì gán bằng rỗng
                string tuKhoa = string.IsNullOrEmpty(txtTimkiem.Text) ? "" : txtTimkiem.Text.Trim();

                using (var db = LayDataContext())
                {
                    var query = db.tbl_saches.AsQueryable();

                    // Điều kiện lọc an toàn tuyệt đối, tránh lỗi dịch SQL
                    if (tuKhoa != "")
                    {
                        query = query.Where(x => x.ten_sach.Contains(tuKhoa) ||
                                                 (x.tac_gia != null && x.tac_gia.Contains(tuKhoa)));
                    }

                    var ds = query
                        .OrderBy(x => x.ma_sach)
                        .Select(x => new
                        {
                            ma_sach = x.ma_sach,
                            ten_sach = x.ten_sach,
                            ten_loai_sach = x.tbl_loai_sach != null ? x.tbl_loai_sach.ten_loai_sach : "Chưa phân loại",
                            so_luong = x.so_luong ?? 0,
                            gia_ban = x.gia_ban ?? 0m
                        })
                        .ToList();

                    dvgSach.AutoGenerateColumns = false;
                    dvgSach.DataSource = null;
                    dvgSach.DataSource = ds;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Chọn dòng trên lưới -> đẩy lên các ô nhập
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                int ma = Convert.ToInt32(dvgSach.Rows[e.RowIndex].Cells["colMa"].Value);

                using (var db = LayDataContext())
                {
                    var sach = db.tbl_saches.FirstOrDefault(x => x.ma_sach == ma);
                    if (sach == null) return;

                    maSachDangChon = sach.ma_sach;
                    txtMaSach.Text = sach.ma_sach.ToString();
                    textTenSach.Text = sach.ten_sach;
                    txtTacGia.Text = sach.tac_gia ?? "";
                    numSoLuong.Value = Math.Min(Math.Max(sach.so_luong ?? 0, numSoLuong.Minimum), numSoLuong.Maximum);
                    numGiaBan.Value = Math.Min(Math.Max(sach.gia_ban ?? 0m, numGiaBan.Minimum), numGiaBan.Maximum);

                    if (sach.ma_loai_sach.HasValue)
                        cbLoaiSach.SelectedValue = sach.ma_loai_sach.Value;
                    else
                        cbLoaiSach.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chọn sách: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // NÚT THÊM (Designer gắn vào button1_Click)
        private void button1_Click(object sender, EventArgs e)
        {
            string ten = textTenSach.Text.Trim();
            if (ten == "")
            {
                MessageBox.Show("Vui lòng nhập tên sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textTenSach.Focus();
                return;
            }

            try
            {
                using (var db = LayDataContext())
                {
                    tbl_sach sach = new tbl_sach
                    {
                        ten_sach = ten,
                        tac_gia = string.IsNullOrWhiteSpace(txtTacGia.Text) ? null : txtTacGia.Text.Trim(),
                        ma_loai_sach = cbLoaiSach.SelectedValue == null ? (int?)null : Convert.ToInt32(cbLoaiSach.SelectedValue),
                        so_luong = Convert.ToInt32(numSoLuong.Value),
                        gia_ban = numGiaBan.Value
                    };

                    db.tbl_saches.InsertOnSubmit(sach);
                    db.SubmitChanges();
                }

                MessageBox.Show("Thêm sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSach();
                btnLamMoi_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // NÚT SỬA (Designer gắn vào button2_Click)
        private void button2_Click(object sender, EventArgs e)
        {
            if (maSachDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn sách cần sửa từ bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ten = textTenSach.Text.Trim();
            if (ten == "")
            {
                MessageBox.Show("Tên sách không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textTenSach.Focus();
                return;
            }

            try
            {
                using (var db = LayDataContext())
                {
                    var sach = db.tbl_saches.FirstOrDefault(x => x.ma_sach == maSachDangChon.Value);
                    if (sach == null)
                    {
                        MessageBox.Show("Không tìm thấy sách cần sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    sach.ten_sach = ten;
                    sach.tac_gia = string.IsNullOrWhiteSpace(txtTacGia.Text) ? null : txtTacGia.Text.Trim();
                    sach.ma_loai_sach = cbLoaiSach.SelectedValue == null ? (int?)null : Convert.ToInt32(cbLoaiSach.SelectedValue);
                    sach.so_luong = Convert.ToInt32(numSoLuong.Value);
                    sach.gia_ban = numGiaBan.Value;

                    db.SubmitChanges();
                }

                MessageBox.Show("Cập nhật sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSach();
                btnLamMoi_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (maSachDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn sách cần xóa từ bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa sách này?", "Xác nhận xóa",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (var db = LayDataContext())
                {
                    bool coTrongHoaDon = db.tbl_chi_tiet_hoa_dons.Any(x => x.ma_sach == maSachDangChon.Value);
                    bool coTrongPhieuNhap = db.tbl_chi_tiet_phieu_nhaps.Any(x => x.ma_sach == maSachDangChon.Value);

                    if (coTrongHoaDon || coTrongPhieuNhap)
                    {
                        MessageBox.Show("Không thể xóa sách này vì đã có trong hóa đơn hoặc phiếu nhập!",
                            "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var sach = db.tbl_saches.FirstOrDefault(x => x.ma_sach == maSachDangChon.Value);
                    if (sach != null)
                    {
                        db.tbl_saches.DeleteOnSubmit(sach);
                        db.SubmitChanges();
                    }
                }

                MessageBox.Show("Xóa sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSach();
                btnLamMoi_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            maSachDangChon = null;
            txtMaSach.Clear();
            textTenSach.Clear();
            txtTacGia.Clear();
            cbLoaiSach.SelectedIndex = -1;
            numSoLuong.Value = 0;
            numGiaBan.Value = 0;
            dvgSach.ClearSelection();
            textTenSach.Focus();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            LoadSach();
        }

        // Gõ tới đâu lọc tới đó
        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            LoadSach();
        }

        // Các hàm rỗng dưới đây được Designer gắn sẵn nên giữ lại để không lỗi biên dịch
        private void label2_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void txtTacGia_TextChanged(object sender, EventArgs e) { }
        private void numSoLuong_ValueChanged(object sender, EventArgs e) { }
        private void numGiaBan_ValueChanged(object sender, EventArgs e) { }
        private void cbLoaiSach_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}