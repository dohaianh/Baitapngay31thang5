using quanlysinhvien.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlysinhvien
{
    public partial class Form1 : Form
    {
        List<Sinhvien> qlSV;

        public Form1()
        {
            InitializeComponent();
        }
       

        private void btnReload_Click(object sender, EventArgs e)
        {
            // Tạo chuỗi kết nối cơ sở dữ liệu
            string connectionString = @"server=HAIANH\HAIANHDO; database = QLSinhVien; Integrated Security = true;";
            // Tạo đối tưỡng kết nối
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            //// Tão đối tượng thực thi lệnh
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            // Thiết lập truy vấn cho đối tượng Command
            string query = "select A.ID,A.HoTen,B.TenLop from SinhVien A,Lop B where A.MaLop = B.ID";
            sqlCommand.CommandText = query;
            //Mở kết nối tới cơ sở dữ liễu
            sqlConnection.Open();
            //Thực thi lệnh bảng phương thức ExcuteReader
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            // Gọi hàm hiển thị dữ liệu màn hình
            this.DisplayCategory(sqlDataReader);
            // Đóng kết nối
            sqlConnection.Close();
        }
        private void DisplayCategory(SqlDataReader reader)
        {
            // Xóa tất cả các dòng hiện tại
            lvDSSV.Items.Clear();
            // Đọc một dòng dữ liệu
            while (reader.Read())
            {
                // Tạo một dòng mới trong listview
                ListViewItem item = new ListViewItem(reader["ID"].ToString());
          
               
                // Bổ sung các thông tin khác cho ListViewItem
                item.SubItems.Add(reader["HoTen"].ToString());
                item.SubItems.Add(reader["TenLop"].ToString());
                lvDSSV.Items.Add(item);
            }
        }
        private void LoadLop()
        {
            // Tạo chuỗi kết nối cơ sở dữ liệu
            string connectionString = @"server=HAIANH\HAIANHDO; database = QLSinhVien; Integrated Security = true;";
            // Tạo đối tưỡng kết nối
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            //// Tão đối tượng thực thi lệnh
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            // Thiết lập truy vấn cho đối tượng Command
            string query = "select * from Lop";
            sqlCommand.CommandText = query;
            //Mở kết nối tới cơ sở dữ liễu
            sqlConnection.Open();
            //Thực thi lệnh bảng phương thức ExcuteReader
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            // Gọi hàm hiển thị dữ liệu màn hình
            this.DisplayLop(sqlDataReader);

            // Đóng kết nối
            sqlConnection.Close();
        }
        private void DisplayLop(SqlDataReader reader)
        {
            // Xóa tất cả các dòng hiện tại
         List<Lop> lopList = new List<Lop>();
            // Đọc một dòng dữ liệu
            while (reader.Read())
            {
                Lop lop = new Lop();
                lop.ID = int.Parse(reader["ID"].ToString());
                lop.TenLop = reader["TenLop"].ToString();
                lopList.Add(lop);
            }
            cboLop.DataSource=lopList;
            cboLop.DisplayMember = "TenLop";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.LoadLop();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            ListView ds = new ListView();

            for (int i = 0; i < lvDSSV.Items.Count; i++)
            {
                if (lvDSSV.Items[i].SubItems[2].Text == txtTimKiem.Text)
                {
                    ds.Items.Add(lvDSSV.Items[i]);

                }
            }
            lvDSSV = ds;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtHotenSV.Text = "";
            txtId.Text = "";
            cboLop.Text = "";
        }
        bool CheckThongTinSV()
        {
            if (string.IsNullOrWhiteSpace(cboLop.Text) || string.IsNullOrWhiteSpace(txtHotenSV.Text))
                return false;
            return true;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {

            if (!CheckThongTinSV())
            {
                MessageBox.Show("Vui Lòng nhập đây đủ thông tin");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtId.Text))
                {
                    // Tạo chuỗi kết nối cơ sở dữ liệu
                    string connectionString = @"server=HAIANH\HAIANHDO; database = QLSinhVien; Integrated Security = true;";
                    // Tạo đối tưỡng kết nối
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    //// Tão đối tượng thực thi lệnh
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    // Thiết lập truy vấn cho đối tượng Command
                    string query = $"UPDATE SinhVien set MaLop = '{(cboLop.SelectedItem as Lop).ID}', HoTen = N'{(txtHotenSV.Text)}' where ID = '{int.Parse(txtId.Text)}'";
                    sqlCommand.CommandText = query;
                    //Mở kết nối tới cơ sở dữ liễu
                    sqlConnection.Open();
                    //Thực thi lệnh bảng phương thức ExcuteReader
                    int count = sqlCommand.ExecuteNonQuery();


                    // Đóng kết nối
                    sqlConnection.Close();

                }
                

            }
                }
    }
}
