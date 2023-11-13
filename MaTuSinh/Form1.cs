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

namespace MaTuSinh
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void xoatext()
        {
            txtMaKhoa.Text = "";
            txtTenKhoa.Text = "";
        }
        private void ketnoi()
        {
            SqlConnection kn = new SqlConnection(@"Data Source=QUANGPRO;Initial Catalog=MaTuSinh;Integrated Security=True"); //khởi tạo đối tượng kết nối đến data sql
            kn.Open();
            try
            {
                string sql = "select * from Khoa";
                SqlCommand commandsql = new SqlCommand(sql, kn); //thực thi câu lệnh sql
                SqlDataAdapter com = new SqlDataAdapter(commandsql); //cho phép người dùng đổ dữ liệu vào một DataTable và cập nhật thay đổi vào database
                DataTable table = new DataTable(); // hiện dữ liệu trong sql thành bảng
                com.Fill(table); // // điều này sẽ truy vấn cơ sở dữ liệu của bạn và trả về kết quả cho bảng của bạn
                dataGridView1.DataSource = table; // đổ dữ liệu vào bảng ds khoa
            }
            catch
            {
                // hiển thị hộp thoại khi không kết nối đc sẽ out form 
                MessageBox.Show("Lỗi kết nối");
            }
            kn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'maTuSinhDataSet.Khoa' table. You can move, or remove it, as needed.
            this.khoaTableAdapter.Fill(this.maTuSinhDataSet.Khoa);
            ketnoi();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            txtMaKhoa.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
            txtTenKhoa.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
        }

        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int count = 0;

            count = dataGridView1.Rows.Count;//đếm tất cả các dòng trong datagridviewl rồi đem đi gần cho count

            string chuoi = "";

            int chuoi2 = 0;

            chuoi = Convert.ToString(dataGridView1.Rows[count - 2].Cells[0].Value);

            chuoi2 = Convert.ToInt32((chuoi.Remove(0, 2)));//loại bỏ kí tự chữ MK

            if (chuoi2 + 1 < 10)

            {
                txtMaKhoa.Text = "MK0" + (chuoi2 + 1).ToString();//cái này có nghĩa là có phép tử này cộng dồn lên khi thỏa mãn điều kiện if VD MK01 sẽ tăng lên MK02 và nếu vượt quá 10 thì sẽ else
            }
            else if (chuoi2 + 1 < 100)

            { txtMaKhoa.Text = "MK" + (chuoi2 + 1).ToString(); }


            SqlConnection kn = new SqlConnection(@"Data Source=QUANGPRO;Initial Catalog=MaTuSinh;Integrated Security=True");

            string TenKhoa = txtTenKhoa.Text;

            if (TenKhoa == "")
            {
                MessageBox.Show("Vui Lòng điền đầy đủ thông tin!");
            }
            else

                try
                {
                    kn.Open();
                    string sql = " select MaKhoa from Khoa where MaKhoa = N'" + txtMaKhoa.Text + @"'";
                    SqlCommand cmd = new SqlCommand(sql, kn);
                    SqlDataReader dta = cmd.ExecuteReader();
                    if (dta.Read() == true)
                    {
                        DialogResult f = MessageBox.Show("Mã khoa đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        kn.Close();

                        kn.Open();
                        string them = "insert into Khoa values (N'" + txtMaKhoa.Text + @"',N'" + txtTenKhoa.Text + @"')";
                        SqlCommand commandthem = new SqlCommand(them, kn);
                        int kq = commandthem.ExecuteNonQuery();
                        if (kq > 0)
                        {
                            MessageBox.Show("Thêm Thành Công!", "Thông báo");
                            xoatext();
                        }
                    }
                }
                catch
                {
                    DialogResult f = MessageBox.Show("Lỗi không thêm được!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            ketnoi();
            kn.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult f = MessageBox.Show("Bạn có thật sự muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (f == DialogResult.Yes)
            {
                SqlConnection kn = new SqlConnection(@"Data Source=QUANGPRO;Initial Catalog=MaTuSinh;Integrated Security=True");
                kn.Open();
                string xoa = "delete Khoa where MaKhoa = N'" + txtMaKhoa.Text + "'";
                SqlCommand comm = new SqlCommand(xoa, kn);
                int kq = comm.ExecuteNonQuery();
                if (kq > 0)
                {
                    MessageBox.Show("Xóa Thành Công!", "Thông báo");
                    xoatext();
                }
                else
                    MessageBox.Show("Lỗi không xóa được!", "Thông báo");
                ketnoi();
            }
        }
    }
}
