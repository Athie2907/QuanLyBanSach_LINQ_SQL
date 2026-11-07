using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyBanSach_LINQ_SQL
{
    public partial class FrmPhieuNhap : Form
    {
        // -------------------------------------------------------------
        // Khởi tạo DataContext kết nối cơ sở dữ liệu từ chuỗi kết nối lưu trong Settings
        // -------------------------------------------------------------
        private QuanLyBanSachDataContext LayDataContext()
        {
            return new QuanLyBanSachDataContext(Properties.Settings.Default.db_QuanLyBanSachConnectionString);
        }

        // Hàm khởi tạo form mặc định
        public FrmPhieuNhap()
        {
            InitializeComponent();

            // Các cột Column1..Column4 trong Designer không gắn dữ liệu nên lưới trống -> tạo lại bằng code
            LuoiHelper.DatCot(dvgPhieuNhap,
                new CotLuoi("colMaPN", "Mã phiếu nhập", "MaPN"),
                new CotLuoi("colNgayLap", "Ngày lập", "NgayLap", "dd/MM/yyyy"),
                new CotLuoi("colNhaCungCap", "Nhà cung cấp", "NhaCungCap"),
                new CotLuoi("colTongTien", "Tổng tiền tham chiếu (VNĐ)", "TongTien", "N0", true));

            dvgPhieuNhap.CellDoubleClick += dvgPhieuNhap_CellDoubleClick;
        }

        // Cờ chặn: lúc gán giá trị mặc định trong Load thì chưa lọc
        private bool _dangKhoiTao = false;

        // Sự kiện chạy tự động ngay khi Form vừa mở lên
        private void FrmPhieuNhap_Load(object sender, EventArgs e)
        {
            _dangKhoiTao = true;

            DateTime now = DateTime.Now;
            // Đặt ngày bắt đầu lọc mặc định là ngày mùng 1 đầu tháng hiện tại
            dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
            // Đặt ngày kết thúc lọc là thời điểm hiện tại
            dtpDenNgay.Value = now;

            // Nạp danh sách nhà cung cấp vào ComboBox để người dùng chọn lọc
            LoadNhaCungCap();

            _dangKhoiTao = false;
            // Nạp toàn bộ danh sách phiếu nhập lên DataGridView theo mốc ngày mặc định
            LoadDanhSachPhieuNhap();
        }

        // -------------------------------------------------------------
        // Hàm lấy danh sách tên các nhà cung cấp phân biệt đổ vào ComboBox cbNhaCungCap
        // -------------------------------------------------------------
        private void LoadNhaCungCap()
        {
            try
            {
                using (var db = LayDataContext())
                {
                    // Lọc lấy danh sách cột ten_nha_cung_cap không trùng lặp (Distinct) từ bảng tbl_phieu_nhap
                    var nccList = db.GetTable<tbl_phieu_nhap>()
                        .Where(p => !string.IsNullOrEmpty(p.ten_nha_cung_cap))
                        .Select(p => p.ten_nha_cung_cap)
                        .Distinct()
                        .ToList();

                    // Thêm dòng mặc định ở đầu để người dùng có thể xem tất cả nhà cung cấp
                    nccList.Insert(0, "-- Tất cả nhà cung cấp --");
                    cbNhaCungCap.DataSource = nccList;
                }
            }
            catch
            {
                // Nếu chưa có dữ liệu thì gán mục mặc định tránh treo chương trình
                cbNhaCungCap.Items.Clear();
                cbNhaCungCap.Items.Add("-- Tất cả nhà cung cấp --");
                cbNhaCungCap.SelectedIndex = 0;
            }
        }

        // -------------------------------------------------------------
        // Hàm truy vấn CSDL và hiển thị danh sách phiếu nhập theo điều kiện lọc
        // -------------------------------------------------------------
        private void LoadDanhSachPhieuNhap()
        {
            // Lấy thời điểm bắt đầu: 00:00:00 của "Từ ngày"
            DateTime tuNgay = dtpTuNgay.Value.Date;
            // Lấy thời điểm kết thúc: 23:59:59 của "Đến ngày"
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);

            // Kiểm tra tính hợp lệ của mốc thời gian lọc
            if (tuNgay > denNgay)
            {
                MessageBox.Show("Từ ngày không được lớn hơn đến ngày!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy tên nhà cung cấp được chọn trên giao diện
            string nccChon = cbNhaCungCap.SelectedItem?.ToString() ?? "-- Tất cả nhà cung cấp --";

            try
            {
                using (var db = LayDataContext())
                {
                    var tablePN = db.GetTable<tbl_phieu_nhap>();
                    var tableCT = db.GetTable<tbl_chi_tiet_phieu_nhap>();
                    var tableSach = db.GetTable<tbl_sach>();

                    // 1. Lọc các phiếu nhập nằm trong khoảng ngày quy định (khớp cột ngay_lap_phieu_nhap)
                    var query = tablePN.Where(p => p.ngay_lap_phieu_nhap >= tuNgay && p.ngay_lap_phieu_nhap <= denNgay);

                    // 2. Nếu người dùng chọn một nhà cung cấp cụ thể thì lọc tiếp theo ten_nha_cung_cap
                    if (nccChon != "-- Tất cả nhà cung cấp --" && !string.IsNullOrEmpty(nccChon))
                    {
                        query = query.Where(p => p.ten_nha_cung_cap == nccChon);
                    }

                    // 3. Kết hợp tính tổng tiền từng phiếu nhập và sắp xếp phiếu mới nhất lên đầu
                    var danhSach = (from p in query
                                    orderby p.ngay_lap_phieu_nhap descending
                                    select new
                                    {
                                        MaPN = p.ma_phieu_nhap,
                                        NgayLap = p.ngay_lap_phieu_nhap,
                                        NhaCungCap = p.ten_nha_cung_cap,
                                        // Tính tổng tiền = Tổng (Số lượng x Giá nhập ước tính 70% giá bán lẻ)
                                        TongTien = (from ct in tableCT
                                                    join s in tableSach on ct.ma_sach equals s.ma_sach
                                                    where ct.ma_phieu_nhap == p.ma_phieu_nhap
                                                    select (decimal?)((ct.so_luong ?? 0) * ((s.gia_ban ?? 0) * 0.7m))).Sum() ?? 0
                                    }).ToList();

                    // Đổ dữ liệu đã xử lý lên bảng DataGridView
                    dvgPhieuNhap.DataSource = null;
                    dvgPhieuNhap.DataSource = danhSach;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phiếu nhập: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tự động nạp lại bảng dữ liệu khi người dùng đổi giá trị ở DateTimePicker "Từ ngày"
        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
            if (_dangKhoiTao) return;
            LoadDanhSachPhieuNhap();
        }

        // Tự động nạp lại bảng dữ liệu khi người dùng đổi giá trị ở DateTimePicker "Đến ngày"
        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {
            if (_dangKhoiTao) return;
            LoadDanhSachPhieuNhap();
        }

        // Tự động lọc khi người dùng thay đổi lựa chọn trong ComboBox Nhà Cung Cấp
        private void cbNhaCungCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dangKhoiTao) return;
            LoadDanhSachPhieuNhap();
        }

        // Nút lọc thủ công theo nhà cung cấp
        private void btnLocTheoNCC_Click(object sender, EventArgs e)
        {
            LoadDanhSachPhieuNhap();
        }

        // -------------------------------------------------------------
        // Nút bấm: Tạo phiếu nhập mới (Mở popup nhập nhà cung cấp -> Lưu CSDL -> Mở chi tiết)
        // -------------------------------------------------------------
        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            // Khởi tạo một hộp thoại popup nhỏ trực tiếp bằng mã C# WinForms
            Form modal = new Form()
            {
                Width = 360,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Tạo phiếu nhập mới",
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lblNCC = new Label() { Left = 20, Top = 20, Text = "Nhà cung cấp:", Width = 120 };
            TextBox txtNCC = new TextBox() { Left = 20, Top = 45, Width = 300, Text = "NXB Giáo Dục" };

            Button btnXacNhan = new Button() { Text = "Tạo", Left = 140, Width = 85, Top = 90, DialogResult = DialogResult.OK };
            Button btnHuy = new Button() { Text = "Hủy", Left = 235, Width = 85, Top = 90, DialogResult = DialogResult.Cancel };

            modal.Controls.Add(lblNCC);
            modal.Controls.Add(txtNCC);
            modal.Controls.Add(btnXacNhan);
            modal.Controls.Add(btnHuy);
            modal.AcceptButton = btnXacNhan;
            modal.CancelButton = btnHuy;

            // Nếu người dùng bấm nút "Tạo" trên cửa sổ popup
            if (modal.ShowDialog() == DialogResult.OK)
            {
                string nhaCungCap = string.IsNullOrWhiteSpace(txtNCC.Text) ? "Nhà cung cấp lẻ" : txtNCC.Text.Trim();

                try
                {
                    int maPNMoi = 0;
                    using (var db = LayDataContext())
                    {
                        // Tạo đối tượng phiếu nhập mới gán đúng cột ngay_lap_phieu_nhap và ten_nha_cung_cap
                        tbl_phieu_nhap pn = new tbl_phieu_nhap
                        {
                            ngay_lap_phieu_nhap = DateTime.Now,
                            ten_nha_cung_cap = nhaCungCap
                        };

                        // Chèn bản ghi mới vào CSDL và lưu lại
                        db.GetTable<tbl_phieu_nhap>().InsertOnSubmit(pn);
                        db.SubmitChanges();

                        // Lấy mã phiếu nhập vừa sinh tự động (Identity)
                        maPNMoi = pn.ma_phieu_nhap;
                    }

                    // Tải lại bộ lọc danh sách nhà cung cấp
                    LoadNhaCungCap();
                    LoadDanhSachPhieuNhap();

                    // Mở màn hình chi tiết phiếu nhập để thủ kho nhập sách vào kho
                    frmChiTietPhieuNhap fChiTiet = new frmChiTietPhieuNhap(maPNMoi);
                    fChiTiet.ShowDialog();

                    // Cập nhật lại tổng tiền trên bảng sau khi thủ kho đóng màn hình chi tiết
                    LoadDanhSachPhieuNhap();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tạo phiếu nhập: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // -------------------------------------------------------------
        // Nút bấm: Xem chi tiết phiếu nhập được chọn trên bảng
        // -------------------------------------------------------------
        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã nhấp chọn dòng nào trên DataGridView chưa
            if (dvgPhieuNhap.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một phiếu nhập từ bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy mã phiếu nhập từ ô dòng hiện tại
            var dongHienTai = dvgPhieuNhap.CurrentRow;
            int maPN = Convert.ToInt32(dongHienTai.Cells["colMaPN"]?.Value ?? dongHienTai.Cells[0].Value);

            // Mở form chi tiết và truyền mã phiếu nhập sang constructor
            frmChiTietPhieuNhap fChiTiet = new frmChiTietPhieuNhap(maPN);
            fChiTiet.ShowDialog();

            // Cập nhật lại lưới phiếu nhập sau khi người dùng sửa đổi chi tiết
            LoadDanhSachPhieuNhap();
        }

        // -------------------------------------------------------------
        // Nút bấm: In phiếu nhập bằng ReportViewer (rptPhieuNhap.rdlc)
        // -------------------------------------------------------------
        private void btnInPhieuNhap_Click(object sender, EventArgs e)
        {
            if (dvgPhieuNhap.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập cần in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maPN = Convert.ToInt32(dvgPhieuNhap.CurrentRow.Cells["colMaPN"].Value);
            InBaoCao.InPhieuNhap(maPN, this);
        }

        // Nhấp đúp vào dòng phiếu nhập để mở nhanh chi tiết
        private void dvgPhieuNhap_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnXemChiTiet_Click(sender, e);
            }
        }

        // (Giữ lại vì Designer đã gắn sự kiện này; không làm gì để bấm 1 lần không mở form)
        private void dvgPhieuNhap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
