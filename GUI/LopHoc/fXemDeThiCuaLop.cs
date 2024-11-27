using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DocumentFormat.OpenXml.Drawing.Charts;
using DTO;
using DataTable = System.Data.DataTable;

namespace GUI.LopHoc
{
    public partial class fXemDeThiCuaLop : Form
    {
        ChiTietDeDTO ctdt;
        ChiTietDeBLL chiTietDeBLL;
        List<CauHoiDTO> listCH;
        MonHocBLL monHocBLL;
        DeThiDTO deThi;
        MonHocDTO mh;
        DataTable dt;
        public fXemDeThiCuaLop(fDanhSachDeThi ds, DeThiDTO obj)
        {
            this.deThi = obj;
            chiTietDeBLL = new ChiTietDeBLL();
            dt = new DataTable();
            monHocBLL = new MonHocBLL();
            mh = new MonHocDTO();
            InitializeComponent();
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");
            listCH = chiTietDeBLL.GetAllCauHoiOfDeThi(deThi);
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nội dung câu hỏi", typeof(string));
            dt.Columns.Add("Môn học", typeof(string));
            dt.Columns.Add("Độ khó", typeof(string));

            loadDataTable();
            loadCHDT();
        }
        public void loadCHDT()
        {
            lblTenDeThi1.Text = deThi.TenDe;
            lblTenMonHoc.Text = monHocBLL.GetMonHocById(deThi.MaMonHoc).TenMonHoc;
            lblThoiGianLamBai.Text = deThi.ThoiGianLamBai + " phút";
        }
        public void loadDataTable()
        {
            dt.Clear();

            foreach (var cauHoi in listCH)
            {
                DataRow row = dt.NewRow();
                row["ID"] = cauHoi.MaCauHoi;
                row["Nội dung câu hỏi"] = cauHoi.NoiDung;
                row["Môn học"] = monHocBLL.GetMonHocById(cauHoi.MaMonHoc).TenMonHoc;
                row["Độ khó"] = cauHoi.DoKho;
                dt.Rows.Add(row);
            }
            dataGridView1.DataSource = dt;
            // Thêm sự kiện DataBindingComplete vào DataGridView
            dataGridView1.DataBindingComplete += dataGridView1_DataBindingComplete;
        }

        public void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dataGridView1.Columns.Contains("Nộidungcâuhỏi"))
            {
                dataGridView1.Columns["Nộidungcâuhỏi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dataGridView1.Columns.Contains("Nộidungcâuhỏi")
                || dataGridView1.Columns.Contains("MônHọc")
                || dataGridView1.Columns.Contains("Độkhó"))
            {
                dataGridView1.Columns["Nộidungcâuhỏi"].HeaderText = "Nội dung câu hỏi";
                dataGridView1.Columns["MônHọc"].HeaderText = "Môn học";
                dataGridView1.Columns["Độkhó"].HeaderText = "Độ khó";
            }

            if (dataGridView1.Columns.Contains("Nội dung câu hỏi"))
            {
                dataGridView1.Columns["Nội dung câu hỏi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

    }
}
