using System;
using System.Data;
using System.Drawing;
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

        public bool DangXuat { get; private set; }

        private bool _ketNoiOk = false;

        private QuanLyBanSachDataContext LayDataContext()
        {
            string connStr = Properties.Settings.Default.db_QuanLyBanSachConnectionString;
            return new QuanLyBanSachDataContext(connStr);
        }

        // Mở 1 form con dạng hộp thoại, đóng xong thì nạp lại dữ liệu các tab
        private void MoForm(Form frm)
        {
            using (frm)
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }

            LamMoiDuLieu();
        }

        // Nạp lại dữ liệu tất cả các tab
        private void LamMoiDuLieu()
        {
            if (!_ketNoiOk)
                return;

            LoadLoaiSach();
            LoadLoaiSachComboBox();
            LoadSach();
            LoadHoaDon();
            LoadPhieuNhap();
            LoadComboBoxSachChiTiet();
        }

        public FormGiaoDien()
        {
            InitializeComponent();

            if (dgChiTietHoaDon != null) dgChiTietHoaDon.CellClick += dgChiTietHoaDon_CellClick;
            if (dgChiTietPhieuNhap != null) dgChiTietPhieuNhap.CellClick += dgChiTietPhieuNhap_CellClick;

            // ----- Cấu hình các lưới -----
            dgSach.AutoGenerateColumns = false;
            dgLoaiSach.AutoGenerateColumns = false;
            dgHoaDon.AutoGenerateColumns = false;
            dgPhieuNhap.AutoGenerateColumns = false;
            dgChiTietPhieuNhap.AutoGenerateColumns = false;

            LuoiHelper.DatCot(dgChiTietHoaDon,
                new CotLuoi("ma_hoa_don", "Mã hóa đơn", "ma_hoa_don"),
                new CotLuoi("ma_sach", "Mã sách", "ma_sach"),
                new CotLuoi("TenSach", "Tên sách", "TenSach"),
                new CotLuoi("so_luong", "Số lượng", "so_luong"),
                new CotLuoi("GiaBan", "Đơn giá", "GiaBan", "N0", true),
                new CotLuoi("ThanhTien", "Thành tiền", "ThanhTien", "N0", true));

            foreach (DataGridView dg in new DataGridView[]
                { dgLoaiSach, dgSach, dgHoaDon, dgPhieuNhap, dgChiTietPhieuNhap })
            {
                if (dg != null)
                {
                    dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dg.ReadOnly = true;
                    dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dg.MultiSelect = false;
                }
            }

            if (dgHoaDon.Columns["ngay_lap_hoa_don"] != null)
                dgHoaDon.Columns["ngay_lap_hoa_don"].DefaultCellStyle.Format = "dd/MM/yyyy";
            if (dgPhieuNhap.Columns["NgayLapPhieuNhap"] != null)
                dgPhieuNhap.Columns["NgayLapPhieuNhap"].DefaultCellStyle.Format = "dd/MM/yyyy";

            if (numChiTietHoaDonSoLuong != null)
            {
                numChiTietHoaDonSoLuong.Minimum = 1;
                numChiTietHoaDonSoLuong.Maximum = 10000;
                numChiTietHoaDonSoLuong.Value = 1;
            }

            if (numChiTietPhieuNhapSoLuong != null)
            {
                numChiTietPhieuNhapSoLuong.Minimum = 1;
                numChiTietPhieuNhapSoLuong.Maximum = 100000;
                numChiTietPhieuNhapSoLuong.Value = 1;
            }

            // ----- Kiểm tra kết nối 1 lần -----
            _ketNoiOk = KiemTraKetNoi();
            if (_ketNoiOk)
            {
                LamMoiDuLieu();
                LoadComboBoxSachChiTiet();
            }
        }

        private bool KiemTraKetNoi()
        {
            try
            {
                using (QuanLyBanSachDataContext db = LayDataContext())
                {
                    db.Connection.Open();
                    db.Connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không kết nối được SQL Server:\n" + ex.Message,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        //-----LOẠI SÁCH-----
        private void LoadLoaiSach()
        {
            try
            {
                using (QuanLyBanSachDataContext db = LayDataContext())
                {
                    var danhSach = db.tbl_loai_saches
                        .Select(x => new
                        {
                            x.ma_loai_sach,
                            x.ten_loai_sach
                        })
                        .ToList();

                    dgLoaiSach.DataSource = danhSach;

                    dgLoaiSach.Columns["ma_loai_sach"].HeaderText = "Mã Loại Sách";
                    dgLoaiSach.Columns["ten_loai_sach"].HeaderText = "Tên Loại Sách";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }

        private void dgLoaiSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgLoaiSach.Rows[e.RowIndex];
                maLoaiSachDangChon = Convert.ToInt32(row.Cells["ma_loai_sach"].Value);
                txtLoaiSachTen.Text = row.Cells["ten_loai_sach"].Value?.ToString() ?? "";
            }
        }

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
                using (QuanLyBanSachDataContext db = LayDataContext())
                {
                    bool daTonTai = db.tbl_loai_saches.Any(x => x.ten_loai_sach == tenLoaiSach);
                    if (daTonTai)
                    {
                        MessageBox.Show("Loại sách này đã tồn tại!");
                        return;
                    }

                    tbl_loai_sach loaiSach = new tbl_loai_sach { ten_loai_sach = tenLoaiSach };
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
                using (QuanLyBanSachDataContext db = LayDataContext())
                {
                    var loaiSach = db.tbl_loai_saches.FirstOrDefault(x => x.ma_loai_sach == maLoaiSachDangChon.Value);
                    if (loaiSach == null)
                    {
                        MessageBox.Show("Không tìm thấy loại sách!");
                        return;
                    }

                    bool trungTen = db.tbl_loai_saches.Any(x => x.ten_loai_sach == tenLoaiSach && x.ma_loai_sach != maLoaiSachDangChon.Value);
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

        private void btnLoaiSachXoa_Click(object sender, EventArgs e)
        {
            if (maLoaiSachDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn loại sách cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa loại sách này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                using (QuanLyBanSachDataContext db = LayDataContext())
                {
                    var loaiSach = db.tbl_loai_saches.FirstOrDefault(x => x.ma_loai_sach == maLoaiSachDangChon.Value);
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
                MessageBox.Show("Không thể xóa loại sách này do vướng ràng buộc dữ liệu!\nChi tiết: " + ex.Message);
            }
        }

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
                using (QuanLyBanSachDataContext db = LayDataContext())
                {
                    var danhSach = db.tbl_loai_saches.OrderBy(x => x.ten_loai_sach).ToList();
                    cbSachLoaiSach.DataSource = danhSach;
                    cbSachLoaiSach.DisplayMember = "ten_loai_sach";
                    cbSachLoaiSach.ValueMember = "ma_loai_sach";
                    cbSachLoaiSach.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải loại sách: " + ex.Message);
            }
        }

        //-----SÁCH-----
        private void LoadSach()
        {
            try
            {
                using (QuanLyBanSachDataContext db = LayDataContext())
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
                    dgSach.DataSource = danhSach;

                    foreach (DataGridViewColumn col in dgSach.Columns)
                    {
                        string tenCot = col.DataPropertyName;
                        switch (tenCot)
                        {
                            case "ma_sach": col.HeaderText = "Mã Sách"; break;
                            case "ten_sach": col.HeaderText = "Tên Sách"; break;
                            case "TenLoaiSach": col.HeaderText = "Loại Sách"; break;
                            case "tac_gia": col.HeaderText = "Tác Giả"; break;
                            case "so_luong": col.HeaderText = "Số Lượng"; break;
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
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgSach.Rows[e.RowIndex];
            maSachDangChon = Convert.ToInt32(row.Cells["ma_sach"].Value);
            txtSachTenSach.Text = row.Cells["ten_sach"].Value?.ToString() ?? "";
            txtSachTacGia.Text = row.Cells["tac_gia"].Value?.ToString() ?? "";

            numSachSoLuong.Value = row.Cells["so_luong"].Value == null || row.Cells["so_luong"].Value == DBNull.Value ? 0 : Convert.ToDecimal(row.Cells["so_luong"].Value);
            numSachGiaBan.Value = row.Cells["gia_ban"].Value == null || row.Cells["gia_ban"].Value == DBNull.Value ? 0 : Convert.ToDecimal(row.Cells["gia_ban"].Value);

            using (QuanLyBanSachDataContext db = LayDataContext())
            {
                var maLoai = db.tbl_saches.Where(x => x.ma_sach == maSachDangChon.Value).Select(x => x.ma_loai_sach).FirstOrDefault();
                cbSachLoaiSach.SelectedValue = maLoai;
                if (maLoai == null) cbSachLoaiSach.SelectedIndex = -1;
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
                using (QuanLyBanSachDataContext db = LayDataContext())
                {
                    tbl_sach sach = new tbl_sach
                    {
                        ten_sach = tenSach,
                        tac_gia = string.IsNullOrWhiteSpace(tacGia) ? null : tacGia,
                        ma_loai_sach = cbSachLoaiSach.SelectedValue == null ? (int?)null : Convert.ToInt32(cbSachLoaiSach.SelectedValue),
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
                using (QuanLyBanSachDataContext db = LayDataContext())
                {
                    var sach = db.tbl_saches.FirstOrDefault(x => x.ma_sach == maSachDangChon.Value);
                    if (sach == null)
                    {
                        MessageBox.Show("Không tìm thấy sách!");
                        return;
                    }

                    sach.ten_sach = tenSach;
                    sach.tac_gia = string.IsNullOrWhiteSpace(tacGia) ? null : tacGia;
                    sach.ma_loai_sach = cbSachLoaiSach.SelectedValue == null ? (int?)null : Convert.ToInt32(cbSachLoaiSach.SelectedValue);
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

        private void btnSachXoa_Click_1(object sender, EventArgs e)
        {
            if (maSachDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn sách cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa sách này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                using (QuanLyBanSachDataContext db = LayDataContext())
                {
                    bool coTrongHoaDon = db.tbl_chi_tiet_hoa_dons.Any(x => x.ma_sach == maSachDangChon.Value);
                    bool coTrongPhieuNhap = db.tbl_chi_tiet_phieu_nhaps.Any(x => x.ma_sach == maSachDangChon.Value);

                    if (coTrongHoaDon || coTrongPhieuNhap)
                    {
                        MessageBox.Show("Không thể xóa sách này vì sách đã có trong hóa đơn hoặc phiếu nhập!");
                        return;
                    }

                    var sach = db.tbl_saches.FirstOrDefault(x => x.ma_sach == maSachDangChon.Value);
                    if (sach != null)
                    {
                        db.tbl_saches.DeleteOnSubmit(sach);
                        db.SubmitChanges();
                    }
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
            using (QuanLyBanSachDataContext db = LayDataContext())
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
                if (dgHoaDon.Columns["ma_hoa_don"] != null) dgHoaDon.Columns["ma_hoa_don"].HeaderText = "Mã hóa đơn";
                if (dgHoaDon.Columns["ngay_lap_hoa_don"] != null) dgHoaDon.Columns["ngay_lap_hoa_don"].HeaderText = "Ngày lập";
                if (dgHoaDon.Columns["ten_khach_hang"] != null) dgHoaDon.Columns["ten_khach_hang"].HeaderText = "Tên khách hàng";
                if (dgHoaDon.Columns["sdt_khach_hang"] != null) dgHoaDon.Columns["sdt_khach_hang"].HeaderText = "Số điện thoại";
            }
        }

        private void dgHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgHoaDon.Rows[e.RowIndex];
            if (row.Cells["ma_hoa_don"].Value == null || row.Cells["ma_hoa_don"].Value == DBNull.Value) return;

            maHoaDonDangChon = Convert.ToInt32(row.Cells["ma_hoa_don"].Value);
            txtHoaDonTenKH.Text = row.Cells["ten_khach_hang"].Value?.ToString() ?? "";
            txtHoaDonSDTKH.Text = row.Cells["sdt_khach_hang"].Value?.ToString() ?? "";

            if (row.Cells["ngay_lap_hoa_don"].Value != null && row.Cells["ngay_lap_hoa_don"].Value != DBNull.Value)
            {
                dateNgayLapHoaDon.Value = Convert.ToDateTime(row.Cells["ngay_lap_hoa_don"].Value);
            }

            LoadChiTietHoaDon(maHoaDonDangChon.Value);
        }

        private void btnHoaDonThem_Click(object sender, EventArgs e)
        {
            using (QuanLyBanSachDataContext db = LayDataContext())
            {
                tbl_hoa_don hoaDon = new tbl_hoa_don
                {
                    ngay_lap_hoa_don = dateNgayLapHoaDon.Value.Date,
                    ten_khach_hang = string.IsNullOrWhiteSpace(txtHoaDonTenKH.Text) ? null : txtHoaDonTenKH.Text.Trim(),
                    sdt_khach_hang = string.IsNullOrWhiteSpace(txtHoaDonSDTKH.Text) ? null : txtHoaDonSDTKH.Text.Trim()
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

            using (QuanLyBanSachDataContext db = LayDataContext())
            {
                var hoaDon = db.tbl_hoa_dons.FirstOrDefault(x => x.ma_hoa_don == maHoaDonDangChon.Value);
                if (hoaDon == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn!");
                    return;
                }

                hoaDon.ngay_lap_hoa_don = dateNgayLapHoaDon.Value.Date;
                hoaDon.ten_khach_hang = string.IsNullOrWhiteSpace(txtHoaDonTenKH.Text) ? null : txtHoaDonTenKH.Text.Trim();
                hoaDon.sdt_khach_hang = string.IsNullOrWhiteSpace(txtHoaDonSDTKH.Text) ? null : txtHoaDonSDTKH.Text.Trim();

                db.SubmitChanges();
            }

            MessageBox.Show("Cập nhật hóa đơn thành công!");
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
            if (dgChiTietHoaDon != null) dgChiTietHoaDon.DataSource = null;
        }

        private void btnHoaDonXoa_Click(object sender, EventArgs e)
        {
            if (maHoaDonDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            using (QuanLyBanSachDataContext db = LayDataContext())
            {
                bool coChiTiet = db.tbl_chi_tiet_hoa_dons.Any(x => x.ma_hoa_don == maHoaDonDangChon.Value);
                if (coChiTiet)
                {
                    MessageBox.Show("Không thể xóa hóa đơn đã có chi tiết hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var hoaDon = db.tbl_hoa_dons.FirstOrDefault(x => x.ma_hoa_don == maHoaDonDangChon.Value);
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
                using (QuanLyBanSachDataContext db = LayDataContext())
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
                    if (dgPhieuNhap.Columns["MaPhieuNhap"] != null) dgPhieuNhap.Columns["MaPhieuNhap"].DataPropertyName = "MaPhieuNhap";
                    if (dgPhieuNhap.Columns["NgayLapPhieuNhap"] != null) dgPhieuNhap.Columns["NgayLapPhieuNhap"].DataPropertyName = "NgayLapPhieuNhap";
                    if (dgPhieuNhap.Columns["TenNhaCungCap"] != null) dgPhieuNhap.Columns["TenNhaCungCap"].DataPropertyName = "TenNhaCungCap";

                    dgPhieuNhap.DataSource = danhSach;

                    if (dgPhieuNhap.Columns["MaPhieuNhap"] != null) dgPhieuNhap.Columns["MaPhieuNhap"].HeaderText = "Mã phiếu nhập";
                    if (dgPhieuNhap.Columns["NgayLapPhieuNhap"] != null) dgPhieuNhap.Columns["NgayLapPhieuNhap"].HeaderText = "Ngày lập phiếu";
                    if (dgPhieuNhap.Columns["TenNhaCungCap"] != null) dgPhieuNhap.Columns["TenNhaCungCap"].HeaderText = "Nhà cung cấp";
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
                if (e.RowIndex < 0 || dgPhieuNhap == null) return;

                DataGridViewRow row = dgPhieuNhap.Rows[e.RowIndex];
                object valMaPN = null;
                if (dgPhieuNhap.Columns.Contains("MaPhieuNhap")) valMaPN = row.Cells["MaPhieuNhap"].Value;
                else if (row.Cells.Count > 0) valMaPN = row.Cells[0].Value;

                if (valMaPN == null || valMaPN == DBNull.Value) return;

                maPhieuNhapDangChon = Convert.ToInt32(valMaPN);
                object valNCC = dgPhieuNhap.Columns.Contains("TenNhaCungCap") ? row.Cells["TenNhaCungCap"].Value : null;
                txtPhieuNhapNhaCungCap.Text = valNCC?.ToString() ?? "";

                object valNgay = dgPhieuNhap.Columns.Contains("NgayLapPhieuNhap") ? row.Cells["NgayLapPhieuNhap"].Value : null;
                if (valNgay != null && valNgay != DBNull.Value)
                {
                    dateNgayLapPhieu.Value = Convert.ToDateTime(valNgay);
                }

                LoadChiTietPhieuNhap(maPhieuNhapDangChon.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn phiếu nhập:\n" + ex.ToString());
            }
        }

        private void btnPhieuNhapThem_Click(object sender, EventArgs e)
        {
            try
            {
                using (QuanLyBanSachDataContext db = LayDataContext())
                {
                    tbl_phieu_nhap phieuNhap = new tbl_phieu_nhap
                    {
                        ngay_lap_phieu_nhap = dateNgayLapPhieu.Value.Date,
                        ten_nha_cung_cap = string.IsNullOrWhiteSpace(txtPhieuNhapNhaCungCap.Text) ? null : txtPhieuNhapNhaCungCap.Text.Trim()
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

        private void btnPhieuNhapSua_Click(object sender, EventArgs e)
        {
            if (maPhieuNhapDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập cần sửa!");
                return;
            }

            using (QuanLyBanSachDataContext db = LayDataContext())
            {
                var phieuNhap = db.tbl_phieu_nhaps.FirstOrDefault(x => x.ma_phieu_nhap == maPhieuNhapDangChon.Value);
                if (phieuNhap == null)
                {
                    MessageBox.Show("Không tìm thấy phiếu nhập!");
                    return;
                }

                phieuNhap.ngay_lap_phieu_nhap = dateNgayLapPhieu.Value.Date;
                phieuNhap.ten_nha_cung_cap = string.IsNullOrWhiteSpace(txtPhieuNhapNhaCungCap.Text) ? null : txtPhieuNhapNhaCungCap.Text.Trim();

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

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu nhập này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            using (QuanLyBanSachDataContext db = LayDataContext())
            {
                bool coChiTiet = db.tbl_chi_tiet_phieu_nhaps.Any(x => x.ma_phieu_nhap == maPhieuNhapDangChon.Value);
                if (coChiTiet)
                {
                    MessageBox.Show("Không thể xóa phiếu nhập đã có chi tiết phiếu nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var phieuNhap = db.tbl_phieu_nhaps.FirstOrDefault(x => x.ma_phieu_nhap == maPhieuNhapDangChon.Value);
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
            if (dgChiTietPhieuNhap != null) dgChiTietPhieuNhap.DataSource = null;
        }

        // ==========================================
        // CHI TIẾT HÓA ĐƠN & PHIẾU NHẬP
        // ==========================================
        private void LoadComboBoxSachChiTiet()
        {
            try
            {
                using (QuanLyBanSachDataContext db = LayDataContext())
                {
                    var danhSachSach = db.tbl_saches
                        .Select(x => new { x.ma_sach, x.ten_sach })
                        .ToList();

                    if (cbChiTietHoaDonSach != null)
                    {
                        cbChiTietHoaDonSach.SelectedIndexChanged -= cbChiTietHoaDonSach_SelectedIndexChanged;
                        cbChiTietHoaDonSach.DataSource = danhSachSach.ToList();
                        cbChiTietHoaDonSach.DisplayMember = "ten_sach";
                        cbChiTietHoaDonSach.ValueMember = "ma_sach";
                        cbChiTietHoaDonSach.SelectedIndex = -1;
                        cbChiTietHoaDonSach.SelectedIndexChanged += cbChiTietHoaDonSach_SelectedIndexChanged;
                    }

                    if (cbChiTietPhieuNhapSach != null)
                    {
                        cbChiTietPhieuNhapSach.SelectedIndexChanged -= cbChiTietPhieuNhapSach_SelectedIndexChanged;
                        cbChiTietPhieuNhapSach.DataSource = danhSachSach.ToList();
                        cbChiTietPhieuNhapSach.DisplayMember = "ten_sach";
                        cbChiTietPhieuNhapSach.ValueMember = "ma_sach";
                        cbChiTietPhieuNhapSach.SelectedIndex = -1;
                        cbChiTietPhieuNhapSach.SelectedIndexChanged += cbChiTietPhieuNhapSach_SelectedIndexChanged;
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
                using (QuanLyBanSachDataContext db = LayDataContext())
                {
                    var danhSach = db.tbl_chi_tiet_hoa_dons
                        .Where(x => x.ma_hoa_don == maHoaDon)
                        .Select(x => new
                        {
                            ma_hoa_don = x.ma_hoa_don,
                            ma_sach = x.ma_sach,
                            TenSach = x.tbl_sach != null ? x.tbl_sach.ten_sach : "",
                            so_luong = x.so_luong ?? 0,
                            GiaBan = x.tbl_sach != null ? x.tbl_sach.gia_ban ?? 0 : 0,
                            ThanhTien = (x.so_luong ?? 0) * (x.tbl_sach != null ? x.tbl_sach.gia_ban ?? 0 : 0)
                        })
                        .ToList();

                    dgChiTietHoaDon.DataSource = null;
                    dgChiTietHoaDon.DataSource = danhSach;

                    dgChiTietHoaDon.Columns["ma_hoa_don"].HeaderText = "Mã hóa đơn";
                    dgChiTietHoaDon.Columns["ma_sach"].HeaderText = "Mã sách";
                    dgChiTietHoaDon.Columns["TenSach"].HeaderText = "Tên sách";
                    dgChiTietHoaDon.Columns["so_luong"].HeaderText = "Số lượng";
                    dgChiTietHoaDon.Columns["GiaBan"].HeaderText = "Đơn giá";
                    dgChiTietHoaDon.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
                    dgChiTietHoaDon.Columns["ThanhTien"].HeaderText = "Thành tiền";
                    dgChiTietHoaDon.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
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
                if (e.RowIndex < 0 || dgChiTietHoaDon == null) return;
                DataGridViewRow row = dgChiTietHoaDon.Rows[e.RowIndex];

                object giaTriMaSach = row.Cells["ma_sach"]?.Value;
                if (giaTriMaSach == null || giaTriMaSach == DBNull.Value) return;

                maSachChiTietHoaDonDangChon = Convert.ToInt32(giaTriMaSach);
                if (cbChiTietHoaDonSach != null) cbChiTietHoaDonSach.SelectedValue = maSachChiTietHoaDonDangChon.Value;

                object giaTriSoLuong = row.Cells["so_luong"]?.Value;
                if (numChiTietHoaDonSoLuong != null && giaTriSoLuong != null && giaTriSoLuong != DBNull.Value)
                {
                    decimal sl = Convert.ToDecimal(giaTriSoLuong);
                    numChiTietHoaDonSoLuong.Value = Math.Min(Math.Max(sl, numChiTietHoaDonSoLuong.Minimum), numChiTietHoaDonSoLuong.Maximum);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn chi tiết hóa đơn:\n" + ex.Message);
            }
        }

        private void cbChiTietHoaDonSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbChiTietHoaDonSach == null || cbChiTietHoaDonSach.SelectedIndex == -1 || cbChiTietHoaDonSach.SelectedValue == null) return;
            if (!int.TryParse(cbChiTietHoaDonSach.SelectedValue.ToString(), out int maSach)) return;

            try
            {
                using (QuanLyBanSachDataContext db = LayDataContext())
                {
                    var sach = db.tbl_saches.FirstOrDefault(x => x.ma_sach == maSach);
                    if (sach != null && numChiTietHoaDonSoLuong != null)
                    {
                        int tonKho = sach.so_luong ?? 0;
                        numChiTietHoaDonSoLuong.ValueChanged -= numChiTietHoaDonSoLuong_ValueChanged;
                        if (tonKho > 0)
                        {
                            numChiTietHoaDonSoLuong.Minimum = 1;
                            numChiTietHoaDonSoLuong.Maximum = tonKho;
                            numChiTietHoaDonSoLuong.Value = 1;
                            numChiTietHoaDonSoLuong.Enabled = true;
                        }
                        else
                        {
                            numChiTietHoaDonSoLuong.Minimum = 0;
                            numChiTietHoaDonSoLuong.Maximum = 0;
                            numChiTietHoaDonSoLuong.Value = 0;
                            numChiTietHoaDonSoLuong.Enabled = false;
                        }
                        numChiTietHoaDonSoLuong.ValueChanged += numChiTietHoaDonSoLuong_ValueChanged;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy thông tin sách: " + ex.Message);
            }
        }

        private void numChiTietHoaDonSoLuong_ValueChanged(object sender, EventArgs e)
        {
            if (cbChiTietHoaDonSach == null || cbChiTietHoaDonSach.SelectedIndex == -1 || cbChiTietHoaDonSach.SelectedValue == null) return;
            if (!int.TryParse(cbChiTietHoaDonSach.SelectedValue.ToString(), out int maSach)) return;

            int soLuongMua = Convert.ToInt32(numChiTietHoaDonSoLuong.Value);
            try
            {
                using (QuanLyBanSachDataContext db = LayDataContext())
                {
                    var sach = db.tbl_saches.FirstOrDefault(x => x.ma_sach == maSach);
                    if (sach == null) return;

                    int tonKho = sach.so_luong ?? 0;
                    if (soLuongMua > tonKho)
                    {
                        MessageBox.Show($"Số lượng tồn kho chỉ còn {tonKho} cuốn!", "Cảnh báo tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        numChiTietHoaDonSoLuong.Value = tonKho;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật số lượng: " + ex.Message);
            }
        }

        private void btnChiTietHoaDonThem_Click_1(object sender, EventArgs e)
        {
            if (maHoaDonDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần thêm chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbChiTietHoaDonSach == null || cbChiTietHoaDonSach.SelectedIndex == -1 || cbChiTietHoaDonSach.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sách cần thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maSach = Convert.ToInt32(cbChiTietHoaDonSach.SelectedValue);
            int soLuongThem = numChiTietHoaDonSoLuong != null ? Convert.ToInt32(numChiTietHoaDonSoLuong.Value) : 0;
            if (soLuongThem <= 0) return;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (QuanLyBanSachDataContext db = LayDataContext())
                    {
                        var sach = db.tbl_saches.FirstOrDefault(x => x.ma_sach == maSach);
                        int tonKho = sach.so_luong ?? 0;
                        if (tonKho < soLuongThem)
                        {
                            MessageBox.Show($"Số lượng sách trong kho không đủ! Chỉ còn {tonKho} cuốn.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        var chiTietCu = db.tbl_chi_tiet_hoa_dons.FirstOrDefault(x => x.ma_hoa_don == maHoaDonDangChon.Value && x.ma_sach == maSach);
                        if (chiTietCu != null)
                        {
                            chiTietCu.so_luong = (chiTietCu.so_luong ?? 0) + soLuongThem;
                        }
                        else
                        {
                            tbl_chi_tiet_hoa_don cthdMoi = new tbl_chi_tiet_hoa_don
                            {
                                ma_hoa_don = maHoaDonDangChon.Value,
                                ma_sach = maSach,
                                so_luong = soLuongThem
                            };
                            db.tbl_chi_tiet_hoa_dons.InsertOnSubmit(cthdMoi);
                        }

                        sach.so_luong = tonKho - soLuongThem;
                        db.SubmitChanges();
                    }
                    scope.Complete();
                }

                MessageBox.Show("Thêm chi tiết hóa đơn thành công!");
                LoadChiTietHoaDon(maHoaDonDangChon.Value);
                LoadSach();
                cbChiTietHoaDonSach.SelectedIndex = -1;
                numChiTietHoaDonSoLuong.Value = 1;
                maSachChiTietHoaDonDangChon = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm chi tiết hóa đơn: " + ex.Message);
            }
        }

        private void btnChiTietHoaDonXoa_Click_1(object sender, EventArgs e)
        {
            if (maHoaDonDangChon == null || maSachChiTietHoaDonDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn dòng sách trong bảng chi tiết hóa đơn cần xóa!");
                return;
            }

            if (MessageBox.Show("Xóa cuốn sách này khỏi hóa đơn và hoàn trả kho?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (QuanLyBanSachDataContext db = LayDataContext())
                    {
                        var cthd = db.tbl_chi_tiet_hoa_dons.FirstOrDefault(x => x.ma_hoa_don == maHoaDonDangChon.Value && x.ma_sach == maSachChiTietHoaDonDangChon.Value);
                        if (cthd != null)
                        {
                            int soLuongHoanTra = cthd.so_luong ?? 0;
                            var sach = db.tbl_saches.FirstOrDefault(x => x.ma_sach == maSachChiTietHoaDonDangChon.Value);
                            if (sach != null)
                            {
                                sach.so_luong = (sach.so_luong ?? 0) + soLuongHoanTra;
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

        private void LoadChiTietPhieuNhap(int maPhieuNhap)
        {
            try
            {
                using (QuanLyBanSachDataContext db = LayDataContext())
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
                    {
                        dgChiTietPhieuNhap.DataSource = null;
                        dgChiTietPhieuNhap.DataSource = danhSach;
                        if (dgChiTietPhieuNhap.Columns["ma_phieu_nhap"] != null) dgChiTietPhieuNhap.Columns["ma_phieu_nhap"].HeaderText = "Mã phiếu";
                        if (dgChiTietPhieuNhap.Columns["ma_sach"] != null) dgChiTietPhieuNhap.Columns["ma_sach"].HeaderText = "Mã sách";
                        if (dgChiTietPhieuNhap.Columns["TenSach"] != null) dgChiTietPhieuNhap.Columns["TenSach"].HeaderText = "Tên sách";
                        if (dgChiTietPhieuNhap.Columns["so_luong"] != null) dgChiTietPhieuNhap.Columns["so_luong"].HeaderText = "Số lượng nhập";
                    }
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

            object valMaSach = row.Cells["ma_sach"]?.Value;
            if (valMaSach != null && valMaSach != DBNull.Value)
            {
                maSachChiTietPhieuNhapDangChon = Convert.ToInt32(valMaSach);
                if (cbChiTietPhieuNhapSach != null) cbChiTietPhieuNhapSach.SelectedValue = maSachChiTietPhieuNhapDangChon.Value;

                object valSL = row.Cells["so_luong"]?.Value;
                if (numChiTietPhieuNhapSoLuong != null && valSL != null && valSL != DBNull.Value)
                {
                    decimal sl = Convert.ToDecimal(valSL);
                    numChiTietPhieuNhapSoLuong.Value = Math.Min(Math.Max(sl, numChiTietPhieuNhapSoLuong.Minimum), numChiTietPhieuNhapSoLuong.Maximum);
                }
            }
        }

        private void cbChiTietPhieuNhapSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbChiTietPhieuNhapSach == null || cbChiTietPhieuNhapSach.SelectedIndex == -1 || cbChiTietPhieuNhapSach.SelectedValue == null) return;
            if (numChiTietPhieuNhapSoLuong != null) numChiTietPhieuNhapSoLuong.Value = 1;
        }

        private void numChiTietPhieuNhapSoLuong_ValueChanged(object sender, EventArgs e)
        {
            if (numChiTietPhieuNhapSoLuong != null && numChiTietPhieuNhapSoLuong.Value < 1)
            {
                numChiTietPhieuNhapSoLuong.Value = 1;
            }
        }

        private void btnChiTietPhieuNhapThem_Click_1(object sender, EventArgs e)
        {
            if (maPhieuNhapDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập cần thêm chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbChiTietPhieuNhapSach == null || cbChiTietPhieuNhapSach.SelectedIndex == -1 || cbChiTietPhieuNhapSach.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sách cần nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maSach = Convert.ToInt32(cbChiTietPhieuNhapSach.SelectedValue);
            int soLuongNhap = numChiTietPhieuNhapSoLuong != null ? Convert.ToInt32(numChiTietPhieuNhapSoLuong.Value) : 0;
            if (soLuongNhap <= 0) return;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (QuanLyBanSachDataContext db = LayDataContext())
                    {
                        var sach = db.tbl_saches.FirstOrDefault(x => x.ma_sach == maSach);
                        if (sach == null) return;

                        var chiTietCu = db.tbl_chi_tiet_phieu_nhaps.FirstOrDefault(x => x.ma_phieu_nhap == maPhieuNhapDangChon.Value && x.ma_sach == maSach);
                        if (chiTietCu != null)
                        {
                            chiTietCu.so_luong = (chiTietCu.so_luong ?? 0) + soLuongNhap;
                        }
                        else
                        {
                            tbl_chi_tiet_phieu_nhap ctpnMoi = new tbl_chi_tiet_phieu_nhap
                            {
                                ma_phieu_nhap = maPhieuNhapDangChon.Value,
                                ma_sach = maSach,
                                so_luong = soLuongNhap
                            };
                            db.tbl_chi_tiet_phieu_nhaps.InsertOnSubmit(ctpnMoi);
                        }

                        sach.so_luong = (sach.so_luong ?? 0) + soLuongNhap;
                        db.SubmitChanges();
                    }
                    scope.Complete();
                }

                MessageBox.Show("Thêm chi tiết phiếu nhập thành công!");
                LoadChiTietPhieuNhap(maPhieuNhapDangChon.Value);
                LoadSach();
                cbChiTietPhieuNhapSach.SelectedIndex = -1;
                numChiTietPhieuNhapSoLuong.Value = 1;
                maSachChiTietPhieuNhapDangChon = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm chi tiết phiếu nhập: " + ex.Message);
            }
        }

        private void btnChiTietPhieuNhapXoa_Click_1(object sender, EventArgs e)
        {
            if (maPhieuNhapDangChon == null || maSachChiTietPhieuNhapDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn dòng sách trong bảng chi tiết phiếu nhập cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Xóa sách này khỏi phiếu nhập và trừ kho tương ứng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (QuanLyBanSachDataContext db = LayDataContext())
                    {
                        var ctpn = db.tbl_chi_tiet_phieu_nhaps.FirstOrDefault(x => x.ma_phieu_nhap == maPhieuNhapDangChon.Value && x.ma_sach == maSachChiTietPhieuNhapDangChon.Value);
                        if (ctpn != null)
                        {
                            int soLuongHuy = ctpn.so_luong ?? 0;
                            var sach = db.tbl_saches.FirstOrDefault(x => x.ma_sach == maSachChiTietPhieuNhapDangChon.Value);
                            if (sach != null)
                            {
                                sach.so_luong = Math.Max(0, (sach.so_luong ?? 0) - soLuongHuy);
                            }

                            db.tbl_chi_tiet_phieu_nhaps.DeleteOnSubmit(ctpn);
                            db.SubmitChanges();
                        }
                    }
                    scope.Complete();
                }

                MessageBox.Show("Đã xóa chi tiết phiếu nhập và cập nhật kho thành công!");
                LoadChiTietPhieuNhap(maPhieuNhapDangChon.Value);
                LoadSach();
                maSachChiTietPhieuNhapDangChon = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa chi tiết phiếu nhập: " + ex.Message);
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (maHoaDonDangChon == null) { MessageBox.Show("Vui lòng chọn hóa đơn cần in!"); return; }
            InBaoCao.InHoaDon(maHoaDonDangChon.Value, this);
        }

        private void btnInPhieuNhap_Click(object sender, EventArgs e)
        {
            if (maPhieuNhapDangChon == null) { MessageBox.Show("Vui lòng chọn phiếu nhập cần in!"); return; }
            InBaoCao.InPhieuNhap(maPhieuNhapDangChon.Value, this);
        }

        // --- CÁC SỰ KIỆN MENU ---
        private void menuSach_Click(object sender, EventArgs e) => MoForm(new frmSach());
        private void menuLoaiSach_Click(object sender, EventArgs e) => MoForm(new frmLoaiSach());
        private void menuKhachHang_Click(object sender, EventArgs e) => MoForm(new frmKhachHang());
        private void menuNhaCungCap_Click(object sender, EventArgs e) => MoForm(new frmNhaCungCap());
        private void menuHoaDon_Click(object sender, EventArgs e) => MoForm(new frmHoaDon());
        private void menuPhieuNhap_Click(object sender, EventArgs e) => MoForm(new FrmPhieuNhap());
        private void menuThongKe_Click(object sender, EventArgs e) => MoForm(new frmThongKeDoanhThu());
        private void menuTaiKhoan_Click(object sender, EventArgs e) => MoForm(new frmQuanLyTaiKhoan());
        private void menuPhanQuyen_Click(object sender, EventArgs e) => MoForm(new frmQuanLyPhanQuyen());
        private void menuDoiMatKhau_Click(object sender, EventArgs e) => MoForm(new frmDoiMatKhau(TenDangNhapHienTai));

        private void menuDangXuat_Click(object sender, EventArgs e)
        {
            DangXuat = true;
            this.Close();
        }

        private void menuThoat_Click(object sender, EventArgs e) => Application.Exit();
        private void báoCáoToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void danhMụcToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void loạiSáchToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void dgSach_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void txtSachTacGia_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
    }
}