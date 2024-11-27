using BLL;
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

namespace GUI
{
    public partial class fXemdapan : Form
    {
        private KetQuaDTO ketQua;
        private List<ChiTietDeDaLamDTO> chiTietDeDaLam;
        private ChiTietDeDaLamBLL ChiTietDeDaLamBLL = new ChiTietDeDaLamBLL();

        public fXemdapan(KetQuaDTO ketQua)
        {
            InitializeComponent();
            this.ketQua = ketQua;

            // Kiểm tra ketQua hợp lệ
            if (ketQua == null || ketQua.MaDe == 0 || ketQua.MaKetQua == 0)
            {
                MessageBox.Show("Thông tin kết quả không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Lấy dữ liệu
            chiTietDeDaLam = ChiTietDeDaLamBLL.GetChiTietDeDaLamByMaDeANDMaKetQua(ketQua.MaDe, ketQua.MaKetQua);

            // Kiểm tra dữ liệu trả về
            if (chiTietDeDaLam == null || chiTietDeDaLam.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu chi tiết để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Xử lý các loại câu hỏi
            HienThiCauHoi();
        }

        private void HienThiCauHoi()
        {
            CauHoiDaLamBLL cauHoiDaLamBLL = new CauHoiDaLamBLL();

            int i = 1;
            foreach (ChiTietDeDaLamDTO chiTiet in chiTietDeDaLam) // Sửa khai báo
            {
                // Lấy thông tin câu hỏi đã làm
                CauHoiDaLamDTO cauHoiDaLamDTO = cauHoiDaLamBLL.GetCauHoiDaLamByMaCauHoi(chiTiet.MaCauHoi);

                // Kiểm tra câu hỏi hợp lệ
                if (cauHoiDaLamDTO == null)
                {
                    MessageBox.Show($"Không tìm thấy thông tin cho câu hỏi có mã {chiTiet.MaCauHoi}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }

                // Phân loại và hiển thị câu hỏi
                switch (cauHoiDaLamDTO.LoaiCauHoi)
                {
                    case "Trắc nghiệm": // Trắc nghiệm
                        HienThiCauTracNghiem(cauHoiDaLamDTO,i);
                        break;

                    case "Điền từ": // Điền chỗ trống
                        HienThiCauDienChoTrong(cauHoiDaLamDTO,i);
                        break;

                    case "Nối câu": // Nối câu
                        HienThiCauNoiCau(cauHoiDaLamDTO,i);
                        break;

                    default: // Loại câu hỏi không xác định
                        MessageBox.Show($"Loại câu hỏi không xác định: {cauHoiDaLamDTO.LoaiCauHoi}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
                i++;
            }
        }

        private void HienThiCauTracNghiem(CauHoiDaLamDTO cauHoiDaLamDTO,int index)
        {
            // Tạo panel chứa toàn bộ câu hỏi
            Panel pnlCauHoi = new Panel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Padding = new Padding(10),
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Tạo TableLayoutPanel để quản lý layout dễ dàng hơn
            TableLayoutPanel tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                ColumnCount = 1,
                RowCount = 2
            };
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // Label cho câu hỏi
            Label lblCauHoi = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Text = $"Câu {index}: {cauHoiDaLamDTO.NoiDung}",
                Margin = new Padding(0, 0, 0, 10),
                Dock = DockStyle.Fill
            };

            // Panel chứa các đáp án
            FlowLayoutPanel pnlDapAn = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 5, 0, 0)
            };

            // Lấy danh sách đáp án
            CauTraLoiDaLamBLL dapAnBLL = new CauTraLoiDaLamBLL();
            List<CauTraLoiDaLamDTO> dsDapAn = dapAnBLL.GetCauTraLoiByMaCauHoi(cauHoiDaLamDTO.MaCauHoiDaLam);
            if (dsDapAn != null && dsDapAn.Count > 0)
            {
                foreach (CauTraLoiDaLamDTO dapAn in dsDapAn)
                {
                    Panel pnlAnswer = new Panel
                    {
                        AutoSize = true,
                        Margin = new Padding(0, 3, 0, 3)
                    };

                    RadioButton radDapAn = new RadioButton
                    {
                        AutoSize = true,
                        Text = "",  // Để trống text của RadioButton
                        Enabled = false,
                        Margin = new Padding(0, 3, 0, 3)
                    };

                    // Label để hiển thị text
                    Label lblDapAn = new Label
                    {
                        AutoSize = true,
                        Text = dapAn.NoiDung,
                        Font = new Font("Arial", 11),
                        Location = new Point(radDapAn.Width + 5, 3)  // Đặt vị trí ngay sau RadioButton
                    };

                    // Xác định màu sắc
                    if (dapAn.IsChon == 1)
                    {
                        radDapAn.Checked = true;
                        if (dapAn.IsChon == dapAn.IsDapAn)
                        {
                            lblDapAn.ForeColor = Color.FromArgb(34, 139, 34);
                        }
                        else
                        {
                            lblDapAn.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        if (dapAn.IsDapAn == 1)
                        {
                            lblDapAn.ForeColor = Color.FromArgb(34, 139, 34);
                        }
                    }

                    // Thêm controls vào panel
                    pnlAnswer.Controls.Add(radDapAn);
                    pnlAnswer.Controls.Add(lblDapAn);
                    pnlDapAn.Controls.Add(pnlAnswer);
                }
            }


            // Thêm các controls vào TableLayoutPanel
            tableLayout.Controls.Add(lblCauHoi, 0, 0);
            tableLayout.Controls.Add(pnlDapAn, 0, 1);

            // Thêm TableLayoutPanel vào panel chính
            pnlCauHoi.Controls.Add(tableLayout);

            // Thêm vào form
            this.pnlMain.Controls.Add(pnlCauHoi);
            pnlCauHoi.BringToFront();
        }

        private void HienThiCauDienChoTrong(CauHoiDaLamDTO cauHoiDaLamDTO, int index)
        {
            // Tạo panel chứa toàn bộ câu hỏi
            Panel pnlCauHoi = new Panel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Padding = new Padding(10),
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Tạo TableLayoutPanel để quản lý layout dễ dàng hơn
            TableLayoutPanel tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                ColumnCount = 1,
                RowCount = 2
            };
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // Label cho câu hỏi
            Label lblCauHoi = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Text = $"Câu {index}: {cauHoiDaLamDTO.NoiDung}",
                Margin = new Padding(0, 0, 0, 10),
                Dock = DockStyle.Fill
            };
            // Panel chứa các đáp án
            FlowLayoutPanel pnlDapAn = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 5, 0, 0)
            };
            // Lấy danh sách đáp án
            CauTraLoiDienChoTrongDaLamBLL dapAnBLL = new CauTraLoiDienChoTrongDaLamBLL();
            List<CauTraLoiDienChoTrongDaLamDTO> dsDapAn = dapAnBLL.GetCauTraLoiByMaCauHoi(cauHoiDaLamDTO.MaCauHoiDaLam);
            // Duyệt qua danh sách đáp án và hiển thị từng phần
            foreach (CauTraLoiDienChoTrongDaLamDTO dapAn in dsDapAn)
            {
                // Label hiển thị phần điền chỗ trống (VD: "(1) - ABC(Đ)")
                Label lblDapAn = new Label
                {
                    AutoSize = true,
                    Font = new Font("Arial", 10),
                    Text = $"({dapAn.ViTri}) - {dapAn.CauTraLoiText} {(dapAn.DapAnText.ToString()==dapAn.CauTraLoiText.ToString() ? "(Đ)" : "(S) - Đáp án đúng là: " +dapAn.DapAnText.ToString())}",
                    Margin = new Padding(0, 5, 0, 5)
                };

                if((dapAn.DapAnText.ToString() == dapAn.CauTraLoiText.ToString()))
                {
                    lblDapAn.ForeColor = Color.FromArgb(34, 139, 34);
                }
                else
                {
                    lblDapAn.ForeColor = Color.Red;
                }

                pnlDapAn.Controls.Add(lblDapAn);
            }

            // Thêm các controls vào TableLayoutPanel
            tableLayout.Controls.Add(lblCauHoi, 0, 0);
            tableLayout.Controls.Add(pnlDapAn, 0, 1);

            // Thêm TableLayoutPanel vào panel chính
            pnlCauHoi.Controls.Add(tableLayout);

            // Thêm vào form
            this.pnlMain.Controls.Add(pnlCauHoi);
            pnlCauHoi.BringToFront();
        }

