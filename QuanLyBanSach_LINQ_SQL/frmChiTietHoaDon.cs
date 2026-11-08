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
    public partial class frmChiTietHoaDon : Form
    {
        private int _maHoaDon = 0;

        private QuanLyBanSachDataContext LayDataContext()
        {
            string connStr = Properties.Settings.Default.db_QuanLyBanSachConnectionString;
            return new QuanLyBanSachDataContext(connStr);
        }

        public frmChiTietHoaDon()
        {
            InitializeComponent();

            // Các cột Column1..Column5 trong Designer không gắn dữ liệu -> tạo lại bằng code
            LuoiHelper.DatCot(dvgChiTiet,
                new CotLuoi("colMaSach", "Mã sách", "MaSach"),
                new CotLuoi("colTenSach", "Tên sách", "TenSach"),
                new CotLuoi("colSoLuong", "Số lượng", "SoLuong"),
                new CotLuoi("colDonGia", "Đơn giá", "DonGia", "N0", true),
                new CotLuoi("colThanhTien", "Thành tiền", "ThanhTien", "N0", true));

            // GỌI THẲNG HÀM NẠP DỮ LIỆU VÀO ĐÂY ĐỂ CHẮC CHẮN CHẠY 100% KHI MỞ FORM
            LoadDuLieuBanDau();
        }

        public frmChiTietHoaDon(int maHD) : this()
        {
            _maHoaDon = maHD;
        }

        // Tách riêng hàm nạp dữ liệu để gọi trực tiếp ở Constructor và sự kiện Load
        private void LoadDuLieuBanDau()
        {
            try
            {
                using (var db = LayDataContext())
                {
                    // Đổ dữ liệu vào ComboBox sách chuẩn xác
                    var listSach = db.GetTable<tbl_sach>()
                        .Select(s => new { s.ma_sach, s.ten_sach, s.gia_ban, s.so_luong })
                        .ToList();

                    cbSach.DataSource = null;
                    cbSach.DisplayMember = "ten_sach";
                    cbSach.ValueMember = "ma_sach";
                    cbSach.DataSource = listSach;
                    cbSach.SelectedIndex = -1; // Để trống lúc đầu

                    // Đổ dữ liệu vào DataGridView và tính tổng tiền
                    var listChiTiet = (from ct in db.GetTable<tbl_chi_tiet_hoa_don>()
                                       join s in db.GetTable<tbl_sach>() on ct.ma_sach equals s.ma_sach
                                       where ct.ma_hoa_don == _maHoaDon
                                       select new
                                       {
                                           MaSach = ct.ma_sach,
                                           TenSach = s.ten_sach,
                                           SoLuong = ct.so_luong,
                                           DonGia = s.gia_ban ?? 0,
                                           ThanhTien = (ct.so_luong ?? 0) * (s.gia_ban ?? 0)
                                       }).ToList();

                    dvgChiTiet.DataSource = null;
                    dvgChiTiet.DataSource = listChiTiet;

                    decimal tongTien = listChiTiet.Sum(x => x.ThanhTien);
                    lblTongTien.Text = tongTien.ToString("N0") + " VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu ban đầu: " + ex.Message);
            }
        }

        private void frmChiTietHoaDon_Load(object sender, EventArgs e)
        {
            // Giữ lại để không lỗi nếu Designer đã liên kết sự kiện Load
        }

        // 2. Khi chọn sách trong ComboBox: Hiển thị ngay đơn giá bán
        private void cbSach_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                            lblDonGia.Text = (sach.gia_ban ?? 0).ToString("N0") + " VNĐ";
                        }
                    }
                }
                catch { }
            }
        }

        // 3. NÚT THÊM SÁCH
        private void btnThemSach_Click(object sender, EventArgs e)
        {
            if (cbSach.SelectedValue == null || cbSach.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn sách!");
                return;
            }

            int soLuongMua = (int)numSoLuong.Value;
            if (soLuongMua <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0!");
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
                        MessageBox.Show("Sách không tồn tại!");
                        return;
                    }

                    if ((sach.so_luong ?? 0) < soLuongMua)
                    {
                        MessageBox.Show($"Kho chỉ còn {sach.so_luong ?? 0} quyển, không đủ bán!");
                        return;
                    }

                    var tableChiTiet = db.GetTable<tbl_chi_tiet_hoa_don>();
                    var dongHienCo = tableChiTiet.FirstOrDefault(x => x.ma_hoa_don == _maHoaDon && x.ma_sach == maSach);

                    if (dongHienCo != null)
                    {
                        dongHienCo.so_luong = (dongHienCo.so_luong ?? 0) + soLuongMua;
                    }
                    else
                    {
                        tbl_chi_tiet_hoa_don ctMoi = new tbl_chi_tiet_hoa_don
                        {
                            ma_hoa_don = _maHoaDon,
                            ma_sach = maSach,
                            so_luong = soLuongMua
                        };
                        tableChiTiet.InsertOnSubmit(ctMoi);
                    }

                    // Trừ tồn kho
                    sach.so_luong -= soLuongMua;
                    db.SubmitChanges();

                    // Tải lại DataGridView và tính tổng tiền
                    LoadDuLieuBanDau();
                }

                numSoLuong.Value = 1;
                cbSach.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sách: " + ex.Message);
            }
        }

        // 4. NÚT XÓA MÓN
        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (dvgChiTiet.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn món cần xóa!");
                return;
            }

            var dongHienTai = dvgChiTiet.CurrentRow;
            int maSach = Convert.ToInt32(dongHienTai.Cells[0].Value);
            int soLuongHoan = Convert.ToInt32(dongHienTai.Cells[2].Value);

            if (MessageBox.Show("Bạn có muốn xóa sách này khỏi hóa đơn?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (var db = LayDataContext())
                    {
                        var tableChiTiet = db.GetTable<tbl_chi_tiet_hoa_don>();
                        var ct = tableChiTiet.FirstOrDefault(x => x.ma_hoa_don == _maHoaDon && x.ma_sach == maSach);

                        if (ct != null)
                        {
                            tableChiTiet.DeleteOnSubmit(ct);

                            // Hoàn trả lại số lượng kho
                            var sach = db.GetTable<tbl_sach>().FirstOrDefault(s => s.ma_sach == maSach);
                            if (sach != null)
                            {
                                sach.so_luong = (sach.so_luong ?? 0) + soLuongHoan;
                            }

                            db.SubmitChanges();
                        }

                        LoadDuLieuBanDau();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                }
            }
        }

        // 5. NÚT ĐÓNG
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numSoLuong_ValueChanged(object sender, EventArgs e)
        {
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
                            int soLuongKho = sach.so_luong ?? 0;
                            int soLuongChon = (int)numSoLuong.Value;

                            if (soLuongChon > soLuongKho)
                            {
                                MessageBox.Show($"Kho chỉ còn {soLuongKho} quyển!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                numSoLuong.Value = soLuongKho > 0 ? soLuongKho : 1;
                                return;
                            }

                            decimal donGia = sach.gia_ban ?? 0;
                            decimal tamTinh = donGia * (int)numSoLuong.Value;
                            lblDonGia.Text = string.Format("{0:N0} VNĐ (Tạm tính: {1:N0} VNĐ)", donGia, tamTinh);
                        }
                    }
                }
                catch { }
            }
        }

        private void lblDonGia_Click(object sender, EventArgs e) { }
        private void lblTongTien_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void dvgChiTiet_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}