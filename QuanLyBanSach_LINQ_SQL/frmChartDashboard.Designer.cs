namespace QuanLyBanSach_LINQ_SQL
{
    partial class frmChartDashboard
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            chartArea2.Name = "ChartArea1";
            this.chrtDoanhThu.ChartAreas.Add(chartArea2);
            this.chrtDoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chrtDoanhThu.Legends.Add(legend2);
            this.chrtDoanhThu.Location = new System.Drawing.Point(0, 0);
            this.chrtDoanhThu.Name = "chrtDoanhThu";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chrtDoanhThu.Series.Add(series2);
            this.chrtDoanhThu.Size = new System.Drawing.Size(776, 397);
            this.chrtDoanhThu.TabIndex = 0;
            this.chrtDoanhThu.Text = "chart1";
            this.chrtDoanhThu.Click += new System.EventHandler(this.chrtDoanhThu_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Xem theo:";
            // 
            // radTuanNay
            // 
            this.radTuanNay.AutoSize = true;
            this.radTuanNay.Location = new System.Drawing.Point(147, 15);
            this.radTuanNay.Name = "radTuanNay";
            this.radTuanNay.Size = new System.Drawing.Size(94, 20);
            this.radTuanNay.TabIndex = 2;
            this.radTuanNay.TabStop = true;
            this.radTuanNay.Text = "7 ngày qua";
            this.radTuanNay.UseVisualStyleBackColor = true;
            this.radTuanNay.CheckedChanged += new System.EventHandler(this.radTuanNay_CheckedChanged);
            // 
            // radThangNay
            // 
            this.radThangNay.AutoSize = true;
            this.radThangNay.Location = new System.Drawing.Point(306, 15);
            this.radThangNay.Name = "radThangNay";
            this.radThangNay.Size = new System.Drawing.Size(92, 20);
            this.radThangNay.TabIndex = 3;
            this.radThangNay.TabStop = true;
            this.radThangNay.Text = "Tháng này";
            this.radThangNay.UseVisualStyleBackColor = true;
            this.radThangNay.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // btnThongKe
            // 
            this.btnThongKe.Location = new System.Drawing.Point(550, 7);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(102, 32);
            this.btnThongKe.TabIndex = 4;
            this.btnThongKe.Text = "Thông kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chrtDoanhThu);
            this.panel1.Location = new System.Drawing.Point(12, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 397);
            this.panel1.TabIndex = 5;
            // 
            // frmChartDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.radThangNay);
            this.Controls.Add(this.radTuanNay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
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