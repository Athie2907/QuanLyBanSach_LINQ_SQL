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
    public partial class frmQuanLyPhanQuyen : Form
    {
        // Hàm đọc trực tiếp chuỗi kết nối từ App.config / Settings
        private QuanLyBanSachDataContext LayDataContext()
        {
            string connStr = Properties.Settings.Default.db_QuanLyBanSachConnectionString;
            return new QuanLyBanSachDataContext(connStr);
        }

        public frmQuanLyPhanQuyen()
        {
            InitializeComponent();
            dgChucNang.AutoGenerateColumns = false;
            dgChucNang.ReadOnly = false;
            dgChucNang.Enabled = true;
            dgChucNang.EditMode = DataGridViewEditMode.EditOnEnter;

            colMaChucNang.ReadOnly = true;
            colTenChucNang.ReadOnly = true;
            colDuocPhep.ReadOnly = false;

            LoadPhanQuyen();

            cbPhanQuyen.SelectedIndexChanged += cbPhanQuyen_SelectedIndexChanged;

            LoadChucNang();

            dgChucNang.CurrentCellDirtyStateChanged += dgChucNang_CurrentCellDirtyStateChanged;
        }

        private void LoadPhanQuyen()
        {
            try
            {
                using (QuanLyBanSachDataContext db = LayDataContext())
                {
                    var danhSach = db.tbl_phan_quyens
                        .Select(x => new
                        {
                            MaPhanQuyen = x.ma_phan_quyen,
                            TenPhanQuyen = x.ten_phan_quyen
                        })
                        .ToList();

                    cbPhanQuyen.DataSource = null;

                    // Thiết lập trước khi gán DataSource
                    cbPhanQuyen.DisplayMember = "TenPhanQuyen";
                    cbPhanQuyen.ValueMember = "MaPhanQuyen";

                    cbPhanQuyen.DataSource = danhSach;

                    cbPhanQuyen.SelectedIndex =
                        danhSach.Count > 0 ? 0 : -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải nhóm quyền: " + ex.Message);
            }
        }

        private void LoadChucNang()
        {
            if (cbPhanQuyen.SelectedValue == null ||
                !(cbPhanQuyen.SelectedValue is int))
            {
                return;
            }

            try
            {
                int maPhanQuyen =
                    (int)cbPhanQuyen.SelectedValue;

                using (QuanLyBanSachDataContext db = LayDataContext())
                {
                    var danhSach = db.tbl_chuc_nangs
                        .Select(x => new ChucNangPhanQuyen
                        {
                            MaChucNang = x.ma_chuc_nang,
                            TenChucNang = x.ten_chuc_nang,

                            DuocPhep = db.tbl_phan_quyen_chuc_nangs
                                .Any(p =>
                                    p.ma_phan_quyen == maPhanQuyen &&
                                    p.ma_chuc_nang == x.ma_chuc_nang)
                        })
                        .ToList();

                    dgChucNang.DataSource = danhSach;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chức năng: " + ex.Message);
            }
        }

        private void frmQuanLyPhanQuyen_Load(object sender, EventArgs e)
        {

        }

        private void cbPhanQuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChucNang();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cbPhanQuyen.SelectedValue == null ||
                !(cbPhanQuyen.SelectedValue is int))
            {
                MessageBox.Show("Vui lòng chọn nhóm quyền!");
                return;
            }

            int maPhanQuyen = (int)cbPhanQuyen.SelectedValue;

            try
            {
                // Kết thúc chỉnh sửa checkbox trước khi lưu
                dgChucNang.EndEdit();

                using (QuanLyBanSachDataContext db = LayDataContext())
                {
                    // Xóa các quyền cũ của nhóm quyền
                    var quyenCu = db.tbl_phan_quyen_chuc_nangs
                        .Where(x => x.ma_phan_quyen == maPhanQuyen);

                    db.tbl_phan_quyen_chuc_nangs.DeleteAllOnSubmit(quyenCu);

                    // Thêm lại các quyền được chọn
                    foreach (DataGridViewRow row in dgChucNang.Rows)
                    {
                        if (row.IsNewRow)
                            continue;

                        bool duocPhep = Convert.ToBoolean(
                            row.Cells["colDuocPhep"].Value ?? false
                        );

                        if (duocPhep)
                        {
                            int maChucNang = Convert.ToInt32(
                                row.Cells["colMaChucNang"].Value
                            );

                            tbl_phan_quyen_chuc_nang quyenMoi =
                                new tbl_phan_quyen_chuc_nang
                                {
                                    ma_phan_quyen = maPhanQuyen,
                                    ma_chuc_nang = maChucNang
                                };

                            db.tbl_phan_quyen_chuc_nangs
                                .InsertOnSubmit(quyenMoi);
                        }
                    }

                    db.SubmitChanges();
                }

                MessageBox.Show("Lưu phân quyền thành công!");

                LoadChucNang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu phân quyền: " + ex.Message);
            }
        }

        private void dgChucNang_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgChucNang.IsCurrentCellDirty)
            {
                dgChucNang.CommitEdit(
                    DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}