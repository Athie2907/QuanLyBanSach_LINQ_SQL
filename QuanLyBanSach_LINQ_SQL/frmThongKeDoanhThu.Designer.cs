namespace QuanLyBanSach_LINQ_SQL
{
    partial class frmThongKeDoanhThu
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chrtDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.radTuanNay = new System.Windows.Forms.RadioButton();
            this.radThangNay = new System.Windows.Forms.RadioButton();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.chrtDoanhThu)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chrtDoanhThu
            // 
            chartArea1.Name = "ChartArea1";
            this.chrtDoanhThu.ChartAreas.Add(chartArea1);
            this.chrtDoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chrtDoanhThu.Legends.Add(legend1);
            this.chrtDoanhThu.Location = new System.Drawing.Point(0, 0);
            this.chrtDoanhThu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chrtDoanhThu.Name = "chrtDoanhThu";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chrtDoanhThu.Series.Add(series1);
//HEAD
            this.chrtDoanhThu.Size = new System.Drawing.Size(873, 496);
            this.chrtDoanhThu.Size = new System.Drawing.Size(776, 397);
            this.chrtDoanhThu.TabIndex = 0;
            this.chrtDoanhThu.Text = "chart1";
            this.chrtDoanhThu.Click += new System.EventHandler(this.chrtDoanhThu_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(42, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Xem theo:";
            // 
            // radTuanNay
            // 
            this.radTuanNay.AutoSize = true;
            this.radTuanNay.Location = new System.Drawing.Point(165, 19);
            this.radTuanNay.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radTuanNay.Name = "radTuanNay";
            this.radTuanNay.Size = new System.Drawing.Size(112, 24);

            this.radTuanNay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radTuanNay.Location = new System.Drawing.Point(151, 10);
            this.radTuanNay.Name = "radTuanNay";
            this.radTuanNay.Size = new System.Drawing.Size(103, 22);
            this.radTuanNay.TabIndex = 2;
            this.radTuanNay.TabStop = true;
            this.radTuanNay.Text = "7 ngày qua";
            this.radTuanNay.UseVisualStyleBackColor = true;
            this.radTuanNay.CheckedChanged += new System.EventHandler(this.radTuanNay_CheckedChanged);
            // 
            // radThangNay
            // 
            this.radThangNay.AutoSize = true;
            this.radThangNay.Location = new System.Drawing.Point(344, 19);
            this.radThangNay.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radThangNay.Name = "radThangNay";
            this.radThangNay.Size = new System.Drawing.Size(108, 24);
            this.radThangNay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radThangNay.Location = new System.Drawing.Point(297, 9);
            this.radThangNay.Name = "radThangNay";
            this.radThangNay.Size = new System.Drawing.Size(100, 22);
            this.radThangNay.TabIndex = 3;
            this.radThangNay.TabStop = true;
            this.radThangNay.Text = "Tháng này";
            this.radThangNay.UseVisualStyleBackColor = true;
            this.radThangNay.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // btnThongKe
            // 
            this.btnThongKe.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnThongKe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKe.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnThongKe.Location = new System.Drawing.Point(619, 9);
            this.btnThongKe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnThongKe.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnThongKe.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnThongKe.Location = new System.Drawing.Point(552, 4);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(115, 40);
            this.btnThongKe.TabIndex = 4;

            this.btnThongKe.Text = "Thống kê";
            this.btnThongKe.Text = "Thông kê";
            this.btnThongKe.UseVisualStyleBackColor = false;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chrtDoanhThu);
            this.panel1.Location = new System.Drawing.Point(14, 51);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(873, 496);
            this.panel1.TabIndex = 5;
            // 
            // frmChartDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 562);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.radThangNay);
            this.Controls.Add(this.radTuanNay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmChartDashboard";
            this.Text = "THỐNG KÊ DOANH THU BÁN SÁCH";
            ((System.ComponentModel.ISupportInitialize)(this.chrtDoanhThu)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chrtDoanhThu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radTuanNay;
        private System.Windows.Forms.RadioButton radThangNay;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Panel panel1;
    }
}