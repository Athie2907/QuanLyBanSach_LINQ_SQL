using System;
using System.Security.Cryptography;
using System.Text;

namespace QuanLyBanSach_LINQ_SQL
{
    public class PasswordHelper
    {
        // Tạo Salt ngẫu nhiên gồm 16 byte (32 ký tự Hex)
        public static string TaoSalt()
        {
            byte[] saltBytes = new byte[16];

            using (RandomNumberGenerator rng =
                RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            return BitConverter.ToString(saltBytes)
                .Replace("-", "");
        }

        // Mã hóa mật khẩu bằng SHA-256 kết hợp Salt
        public static string MaHoaMatKhau(
            string matKhau, string salt)
        {
            string chuoiMaHoa = salt + matKhau;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(chuoiMaHoa);

                byte[] hash = sha256.ComputeHash(bytes);

                StringBuilder ketQua = new StringBuilder();

                foreach (byte b in hash)
                {
                    ketQua.Append(b.ToString("x2"));
                }

                return ketQua.ToString();
            }
        }

        // Kiểm tra mật khẩu nhập vào
        public static bool KiemTraMatKhau(
            string matKhauNhap,
            string salt,
            string matKhauHash)
        {
            string hashNhap = MaHoaMatKhau(matKhauNhap, salt);

            return string.Equals(
                hashNhap,
                matKhauHash,
                StringComparison.OrdinalIgnoreCase
            );
        }
    }
}