        private void HienThiCauNoiCau(CauHoiDaLamDTO cauHoiDaLamDTO, int index)
        {
            Panel pnlCauHoi = new Panel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Padding = new Padding(10),
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblCauHoi = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Text = $"Câu {index}: {cauHoiDaLamDTO.NoiDung}",
                Margin = new Padding(0, 0, 0, 10)
            };

            TableLayoutPanel tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                ColumnCount = 3,
                Padding = new Padding(10)
            };

            // Điều chỉnh lại ColumnStyles để phù hợp hơn
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F)); // Bên trái
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F)); // Mũi tên
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F)); // Bên phải

            NoiCauDaLamBLL noiCauBLL = new NoiCauDaLamBLL();
            List<NoiCauDaLamDTO> dsNoiCau = noiCauBLL.GetNoiCauByMaCauHoi(cauHoiDaLamDTO.MaCauHoiDaLam);

            int currentRow = 0;
            foreach (NoiCauDaLamDTO noiCau in dsNoiCau)
            {
                NoiCauTraLoiBLLDaLam noiCauTraLoiBLL = new NoiCauTraLoiBLLDaLam();
                NoiCauTraLoiDaLamDTO NoiCauTraLoi = noiCauTraLoiBLL.GetNoiCauTraLoiByMaNoiCau(noiCau.MaNoiCauDaLam);

                // Thêm row mới cho mỗi cặp nối
                tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                Label lblCauNguon = new Label
                {
                    AutoSize = true,
                    Dock = DockStyle.Fill,
                    Font = new Font("Arial", 10),
                    Text = NoiCauTraLoi.NoiDung,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(5),
                    MaximumSize = new Size(300, 0), // Giới hạn chiều rộng tối đa
                    AutoEllipsis = true, // Hiển thị ... nếu text quá dài
                };

                Label lblMuiTen = new Label
                {
                    AutoSize = true,
                    Dock = DockStyle.Fill,
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    Text = NoiCauTraLoi.DapAnNoi.ToString() == NoiCauTraLoi.DapAnChon.ToString() ? "→" : "✖",
                    ForeColor = NoiCauTraLoi.DapAnNoi.ToString() == NoiCauTraLoi.DapAnChon.ToString() ? Color.Green : Color.Red,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblCauDich = new Label
                {
                    AutoSize = true,
                    Dock = DockStyle.Fill,
                    Font = new Font("Arial", 10),
                    Text = NoiCauTraLoi.DapAnChon,
                    TextAlign = ContentAlignment.MiddleLeft,
                    ForeColor = NoiCauTraLoi.DapAnNoi.ToString() == NoiCauTraLoi.DapAnChon.ToString() ? Color.Green : Color.Red,
                    Padding = new Padding(5),
                    MaximumSize = new Size(300, 0), // Giới hạn chiều rộng tối đa
                    AutoEllipsis = true
                };

                // Thêm controls vào TableLayoutPanel với chỉ định row và column
                tableLayout.Controls.Add(lblCauNguon, 0, currentRow);
                tableLayout.Controls.Add(lblMuiTen, 1, currentRow);
                tableLayout.Controls.Add(lblCauDich, 2, currentRow);

                if (NoiCauTraLoi.DapAnNoi.ToString() != NoiCauTraLoi.DapAnChon.ToString())
                {
                    // Thêm row mới cho đáp án đúng
                    currentRow++;
                    tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                    Label correct = new Label
                    {
                        AutoSize = true,
                        Dock = DockStyle.Fill,
                        Font = new Font("Arial", 10),
                        Text = $"Đáp án đúng: {NoiCauTraLoi.DapAnNoi}",
                        TextAlign = ContentAlignment.MiddleLeft,
                        ForeColor = Color.Green,
                        Padding = new Padding(5),
                        MaximumSize = new Size(300, 0),
                        AutoEllipsis = true
                    };

                    // Thêm label đáp án đúng vào cột cuối cùng của row mới
                    tableLayout.Controls.Add(correct, 2, currentRow);
                }

                currentRow++;
            }

            pnlCauHoi.Controls.Add(lblCauHoi);
            pnlCauHoi.Controls.Add(tableLayout);

            this.pnlMain.Controls.Add(pnlCauHoi);
            pnlCauHoi.BringToFront();
        }
    }
}
