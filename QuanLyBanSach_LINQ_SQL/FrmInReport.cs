using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace QuanLyBanSach_LINQ_SQL
{
    /// <summary>Form hiển thị / in báo cáo bằng ReportViewer (chế độ Local, file .rdlc nhúng trong exe).</summary>
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

        /// <param name="tenReportNhung">Ví dụ: "QuanLyBanSach_LINQ_SQL.rptHoaDon.rdlc"</param>
        public FrmInReport(string tenReportNhung, string tieuDe,
                           IList<DongBaoCao> duLieu, IEnumerable<ReportParameter> thamSo) : this()
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
                this.Text = "BÁO CÁO - " + (_tieuDe ?? "");

                reportViewer1.Reset();
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                reportViewer1.LocalReport.ReportEmbeddedResource = _tenReportNhung;

                // Tên nguồn dữ liệu phải trùng tên DataSet trong file .rdlc: "DataSetBaoCao"
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("DataSetBaoCao", _duLieu));

                if (_thamSo != null)
                    reportViewer1.LocalReport.SetParameters(_thamSo);

                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.PageWidth;
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                string chiTiet = ex.Message;
                if (ex.InnerException != null)
                    chiTiet += "\n" + ex.InnerException.Message;

                MessageBox.Show(
                    "Lỗi hiển thị báo cáo:\n" + chiTiet,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
