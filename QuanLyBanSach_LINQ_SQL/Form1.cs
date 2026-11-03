
using System;
using System.Linq;
using System.Windows.Forms;
namespace QuanLyBanSach_LINQ_SQL
{
    public partial class Form1 : Form
    {
        private int? maLoaiSachDangChon = null;
        private int? maSachDangChon = null;
        private int? maHoaDonDangChon = null;
        private int? maPhieuNhapDangChon = null;

        public Form1()
        {
            InitializeComponent();

            KiemTraKetNoi();
            // Gắn sự kiện cho các nút Loại Sách
            btnLoaiSachThem.Click += btnLoaiSachThem_Click;
            btnLoaiSachSua.Click += btnLoaiSachSua_Click;
            btnLoaiSachXoa.Click += btnLoaiSachXoa_Click;

            // Gắn sự kiện chọn dòng
            dgLoaiSach.CellClick += dataGridView1_CellClick;

            // Cấu hình bảng
            dgLoaiSach.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgLoaiSach.ReadOnly = true;
            dgLoaiSach.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgLoaiSach.MultiSelect = false;

            // Hiển thị danh sách loại sách
            LoadLoaiSach();

            // Gắn sự kiện cho các nút Sách
            btnSachThem.Click += btnSachThem_Click;
            btnSachSua.Click += btnSachSua_Click;
            btnSachXoa.Click += btnSachXoa_Click;

            // Gắn sự kiện chọn dòng sách
            dgSach.CellClick += dgSach_CellClick;

            // Cấu hình bảng sách
            dgSach.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgSach.ReadOnly = true;

            dgSach.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgSach.MultiSelect = false;

            // Tải dữ liệu sách và loại sách
            LoadLoaiSachComboBox();
            LoadSach();


            btnHoaDonThem.Click += btnHoaDonThem_Click;
            btnHoaDonSua.Click += btnHoaDonSua_Click;
            btnHoaDonXoa.Click += btnHoaDonXoa_Click;
            dgHoaDon.CellClick += dgHoaDon_CellClick;

            dgHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgHoaDon.ReadOnly = true;
            dgHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgHoaDon.MultiSelect = false;

            LoadHoaDon();


            btnPhieuNhapThem.Click += btnPhieuNhapThem_Click;
            btnPhieuNhapSua.Click += btnPhieuNhapSua_Click;
            btnPhieuNhapXoa.Click += btnPhieuNhapXoa_Click;
            dgPhieuNhap.CellClick += dgPhieuNhap_CellClick;

            dgPhieuNhap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgPhieuNhap.ReadOnly = true;
            dgPhieuNhap.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgPhieuNhap.MultiSelect = false;

            LoadPhieuNhap();
        }

