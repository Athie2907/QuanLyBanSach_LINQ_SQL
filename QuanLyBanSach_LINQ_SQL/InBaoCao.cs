using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace QuanLyBanSach_LINQ_SQL
{
    /// <summary>
    /// Lấy dữ liệu bằng LINQ to SQL rồi mở FrmInReport (ReportViewer).
    /// Dùng chung cho FormGiaoDien, frmHoaDon và FrmPhieuNhap để không viết lặp code.
    /// </summary>
    public static class InBaoCao
    {
        private static QuanLyBanSachDataContext TaoDataContext()
        {
            return new QuanLyBanSachDataContext(
                Properties.Settings.Default.db_QuanLyBanSachConnectionString);
        }

        public static void InHoaDon(int maHoaDon, IWin32Window chuSoHuu = null)
        {
            try
            {
                List<DongBaoCao> duLieu;
                tbl_hoa_don hd;

                using (QuanLyBanSachDataContext db = TaoDataContext())
                {
                    hd = db.tbl_hoa_dons.FirstOrDefault(x => x.ma_hoa_don == maHoaDon);
                    if (hd == null)
                    {
                        MessageBox.Show("Không tìm thấy hóa đơn cần in!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var chiTiet = (from ct in db.tbl_chi_tiet_hoa_dons
                                   join s in db.tbl_saches on ct.ma_sach equals s.ma_sach
                                   where ct.ma_hoa_don == maHoaDon
                                   orderby s.ten_sach
                                   select new
                                   {
                                       TenSach = s.ten_sach,
                                       SoLuong = ct.so_luong ?? 0,
                                       DonGia = s.gia_ban ?? 0m
                                   }).ToList();

                    if (chiTiet.Count == 0)
                    {
                        MessageBox.Show("Hóa đơn này chưa có chi tiết để in!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    duLieu = chiTiet
                        .Select((x, i) => new DongBaoCao
                        {
                            STT = i + 1,
                            TenSach = x.TenSach,
                            SoLuong = x.SoLuong,
                            DonGia = x.DonGia,
                            ThanhTien = x.SoLuong * x.DonGia
                        })
                        .ToList();
                }

                string ngayLap = hd.ngay_lap_hoa_don.HasValue
                    ? hd.ngay_lap_hoa_don.Value.ToString("dd/MM/yyyy")
                    : "";

                ReportParameter[] thamSo = new ReportParameter[]
                {
                    new ReportParameter("MaHoaDon", hd.ma_hoa_don.ToString()),
                    new ReportParameter("NgayLap", ngayLap),
                    new ReportParameter("TenKhach", hd.ten_khach_hang ?? ""),
                    new ReportParameter("SoDienThoai", hd.sdt_khach_hang ?? "")
                };

                using (FrmInReport frm = new FrmInReport(
                    "QuanLyBanSach_LINQ_SQL.rptHoaDon.rdlc",
                    "HÓA ĐƠN #" + maHoaDon,
                    duLieu,
                    thamSo))
                {
                    frm.ShowDialog(chuSoHuu);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo dữ liệu in hóa đơn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void InPhieuNhap(int maPhieuNhap, IWin32Window chuSoHuu = null)
        {
            try
            {
                List<DongBaoCao> duLieu;
                tbl_phieu_nhap pn;

                using (QuanLyBanSachDataContext db = TaoDataContext())
                {
                    pn = db.tbl_phieu_nhaps.FirstOrDefault(x => x.ma_phieu_nhap == maPhieuNhap);
                    if (pn == null)
                    {
                        MessageBox.Show("Không tìm thấy phiếu nhập cần in!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var chiTiet = (from ct in db.tbl_chi_tiet_phieu_nhaps
                                   join s in db.tbl_saches on ct.ma_sach equals s.ma_sach
                                   where ct.ma_phieu_nhap == maPhieuNhap
                                   orderby s.ten_sach
                                   select new
                                   {
                                       TenSach = s.ten_sach,
                                       SoLuong = ct.so_luong ?? 0
                                   }).ToList();

                    if (chiTiet.Count == 0)
                    {
                        MessageBox.Show("Phiếu nhập này chưa có chi tiết để in!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    duLieu = chiTiet
                        .Select((x, i) => new DongBaoCao
                        {
                            STT = i + 1,
                            TenSach = x.TenSach,
                            SoLuong = x.SoLuong
                        })
                        .ToList();
                }

                string ngayLap = pn.ngay_lap_phieu_nhap.HasValue
                    ? pn.ngay_lap_phieu_nhap.Value.ToString("dd/MM/yyyy")
                    : "";

                ReportParameter[] thamSo = new ReportParameter[]
                {
                    new ReportParameter("MaPhieuNhap", pn.ma_phieu_nhap.ToString()),
                    new ReportParameter("NgayLap", ngayLap),
                    new ReportParameter("NhaCungCap", pn.ten_nha_cung_cap ?? "")
                };

                using (FrmInReport frm = new FrmInReport(
                    "QuanLyBanSach_LINQ_SQL.rptPhieuNhap.rdlc",
                    "PHIẾU NHẬP #" + maPhieuNhap,
                    duLieu,
                    thamSo))
                {
                    frm.ShowDialog(chuSoHuu);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo dữ liệu in phiếu nhập: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
