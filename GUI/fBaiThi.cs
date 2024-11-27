using BLL;
using DAL;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.CustomUI;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.VariantTypes;
using DTO;
using GUI.LopHoc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OfficeOpenXml.ExcelErrorValue;
using Color = System.Drawing.Color;
using Control = System.Windows.Forms.Control;
using Font = System.Drawing.Font;
using GroupBox = System.Windows.Forms.GroupBox;
using Size = System.Drawing.Size;

namespace GUI
{
    public partial class fBaiThi : Form
    {
        private DeThiDTO deThi;
        private LopDTO lop;
        private fChiTietLop fChiTietLop;

        private KetQuaBLL ketQuaBLL;
        private DeThiBLL DeThiBLL;
        private CauHoiBLL cauHoiBLL;
        private ChiTietDeBLL chiTietDeBLL;
        private MonHocBLL monHocBLL;
        private NguoiDungBLL nguoiDungBLL;

        private ChiTietDeDaLamBLL chiTietDeDaLamBLL;
        private CauHoiDaLamBLL cauHoiDaLamBLL;

        private CauTraLoiBLL cauTraLoiBLL;
        private CauTraLoiDaLamBLL cauTraLoiDaLamBLL;
        

        private CauTraLoiDienChoTrongBLL cauTraLoiDienChoTrongBLL;
        private CauTraLoiDienChoTrongDaLamBLL cauTraLoiDienChoTrongDaLamBLL;

        private NoiCauDaLamBLL noiCauDaLamBLL;
        private NoiCauTraLoiDaLamBLL noiCauTraLoiDaLamBLL;

        private NoiCauBLL noiCauBLL;
        private NoiCauTraLoiBLL noiCauTraLoiBLL;

        private GroupBox[] groupBox;
        private Panel[] slide;

        private int currentIndex = 0;
        private int soCauChuaChon = 0;
        private Timer countdownTimer;
        private int remainingTimeInSeconds; // Số giây còn lại

