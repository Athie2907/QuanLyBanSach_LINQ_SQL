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
    public partial class frmHoaDon : Form
    {
        public frmHoaDon()
        {
            InitializeComponent();

            // Các cột Column1..Column4 trong Designer không gắn dữ liệu nên lưới trống -> tạo lại bằng code
            LuoiHelper.DatCot(dvgHoaDon,
                new CotLuoi("colMaHoaDon", "Mã HĐ", "MaHoaDon"),
                new CotLuoi("colNgayLap", "Ngày lập", "NgayLap", "dd/MM/yyyy"),
                new CotLuoi("colTenKH", "Tên khách hàng", "TenKhachHang"),
                new CotLuoi("colSDT", "Số điện thoại", "SoDienThoai"),
                new CotLuoi("colTongTien", "Tổng tiền (VNĐ)", "TongTien", "N0", true));

            dvgHoaDon.CellDoubleClick += dvgHoaDon_CellDoubleClick;
        }

        // Cờ chặn: lúc gán ngày mặc định trong Load thì chưa lọc
        private bool _dangKhoiTao = false;

        // Tải danh sách hóa đơn khi form khởi chạy
        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            _dangKhoiTao = true;

            // Mặc định khoảng thời gian lọc: từ đầu tháng hiện tại đến ngày hôm nay
            DateTime now = DateTime.Now;
            dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
            dtpDenNgay.Value = now;

            _dangKhoiTao = false;

            // Thực thi lọc để hiển thị danh sách ban đầu
            btnLoc_Click(null, null);
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        // Tự động tải lại danh sách khi thay đổi "Từ ngày"
        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
            if (_dangKhoiTao) return;
            btnLoc_Click(null, null);
        }

        // Tự động tải lại danh sách khi thay đổi "Đến ngày"
        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {
            if (_dangKhoiTao) return;
            btnLoc_Click(null, null);
        }

        // 1. NÚT LỌC: Lọc danh sách hóa đơn theo khoảng thời gian và tính tổng tiền từng hóa đơn
        private void btnLoc_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1); // Lấy đến hết ngày 23:59:59

            if (tuNgay > denNgay)
            {
                MessageBox.Show("Từ ngày không được lớn hơn đến ngày!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string connStr = Properties.Settings.Default.db_QuanLyBanSachConnectionString;
                using (var db = new QuanLyBanSachDataContext(connStr))
                {
                    var tableHD = db.GetTable<tbl_hoa_don>();
                    var tableCT = db.GetTable<tbl_chi_tiet_hoa_don>();
                    var tableSach = db.GetTable<tbl_sach>();

                    var danhSach = (from hd in tableHD
                                    where hd.ngay_lap_hoa_don >= tuNgay && hd.ngay_lap_hoa_don <= denNgay
                                    orderby hd.ngay_lap_hoa_don descending
                                    select new
                                    {
                                        MaHoaDon = hd.ma_hoa_don,
                                        NgayLap = hd.ngay_lap_hoa_don,
                                        TenKhachHang = hd.ten_khach_hang,
                                        SoDienThoai = hd.sdt_khach_hang,
                                        TongTien = (from ct in tableCT
                                                    join s in tableSach on ct.ma_sach equals s.ma_sach
                                                    where ct.ma_hoa_don == hd.ma_hoa_don
                                                    select (decimal?)((ct.so_luong ?? 0) * (s.gia_ban ?? 0))).Sum() ?? 0
                                    }).ToList();

                    dvgHoaDon.DataSource = null;
                    dvgHoaDon.DataSource = danhSach;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lọc danh sách hóa đơn: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 2. NÚT TẠO MỚI: Thêm nhanh một bản ghi hóa đơn mới vào CSDL rồi mở form chi tiết
        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            // Tạo form nhập liệu nhỏ nhanh ngay trong code
            Form prompt = new Form()
            {
                Width = 360,
                Height = 220,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Tạo hóa đơn mới",
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lblTen = new Label() { Left = 20, Top = 20, Text = "Tên khách hàng:", Width = 120 };
            TextBox txtTen = new TextBox() { Left = 20, Top = 45, Width = 300, Text = "Khách vãng lai" };

            Label lblSdt = new Label() { Left = 20, Top = 80, Text = "Số điện thoại:", Width = 120 };
            TextBox txtSdt = new TextBox() { Left = 20, Top = 105, Width = 300 };

            Button btnXacNhan = new Button() { Text = "Tạo", Left = 140, Width = 85, Top = 140, DialogResult = DialogResult.OK };
            Button btnHuy = new Button() { Text = "Hủy", Left = 235, Width = 85, Top = 140, DialogResult = DialogResult.Cancel };

            prompt.Controls.Add(lblTen);
            prompt.Controls.Add(txtTen);
            prompt.Controls.Add(lblSdt);
            prompt.Controls.Add(txtSdt);
            prompt.Controls.Add(btnXacNhan);
            prompt.Controls.Add(btnHuy);
            prompt.AcceptButton = btnXacNhan;
            prompt.CancelButton = btnHuy;

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                string tenKhach = string.IsNullOrWhiteSpace(txtTen.Text) ? "Khách vãng lai" : txtTen.Text.Trim();
                string sdtKhach = txtSdt.Text.Trim();

                try
                {
                    int maHoaDonVuaTao = 0;
                    string connStr = Properties.Settings.Default.db_QuanLyBanSachConnectionString;
                    using (var db = new QuanLyBanSachDataContext(connStr))
                    {
                        tbl_hoa_don hdMoi = new tbl_hoa_don
                        {
                            ngay_lap_hoa_don = DateTime.Now,
                            ten_khach_hang = tenKhach,
                            sdt_khach_hang = sdtKhach
                        };

                        db.GetTable<tbl_hoa_don>().InsertOnSubmit(hdMoi);
                        db.SubmitChanges();

                        maHoaDonVuaTao = hdMoi.ma_hoa_don;
                    }

                    MessageBox.Show($"Tạo thành công hóa đơn #{maHoaDonVuaTao}! Bấm OK để thêm sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mở form chi tiết để chọn sách vào hóa đơn vừa tạo
                    frmChiTietHoaDon fChiTiet = new frmChiTietHoaDon(maHoaDonVuaTao);
                    fChiTiet.ShowDialog();

                    // Tải lại danh sách sau khi đóng form chi tiết
                    btnLoc_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tạo hóa đơn mới: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 3. NÚT XEM CHI TIẾT: Lấy mã hóa đơn đang chọn trên DataGridView và mở frmChiTietHoaDon
        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (dvgHoaDon.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn từ danh sách để xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dongHienTai = dvgHoaDon.CurrentRow;
            int maHoaDon = Convert.ToInt32(dongHienTai.Cells["colMaHoaDon"]?.Value ?? dongHienTai.Cells[0].Value);

            frmChiTietHoaDon fChiTiet = new frmChiTietHoaDon(maHoaDon);
            fChiTiet.ShowDialog();

            // Tải lại dữ liệu để cập nhật lại tổng tiền nếu có thêm/bớt sách
            btnLoc_Click(null, null);
        }

        // 4. NÚT IN HÓA ĐƠN: mở báo cáo ReportViewer (rptHoaDon.rdlc)
        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (dvgHoaDon.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maHoaDon = Convert.ToInt32(dvgHoaDon.CurrentRow.Cells["colMaHoaDon"].Value);
            InBaoCao.InHoaDon(maHoaDon, this);
        }

        // Nhấp đúp vào dòng hóa đơn trên lưới để mở nhanh chi tiết
        private void dvgHoaDon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnXemChiTiet_Click(null, null);
            }
        }

        // (Giữ lại vì Designer đã gắn sự kiện này; không làm gì để bấm 1 lần không mở form)
        private void dvgHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}