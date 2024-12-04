using BLL;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.CustomUI;
using DTO;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComboBox = System.Windows.Forms.ComboBox;
using System.Diagnostics;

namespace GUI.LopHoc
{
    public partial class fDanhSachSV : Form
    {
        private LopDTO lopDTO;
        private DataTable dt;
        private DataTable dt1;

        private NguoiDungBLL nguoiDungBLL;
        private ChiTietLopBLL chiTietLopBLL;
        private LopBLL lopBLL;
        private DeThiBLL deThiBLL;
        private ThongKeBLL thongKeBLL;

        private List<NguoiDungDTO> lHocSinhTrongLop;
        private Dictionary<NguoiDungDTO, KetQuaDTO> listDiemTBCuaHs;
        private Dictionary<NguoiDungDTO, KetQuaDTO> listTop5HsCoDiemCaoNhat;
        private Dictionary<NguoiDungDTO, KetQuaDTO>  lDTB;
        private List<DeThiDTO> listDeThiCuaLop;
        private List<string> listHoTenHs;
        private List<DeThiDTO> listDeThi;
        private List<string> listTrangThai;


        private string selectedTrangThai;
        private int selectedIdDeThi;
        private long selectedIdND = 0;
        private int soLuongHsDaNopBai = 0;
        private int soLuongDeThiCoTrongLop  = 0;
        private int soLuongDeThiDangMo = 0;
        public fDanhSachSV(LopDTO lopDTO)
        {
            nguoiDungBLL = new NguoiDungBLL();
            chiTietLopBLL = new ChiTietLopBLL();
            thongKeBLL = new ThongKeBLL();
            lopBLL = new LopBLL();
            deThiBLL = new DeThiBLL();
            InitializeComponent();
            if(fDangNhap.nhomQuyenDTO.TenQuyen.Contains("Học sinh"))
            {
                tabThongKe.TabPages.Remove(tabPage2); // Loại bỏ tabPage2 khỏi tabThongKe
            }
            this.lopDTO = lopDTO;
            lblCountSLDeThi.Text = thongKeBLL.GetSoLuongDeThi(lopDTO.MaLop).ToString();
            lblCountDtDangMo.Text = thongKeBLL.getCountCauHoi().ToString();

            lHocSinhTrongLop = chiTietLopBLL.GetSV(lopDTO.MaLop) ?? new List<NguoiDungDTO>();
            listDiemTBCuaHs = thongKeBLL.GetAllDiemTBCuaHs(lopDTO.MaLop) ?? new Dictionary<NguoiDungDTO, KetQuaDTO>();
            listTop5HsCoDiemCaoNhat = thongKeBLL.GetTop5HsCoDiemCaoNhatTheoDeThi(lopDTO.MaLop, selectedIdDeThi) ?? new Dictionary<NguoiDungDTO, KetQuaDTO>();

            soLuongDeThiCoTrongLop = deThiBLL.GetAllDeThiCuaLop(lopDTO).Count;
            lDTB = thongKeBLL.GetAllDiemTBCuaHs(lopDTO.MaLop);
            listDeThiCuaLop = deThiBLL.GetAllDeThiCuaLop(lopDTO);
            soLuongHsDaNopBai = thongKeBLL.getSlHSDaNopBai(lopDTO.MaLop, selectedIdDeThi);
            if (listHoTenHs == null)
            {
                listHoTenHs = new List<string>();
                listHoTenHs.Add("Tất cả");
            }
            if (listDiemTBCuaHs == null)
            {
                listDiemTBCuaHs = new Dictionary<NguoiDungDTO, KetQuaDTO>();
            }
            if (listDeThi == null)
            {
                listDeThi = new List<DeThiDTO>();
            }
            if (listTop5HsCoDiemCaoNhat == null)
            {
                listTop5HsCoDiemCaoNhat = new Dictionary<NguoiDungDTO, KetQuaDTO>();
            }
            if (listTrangThai == null)
            {
                listTrangThai = new List<string> { "Tất cả", "Đã nộp", "Chưa nộp" };
            }
            foreach (DeThiDTO item in listDeThiCuaLop)
            {
                listDeThi.Add(deThiBLL.GetById(item));
                if (item.TrangThai == 0)
                {
                    soLuongDeThiDangMo++;
                }
            }
            //foreach (var item in lDTB)
            //{
            //    listHoTenHs.Add(item.Key.HoTen);
            //    if (soLuongDeThiCoTrongLop == 0)
            //    {
            //        listDiemTBCuaHs[item.Key] = null;
            //        continue;
            //    }
            //    KetQuaDTO kq = new KetQuaDTO
            //    {
            //        Diem = item.Value.Diem / soLuongDeThiCoTrongLop
            //    };
            //    listDiemTBCuaHs[item.Key] = kq;
            //}
            dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã học sinh", typeof(long));
            dt.Columns.Add("Họ và tên", typeof(string));
            dt.Columns.Add("SDT", typeof(string));
            //dt2
            dt1 = new DataTable();
            dt1.Columns.Add("STT", typeof(int));
            dt1.Columns.Add("Mã học sinh", typeof(long));
            dt1.Columns.Add("Họ và tên", typeof(string));
            dt1.Columns.Add("SDT", typeof(string));
            dt1.Columns.Add("Điểm", typeof(double));

            if(!fDangNhap.nhomQuyenDTO.TenQuyen.Contains("Học sinh"))
            {
                load();
                loadDataGridView();
                loadCbTrangThai();
                loadCBHocSinh();
                loadCbDeThi();
                loadChartTongQuan();
                loadPieChart(soLuongHsDaNopBai);
                loadChartTop5HsDiemCao();
                StyleDataGridView();
            } else
            {
                loadDataGridView();

            }
            //load();
            //loadDataGridView();
            //loadCbTrangThai();
            //loadCBHocSinh();
            //loadCbDeThi();
            //loadChartTongQuan();
            //loadPieChart(soLuongHsDaNopBai);
            //loadChartTop5HsDiemCao();
            //StyleDataGridView();
        }
        private void load()
        {
            lblCountSLDeThi.Text = soLuongDeThiCoTrongLop.ToString();
            lblCountDtDangMo.Text = soLuongDeThiDangMo.ToString();
        }
        private void loadCbTrangThai()
        {
            cbTrangThai.DataSource = listTrangThai;
            //cbTrangThai.SelectedIndex = 0;
        }
        private void loadCbDeThi()
        {
            try
            {
                cbDeThi.ValueMember = "MaDe";
                cbDeThi.DisplayMember = "TenDe";
                cbDeThi.DataSource = listDeThi;
                cbDeThi.SelectedIndex = 0;
                selectedIdDeThi = Convert.ToInt32( cbDeThi.SelectedValue?.ToString());
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        private void loadCBHocSinh()
        {
            try
            {
                cbbSV.ValueMember = "MaNguoiDung";
                cbbSV.DisplayMember = "HoTen";
                cbbSV.DataSource = lHocSinhTrongLop;
                cbbSV.SelectedIndex = 0;
            } catch (Exception e)
            {
                e.ToString();
            }
        }
        private void loadDataGridView()
        {
            dt.Clear();
            dataGridView1.DataSource = null;
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
            StyleDataGridView();

        }
        private void loadDataGridView2()
        {
            dt1.Clear();
            dataGridView2.DataSource = null;
            int stt = 1;
            foreach (NguoiDungDTO hs in lHocSinhTrongLop)
            {
                double diem = thongKeBLL.getDiemCuaDeThiByUserId(lopDTO.MaLop, selectedIdDeThi, hs.MaNguoiDung);
                if (diem == -1)
                    continue;
                if (selectedTrangThai.Equals("Tất cả"))
                {
                    DataRow row = dt1.NewRow();
                    row["STT"] = stt;
                    row["Mã học sinh"] = hs.MaNguoiDung;
                    row["Họ và tên"] = hs.HoTen;
                    row["SDT"] = hs.SDT;
                    row["Điểm"] = diem;
                    dt1.Rows.Add(row);
                    stt++;
                }
                else if (selectedTrangThai.Equals("Đã nộp"))
                {
                    if (diem != -1)
                    {
                        DataRow row = dt1.NewRow();
                        row["STT"] = stt;
                        row["Mã học sinh"] = hs.MaNguoiDung;
                        row["Họ và tên"] = hs.HoTen;
                        row["SDT"] = hs.SDT;
                        row["Điểm"] = diem;
                        dt1.Rows.Add(row);
                        stt++;
                    }
                }
                else if (selectedTrangThai.Equals("Chưa nộp"))
                {
                    if (diem == -1)
                    {
                        DataRow row = dt1.NewRow();
                        row["STT"] = stt;
                        row["Mã học sinh"] = hs.MaNguoiDung;
                        row["Họ và tên"] = hs.HoTen;
                        row["SDT"] = hs.SDT;
                        row["Điểm"] = diem;
                        dt1.Rows.Add(row);
                        stt++;

                    }
                }

            }
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.DataSource = dt1;
            dataGridView2.Columns["STT"].Width = 250;
            dataGridView2.Columns["Mã học sinh"].Width = 250;
            dataGridView2.Columns["Họ và tên"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns["SDT"].Width = 200;
            dataGridView2.Columns["Điểm"].Width = 100;
        }
        private void cbDeThi_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedValue != null)
            {
                selectedIdDeThi = Convert.ToInt32(cb.SelectedValue);
                soLuongHsDaNopBai = thongKeBLL.getSlHSDaNopBai(lopDTO.MaLop, selectedIdDeThi);
                loadPieChart(soLuongHsDaNopBai);
                loadChartTop5HsDiemCao();
                loadDataGridView2();
            }
        }
        private void cbTrangThai_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedValue != null)
            {
                selectedTrangThai = cb.SelectedValue.ToString();
                loadDataGridView2();
            }
        }
        private void btnXuatDSHS_Click(object sender, EventArgs e)
        {
            string tenLop = lopDTO.TenLop;
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Workbook|*.xlsx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            DataTable dt = (DataTable)dataGridView1.DataSource;

                            if (dt != null)
                            {
                                var worksheet = workbook.Worksheets.Add("Sheet1");
                                var tenlop = worksheet.Cell(1, 1);
                                tenlop.Value = "Tên lớp: " + tenLop;
                                tenlop.Style.Font.Bold = true;

                                worksheet.Cell(2, 1).InsertTable(dt.AsEnumerable());

                                workbook.SaveAs(sfd.FileName);
                                MessageBox.Show("Xuất thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Không có dữ liệu để xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        public void StyleDataGridView()
        {
            dataGridView1.Columns["STT"].Width = 100;
            dataGridView1.Columns["Mã học sinh"].Width = 350;
            dataGridView1.Columns["SDT"].Width = 250;
            dataGridView1.Columns["Họ và tên"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


        }
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void loadChartTongQuan()
        {
            cartesianChart1.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Điểm trung bình",
                    Values = listDiemTBCuaHs.Where(x => x.Value != null).Select(x => (decimal)x.Value.Diem).AsChartValues(),

                }
            };


            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Họ tên học sinh",
                Labels = listDiemTBCuaHs.Select(x => x.Key.HoTen).ToArray(),
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Điểm trung bình",
                LabelFormatter = value => value.ToString("N2")
            });

        }

