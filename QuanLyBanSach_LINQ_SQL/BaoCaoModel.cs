namespace QuanLyBanSach_LINQ_SQL
{
    /// <summary>
    /// 1 dòng dữ liệu đưa vào ReportViewer (DataSet "DataSetBaoCao" trong file .rdlc).
    /// Phải là class public có thuộc tính public thì ReportViewer mới đọc được.
    /// </summary>
    public class DongBaoCao
    {
        public int STT { get; set; }
        public string TenSach { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
    }
}
