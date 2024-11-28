using BLL;
using DTO;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace GUI
{
    public partial class fThongKe : UserControl
    {
        private StatisticBLL _statisticBLL = new StatisticBLL();
        private List<ThongKeDiemDTO> listThongKeDiem = new List<ThongKeDiemDTO> ();
        private List<ThongKeDiemTheoMonDTO> listThongKeDiemTheoMon = new List<ThongKeDiemTheoMonDTO>();
        private List<ThongKeDiemTheoLopDTO> listThongKeDiemTheoLop = new List<ThongKeDiemTheoLopDTO> ();
        private List<ThongKeDiemDTO> _thongKeDiemResult = new List<ThongKeDiemDTO> ();
        private LopBLL _lopBll = new LopBLL();
        private MonHocBLL _monHocBLL = new MonHocBLL();
        public fThongKe()
        {
            InitializeComponent(); 
            load();
        }

        private void pieChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        public void load()
        {
            List<MonHocDTO> mhList = new List<MonHocDTO>();
            List<LopDTO> lopList = _lopBll.getAll();
            List<MonHocDTO> monList = _monHocBLL.GetAll();
            listThongKeDiemTheoMon = _statisticBLL.ThongKeDiemTheoMon();
            listThongKeDiemTheoLop = _statisticBLL.ThongKeDiemTheoLop();
            MessageBox.Show(listThongKeDiemTheoMon.Count().ToString());
            cbDiemTheoLop.DataSource = lopList;
            cbDiemTheoMon.DataSource = monList;
        }

        private void UpdateChart()
        {
            cartesianChart1.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Điểm số",
                    Values = new ChartValues<double>() // Khởi tạo giá trị trống
                }
            };

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Sinh viên",
                Labels = new string[0] // Khởi tạo nhãn trục trống
            });
            cartesianChart1.Series[0].Values = new ChartValues<double>(listThongKeDiem.Select(d => (double)d.diem));
            cartesianChart1.AxisX[0].Labels = listThongKeDiem.Select(d => d.Ten).ToArray();
            dtgvThongKeDiem.DataSource = listThongKeDiem;

            dtgvThongKeDiem.Columns["MonHoc"].Visible = false; 
            dtgvThongKeDiem.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Tên Môn Học",
                DataPropertyName = "MonHoc.TenMonHoc"
            });

            // Hiển thị các cột khác nếu cần
            dtgvThongKeDiem.Columns["Lop"].Visible = false; 
            dtgvThongKeDiem.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Tên Lớp",
                DataPropertyName = "Lop.TenLop"
            });
        }

        private void LoadDtgvTheoMon(List<ThongKeDiemTheoMonDTO> listThongKeDiemTheoMon)
        {
            dtgvThongKeDiem.DataSource = null; // Đặt DataSource về null trước khi thêm cột
            dtgvThongKeDiem.Columns.Clear();  // Xóa các cột hiện tại trong DataGridView

            // Tạo các cột và gắn DataPropertyName tương ứng
            DataGridViewTextBoxColumn maMonColumn = new DataGridViewTextBoxColumn();
            maMonColumn.HeaderText = "Mã môn";
            maMonColumn.DataPropertyName = "MaMonHoc"; // Tên thuộc tính trong DTO hoặc đối tượng dữ liệu
            dtgvThongKeDiem.Columns.Add(maMonColumn);

            DataGridViewTextBoxColumn tenMonColumn = new DataGridViewTextBoxColumn();
            tenMonColumn.HeaderText = "Tên môn";
            tenMonColumn.DataPropertyName = "TenMonHoc"; // Tên thuộc tính trong DTO hoặc đối tượng dữ liệu
            dtgvThongKeDiem.Columns.Add(tenMonColumn);

            DataGridViewTextBoxColumn diemTBColumn = new DataGridViewTextBoxColumn();
            diemTBColumn.HeaderText = "Điểm trung bình";
            diemTBColumn.DataPropertyName = "DiemTrungBinh"; // Tên thuộc tính trong DTO hoặc đối tượng dữ liệu
            dtgvThongKeDiem.Columns.Add(diemTBColumn);

            DataGridViewTextBoxColumn soBaiThiColumn = new DataGridViewTextBoxColumn();
            soBaiThiColumn.HeaderText = "Số bài thi";
            soBaiThiColumn.DataPropertyName = "SoBaiThi"; // Tên thuộc tính trong DTO hoặc đối tượng dữ liệu
            dtgvThongKeDiem.Columns.Add(soBaiThiColumn);

            // Gán lại DataSource
            dtgvThongKeDiem.DataSource = listThongKeDiemTheoMon;

        }

        private void UpdateChartTheoMon(List<ThongKeDiemTheoMonDTO> listThongKeDiemTheoMon)
        {
            LoadDtgvTheoMon(listThongKeDiemTheoMon);
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            // Khởi tạo Series với kiểu dữ liệu là LineSeries
            var lineSeries = new LiveCharts.Wpf.LineSeries
            {
                Title = "Điểm số", // Tiêu đề dòng
                Values = new ChartValues<double>(listThongKeDiemTheoMon.Select(d => (double)d.diemTrungBinh)), // Giá trị điểm số
                PointGeometry = DefaultGeometries.Circle, // Hình dạng điểm trên Line
                PointGeometrySize = 10, // Kích thước điểm
            };

            // Cập nhật Series vào CartesianChart
            cartesianChart1.Series = new SeriesCollection { lineSeries };

            // Cập nhật trục X (Tên môn học hoặc lớp học)
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Môn học",
                Labels = listThongKeDiemTheoMon.Select(d => d.tenMonHoc).ToArray(), // Hiển thị tên môn học
                Separator = new Separator { Step = 1 } // Đảm bảo các nhãn không bị trùng
            });

            // Cập nhật trục Y (Điểm số)
            cartesianChart1.AxisY.Clear();
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Điểm số",
                LabelFormatter = value => value.ToString("F1") // Hiển thị 1 chữ số thập phân
            });
        }

        private void UpdateChartTheoLop(List<ThongKeDiemTheoLopDTO> listThongKeDiemTheoLop)
        {
            dtgvThongKeDiem.DataSource = listThongKeDiemTheoLop;
            // Khởi tạo Series với kiểu dữ liệu là LineSeries
            var lineSeries = new LiveCharts.Wpf.LineSeries
            {
                Title = "Điểm số", // Tiêu đề dòng
                Values = new ChartValues<decimal>(listThongKeDiemTheoLop.Select(d => d.diemTrungBinh)), // Giá trị điểm số
                PointGeometry = DefaultGeometries.Circle, // Hình dạng điểm trên Line
                PointGeometrySize = 10, // Kích thước điểm
            };

            // Cập nhật Series vào CartesianChart
            cartesianChart1.Series = new SeriesCollection { lineSeries };

            // Cập nhật trục X (Tên môn học hoặc lớp học)
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Môn học/Lớp học",
                Labels = listThongKeDiemTheoLop.Select(d => d.tenLop).ToArray(), // Hiển thị tên môn học
                Separator = new Separator { Step = 1 } // Đảm bảo các nhãn không bị trùng
            });

            // Cập nhật trục Y (Điểm số)
            cartesianChart1.AxisY.Clear();
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Điểm số",
                LabelFormatter = value => value.ToString("F1") // Hiển thị 1 chữ số thập phân
            });
        }

        private void cbDiemTheoLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tenLop = cbDiemTheoLop.Text;
            List<ThongKeDiemTheoLopDTO> listRs = listThongKeDiemTheoLop
                .Where(x => x.tenLop == tenLop) // Điều kiện lọc theo tên lớp
                .ToList();

            UpdateChartTheoLop(listRs);
        }

        private void btnTheoTatCaMon_Click(object sender, EventArgs e)
        {
            MessageBox.Show(listThongKeDiemTheoMon.Count().ToString());
            UpdateChartTheoMon(listThongKeDiemTheoMon);
        }

        private void btnTheoTatCaLop_Click(object sender, EventArgs e)
        {
            UpdateChartTheoLop(listThongKeDiemTheoLop);
        }
    }
}
