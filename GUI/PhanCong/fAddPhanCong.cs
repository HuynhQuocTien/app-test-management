using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DocumentFormat.OpenXml.Office.Word;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Windows.Controls;
using BLL;
using DTO;

namespace GUI.PhanCong
{
    public partial class fAddPhanCong : Form
    {
        private bool isDataBinding = false;
        private PhanCongControl phanCongControl;
        public fAddPhanCong(PhanCongControl phanCongControl)
        {
            InitializeComponent();
            this.phanCongControl = phanCongControl;
            //Data giáo viên
            loadComboBox();
            //Data LB
        }

        private PhanCongDTO getInfo(int maMonHoc, long maGV)
        {
            int MaPhanCong = 0;
            int MaMonHoc = maMonHoc;
            long MaGiaoVien = maGV;
            return new PhanCongDTO(MaPhanCong,MaMonHoc,MaGiaoVien);
        }
        private void loadComboBox()
        {
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                try
                {
                    isDataBinding = true;  // Bắt đầu quá trình gán dữ liệu

                    // Gán dữ liệu vào DataGridView
                    PhanCongBLL phanCongBLL = new PhanCongBLL();
                    cbMonHoc.DataSource = phanCongBLL.loadComboBox();
                    cbMonHoc.DisplayMember = "TenNguoiDung";  // Tên cột hiển thị
                    cbMonHoc.ValueMember = "MaNguoiDung";     // Giá trị ẩn (ID)

                    cbMonHoc.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
                }
                finally
                {
                    isDataBinding = false;  // Kết thúc quá trình gán dữ liệu
                }
            }
        }

        private void loadListboxPC(string id)
        {
                try
                {
                    PhanCongBLL phanCongBLL = new PhanCongBLL();

                    // Xóa các mục hiện có (nếu cần)
                    listBox1.Items.Clear();

                    // Thêm từng mục vào ListBox
                    foreach (DataRow row in phanCongBLL.loadListboxPC(id).Rows)
                    {
                        listBox1.Items.Add(new KeyValuePair<string, int>(
                            row["TenMonHoc"].ToString(),
                            Convert.ToInt32(row["MaMonHoc"])
                        ));
                    }

                    // Đặt DisplayMember và ValueMember cho ListBox
                    listBox1.DisplayMember = "Key";  // Hiển thị tên môn học
                    listBox1.ValueMember = "Value";  // Mã môn học
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
                } 
        }

        private void loadListboxCPC(string id)
        {
                try
                {

                    // Xóa các mục hiện có (nếu cần)
                    lbCauHoi.Items.Clear();

                    // Thêm từng mục vào ListBox
                    PhanCongBLL phanCongBLL = new PhanCongBLL();

                    foreach (DataRow row in phanCongBLL.loadListboxCPC(id).Rows)
                    {
                            lbCauHoi.Items.Add(new KeyValuePair<string, int>(
                            row["TenMonHoc"].ToString(),
                            Convert.ToInt32(row["MaMonHoc"])
                        ));
                    }

                    // Đặt DisplayMember và ValueMember cho ListBox
                    lbCauHoi.DisplayMember = "Key";  // Hiển thị tên môn học
                    lbCauHoi.ValueMember = "Value";  // Mã môn học
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
                }
            
        }

        private void cbMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu không phải quá trình gán dữ liệu thì mới thực hiện
            if (!isDataBinding && cbMonHoc.SelectedIndex != -1)
            {
                // Lấy giá trị (ValueMember) của mục đã chọn
                string selectedValue = cbMonHoc.SelectedValue.ToString();

                loadListboxPC(selectedValue);
                loadListboxCPC(selectedValue);
            }
        }

        private void buttonMoveAllToRight_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu listBoxLeft có item
            if (lbCauHoi.Items.Count > 0)
            {
                // Thêm tất cả item từ listBoxLeft sang listBoxRight
                foreach (var item in lbCauHoi.Items)
                {
                    listBox1.Items.Add(item);
                }

                // Xóa tất cả item trong listBoxLeft sau khi chuyển
                lbCauHoi.Items.Clear();
            }
        }

        // Nút chuyển tất cả từ phải sang trái
        private void buttonMoveAllToLeft_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu listBoxRight có item
            if (listBox1.Items.Count > 0)
            {
                // Thêm tất cả item từ listBoxRight sang listBoxLeft
                foreach (var item in listBox1.Items)
                {
                    lbCauHoi.Items.Add(item);
                }

                // Xóa tất cả item trong listBoxRight sau khi chuyển
                listBox1.Items.Clear();
            }
        }

        private void buttonRightToLeft_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                // Lấy item được chọn
                var selectedItem = listBox1.SelectedItem;

                // Thêm item đó vào listBox2
                lbCauHoi.Items.Add(selectedItem);

                // Xóa item đó khỏi listBox1
                listBox1.Items.Remove(selectedItem);
            }
        }

        private void buttonLeftToRight_Click(object sender, EventArgs e)
        {
            if (lbCauHoi.SelectedItem != null)
            {
                // Lấy item được chọn
                var selectedItem = lbCauHoi.SelectedItem;

                // Thêm item đó vào listBox2
                listBox1.Items.Add(selectedItem);

                // Xóa item đó khỏi listBox1
                lbCauHoi.Items.Remove(selectedItem);
            }
        }

        
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string selectedValue = cbMonHoc.SelectedValue.ToString();
            // Kiểm tra nếu đã có phân công
            PhanCongBLL phanCongBLL = new PhanCongBLL();    
            if (phanCongBLL.CheckPCExists(selectedValue))
            {
                DeletePC(selectedValue);  // Xóa phân công cũ và thêm mới
            }
            else
            {
                AddPC(selectedValue);  // Thêm mới nếu chưa có phân công
            }
        }

        private void DeletePC(string id)
        {
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                try
                {

                    PhanCongBLL phanCongBLL=new PhanCongBLL();                 
                    if (phanCongBLL.DeleteMAGV(id))
                    {
                        AddPC(id);
                    }
                    else
                    {
                        MessageBox.Show("Mời thử lại!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
                }
            }
        }

        private void AddPC(string id)
        {
                try
                {
                    if (listBox1.Items.Count > 0)
                    {
                        // Lặp qua từng item trong listBox1
                        foreach (var item in listBox1.Items)
                        {
                            // Ép kiểu item về KeyValuePair<string, string>
                            var selectedItem = (KeyValuePair<string, int>)item;

                            // Lấy giá trị key và value
                            string tenMonHoc = selectedItem.Key;  // Đây là tên môn học
                            int maMonHoc = selectedItem.Value;  // Đây là mã môn học (dùng để lưu vào SQL)

                            // Tạo câu lệnh SQL Insert
                            PhanCongBLL phanCongBLL = new PhanCongBLL();
                        if (!phanCongBLL.Add(getInfo(maMonHoc, Convert.ToInt64(id))))
                        {
                            MessageBox.Show("Lỗi thêm dữ liệu!");
                            return; // Nếu muốn thoát khỏi phương thức
                        }

                    }

                    MessageBox.Show("Thành công!");
                    this.Close();
                    this.Dispose();
                }
                    else
                    {
                        MessageBox.Show("Thành công!");
                        this.Close();
                        this.Dispose();
                }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
                }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
