using DAL;
using GUI.MonHoc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using BLL;
using DocumentFormat.OpenXml.Office.Word;
using GUI.CauHoi;


namespace GUI.PhanCong
{
    public partial class PhanCongControl : UserControl
    {
        private int Allrecord;
        public PhanCongControl()
        {
            InitializeComponent();
            LoadDataToGridView();
            phanTrang();
        }
        public void phanTrang()
        {
            // Đặt giới hạn số trang cho NumericUpDown
            int totalRecords = Allrecord;  // Tổng số bản ghi

            int recordsPerPage = 10; // Số bản ghi trên mỗi trang
            int totalPages = (int)Math.Ceiling((double)totalRecords / recordsPerPage);
            this.numericUpDown1.Minimum = 1;
            this.numericUpDown1.Maximum = totalPages;
            this.label2.Text = "Trên tổng " + totalPages + " trang";
            // Sự kiện khi thay đổi trang
            this.numericUpDown1.ValueChanged += (sender, e) =>
            {
                int selectedPage = (int)numericUpDown1.Value;
                LoadPage(selectedPage, recordsPerPage);
            };
        }

        private void LoadPage(int pageNumber, int recordsPerPage)
        {
            int startRecord = (pageNumber - 1) * recordsPerPage;

            // Tải dữ liệu từ cơ sở dữ liệu hoặc danh sách, lấy các bản ghi từ startRecord đến startRecord + recordsPerPage
            // Ví dụ:
            PhanCongBLL phanCongBLL = new PhanCongBLL();
            DataTable pageData = phanCongBLL.GetDataForPage(startRecord, recordsPerPage);
            dataGridView1.DataSource = pageData;
        }

        

        private void LoadDataToGridView()
        {
            // Kết nối đến cơ sở dữ liệu SQL Server
            PhanCongBLL phanCongBLL = new PhanCongBLL();
            dataGridView1.DataSource = phanCongBLL.GetAll();
            Allrecord=dataGridView1.RowCount ;
            LoadPage(1, 10);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            fAddPhanCong fthemPhanCong = new fAddPhanCong();
            fthemPhanCong.Show();
            fthemPhanCong.FormClosed += (s, args) => {
                LoadDataToGridView();
                phanTrang();
                this.numericUpDown1.Value = 1;
            };
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
               PhanCongBLL phanCongBLL = new PhanCongBLL();
               dataGridView1.DataSource = phanCongBLL.GetTimKiem(textBoxTimKiem.Text);
                this.numericUpDown1.Enabled = false;
                this.numericUpDown1.Value = 1;
        }
    }
}
