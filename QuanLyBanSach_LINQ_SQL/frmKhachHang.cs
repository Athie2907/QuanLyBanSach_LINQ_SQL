using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyBanSach_LINQ_SQL
{
    public partial class frmKhachHang : Form
    {
        // Biến lưu số điện thoại hoặc mã của khách hàng đang chọn
        private string sdtKhachHangDangChon = null;

        // Khởi tạo DataContext kết nối cơ sở dữ liệu
        private QuanLyBanSachDataContext LayDataContext()
        {
            return new QuanLyBanSachDataContext(Properties.Settings.Default.db_QuanLyBanSachConnectionString);
        }

        public frmKhachHang()
        {
            InitializeComponent();

            // Lưới trong Designer chưa có cột nào (và đã tắt tự sinh cột) nên không hiện gì -> tạo cột bằng code
            LuoiHelper.DatCot(dgvDanhSach,
                new CotLuoi("STT", "STT", "STT"),
                new CotLuoi("TenKH", "Tên khách hàng", "TenKH"),
                new CotLuoi("SDT", "Số điện thoại", "SDT"),
                new CotLuoi("DiaChi", "Địa chỉ", "DiaChi"));
            dgvDanhSach.ColumnHeadersDefaultCellStyle.Font = new Font(dgvDanhSach.Font, FontStyle.Bold);

            // Sử dụng CellClick thay vì CellContentClick để bấm vào bất kỳ vị trí nào trên dòng cũng nhận
            dgvDanhSach.CellClick += dgvDanhSach_CellContentClick;
        }

        // Sự kiện khi form mở lên
        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            txtMa.ReadOnly = true; // Khóa ô mã (chỉ hiển thị STT)
            LoadDanhSachKhachHang();
        }

        // Tải danh sách khách hàng lên DataGridView
        private void LoadDanhSachKhachHang()
        {
            try
            {
                using (var db = LayDataContext())
                {
                    // Đã sửa lỗi: Thay string.IsNullOrEmpty bằng biểu thức so sánh phù hợp với LINQ to SQL
                    var list = db.GetTable<tbl_hoa_don>()
                        .Where(h => h.ten_khach_hang != null && h.ten_khach_hang != "")
                        .Select(h => new
                        {
                            TenKH = h.ten_khach_hang,
                            SDT = h.sdt_khach_hang ?? "Chưa có"
                        })
                        .Distinct()
                        .AsEnumerable()
                        .Select((kh, index) => new
                        {
                            STT = index + 1,
                            TenKH = kh.TenKH,
                            SDT = kh.SDT,
                            DiaChi = "TP. Hồ Chí Minh" // Địa chỉ mặc định nếu chưa có cột trong CSDL
                        })
                        .ToList();

                    dgvDanhSach.DataSource = null;
                    dgvDanhSach.DataSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút THÊM KHÁCH HÀNG (button1)
        private void button1_Click(object sender, EventArgs e)
        {
            string tenKH = txtTen.Text.Trim();
            string sdt = txtSDT.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenKH))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }

            try
            {
                using (var db = LayDataContext())
                {
                    var tableHD = db.GetTable<tbl_hoa_don>();

                    // Kiểm tra xem khách hàng này đã có trong danh sách chưa
                    bool daTonTai = tableHD.Any(x => x.ten_khach_hang == tenKH && x.sdt_khach_hang == sdt);
                    if (daTonTai)
                    {
                        MessageBox.Show("Khách hàng này đã tồn tại trong danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Tạo một bản ghi hóa đơn ban đầu lưu thông tin khách
                    tbl_hoa_don hdMoi = new tbl_hoa_don
                    {
                        ngay_lap_hoa_don = DateTime.Now,
                        ten_khach_hang = tenKH,
                        sdt_khach_hang = string.IsNullOrWhiteSpace(sdt) ? null : sdt
                    };

                    tableHD.InsertOnSubmit(hdMoi);
                    db.SubmitChanges();
                }

                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachKhachHang();
                btnLamMoi_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // NÚT SỬA: Cập nhật lại tên và SĐT của khách hàng trên các hóa đơn
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(sdtKhachHangDangChon) && string.IsNullOrEmpty(txtTen.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa từ bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenMoi = txtTen.Text.Trim();
            string sdtMoi = txtSDT.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenMoi))
            {
                MessageBox.Show("Tên khách hàng không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }

            try
            {
                using (var db = LayDataContext())
                {
                    string tenCu = txtTen.Tag == null ? "" : txtTen.Tag.ToString();
                    string sdtCu = sdtKhachHangDangChon ?? "";

                    var danhSachHD = db.GetTable<tbl_hoa_don>()
                        .Where(x => x.ten_khach_hang == tenCu && (x.sdt_khach_hang ?? "") == sdtCu)
                        .ToList();

                    if (danhSachHD.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy thông tin khách hàng cần sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    foreach (var hd in danhSachHD)
                    {
                        hd.ten_khach_hang = tenMoi;
                        hd.sdt_khach_hang = string.IsNullOrWhiteSpace(sdtMoi) ? null : sdtMoi;
                    }

                    db.SubmitChanges();
                }

                MessageBox.Show("Cập nhật thông tin khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachKhachHang();
                btnLamMoi_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // NÚT XÓA: Xóa thông tin khách hàng
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTen.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa từ bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenXoa = txtTen.Text.Trim();

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa khách hàng '{tenXoa}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (var db = LayDataContext())
                    {
                        var hoaDons = db.GetTable<tbl_hoa_don>()
                            .Where(x => x.ten_khach_hang == tenXoa)
                            .ToList();

                        var tableCT = db.GetTable<tbl_chi_tiet_hoa_don>();

                        bool daMuaHang = hoaDons.Any(hd => tableCT.Any(ct => ct.ma_hoa_don == hd.ma_hoa_don));
                        if (daMuaHang)
                        {
                            MessageBox.Show("Không thể xóa khách hàng này vì đã có lịch sử đơn hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        db.GetTable<tbl_hoa_don>().DeleteAllOnSubmit(hoaDons);
                        db.SubmitChanges();
                    }

                    MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachKhachHang();
                    btnLamMoi_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // NÚT LÀM MỚI: Xóa trắng các ô nhập
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            sdtKhachHangDangChon = null;
            txtMa.Clear();
            txtTen.Clear();
            txtTen.Tag = null;
            txtSDT.Clear();
            txtDiaChi.Clear();
            dgvDanhSach.ClearSelection();
            txtTen.Focus();
        }

        // Sự kiện click chọn dòng trên DataGridView
        private void dgvDanhSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvDanhSach.Rows[e.RowIndex];

            object valMa = row.Cells["STT"]?.Value ?? row.Cells[0].Value;
            txtMa.Text = valMa?.ToString() ?? "";

            object valTen = row.Cells["TenKH"]?.Value ?? (row.Cells.Count > 1 ? row.Cells[1].Value : null);
            txtTen.Text = valTen?.ToString() ?? "";
            txtTen.Tag = txtTen.Text;

            object valSDT = row.Cells["SDT"]?.Value ?? (row.Cells.Count > 2 ? row.Cells[2].Value : null);
            txtSDT.Text = valSDT?.ToString() == "Chưa có" ? "" : valSDT?.ToString() ?? "";
            sdtKhachHangDangChon = txtSDT.Text;

            object valDiaChi = row.Cells["DiaChi"]?.Value ?? (row.Cells.Count > 3 ? row.Cells[3].Value : null);
            txtDiaChi.Text = valDiaChi?.ToString() ?? "";
        }

        private void txtMa_TextChanged(object sender, EventArgs e) { }
        private void txtTen_TextChanged(object sender, EventArgs e) { }
        private void txtSDT_TextChanged(object sender, EventArgs e) { }
        private void txtDiaChi_TextChanged(object sender, EventArgs e) { }
    }
}