using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyBanSach_LINQ_SQL
{
    public partial class frmChiTietPhieuNhap : Form
    {
        private int _maPhieuNhap = 0;

        private QuanLyBanSachDataContext LayDataContext()
        {
            string connStr = Properties.Settings.Default.db_QuanLyBanSachConnectionString;
            return new QuanLyBanSachDataContext(connStr);
        }

        // Hàm khởi tạo form mặc định
        public frmChiTietPhieuNhap()
        {
            InitializeComponent();

            // Các cột trong Designer không gắn dữ liệu -> tạo lại bằng code
            LuoiHelper.DatCot(dvgChiTiet,
                new CotLuoi("colMaSach", "Mã sách", "MaSach"),
                new CotLuoi("colTenSach", "Tên sách", "TenSach"),
                new CotLuoi("colSoLuong", "Số lượng", "SoLuong"),
                new CotLuoi("colDonGiaNhap", "Đơn giá nhập (tham chiếu)", "DonGiaNhap", "N0", true),
                new CotLuoi("colThanhTien", "Thành tiền", "ThanhTien", "N0", true));

            dvgChiTiet.CellDoubleClick += dvgChiTiet_CellDoubleClick;
            dvgChiTiet.CellContentClick += dvgChiTiet_CellContentClick;
            LoadDanhSachSach();
        }

        // Constructor nhận mã phiếu nhập truyền từ frmPhieuNhap sang
        public frmChiTietPhieuNhap(int maPN) : this()
        {
            _maPhieuNhap = maPN;
        }

        private void frmChiTietPhieuNhap_Load(object sender, EventArgs e)
        {
            // Thiết lập giới hạn tối đa cho ô nhập tiền và số lượng
            numDonGiaNhap.Maximum = 100000000;
            numDonGiaNhap.ThousandsSeparator = true;
            numSoLuong.Maximum = 100000;

            // NẠP DANH SÁCH SÁCH VÀO COMBOBOX NGAY KHI FORM LOAD LÊN
            LoadDanhSachSach();
            LoadChiTiet();
        }

        // Tải danh mục sách vào ComboBox để chọn sách nhập
        private void LoadDanhSachSach()
        {
            try
            {
                using (var db = LayDataContext())
                {
                    // Lấy danh sách trực tiếp dưới dạng List
                    var listSach = db.tbl_saches
                        .Select(s => new
                        {
                            ma_sach = s.ma_sach,
                            ten_sach = s.ten_sach
                        })
                        .ToList();

                    // Gán DataSource theo đúng chuẩn để ComboBox hiển thị đầy đủ
                    cbSach.DataSource = null;
                    cbSach.DisplayMember = "ten_sach";
                    cbSach.ValueMember = "ma_sach";
                    cbSach.DataSource = listSach;
                    cbSach.SelectedIndex = -1; // Không chọn sẵn dòng nào lúc đầu
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tải danh sách chi tiết các cuốn sách trong phiếu nhập và tính tổng tiền
        private void LoadChiTiet()
        {
            try
            {
                using (var db = LayDataContext())
                {
                    var listCT = (from ct in db.GetTable<tbl_chi_tiet_phieu_nhap>()
                                  join s in db.GetTable<tbl_sach>() on ct.ma_sach equals s.ma_sach
                                  where ct.ma_phieu_nhap == _maPhieuNhap
                                  select new
                                  {
                                      MaSach = ct.ma_sach,
                                      TenSach = s.ten_sach,
                                      SoLuong = ct.so_luong,
                                      DonGiaNhap = (s.gia_ban ?? 0) * 0.7m,
                                      ThanhTien = (ct.so_luong ?? 0) * ((s.gia_ban ?? 0) * 0.7m)
                                  }).ToList();

                    dvgChiTiet.DataSource = null;
                    dvgChiTiet.DataSource = listCT;

                    decimal tongTien = listCT.Sum(x => x.ThanhTien);
                    lblTongTien.Text = tongTien.ToString("N0") + " VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết phiếu nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tự động gợi ý đơn giá nhập tương ứng với sách đang chọn
            if (cbSach.SelectedValue == null || cbSach.SelectedIndex == -1) return;

            int maSach;
            if (int.TryParse(cbSach.SelectedValue.ToString(), out maSach))
            {
                try
                {
                    using (var db = LayDataContext())
                    {
                        var sach = db.GetTable<tbl_sach>().FirstOrDefault(s => s.ma_sach == maSach);
                        if (sach != null)
                        {
                            decimal giaGoiY = (sach.gia_ban ?? 0) * 0.7m;
                            numDonGiaNhap.Value = giaGoiY > 0 ? giaGoiY : 0;
                        }
                    }
                }
                catch { }
            }
        }

        private void numSoLuong_ValueChanged(object sender, EventArgs e)
        {
            int soLuong = (int)numSoLuong.Value;
            decimal donGia = numDonGiaNhap.Value;
            decimal tamTinh = soLuong * donGia;

            if (this.Controls.ContainsKey("lblThanhTien"))
            {
                this.Controls["lblThanhTien"].Text = "Thành tiền: " + tamTinh.ToString("N0") + " VNĐ";
            }
        }

        private void numDonGiaNhap_ValueChanged(object sender, EventArgs e)
        {
            int soLuong = (int)numSoLuong.Value;
            decimal donGia = numDonGiaNhap.Value;
            decimal tamTinh = soLuong * donGia;

            if (this.Controls.ContainsKey("lblThanhTien"))
            {
                this.Controls["lblThanhTien"].Text = "Thành tiền: " + tamTinh.ToString("N0") + " VNĐ";
            }
        }

        // NÚT THÊM SÁCH VÀO PHIẾU NHẬP (CỘNG TỒN KHO)
        private void btnThemSach_Click(object sender, EventArgs e)
        {
            if (cbSach.SelectedValue == null || cbSach.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn sách cần nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soLuongNhap = (int)numSoLuong.Value;
            if (soLuongNhap <= 0)
            {
                MessageBox.Show("Số lượng nhập phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maSach = Convert.ToInt32(cbSach.SelectedValue);

            try
            {
                using (var db = LayDataContext())
                {
                    var sach = db.GetTable<tbl_sach>().FirstOrDefault(s => s.ma_sach == maSach);
                    if (sach == null)
                    {
                        MessageBox.Show("Sách không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var tableCT = db.GetTable<tbl_chi_tiet_phieu_nhap>();
                    var dongHienCo = tableCT.FirstOrDefault(x => x.ma_phieu_nhap == _maPhieuNhap && x.ma_sach == maSach);

                    if (dongHienCo != null)
                    {
                        dongHienCo.so_luong = (dongHienCo.so_luong ?? 0) + soLuongNhap;
                    }
                    else
                    {
                        tbl_chi_tiet_phieu_nhap ctMoi = new tbl_chi_tiet_phieu_nhap
                        {
                            ma_phieu_nhap = _maPhieuNhap,
                            ma_sach = maSach,
                            so_luong = soLuongNhap
                        };
                        tableCT.InsertOnSubmit(ctMoi);
                    }

                    sach.so_luong = (sach.so_luong ?? 0) + soLuongNhap;
                    db.SubmitChanges();
                }

                MessageBox.Show("Đã thêm sách vào phiếu nhập và cộng tồn kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                numSoLuong.Value = 1;
                cbSach.SelectedIndex = -1;
                LoadChiTiet();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sách vào phiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // NÚT XÓA MÓN KHỎI PHIẾU NHẬP (TRỪ BỚT TỒN KHO)
        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (dvgChiTiet.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dòng sách cần xóa khỏi phiếu nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dongHienTai = dvgChiTiet.CurrentRow;
            int maSach = Convert.ToInt32(dongHienTai.Cells["colMaSach"]?.Value ?? dongHienTai.Cells[0].Value);
            int soLuongXoa = Convert.ToInt32(dongHienTai.Cells["colSoLuong"]?.Value ?? dongHienTai.Cells[2].Value);

            var xacNhan = MessageBox.Show("Xóa mục này sẽ trừ lại số lượng sách đã cộng vào kho. Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (xacNhan == DialogResult.Yes)
            {
                try
                {
                    using (var db = LayDataContext())
                    {
                        var tableCT = db.GetTable<tbl_chi_tiet_phieu_nhap>();
                        var ct = tableCT.FirstOrDefault(x => x.ma_phieu_nhap == _maPhieuNhap && x.ma_sach == maSach);

                        if (ct != null)
                        {
                            tableCT.DeleteOnSubmit(ct);

                            var sach = db.GetTable<tbl_sach>().FirstOrDefault(s => s.ma_sach == maSach);
                            if (sach != null)
                            {
                                sach.so_luong = Math.Max(0, (sach.so_luong ?? 0) - soLuongXoa);
                            }

                            db.SubmitChanges();
                        }
                    }

                    LoadChiTiet();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblTongTien_Click(object sender, EventArgs e) { }

        private void dvgChiTiet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dvgChiTiet.Rows[e.RowIndex];

            try
            {
                if (dvgChiTiet.Columns[e.ColumnIndex].Name == "colXoa" ||
                    dvgChiTiet.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    btnXoaMon_Click(sender, e);
                    return;
                }

                var maSachVal = row.Cells["colMaSach"]?.Value ?? row.Cells[0].Value;
                if (maSachVal != null)
                {
                    cbSach.SelectedValue = Convert.ToInt32(maSachVal);
                }

                var soLuongVal = row.Cells["colSoLuong"]?.Value ?? row.Cells[2].Value;
                if (soLuongVal != null)
                {
                    numSoLuong.Value = Math.Min(Math.Max(Convert.ToDecimal(soLuongVal), numSoLuong.Minimum), numSoLuong.Maximum);
                }

                var donGiaVal = row.Cells["colDonGiaNhap"]?.Value ?? row.Cells[3].Value;
                if (donGiaVal != null)
                {
                    numDonGiaNhap.Value = Math.Min(Math.Max(Convert.ToDecimal(donGiaVal), numDonGiaNhap.Minimum), numDonGiaNhap.Maximum);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chọn dòng: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dvgChiTiet_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Có thể xử lý nếu muốn double click
            }
        }
    }
}