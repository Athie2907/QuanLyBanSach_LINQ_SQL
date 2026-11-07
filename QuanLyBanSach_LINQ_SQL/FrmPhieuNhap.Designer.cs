namespace QuanLyBanSach_LINQ_SQL
{
    partial class FrmPhieuNhap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnInPhieuNhap = new System.Windows.Forms.Button();
            this.btnXemChiTiet = new System.Windows.Forms.Button();
            this.btnTaoMoi = new System.Windows.Forms.Button();
            this.btnLocTheoNCC = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dvgPhieuNhap = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbNhaCungCap = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgPhieuNhap)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInPhieuNhap
            // 
            this.btnInPhieuNhap.Location = new System.Drawing.Point(555, 95);
            this.btnInPhieuNhap.Name = "btnInPhieuNhap";
            this.btnInPhieuNhap.Size = new System.Drawing.Size(182, 74);
            this.btnInPhieuNhap.TabIndex = 17;
            this.btnInPhieuNhap.Text = "In phiếu nhập";
            this.btnInPhieuNhap.UseVisualStyleBackColor = true;
            this.btnInPhieuNhap.Click += new System.EventHandler(this.btnInPhieuNhap_Click);
            // 
            // btnXemChiTiet
            // 
            this.btnXemChiTiet.Location = new System.Drawing.Point(311, 95);
            this.btnXemChiTiet.Name = "btnXemChiTiet";
            this.btnXemChiTiet.Size = new System.Drawing.Size(187, 74);
            this.btnXemChiTiet.TabIndex = 16;
            this.btnXemChiTiet.Text = "Xem chi tiết";
            this.btnXemChiTiet.UseVisualStyleBackColor = true;
            this.btnXemChiTiet.Click += new System.EventHandler(this.btnXemChiTiet_Click);
            // 
            // btnTaoMoi
            // 
            this.btnTaoMoi.Location = new System.Drawing.Point(80, 95);
            this.btnTaoMoi.Name = "btnTaoMoi";
            this.btnTaoMoi.Size = new System.Drawing.Size(174, 74);
            this.btnTaoMoi.TabIndex = 15;
            this.btnTaoMoi.Text = "Tạo phiếu nhập mới";
            this.btnTaoMoi.UseVisualStyleBackColor = true;
            this.btnTaoMoi.Click += new System.EventHandler(this.btnTaoMoi_Click);
            // 
            // btnLocTheoNCC
            // 
            this.btnLocTheoNCC.Location = new System.Drawing.Point(704, 13);
            this.btnLocTheoNCC.Name = "btnLocTheoNCC";
            this.btnLocTheoNCC.Size = new System.Drawing.Size(58, 46);
            this.btnLocTheoNCC.TabIndex = 14;
            this.btnLocTheoNCC.Text = "Lọc";
            this.btnLocTheoNCC.UseVisualStyleBackColor = true;
            this.btnLocTheoNCC.Click += new System.EventHandler(this.btnLocTheoNCC_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(370, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "Đến ngày: ";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Location = new System.Drawing.Point(446, 23);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(227, 22);
            this.dtpDenNgay.TabIndex = 12;
            this.dtpDenNgay.ValueChanged += new System.EventHandler(this.dtpDenNgay_ValueChanged);
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Location = new System.Drawing.Point(95, 23);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(233, 22);
            this.dtpTuNgay.TabIndex = 11;
            this.dtpTuNgay.ValueChanged += new System.EventHandler(this.dtpTuNgay_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Từ ngày: ";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dvgPhieuNhap);
            this.panel1.Location = new System.Drawing.Point(5, 187);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(790, 251);
            this.panel1.TabIndex = 9;
            // 
            // dvgPhieuNhap
            // 
            this.dvgPhieuNhap.AllowUserToAddRows = false;
            this.dvgPhieuNhap.AllowUserToDeleteRows = false;
            this.dvgPhieuNhap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dvgPhieuNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgPhieuNhap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dvgPhieuNhap.Location = new System.Drawing.Point(7, 13);
            this.dvgPhieuNhap.Name = "dvgPhieuNhap";
            this.dvgPhieuNhap.ReadOnly = true;
            this.dvgPhieuNhap.RowHeadersWidth = 51;
            this.dvgPhieuNhap.RowTemplate.Height = 24;
            this.dvgPhieuNhap.Size = new System.Drawing.Size(776, 225);
            this.dvgPhieuNhap.TabIndex = 0;
            this.dvgPhieuNhap.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvgPhieuNhap_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Mã HĐ";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 170F;
            this.Column2.HeaderText = "Ngày lập";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 120F;
            this.Column3.HeaderText = "Tổng tiền";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.FillWeight = 80F;
            this.Column4.HeaderText = "Ghi chú";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // cbNhaCungCap
            // 
            this.cbNhaCungCap.FormattingEnabled = true;
            this.cbNhaCungCap.Location = new System.Drawing.Point(129, 65);
            this.cbNhaCungCap.Name = "cbNhaCungCap";
            this.cbNhaCungCap.Size = new System.Drawing.Size(199, 24);
            this.cbNhaCungCap.TabIndex = 18;
            this.cbNhaCungCap.SelectedIndexChanged += new System.EventHandler(this.cbNhaCungCap_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 68);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(96, 16);
            this.label3.TabIndex = 19;
            this.label3.Text = "Nhà cung cấp: ";
            // 
            // FrmPhieuNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbNhaCungCap);
            this.Controls.Add(this.btnInPhieuNhap);
            this.Controls.Add(this.btnXemChiTiet);
            this.Controls.Add(this.btnTaoMoi);
            this.Controls.Add(this.btnLocTheoNCC);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpDenNgay);
            this.Controls.Add(this.dtpTuNgay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "FrmPhieuNhap";
            this.Text = "QUẢN LÝ PHIẾU NHẬP SÁCH";
            this.Load += new System.EventHandler(this.FrmPhieuNhap_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvgPhieuNhap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInPhieuNhap;
        private System.Windows.Forms.Button btnXemChiTiet;
        private System.Windows.Forms.Button btnTaoMoi;
        private System.Windows.Forms.Button btnLocTheoNCC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dvgPhieuNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.ComboBox cbNhaCungCap;
        private System.Windows.Forms.Label label3;
    }
}