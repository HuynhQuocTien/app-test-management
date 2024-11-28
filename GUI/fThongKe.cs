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
        private List<ThongKeDiemDTO> _thongKeDiemResult = new List<ThongKeDiemDTO> ();
        private LopBLL _lopBll = new LopBLL();
        private MonHocBLL _monHocBLL = new MonHocBLL();
        private List<LopDTO> lopList = new List<LopDTO> ();
        private List<MonHocDTO> monList = new List<MonHocDTO>();
        public fThongKe()
        {
            InitializeComponent();
            GeneralLoad();
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
            // load cb 
            lblCountHS.Text = SoLuongSinhVien.ToString();
            lblCountGV.Text = SoLuongGiaoVien.ToString();
            lblCountCauHoi.Text = SoLuongCauHoi.ToString();
            cbLopTab1.DataSource = lopList;
            cbMonHocTab1.DataSource = monList;


            listSVTheoLop = _statisticBLL.ThongKeSVThamGiaThiTheoLop();
            listSVTheoMon = _statisticBLL.ThongKeSVThamGiaThiTheoMon();
        }

        public void updateChartSinhVienThamGiaThi()
        {

        }

        public void LoadDiemSoPanel()
        {
            List<MonHocDTO> mhList = new List<MonHocDTO>();
            listThongKeDiemTheoMon = _statisticBLL.ThongKeDiemTheoMon();
            listThongKeDiemTheoLop = _statisticBLL.ThongKeDiemTheoLop();
            //MessageBox.Show(listThongKeDiemTheoMon.Count().ToString());
            cbDiemTheoLop.DataSource = lopList;
            cbDiemTheoMon.DataSource = monList;
        }

        private void LoadDtgvSVTheoLop(List<ThongKeSVThamGiaThiTheoLopDTO> list)
        {
            dtgvSVThamGiaThi.DataSource = null;
            dtgvSVThamGiaThi.Columns.Clear();

            // Tạo các cột và gắn DataPropertyName tương ứng
            DataGridViewTextBoxColumn maMonColumn = new DataGridViewTextBoxColumn();
            maMonColumn.HeaderText = "Mã lớp";
            maMonColumn.DataPropertyName = "MaLop"; // Tên thuộc tính trong DTO hoặc đối tượng dữ liệu
            dtgvThongKeDiem.Columns.Add(maMonColumn);

            DataGridViewTextBoxColumn tenMonColumn = new DataGridViewTextBoxColumn();
            tenMonColumn.HeaderText = "Tên lớp";
            tenMonColumn.DataPropertyName = "TenLop"; // Tên thuộc tính trong DTO hoặc đối tượng dữ liệu
            dtgvThongKeDiem.Columns.Add(tenMonColumn);

            DataGridViewTextBoxColumn diemTBColumn = new DataGridViewTextBoxColumn();
            diemTBColumn.HeaderText = "Số sinh viên tham gia thi";
            diemTBColumn.DataPropertyName = "SoLuongSV"; // Tên thuộc tính trong DTO hoặc đối tượng dữ liệu
            dtgvThongKeDiem.Columns.Add(diemTBColumn);

            // Gán lại DataSource
            dtgvSVThamGiaThi.DataSource = list;
        }

        private void LoadDtgvSVTheoMon(List<ThongKeSVThamGiaThiTheoMonDTO> list)
        {
            dtgvSVThamGiaThi.DataSource = null;
            dtgvSVThamGiaThi.Columns.Clear();

            // Tạo các cột và gắn DataPropertyName tương ứng
            DataGridViewTextBoxColumn maMonColumn = new DataGridViewTextBoxColumn();
            maMonColumn.HeaderText = "Mã môn";
            maMonColumn.DataPropertyName = "MaMon"; // Tên thuộc tính trong DTO hoặc đối tượng dữ liệu
            dtgvThongKeDiem.Columns.Add(maMonColumn);

            DataGridViewTextBoxColumn tenMonColumn = new DataGridViewTextBoxColumn();
            tenMonColumn.HeaderText = "Tên môn";
            tenMonColumn.DataPropertyName = "TenMon"; // Tên thuộc tính trong DTO hoặc đối tượng dữ liệu
            dtgvThongKeDiem.Columns.Add(tenMonColumn);

            DataGridViewTextBoxColumn diemTBColumn = new DataGridViewTextBoxColumn();
            diemTBColumn.HeaderText = "Số sinh viên tham gia thi";
            diemTBColumn.DataPropertyName = "SoLuongSV"; // Tên thuộc tính trong DTO hoặc đối tượng dữ liệu
            dtgvThongKeDiem.Columns.Add(diemTBColumn);

            // Gán lại DataSource
            dtgvSVThamGiaThi.DataSource = list;
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
        private void LoadDtgvTheoLop(List<ThongKeDiemTheoLopDTO> listThongKeDiemTheoLop)
        {
            dtgvThongKeDiem.DataSource = null; // Đặt DataSource về null trước khi thêm cột
            dtgvThongKeDiem.Columns.Clear();  // Xóa các cột hiện tại trong DataGridView

            // Tạo các cột và gắn DataPropertyName tương ứng
            DataGridViewTextBoxColumn maMonColumn = new DataGridViewTextBoxColumn();
            maMonColumn.HeaderText = "Mã lớp";
            maMonColumn.DataPropertyName = "MaLop"; // Tên thuộc tính trong DTO hoặc đối tượng dữ liệu
            dtgvThongKeDiem.Columns.Add(maMonColumn);

            DataGridViewTextBoxColumn tenMonColumn = new DataGridViewTextBoxColumn();
            tenMonColumn.HeaderText = "Tên lớp";
            tenMonColumn.DataPropertyName = "TenLop"; // Tên thuộc tính trong DTO hoặc đối tượng dữ liệu
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
            dtgvThongKeDiem.DataSource = listThongKeDiemTheoLop;

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

        private void UpdateChartSVTheoMon(List<ThongKeSVThamGiaThiTheoMonDTO> list)
        {
            LoadDtgvSVTheoMon(list);
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
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
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
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

        private void UpdateChartTheoLop(List<ThongKeDiemTheoLopDTO> listThongKeDiemTheoLop)
        {
            LoadDtgvTheoLop(listThongKeDiemTheoLop);
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

            UpdateChartTheoMon(listThongKeDiemTheoMon);
        }

        private void btnTheoTatCaLop_Click(object sender, EventArgs e)
        {
            UpdateChartTheoLop(listThongKeDiemTheoLop);
        }

        private void tabPage1_Paint(object sender, PaintEventArgs e)
        {
            LoadSinhVienThamGiaThiPanel();
        }

        private void tabPage2_Paint(object sender, PaintEventArgs e)
        {
            LoadDiemSoPanel();
        }

        private void cbDiemTheoMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tenMon = cbDiemTheoMon.Text;
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
    }
}
