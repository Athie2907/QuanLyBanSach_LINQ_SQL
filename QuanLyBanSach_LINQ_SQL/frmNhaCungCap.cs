using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyBanSach_LINQ_SQL
{
    public partial class frmNhaCungCap : Form
    {
        // Biến lưu trữ mã nhà cung cấp đang được chọn trên DataGridView
        private int? maNhaCungCapDangChon = null;

        // Khởi tạo DataContext kết nối cơ sở dữ liệu từ cấu hình chuỗi kết nối
        private QuanLyBanSachDataContext LayDataContext()
        {
            return new QuanLyBanSachDataContext(Properties.Settings.Default.db_QuanLyBanSachConnectionString);
        }

        public frmNhaCungCap()
        {
            InitializeComponent();

            // Lưới trong Designer chưa có cột nào (và đã tắt tự sinh cột) nên không hiện gì -> tạo cột bằng code
            LuoiHelper.DatCot(dgvDanhSach,
                new CotLuoi("MaNCC", "Mã NCC", "MaNCC"),
                new CotLuoi("TenNCC", "Tên nhà cung cấp", "TenNCC"),
                new CotLuoi("SDT", "Số điện thoại", "SDT"),
                new CotLuoi("DiaChi", "Địa chỉ", "DiaChi"));

            // Cấu hình hiển thị tiêu đề in đậm và tắt style mặc định của Windows
            dgvDanhSach.EnableHeadersVisualStyles = false;
            dgvDanhSach.ColumnHeadersDefaultCellStyle.Font = new Font(dgvDanhSach.Font, FontStyle.Bold);

            // Dùng CellClick thay vì CellContentClick để click vào bất cứ đâu trên dòng đều nhận
            dgvDanhSach.CellClick += dgvDanhSach_CellContentClick;
        }

        // Sự kiện khi form mở lên: nạp dữ liệu ban đầu và khóa ô mã
        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            txtMa.ReadOnly = true; // Khóa ô nhập mã vì mã là trường tự tăng (Identity)
            LoadDanhSachNhaCungCap();
        }

        // Hàm tải danh sách nhà cung cấp từ bảng tbl_nha_cung_cap lên DataGridView
        private void LoadDanhSachNhaCungCap()
        {
            try
            {
                using (var db = LayDataContext())
                {
                    var list = db.GetTable<tbl_nha_cung_cap>()
                        .Select(x => new
                        {
                            MaNCC = x.ma_nha_cung_cap,
                            TenNCC = x.ten_nha_cung_cap,
                            SDT = x.so_dien_thoai,
                            DiaChi = x.dia_chi
                        })
                        .ToList();

                    dgvDanhSach.DataSource = null;
                    dgvDanhSach.DataSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // NÚT THÊM: Thêm nhà cung cấp mới vào bảng tbl_nha_cung_cap
        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenNCC = txtTen.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();

            // Kiểm tra ràng buộc bắt buộc nhập tên nhà cung cấp
            if (string.IsNullOrWhiteSpace(tenNCC))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }

            try
            {
                using (var db = LayDataContext())
                {
                    var tableNCC = db.GetTable<tbl_nha_cung_cap>();

                    // Kiểm tra xem tên nhà cung cấp đã tồn tại chưa
                    if (tableNCC.Any(x => x.ten_nha_cung_cap == tenNCC))
                    {
                        MessageBox.Show("Nhà cung cấp này đã tồn tại trong danh mục!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Khởi tạo bản ghi mới
                    tbl_nha_cung_cap ncc = new tbl_nha_cung_cap
                    {
                        ten_nha_cung_cap = tenNCC,
                        so_dien_thoai = string.IsNullOrWhiteSpace(sdt) ? null : sdt,
                        dia_chi = string.IsNullOrWhiteSpace(diaChi) ? null : diaChi
                    };

                    tableNCC.InsertOnSubmit(ncc);
                    db.SubmitChanges();
                }

                MessageBox.Show("Thêm nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachNhaCungCap();
                btnLamMoi_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // NÚT SỬA: Cập nhật thông tin của nhà cung cấp đang chọn
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (maNhaCungCapDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp từ bảng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenNCC = txtTen.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenNCC))
            {
                MessageBox.Show("Tên nhà cung cấp không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }

            try
            {
                using (var db = LayDataContext())
                {
                    var tableNCC = db.GetTable<tbl_nha_cung_cap>();
                    var ncc = tableNCC.FirstOrDefault(x => x.ma_nha_cung_cap == maNhaCungCapDangChon.Value);

                    if (ncc == null)
                    {
                        MessageBox.Show("Không tìm thấy thông tin nhà cung cấp cần sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Kiểm tra xem tên mới có bị trùng với nhà cung cấp khác hay không
                    if (tableNCC.Any(x => x.ten_nha_cung_cap == tenNCC && x.ma_nha_cung_cap != maNhaCungCapDangChon.Value))
                    {
                        MessageBox.Show("Tên nhà cung cấp này đã bị trùng với một đơn vị khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Gán thông tin cập nhật
                    ncc.ten_nha_cung_cap = tenNCC;
                    ncc.so_dien_thoai = string.IsNullOrWhiteSpace(sdt) ? null : sdt;
                    ncc.dia_chi = string.IsNullOrWhiteSpace(diaChi) ? null : diaChi;

                    db.SubmitChanges();
                }

                MessageBox.Show("Cập nhật thông tin nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachNhaCungCap();
                btnLamMoi_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // NÚT XÓA: Xóa nhà cung cấp khỏi CSDL
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (maNhaCungCapDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần xóa từ bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa nhà cung cấp này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.No) return;

            try
            {
                using (var db = LayDataContext())
                {
                    var tableNCC = db.GetTable<tbl_nha_cung_cap>();
                    var ncc = tableNCC.FirstOrDefault(x => x.ma_nha_cung_cap == maNhaCungCapDangChon.Value);

                    if (ncc != null)
                    {
                        // Kiểm tra xem nhà cung cấp này đã từng lập phiếu nhập nào chưa
                        bool daCoPhieuNhap = db.GetTable<tbl_phieu_nhap>()
                            .Any(p => p.ten_nha_cung_cap == ncc.ten_nha_cung_cap);

                        if (daCoPhieuNhap)
                        {
                            MessageBox.Show("Không thể xóa nhà cung cấp này vì đã có phiếu nhập hàng liên kết trong hệ thống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        tableNCC.DeleteOnSubmit(ncc);
                        db.SubmitChanges();
                    }
                }

                MessageBox.Show("Xóa nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachNhaCungCap();
                btnLamMoi_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // NÚT LÀM MỚI: Xóa trắng toàn bộ các ô nhập liệu
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            maNhaCungCapDangChon = null;
            txtMa.Clear();
            txtTen.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            dgvDanhSach.ClearSelection();
            txtTen.Focus();
        }

        // Sự kiện click chọn dòng trên DataGridView: Đẩy thông tin ngược lên các ô nhập liệu
        private void dgvDanhSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvDanhSach.Rows[e.RowIndex];

            // 1. Lấy mã nhà cung cấp
            object valMa = row.Cells["MaNCC"]?.Value ?? row.Cells[0].Value;
            if (valMa != null && valMa != DBNull.Value)
            {
                maNhaCungCapDangChon = Convert.ToInt32(valMa);
                txtMa.Text = maNhaCungCapDangChon.ToString();
            }

            // 2. Lấy tên nhà cung cấp
            object valTen = row.Cells["TenNCC"]?.Value ?? (row.Cells.Count > 1 ? row.Cells[1].Value : null);
            txtTen.Text = valTen?.ToString() ?? "";

            // 3. Lấy số điện thoại
            object valSDT = row.Cells["SDT"]?.Value ?? (row.Cells.Count > 2 ? row.Cells[2].Value : null);
            txtSDT.Text = valSDT?.ToString() ?? "";

            // 4. Lấy địa chỉ
            object valDiaChi = row.Cells["DiaChi"]?.Value ?? (row.Cells.Count > 3 ? row.Cells[3].Value : null);
            txtDiaChi.Text = valDiaChi?.ToString() ?? "";
        }

        private void txtMa_TextChanged(object sender, EventArgs e) { }
        private void txtTen_TextChanged(object sender, EventArgs e) { }
        private void txtSDT_TextChanged(object sender, EventArgs e) { }
        private void txtDiaChi_TextChanged(object sender, EventArgs e) { }
    }
}