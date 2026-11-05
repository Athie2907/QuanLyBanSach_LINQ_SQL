using System;
using System.Windows.Forms;

namespace QuanLyBanSach_LINQ_SQL
{
    public partial class FrmInReport : Form
    {
        // Hàm khởi tạo (Constructor) có tham số để truyền dữ liệu và tiêu đề
        public FrmInReport(object dataSource, string tieuDeForm)
        {
            InitializeComponent();

            // Thiết lập tiêu đề cho form (ví dụ: "HÓA ĐƠN BÁN SÁCH")
            this.Text = tieuDeForm;

            // Gán nguồn dữ liệu (DataSource) cho DataGridView
            // dataSource có thể là một List, một BindingSource, hoặc kết quả truy vấn LINQ to SQL
            dataGridView1.DataSource = dataSource;

            // Tùy chỉnh giao diện DataGridView cho đẹp mắt
            // Tự động điều chỉnh độ rộng các cột
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Chế độ chỉ đọc, không cho phép người dùng sửa trực tiếp trên form in
            dataGridView1.ReadOnly = true;
            // Cho phép chọn nguyên dòng
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // Chỉ cho phép chọn một dòng
            dataGridView1.MultiSelect = false;
        }

        private void FrmInReport_Load(object sender, EventArgs e)
        {
            // Bạn có thể thêm các xử lý khác khi form được tải tại đây nếu cần
        }

        private void FrmInReport_Load_1(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }
    }
}