        private void KiemTraKetNoi()
        {
            try
            {
                using (QuanLyBanSachDataContext db =
                    new QuanLyBanSachDataContext())
                {
                    db.Connection.Open();

                    MessageBox.Show(
                        "Kết nối SQL Server thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    db.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        // Hiển thị danh sách loại sách
        private void LoadLoaiSach()
        {
            try
            {
                using (QuanLyBanSachDataContext db =
                    new QuanLyBanSachDataContext())
                {
                    var danhSach = db.tbl_loai_saches
                        .Select(x => new
                        {
                            x.ma_loai_sach,
                            x.ten_loai_sach
                        })
                        .ToList();

                    dgLoaiSach.DataSource = danhSach;

                    dgLoaiSach.Columns["ma_loai_sach"]
                        .HeaderText = "Mã Loại Sách";

                    dgLoaiSach.Columns["ten_loai_sach"]
                        .HeaderText = "Tên Loại Sách";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }

        // Chọn một dòng trong DataGridView
        private void dataGridView1_CellClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row =
                    dgLoaiSach.Rows[e.RowIndex];

                maLoaiSachDangChon = Convert.ToInt32(
                    row.Cells["MaLoaiSach"].Value
                );

                txtLoaiSachTen.Text =
                    row.Cells["TenLoaiSach"].Value?.ToString();
            }
        }

        // Thêm loại sách
        private void btnLoaiSachThem_Click(
    object sender, EventArgs e)
        {
            string tenLoaiSach = txtLoaiSachTen.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenLoaiSach))
            {
                MessageBox.Show("Vui lòng nhập tên loại sách!");
                txtLoaiSachTen.Focus();
                return;
            }

            try
            {
                using (QuanLyBanSachDataContext db =
                    new QuanLyBanSachDataContext())
                {
                    bool daTonTai = db.tbl_loai_saches
                        .Any(x => x.ten_loai_sach == tenLoaiSach);

                    if (daTonTai)
                    {
                        MessageBox.Show("Loại sách này đã tồn tại!");
                        return;
                    }

                    tbl_loai_sach loaiSach = new tbl_loai_sach
                    {
                        ten_loai_sach = tenLoaiSach
                    };

                    db.tbl_loai_saches.InsertOnSubmit(loaiSach);
                    db.SubmitChanges();
                }

                MessageBox.Show("Thêm loại sách thành công!");

                LoadLoaiSach();
                LamMoiLoaiSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm loại sách: " + ex.Message);
            }
        }

        // Sửa loại sách
        private void btnLoaiSachSua_Click(
    object sender, EventArgs e)
        {
            if (maLoaiSachDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn loại sách cần sửa!");
                return;
            }

            string tenLoaiSach = txtLoaiSachTen.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenLoaiSach))
            {
                MessageBox.Show("Vui lòng nhập tên loại sách!");
                return;
            }

            try
            {
                using (QuanLyBanSachDataContext db =
                    new QuanLyBanSachDataContext())
                {
                    var loaiSach = db.tbl_loai_saches
                        .FirstOrDefault(x =>
                            x.ma_loai_sach == maLoaiSachDangChon.Value);

                    if (loaiSach == null)
                    {
                        MessageBox.Show("Không tìm thấy loại sách!");
                        return;
                    }

                    bool trungTen = db.tbl_loai_saches.Any(x =>
                        x.ten_loai_sach == tenLoaiSach &&
                        x.ma_loai_sach != maLoaiSachDangChon.Value);

                    if (trungTen)
                    {
                        MessageBox.Show("Tên loại sách đã tồn tại!");
                        return;
                    }

                    loaiSach.ten_loai_sach = tenLoaiSach;

                    db.SubmitChanges();
                }

                MessageBox.Show("Sửa loại sách thành công!");

                LoadLoaiSach();
                LamMoiLoaiSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa loại sách: " + ex.Message);
            }
        }

        // Xóa loại sách
        private void btnLoaiSachXoa_Click(
    object sender, EventArgs e)
        {
            if (maLoaiSachDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn loại sách cần xóa!");
                return;
            }

            DialogResult ketQua = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa loại sách này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (ketQua == DialogResult.No)
            {
                return;
            }

            try
            {
                using (QuanLyBanSachDataContext db =
                    new QuanLyBanSachDataContext())
                {
                    var loaiSach = db.tbl_loai_saches
                        .FirstOrDefault(x =>
                            x.ma_loai_sach == maLoaiSachDangChon.Value);

                    if (loaiSach == null)
                    {
                        MessageBox.Show("Không tìm thấy loại sách!");
                        return;
                    }

                    db.tbl_loai_saches.DeleteOnSubmit(loaiSach);
                    db.SubmitChanges();
                }

                MessageBox.Show("Xóa loại sách thành công!");

                LoadLoaiSach();
                LamMoiLoaiSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể xóa loại sách này. Có thể đang có sách thuộc loại sách đó!\n" +
                    "Chi tiết: " + ex.Message
                );
            }
        }

        // Làm trống ô nhập liệu
        private void LamMoiLoaiSach()
        {
            txtLoaiSachTen.Clear();
            maLoaiSachDangChon = null;
            dgLoaiSach.ClearSelection();
        }

