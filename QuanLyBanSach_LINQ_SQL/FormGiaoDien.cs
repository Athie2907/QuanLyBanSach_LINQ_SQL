using System;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;

namespace QuanLyBanSach_LINQ_SQL
{
    public partial class FormGiaoDien : Form
    {
        private int? maLoaiSachDangChon = null;
        private int? maSachDangChon = null;
        private int? maHoaDonDangChon = null;
        private int? maPhieuNhapDangChon = null;

        // Biến lưu mã sách đang chọn trong bảng chi tiết
        private int? maSachChiTietHoaDonDangChon = null;
        private int? maSachChiTietPhieuNhapDangChon = null;

        public string TenDangNhapHienTai { get; set; }

        // Kiểm tra tài khoản hiện tại có được cấp chức năng hay không
        // Kiểm tra tài khoản hiện tại có được cấp chức năng hay không
        private bool KiemTraQuyen(string maChucNang)
        {
            try
            {
                using (QuanLyBanSachDataContext db =
                    new QuanLyBanSachDataContext())
                {
                    var taiKhoan = db.tbl_nguoi_dungs
                        .FirstOrDefault(x =>
                            x.ten_dang_nhap == TenDangNhapHienTai);

                    if (taiKhoan == null || !taiKhoan.trang_thai)
                        return false;

                    int maPhanQuyen = taiKhoan.ma_phan_quyen;

                    return (
                        from pq in db.tbl_phan_quyen_chuc_nangs
                        join cn in db.tbl_chuc_nangs
                            on pq.ma_chuc_nang equals cn.ma_chuc_nang
                        where pq.ma_phan_quyen == maPhanQuyen
                            && cn.ma_chuc_nang_code == maChucNang
                        select pq
                    ).Any();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra quyền: " + ex.Message);
                return false;
            }
        }

        private void ApDungPhanQuyen()
        {
            // Quản lý tài khoản và phân quyền
            btnQuanLyTaiKhoan.Enabled =
                KiemTraQuyen("QUAN_LY_TAI_KHOAN");

            btnQuanLyPhanQuyen.Enabled =
                KiemTraQuyen("QUAN_LY_PHAN_QUYEN");

            // Đổi mật khẩu
            btnMoDoiMatKhau.Enabled =
                KiemTraQuyen("DOI_MAT_KHAU");

            // Quản lý loại sách
            bool quyenLoaiSach = KiemTraQuyen("QUAN_LY_LOAI_SACH");
            btnLoaiSachThem.Enabled = quyenLoaiSach;
            btnLoaiSachSua.Enabled = quyenLoaiSach;
            btnLoaiSachXoa.Enabled = quyenLoaiSach;

            // Quản lý sách
            bool quyenSach = KiemTraQuyen("QUAN_LY_SACH");
            btnSachThem.Enabled = quyenSach;
            btnSachSua.Enabled = quyenSach;
            btnSachXoa.Enabled = quyenSach;

            // Quản lý hóa đơn
            bool quyenHoaDon = KiemTraQuyen("QUAN_LY_HOA_DON");
            btnHoaDonThem.Enabled = quyenHoaDon;
            btnHoaDonSua.Enabled = quyenHoaDon;
            btnHoaDonXoa.Enabled = quyenHoaDon;

            // Chi tiết hóa đơn
            bool quyenChiTietHoaDon =
                KiemTraQuyen("QUAN_LY_CHI_TIET_HOA_DON");

            btnChiTietHoaDonThem.Enabled = quyenChiTietHoaDon;
            btnChiTietHoaDonXoa.Enabled = quyenChiTietHoaDon;

            // Quản lý phiếu nhập
            bool quyenPhieuNhap = KiemTraQuyen("QUAN_LY_PHIEU_NHAP");
            btnPhieuNhapThem.Enabled = quyenPhieuNhap;
            btnPhieuNhapSua.Enabled = quyenPhieuNhap;
            btnPhieuNhapXoa.Enabled = quyenPhieuNhap;

            // Chi tiết phiếu nhập
            bool quyenChiTietPhieuNhap =
                KiemTraQuyen("QUAN_LY_CHI_TIET_PHIEU_NHAP");

            btnChiTietPhieuNhapThem.Enabled = quyenChiTietPhieuNhap;
            btnChiTietPhieuNhapXoa.Enabled = quyenChiTietPhieuNhap;

            // In báo cáo
            bool quyenInBaoCao = KiemTraQuyen("IN_BAO_CAO");
            btnInHoaDon.Enabled = quyenInBaoCao;
            btnInPhieuNhap.Enabled = quyenInBaoCao;

        }
        private void FormGiaoDien_Shown(object sender, EventArgs e)
        {
            ApDungPhanQuyen();
        }


        public FormGiaoDien()
        {
            InitializeComponent();
            btnQuanLyTaiKhoan.Click += btnQuanLyTaiKhoan_Click;
            btnMoDoiMatKhau.Click += btnMoDoiMatKhau_Click;

            this.Shown += FormGiaoDien_Shown;

            dgSach.AutoGenerateColumns = false;
            dgChiTietHoaDon.AutoGenerateColumns = false;
            dgPhieuNhap.AutoGenerateColumns = false;
            dgChiTietPhieuNhap.AutoGenerateColumns = false;

            KiemTraKetNoi();
            // Gắn sự kiện cho các nút Loại Sách
            btnLoaiSachThem.Click += btnLoaiSachThem_Click;
            btnLoaiSachSua.Click += btnLoaiSachSua_Click;
            btnLoaiSachXoa.Click += btnLoaiSachXoa_Click;

            // Gắn sự kiện chọn dòng
            dgLoaiSach.CellClick += dgLoaiSach_CellClick;

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
            btnSachThem.Click += btnSachThem_Click_1;
            btnSachSua.Click += btnSachSua_Click_1;
            btnSachXoa.Click += btnSachXoa_Click_1;

            // Gắn sự kiện chọn dòng sách
            dgSach.CellClick += dgSach_CellClick_1;

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
            LoadComboBoxSachChiTiet();
            

            if (dgChiTietHoaDon != null)
            {
                dgChiTietHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgChiTietHoaDon.ReadOnly = true;
                dgChiTietHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgChiTietHoaDon.MultiSelect = false;
                dgChiTietHoaDon.CellClick += dgChiTietHoaDon_CellClick;
            }

            if (btnChiTietHoaDonThem != null) btnChiTietHoaDonThem.Click += btnChiTietHoaDonThem_Click;
            if (btnChiTietHoaDonXoa != null) btnChiTietHoaDonXoa.Click += btnChiTietHoaDonXoa_Click;

            if (dgChiTietPhieuNhap != null)
            {
                dgChiTietPhieuNhap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgChiTietPhieuNhap.ReadOnly = true;
                dgChiTietPhieuNhap.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgChiTietPhieuNhap.MultiSelect = false;
                dgChiTietPhieuNhap.CellClick += dgChiTietPhieuNhap_CellClick;
            }

            if (btnChiTietPhieuNhapThem != null) btnChiTietPhieuNhapThem.Click += btnChiTietPhieuNhapThem_Click;
            if (btnChiTietPhieuNhapXoa != null) btnChiTietPhieuNhapXoa.Click += btnChiTietPhieuNhapXoa_Click;

            // --- GẮN SỰ KIỆN CHO NÚT IN ---
            if (btnInHoaDon != null) btnInHoaDon.Click += btnInHoaDon_Click;
            if (btnInPhieuNhap != null) btnInPhieuNhap.Click += btnInPhieuNhap_Click;
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
        //-----LOẠI SÁCH-----
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
        private void dgLoaiSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row =
                    dgLoaiSach.Rows[e.RowIndex];

                maLoaiSachDangChon = Convert.ToInt32(
                    row.Cells["ma_loai_sach"].Value
                );

                txtLoaiSachTen.Text = 
                    row.Cells["ten_loai_sach"].Value?.ToString() ?? "";
            }
        }

