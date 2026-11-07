using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanSach_LINQ_SQL
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ReportViewer cần nạp thư viện native của SQL Server Types (x86/x64)
            try
            {
                SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            }
            catch
            {
                // Không bắt buộc: chỉ ảnh hưởng báo cáo có dữ liệu không gian (geography), báo cáo của dự án không dùng
            }

            // Tạo tài khoản admin mặc định nếu CSDL chưa có tài khoản nào
            string ketQua = KhoiTaoTaiKhoan.TaoTaiKhoanAdmin();
            if (ketQua.StartsWith("Lỗi") || ketQua.StartsWith("Không tìm thấy"))
            {
                MessageBox.Show(
                    ketQua +
                    "\n\nKiểm tra lại: (1) SQL Server đang chạy, (2) chuỗi kết nối trong App.config," +
                    " (3) đã tạo database db_QuanLyBanSach cùng dữ liệu nhóm quyền \"Quản trị viên\".",
                    "Không khởi tạo được dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            Application.Run(new frmLogin());
        }
    }
}