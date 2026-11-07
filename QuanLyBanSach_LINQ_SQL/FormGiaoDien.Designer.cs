using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace QuanLyBanSach_LINQ_SQL
{
    partial class FormGiaoDien
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numSachGiaBan = new System.Windows.Forms.NumericUpDown();
            this.numSachSoLuong = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbSachLoaiSach = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSachTacGia = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSachTenSach = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSachSua = new System.Windows.Forms.Button();
            this.btnSachXoa = new System.Windows.Forms.Button();
            this.btnSachThem = new System.Windows.Forms.Button();
            this.dgSach = new System.Windows.Forms.DataGridView();
            this.ma_sach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ten_sach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loai_sach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tac_gia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.so_luong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gia_ban = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtLoaiSachTen = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnLoaiSachSua = new System.Windows.Forms.Button();
            this.btnLoaiSachXoa = new System.Windows.Forms.Button();
            this.btnLoaiSachThem = new System.Windows.Forms.Button();
            this.dgLoaiSach = new System.Windows.Forms.DataGridView();
            this.ma_loai_sach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ten_loai_sach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgChiTietHoaDon = new System.Windows.Forms.DataGridView();
            this.MaHoaDon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaSach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenSach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GiaBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbChiTietHoaDonSach = new System.Windows.Forms.ComboBox();
            this.btnChiTietHoaDonXoa = new System.Windows.Forms.Button();
            this.btnChiTietHoaDonThem = new System.Windows.Forms.Button();
            this.numChiTietHoaDonSoLuong = new System.Windows.Forms.NumericUpDown();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnInHoaDon = new System.Windows.Forms.Button();
            this.txtHoaDonSDTKH = new System.Windows.Forms.TextBox();
            this.txtHoaDonTenKH = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dateNgayLapHoaDon = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnHoaDonSua = new System.Windows.Forms.Button();
            this.btnHoaDonXoa = new System.Windows.Forms.Button();
            this.btnHoaDonThem = new System.Windows.Forms.Button();
            this.dgHoaDon = new System.Windows.Forms.DataGridView();
            this.ma_hoa_don = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngay_lap_hoa_don = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ten_khach_hang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sdt_khach_hang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.cbChiTietPhieuNhapSach = new System.Windows.Forms.ComboBox();
            this.numChiTietPhieuNhapSoLuong = new System.Windows.Forms.NumericUpDown();
            this.btnChiTietPhieuNhapThem = new System.Windows.Forms.Button();
            this.btnChiTietPhieuNhapXoa = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.dgChiTietPhieuNhap = new System.Windows.Forms.DataGridView();
            this.colMaPhieuNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaSach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenSach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnInPhieuNhap = new System.Windows.Forms.Button();
            this.txtPhieuNhapNhaCungCap = new System.Windows.Forms.TextBox();
            this.dateNgayLapPhieu = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnPhieuNhapSua = new System.Windows.Forms.Button();
            this.btnPhieuNhapXoa = new System.Windows.Forms.Button();
            this.btnPhieuNhapThem = new System.Windows.Forms.Button();
            this.dgPhieuNhap = new System.Windows.Forms.DataGridView();
            this.MaPhieuNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayLapPhieuNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenNhaCungCap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.danhMụcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSach = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLoaiSach = new System.Windows.Forms.ToolStripMenuItem();
            this.kháchHàngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nhàCungCấpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nghiệpVụToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPhieuNhap = new System.Windows.Forms.ToolStripMenuItem();
            this.báoCáoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thốngKêDoanhThuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nghiệpVụToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTaiKhoan = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPhanQuyen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDoiMatKhau = new System.Windows.Forms.ToolStripMenuItem();
            this.đăngXuấtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thoátToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSachGiaBan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSachSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSach)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLoaiSach)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgChiTietHoaDon)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numChiTietHoaDonSoLuong)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgHoaDon)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numChiTietPhieuNhapSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgChiTietPhieuNhap)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPhieuNhap)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 30);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(985, 430);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.dgSach);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(977, 397);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Sách";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.numSachGiaBan);
            this.panel1.Controls.Add(this.numSachSoLuong);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cbSachLoaiSach);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtSachTacGia);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtSachTenSach);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnSachSua);
            this.panel1.Controls.Add(this.btnSachXoa);
            this.panel1.Controls.Add(this.btnSachThem);
            this.panel1.Location = new System.Drawing.Point(4, 282);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(965, 101);
            this.panel1.TabIndex = 1;
            // 
            // numSachGiaBan
            // 
            this.numSachGiaBan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSachGiaBan.Location = new System.Drawing.Point(603, 54);
            this.numSachGiaBan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numSachGiaBan.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numSachGiaBan.Name = "numSachGiaBan";
            this.numSachGiaBan.Size = new System.Drawing.Size(148, 24);
            this.numSachGiaBan.TabIndex = 13;
            // 
            // numSachSoLuong
            // 
            this.numSachSoLuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSachSoLuong.Location = new System.Drawing.Point(455, 53);
            this.numSachSoLuong.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numSachSoLuong.Name = "numSachSoLuong";
            this.numSachSoLuong.Size = new System.Drawing.Size(63, 24);
            this.numSachSoLuong.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(538, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 18);
            this.label5.TabIndex = 10;
            this.label5.Text = "Giá Bán";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(372, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "Số Lượng";
            // 
            // cbSachLoaiSach
            // 
            this.cbSachLoaiSach.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSachLoaiSach.FormattingEnabled = true;
            this.cbSachLoaiSach.Location = new System.Drawing.Point(98, 52);
            this.cbSachLoaiSach.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbSachLoaiSach.Name = "cbSachLoaiSach";
            this.cbSachLoaiSach.Size = new System.Drawing.Size(260, 26);
            this.cbSachLoaiSach.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "Loại Sách";
            // 
            // txtSachTacGia
            // 
            this.txtSachTacGia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSachTacGia.Location = new System.Drawing.Point(442, 23);
            this.txtSachTacGia.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSachTacGia.Name = "txtSachTacGia";
            this.txtSachTacGia.Size = new System.Drawing.Size(166, 24);
            this.txtSachTacGia.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(372, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tác Giả";
            // 
            // txtSachTenSach
            // 
            this.txtSachTenSach.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSachTenSach.Location = new System.Drawing.Point(98, 22);
            this.txtSachTenSach.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSachTenSach.Name = "txtSachTenSach";
            this.txtSachTenSach.Size = new System.Drawing.Size(260, 24);
            this.txtSachTenSach.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tên Sách";
            // 
            // btnSachSua
            // 
            this.btnSachSua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSachSua.Location = new System.Drawing.Point(769, 37);
            this.btnSachSua.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSachSua.Name = "btnSachSua";
            this.btnSachSua.Size = new System.Drawing.Size(81, 26);
            this.btnSachSua.TabIndex = 2;
            this.btnSachSua.Text = "Sửa";
            this.btnSachSua.UseVisualStyleBackColor = true;
            this.btnSachSua.Click += new System.EventHandler(this.btnSachSua_Click_1);
            // 
            // btnSachXoa
            // 
            this.btnSachXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSachXoa.Location = new System.Drawing.Point(769, 62);
            this.btnSachXoa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSachXoa.Name = "btnSachXoa";
            this.btnSachXoa.Size = new System.Drawing.Size(81, 26);
            this.btnSachXoa.TabIndex = 1;
            this.btnSachXoa.Text = "Xóa";
            this.btnSachXoa.UseVisualStyleBackColor = true;
            this.btnSachXoa.Click += new System.EventHandler(this.btnSachXoa_Click_1);
            // 
            // btnSachThem
            // 
            this.btnSachThem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSachThem.Location = new System.Drawing.Point(769, 11);
            this.btnSachThem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSachThem.Name = "btnSachThem";
            this.btnSachThem.Size = new System.Drawing.Size(81, 26);
            this.btnSachThem.TabIndex = 0;
            this.btnSachThem.Text = "Thêm";
            this.btnSachThem.UseVisualStyleBackColor = true;
            this.btnSachThem.Click += new System.EventHandler(this.btnSachThem_Click_1);
            // 
            // dgSach
            // 
            this.dgSach.AllowUserToAddRows = false;
            this.dgSach.AllowUserToDeleteRows = false;
            this.dgSach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSach.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ma_sach,
            this.ten_sach,
            this.loai_sach,
            this.tac_gia,
            this.so_luong,
            this.gia_ban});
            this.dgSach.Location = new System.Drawing.Point(6, 4);
            this.dgSach.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgSach.Name = "dgSach";
            this.dgSach.ReadOnly = true;
            this.dgSach.RowHeadersWidth = 62;
            this.dgSach.Size = new System.Drawing.Size(967, 274);
            this.dgSach.TabIndex = 0;
            this.dgSach.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSach_CellClick_1);
            this.dgSach.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSach_CellContentClick);
            // 
            // ma_sach
            // 
            this.ma_sach.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ma_sach.DataPropertyName = "ma_sach";
            this.ma_sach.HeaderText = "Mã Sách";
            this.ma_sach.MinimumWidth = 8;
            this.ma_sach.Name = "ma_sach";
            this.ma_sach.ReadOnly = true;
            // 
            // ten_sach
            // 
            this.ten_sach.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ten_sach.DataPropertyName = "ten_sach";
            this.ten_sach.HeaderText = "Tên Sách";
            this.ten_sach.MinimumWidth = 8;
            this.ten_sach.Name = "ten_sach";
            this.ten_sach.ReadOnly = true;
            // 
            // loai_sach
            // 
            this.loai_sach.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.loai_sach.DataPropertyName = "ten_loai_sach";
            this.loai_sach.HeaderText = "Loại Sách";
            this.loai_sach.MinimumWidth = 8;
            this.loai_sach.Name = "loai_sach";
            this.loai_sach.ReadOnly = true;
            // 
            // tac_gia
            // 
            this.tac_gia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.tac_gia.DataPropertyName = "tac_gia";
            this.tac_gia.HeaderText = "Tác Giả";
            this.tac_gia.MinimumWidth = 8;
            this.tac_gia.Name = "tac_gia";
            this.tac_gia.ReadOnly = true;
            // 
            // so_luong
            // 
            this.so_luong.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.so_luong.DataPropertyName = "so_luong";
            this.so_luong.HeaderText = "Số Lượng";
            this.so_luong.MinimumWidth = 8;
            this.so_luong.Name = "so_luong";
            this.so_luong.ReadOnly = true;
            // 
            // gia_ban
            // 
            this.gia_ban.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.gia_ban.DataPropertyName = "gia_ban";
            this.gia_ban.HeaderText = "Giá Bán";
            this.gia_ban.MinimumWidth = 8;
            this.gia_ban.Name = "gia_ban";
            this.gia_ban.ReadOnly = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.dgLoaiSach);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(977, 397);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Loại Sách";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtLoaiSachTen);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.btnLoaiSachSua);
            this.panel2.Controls.Add(this.btnLoaiSachXoa);
            this.panel2.Controls.Add(this.btnLoaiSachThem);
            this.panel2.Location = new System.Drawing.Point(6, 276);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(968, 102);
            this.panel2.TabIndex = 3;
            // 
            // txtLoaiSachTen
            // 
            this.txtLoaiSachTen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoaiSachTen.Location = new System.Drawing.Point(175, 35);
            this.txtLoaiSachTen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLoaiSachTen.Name = "txtLoaiSachTen";
            this.txtLoaiSachTen.Size = new System.Drawing.Size(260, 24);
            this.txtLoaiSachTen.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(36, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(103, 18);
            this.label10.TabIndex = 3;
            this.label10.Text = "Tên Loại Sách";
            // 
            // btnLoaiSachSua
            // 
            this.btnLoaiSachSua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoaiSachSua.Location = new System.Drawing.Point(861, 38);
            this.btnLoaiSachSua.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoaiSachSua.Name = "btnLoaiSachSua";
            this.btnLoaiSachSua.Size = new System.Drawing.Size(81, 26);
            this.btnLoaiSachSua.TabIndex = 2;
            this.btnLoaiSachSua.Text = "Sửa";
            this.btnLoaiSachSua.UseVisualStyleBackColor = true;
            this.btnLoaiSachSua.Click += new System.EventHandler(this.btnLoaiSachSua_Click);
            // 
            // btnLoaiSachXoa
            // 
            this.btnLoaiSachXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoaiSachXoa.Location = new System.Drawing.Point(861, 68);
            this.btnLoaiSachXoa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoaiSachXoa.Name = "btnLoaiSachXoa";
            this.btnLoaiSachXoa.Size = new System.Drawing.Size(81, 26);
            this.btnLoaiSachXoa.TabIndex = 1;
            this.btnLoaiSachXoa.Text = "Xóa";
            this.btnLoaiSachXoa.UseVisualStyleBackColor = true;
            this.btnLoaiSachXoa.Click += new System.EventHandler(this.btnLoaiSachXoa_Click);
            // 
            // btnLoaiSachThem
            // 
            this.btnLoaiSachThem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoaiSachThem.Location = new System.Drawing.Point(861, 8);
            this.btnLoaiSachThem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoaiSachThem.Name = "btnLoaiSachThem";
            this.btnLoaiSachThem.Size = new System.Drawing.Size(81, 26);
            this.btnLoaiSachThem.TabIndex = 0;
            this.btnLoaiSachThem.Text = "Thêm";
            this.btnLoaiSachThem.UseVisualStyleBackColor = true;
            this.btnLoaiSachThem.Click += new System.EventHandler(this.btnLoaiSachThem_Click);
            // 
            // dgLoaiSach
            // 
            this.dgLoaiSach.AllowUserToAddRows = false;
            this.dgLoaiSach.AllowUserToDeleteRows = false;
            this.dgLoaiSach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLoaiSach.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ma_loai_sach,
            this.ten_loai_sach});
            this.dgLoaiSach.Location = new System.Drawing.Point(4, 8);
            this.dgLoaiSach.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgLoaiSach.Name = "dgLoaiSach";
            this.dgLoaiSach.ReadOnly = true;
            this.dgLoaiSach.RowHeadersWidth = 62;
            this.dgLoaiSach.Size = new System.Drawing.Size(965, 264);
            this.dgLoaiSach.TabIndex = 2;
            this.dgLoaiSach.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgLoaiSach_CellClick);
            // 
            // ma_loai_sach
            // 
            this.ma_loai_sach.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ma_loai_sach.DataPropertyName = "ma_loai_sach";
            this.ma_loai_sach.HeaderText = "Mã Loại Sách";
            this.ma_loai_sach.MinimumWidth = 8;
            this.ma_loai_sach.Name = "ma_loai_sach";
            this.ma_loai_sach.ReadOnly = true;
            // 
            // ten_loai_sach
            // 
            this.ten_loai_sach.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ten_loai_sach.DataPropertyName = "ten_loai_sach";
            this.ten_loai_sach.HeaderText = "Tên Loại Sách";
            this.ten_loai_sach.MinimumWidth = 8;
            this.ten_loai_sach.Name = "ten_loai_sach";
            this.ten_loai_sach.ReadOnly = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.panel3);
            this.tabPage3.Controls.Add(this.dgHoaDon);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(977, 397);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Hóa Đơn";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgChiTietHoaDon);
            this.groupBox1.Controls.Add(this.panel5);
            this.groupBox1.Location = new System.Drawing.Point(4, 199);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(970, 192);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chi tiết Hóa Đơn";
            // 
            // dgChiTietHoaDon
            // 
            this.dgChiTietHoaDon.AllowUserToAddRows = false;
            this.dgChiTietHoaDon.AllowUserToDeleteRows = false;
            this.dgChiTietHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgChiTietHoaDon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaHoaDon,
            this.MaSach,
            this.TenSach,
            this.SoLuong,
            this.GiaBan,
            this.ThanhTien});
            this.dgChiTietHoaDon.Location = new System.Drawing.Point(1, 25);
            this.dgChiTietHoaDon.Name = "dgChiTietHoaDon";
            this.dgChiTietHoaDon.ReadOnly = true;
            this.dgChiTietHoaDon.RowHeadersWidth = 51;
            this.dgChiTietHoaDon.RowTemplate.Height = 24;
            this.dgChiTietHoaDon.Size = new System.Drawing.Size(963, 89);
            this.dgChiTietHoaDon.TabIndex = 0;
            // 
            // MaHoaDon
            // 
            this.MaHoaDon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MaHoaDon.DataPropertyName = "ma_hoa_don";
            this.MaHoaDon.HeaderText = "Mã hóa đơn";
            this.MaHoaDon.MinimumWidth = 8;
            this.MaHoaDon.Name = "MaHoaDon";
            this.MaHoaDon.ReadOnly = true;
            // 
            // MaSach
            // 
            this.MaSach.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MaSach.DataPropertyName = "ma_sach";
            this.MaSach.HeaderText = "Mã sách";
            this.MaSach.MinimumWidth = 8;
            this.MaSach.Name = "MaSach";
            this.MaSach.ReadOnly = true;
            // 
            // TenSach
            // 
            this.TenSach.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TenSach.DataPropertyName = "TenSach";
            this.TenSach.HeaderText = "Tên sách";
            this.TenSach.MinimumWidth = 8;
            this.TenSach.Name = "TenSach";
            this.TenSach.ReadOnly = true;
            // 
            // SoLuong
            // 
            this.SoLuong.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SoLuong.DataPropertyName = "so_luong";
            this.SoLuong.HeaderText = "Số lượng";
            this.SoLuong.MinimumWidth = 8;
            this.SoLuong.Name = "SoLuong";
            this.SoLuong.ReadOnly = true;
            // 
            // GiaBan
            // 
            this.GiaBan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GiaBan.DataPropertyName = "GiaBan";
            this.GiaBan.HeaderText = "Giá bán";
            this.GiaBan.MinimumWidth = 8;
            this.GiaBan.Name = "GiaBan";
            this.GiaBan.ReadOnly = true;
            // 
            // ThanhTien
            // 
            this.ThanhTien.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ThanhTien.HeaderText = "Thành tiền";
            this.ThanhTien.MinimumWidth = 8;
            this.ThanhTien.Name = "ThanhTien";
            this.ThanhTien.ReadOnly = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.cbChiTietHoaDonSach);
            this.panel5.Controls.Add(this.btnChiTietHoaDonXoa);
            this.panel5.Controls.Add(this.btnChiTietHoaDonThem);
            this.panel5.Controls.Add(this.numChiTietHoaDonSoLuong);
            this.panel5.Location = new System.Drawing.Point(1, 131);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(969, 61);
            this.panel5.TabIndex = 4;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(325, 12);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 20);
            this.label13.TabIndex = 6;
            this.label13.Text = "Số Lượng";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 20);
            this.label7.TabIndex = 5;
            this.label7.Text = "Chọn Sách";
            // 
            // cbChiTietHoaDonSach
            // 
            this.cbChiTietHoaDonSach.FormattingEnabled = true;
            this.cbChiTietHoaDonSach.Location = new System.Drawing.Point(129, 4);
            this.cbChiTietHoaDonSach.Name = "cbChiTietHoaDonSach";
            this.cbChiTietHoaDonSach.Size = new System.Drawing.Size(142, 28);
            this.cbChiTietHoaDonSach.TabIndex = 4;
            this.cbChiTietHoaDonSach.SelectedIndexChanged += new System.EventHandler(this.cbChiTietHoaDonSach_SelectedIndexChanged);
            // 
            // btnChiTietHoaDonXoa
            // 
            this.btnChiTietHoaDonXoa.Location = new System.Drawing.Point(803, 5);
            this.btnChiTietHoaDonXoa.Name = "btnChiTietHoaDonXoa";
            this.btnChiTietHoaDonXoa.Size = new System.Drawing.Size(157, 35);
            this.btnChiTietHoaDonXoa.TabIndex = 3;
            this.btnChiTietHoaDonXoa.Text = "Xóa Chi Tiết";
            this.btnChiTietHoaDonXoa.UseVisualStyleBackColor = true;
            this.btnChiTietHoaDonXoa.Click += new System.EventHandler(this.btnChiTietHoaDonXoa_Click_1);
            // 
            // btnChiTietHoaDonThem
            // 
            this.btnChiTietHoaDonThem.Location = new System.Drawing.Point(629, 5);
            this.btnChiTietHoaDonThem.Name = "btnChiTietHoaDonThem";
            this.btnChiTietHoaDonThem.Size = new System.Drawing.Size(168, 35);
            this.btnChiTietHoaDonThem.TabIndex = 2;
            this.btnChiTietHoaDonThem.Text = "Thêm Chi Tiết";
            this.btnChiTietHoaDonThem.UseVisualStyleBackColor = true;
            this.btnChiTietHoaDonThem.Click += new System.EventHandler(this.btnChiTietHoaDonThem_Click_1);
            // 
            // numChiTietHoaDonSoLuong
            // 
            this.numChiTietHoaDonSoLuong.Location = new System.Drawing.Point(436, 10);
            this.numChiTietHoaDonSoLuong.Name = "numChiTietHoaDonSoLuong";
            this.numChiTietHoaDonSoLuong.Size = new System.Drawing.Size(115, 26);
            this.numChiTietHoaDonSoLuong.TabIndex = 1;
            this.numChiTietHoaDonSoLuong.ValueChanged += new System.EventHandler(this.numChiTietHoaDonSoLuong_ValueChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnInHoaDon);
            this.panel3.Controls.Add(this.txtHoaDonSDTKH);
            this.panel3.Controls.Add(this.txtHoaDonTenKH);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.dateNgayLapHoaDon);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.btnHoaDonSua);
            this.panel3.Controls.Add(this.btnHoaDonXoa);
            this.panel3.Controls.Add(this.btnHoaDonThem);
            this.panel3.Location = new System.Drawing.Point(4, 101);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(970, 93);
            this.panel3.TabIndex = 3;
            // 
            // btnInHoaDon
            // 
            this.btnInHoaDon.Location = new System.Drawing.Point(615, 6);
            this.btnInHoaDon.Name = "btnInHoaDon";
            this.btnInHoaDon.Size = new System.Drawing.Size(136, 29);
            this.btnInHoaDon.TabIndex = 19;
            this.btnInHoaDon.Text = "In Hóa Đơn";
            this.btnInHoaDon.UseVisualStyleBackColor = true;
            // 
            // txtHoaDonSDTKH
            // 
            this.txtHoaDonSDTKH.Location = new System.Drawing.Point(591, 48);
            this.txtHoaDonSDTKH.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtHoaDonSDTKH.Name = "txtHoaDonSDTKH";
            this.txtHoaDonSDTKH.Size = new System.Drawing.Size(207, 26);
            this.txtHoaDonSDTKH.TabIndex = 18;
            // 
            // txtHoaDonTenKH
            // 
            this.txtHoaDonTenKH.Location = new System.Drawing.Point(157, 44);
            this.txtHoaDonTenKH.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtHoaDonTenKH.Name = "txtHoaDonTenKH";
            this.txtHoaDonTenKH.Size = new System.Drawing.Size(271, 26);
            this.txtHoaDonTenKH.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(434, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 18);
            this.label6.TabIndex = 15;
            this.label6.Text = "SDT Khách Hàng";
            // 
            // dateNgayLapHoaDon
            // 
            this.dateNgayLapHoaDon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateNgayLapHoaDon.Location = new System.Drawing.Point(157, 6);
            this.dateNgayLapHoaDon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateNgayLapHoaDon.Name = "dateNgayLapHoaDon";
            this.dateNgayLapHoaDon.Size = new System.Drawing.Size(271, 24);
            this.dateNgayLapHoaDon.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 18);
            this.label8.TabIndex = 7;
            this.label8.Text = "Tên Khách Hàng";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(8, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(134, 18);
            this.label11.TabIndex = 3;
            this.label11.Text = "Ngày Lập Hóa Đơn";
            // 
            // btnHoaDonSua
            // 
            this.btnHoaDonSua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHoaDonSua.Location = new System.Drawing.Point(883, 32);
            this.btnHoaDonSua.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHoaDonSua.Name = "btnHoaDonSua";
            this.btnHoaDonSua.Size = new System.Drawing.Size(81, 26);
            this.btnHoaDonSua.TabIndex = 2;
            this.btnHoaDonSua.Text = "Sửa";
            this.btnHoaDonSua.UseVisualStyleBackColor = true;
            this.btnHoaDonSua.Click += new System.EventHandler(this.btnHoaDonSua_Click);
            // 
            // btnHoaDonXoa
            // 
            this.btnHoaDonXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHoaDonXoa.Location = new System.Drawing.Point(883, 62);
            this.btnHoaDonXoa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHoaDonXoa.Name = "btnHoaDonXoa";
            this.btnHoaDonXoa.Size = new System.Drawing.Size(81, 26);
            this.btnHoaDonXoa.TabIndex = 1;
            this.btnHoaDonXoa.Text = "Xóa";
            this.btnHoaDonXoa.UseVisualStyleBackColor = true;
            this.btnHoaDonXoa.Click += new System.EventHandler(this.btnHoaDonXoa_Click);
            // 
            // btnHoaDonThem
            // 
            this.btnHoaDonThem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHoaDonThem.Location = new System.Drawing.Point(880, 2);
            this.btnHoaDonThem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHoaDonThem.Name = "btnHoaDonThem";
            this.btnHoaDonThem.Size = new System.Drawing.Size(81, 26);
            this.btnHoaDonThem.TabIndex = 0;
            this.btnHoaDonThem.Text = "Thêm";
            this.btnHoaDonThem.UseVisualStyleBackColor = true;
            this.btnHoaDonThem.Click += new System.EventHandler(this.btnHoaDonThem_Click);
            // 
            // dgHoaDon
            // 
            this.dgHoaDon.AllowUserToAddRows = false;
            this.dgHoaDon.AllowUserToDeleteRows = false;
            this.dgHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgHoaDon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ma_hoa_don,
            this.ngay_lap_hoa_don,
            this.ten_khach_hang,
            this.sdt_khach_hang});
            this.dgHoaDon.Location = new System.Drawing.Point(4, 8);
            this.dgHoaDon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgHoaDon.Name = "dgHoaDon";
            this.dgHoaDon.ReadOnly = true;
            this.dgHoaDon.RowHeadersWidth = 62;
            this.dgHoaDon.Size = new System.Drawing.Size(970, 89);
            this.dgHoaDon.TabIndex = 2;
            this.dgHoaDon.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgHoaDon_CellClick);
            // 
            // ma_hoa_don
            // 
            this.ma_hoa_don.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ma_hoa_don.DataPropertyName = "ma_hoa_don";
            this.ma_hoa_don.HeaderText = "Mã Hóa Đơn";
            this.ma_hoa_don.MinimumWidth = 8;
            this.ma_hoa_don.Name = "ma_hoa_don";
            this.ma_hoa_don.ReadOnly = true;
            // 
            // ngay_lap_hoa_don
            // 
            this.ngay_lap_hoa_don.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ngay_lap_hoa_don.DataPropertyName = "ngay_lap_hoa_don";
            this.ngay_lap_hoa_don.HeaderText = "Ngày lập";
            this.ngay_lap_hoa_don.MinimumWidth = 8;
            this.ngay_lap_hoa_don.Name = "ngay_lap_hoa_don";
            this.ngay_lap_hoa_don.ReadOnly = true;
            // 
            // ten_khach_hang
            // 
            this.ten_khach_hang.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ten_khach_hang.DataPropertyName = "ten_khach_hang";
            this.ten_khach_hang.HeaderText = "Tên khách ";
            this.ten_khach_hang.MinimumWidth = 8;
            this.ten_khach_hang.Name = "ten_khach_hang";
            this.ten_khach_hang.ReadOnly = true;
            // 
            // sdt_khach_hang
            // 
            this.sdt_khach_hang.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sdt_khach_hang.DataPropertyName = "sdt_khach_hang";
            this.sdt_khach_hang.HeaderText = "Số điện thoại";
            this.sdt_khach_hang.MinimumWidth = 8;
            this.sdt_khach_hang.Name = "sdt_khach_hang";
            this.sdt_khach_hang.ReadOnly = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox2);
            this.tabPage4.Controls.Add(this.panel4);
            this.tabPage4.Controls.Add(this.dgPhieuNhap);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage4.Size = new System.Drawing.Size(977, 397);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Phiếu Nhập";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel6);
            this.groupBox2.Controls.Add(this.dgChiTietPhieuNhap);
            this.groupBox2.Location = new System.Drawing.Point(0, 200);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(981, 179);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chi tiết Phiếu Nhập ";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.cbChiTietPhieuNhapSach);
            this.panel6.Controls.Add(this.numChiTietPhieuNhapSoLuong);
            this.panel6.Controls.Add(this.btnChiTietPhieuNhapThem);
            this.panel6.Controls.Add(this.btnChiTietPhieuNhapXoa);
            this.panel6.Controls.Add(this.label15);
            this.panel6.Controls.Add(this.label14);
            this.panel6.Location = new System.Drawing.Point(4, 132);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(970, 50);
            this.panel6.TabIndex = 1;
            // 
            // cbChiTietPhieuNhapSach
            // 
            this.cbChiTietPhieuNhapSach.FormattingEnabled = true;
            this.cbChiTietPhieuNhapSach.Location = new System.Drawing.Point(117, 10);
            this.cbChiTietPhieuNhapSach.Name = "cbChiTietPhieuNhapSach";
            this.cbChiTietPhieuNhapSach.Size = new System.Drawing.Size(170, 28);
            this.cbChiTietPhieuNhapSach.TabIndex = 5;
            this.cbChiTietPhieuNhapSach.SelectedIndexChanged += new System.EventHandler(this.cbChiTietPhieuNhapSach_SelectedIndexChanged);
            // 
            // numChiTietPhieuNhapSoLuong
            // 
            this.numChiTietPhieuNhapSoLuong.Location = new System.Drawing.Point(444, 13);
            this.numChiTietPhieuNhapSoLuong.Name = "numChiTietPhieuNhapSoLuong";
            this.numChiTietPhieuNhapSoLuong.Size = new System.Drawing.Size(101, 26);
            this.numChiTietPhieuNhapSoLuong.TabIndex = 4;
            this.numChiTietPhieuNhapSoLuong.ValueChanged += new System.EventHandler(this.numChiTietPhieuNhapSoLuong_ValueChanged);
            // 
            // btnChiTietPhieuNhapThem
            // 
            this.btnChiTietPhieuNhapThem.Location = new System.Drawing.Point(628, 11);
            this.btnChiTietPhieuNhapThem.Name = "btnChiTietPhieuNhapThem";
            this.btnChiTietPhieuNhapThem.Size = new System.Drawing.Size(175, 30);
            this.btnChiTietPhieuNhapThem.TabIndex = 3;
            this.btnChiTietPhieuNhapThem.Text = "Thêm Chi Tiết";
            this.btnChiTietPhieuNhapThem.UseVisualStyleBackColor = true;
            this.btnChiTietPhieuNhapThem.Click += new System.EventHandler(this.btnChiTietPhieuNhapThem_Click_1);
            // 
            // btnChiTietPhieuNhapXoa
            // 
            this.btnChiTietPhieuNhapXoa.Location = new System.Drawing.Point(808, 10);
            this.btnChiTietPhieuNhapXoa.Name = "btnChiTietPhieuNhapXoa";
            this.btnChiTietPhieuNhapXoa.Size = new System.Drawing.Size(155, 31);
            this.btnChiTietPhieuNhapXoa.TabIndex = 2;
            this.btnChiTietPhieuNhapXoa.Text = "Xóa Chi Tiết";
            this.btnChiTietPhieuNhapXoa.UseVisualStyleBackColor = true;
            this.btnChiTietPhieuNhapXoa.Click += new System.EventHandler(this.btnChiTietPhieuNhapXoa_Click_1);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(335, 14);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(88, 20);
            this.label15.TabIndex = 1;
            this.label15.Text = "Số Lượng";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(5, 14);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 20);
            this.label14.TabIndex = 0;
            this.label14.Text = "Chọn Sách";
            // 
            // dgChiTietPhieuNhap
            // 
            this.dgChiTietPhieuNhap.AllowUserToAddRows = false;
            this.dgChiTietPhieuNhap.AllowUserToDeleteRows = false;
            this.dgChiTietPhieuNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgChiTietPhieuNhap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaPhieuNhap,
            this.colMaSach,
            this.colTenSach,
            this.colSoLuong});
            this.dgChiTietPhieuNhap.Location = new System.Drawing.Point(3, 25);
            this.dgChiTietPhieuNhap.Name = "dgChiTietPhieuNhap";
            this.dgChiTietPhieuNhap.ReadOnly = true;
            this.dgChiTietPhieuNhap.RowHeadersWidth = 51;
            this.dgChiTietPhieuNhap.RowTemplate.Height = 24;
            this.dgChiTietPhieuNhap.Size = new System.Drawing.Size(971, 101);
            this.dgChiTietPhieuNhap.TabIndex = 0;
            // 
            // colMaPhieuNhap
            // 
            this.colMaPhieuNhap.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMaPhieuNhap.DataPropertyName = "ma_phieu_nhap";
            this.colMaPhieuNhap.HeaderText = "Mã phiếu nhập";
            this.colMaPhieuNhap.MinimumWidth = 8;
            this.colMaPhieuNhap.Name = "colMaPhieuNhap";
            this.colMaPhieuNhap.ReadOnly = true;
            // 
            // colMaSach
            // 
            this.colMaSach.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMaSach.DataPropertyName = "ma_sach";
            this.colMaSach.HeaderText = "Mã sách";
            this.colMaSach.MinimumWidth = 8;
            this.colMaSach.Name = "colMaSach";
            this.colMaSach.ReadOnly = true;
            // 
            // colTenSach
            // 
            this.colTenSach.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTenSach.DataPropertyName = "TenSach";
            this.colTenSach.HeaderText = "Tên sách";
            this.colTenSach.MinimumWidth = 8;
            this.colTenSach.Name = "colTenSach";
            this.colTenSach.ReadOnly = true;
            // 
            // colSoLuong
            // 
            this.colSoLuong.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colSoLuong.DataPropertyName = "so_luong";
            this.colSoLuong.HeaderText = "Số lượng";
            this.colSoLuong.MinimumWidth = 8;
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.ReadOnly = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnInPhieuNhap);
            this.panel4.Controls.Add(this.txtPhieuNhapNhaCungCap);
            this.panel4.Controls.Add(this.dateNgayLapPhieu);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.btnPhieuNhapSua);
            this.panel4.Controls.Add(this.btnPhieuNhapXoa);
            this.panel4.Controls.Add(this.btnPhieuNhapThem);
            this.panel4.Location = new System.Drawing.Point(4, 117);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(970, 78);
            this.panel4.TabIndex = 5;
            // 
            // btnInPhieuNhap
            // 
            this.btnInPhieuNhap.Location = new System.Drawing.Point(540, 18);
            this.btnInPhieuNhap.Name = "btnInPhieuNhap";
            this.btnInPhieuNhap.Size = new System.Drawing.Size(163, 34);
            this.btnInPhieuNhap.TabIndex = 18;
            this.btnInPhieuNhap.Text = "In Phiếu Nhập";
            this.btnInPhieuNhap.UseVisualStyleBackColor = true;
            // 
            // txtPhieuNhapNhaCungCap
            // 
            this.txtPhieuNhapNhaCungCap.Location = new System.Drawing.Point(173, 47);
            this.txtPhieuNhapNhaCungCap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPhieuNhapNhaCungCap.Name = "txtPhieuNhapNhaCungCap";
            this.txtPhieuNhapNhaCungCap.Size = new System.Drawing.Size(271, 26);
            this.txtPhieuNhapNhaCungCap.TabIndex = 17;
            // 
            // dateNgayLapPhieu
            // 
            this.dateNgayLapPhieu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateNgayLapPhieu.Location = new System.Drawing.Point(173, 13);
            this.dateNgayLapPhieu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateNgayLapPhieu.Name = "dateNgayLapPhieu";
            this.dateNgayLapPhieu.Size = new System.Drawing.Size(271, 24);
            this.dateNgayLapPhieu.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(134, 18);
            this.label9.TabIndex = 7;
            this.label9.Text = "Tên Nhà Cung Cấp";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 13);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(150, 18);
            this.label12.TabIndex = 3;
            this.label12.Text = "Ngày Lập Phiếu Nhập";
            // 
            // btnPhieuNhapSua
            // 
            this.btnPhieuNhapSua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPhieuNhapSua.Location = new System.Drawing.Point(768, 26);
            this.btnPhieuNhapSua.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPhieuNhapSua.Name = "btnPhieuNhapSua";
            this.btnPhieuNhapSua.Size = new System.Drawing.Size(81, 26);
            this.btnPhieuNhapSua.TabIndex = 2;
            this.btnPhieuNhapSua.Text = "Sửa";
            this.btnPhieuNhapSua.UseVisualStyleBackColor = true;
            this.btnPhieuNhapSua.Click += new System.EventHandler(this.btnPhieuNhapSua_Click);
            // 
            // btnPhieuNhapXoa
            // 
            this.btnPhieuNhapXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPhieuNhapXoa.Location = new System.Drawing.Point(768, 51);
            this.btnPhieuNhapXoa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPhieuNhapXoa.Name = "btnPhieuNhapXoa";
            this.btnPhieuNhapXoa.Size = new System.Drawing.Size(81, 26);
            this.btnPhieuNhapXoa.TabIndex = 1;
            this.btnPhieuNhapXoa.Text = "Xóa";
            this.btnPhieuNhapXoa.UseVisualStyleBackColor = true;
            this.btnPhieuNhapXoa.Click += new System.EventHandler(this.btnPhieuNhapXoa_Click);
            // 
            // btnPhieuNhapThem
            // 
            this.btnPhieuNhapThem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPhieuNhapThem.Location = new System.Drawing.Point(768, 0);
            this.btnPhieuNhapThem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPhieuNhapThem.Name = "btnPhieuNhapThem";
            this.btnPhieuNhapThem.Size = new System.Drawing.Size(81, 26);
            this.btnPhieuNhapThem.TabIndex = 0;
            this.btnPhieuNhapThem.Text = "Thêm";
            this.btnPhieuNhapThem.UseVisualStyleBackColor = true;
            this.btnPhieuNhapThem.Click += new System.EventHandler(this.btnPhieuNhapThem_Click);
            // 
            // dgPhieuNhap
            // 
            this.dgPhieuNhap.AllowUserToAddRows = false;
            this.dgPhieuNhap.AllowUserToDeleteRows = false;
            this.dgPhieuNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPhieuNhap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaPhieuNhap,
            this.NgayLapPhieuNhap,
            this.TenNhaCungCap});
            this.dgPhieuNhap.Location = new System.Drawing.Point(4, 8);
            this.dgPhieuNhap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgPhieuNhap.Name = "dgPhieuNhap";
            this.dgPhieuNhap.ReadOnly = true;
            this.dgPhieuNhap.RowHeadersWidth = 62;
            this.dgPhieuNhap.Size = new System.Drawing.Size(970, 105);
            this.dgPhieuNhap.TabIndex = 4;
            this.dgPhieuNhap.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPhieuNhap_CellClick);
            // 
            // MaPhieuNhap
            // 
            this.MaPhieuNhap.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MaPhieuNhap.DataPropertyName = "MaPhieuNhap";
            this.MaPhieuNhap.HeaderText = "Mã phiếu nhập";
            this.MaPhieuNhap.MinimumWidth = 8;
            this.MaPhieuNhap.Name = "MaPhieuNhap";
            this.MaPhieuNhap.ReadOnly = true;
            // 
            // NgayLapPhieuNhap
            // 
            this.NgayLapPhieuNhap.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NgayLapPhieuNhap.DataPropertyName = "NgayLapPhieuNhap";
            this.NgayLapPhieuNhap.HeaderText = "Ngày lập phiếu";
            this.NgayLapPhieuNhap.MinimumWidth = 8;
            this.NgayLapPhieuNhap.Name = "NgayLapPhieuNhap";
            this.NgayLapPhieuNhap.ReadOnly = true;
            // 
            // TenNhaCungCap
            // 
            this.TenNhaCungCap.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TenNhaCungCap.DataPropertyName = "TenNhaCungCap";
            this.TenNhaCungCap.HeaderText = "Nhà cung cấp";
            this.TenNhaCungCap.MinimumWidth = 8;
            this.TenNhaCungCap.Name = "TenNhaCungCap";
            this.TenNhaCungCap.ReadOnly = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.danhMụcToolStripMenuItem,
            this.nghiệpVụToolStripMenuItem,
            this.báoCáoToolStripMenuItem,
            this.nghiệpVụToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(996, 28);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // danhMụcToolStripMenuItem
            // 
            this.danhMụcToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSach,
            this.menuLoaiSach,
            this.kháchHàngToolStripMenuItem,
            this.nhàCungCấpToolStripMenuItem});
            this.danhMụcToolStripMenuItem.Name = "danhMụcToolStripMenuItem";
            this.danhMụcToolStripMenuItem.Size = new System.Drawing.Size(90, 24);
            this.danhMụcToolStripMenuItem.Text = "Danh mục";
            this.danhMụcToolStripMenuItem.Click += new System.EventHandler(this.danhMụcToolStripMenuItem_Click);
            // 
            // menuSach
            // 
            this.menuSach.Name = "menuSach";
            this.menuSach.Size = new System.Drawing.Size(183, 26);
            this.menuSach.Text = "Sách";
            this.menuSach.Click += new System.EventHandler(this.menuSach_Click);
            // 
            // menuLoaiSach
            // 
            this.menuLoaiSach.Name = "menuLoaiSach";
            this.menuLoaiSach.Size = new System.Drawing.Size(183, 26);
            this.menuLoaiSach.Text = "Loại sách";
            this.menuLoaiSach.Click += new System.EventHandler(this.menuLoaiSach_Click);
            // 
            // kháchHàngToolStripMenuItem
            // 
            this.kháchHàngToolStripMenuItem.Name = "kháchHàngToolStripMenuItem";
            this.kháchHàngToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.kháchHàngToolStripMenuItem.Text = "Khách hàng";
            this.kháchHàngToolStripMenuItem.Click += new System.EventHandler(this.menuKhachHang_Click);
            // 
            // nhàCungCấpToolStripMenuItem
            // 
            this.nhàCungCấpToolStripMenuItem.Name = "nhàCungCấpToolStripMenuItem";
            this.nhàCungCấpToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.nhàCungCấpToolStripMenuItem.Text = "Nhà cung cấp";
            this.nhàCungCấpToolStripMenuItem.Click += new System.EventHandler(this.menuNhaCungCap_Click);
            // 
            // nghiệpVụToolStripMenuItem
            // 
            this.nghiệpVụToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHoaDon,
            this.menuPhieuNhap});
            this.nghiệpVụToolStripMenuItem.Name = "nghiệpVụToolStripMenuItem";
            this.nghiệpVụToolStripMenuItem.Size = new System.Drawing.Size(91, 24);
            this.nghiệpVụToolStripMenuItem.Text = "Nghiệp vụ";
            // 
            // menuHoaDon
            // 
            this.menuHoaDon.Name = "menuHoaDon";
            this.menuHoaDon.Size = new System.Drawing.Size(216, 26);
            this.menuHoaDon.Text = "Hóa đơn bán hàng";
            this.menuHoaDon.Click += new System.EventHandler(this.menuHoaDon_Click);
            // 
            // menuPhieuNhap
            // 
            this.menuPhieuNhap.Name = "menuPhieuNhap";
            this.menuPhieuNhap.Size = new System.Drawing.Size(216, 26);
            this.menuPhieuNhap.Text = "Phiếu nhập hàng";
            this.menuPhieuNhap.Click += new System.EventHandler(this.menuPhieuNhap_Click);
            // 
            // báoCáoToolStripMenuItem
            // 
            this.báoCáoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thốngKêDoanhThuToolStripMenuItem});
            this.báoCáoToolStripMenuItem.Name = "báoCáoToolStripMenuItem";
            this.báoCáoToolStripMenuItem.Size = new System.Drawing.Size(79, 24);
            this.báoCáoToolStripMenuItem.Text = "Báo Cáo";
            // 
            // thốngKêDoanhThuToolStripMenuItem
            // 
            this.thốngKêDoanhThuToolStripMenuItem.Name = "thốngKêDoanhThuToolStripMenuItem";
            this.thốngKêDoanhThuToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.thốngKêDoanhThuToolStripMenuItem.Text = "Thống kê doanh thu";
            this.thốngKêDoanhThuToolStripMenuItem.Click += new System.EventHandler(this.menuThongKe_Click);
            // 
            // nghiệpVụToolStripMenuItem1
            // 
            this.nghiệpVụToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTaiKhoan,
            this.menuPhanQuyen,
            this.menuDoiMatKhau,
            this.đăngXuấtToolStripMenuItem,
            this.thoátToolStripMenuItem});
            this.nghiệpVụToolStripMenuItem1.Name = "nghiệpVụToolStripMenuItem1";
            this.nghiệpVụToolStripMenuItem1.Size = new System.Drawing.Size(85, 24);
            this.nghiệpVụToolStripMenuItem1.Text = "Hệ thống";
            // 
            // menuTaiKhoan
            // 
            this.menuTaiKhoan.Name = "menuTaiKhoan";
            this.menuTaiKhoan.Size = new System.Drawing.Size(224, 26);
            this.menuTaiKhoan.Text = "Quản lý tài khoản";
            this.menuTaiKhoan.Click += new System.EventHandler(this.menuTaiKhoan_Click);
            // 
            // menuPhanQuyen
            // 
            this.menuPhanQuyen.Name = "menuPhanQuyen";
            this.menuPhanQuyen.Size = new System.Drawing.Size(224, 26);
            this.menuPhanQuyen.Text = "Quản lý phân quyền";
            this.menuPhanQuyen.Click += new System.EventHandler(this.menuPhanQuyen_Click);
            // 
            // menuDoiMatKhau
            // 
            this.menuDoiMatKhau.Name = "menuDoiMatKhau";
            this.menuDoiMatKhau.Size = new System.Drawing.Size(224, 26);
            this.menuDoiMatKhau.Text = "Đổi Mật Khẩu";
            this.menuDoiMatKhau.Click += new System.EventHandler(this.menuDoiMatKhau_Click);
            // 
            // đăngXuấtToolStripMenuItem
            // 
            this.đăngXuấtToolStripMenuItem.Name = "đăngXuấtToolStripMenuItem";
            this.đăngXuấtToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.đăngXuấtToolStripMenuItem.Text = "Đăng xuất";
            this.đăngXuấtToolStripMenuItem.Click += new System.EventHandler(this.menuDangXuat_Click);
            // 
            // thoátToolStripMenuItem
            // 
            this.thoátToolStripMenuItem.Name = "thoátToolStripMenuItem";
            this.thoátToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.thoátToolStripMenuItem.Text = "Thoát";
            this.thoátToolStripMenuItem.Click += new System.EventHandler(this.menuThoat_Click);
            // 
            // FormGiaoDien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 459);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormGiaoDien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Bán Sách";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSachGiaBan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSachSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSach)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLoaiSach)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgChiTietHoaDon)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numChiTietHoaDonSoLuong)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgHoaDon)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numChiTietPhieuNhapSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgChiTietPhieuNhap)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPhieuNhap)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private DataGridView dgSach;
        private Panel panel1;
        private Button btnSachSua;
        private Button btnSachXoa;
        private Button btnSachThem;
        private ComboBox cbSachLoaiSach;
        private Label label3;
        private TextBox txtSachTacGia;
        private Label label2;
        private TextBox txtSachTenSach;
        private Label label1;
        private NumericUpDown numSachSoLuong;
        private Label label5;
        private Label label4;
        private NumericUpDown numSachGiaBan;
        private Panel panel2;
        private TextBox txtLoaiSachTen;
        private Label label10;
        private Button btnLoaiSachSua;
        private Button btnLoaiSachXoa;
        private Button btnLoaiSachThem;
        private DataGridView dgLoaiSach;
        private Panel panel3;
        private Label label8;
        private Label label11;
        private Button btnHoaDonSua;
        private Button btnHoaDonXoa;
        private Button btnHoaDonThem;
        private DataGridView dgHoaDon;
        private Label label6;
        private DateTimePicker dateNgayLapHoaDon;
        private TextBox txtHoaDonSDTKH;
        private TextBox txtHoaDonTenKH;
        private Panel panel4;
        private TextBox txtPhieuNhapNhaCungCap;
        private DateTimePicker dateNgayLapPhieu;
        private Label label9;
        private Label label12;
        private Button btnPhieuNhapSua;
        private Button btnPhieuNhapXoa;
        private Button btnPhieuNhapThem;
        private DataGridView dgPhieuNhap;
        private GroupBox groupBox1;
        private DataGridView dgChiTietHoaDon;
        private Panel panel5;
        private Button btnChiTietHoaDonXoa;
        private Button btnChiTietHoaDonThem;
        private NumericUpDown numChiTietHoaDonSoLuong;
        private Label label13;
        private Label label7;
        private ComboBox cbChiTietHoaDonSach;
        private GroupBox groupBox2;
        private Panel panel6;
        private ComboBox cbChiTietPhieuNhapSach;
        private NumericUpDown numChiTietPhieuNhapSoLuong;
        private Button btnChiTietPhieuNhapThem;
        private Button btnChiTietPhieuNhapXoa;
        private Label label15;
        private Label label14;
        private DataGridView dgChiTietPhieuNhap;
        private Button btnInHoaDon;
        private Button btnInPhieuNhap;
        private DataGridViewTextBoxColumn ma_sach;
        private DataGridViewTextBoxColumn ten_sach;
        private DataGridViewTextBoxColumn loai_sach;
        private DataGridViewTextBoxColumn tac_gia;
        private DataGridViewTextBoxColumn so_luong;
        private DataGridViewTextBoxColumn gia_ban;
        private DataGridViewTextBoxColumn ma_loai_sach;
        private DataGridViewTextBoxColumn ten_loai_sach;
        private DataGridViewTextBoxColumn MaHoaDon;
        private DataGridViewTextBoxColumn MaSach;
        private DataGridViewTextBoxColumn TenSach;
        private DataGridViewTextBoxColumn SoLuong;
        private DataGridViewTextBoxColumn GiaBan;
        private DataGridViewTextBoxColumn ThanhTien;
        private DataGridViewTextBoxColumn ma_hoa_don;
        private DataGridViewTextBoxColumn ngay_lap_hoa_don;
        private DataGridViewTextBoxColumn ten_khach_hang;
        private DataGridViewTextBoxColumn sdt_khach_hang;
        private DataGridViewTextBoxColumn colMaPhieuNhap;
        private DataGridViewTextBoxColumn colMaSach;
        private DataGridViewTextBoxColumn colTenSach;
        private DataGridViewTextBoxColumn colSoLuong;
        private DataGridViewTextBoxColumn MaPhieuNhap;
        private DataGridViewTextBoxColumn NgayLapPhieuNhap;
        private DataGridViewTextBoxColumn TenNhaCungCap;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem danhMụcToolStripMenuItem;
        private ToolStripMenuItem menuSach;
        private ToolStripMenuItem nghiệpVụToolStripMenuItem;
        private ToolStripMenuItem báoCáoToolStripMenuItem;
        private ToolStripMenuItem thốngKêDoanhThuToolStripMenuItem;
        private ToolStripMenuItem nghiệpVụToolStripMenuItem1;
        private ToolStripMenuItem menuLoaiSach;
        private ToolStripMenuItem kháchHàngToolStripMenuItem;
        private ToolStripMenuItem nhàCungCấpToolStripMenuItem;
        private ToolStripMenuItem menuHoaDon;
        private ToolStripMenuItem menuPhieuNhap;
        private ToolStripMenuItem menuTaiKhoan;
        private ToolStripMenuItem menuPhanQuyen;
        private ToolStripMenuItem menuDoiMatKhau;
        private ToolStripMenuItem đăngXuấtToolStripMenuItem;
        private ToolStripMenuItem thoátToolStripMenuItem;
    }
}