        private void LoadLoaiSachComboBox()
        {
            try
            {
                using (QuanLyBanSachDataContext db =
                    new QuanLyBanSachDataContext())
                {
                    var danhSach = db.tbl_loai_saches
                        .OrderBy(x => x.ten_loai_sach)
                        .ToList();

                    cbSachLoaiSach.DataSource = danhSach;
                    cbSachLoaiSach.DisplayMember = "ten_loai_sach";
                    cbSachLoaiSach.ValueMember = "ma_loai_sach";
                    cbSachLoaiSach.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải loại sách: " + ex.Message
                );
            }
        }
        private void LoadSach()
        {
            try
            {
                using (QuanLyBanSachDataContext db =
                    new QuanLyBanSachDataContext())
                {
                    var danhSach = db.tbl_saches
                        .Select(x => new
                        {
                            x.ma_sach,
                            x.ten_sach,

                            TenLoaiSach = x.tbl_loai_sach != null
                                ? x.tbl_loai_sach.ten_loai_sach
                                : "Chưa phân loại",

                            x.tac_gia,
                            x.so_luong,
                            x.gia_ban
                        })
                        .ToList();

                    dgSach.DataSource = danhSach;

                    dgSach.Columns["ma_sach"].HeaderText = "Mã Sách";
                    dgSach.Columns["ten_sach"].HeaderText = "Tên Sách";
                    dgSach.Columns["TenLoaiSach"].HeaderText = "Loại Sách";
                    dgSach.Columns["tac_gia"].HeaderText = "Tác Giả";
                    dgSach.Columns["so_luong"].HeaderText = "Số Lượng";
                    dgSach.Columns["gia_ban"].HeaderText = "Giá Bán";

                    dgSach.Columns["gia_ban"].DefaultCellStyle.Format = "N0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải danh sách sách: " + ex.Message
                );
            }
        }

        private void dgSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgSach.Rows[e.RowIndex];

            maSachDangChon = Convert.ToInt32(
                row.Cells["ma_sach"].Value
            );

            txtSachTenSach.Text =
                row.Cells["ten_sach"].Value?.ToString() ?? "";

            txtSachTacGia.Text =
                row.Cells["tac_gia"].Value?.ToString() ?? "";

            // Lấy số lượng và giới hạn giá trị phù hợp với NumericUpDown
            numSachSoLuong.Value =
                row.Cells["so_luong"].Value == null ||
                row.Cells["so_luong"].Value == DBNull.Value
                ? 0
                : Math.Min(
                    Math.Max(
                        Convert.ToDecimal(row.Cells["so_luong"].Value),
                        numSachSoLuong.Minimum
                    ),
                    numSachSoLuong.Maximum
                );

            // Lấy giá bán và giới hạn giá trị phù hợp với NumericUpDown
            numSachGiaBan.Value =
                row.Cells["gia_ban"].Value == null ||
                row.Cells["gia_ban"].Value == DBNull.Value
                ? 0
                : Math.Min(
                    Math.Max(
                        Convert.ToDecimal(row.Cells["gia_ban"].Value),
                        numSachGiaBan.Minimum
                    ),
                    numSachGiaBan.Maximum
                );

