using BLL;
using DAL;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Color = System.Drawing.Color;
using Control = System.Windows.Forms.Control;
using Font = System.Drawing.Font;

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
        private CauTraLoiBLL cauTraLoiBLL;
        private CauTraLoiDienChoTrongBLL cauTraLoiDienChoTrongBLL;
        NoiCauBLL noiCauBLL;
        NoiCauTraLoiBLL noiCauTraLoiBLL;

        private GroupBox[] groupBox;
        private Panel[] slide;

        private int currentIndex = 0;
        private int soCauChuaChon = 0;
        private Timer countdownTimer;
        private int remainingTimeInSeconds; // Số giây còn lại
                                                                                                                                                                                                                                                                                                                                                                           
        List<CauTraLoiDTO> cauTraLoiDTOs = new List<CauTraLoiDTO>();
        List<CauTraLoiDienChoTrongDTO> cauTraLoiDienChoTrongDTOs = new List<CauTraLoiDienChoTrongDTO>();
        List<NoiCauDTO> noiCauDTOs = new List<NoiCauDTO>();
        List<NoiCauTraLoiDTO> noiCauTraLoiDTOs = new List<NoiCauTraLoiDTO>();
        private int so_cau_hoi;
        private int flag = -1; // dat co dong form
        private int counter = 1;

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
            cauTraLoiBLL = new CauTraLoiBLL();
            cauTraLoiDienChoTrongBLL = new CauTraLoiDienChoTrongBLL();
            noiCauTraLoiBLL = new NoiCauTraLoiBLL();
            noiCauBLL = new NoiCauBLL();

            List<CauHoiDTO> dsCauHoi = chiTietDeBLL.GetAllCauHoiOfDeThi(deThi);
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
            for (int i = 0; i < so_cau_hoi; i++)
            {
                if (GetTagValue(groupBox[i]))
                {
                    d++;
                }
                else
                {
                    s++;
                }
            }
            double diemCuaMotCauDung = (10.0f / so_cau_hoi);
            double diem = d * diemCuaMotCauDung;
            // check xem giáo viên đã làm bài chưa nếu có thì cập nhật lại kết quả
            KetQuaDTO kq = ketQuaBLL.Get(deThi.MaDe, fDangNhap.nguoiDungDTO.MaNguoiDung);
            if (kq == null)
            {
                KetQuaDTO kqInsert = new KetQuaDTO(deThi.MaDe, fDangNhap.nguoiDungDTO.MaNguoiDung, Convert.ToDecimal(diem), d, s - soCauChuaChon, 1, 0);
                //if (ketQuaBLL.Add(kqInsert))
                //{
                    MessageBox.Show("Nộp bài thành công");
                    fKetQua f = new fKetQua(deThi, lop, kqInsert);
                    f.ShowDialog();
                //}
                //else
                //{
                //    MessageBox.Show("Nộp bài thất bại");

                //}
            }
            else
            {
                
                KetQuaDTO kqUpdate = new KetQuaDTO(kq.MaKetQua, deThi.MaDe, fDangNhap.nguoiDungDTO.MaNguoiDung, Convert.ToDecimal(diem), d, s - soCauChuaChon, 1, 0);
                //if (ketQuaBLL.Update(kqUpdate))
                //{             
                    fKetQua f = new fKetQua(deThi, lop, kqUpdate);
                    f.ShowDialog();
                //}
            }
            this.Dispose();
            fChiTietLop.Dispose();
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
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            //if (int.TryParse(textBoxSearch.Text, out int rowIndex))
            //{
            //    // Kiểm tra hàng nhập có hợp lệ không
            //    if (rowIndex >= 1 && rowIndex <= flowLayoutPanel2.Controls.Count)
            //    {
            //        Control rowToScroll = flowLayoutPanel2.Controls[rowIndex - 1]; // Hàng bắt đầu từ 0
            //        flowLayoutPanel2.ScrollControlIntoView(rowToScroll); // Cuộn tới hàng được chọn
            //    }
            //    else
            //    {
            //        MessageBox.Show("Không có hàng tương ứng với số đã nhập.");
            //    }
            //}
        }
        private void LayDuLieuCacTextBox(TableLayoutPanel tableLayoutPanel)
        {
            // Duyệt qua tất cả các câu (mỗi câu là 1 hàng trong TableLayoutPanel)
            for (int row = 0; row < tableLayoutPanel.RowCount; row++)
            {
                // Lấy Title của câu (cột 0)
                Label titleLabel = tableLayoutPanel.GetControlFromPosition(0, row) as Label;
                if (titleLabel != null)
                {
                    string title = titleLabel.Text; // Tiêu đề câu
                    Console.WriteLine("Câu: " + title); // In ra câu (ví dụ: Câu 1, Câu 2, ...)

                    // Duyệt qua các cột chứa đáp án (từ cột 1 trở đi)
                    for (int col = 1; col < tableLayoutPanel.ColumnCount; col++)
                    {
                        Panel panel = tableLayoutPanel.GetControlFromPosition(col, row) as Panel;
                        if (panel != null)
                        {
                            // Tìm TextBox trong Panel
                            TextBox textBox = panel.Controls.OfType<TextBox>().FirstOrDefault();
                            if (textBox != null)
                            {
                                string dapAn = textBox.Text; // Dữ liệu từ TextBox
                                Console.WriteLine($"Đáp án của {title}: {dapAn}"); // In ra đáp án của câu
                            }
                        }
                    }
                }
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
                        case 1: rd[i - 1].Location = new Point(15, 63); if (cauTraLoiList[i - 1].IsDapAn == 1) { rd[i - 1].Tag = "true"; } else { rd[i - 1].Tag = "false"; } break;
                        case 2: rd[i - 1].Location = new Point(15, 125); if (cauTraLoiList[i - 1].IsDapAn == 1) { rd[i - 1].Tag = "true"; } else { rd[i - 1].Tag = "false"; } break;
                        case 3: rd[i - 1].Location = new Point(15, 185); if (cauTraLoiList[i - 1].IsDapAn == 1) { rd[i - 1].Tag = "true"; } else { rd[i - 1].Tag = "false"; } break;
                        case 4: rd[i - 1].Location = new Point(15, 245); if (cauTraLoiList[i - 1].IsDapAn == 1) { rd[i - 1].Tag = "true"; } else { rd[i - 1].Tag = "false"; } break;

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
                    CreatePanelDienTu("|| Câu "+i,step, dataDT.Count);
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
        private bool GetTagValue(GroupBox grp)
        {
            bool isAnswer = false;
            if (grp != null)
            {
                try
                {
                    bool anyRadioButtonChecked = false; // Biến này để kiểm tra xem có RadioButton nào được chọn hay không
                    foreach (Control ctl in grp.Controls) // Duyệt qua tất cả các control trong groupbox
                    {
                        if (ctl is RadioButton)
                        {
                            RadioButton rbtn = (RadioButton)ctl; // Ép kiểu control thành radiobutton
                            if (rbtn.Checked)
                            {
                                anyRadioButtonChecked = true;
                                if (rbtn.Tag.ToString() == "true")
                                {
                                    isAnswer = true;
                                    break;
                                }
                            }
                        }
                    }
                    // Kiểm tra nếu không có RadioButton nào được chọn, tăng giá trị của soCauChuaChon
                    if (!anyRadioButtonChecked)
                    {
                        soCauChuaChon++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            else
            {
                isAnswer = false;
            }
            return isAnswer;
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            flag = 1;
            int d = 0;
            int s = 0;
            for (int i = 0; i < so_cau_hoi; i++)
            {
                if (GetTagValue(groupBox[i]))
                {
                    d++;
                }
                else
                {
                    s++;
                }

            }
            DialogResult result = MessageBox.Show("Xác nhận nộp bài", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                double diemCuaMotCauDung = (10.0f / so_cau_hoi);
                double diem = Math.Round(d * diemCuaMotCauDung, 2);

                // check xem giáo viên đã làm bài chưa nếu có thì cập nhật lại kết quả
                KetQuaDTO kq = ketQuaBLL.Get(deThi.MaDe, fDangNhap.nguoiDungDTO.MaNguoiDung);
                if (kq == null)
                {
                    KetQuaDTO kqInsert = new KetQuaDTO(-1, deThi.MaDe, fDangNhap.nguoiDungDTO.MaNguoiDung, Convert.ToDecimal(diem), d, s - soCauChuaChon, 1, 0);
                    //ketQuaBLL.Add(kqInsert);
                    fKetQua f = new fKetQua(deThi, lop, kqInsert);
                    f.ShowDialog();
                }
                else
                {
                    KetQuaDTO kqUpdate = new KetQuaDTO(kq.MaKetQua, deThi.MaDe, fDangNhap.nguoiDungDTO.MaNguoiDung, Convert.ToDecimal(diem), d, s - soCauChuaChon, 1, 0);
                    //ketQuaBLL.Update(kqUpdate);
                    fKetQua f = new fKetQua(deThi, lop, kqUpdate);
                    f.ShowDialog();
                }

                this.Dispose();
                fChiTietLop.Dispose();
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