        private void loadPieChart(int slhsdn)
        {
            if (soLuongDeThiCoTrongLop == 0)
                return;
            slhsdn = soLuongHsDaNopBai;
            Func<ChartPoint, string> labelPoint = chartPoint =>
        string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            pieChart1.Series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Đã nộp bài",
                    Values = new ChartValues<int> {slhsdn},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "Chưa nộp bài",
                    Values = new ChartValues<int> {lHocSinhTrongLop.Count - slhsdn},
                    DataLabels = true,
                    LabelPoint = labelPoint
                }
            };

            pieChart1.LegendLocation = LegendLocation.Bottom;
        }

        private void loadChartTop5HsDiemCao()
        {
            // Khởi cột giá trị cho biểu đồ thống kê top 5 hs có điểm cao nhất theo đề thi
            SeriesCollection columnSeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Điểm",
                    Values = new ChartValues<double>(),
                }
            };
            cartesianChart2.Series = columnSeriesCollection;
            if (cartesianChart2.Series.Any())
            {
                var columnSeries = cartesianChart2.Series[0] as ColumnSeries;
                if (columnSeries == null)
                {
                    return; // Đảm bảo rằng Series tồn tại
                }

                listTop5HsCoDiemCaoNhat = thongKeBLL.GetTop5HsCoDiemCaoNhatTheoDeThi(lopDTO.MaLop, selectedIdDeThi);
                List<decimal> lDiem = new List<decimal>();
                List<string> lname = new List<string>();
                foreach (var item in listTop5HsCoDiemCaoNhat)
                {
                    if (item.Value != null)
                    {
                        lDiem.Add(item.Value.Diem);
                        lname.Add(item.Key.HoTen);
                    }
                }

                // Cập nhật giá trị của Series hiện có
                columnSeries.Values = lDiem.Select(value => (double)value).AsChartValues();

                // Kiểm tra xem AxisX đã tồn tại
                if (cartesianChart2.AxisX.Count > 0)
                {
                    // Nếu tồn tại, xóa nó đi
                    cartesianChart2.AxisX.Clear();
                }

                // Tạo một Axis mới cho cột X
                cartesianChart2.AxisX.Add(new Axis
                {
                    Title = "Họ tên học sinh",
                    Labels = lname
                });
            }
        }

        private void cbbSV_SelectedValueChanged(object sender, EventArgs e)
        {
            //ComboBox cb = sender as ComboBox;
            //if (cb.SelectedValue != null)
            //{
            //    selectedIdND = Convert.ToInt64(cb.SelectedValue);
            //    //soLuongHsDaNopBai = thongKeBLL.getSlHSDaNopBai(lopDTO.MaLop, selectedIdND);
            //    //loadPieChart(soLuongHsDaNopBai);
            //    loadChartTop5HsDiemCao();
            //    loadDataGridView2();
            //}
        }
    }
}
