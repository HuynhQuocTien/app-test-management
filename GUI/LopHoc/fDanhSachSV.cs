using BLL;
using DocumentFormat.OpenXml.Office2010.CustomUI;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.LopHoc
{
    public partial class fDanhSachSV : Form
    {
        private LopDTO lopDTO;
        private DataTable dt;

        private NguoiDungBLL nguoiDungBLL;
        private ChiTietLopBLL chiTietLopBLL;
        private LopBLL lopBLL;

        private List<NguoiDungDTO> lHocSinhTrongLop;
        public fDanhSachSV(LopDTO lopDTO)
        {
            nguoiDungBLL = new NguoiDungBLL();
            chiTietLopBLL = new ChiTietLopBLL();
            lopBLL = new LopBLL();
            InitializeComponent();
            this.lopDTO = lopDTO;
            lHocSinhTrongLop = chiTietLopBLL.GetSV(lopDTO.MaLop) ?? new List<NguoiDungDTO>();

            dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã học sinh", typeof(long));
            dt.Columns.Add("Họ và tên", typeof(string));
            dt.Columns.Add("SDT", typeof(string));
            loadDataGridView();
        }
        private void loadDataGridView()
        {
            dt.Clear();
            int stt = 1;
            foreach (NguoiDungDTO hs in lHocSinhTrongLop)
            {
                DataRow row = dt.NewRow();
                row["STT"] = stt;
                row["Mã học sinh"] = hs.MaNguoiDung;
                row["Họ và tên"] = hs.HoTen;
                row["SDT"] = hs.SDT;
                dt.Rows.Add(row);
                stt++;
            }
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.DataSource = dt;
        }
        private void cbDeThi_SelectedValueChanged(object sender, EventArgs e)
        {

        }
        private void cbTrangThai_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }
        private void btnXuatDSHS_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
