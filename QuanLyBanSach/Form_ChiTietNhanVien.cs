using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DE4QLHANGHOA_ADO
{
    public partial class Form_ChiTietNhanVien : Form
    {
        public Form_ChiTietNhanVien(bool check)
        {
            InitializeComponent();
            if (check)
            {
                lblThem.Hide();
                btnThem.Hide();
               
                LoadChiTiet();
               
            }
            
            else
            {
                lblSua.Hide();
                btnSua.Hide();
                btnCapNhat.Hide();
                label1.Hide();
                txtMaNhanVien.Hide();
            }
        }
        //Hàm kết nối database
        public DataTable Connect(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                Connect constr = new Connect();
                SqlConnection con = new SqlConnection(constr.connectString);
                SqlCommand com = new SqlCommand(query, con);
                com.CommandType = CommandType.Text;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(com);
                //tạo 1kho ảo để lưu dl về
                //là lớp con của DataSet
                da.Fill(dt);
                con.Close();
            }
            catch (Exception e)
            {
                string a = e.ToString();
                MessageBox.Show("Kết nối thất bại");
            }
            return dt;
        }
        // hàm thực thi query insert update delete
        public void ExecQuery(string query)
        {
            Connect constr = new Connect();
            SqlConnection con = new SqlConnection(constr.connectString);
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            com.ExecuteNonQuery();
            con.Close();
        }
        // hàm select nhân viên bằng mã nhân viên, mã nhân viên là một thuộc tính ở form_nhanvien
        public void LoadChiTiet()
        {
           
            DataTable dtNhanVien = new DataTable();
            string query = "Select * from nhanvien where manv='" + Form_NhanVien.maNhanVien + "'";
           
            dtNhanVien = Connect(query);
            DataRow dr = dtNhanVien.Rows[0];
            txtMaNhanVien.Text = dr[0].ToString();
            txtTenNhanVien.Text = dr[1].ToString();
            txtDiaChi.Text = dr[2].ToString();
            txtNgaySinh.Text = System.Convert.ToDateTime(dr[3]).ToString("dd/MM/yyyy");


            txtMaNhanVien.Enabled = true;
            txtTenNhanVien.ReadOnly=true;
            txtDiaChi.ReadOnly=true;
            txtNgaySinh.ReadOnly=true;

        }
        // nút sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
           
            txtTenNhanVien.ReadOnly = false;
            txtDiaChi.ReadOnly = false;
            txtNgaySinh.ReadOnly = false;
           
            txtTenNhanVien.Text = "";
            txtDiaChi.Text = "";
            txtNgaySinh.Text = "";
            txtTenNhanVien.Focus();
            btnSua.Hide();            
        }

        private void Form_ChiTietNhanVien_Load(object sender, EventArgs e)
        {

        }
        // Nút cập nhật, hiện ra sau khi nút sửa ẩn, hoạt động sau khi nhập liệu
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (txtMaNhanVien.Text != "")
            {
                string manv = txtMaNhanVien.Text;
                string tennv = txtTenNhanVien.Text;
                string diachi = txtDiaChi.Text;
                string ngaysinh = System.Convert.ToDateTime(txtNgaySinh.Text).ToString();

                string query = "update nhanvien set tennv=N'" + tennv + "',diachinv=N'" + diachi + "',ngaysinh='" + ngaysinh + "' where manv='" + manv + "'";
                ExecQuery(query);
                Form_NhanVien form_NhanVien = new Form_NhanVien();
                form_NhanVien.LoadNhanVien();
                form_NhanVien.Show();
                Close();
            }
        }
        // nút quay về 
        private void btnQuayve_Click(object sender, EventArgs e)
        {
            Form_NhanVien form_NhanVien = new Form_NhanVien();
            form_NhanVien.LoadNhanVien();
            form_NhanVien.Show();
            Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenNhanVien.Text != "" && txtDiaChi.Text !="" && txtNgaySinh.Text !="")
            {
                string tennv = txtTenNhanVien.Text;
                string diachi = txtDiaChi.Text;
                string ngaysinh = System.Convert.ToDateTime(txtNgaySinh.Text).ToString();

                string query = "insert into nhanvien (tennv,diachinv,ngaysinh) values (N'" + tennv + "',N'" + diachi + "','" + ngaysinh + "')";
                ExecQuery(query);
                Form_NhanVien form_NhanVien = new Form_NhanVien();
                form_NhanVien.LoadNhanVien();
                form_NhanVien.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Xin hãy nhập đủ các trường");
            }
        }
    }
}
