using BLL;
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

        private GroupBox[] groupBox;
        private Panel[] slide;

        private int currentIndex = 0;
        private int soCauChuaChon = 0;
        private Timer countdownTimer;
        private int remainingTimeInSeconds; // Số giây còn lại

        private int so_cau_hoi;
        private int flag = -1; // dat co dong form
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

            List<CauHoiDTO> dsCauHoi = chiTietDeBLL.GetAllCauHoiOfDeThi(deThi);
            so_cau_hoi = dsCauHoi.Count;

            Random random = new Random();
            dsCauHoi.Sort((x, y) => random.Next(-1, 2));
            
            InitializeComponent();

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
            pictureBox1.ImageLocation = fDangNhap.nguoiDungDTO.Avatar;

            // Khởi tạo đối tượng Timer và cấu hình nó
            countdownTimer = new System.Windows.Forms.Timer();
            countdownTimer.Interval = 1000; // Mỗi lần đếm là 1 giây (1000 ms)
            countdownTimer.Tick += new EventHandler(CountdownTimer_Tick);
            countdownTimer.Start();

            // Đặt thời gian ban đầu là 15 phút (900 giây)
            remainingTimeInSeconds =(int)((deThi.ThoiGianKetThuc - deThi.ThoiGianBatDau).TotalSeconds);
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
                KetQuaDTO kqInsert = new KetQuaDTO(-1,deThi.MaDe, fDangNhap.nguoiDungDTO.MaNguoiDung, Convert.ToDecimal(diem), d, s - soCauChuaChon,1,0);
                ketQuaBLL.Add(kqInsert);
                fKetQua f = new fKetQua(deThi, lop, kqInsert);
                f.ShowDialog();
            }
            else
            {
                KetQuaDTO kqUpdate = new KetQuaDTO(kq.MaKetQua, deThi.MaDe, fDangNhap.nguoiDungDTO.MaNguoiDung, Convert.ToDecimal(diem), d, s - soCauChuaChon, 1, 0);
                ketQuaBLL.Update(kqUpdate);
                fKetQua f = new fKetQua(deThi, lop, kqUpdate);
                f.ShowDialog();
            }
            this.Dispose();
            fChiTietLop.Dispose();
        }
        private void TaoDapAn(GroupBox g, int ma_cau_hoi)
        {
            List<CauTraLoiDTO> cauTraLoiList = cauTraLoiBLL.getByMaCauHoi(ma_cau_hoi);
            int so_dap_an = cauTraLoiList.Count;
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
        private void next_slide(int n)
        {
            panel1.Controls.Clear();
            currentIndex++;
            if (currentIndex >= n)
            {
                currentIndex = 0;
            }
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
                slide[i - 1].BackColor = Color.BurlyWood;
                string cauhoi = "Câu " + i + ": " + list[i - 1].NoiDung;
                string cautraloi = "";

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
                    ketQuaBLL.Add(kqInsert);
                    fKetQua f = new fKetQua(deThi, lop, kqInsert);
                    f.ShowDialog();
                }
                else
                {
                    KetQuaDTO kqUpdate = new KetQuaDTO(kq.MaKetQua, deThi.MaDe, fDangNhap.nguoiDungDTO.MaNguoiDung, Convert.ToDecimal(diem), d, s - soCauChuaChon, 1, 0);
                    ketQuaBLL.Update(kqUpdate);
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

    }
}