            // Lấy mã loại sách của sách đang chọn
            using (QuanLyBanSachDataContext db =
                new QuanLyBanSachDataContext())
            {
                var maLoai = db.tbl_saches
                    .Where(x => x.ma_sach == maSachDangChon.Value)
                    .Select(x => x.ma_loai_sach)
                    .FirstOrDefault();

                cbSachLoaiSach.SelectedValue = maLoai;

                if (maLoai == null)
                    cbSachLoaiSach.SelectedIndex = -1;
            }
        }

        private void btnSachThem_Click(object sender, EventArgs e)
        {
            string tenSach = txtSachTenSach.Text.Trim();
            string tacGia = txtSachTacGia.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenSach))
            {
                MessageBox.Show("Vui lòng nhập tên sách!");
                txtSachTenSach.Focus();
                return;
            }

            try
            {
                using (QuanLyBanSachDataContext db =
                    new QuanLyBanSachDataContext())
                {
                    tbl_sach sach = new tbl_sach
                    {
                        ten_sach = tenSach,

                        tac_gia = string.IsNullOrWhiteSpace(tacGia)
                            ? null : tacGia,

                        ma_loai_sach = cbSachLoaiSach.SelectedValue == null ? (int?)null 
                        : Convert.ToInt32(cbSachLoaiSach.SelectedValue),

                        so_luong = Convert.ToInt32(numSachSoLuong.Value),

                        gia_ban = numSachGiaBan.Value
                    };

                    db.tbl_saches.InsertOnSubmit(sach);
                    db.SubmitChanges();
                }

                MessageBox.Show("Thêm sách thành công!");

                LoadSach();
                LoadLoaiSachComboBox();
                LamMoiSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm sách: " + ex.Message);
            }
        }

        private void btnSachSua_Click(object sender, EventArgs e)
        {
            if (maSachDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn sách cần sửa!");
                return;
            }

            string tenSach = txtSachTenSach.Text.Trim();
            string tacGia = txtSachTacGia.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenSach))
            {
                MessageBox.Show("Vui lòng nhập tên sách!");
                txtSachTenSach.Focus();
                return;
            }

            try
            {
                using (QuanLyBanSachDataContext db =
                    new QuanLyBanSachDataContext())
                {
                    var sach = db.tbl_saches
                        .FirstOrDefault(x =>
                            x.ma_sach == maSachDangChon.Value);

                    if (sach == null)
                    {
                        MessageBox.Show("Không tìm thấy sách!");
                        return;
                    }

                    sach.ten_sach = tenSach;

                    sach.tac_gia = string.IsNullOrWhiteSpace(tacGia)
                        ? null : tacGia;

                    sach.ma_loai_sach = cbSachLoaiSach.SelectedValue == null ? (int?)null 
                        : Convert.ToInt32(cbSachLoaiSach.SelectedValue);

                    sach.so_luong = Convert.ToInt32(numSachSoLuong.Value);

                    sach.gia_ban = numSachGiaBan.Value;

                    db.SubmitChanges();
                }

                MessageBox.Show("Cập nhật sách thành công!");

                LoadSach();
                LoadLoaiSachComboBox();
                LamMoiSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa sách: " + ex.Message);
            }
        }

        private void btnSachXoa_Click(object sender, EventArgs e)
        {
            if (maSachDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn sách cần xóa!");
                return;
            }

            DialogResult ketQua = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa sách này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (ketQua == DialogResult.No)
            {
                return;
            }

            try
            {
                using (QuanLyBanSachDataContext db =
                    new QuanLyBanSachDataContext())
                {
                    // Kiểm tra sách đã xuất hiện trong hóa đơn chưa
                    bool coTrongHoaDon = db.tbl_chi_tiet_hoa_dons
                        .Any(x => x.ma_sach == maSachDangChon.Value);

                    // Kiểm tra sách đã xuất hiện trong phiếu nhập chưa
                    bool coTrongPhieuNhap = db.tbl_chi_tiet_phieu_nhaps
                        .Any(x => x.ma_sach == maSachDangChon.Value);

                    if (coTrongHoaDon || coTrongPhieuNhap)
                    {
                        MessageBox.Show(
                            "Không thể xóa sách này vì sách đã có trong hóa đơn hoặc phiếu nhập!"
                        );
                        return;
                    }

                    var sach = db.tbl_saches
                        .FirstOrDefault(x =>
                            x.ma_sach == maSachDangChon.Value);

                    if (sach == null)
                    {
                        MessageBox.Show("Không tìm thấy sách!");
                        return;
                    }

                    db.tbl_saches.DeleteOnSubmit(sach);
                    db.SubmitChanges();
                }

                MessageBox.Show("Xóa sách thành công!");

                LoadSach();
                LamMoiSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa sách: " + ex.Message);
            }
        }

        private void LamMoiSach()
        {
            txtSachTenSach.Clear();
            txtSachTacGia.Clear();

            cbSachLoaiSach.SelectedIndex = -1;

            numSachSoLuong.Value = 0;
            numSachGiaBan.Value = 0;

            maSachDangChon = null;

            dgSach.ClearSelection();
        }

        private void LoadHoaDon()
        {
            using (QuanLyBanSachDataContext db = new QuanLyBanSachDataContext())
            {
                var danhSach = db.tbl_hoa_dons
                    .Select(x => new
                    {
                        MaHoaDon = x.ma_hoa_don,
                        NgayLapHoaDon = x.ngay_lap_hoa_don,
                        TenKhachHang = x.ten_khach_hang,
                        SdtKhachHang = x.sdt_khach_hang
                    })
                    .ToList();

                dgHoaDon.DataSource = danhSach;

                dgHoaDon.Columns["MaHoaDon"].HeaderText = "Mã hóa đơn";
                dgHoaDon.Columns["NgayLapHoaDon"].HeaderText = "Ngày lập";
                dgHoaDon.Columns["TenKhachHang"].HeaderText = "Tên khách hàng";
                dgHoaDon.Columns["SdtKhachHang"].HeaderText = "Số điện thoại";
            }
        }

        private void dgHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgHoaDon.Rows[e.RowIndex];

            maHoaDonDangChon = Convert.ToInt32(row.Cells["MaHoaDon"].Value);

            txtHoaDonTenKH.Text = row.Cells["TenKhachHang"].Value?.ToString() ?? "";
            txtHoaDonSDTKH.Text = row.Cells["SdtKhachHang"].Value?.ToString() ?? "";

            if (row.Cells["NgayLapHoaDon"].Value != null &&
                row.Cells["NgayLapHoaDon"].Value != DBNull.Value)
            {
                dateNgayLapHoaDon.Value =
                    Convert.ToDateTime(row.Cells["NgayLapHoaDon"].Value);
            }
        }

        private void btnHoaDonThem_Click(object sender, EventArgs e)
        {
            using (QuanLyBanSachDataContext db = new QuanLyBanSachDataContext())
            {
                tbl_hoa_don hoaDon = new tbl_hoa_don
                {
                    ngay_lap_hoa_don = dateNgayLapHoaDon.Value.Date,

                    ten_khach_hang = string.IsNullOrWhiteSpace(txtHoaDonTenKH.Text)
                        ? null
                        : txtHoaDonTenKH.Text.Trim(),

                    sdt_khach_hang = string.IsNullOrWhiteSpace(txtHoaDonSDTKH.Text)
                        ? null
                        : txtHoaDonSDTKH.Text.Trim()
                };

                db.tbl_hoa_dons.InsertOnSubmit(hoaDon);
                db.SubmitChanges();
            }

            MessageBox.Show("Thêm hóa đơn thành công!");

            LoadHoaDon();
            LamMoiHoaDon();
        }

        private void btnHoaDonSua_Click(object sender, EventArgs e)
        {
            if (maHoaDonDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần sửa!");
                return;
            }

            using (QuanLyBanSachDataContext db = new QuanLyBanSachDataContext())
            {
                var hoaDon = db.tbl_hoa_dons
                    .FirstOrDefault(x => x.ma_hoa_don == maHoaDonDangChon.Value);

                if (hoaDon == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn!");
                    return;
                }

                hoaDon.ngay_lap_hoa_don = dateNgayLapHoaDon.Value.Date;

                hoaDon.ten_khach_hang = string.IsNullOrWhiteSpace(txtHoaDonTenKH.Text)
                    ? null
                    : txtHoaDonTenKH.Text.Trim();

                hoaDon.sdt_khach_hang = string.IsNullOrWhiteSpace(txtHoaDonSDTKH.Text)
                    ? null
                    : txtHoaDonSDTKH.Text.Trim();

                db.SubmitChanges();
            }

            MessageBox.Show("Cập nhật hóa đơn thành công!");

            LoadHoaDon();
            LamMoiHoaDon();
        }
        private void btnHoaDonXoa_Click(object sender, EventArgs e)
        {
            if (maHoaDonDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa!");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa hóa đơn này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
                return;

            using (QuanLyBanSachDataContext db = new QuanLyBanSachDataContext())
            {
                bool coChiTiet = db.tbl_chi_tiet_hoa_dons
                    .Any(x => x.ma_hoa_don == maHoaDonDangChon.Value);

                if (coChiTiet)
                {
                    MessageBox.Show(
                        "Không thể xóa hóa đơn đã có chi tiết hóa đơn!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                var hoaDon = db.tbl_hoa_dons
                    .FirstOrDefault(x => x.ma_hoa_don == maHoaDonDangChon.Value);

                if (hoaDon != null)
                {
                    db.tbl_hoa_dons.DeleteOnSubmit(hoaDon);
                    db.SubmitChanges();
                }
            }

            MessageBox.Show("Xóa hóa đơn thành công!");

            LoadHoaDon();
            LamMoiHoaDon();
        }
        private void LamMoiHoaDon()
        {
            maHoaDonDangChon = null;

            txtHoaDonTenKH.Clear();
            txtHoaDonSDTKH.Clear();

            dateNgayLapHoaDon.Value = DateTime.Today;

            dgHoaDon.ClearSelection();
        }

        private void LoadPhieuNhap()
        {
            using (QuanLyBanSachDataContext db = new QuanLyBanSachDataContext())
            {
                var danhSach = db.tbl_phieu_nhaps
                    .Select(x => new
                    {
                        MaPhieuNhap = x.ma_phieu_nhap,
                        NgayLapPhieuNhap = x.ngay_lap_phieu_nhap,
                        TenNhaCungCap = x.ten_nha_cung_cap
                    })
                    .ToList();

                dgPhieuNhap.DataSource = danhSach;

                dgPhieuNhap.Columns["MaPhieuNhap"].HeaderText = "Mã phiếu nhập";
                dgPhieuNhap.Columns["NgayLapPhieuNhap"].HeaderText = "Ngày lập phiếu";
                dgPhieuNhap.Columns["TenNhaCungCap"].HeaderText = "Nhà cung cấp";
            }
        }

        private void dgPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgPhieuNhap.Rows[e.RowIndex];

            maPhieuNhapDangChon = Convert.ToInt32(
                row.Cells["MaPhieuNhap"].Value
            );

            txtPhieuNhapNhaCungCap.Text =
                row.Cells["TenNhaCungCap"].Value?.ToString() ?? "";

            if (row.Cells["NgayLapPhieuNhap"].Value != null &&
                row.Cells["NgayLapPhieuNhap"].Value != DBNull.Value)
            {
                dateNgayLapPhieu.Value = Convert.ToDateTime(
                    row.Cells["NgayLapPhieuNhap"].Value
                );
            }
        }

        private void btnPhieuNhapThem_Click(object sender, EventArgs e)
        {
            using (QuanLyBanSachDataContext db = new QuanLyBanSachDataContext())
            {
                tbl_phieu_nhap phieuNhap = new tbl_phieu_nhap
                {
                    ngay_lap_phieu_nhap = dateNgayLapPhieu.Value.Date,

                    ten_nha_cung_cap = string.IsNullOrWhiteSpace(
                        txtPhieuNhapNhaCungCap.Text)
                        ? null
                        : txtPhieuNhapNhaCungCap.Text.Trim()
                };

                db.tbl_phieu_nhaps.InsertOnSubmit(phieuNhap);
                db.SubmitChanges();
            }

            MessageBox.Show("Thêm phiếu nhập thành công!");

            LoadPhieuNhap();
            LamMoiPhieuNhap();
        }

        private void btnPhieuNhapSua_Click(object sender, EventArgs e)
        {
            if (maPhieuNhapDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập cần sửa!");
                return;
            }

            using (QuanLyBanSachDataContext db = new QuanLyBanSachDataContext())
            {
                var phieuNhap = db.tbl_phieu_nhaps
                    .FirstOrDefault(x => x.ma_phieu_nhap == maPhieuNhapDangChon.Value);

                if (phieuNhap == null)
                {
                    MessageBox.Show("Không tìm thấy phiếu nhập!");
                    return;
                }

                phieuNhap.ngay_lap_phieu_nhap = dateNgayLapPhieu.Value.Date;

                phieuNhap.ten_nha_cung_cap = string.IsNullOrWhiteSpace(
                    txtPhieuNhapNhaCungCap.Text)
                    ? null
                    : txtPhieuNhapNhaCungCap.Text.Trim();

                db.SubmitChanges();
            }

            MessageBox.Show("Cập nhật phiếu nhập thành công!");

            LoadPhieuNhap();
            LamMoiPhieuNhap();
        }

        private void btnPhieuNhapXoa_Click(object sender, EventArgs e)
        {
            if (maPhieuNhapDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập cần xóa!");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa phiếu nhập này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
                return;

            using (QuanLyBanSachDataContext db = new QuanLyBanSachDataContext())
            {
                bool coChiTiet = db.tbl_chi_tiet_phieu_nhaps
                    .Any(x => x.ma_phieu_nhap == maPhieuNhapDangChon.Value);

                if (coChiTiet)
                {
                    MessageBox.Show(
                        "Không thể xóa phiếu nhập đã có chi tiết phiếu nhập!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                var phieuNhap = db.tbl_phieu_nhaps
                    .FirstOrDefault(x => x.ma_phieu_nhap == maPhieuNhapDangChon.Value);

                if (phieuNhap != null)
                {
                    db.tbl_phieu_nhaps.DeleteOnSubmit(phieuNhap);
                    db.SubmitChanges();
                }
            }

            MessageBox.Show("Xóa phiếu nhập thành công!");

            LoadPhieuNhap();
            LamMoiPhieuNhap();
        }
        private void LamMoiPhieuNhap()
        {
            maPhieuNhapDangChon = null;

            txtPhieuNhapNhaCungCap.Clear();
            dateNgayLapPhieu.Value = DateTime.Today;

            dgPhieuNhap.ClearSelection();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
