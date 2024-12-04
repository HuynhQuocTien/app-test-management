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

        //tab 2
        private List<ThongKeDiemTheoMonDTO> listThongKeDiemTheoMon = new List<ThongKeDiemTheoMonDTO>();
        private List<ThongKeDiemTheoLopDTO> listThongKeDiemTheoLop = new List<ThongKeDiemTheoLopDTO> ();

        // tab 1
        private List<ThongKeSVThamGiaThiTheoLopDTO> listSVTheoLop = new List<ThongKeSVThamGiaThiTheoLopDTO>();
        private List<ThongKeSVThamGiaThiTheoMonDTO> listSVTheoMon = new List<ThongKeSVThamGiaThiTheoMonDTO>();

        // tab 3
        private List<ThongKeDTBTheoLop> listDTBTheoLop = new List<ThongKeDTBTheoLop> ();
        private List<ThongKeDTBTheoMon> listDTBTheoMon = new List<ThongKeDTBTheoMon> ();

        // DATA COMBO BOX
        private LopBLL _lopBll = new LopBLL();
        private MonHocBLL _monHocBLL = new MonHocBLL();
        private List<LopDTO> lopList = new List<LopDTO> ();
        private List<MonHocDTO> monList = new List<MonHocDTO>();
        public fThongKe()
        {
            GeneralLoad();
            InitializeComponent();

            LoadDiemSoPanel();
            LoadSinhVienThamGiaThiPanel();
            LoadDTBPanel();
        }

        private void pieChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        public void GeneralLoad()
        {
            lopList = _lopBll.getAll();
            monList = _monHocBLL.GetAll();
        }

        public void LoadSinhVienThamGiaThiPanel()
        {
            int SoLuongSinhVien = _statisticBLL.SoLuongSinhVien();
            int SoLuongGiaoVien = _statisticBLL.SoLuongGiangVien();
            int SoLuongCauHoi = _statisticBLL.SoLuongCauHoi();
            //tabControl1.TabPages.Remove(tabPage3);
            // load cb 
            lblCountHS.Text = SoLuongSinhVien.ToString();
            lblCountGV.Text = SoLuongGiaoVien.ToString();
            lblCountCauHoi.Text = SoLuongCauHoi.ToString();
            cbLopTab1.DataSource = lopList;
            cbMonHocTab1.DataSource = monList;


            listSVTheoLop = _statisticBLL.ThongKeSVThamGiaThiTheoLop();
            listSVTheoMon = _statisticBLL.ThongKeSVThamGiaThiTheoMon();
        }

        public void LoadDiemSoPanel()
        {
            List<MonHocDTO> mhList = new List<MonHocDTO>();
            listThongKeDiemTheoMon = _statisticBLL.ThongKeDiemTheoMon();
            listThongKeDiemTheoLop = _statisticBLL.ThongKeDiemTheoLop();
            cbDiemTheoMonTab2.DataSource = monList;
            cbDiemTheoLopTab2.DataSource = lopList;
        }

        private void LoadDTBPanel()
        {
            listDTBTheoLop = _statisticBLL.ThongKeDTBTheoLop();
            listDTBTheoMon = _statisticBLL.ThongKeDTBTheoMon();
        }

        //LOAD DATA GRIDVIEW ..

        private void LoadDtgvSVTheoLop(List<ThongKeSVThamGiaThiTheoLopDTO> list)
        {
            dtgvSVThamGiaThi.DataSource = null;
            // Đặt AutoGenerateColumns = true để tự động tạo cột
            dtgvSVThamGiaThi.AutoGenerateColumns = true;

            // Gán lại DataSource
            dtgvSVThamGiaThi.DataSource = list;

            // Điều chỉnh để các cột tự động "fill" vừa toàn bộ chiều rộng DataGridView
            dtgvSVThamGiaThi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Đặt lại header text nếu cần (nếu muốn thay đổi tiêu đề mặc định của cột)
            dtgvSVThamGiaThi.Columns["MaLop"].HeaderText = "Mã lớp";
            dtgvSVThamGiaThi.Columns["TenLop"].HeaderText = "Tên lớp";
            dtgvSVThamGiaThi.Columns["SoLuongSinhVien"].HeaderText = "Số Lượng Sinh Vien";
        }

        private void LoadDtgvSVTheoMon(List<ThongKeSVThamGiaThiTheoMonDTO> list)
        {
            dtgvSVThamGiaThi.DataSource = null;
            // Đặt AutoGenerateColumns = true để tự động tạo cột
            dtgvSVThamGiaThi.AutoGenerateColumns = true;

            // Gán lại DataSource
            dtgvSVThamGiaThi.DataSource = list;

            // Điều chỉnh để các cột tự động "fill" vừa toàn bộ chiều rộng DataGridView
            dtgvSVThamGiaThi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dtgvSVThamGiaThi.Columns["maMonHoc"].HeaderText = "Mã môn";
            dtgvSVThamGiaThi.Columns["tenMonHoc"].HeaderText = "Tên môn học";
            dtgvSVThamGiaThi.Columns["soLuongSV"].HeaderText = "Số Lượng Sinh Viên";
        }


        private void LoadDtgvTheoMon(List<ThongKeDiemTheoMonDTO> listThongKeDiemTheoMon)
        {
            dtgvThongKeDiem.DataSource = null;

            // Đặt AutoGenerateColumns = true để tự động tạo cột
            dtgvThongKeDiem.AutoGenerateColumns = true;

            // Gán lại DataSource
            dtgvThongKeDiem.DataSource = listThongKeDiemTheoMon;

            // Điều chỉnh để các cột tự động "fill" vừa toàn bộ chiều rộng DataGridView
            dtgvThongKeDiem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Đặt lại header text nếu cần (nếu muốn thay đổi tiêu đề mặc định của cột)
            if (dtgvThongKeDiem.Columns.Contains("maMonHoc"))
            {
                dtgvThongKeDiem.Columns["maMonHoc"].HeaderText = "Mã môn";
            }

            dtgvThongKeDiem.Columns["tenMonHoc"].HeaderText = "Tên môn học";
            dtgvThongKeDiem.Columns["diemTrungBinh"].HeaderText = "Điểm trung bình";
            dtgvThongKeDiem.Columns["soBaiThi"].HeaderText = "Số bài thi";

        }
        private void LoadDtgvTheoLop(List<ThongKeDiemTheoLopDTO> listThongKeDiemTheoLop)
        {
            dtgvThongKeDiem.DataSource = null;

            // Đặt AutoGenerateColumns = true để tự động tạo cột
            dtgvThongKeDiem.AutoGenerateColumns = true;

            // Gán lại DataSource
            dtgvThongKeDiem.DataSource = listThongKeDiemTheoLop;

            // Điều chỉnh để các cột tự động "fill" vừa toàn bộ chiều rộng DataGridView
            dtgvThongKeDiem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Đặt lại header text nếu cần (nếu muốn thay đổi tiêu đề mặc định của cột)
            dtgvThongKeDiem.Columns["maLop"].HeaderText = "Mã lớp";
            dtgvThongKeDiem.Columns["tenLop"].HeaderText = "Tên lớp";
            dtgvThongKeDiem.Columns["diemTrungBinh"].HeaderText = "Điểm trung bình";
            dtgvThongKeDiem.Columns["soBaiThi"].HeaderText = "Số bài thi";

        }

        private void LoaddtgvThongKeDTBTheoLop(List<ThongKeDTBTheoLop> list)
        {
            dtgvThongKeDTB.DataSource = null;

            // Đặt AutoGenerateColumns = true để tự động tạo cột
            dtgvThongKeDTB.AutoGenerateColumns = true;

            // Gán lại DataSource
            dtgvThongKeDTB.DataSource = listThongKeDiemTheoLop;

            // Điều chỉnh để các cột tự động "fill" vừa toàn bộ chiều rộng DataGridView
            dtgvThongKeDTB.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Đặt lại header text nếu cần (nếu muốn thay đổi tiêu đề mặc định của cột)
            dtgvThongKeDTB.Columns["maLop"].HeaderText = "Mã lớp";
            dtgvThongKeDTB.Columns["tenLop"].HeaderText = "Tên lớp";
            dtgvThongKeDTB.Columns["diemTrungBinh"].HeaderText = "Điểm trung bình";
            dtgvThongKeDTB.Columns["soBaiThi"].HeaderText = "Số bài thi";

        }

        private void LoaddtgvThongKeDTBTheoMon(List<ThongKeDTBTheoMon> list)
        {
            dtgvThongKeDTB.DataSource = null;

            // Đặt AutoGenerateColumns = true để tự động tạo cột
            dtgvThongKeDTB.AutoGenerateColumns = true;

            // Gán lại DataSource
            dtgvThongKeDTB.DataSource = listThongKeDiemTheoMon;

            // Điều chỉnh để các cột tự động "fill" vừa toàn bộ chiều rộng DataGridView
            dtgvThongKeDTB.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Đặt lại header text nếu cần (nếu muốn thay đổi tiêu đề mặc định của cột)
            dtgvThongKeDTB.Columns["maMonHoc"].HeaderText = "Mã môn học";
            dtgvThongKeDTB.Columns["tenMonHoc"].HeaderText = "Tên môn học";
            dtgvThongKeDTB.Columns["diemTrungBinh"].HeaderText = "Điểm trung bình";
            dtgvThongKeDTB.Columns["soBaiThi"].HeaderText = "Số bài thi";

        }

        //LOAD DATA GRIDVIEW ;

        // UPDATE CHART ..

        // TAB 1
        private void UpdateChartTheoMon(List<ThongKeDiemTheoMonDTO> listThongKeDiemTheoMon)
        {
            LoadDtgvTheoMon(listThongKeDiemTheoMon);
            // Khởi tạo Series với kiểu dữ liệu là ColumnSeries (Bar Chart)
            var columnSeries = new LiveCharts.Wpf.ColumnSeries
            {
                Title = "Điểm số", // Tiêu đề cột
                Values = new ChartValues<double>(listThongKeDiemTheoMon.Select(d => (double)d.diemTrungBinh)), // Giá trị điểm số
                DataLabels = true, // Hiển thị giá trị trên cột
                LabelPoint = point => point.Y.ToString("F1") // Định dạng số trên cột (1 chữ số thập phân)
            };

            // Cập nhật Series vào CartesianChart
            cartesianChart1.Series = new SeriesCollection { columnSeries };

            // Cập nhật trục X (Tên môn học hoặc lớp học)
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Môn học", // Tiêu đề trục X
                Labels = listThongKeDiemTheoMon.Select(d => d.tenMonHoc).ToArray(), // Hiển thị tên môn học
                Separator = new Separator { Step = 1 } // Đảm bảo nhãn không bị trùng
            });

            // Cập nhật trục Y (Điểm số)
            cartesianChart1.AxisY.Clear();
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Điểm số", // Tiêu đề trục Y
                LabelFormatter = value => value.ToString("F1") // Định dạng số hiển thị (1 chữ số thập phân)
            });

            // Tùy chỉnh biểu đồ
            cartesianChart1.LegendLocation = LegendLocation.Right; // Hiển thị chú thích bên phải

        }

        private void UpdateChartTheoLop(List<ThongKeDiemTheoLopDTO> listThongKeDiemTheoLop)
        {
            LoadDtgvTheoLop(listThongKeDiemTheoLop);
            // Khởi tạo Series với kiểu dữ liệu là ColumnSeries (Bar Chart)
            var columnSeries = new LiveCharts.Wpf.ColumnSeries
            {
                Title = "Điểm số", // Tiêu đề cột
                Values = new ChartValues<decimal>(listThongKeDiemTheoLop.Select(d => d.diemTrungBinh)), // Giá trị điểm số
                DataLabels = true, // Hiển thị giá trị trên cột
                LabelPoint = point => point.Y.ToString("F1") // Định dạng số trên cột (1 chữ số thập phân)
            };

            // Cập nhật Series vào CartesianChart
            cartesianChart1.Series = new SeriesCollection { columnSeries };

            // Cập nhật trục X (Tên lớp học)
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Tên lớp học", // Tiêu đề trục X
                Labels = listThongKeDiemTheoLop.Select(d => d.tenLop).ToArray(), // Hiển thị tên lớp học
                Separator = new Separator { Step = 1 } // Đảm bảo các nhãn không bị trùng
            });

            // Cập nhật trục Y (Điểm số)
            cartesianChart1.AxisY.Clear();
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Điểm trung bình", // Tiêu đề trục Y
                LabelFormatter = value => value.ToString("F1") // Định dạng số hiển thị (1 chữ số thập phân)
            });

            // Tùy chỉnh biểu đồ
            cartesianChart1.LegendLocation = LegendLocation.Right; // Hiển thị chú thích bên phải

        }

        // TAB 2

        private void UpdateChartSVTheoMon(List<ThongKeSVThamGiaThiTheoMonDTO> list)
        {
            LoadDtgvSVTheoMon(list);
            // Khởi tạo Series với kiểu dữ liệu là LineSeries
            var lineSeries = new LiveCharts.Wpf.LineSeries
            {
                Title = "Số lượng sinh viên", // Tiêu đề dòng
                Values = new ChartValues<int>(list.Select(d => d.soLuongSV)), // Giá trị điểm số
                PointGeometry = DefaultGeometries.Circle, // Hình dạng điểm trên Line
                PointGeometrySize = 10, // Kích thước điểm
            };

            // Cập nhật Series vào CartesianChart
            cartesianChart2.Series = new SeriesCollection { lineSeries };

            // Cập nhật trục X (Tên môn học hoặc lớp học)
            cartesianChart2.AxisX.Clear();
            cartesianChart2.AxisX.Add(new Axis
            {
                Title = "Môn học",
                Labels = list.Select(d => d.tenMonHoc).ToArray(), // Hiển thị tên môn học
                Separator = new Separator { Step = 1 } // Đảm bảo các nhãn không bị trùng
            });

            // Cập nhật trục Y (Điểm số)
            cartesianChart2.AxisY.Clear();
            cartesianChart2.AxisY.Add(new Axis
            {
                Title = "Số lượng sinh viên",
                LabelFormatter = value => value.ToString("F1") // Hiển thị 1 chữ số thập phân
            });
        }

        private void UpdateChartSVTheoLop(List<ThongKeSVThamGiaThiTheoLopDTO> list)
        {
            LoadDtgvSVTheoLop(list);
            // Khởi tạo Series với kiểu dữ liệu là LineSeries
            var lineSeries = new LiveCharts.Wpf.LineSeries
            {
                Title = "Số lượng sinh viên", // Tiêu đề dòng
                Values = new ChartValues<int>(list.Select(d => d.soLuongSinhVien)), // Giá trị điểm số
                PointGeometry = DefaultGeometries.Circle, // Hình dạng điểm trên Line
                PointGeometrySize = 10, // Kích thước điểm
            };

            // Cập nhật Series vào CartesianChart
            cartesianChart2.Series = new SeriesCollection { lineSeries };

            // Cập nhật trục X (Tên môn học hoặc lớp học)
            cartesianChart2.AxisX.Clear();
            cartesianChart2.AxisX.Add(new Axis
            {
                Title = "Lớp học",
                Labels = list.Select(d => d.tenLop).ToArray(), // Hiển thị tên môn học
                Separator = new Separator { Step = 1 } // Đảm bảo các nhãn không bị trùng
            });

            // Cập nhật trục Y (Điểm số)
            cartesianChart2.AxisY.Clear();
            cartesianChart2.AxisY.Add(new Axis
            {
                Title = "Số lượng sinh viên",
                LabelFormatter = value => value.ToString("F1") // Hiển thị 1 chữ số thập phân
            });
        }

        // TAB 3

        private void UpdateChartDTBTheoLop(List<ThongKeDTBTheoLop> list)
        {
            LoaddtgvThongKeDTBTheoLop(list);
            // Khởi tạo Series với kiểu dữ liệu là LineSeries
            var lineSeries = new LiveCharts.Wpf.LineSeries
            {
                Title = "Điểm trung bình", // Tiêu đề dòng
                Values = new ChartValues<decimal>(list.Select(d => d.diemTrungBinh)), // Giá trị điểm số
                PointGeometry = DefaultGeometries.Circle, // Hình dạng điểm trên Line
                PointGeometrySize = 10, // Kích thước điểm
            };

            // Cập nhật Series vào CartesianChart
            cartesianChart3.Series = new SeriesCollection { lineSeries };

            // Cập nhật trục X (Tên môn học hoặc lớp học)
            cartesianChart3.AxisX.Clear();
            cartesianChart3.AxisX.Add(new Axis
            {
                Title = "Lớp học",
                Labels = list.Select(d => d.tenLop).ToArray(), // Hiển thị tên môn học
                Separator = new Separator { Step = 1 } // Đảm bảo các nhãn không bị trùng
            });

            // Cập nhật trục Y (Điểm số)
            cartesianChart3.AxisY.Clear();
            cartesianChart3.AxisY.Add(new Axis
            {
                Title = "Điểm trung bình",
                LabelFormatter = value => value.ToString("F1") // Hiển thị 1 chữ số thập phân
            });
        }

        private void UpdateChartDTBTheoMon(List<ThongKeDTBTheoMon> list)
        {
            LoaddtgvThongKeDTBTheoMon(list);
            // Khởi tạo Series với kiểu dữ liệu là LineSeries
            var lineSeries = new LiveCharts.Wpf.LineSeries
            {
                Title = "Điểm trung bình", // Tiêu đề dòng
                Values = new ChartValues<decimal>(list.Select(d => d.diemTrungBinh)), // Giá trị điểm số
                PointGeometry = DefaultGeometries.Circle, // Hình dạng điểm trên Line
                PointGeometrySize = 10, // Kích thước điểm
            };

            // Cập nhật Series vào CartesianChart
            cartesianChart3.Series = new SeriesCollection { lineSeries };

            // Cập nhật trục X (Tên môn học hoặc lớp học)
            cartesianChart3.AxisX.Clear();
            cartesianChart3.AxisX.Add(new Axis
            {
                Title = "Môn học",
                Labels = list.Select(d => d.tenMonHoc).ToArray(), // Hiển thị tên môn học
                Separator = new Separator { Step = 1 } // Đảm bảo các nhãn không bị trùng
            });

            // Cập nhật trục Y (Điểm số)
            cartesianChart3.AxisY.Clear();
            cartesianChart3.AxisY.Add(new Axis
            {
                Title = "Điểm trung bình",
                LabelFormatter = value => value.ToString("F1") // Hiển thị 1 chữ số thập phân
            });
        }


        // UPDATE CHART ;

        // EVENT ..

        private void cbDiemTheoLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tenLop = cbDiemTheoMonTab2.Text;
            List<ThongKeDiemTheoLopDTO> listRs = listThongKeDiemTheoLop
                .Where(x => x.tenLop == tenLop) // Điều kiện lọc theo tên lớp
                .ToList();

            UpdateChartTheoLop(listRs);
        }

        private void btnTheoTatCaMon_Click(object sender, EventArgs e)
        {

            UpdateChartTheoMon(listThongKeDiemTheoMon);
        }

        private void btnTheoTatCaLop_Click(object sender, EventArgs e)
        {
            UpdateChartTheoLop(listThongKeDiemTheoLop);
        }

        private void tabPage1_Paint(object sender, PaintEventArgs e)
        {
            LoadSinhVienThamGiaThiPanel();
            UpdateChartSVTheoMon(listSVTheoMon);
        }

        private void tabPage2_Paint(object sender, PaintEventArgs e)
        {
            LoadDiemSoPanel();
            UpdateChartTheoMon(listThongKeDiemTheoMon);

        }

        private void cbDiemTheoMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tenMon = cbDiemTheoLopTab2.Text;
            List<ThongKeDiemTheoMonDTO> listRs = listThongKeDiemTheoMon
                .Where(x => x.tenMonHoc == tenMon) // Điều kiện lọc theo tên lớp
                .ToList();

            UpdateChartTheoMon(listRs);
        }

        private void btnSvTheoMon_Click(object sender, EventArgs e)
        {
            UpdateChartSVTheoMon(listSVTheoMon);
        }

        private void btnSVTheoLop_Click(object sender, EventArgs e)
        {
            UpdateChartSVTheoLop(listSVTheoLop);
        }

        private void cbMonHocTab1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tenMon = cbMonHocTab1.Text;
            List<ThongKeSVThamGiaThiTheoMonDTO> list = listSVTheoMon
                .Where(x => x.tenMonHoc == tenMon)
                .ToList();
            UpdateChartSVTheoMon(list);
        }

        private void cbLopTab1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tenLop = cbLopTab1.Text;
            List<ThongKeSVThamGiaThiTheoLopDTO> list = listSVTheoLop
                .Where(x => x.tenLop == tenLop)
                .ToList();
            UpdateChartSVTheoLop(list);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateChartDTBTheoMon(listDTBTheoMon);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateChartDTBTheoLop(listDTBTheoLop);
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            UpdateChartDTBTheoMon(listDTBTheoMon);
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            UpdateChartDTBTheoLop(listDTBTheoLop);
        }

        private void tableLayoutPanel21_Paint(object sender, PaintEventArgs e)
        {
            UpdateChartDTBTheoMon(listDTBTheoMon);
        }

        // EVENT ;
    }
}