        List<CauHoiDaLamDTO> cauHoiDaLamDTOs = new List<CauHoiDaLamDTO>();
        List<CauTraLoiDTO> cauTraLoiDTOs = new List<CauTraLoiDTO>();
        List<CauTraLoiDienChoTrongDTO> cauTraLoiDienChoTrongDTOs = new List<CauTraLoiDienChoTrongDTO>();
        List<NoiCauDTO> noiCauDTOs = new List<NoiCauDTO>();
        List<NoiCauTraLoiDTO> noiCauTraLoiDTOs = new List<NoiCauTraLoiDTO>();
        List<ChiTietDeDaLamDTO> chiTietDeDaLamDTOs = new List<ChiTietDeDaLamDTO>();
        private int so_cau_hoi;
        private int flag = -1; // dat co dong form
        private int counter = 1;
        Dictionary<int, List<string>> listAns;
        Dictionary<string, string> noiCauNguoiDung;
        List<CauHoiDTO> dsCauHoi;
        public fBaiThi(DeThiDTO deThi,LopDTO lop, fChiTietLop fChiTietLop)
        {
            this.deThi = deThi;
            this.lop = lop;
            this.fChiTietLop = fChiTietLop;

            ketQuaBLL = new KetQuaBLL();
            DeThiBLL = new DeThiBLL();
            cauHoiBLL = new CauHoiBLL();
            monHocBLL = new MonHocBLL();
            chiTietDeBLL = new ChiTietDeBLL();
            nguoiDungBLL = new NguoiDungBLL();

            chiTietDeDaLamBLL = new ChiTietDeDaLamBLL();
            cauHoiDaLamBLL = new CauHoiDaLamBLL();
            cauTraLoiBLL = new CauTraLoiBLL();
            cauTraLoiDaLamBLL = new CauTraLoiDaLamBLL();
            cauTraLoiDienChoTrongBLL = new CauTraLoiDienChoTrongBLL();
            cauTraLoiDienChoTrongDaLamBLL = new CauTraLoiDienChoTrongDaLamBLL();
            noiCauDaLamBLL = new NoiCauDaLamBLL();
            noiCauTraLoiDaLamBLL = new NoiCauTraLoiDaLamBLL();
            noiCauTraLoiBLL = new NoiCauTraLoiBLL();
            noiCauBLL = new NoiCauBLL();

            listAns = new Dictionary<int, List<string>>();
            noiCauNguoiDung = new Dictionary<string, string>();
            dsCauHoi = chiTietDeBLL.GetAllCauHoiOfDeThi(deThi);
            so_cau_hoi = dsCauHoi.Count;

            Random random = new Random();
            dsCauHoi.Sort((x, y) => random.Next(-1, 2));
            
            InitializeComponent();
            label1.Text = "1/" + so_cau_hoi.ToString();

            TaoCauHoi(dsCauHoi);
            tao_slide(dsCauHoi);
            loadData();            

        }
        private void loadData()
        {
            lblTenThiSinh.Text = fDangNhap.nguoiDungDTO.HoTen;
            lblNgaySinh.Text = fDangNhap.nguoiDungDTO.NgaySinh.ToString();
            lblMonThi.Text = monHocBLL.GetMonHocById(deThi.MaMonHoc).TenMonHoc;
            lblLop.Text = lop.TenLop.ToString();
            lblNgayThi.Text = DateTime.Now.ToString();
            lblSoCauHoi.Text = so_cau_hoi.ToString();
            Load_pictureBox1();

            // Khởi tạo đối tượng Timer và cấu hình nó
            countdownTimer = new System.Windows.Forms.Timer();
            countdownTimer.Interval = 1000; // Mỗi lần đếm là 1 giây (1000 ms)
            countdownTimer.Tick += new EventHandler(CountdownTimer_Tick);
            countdownTimer.Start();

            // Đặt thời gian ban đầu là 15 phút (900 giây)
            remainingTimeInSeconds =deThi.ThoiGianLamBai*60;
            UpdateTimerLabel();
        }
        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (remainingTimeInSeconds > 0)
            {
                remainingTimeInSeconds--;
                UpdateTimerLabel();
            }
            else
            {
                // Thời gian đã hết, bạn có thể thực hiện các hành động tương ứng ở đây.
                countdownTimer.Stop();
                NopBai();
            }

        }
        private void Load_pictureBox1()
        {
            // Lấy đường dẫn của thư mục chứa file thực thi của ứng dụng
            string exePath = Application.StartupPath;

            for (int i = 0; i < 5; i++)
            {
                DirectoryInfo parent = Directory.GetParent(exePath);
                if (parent == null)
                {
                    throw new DirectoryNotFoundException($"Không thể đi ngược 5 cấp thư mục từ {Application.StartupPath}");
                }
                exePath = parent.FullName;
            }
            // Tạo đường dẫn đến thư mục Avatar
            string avatarFolderPath = Path.Combine(exePath, "GUI", "Users", "Avatar");
            // Lấy tên file ảnh từ cơ sở dữ liệu
            string tenAnh = fDangNhap.nguoiDungDTO.Avatar;

            string defaultImagePath = Path.Combine(avatarFolderPath, "images.png");
            // Tạo đường dẫn đầy đủ tới file ảnh
            string imagePath = Path.Combine(avatarFolderPath, tenAnh);

            // Kiểm tra xem file ảnh có tồn tại hay không
            if (File.Exists(imagePath))
            {
                // Gán ảnh vào PictureBox nếu file tồn tại
                pictureBox1.Image = Image.FromFile(imagePath);

            }
            else
            {
                // Nếu không tìm thấy ảnh, bạn có thể hiển thị ảnh mặc định
                pictureBox1.Image = Image.FromFile(defaultImagePath); ; // Hoặc gán ảnh mặc định
            }
        }
        private void UpdateTimerLabel()
        {
            int minutes = remainingTimeInSeconds / 60;
            int seconds = remainingTimeInSeconds % 60;
            lblThoiGianLamBai.Text = $"{minutes:00}:{seconds:00}";
        }
        private void NopBai()
        {
            flag = 1;
            int d = 0;
            int s = 0;
            int DTNCDung = 0;
            int DTNCSai = 0;
            double diemDTNC = 0.00;
            double diemCuaMotCauDung = (10.00f / so_cau_hoi);
            double diem = 0.00;
            int maKQ = 0;
            KetQuaDTO kq = ketQuaBLL.Get(deThi.MaDe, fDangNhap.nguoiDungDTO.MaNguoiDung);

            AddCauHoiDaLamFull(kq, ref d, ref s);


            GetDataFromRows(kq, ref DTNCDung, ref DTNCSai, ref diemDTNC, diemCuaMotCauDung);

            diem = d * diemCuaMotCauDung;
            d += DTNCDung;
            s += DTNCSai;
            diem += diemDTNC;
            if (kq == null)
            {
                maKQ = ketQuaBLL.GetAutoIncrement() == 0 ? 1 : ketQuaBLL.GetAutoIncrement();
                KetQuaDTO kqInsert = new KetQuaDTO
                {
                    MaKetQua = maKQ,
                    MaDe = deThi.MaDe,
                    MaNguoiDung = fDangNhap.nguoiDungDTO.MaNguoiDung,
                    Diem = Math.Round(Convert.ToDecimal(diem), 2),
                    SoCauDung = d,
                    SoCauSai = s,
                    is_delete = 0,
                    TrangThai = 1
                };

                if (ketQuaBLL.Add(kqInsert))
                {
                    foreach (var item in cauHoiDaLamDTOs)
                    {
                        ChiTietDeDaLamDTO ctddl = new ChiTietDeDaLamDTO
                        {
                            MaCauHoi = item.MaCauHoiDaLam,
                            MaDe = deThi.MaDe,
                            MaKetQua = maKQ
                        };
                        chiTietDeDaLamBLL.AddChiTietDeDaLam(ctddl);
                    }
                    MessageBox.Show("Nộp bài thành công");
                    fKetQua f = new fKetQua(deThi, lop, kqInsert);
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Nộp bài thất bại");

                }

            }
            else
            {
                KetQuaDTO kqUpdate = new KetQuaDTO
                {
                    MaKetQua = kq.MaKetQua,
                    MaDe = deThi.MaDe,
                    MaNguoiDung = fDangNhap.nguoiDungDTO.MaNguoiDung,
                    Diem = Math.Round(Convert.ToDecimal(diem), 2),
                    SoCauDung = d,
                    SoCauSai = s,
                    is_delete = 0,
                    TrangThai = 1
                };
                if (ketQuaBLL.Update(kqUpdate))
                {
                    foreach (var item in cauHoiDaLamDTOs)
                    {
                        ChiTietDeDaLamDTO ctddl = new ChiTietDeDaLamDTO
                        {
                            MaCauHoi = item.MaCauHoiDaLam,
                            MaDe = deThi.MaDe,
                            MaKetQua = kq.MaKetQua
                        };
                        chiTietDeDaLamBLL.UpdateChiTietDeDaLam(ctddl);
                    }
                    MessageBox.Show("Nộp bài thành công");
                    fKetQua f = new fKetQua(deThi, lop, kqUpdate);
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Nộp bài thất bại");
                }
                
            }
            this.Dispose();
            fChiTietLop.Dispose();
        }
        public void AddCauHoiDaLamFull(KetQuaDTO kq,ref int d,ref int s)
        {
            for (int i = 0; i < so_cau_hoi; i++)
            {
                int selectedIndex = -1; // Khởi tạo chỉ số RadioButton được chọn
                int maKQ = -1;
                CauHoiDaLamDTO cauHoiDaLamDTO = new CauHoiDaLamDTO
                {

                    MaCauHoiDaLam = cauHoiDaLamBLL.getAutoIncrement(),
                    NoiDung = dsCauHoi[i].NoiDung,
                    IdNguoiTao = dsCauHoi[i].MaNguoiTao,
                    MaMonHoc = dsCauHoi[i].MaMonHoc,
                    DoKho = dsCauHoi[i].DoKho,
                    LoaiCauHoi = dsCauHoi[i].LoaiCauHoi,

                };
                List<CauTraLoiDTO> listCTLDto = cauTraLoiBLL.getByMaCauHoi(dsCauHoi[i].MaCauHoi);
                if (kq == null)
                {
                    if (!cauHoiDaLamBLL.Add(cauHoiDaLamDTO))
                    {
                        MessageBox.Show("Lỗi Add CauHoiDaLam");
                        return;
                    }
                    cauHoiDaLamDTOs.Add(cauHoiDaLamDTO);
                }
                else
                {
                    cauHoiDaLamDTO = chiTietDeDaLamBLL.GetAllCauHoiDaLamOfDeThi(deThi).FirstOrDefault(x => x.NoiDung == dsCauHoi[i].NoiDung &&
                                                                                                                        x.IdNguoiTao == dsCauHoi[i].MaNguoiTao &&
                                                                                                                        x.MaMonHoc == dsCauHoi[i].MaMonHoc);
                    if(cauHoiDaLamBLL.Update(cauHoiDaLamDTO))
                    {
                        MessageBox.Show("Lỗi Update CauHoiDaLam");
                        return;
                    }
                    cauHoiDaLamDTOs.Add(cauHoiDaLamDTO);

                }
                //Add Câu hỏi đã làm xong
                if (GetTagValue(groupBox[i],ref selectedIndex))
                {
                    if (dsCauHoi[i].LoaiCauHoi == "Trắc nghiệm")
                        d++;
                    
                }
                else
                {
                    if (dsCauHoi[i].LoaiCauHoi == "Trắc nghiệm")
                        s++;
                }
                //Bắt đầu add câu trả lời đã làm
                if (kq == null)
                {
                    foreach (var item in listCTLDto)
                    {
                        CauTraLoiDaLamDTO ctldl = new CauTraLoiDaLamDTO
                        {
                            MaCauHoiDaLam = cauHoiDaLamBLL.getAutoIncrement()-1,
                            NoiDung = dsCauHoi[i].NoiDung,
                            IsDapAn = item.IsDapAn,
                            IsChon = listCTLDto.IndexOf(item) == selectedIndex ? 1 : 0
                        };
                        if (!cauTraLoiDaLamBLL.AddCauTraLoi(ctldl))
                        {
                            MessageBox.Show("Lỗi Add CauTraLoiDaLam");
                            return;
                        }
                    }
                }
                else
                {
                    List<CauTraLoiDaLamDTO> dsCauTLDaLam = cauTraLoiDaLamBLL.GetCauTraLoiDaLamOfDeThi(deThi.MaDe).Where(x => x.MaCauHoiDaLam == cauHoiDaLamDTO.MaCauHoiDaLam).ToList();
                    foreach (var item in dsCauTLDaLam)
                    {
                        CauTraLoiDaLamDTO ctldl = new CauTraLoiDaLamDTO
                        {
                            MaCauTraLoiDaLam = item.MaCauTraLoiDaLam,
                            MaCauHoiDaLam = cauHoiDaLamDTO.MaCauHoiDaLam,
                            NoiDung = dsCauHoi[i].NoiDung,
                            IsDapAn = item.IsDapAn,
                            IsChon =  dsCauTLDaLam.IndexOf(item) == selectedIndex ? 1 : 0
                        };
                        if (!cauTraLoiDaLamBLL.AddCauTraLoi(ctldl))
                        {
                            MessageBox.Show("Lỗi Add CauTraLoiDaLam");
                            return;
                        }
                    }


                }
                
            }
        }
        public void CreatePanelDienTu(string CauHoiName,int step, int sodapandien)
        {
            CreateRow(CauHoiName, 3, 3+ step,sodapandien);  // Tạo hàng đầu tiên
            //CreateRow("Điền từ", 3, 58); // Tạo hàng thứ hai +55
        }
        private void CreateRow(string title, int x, int y, int sodapandien)
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
            {
                ColumnCount = 6,
                Location = new System.Drawing.Point(x, y),
                Size = new System.Drawing.Size(961, 49),
                //Dock = DockStyle.Top
                AutoSize = true, // Tự động điều chỉnh kích thước theo nội dung
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            Label mainLabel = new Label
            {
                Text = title,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Segoe UI", 9.75F)
            };

            tableLayoutPanel.Controls.Add(mainLabel, 0, 0);
            for (int i = 1; i <= sodapandien; i++)
            {
                Panel panel = new Panel {
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink
                };
                Label label = new Label { Text = $"({i})", Location = new System.Drawing.Point(3, 9), AutoSize = true, };
                TextBox textBox = new TextBox { 
                    Size = new System.Drawing.Size(135, 29), 
                    Location = new System.Drawing.Point(30, 5), 
                    TextAlign = HorizontalAlignment.Center 
                };

                panel.Controls.Add(label);
                panel.Controls.Add(textBox);
                tableLayoutPanel.Controls.Add(panel, i, 0);
            }

            this.flowLayoutPanel2.Controls.Add(tableLayoutPanel); // Thêm vào form hiện tại
        }
        private Dictionary<int, List<string>> GetTextBoxValuesFromTableLayoutPanel(TableLayoutPanel tableLayoutPanel)
        {
            int key = -1;
            List<string> values = new List<string>();
            foreach (Control control in tableLayoutPanel.Controls)
            {
                if (control is Panel panel) // Nếu là Panel, duyệt tiếp các Control bên trong Panel
                {
                    foreach (Control panelControl in panel.Controls)
                    {
                        if (panelControl is TextBox textBox) // Nếu là TextBox, lấy giá trị
                        {
                            values.Add(textBox.Text);
                        }
                    }
                    
                    listAns[key] = values;
                }
                if (control is Label labelCau)
                {
                    key = -1;
                    values = new List<string>();
                    if (labelCau.Text.ToLower().Contains("câu"))
                    {
                        key = ExtractNumberFromLabelText(labelCau.Text.Trim());
                        listAns.Add(key, new List<string>());
                    }
                }

            }

            return listAns;
        }
        private void GetDataFromRows(KetQuaDTO kq,ref int d,ref int s,ref double diem,double diemcuamotcau)
        {
            Dictionary<int, List<string>> listTextBox = new Dictionary<int, List<string>>();
            foreach (Control control in this.flowLayoutPanel2.Controls)
            {
                if (control is TableLayoutPanel tableLayoutPanel)
                {
                    // Lấy dữ liệu từ TableLayoutPanel
                    Dictionary<int, List<string>> textBoxValues = GetTextBoxValuesFromTableLayoutPanel(tableLayoutPanel);

                    // Xử lý dữ liệu lấy được
                    foreach (var kvp in textBoxValues)
                    {
                        int key = kvp.Key; // Key là một số nguyên
                        List<string> values = kvp.Value; // Value là danh sách các chuỗi

                        Console.WriteLine($"Key: {key}");
                        foreach (string value in values)
                        {
                            Console.WriteLine($" - Value: {value}"); // In từng giá trị trong danh sách
                        } // In giá trị ra hoặc xử lý theo nhu cầu
                    }
                    listTextBox = textBoxValues;
                }
            }
            for (int i = 0; i < listTextBox.Count; i++)
            {
                int key = listTextBox.ElementAt(i).Key;
                List<string> values = listTextBox.ElementAt(i).Value;
                List<bool> listCheckDT = new List<bool>();
                List<bool> listCheckNC = new List<bool>();
                CauHoiDTO cauHoiTemp = dsCauHoi[key - 1];
                int temp = 0;
                if (cauHoiTemp.LoaiCauHoi.Contains("Điền từ"))
                {
                    int flagCount = 0;
                    CauHoiDaLamDTO cauhoiSoSanh = cauHoiDaLamDTOs.FirstOrDefault(c => c.NoiDung == cauHoiTemp.NoiDung &&
                                                                                       c.MaMonHoc == cauHoiTemp.MaMonHoc &&
                                                                                       c.IdNguoiTao == cauHoiTemp.MaNguoiTao);
                    for (int ans = 0; ans < values.Count; ans++)
                    {
                        double diemDT = 1.0 * diemcuamotcau / values.Count;
                        Boolean isDapAn = false;
                        CauTraLoiDienChoTrongDTO ctl = cauTraLoiDienChoTrongBLL.GetCauTraLoiByMaCauHoiAndViTri(cauHoiTemp.MaCauHoi, ans + 1);
                        CauTraLoiDienChoTrongDaLamDTO cauTLDTT = new CauTraLoiDienChoTrongDaLamDTO
                        {
                            MaCauHoi = cauHoiTemp.MaCauHoi,
                            ViTri = ans + 1,
                            CauTraLoiText = values[ans],
                            DapAnText = ctl.DapAnText,
                            IsDelete = 0
                        };
                        isDapAn = !string.IsNullOrEmpty(values[ans]) && ctl.DapAnText.ToLower().Equals(values[ans].ToLower());

                        if (isDapAn)
                        {
                            diem += diemDT;
                            flagCount++;
                        }
                        if (kq == null)
                        {
                            cauTraLoiDienChoTrongDaLamBLL.Add(cauTLDTT);

                        }
                        else
                        {
                            cauTraLoiDienChoTrongDaLamBLL.Update(cauTLDTT);
                        }
                        //MessageBox.Show($"MaCauHoi: {cauTLDTT.MaCauHoi} Vt: {cauTLDTT.ViTri} CTL: {cauTLDTT.CauTraLoiText} DA: {cauTLDTT.DapAnText}");
                        //cauTraLoiDienChoTrongDaLamBLL.Add(cauTLDTT);
                    }
                    if (flagCount == values.Count)
                    {
                        d++;
                    }
                    else
                    {
                        s++;
                    }

                }
                else if (cauHoiTemp.LoaiCauHoi.Contains("Nối câu"))
                {
                    int flagCount = 0;
                    CauHoiDaLamDTO cauhoiSoSanh = cauHoiDaLamDTOs.FirstOrDefault(c => c.NoiDung == cauHoiTemp.NoiDung &&
                                                                                        c.MaMonHoc == cauHoiTemp.MaMonHoc &&
                                                                                        c.IdNguoiTao == cauHoiTemp.MaNguoiTao);

                    for (int ans = 0; ans < values.Count; ans++)
                    {
                        double diemNC = 1.0 * diemcuamotcau / values.Count;

                        Boolean isDapAn = false;
                        //List<NoiCauDaLamDTO> listNCDL =  noiCauDaLamBLL.GetAllNoiCauDaLam().Where(x => x.MaCauHoi == cauHoiTemp.MaCauHoi).ToList();
                        //List<NoiCauTraLoiDaLamDTO> listNCTLDL = noiCauTraLoiDaLamBLL.GetAllNoiCauTraLoiDaLam().Where(x => x.MaCauNoi == listNCDL[ans].MaNoiCauDaLam).ToList();
                        List<NoiCauDTO> listNC = noiCauBLL.GetAll(cauHoiTemp.MaCauHoi);
                        List<NoiCauTraLoiDTO> listNCTLDL = noiCauTraLoiBLL.GetAll(cauHoiTemp.MaCauHoi);

                        NoiCauDaLamDTO noicauInsert = new NoiCauDaLamDTO
                        {
                            MaCauHoi = cauhoiSoSanh.MaCauHoiDaLam,
                            NoiDung = listNC[ans].NoiDung

                        };
                        noiCauDaLamBLL.AddNoiCauDaLam(noicauInsert);
                        noicauInsert.MaNoiCauDaLam = noiCauDaLamBLL.GetAutoIncrement() > 1 ? noiCauDaLamBLL.GetAutoIncrement() - 1 : 1;
                        NoiCauTraLoiDaLamDTO ncautraloi = new NoiCauTraLoiDaLamDTO
                        {
                            MaCauTLDaLam = 0, // Assign appropriate value
                            MaCauNoi = noicauInsert.MaNoiCauDaLam,
                            NoiDung = listNC[ans].NoiDung,
                            DapAnNoi = listNCTLDL[ans].NoiDung,
                            DapAnChon = values[ans],
                        };
                        string valueOfColB = noiCauNguoiDung.TryGetValue(ncautraloi.DapAnChon, out var value) ? value : null;

                        isDapAn = valueOfColB != null && ncautraloi.DapAnNoi != null
                                    ? ncautraloi.DapAnNoi.ToLower().Equals(valueOfColB.ToLower()) : false;
                        listCheckNC.Add(isDapAn);

                        if (isDapAn)
                        {
                            diem += diemNC;
                            flagCount++;
                        }

                        if (kq == null)
                        {
                            noiCauTraLoiDaLamBLL.AddNoiCauTraLoiDaLam(ncautraloi);

                        }
                        else
                        {
                            noiCauTraLoiDaLamBLL.UpdateNoiCauTraLoiDaLam(ncautraloi);
                        }
                        //MessageBox.Show($"{ncautraloi.MaCauNoi}, NoiDung: {ncautraloi.NoiDung}, DapAnNoi: {ncautraloi.DapAnNoi}, DapAnChon: {ncautraloi.DapAnChon}.{valueOfColB}, check : {isDapAn}");
                    }
                    if (flagCount == values.Count)
                    {
                        d++;
                    }
                    else
                    {
                        s++;
                    }

                }
            }

        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = "";
            // Chuyển ký tự nhập vào thành chữ hoa
            e.KeyChar = char.ToUpper(e.KeyChar);

            // Chỉ cho phép nhập các ký tự A, B, C, D, E, F và phím xóa (Backspace)
            if (!"ABCDEFGHIJ\b".Contains(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho ký tự không hợp lệ xuất hiện trong textbox
            }
        }


        private TableLayoutPanel TaoPanelNoiCau(string title, int soLuongDapAn)
        {
            // Tạo TableLayoutPanel với 6 cột
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
            {
                ColumnCount = soLuongDapAn + 1, // 1 cột cho tiêu đề, các cột còn lại cho đáp án
                Location = new System.Drawing.Point(0, 0),
                Size = new System.Drawing.Size(961, 49),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            // Tạo Label tiêu đề
            Label mainLabel = new Label
            {
                Text = title,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Segoe UI", 9.75F)
            };
            tableLayoutPanel.Controls.Add(mainLabel, 0, 0); // Tiêu đề vào cột 0

            // Thêm các Panel với Label và TextBox vào các cột tiếp theo
            for (int i = 1; i <= soLuongDapAn; i++)
            {
                // Tạo Panel chứa Label và TextBox
                Panel panel = new Panel
                {
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink
                };

                // Tạo Label (ví dụ: (1), (2), ...)
                Label label = new Label
                {
                    Text = $"{i} -> ",
                    Location = new System.Drawing.Point(3, 9),
                    AutoSize = true
                };

                // Tạo TextBox cho đáp án
                TextBox textBox = new TextBox
                {
                    Size = new System.Drawing.Size(135, 29),
                    Location = new System.Drawing.Point(30, 5),
                    TextAlign = HorizontalAlignment.Center
                };
                 textBox.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
                // Thêm Label và TextBox vào Panel
                panel.Controls.Add(label);
                panel.Controls.Add(textBox);

                // Thêm Panel vào TableLayoutPanel tại cột i
                tableLayoutPanel.Controls.Add(panel, i, 0);
            }

            return tableLayoutPanel;
        }

        private void TaoDapAnNoi(string CauHoiName, int sodapan)
        {
            // Thêm bảng nội dung với 5 đáp án
            TableLayoutPanel a = TaoPanelNoiCau("Câu " + CauHoiName, sodapan);
            this.flowLayoutPanel2.Controls.Add(a);
        }
        private void TaoDapAn(GroupBox g, int ma_cau_hoi)
        {
            CauHoiDTO cauHoi = cauHoiBLL.GetCauHoiById(ma_cau_hoi);
            List<CauTraLoiDTO> cauTraLoiList = cauTraLoiBLL.getByMaCauHoi(ma_cau_hoi);
            int so_dap_an = cauTraLoiList.Count;
            
            if(so_dap_an != 0 && cauHoi.LoaiCauHoi.Contains("Trắc nghiệm"))
            {
                RadioButton[] rd = new RadioButton[so_dap_an];
                for (int i = 1; i <= so_dap_an; i++)
                {
                    rd[i - 1] = new RadioButton();
                    rd[i - 1].AutoSize = true;
                    rd[i - 1].Name = "radioButton_" + i + g.Text;
                    rd[i - 1].Font = new Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    switch (i)
                    {
                        case 1: rd[i - 1].Location = new Point(15, 58); if (cauTraLoiList[i - 1].IsDapAn == 1) { rd[i - 1].Tag = "true"; } else { rd[i - 1].Tag = "false"; } break;
                        case 2: rd[i - 1].Location = new Point(15, 123); if (cauTraLoiList[i - 1].IsDapAn == 1) { rd[i - 1].Tag = "true"; } else { rd[i - 1].Tag = "false"; } break;
                        case 3: rd[i - 1].Location = new Point(15, 183); if (cauTraLoiList[i - 1].IsDapAn == 1) { rd[i - 1].Tag = "true"; } else { rd[i - 1].Tag = "false"; } break;
                        case 4: rd[i - 1].Location = new Point(15, 243); if (cauTraLoiList[i - 1].IsDapAn == 1) { rd[i - 1].Tag = "true"; } else { rd[i - 1].Tag = "false"; } break;

                    }
                    rd[i - 1].Size = new Size(14, 13);
                    rd[i - 1].TabIndex = 1;
                    rd[i - 1].TabStop = false;
                    rd[i - 1].UseVisualStyleBackColor = true;

                    g.Controls.Add(rd[i - 1]);
                }
            } else if(cauHoi.LoaiCauHoi.Contains("Điền từ"))
            {
                //TaoDapAnDien(cauHoi);
            }
            else
            {
                //MessageBox.Show("Noi tu");
            }


        }
        private int ExtractNumberFromLabelText(string labelText)
        {
            // Giả sử Label.Text có dạng "|| Cau 7"
            string[] parts = labelText.Split(' '); // Tách chuỗi theo khoảng trắng
            if (parts.Length > 0 && int.TryParse(parts[1], out int number))
            {
                return number; // Trả về số đã chuyển đổi
            }

            return -1; // Trả về -1 nếu không lấy được số
        }
        private void TaoCauHoi(List<CauHoiDTO> list)
        {
            groupBox = new GroupBox[list.Count];
            for (int i = 1; i <= list.Count; i++)
            {

                List<CauTraLoiDTO> cauTraLoiList = cauTraLoiBLL.getByMaCauHoi(list[i - 1].MaCauHoi);
                groupBox[i - 1] = new GroupBox();
                groupBox[i - 1].Name = "groupBox" + i;
                groupBox[i - 1].Location = new Point(5, 5);
                groupBox[i - 1].Margin = new Padding(0, 10, 5, 0);
                groupBox[i - 1].Size = new Size(50, 290);
                groupBox[i - 1].TabIndex = 0;
                groupBox[i - 1].TabStop = false;
                groupBox[i - 1].Text = "" + i;
                groupBox[i - 1].MouseUp += GroupBox_MouseUp;
                groupBox[i - 1].Font = new Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                if(list[i - 1].LoaiCauHoi.Contains("Điền từ")){
                    List<CauTraLoiDienChoTrongDTO> dataDT = cauTraLoiDienChoTrongBLL.GetAll(list[i - 1].MaCauHoi);
                    int step = 0;
                    CreatePanelDienTu("Câu "+i,step, dataDT.Count);
                    step += 55;
                } else if (list[i - 1].LoaiCauHoi.Contains("Nối câu"))
                {
                    List<NoiCauDTO> dataNC =  noiCauBLL.GetAllByMaCauHoi(list[i - 1].MaCauHoi);
                    TaoDapAnNoi(i.ToString(),dataNC.Count);
                }

                TaoDapAn(groupBox[i - 1], list[i - 1].MaCauHoi);

                flowLayoutPanel1.Controls.Add(groupBox[i - 1]);
            }
        }
        private void GroupBox_MouseUp(object sender, MouseEventArgs e)
        {
            GroupBox groupBox = sender as GroupBox;
            string text = groupBox.Text;
            int index = Convert.ToInt32(text);

            currentIndex = index - 1;
            panel1.Controls.Clear();
            panel1.Controls.Add(slide[currentIndex]);
        }
        public void UpdateLabelSilde(int current)
        {
            label1.Text = current.ToString() + "/" + so_cau_hoi.ToString();
            
        }
        private void next_slide(int n)
        {
            panel1.Controls.Clear();
            currentIndex++;
            if (currentIndex >= n)
            {
                currentIndex = 0;
            }
            UpdateLabelSilde(currentIndex);
            panel1.Controls.Add(slide[currentIndex]);
        }
        private void tao_slide(List<CauHoiDTO> list)
        {

            slide = new Panel[list.Count];
            for (int i = 1; i <= list.Count; i++)
            {
                List<CauTraLoiDTO> cauTraLoiList = cauTraLoiBLL.getByMaCauHoi(list[i - 1].MaCauHoi);
                slide[i - 1] = new Panel();
                slide[i - 1].Name = "slide" + i;
                slide[i - 1].Size = panel1.Size;
                //slide[i - 1].BackColor = Color.BurlyWood;
                string cauhoi = "Câu " + i + ": " + list[i - 1].NoiDung;
                string cautraloi = "";
                if (list[i - 1].LoaiCauHoi.Contains("Nối câu"))
                {
                    List<NoiCauDTO> listNC =   noiCauBLL.GetAllByMaCauHoi(list[i - 1].MaCauHoi);
                    List<NoiCauTraLoiDTO> listNCTL = noiCauTraLoiBLL.GetAll(list[i - 1].MaCauHoi);
                    // Tạo Panel chứa Label và RichTextBox
                    Panel panel = new Panel
                    {
                        Dock = DockStyle.Top, // Panel sẽ chiếm không gian ở trên
                        Height = 160 // Tăng chiều cao của Panel để đủ chứa Label và RichTextBox
                    };
                    string NoiDungCotA = "";
                    string NoiDungCotB = "";
                    for (int m =0;m < listNC.Count;m++)
                    {
                        NoiDungCotA += m+1 +"."+ listNC[m].NoiDung + "\n";
                    }
                    for(int m = 0; m < listNCTL.Count; m++)
                    {
                        char letter = (char)('A' + m); // Convert m to the corresponding letter starting from 'A'

                        NoiDungCotB += letter + "." + listNCTL[m].NoiDung + "\n";
                        noiCauNguoiDung.Add(letter.ToString(), listNCTL[m].NoiDung);
                    }
                    // Tạo Label cho tiêu đề chính (Câu 7: Nối các từ lại với nhau)
                    Label lblTitle = new Label
                    {
                        Text = "Câu 7: Nối các từ lại với nhau",
                        AutoSize = false,
                        Height = 40, // Cố định chiều cao cho Label
                        Dock = DockStyle.Top,
                        Font = new Font("Arial", 12, FontStyle.Bold),
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    // Tạo TableLayoutPanel để chia thành 2 cột
                    TableLayoutPanel tableLayout = new TableLayoutPanel
                    {
                        RowCount = 2, // 2 hàng: 1 hàng cho tiêu đề cột, 1 hàng cho RichTextBox
                        ColumnCount = 2, // 2 cột
                        Dock = DockStyle.Fill, // Chiếm toàn bộ không gian còn lại
                        AutoSize = true
                    };

                    // Cấu hình các cột (tự động co giãn đều)
                    tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50)); // Cột 1 chiếm 50%
                    tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50)); // Cột 2 chiếm 50%

                    // Tạo Label cho tiêu đề cột
                    Label lblColumnA = new Label
                    {
                        Text = "Cột A",
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Arial", 10, FontStyle.Bold),
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    Label lblColumnB = new Label
                    {
                        Text = "Cột B",
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Arial", 10, FontStyle.Bold),
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    // Tạo RichTextBox cho cột đầu tiên (Câu hỏi)
                    RichTextBox rtbColumn1 = new RichTextBox
                    {
                        Text = string.IsNullOrEmpty(NoiDungCotA) ? "Không có dữ liệu" : NoiDungCotA,
                        Dock = DockStyle.Fill,
                        ReadOnly = true,
                        BorderStyle = BorderStyle.FixedSingle,
                        WordWrap = true
                    };

                    // Tạo RichTextBox cho cột thứ hai (Đáp án)
                    RichTextBox rtbColumn2 = new RichTextBox
                    {
                        Text = string.IsNullOrEmpty(NoiDungCotB) ? "Không có dữ liệu" : NoiDungCotB,
                        Dock = DockStyle.Fill,
                        ReadOnly = true,
                        BorderStyle = BorderStyle.FixedSingle,
                        WordWrap = true
                    };

                    // Thêm các Label tiêu đề cột vào TableLayoutPanel (hàng 1)
                    tableLayout.Controls.Add(lblColumnA, 0, 0); // Cột A
                    tableLayout.Controls.Add(lblColumnB, 1, 0); // Cột B

                    // Thêm các RichTextBox vào TableLayoutPanel (hàng 2)
                    tableLayout.Controls.Add(rtbColumn1, 0, 1); // Cột A - RichTextBox
                    tableLayout.Controls.Add(rtbColumn2, 1, 1); // Cột B - RichTextBox

                    // Thêm TableLayoutPanel và Label vào Panel
                    panel.Controls.Add(tableLayout);
                    panel.Controls.Add(lblTitle);

                    // Thêm Panel vào Slide hoặc Form
                    slide[i - 1].Controls.Add(panel);




                }
                else
                {
                    RichTextBox richTextBox1 = new RichTextBox();

                    richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
                    richTextBox1.Location = new System.Drawing.Point(0, 0);
                    richTextBox1.Name = "richTextBox" + i;
                    richTextBox1.Size = new System.Drawing.Size(725, 273);
                    richTextBox1.TabIndex = 0;
                    richTextBox1.Font = new Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    for (int j = 0; j < cauTraLoiList.Count; j++)
                    {
                        cautraloi += (j + 1) + ". " + cauTraLoiList[j].NoiDung + "\n";
                    }

                    richTextBox1.Enabled = true;
                    richTextBox1.ReadOnly = true;
                    richTextBox1.Text = cauhoi + "\n\n" + cautraloi;
                    slide[i - 1].Controls.Add(richTextBox1);
                }
                panel1.Controls.Add(slide[i - 1]);
            }

        }
        private bool GetTagValue(GroupBox grp, ref int dem)
        {
            bool isAnswer = false; // Biến xác định đáp án đúng

            if (grp != null)
            {
                try
                {
                    int index = 0; // Biến đếm để xác định vị trí của RadioButton

                    foreach (Control ctl in grp.Controls) // Duyệt qua tất cả các control trong GroupBox
                    {
                        if (ctl is RadioButton rbtn) // Kiểm tra nếu control là RadioButton
                        {
                            if (rbtn.Checked)
                            {
                                dem = index; // Ghi nhận chỉ số của RadioButton được chọn

                                if (rbtn.Tag != null && rbtn.Tag.ToString() == "true") // Kiểm tra Tag
                                {
                                    isAnswer = true; // Đáp án đúng
                                }
                                break; // Thoát vòng lặp khi tìm thấy RadioButton được chọn
                            }
                            index++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return isAnswer; // Trả về kết quả kiểm tra đáp án
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Xác nhận nộp bài", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                NopBai();
            }

        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            next_slide(so_cau_hoi);
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            prev_slide(so_cau_hoi);
        }
        private void prev_slide(int n)
        {
            panel1.Controls.Clear();
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = n - 1;
            }
            UpdateLabelSilde(currentIndex);
            panel1.Controls.Add(slide[currentIndex]);
        }
        private void Baithi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flag == -1)
            {
                DialogResult rs = MessageBox.Show("Thoát đồng nghĩa với nộp bài. Bạn có muốn thoát", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (rs == DialogResult.OK)
                {
                    NopBai();
                }
                if (rs == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }

        }
        private void Baithi_Load(object sender, EventArgs e)
        {

        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            
        }

        private void numericUpDownSearch_ValueChanged(object sender, EventArgs e)
        {
            int selectedSilde = (int)numericUpDownSearch.Value;
            if(selectedSilde > 0 && selectedSilde <= so_cau_hoi)
            {
                panel1.Controls.Clear();
                currentIndex = selectedSilde - 1;
                UpdateLabelSilde(currentIndex);
                panel1.Controls.Add(slide[currentIndex]);
            } else
            {
                MessageBox.Show("Không tìm thấy câu hỏi");
                return;
            }
        }
    }
}
