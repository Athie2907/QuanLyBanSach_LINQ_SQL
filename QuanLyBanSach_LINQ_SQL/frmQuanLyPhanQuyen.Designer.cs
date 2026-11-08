namespace QuanLyBanSach_LINQ_SQL
{
    partial class frmQuanLyPhanQuyen
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
            this.lblNhomQuyen = new System.Windows.Forms.Label();
            this.cbPhanQuyen = new System.Windows.Forms.ComboBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.dgChucNang = new System.Windows.Forms.DataGridView();
            this.colMaChucNang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenChucNang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDuocPhep = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgChucNang)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNhomQuyen
            // 
            this.lblNhomQuyen.AutoSize = true;
            this.lblNhomQuyen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNhomQuyen.Location = new System.Drawing.Point(74, 33);
            this.lblNhomQuyen.Name = "lblNhomQuyen";
            this.lblNhomQuyen.Size = new System.Drawing.Size(121, 22);
            this.lblNhomQuyen.TabIndex = 0;
            this.lblNhomQuyen.Text = "Nhóm quyền";
            // 
            // cbPhanQuyen
            // 
            this.cbPhanQuyen.FormattingEnabled = true;
            this.cbPhanQuyen.Location = new System.Drawing.Point(201, 32);
            this.cbPhanQuyen.Name = "cbPhanQuyen";
            this.cbPhanQuyen.Size = new System.Drawing.Size(250, 28);
            this.cbPhanQuyen.TabIndex = 1;
            this.cbPhanQuyen.SelectedIndexChanged += new System.EventHandler(this.cbPhanQuyen_SelectedIndexChanged);
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnLuu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnLuu.Location = new System.Drawing.Point(102, 76);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(178, 42);
            this.btnLuu.TabIndex = 2;
            this.btnLuu.Text = "Lưu phân quyền";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnLamMoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnLamMoi.Location = new System.Drawing.Point(322, 76);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(129, 42);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // dgChucNang
            // 
            this.dgChucNang.AllowUserToAddRows = false;
            this.dgChucNang.AllowUserToDeleteRows = false;
            this.dgChucNang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgChucNang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaChucNang,
            this.colTenChucNang,
            this.colDuocPhep});
            this.dgChucNang.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgChucNang.Location = new System.Drawing.Point(2, 152);
            this.dgChucNang.MultiSelect = false;
            this.dgChucNang.Name = "dgChucNang";
            this.dgChucNang.ReadOnly = true;
            this.dgChucNang.RowHeadersWidth = 62;
            this.dgChucNang.RowTemplate.Height = 28;
            this.dgChucNang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgChucNang.Size = new System.Drawing.Size(553, 193);
            this.dgChucNang.TabIndex = 4;
            this.dgChucNang.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgChucNang_CurrentCellDirtyStateChanged);
            // 
            // colMaChucNang
            // 
            this.colMaChucNang.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMaChucNang.DataPropertyName = "MaChucNang";
            this.colMaChucNang.HeaderText = "Mã chức năng";
            this.colMaChucNang.MinimumWidth = 8;
            this.colMaChucNang.Name = "colMaChucNang";
            this.colMaChucNang.ReadOnly = true;
            // 
            // colTenChucNang
            // 
            this.colTenChucNang.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTenChucNang.DataPropertyName = "TenChucNang";
            this.colTenChucNang.HeaderText = "Tên chức năng";
            this.colTenChucNang.MinimumWidth = 8;
            this.colTenChucNang.Name = "colTenChucNang";
            this.colTenChucNang.ReadOnly = true;
            // 
            // colDuocPhep
            // 
            this.colDuocPhep.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDuocPhep.DataPropertyName = "DuocPhep";
            this.colDuocPhep.HeaderText = "Được phép";
            this.colDuocPhep.MinimumWidth = 8;
            this.colDuocPhep.Name = "colDuocPhep";
            this.colDuocPhep.ReadOnly = true;
            this.colDuocPhep.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colDuocPhep.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // frmQuanLyPhanQuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 351);
            this.Controls.Add(this.dgChucNang);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.cbPhanQuyen);
            this.Controls.Add(this.lblNhomQuyen);
            this.Name = "frmQuanLyPhanQuyen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmQuanLyPhanQuyen";
            this.Load += new System.EventHandler(this.frmQuanLyPhanQuyen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgChucNang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNhomQuyen;
        private System.Windows.Forms.ComboBox cbPhanQuyen;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.DataGridView dgChucNang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaChucNang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenChucNang;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colDuocPhep;
    }
}