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

namespace GUI.PhanCong
{
    public partial class fAddPhanCong : Form
    {
        private bool isDataBinding = false;
        public fAddPhanCong()
        {
            InitializeComponent();

            //Data giáo viên
            loadComboBox();
            //Data LB
        }

        private void loadComboBox()
        {
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                try
                {
                    isDataBinding = true;  // Bắt đầu quá trình gán dữ liệu
                    string query = "SELECT ND.Ten as 'TenNguoiDung', ND.MaNguoiDung as 'MaNguoiDung' FROM NguoiDung ND INNER JOIN TaiKhoan TK ON ND.MaNguoiDung = TK.Username INNER JOIN NhomQuyen NQ ON NQ.MaNhomQuyen = TK.MaNhomQuyen WHERE NQ.MaNhomQuyen = 2;";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();

                    // Đổ dữ liệu vào DataTable
                    da.Fill(dt);

                    // Gán dữ liệu vào DataGridView
                    cbMonHoc.DataSource = dt;
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
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                try
                {

                    string query = "SELECT MH.TenMonHoc, MH.MaMonHoc FROM PhanCong PC INNER JOIN MonHoc MH ON MH.MaMonHoc = PC.MaMonHoc WHERE PC.MaGV = @MaGV;";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaGV", id);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);  // Sử dụng SqlCommand ở đây
                    DataTable dt = new DataTable();

                    // Đổ dữ liệu vào DataTable
                    da.Fill(dt);

                    // Xóa các mục hiện có (nếu cần)
                    listBox1.Items.Clear();

                    // Thêm từng mục vào ListBox
                    foreach (DataRow row in dt.Rows)
                    {
                        listBox1.Items.Add(new KeyValuePair<string, string>(row["TenMonHoc"].ToString(), row["MaMonHoc"].ToString()));  // Thêm tên môn học vào ListBox
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
        }

        private void loadListboxCPC(string id)
        {
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                try
                {

                    string query = "SELECT MH.TenMonHoc, MH.MaMonHoc FROM MonHoc MH WHERE NOT EXISTS (SELECT 1 FROM PhanCong PC WHERE PC.MaMonHoc = MH.MaMonHoc AND PC.MaGV = @MaGV);";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaGV", id);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);  // Sử dụng SqlCommand ở đây
                    DataTable dt = new DataTable();

                    // Đổ dữ liệu vào DataTable
                    da.Fill(dt);

                    // Xóa các mục hiện có (nếu cần)
                    lbCauHoi.Items.Clear();

                    // Thêm từng mục vào ListBox
                    foreach (DataRow row in dt.Rows)
                    {
                        lbCauHoi.Items.Add(new KeyValuePair<string, string>(row["TenMonHoc"].ToString(), row["MaMonHoc"].ToString()));  // Thêm tên môn học vào ListBox
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

        private bool CheckPCExists(string id)
        {
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                try
                {
                    string query = "SELECT COUNT(1) FROM PhanCong WHERE MaGV = @MaGV";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaGV", id);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0; // Trả về true nếu có bản ghi tồn tại
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
                    return false;
                }
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string selectedValue = cbMonHoc.SelectedValue.ToString();
            // Kiểm tra nếu đã có phân công
            if (CheckPCExists(selectedValue))
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

                    string query = "Delete FROM PhanCong Where MaGV=@MaGV;";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaGV", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
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
            using (SqlConnection conn = GetConnectionDb.GetConnection())
            {
                try
                {
                    if (listBox1.Items.Count > 0)
                    {
                        // Lặp qua từng item trong listBox1
                        foreach (var item in listBox1.Items)
                        {
                            // Ép kiểu item về KeyValuePair<string, string>
                            var selectedItem = (KeyValuePair<string, string>)item;

                            // Lấy giá trị key và value
                            string tenMonHoc = selectedItem.Key;  // Đây là tên môn học
                            string maMonHoc = selectedItem.Value;  // Đây là mã môn học (dùng để lưu vào SQL)

                            // Tạo câu lệnh SQL Insert
                            string query = "INSERT INTO PhanCong (MaMonHoc, MaGV) VALUES (@MaMonHoc, @MaGV);";
                            SqlCommand cmd = new SqlCommand(query, conn);

                            // Thêm tham số cho câu lệnh SQL
                            cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);  // Giá trị mã môn học
                            cmd.Parameters.AddWithValue("@MaGV", id);  // Giá trị mã giáo viên (được truyền vào)

                            // Thực thi câu lệnh SQL
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Thêm dữ liệu thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Xóa thành công!");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
                }
            }
        }
    }
}
