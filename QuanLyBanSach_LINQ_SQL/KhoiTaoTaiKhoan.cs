using System;
using System.Linq;

namespace QuanLyBanSach_LINQ_SQL
{
    public class KhoiTaoTaiKhoan
    {
        public static string TaoTaiKhoanAdmin()
        {
            try
            {
                string connStr = Properties.Settings.Default.db_QuanLyBanSachConnectionString;
                using (QuanLyBanSachDataContext db = new QuanLyBanSachDataContext(connStr))
                {
                    if (db.tbl_nguoi_dungs.Any())
                    {
                        return "Database đã có tài khoản. Không cần tạo thêm!";
                    }

                    var quyenAdmin = db.tbl_phan_quyens
                        .FirstOrDefault(x => x.ten_phan_quyen == "Quản trị viên");

                    if (quyenAdmin == null)
                    {
                        return "Không tìm thấy nhóm quyền Quản trị viên!";
                    }

                    string salt = PasswordHelper.TaoSalt();
                    string matKhauHash = PasswordHelper.MaHoaMatKhau("Admin@123", salt);

                    tbl_nguoi_dung taiKhoan = new tbl_nguoi_dung
                    {
                        ten_dang_nhap = "admin",
                        mat_khau_hash = matKhauHash,
                        salt = salt,
                        ho_ten = "Quản trị viên",
                        ma_phan_quyen = quyenAdmin.ma_phan_quyen,
                        trang_thai = true,
                        ngay_tao = DateTime.Now
                    };

                    db.tbl_nguoi_dungs.InsertOnSubmit(taiKhoan);
                    db.SubmitChanges();

                    return "Tạo tài khoản Quản trị viên thành công!";
                }
            }
            catch (Exception ex)
            {
                return "Lỗi tạo tài khoản: " + ex.Message;
            }
        }
    }
}