using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyBanSach_LINQ_SQL
{
    public partial class frmChartDashboard : Form
    {
        // Khởi tạo DataContext kết nối cơ sở dữ liệu
        private QuanLyBanSachDataContext LayDataContext()
        {
            return new QuanLyBanSachDataContext(Properties.Settings.Default.db_QuanLyBanSachConnectionString);
        }

        public frmChartDashboard()
        {
            InitializeComponent();
        }

        // Sự kiện khi form vừa mở
        private void frmChartDashboard_Load(object sender, EventArgs e)
        {
            try
            {
                if (radTuanNay.Checked)
                    ThongKeTheoTuanNay();
                else
                    radTuanNay.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu biểu đồ: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------------
        // HÀM VẼ DỮ LIỆU LÊN BIỂU ĐỒ chrtDoanhThu
        // -------------------------------------------------------------
        private void VeBieuDoDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                using (var db = LayDataContext())
                {
                    DateTime batDau = tuNgay.Date;
                    DateTime ketThuc = denNgay.Date.AddDays(1).AddTicks(-1);

                    // 1. Lấy các dòng chi tiết trong khoảng ngày, tính thành tiền từng dòng
                    var dong = (from hd in db.GetTable<tbl_hoa_don>()
                                join ct in db.GetTable<tbl_chi_tiet_hoa_don>() on hd.ma_hoa_don equals ct.ma_hoa_don
                                join s in db.GetTable<tbl_sach>() on ct.ma_sach equals s.ma_sach
                                where hd.ngay_lap_hoa_don >= batDau && hd.ngay_lap_hoa_don <= ketThuc
                                select new
                                {
                                    Ngay = hd.ngay_lap_hoa_don.Value,
                                    ThanhTien = (ct.so_luong ?? 0) * (s.gia_ban ?? 0m)
                                }).ToList();

                    // Cộng dồn theo ngày
                    var dataDB = dong
                        .GroupBy(x => x.Ngay.Date)
                        .Select(g => new { Ngay = g.Key, DoanhThu = g.Sum(x => x.ThanhTien) })
                        .ToList();

                    // 2. Tạo đủ mọi ngày trong khoảng để trục X luôn liên tục
                    var danhSachNgay = new System.Collections.Generic.List<KeyValuePair<DateTime, decimal>>();
                    for (DateTime d = batDau; d <= denNgay.Date; d = d.AddDays(1))
                    {
                        var record = dataDB.FirstOrDefault(x => x.Ngay == d);
                        danhSachNgay.Add(new KeyValuePair<DateTime, decimal>(d, record != null ? record.DoanhThu : 0m));
                    }

                    // 3. Cấu hình lại biểu đồ
                    chrtDoanhThu.Series.Clear();
                    chrtDoanhThu.Titles.Clear();

                    Title tieuDe = new Title($"THỐNG KÊ DOANH THU ({tuNgay:dd/MM/yyyy} - {denNgay:dd/MM/yyyy})");
                    tieuDe.Font = new Font("Segoe UI", 13, FontStyle.Bold);
                    tieuDe.ForeColor = Color.DarkSlateBlue;
                    chrtDoanhThu.Titles.Add(tieuDe);

                    Series series = new Series("DoanhThu")
                    {
                        ChartType = SeriesChartType.Column,
                        IsValueShownAsLabel = true, // Hiện số tiền trên đầu cột
                        Color = Color.CornflowerBlue
                    };

                    foreach (var item in danhSachNgay)
                    {
                        int pIndex = series.Points.AddXY(item.Key.ToString("dd/MM"), item.Value);
                        // Nếu doanh thu > 0 thì hiển thị nhãn tiền, bằng 0 thì để trống cho đỡ rối
                        if (item.Value > 0)
                        {
                            series.Points[pIndex].Label = item.Value.ToString("N0") + " đ";
                        }
                    }

                    chrtDoanhThu.Series.Add(series);

                    // Cấu hình hiển thị trục
                    if (chrtDoanhThu.ChartAreas.Count > 0)
                    {
                        chrtDoanhThu.ChartAreas[0].AxisX.Title = "Ngày";
                        chrtDoanhThu.ChartAreas[0].AxisX.Interval = 1;
                        chrtDoanhThu.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                        chrtDoanhThu.ChartAreas[0].AxisY.Title = "Doanh thu (VNĐ)";
                        chrtDoanhThu.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
                        chrtDoanhThu.ChartAreas[0].AxisY.LabelStyle.Format = "N0";
                    }

                    // 4. Kiểm tra nếu trong tuần không có đồng nào
                    decimal tongTien = dataDB.Sum(x => x.DoanhThu);
                    if (tongTien == 0)
                    {
                        // Kiểm tra xem trong toàn bộ CSDL đã có hóa đơn nào chưa
                        bool coHoaDonNaoChua = db.GetTable<tbl_hoa_don>().Any();
                        if (!coHoaDonNaoChua)
                        {
                            MessageBox.Show("Cơ sở dữ liệu hiện chưa có hóa đơn bán sách nào!\nHãy tạo thử 1 hóa đơn để xem cột biểu đồ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị biểu đồ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------------
        // XỬ LÝ THEO CÁC MỐC THỜI GIAN
        // -------------------------------------------------------------
        private void ThongKeTheoTuanNay()
        {
            DateTime now = DateTime.Today;
            // Xác định ngày Thứ Hai đầu tuần
            int delta = DayOfWeek.Monday - now.DayOfWeek;
            if (delta > 0) delta -= 7;
            DateTime dauTuan = now.AddDays(delta);
            DateTime cuoiTuan = dauTuan.AddDays(6);

            VeBieuDoDoanhThu(dauTuan, cuoiTuan);
        }

        private void ThongKeTheoThangNay()
        {
            DateTime now = DateTime.Today;
            DateTime dauThang = new DateTime(now.Year, now.Month, 1);
            DateTime cuoiThang = dauThang.AddMonths(1).AddDays(-1);

            VeBieuDoDoanhThu(dauThang, cuoiThang);
        }

        // Khi người dùng tích chọn "Tuần này"
        private void radTuanNay_CheckedChanged(object sender, EventArgs e)
        {
            if (radTuanNay.Checked)
            {
                ThongKeTheoTuanNay();
            }
        }

        // Sự kiện khi người dùng chọn nút Radio thứ hai (Tháng này)
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rad && rad.Checked)
            {
                ThongKeTheoThangNay();
            }
        }

        // Nút Thống Kê
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            DateTime tuNgay;
            DateTime denNgay;

            // Nếu trên form có sẵn dtpTuNgay và dtpDenNgay thì lấy theo khoảng ngày người dùng chọn
            if (this.Controls.ContainsKey("dtpTuNgay") && this.Controls.ContainsKey("dtpDenNgay"))
            {
                tuNgay = ((DateTimePicker)this.Controls["dtpTuNgay"]).Value.Date;
                denNgay = ((DateTimePicker)this.Controls["dtpDenNgay"]).Value.Date;
            }
            else
            {
                // Nếu chọn tuần này thì nạp tuần, ngược lại nạp tháng
                if (radTuanNay.Checked)
                {
                    ThongKeTheoTuanNay();
                    return;
                }
                else
                {
                    ThongKeTheoThangNay();
                    return;
                }
            }

            if (tuNgay > denNgay)
            {
                MessageBox.Show("Từ ngày không được lớn hơn đến ngày!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            VeBieuDoDoanhThu(tuNgay, denNgay);
        }

        private void chrtDoanhThu_Click(object sender, EventArgs e)
        {
            // Chuyển EventArgs sang MouseEventArgs để lấy tọa độ chuột bấm
            MouseEventArgs me = e as MouseEventArgs;
            if (me == null) return;

            // Xác định đối tượng tại vị trí nhấp chuột
            HitTestResult result = chrtDoanhThu.HitTest(me.X, me.Y);

            // Nếu người dùng bấm trúng vào một điểm dữ liệu (cột biểu đồ)
            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                DataPoint point = result.Series.Points[result.PointIndex];
                string ngay = point.AxisLabel;
                double doanhThu = point.YValues[0];

                MessageBox.Show(
                    $"Ngày: {ngay}\nDoanh thu: {doanhThu:N0} VNĐ",
                    "Chi tiết doanh thu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }
    }
}