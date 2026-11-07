using System;
using System.Windows.Forms;

namespace QuanLyBanSach_LINQ_SQL
{
    /// <summary>Mô tả 1 cột của DataGridView.</summary>
    public class CotLuoi
    {
        public string Ten { get; private set; }       // Name của cột -> dùng cho row.Cells["..."]
        public string TieuDe { get; private set; }    // Chữ hiển thị trên tiêu đề cột
        public string DuLieu { get; private set; }    // Tên thuộc tính dữ liệu (DataPropertyName)
        public string DinhDang { get; private set; }  // Ví dụ "N0", "dd/MM/yyyy"
        public bool CanPhai { get; private set; }

        public CotLuoi(string ten, string tieuDe, string duLieu, string dinhDang = null, bool canPhai = false)
        {
            Ten = ten;
            TieuDe = tieuDe;
            DuLieu = duLieu;
            DinhDang = dinhDang;
            CanPhai = canPhai;
        }
    }

    public static class LuoiHelper
    {
        /// <summary>
        /// Tạo lại toàn bộ cột cho lưới bằng code (tên cột + DataPropertyName chuẩn),
        /// thay cho các cột Column1, Column2... trong Designer vốn không gắn dữ liệu.
        /// Gọi ngay sau InitializeComponent().
        /// </summary>
        public static void DatCot(DataGridView dg, params CotLuoi[] cacCot)
        {
            dg.AutoGenerateColumns = false;
            dg.Columns.Clear();

            foreach (CotLuoi c in cacCot)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.Name = c.Ten;
                col.HeaderText = c.TieuDe;
                col.DataPropertyName = c.DuLieu;
                col.ReadOnly = true;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                if (!string.IsNullOrEmpty(c.DinhDang))
                    col.DefaultCellStyle.Format = c.DinhDang;

                if (c.CanPhai)
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dg.Columns.Add(col);
            }

            dg.AllowUserToAddRows = false;
            dg.AllowUserToDeleteRows = false;
            dg.ReadOnly = true;
            dg.MultiSelect = false;
            dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