        // Thêm loại sách
        private void btnLoaiSachThem_Click(object sender, EventArgs e)
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
        private void btnLoaiSachSua_Click(object sender, EventArgs e)
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
        private void btnLoaiSachXoa_Click(object sender, EventArgs e)
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
        //-----SÁCH-----
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

                            ten_loai_sach = x.tbl_loai_sach != null ? x.tbl_loai_sach.ten_loai_sach : "Chưa phân loại",

                            x.tac_gia,
                            x.so_luong,
                            x.gia_ban
                        })
                        .ToList();

                    dgSach.AutoGenerateColumns = false;

                    // Gán dữ liệu cho DataGridView
                    dgSach.DataSource = danhSach;

                    // Đặt tiêu đề và định dạng cột
                    foreach (DataGridViewColumn col in dgSach.Columns)
                    {
                        string tenCot = col.DataPropertyName;

                        switch (tenCot)
                        {
                            case "ma_sach":
                                col.HeaderText = "Mã Sách";
                                break;

                            case "ten_sach":
                                col.HeaderText = "Tên Sách";
                                break;

                            case "TenLoaiSach":
                                col.HeaderText = "Loại Sách";
                                break;

                            case "tac_gia":
                                col.HeaderText = "Tác Giả";
                                break;

                            case "so_luong":
                                col.HeaderText = "Số Lượng";
                                break;

                            case "gia_ban":
                                col.HeaderText = "Giá Bán";
                                col.DefaultCellStyle.Format = "N0";
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách sách:\n" + ex.ToString());
            }
        }

        private void dgSach_CellClick_1(object sender, DataGridViewCellEventArgs e)
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

        private void btnSachThem_Click_1(object sender, EventArgs e)
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

                        ma_loai_sach = cbSachLoaiSach.SelectedValue == null
                            ? (int?)null
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

        // Sửa loại sách
        private void btnSachSua_Click_1(object sender, EventArgs e)
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

                    sach.ma_loai_sach =
                        cbSachLoaiSach.SelectedValue == null
                        ? (int?)null
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

        // Xóa loại sách
        private void btnSachXoa_Click_1(object sender, EventArgs e)
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

        // --- HÓA ĐƠN ---
        private void LoadHoaDon()
        {
            using (QuanLyBanSachDataContext db = new QuanLyBanSachDataContext())
            {
                var danhSach = db.tbl_hoa_dons
                    .Select(x => new
                    {
                        ma_hoa_don = x.ma_hoa_don,
                        ngay_lap_hoa_don = x.ngay_lap_hoa_don,
                        ten_khach_hang = x.ten_khach_hang,
                        sdt_khach_hang = x.sdt_khach_hang
                    })
                    .ToList();

                dgHoaDon.DataSource = danhSach;

                dgHoaDon.Columns["ma_hoa_don"].HeaderText = "Mã hóa đơn";
                dgHoaDon.Columns["ngay_lap_hoa_don"].HeaderText = "Ngày lập";
                dgHoaDon.Columns["ten_khach_hang"].HeaderText = "Tên khách hàng";
                dgHoaDon.Columns["sdt_khach_hang"].HeaderText = "Số điện thoại";
            }
        }

        private void dgHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgHoaDon.Rows[e.RowIndex];

            if (row.Cells["ma_hoa_don"].Value == null ||
                row.Cells["ma_hoa_don"].Value == DBNull.Value)
                return;

            maHoaDonDangChon = Convert.ToInt32(
                row.Cells["ma_hoa_don"].Value
            );

            txtHoaDonTenKH.Text =
                row.Cells["ten_khach_hang"].Value?.ToString() ?? "";

            txtHoaDonSDTKH.Text =
                row.Cells["sdt_khach_hang"].Value?.ToString() ?? "";

            if (row.Cells["ngay_lap_hoa_don"].Value != null &&
                row.Cells["ngay_lap_hoa_don"].Value != DBNull.Value)
            {
                dateNgayLapHoaDon.Value =
                    Convert.ToDateTime(row.Cells["ngay_lap_hoa_don"].Value);
            }

            LoadChiTietHoaDon(maHoaDonDangChon.Value);
        }
        //Thêm hóa đơn
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
        //Sửa hóa đơn
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

        // Làm mới hóa đơn
        private void LamMoiHoaDon()
        {
            maHoaDonDangChon = null;

            txtHoaDonTenKH.Clear();
            txtHoaDonSDTKH.Clear();

            dateNgayLapHoaDon.Value = DateTime.Today;

            dgHoaDon.ClearSelection();
            if (dgChiTietHoaDon != null) dgChiTietHoaDon.DataSource = null;
        }
        // Xóa hóa đơn
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

        // --- PHIẾU NHẬP ---
        private void LoadPhieuNhap()
        {
            try
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

                    dgPhieuNhap.DataSource = null;

                    dgPhieuNhap.Columns["MaPhieuNhap"].DataPropertyName = "MaPhieuNhap";
                    dgPhieuNhap.Columns["NgayLapPhieuNhap"].DataPropertyName = "NgayLapPhieuNhap";
                    dgPhieuNhap.Columns["TenNhaCungCap"].DataPropertyName = "TenNhaCungCap";

                    dgPhieuNhap.DataSource = danhSach;

                    dgPhieuNhap.Columns["MaPhieuNhap"].HeaderText = "Mã phiếu nhập";
                    dgPhieuNhap.Columns["NgayLapPhieuNhap"].HeaderText = "Ngày lập phiếu";
                    dgPhieuNhap.Columns["TenNhaCungCap"].HeaderText = "Nhà cung cấp";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải phiếu nhập:\n" + ex.ToString());
            }
        }

        private void dgPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                DataGridViewRow row = dgPhieuNhap.Rows[e.RowIndex];

                if (row.Cells["MaPhieuNhap"].Value == null ||
                    row.Cells["MaPhieuNhap"].Value == DBNull.Value)
                    return;

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

                LoadChiTietPhieuNhap(maPhieuNhapDangChon.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi chọn phiếu nhập:\n" + ex.ToString()
                );
            }
        }
        // Thêm phiếu nhập
        private void btnPhieuNhapThem_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm phiếu nhập:\n" + ex.ToString());
            }
        }
        // Sửa phiếu nhập
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
        // Xóa phiếu nhập
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
        // Làm mới phiếu nhập
        private void LamMoiPhieuNhap()
        {
            maPhieuNhapDangChon = null;

            txtPhieuNhapNhaCungCap.Clear();
            dateNgayLapPhieu.Value = DateTime.Today;

            dgPhieuNhap.ClearSelection();
            if (dgChiTietPhieuNhap != null) dgChiTietPhieuNhap.DataSource = null;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            btnChiTietPhieuNhapXoa_Click(sender, e);
        }

        // ==========================================
        // PHẦN BỔ SUNG: CHI TIẾT HÓA ĐƠN & PHIẾU NHẬP
        // ==========================================

        private void LoadComboBoxSachChiTiet()
        {
            try
            {
                using (QuanLyBanSachDataContext db = new QuanLyBanSachDataContext())
                {
                    var danhSachSach = db.tbl_saches
                        .Select(x => new { x.ma_sach, x.ten_sach })
                        .ToList();

                    if (cbChiTietHoaDonSach != null)
                    {
                        cbChiTietHoaDonSach.DataSource = danhSachSach.ToList();
                        cbChiTietHoaDonSach.DisplayMember = "ten_sach";
                        cbChiTietHoaDonSach.ValueMember = "ma_sach";
                        cbChiTietHoaDonSach.SelectedIndex = -1;
                    }

                    if (cbChiTietPhieuNhapSach != null)
                    {
                        cbChiTietPhieuNhapSach.DataSource = danhSachSach.ToList();
                        cbChiTietPhieuNhapSach.DisplayMember = "ten_sach";
                        cbChiTietPhieuNhapSach.ValueMember = "ma_sach";
                        cbChiTietPhieuNhapSach.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải combobox sách: " + ex.Message);
            }
        }

        
        private void LoadChiTietHoaDon(int maHoaDon)
        {
            try
            {
                using (QuanLyBanSachDataContext db = new QuanLyBanSachDataContext())
                {
                    var danhSach = db.tbl_chi_tiet_hoa_dons
                        .Where(x => x.ma_hoa_don == maHoaDon)
                        .Select(x => new
                        {
                            x.ma_hoa_don,
                            x.ma_sach,
                            TenSach = x.tbl_sach != null ? x.tbl_sach.ten_sach : "",
                            x.so_luong,
                            GiaBan = x.tbl_sach != null ? x.tbl_sach.gia_ban ?? 0 : 0,
                            ThanhTien = (x.so_luong ?? 0) * (x.tbl_sach != null ? x.tbl_sach.gia_ban ?? 0 : 0)
                        })
                        .ToList();

                    if (dgChiTietHoaDon != null)
                    {
                        dgChiTietHoaDon.DataSource = danhSach;

                        // Định dạng hiển thị phân cách hàng nghìn cho bảng Chi tiết hóa đơn
                        if (dgChiTietHoaDon.Columns["GiaBan"] != null)
                            dgChiTietHoaDon.Columns["GiaBan"].DefaultCellStyle.Format = "N0";

                        if (dgChiTietHoaDon.Columns["ThanhTien"] != null)
                            dgChiTietHoaDon.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết hóa đơn: " + ex.Message);
            }
        }

        private void dgChiTietHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || dgChiTietHoaDon == null)
                    return;

                DataGridViewRow row = dgChiTietHoaDon.Rows[e.RowIndex];

                // Lấy mã sách
                object giaTriMaSach = row.Cells["MaSach"].Value;

                if (giaTriMaSach == null || giaTriMaSach == DBNull.Value)
                    return;

                maSachChiTietHoaDonDangChon = Convert.ToInt32(giaTriMaSach);

                // Chọn sách trong ComboBox
                if (cbChiTietHoaDonSach != null)
                {
                    cbChiTietHoaDonSach.SelectedValue =
                        maSachChiTietHoaDonDangChon.Value;
                }

                // Lấy số lượng
                object giaTriSoLuong = row.Cells["SoLuong"].Value;

                if (numChiTietHoaDonSoLuong != null &&
                    giaTriSoLuong != null &&
                    giaTriSoLuong != DBNull.Value)
                {
                    decimal sl = Convert.ToDecimal(giaTriSoLuong);

                    numChiTietHoaDonSoLuong.Value =
                        Math.Min(
                            Math.Max(sl, numChiTietHoaDonSoLuong.Minimum),
                            numChiTietHoaDonSoLuong.Maximum
                        );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi chọn chi tiết hóa đơn:\n" + ex.ToString()
                );
            }
        }

        private void btnChiTietHoaDonThem_Click(object sender, EventArgs e)
        {
            if (maHoaDonDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần thêm chi tiết!");
                return;
            }
            if (cbChiTietHoaDonSach == null || cbChiTietHoaDonSach.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sách!");
                return;
            }

            int maSach = Convert.ToInt32(cbChiTietHoaDonSach.SelectedValue);
            int soLuongThem = numChiTietHoaDonSoLuong != null ? Convert.ToInt32(numChiTietHoaDonSoLuong.Value) : 0;

            if (soLuongThem <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0!");
                return;
            }

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (QuanLyBanSachDataContext db = new QuanLyBanSachDataContext())
                    {
                        var sach = db.tbl_saches.FirstOrDefault(x => x.ma_sach == maSach);
                        if (sach == null || sach.so_luong < soLuongThem)
                        {
                            MessageBox.Show("Số lượng sách trong kho không đủ!");
                            return;
                        }

                        var chiTietCu = db.tbl_chi_tiet_hoa_dons
                            .FirstOrDefault(x => x.ma_hoa_don == maHoaDonDangChon.Value && x.ma_sach == maSach);

                        if (chiTietCu != null)
                        {
                            chiTietCu.so_luong += soLuongThem;
                        }
                        else
                        {
                            tbl_chi_tiet_hoa_don cthd = new tbl_chi_tiet_hoa_don
                            {
                                ma_hoa_don = maHoaDonDangChon.Value,
                                ma_sach = maSach,
                                so_luong = soLuongThem
                            };
                            db.tbl_chi_tiet_hoa_dons.InsertOnSubmit(cthd);
                        }

                        // Giảm tồn kho
                        sach.so_luong -= soLuongThem;
                        db.SubmitChanges();
                    }
                    scope.Complete();
                }

                MessageBox.Show("Thêm chi tiết hóa đơn thành công!");
                LoadChiTietHoaDon(maHoaDonDangChon.Value);
                LoadSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm chi tiết hóa đơn: " + ex.Message);
            }
        }

        private void btnChiTietHoaDonXoa_Click(object sender, EventArgs e)
        {
            if (maHoaDonDangChon == null || maSachChiTietHoaDonDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn dòng chi tiết hóa đơn cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa sách này khỏi hóa đơn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (QuanLyBanSachDataContext db = new QuanLyBanSachDataContext())
                    {
                        var cthd = db.tbl_chi_tiet_hoa_dons
                            .FirstOrDefault(x => x.ma_hoa_don == maHoaDonDangChon.Value && x.ma_sach == maSachChiTietHoaDonDangChon.Value);

                        if (cthd != null)
                        {
                            // Hoàn trả tồn kho
                            var sach = db.tbl_saches.FirstOrDefault(x => x.ma_sach == maSachChiTietHoaDonDangChon.Value);
                            if (sach != null)
                            {
                                sach.so_luong += cthd.so_luong;
                            }

                            db.tbl_chi_tiet_hoa_dons.DeleteOnSubmit(cthd);
                            db.SubmitChanges();
                        }
                    }
                    scope.Complete();
                }

                MessageBox.Show("Xóa chi tiết hóa đơn thành công!");
                LoadChiTietHoaDon(maHoaDonDangChon.Value);
                LoadSach();
                maSachChiTietHoaDonDangChon = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa chi tiết hóa đơn: " + ex.Message);
            }
        }

        // --- CHI TIẾT PHIẾU NHẬP ---
        private void LoadChiTietPhieuNhap(int maPhieuNhap)
        {
            try
            {
                using (QuanLyBanSachDataContext db = new QuanLyBanSachDataContext())
                {
                    var danhSach = db.tbl_chi_tiet_phieu_nhaps
                        .Where(x => x.ma_phieu_nhap == maPhieuNhap)
                        .Select(x => new
                        {
                            x.ma_phieu_nhap,
                            x.ma_sach,
                            TenSach = x.tbl_sach != null ? x.tbl_sach.ten_sach : "",
                            x.so_luong
                        })
                        .ToList();

                    if (dgChiTietPhieuNhap != null)
                        dgChiTietPhieuNhap.DataSource = danhSach;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết phiếu nhập: " + ex.Message);
            }
        }

        private void dgChiTietPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgChiTietPhieuNhap == null) return;
            DataGridViewRow row = dgChiTietPhieuNhap.Rows[e.RowIndex];
            if (row.Cells["colMaSach"].Value != null)
            {
                maSachChiTietPhieuNhapDangChon = Convert.ToInt32(row.Cells["colMaSach"].Value);
                if (cbChiTietPhieuNhapSach != null) cbChiTietPhieuNhapSach.SelectedValue = maSachChiTietPhieuNhapDangChon.Value;
                if (numChiTietPhieuNhapSoLuong != null && row.Cells["colSoLuong"].Value != null)
                {
                    decimal sl = Convert.ToDecimal(row.Cells["colSoLuong"].Value);
                    numChiTietPhieuNhapSoLuong.Value = Math.Min(Math.Max(sl, numChiTietPhieuNhapSoLuong.Minimum), numChiTietPhieuNhapSoLuong.Maximum);
                }
            }
        }

        private void btnChiTietPhieuNhapThem_Click(object sender, EventArgs e)
        {
            if (maPhieuNhapDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập cần thêm chi tiết!");
                return;
            }
            if (cbChiTietPhieuNhapSach == null || cbChiTietPhieuNhapSach.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sách!");
                return;
            }

            int maSach = Convert.ToInt32(cbChiTietPhieuNhapSach.SelectedValue);
            int soLuongNhap = numChiTietPhieuNhapSoLuong != null ? Convert.ToInt32(numChiTietPhieuNhapSoLuong.Value) : 0;

            if (soLuongNhap <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0!");
                return;
            }

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (QuanLyBanSachDataContext db = new QuanLyBanSachDataContext())
                    {
                        var chiTietCu = db.tbl_chi_tiet_phieu_nhaps
                            .FirstOrDefault(x => x.ma_phieu_nhap == maPhieuNhapDangChon.Value && x.ma_sach == maSach);

                        if (chiTietCu != null)
                        {
                            chiTietCu.so_luong += soLuongNhap;
                        }
                        else
                        {
                            tbl_chi_tiet_phieu_nhap ctpn = new tbl_chi_tiet_phieu_nhap
                            {
                                ma_phieu_nhap = maPhieuNhapDangChon.Value,
                                ma_sach = maSach,
                                so_luong = soLuongNhap
                            };
                            db.tbl_chi_tiet_phieu_nhaps.InsertOnSubmit(ctpn);
                        }

                        // Tăng tồn kho khi nhập hàng
                        var sach = db.tbl_saches.FirstOrDefault(x => x.ma_sach == maSach);
                        if (sach != null)
                        {
                            sach.so_luong = (sach.so_luong ?? 0) + soLuongNhap;
                        }

                        db.SubmitChanges();
                    }
                    scope.Complete();
                }

                MessageBox.Show("Thêm chi tiết phiếu nhập thành công!");
                LoadChiTietPhieuNhap(maPhieuNhapDangChon.Value);
                LoadSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm chi tiết phiếu nhập: " + ex.Message);
            }
        }

        private void btnChiTietPhieuNhapXoa_Click(object sender, EventArgs e)
        {
            if (maPhieuNhapDangChon == null || maSachChiTietPhieuNhapDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn dòng chi tiết phiếu nhập cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa chi tiết phiếu nhập này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (QuanLyBanSachDataContext db = new QuanLyBanSachDataContext())
                    {
                        var ctpn = db.tbl_chi_tiet_phieu_nhaps
                            .FirstOrDefault(x => x.ma_phieu_nhap == maPhieuNhapDangChon.Value && x.ma_sach == maSachChiTietPhieuNhapDangChon.Value);

                        if (ctpn != null)
                        {
                            // Giảm lại số lượng trong kho vì hủy chi tiết phiếu nhập
                            var sach = db.tbl_saches.FirstOrDefault(x => x.ma_sach == maSachChiTietPhieuNhapDangChon.Value);
                            if (sach != null)
                            {
                                sach.so_luong = (sach.so_luong ?? 0) - ctpn.so_luong;
                                if (sach.so_luong < 0) sach.so_luong = 0;
                            }

                            db.tbl_chi_tiet_phieu_nhaps.DeleteOnSubmit(ctpn);
                            db.SubmitChanges();
                        }
                    }
                    scope.Complete();
                }

                MessageBox.Show("Xóa chi tiết phiếu nhập thành công!");
                LoadChiTietPhieuNhap(maPhieuNhapDangChon.Value);
                LoadSach();
                maSachChiTietPhieuNhapDangChon = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa chi tiết phiếu nhập: " + ex.Message);
            }
        }

        // --- HÀM SỰ KIỆN IN HÓA ĐƠN & PHIẾU NHẬP ---
        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (maHoaDonDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần in!");
                return;
            }

            try
            {
                using (QuanLyBanSachDataContext db = new QuanLyBanSachDataContext())
                {
                    var data = db.tbl_chi_tiet_hoa_dons
                        .Where(x => x.ma_hoa_don == maHoaDonDangChon.Value)
                        .Select(x => new
                        {
                            MãHĐ = x.ma_hoa_don,
                            TênKhách = x.tbl_hoa_don != null ? x.tbl_hoa_don.ten_khach_hang : "",
                            TênSách = x.tbl_sach != null ? x.tbl_sach.ten_sach : "",
                            SốLượng = x.so_luong,
                            ĐơnGiá = x.tbl_sach != null ? x.tbl_sach.gia_ban ?? 0 : 0,
                            ThànhTiền = (x.so_luong ?? 0) * (x.tbl_sach != null ? x.tbl_sach.gia_ban ?? 0 : 0)
                        })
                        .ToList();

                    if (data.Count == 0)
                    {
                        MessageBox.Show("Hóa đơn không có chi tiết để in!");
                        return;
                    }

                    FrmInReport frm = new FrmInReport(data, "HÓA ĐƠN BÁN SÁCH");

                    if (frm.Controls["dataGridView1"] is DataGridView dgReport)
                    {
                        if (dgReport.Columns["ĐơnGiá"] != null)
                            dgReport.Columns["ĐơnGiá"].DefaultCellStyle.Format = "N0";

                        if (dgReport.Columns["ThànhTiền"] != null)
                            dgReport.Columns["ThànhTiền"].DefaultCellStyle.Format = "N0";
                    }

                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo dữ liệu in hóa đơn: " + ex.Message);
            }
        }

        private void btnInPhieuNhap_Click(object sender, EventArgs e)
        {
            if (maPhieuNhapDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập cần in!");
                return;
            }

            try
            {
                using (QuanLyBanSachDataContext db = new QuanLyBanSachDataContext())
                {
                    var data = db.tbl_chi_tiet_phieu_nhaps
                        .Where(x => x.ma_phieu_nhap == maPhieuNhapDangChon.Value)
                        .Select(x => new
                        {
                            MãPhiếuNhập = x.ma_phieu_nhap,
                            NhàCungCấp = x.tbl_phieu_nhap != null ? x.tbl_phieu_nhap.ten_nha_cung_cap : "",
                            TênSách = x.tbl_sach != null ? x.tbl_sach.ten_sach : "",
                            SốLượngNhập = x.so_luong
                        })
                        .ToList();

                    if (data.Count == 0)
                    {
                        MessageBox.Show("Phiếu nhập không có chi tiết để in!");
                        return;
                    }

                    FrmInReport frm = new FrmInReport(data, "PHIẾU NHẬP SÁCH");
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo dữ liệu in phiếu nhập: " + ex.Message);
            }
        }

        private void dgSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgPhieuNhap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnChiTietPhieuNhapThem_Click_1(object sender, EventArgs e)
        {

        }

        private void dgLoaiSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnMoDoiMatKhau_Click(object sender, EventArgs e)
        {
            using (frmDoiMatKhau frm = new frmDoiMatKhau(TenDangNhapHienTai))
            {
                frm.ShowDialog();
            }
        }

        private void btnQuanLyTaiKhoan_Click(object sender, EventArgs e)
        {
            using (frmQuanLyTaiKhoan frm = new frmQuanLyTaiKhoan())
            {
                frm.ShowDialog();
            }
        }

        private void btnQuanLyPhanQuyen_Click(object sender, EventArgs e)
        {
            using (frmQuanLyPhanQuyen frm = new frmQuanLyPhanQuyen())
            {
                frm.ShowDialog();
            }
        }

        private void cbChiTietPhieuNhapSach_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}