using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace QuanLyBanSach_LINQ_SQL
{
    /// <summary>
    /// Form hiển thị báo cáo bằng ReportViewer (dùng chung cho Hóa đơn và Phiếu nhập).
    /// Tên file .rdlc truyền vào qua constructor (rptHoaDon.rdlc hoặc rptPhieuNhap.rdlc).
    /// </summary>
    public partial class FrmInReport : Form
    {
        private readonly string _tenReportNhung;
        private readonly string _tieuDe;
        private readonly IList<DongBaoCao> _duLieu;
        private readonly IEnumerable<ReportParameter> _thamSo;

        public FrmInReport()
        {
            InitializeComponent();
        }

        public FrmInReport(string tenReportNhung, string tieuDe, IList<DongBaoCao> duLieu, IEnumerable<ReportParameter> thamSo) : this()
        {
            _tenReportNhung = tenReportNhung;
            _tieuDe = tieuDe;
            _duLieu = duLieu;
            _thamSo = thamSo;
        }

        private void FrmInReport_Load(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_tenReportNhung))
                    throw new Exception("Chưa chỉ định file báo cáo (.rdlc) cần in!");

                this.Text = "BÁO CÁO - " + (_tieuDe ?? "");

                reportViewer1.Reset();
                reportViewer1.ProcessingMode = ProcessingMode.Local;

                // Tìm đúng file .rdlc được nhúng theo tên truyền vào
                // (chấp nhận cả "rptHoaDon.rdlc" lẫn "QuanLyBanSach_LINQ_SQL.rptHoaDon.rdlc")
                Assembly assembly = Assembly.GetExecutingAssembly();
                string tenCanTim = _tenReportNhung.Trim();
                string resourceName = assembly.GetManifestResourceNames()
                    .FirstOrDefault(r => r.Equals(tenCanTim, StringComparison.OrdinalIgnoreCase)
                                      || r.EndsWith("." + tenCanTim, StringComparison.OrdinalIgnoreCase));

                if (string.IsNullOrEmpty(resourceName))
                    throw new Exception("Không tìm thấy file báo cáo nhúng \"" + tenCanTim + "\" trong dự án!\n" +
                                        "Kiểm tra Build Action của file .rdlc phải là \"Embedded Resource\".");

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                        throw new Exception("Không thể đọc dữ liệu của file báo cáo: " + resourceName);

                    reportViewer1.LocalReport.LoadReportDefinition(stream);
                }

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("DataSetBaoCao", _duLieu));

                if (_thamSo != null)
                {
                    reportViewer1.LocalReport.SetParameters(_thamSo);
                }

                reportViewer1.ShowParameterPrompts = false;
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.PageWidth;
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị báo cáo:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}