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
    public partial class Form_ChiTietKhachHang : Form
    {
        public Form_ChiTietKhachHang(bool check)
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
                txtMaKhachHang.Hide();
            }
        }
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
        public void ExecQuery(string query)
        {
            Connect constr = new Connect();
            SqlConnection con = new SqlConnection(constr.connectString);
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            com.ExecuteNonQuery();
            con.Close();
        }
        public void LoadChiTiet()
        {

            DataTable dtKhachHang = new DataTable();
            string query = "Select * from khachhang where makh='" + Form_KhachHang.maKhachHang + "'";

            dtKhachHang = Connect(query);
            DataRow dr = dtKhachHang.Rows[0];
            txtMaKhachHang.Text = dr[0].ToString();
            txtTenKhachHang.Text = dr[1].ToString();
            txtDiaChi.Text = dr[2].ToString();
            txtSDT.Text =dr[3].ToString();


            txtMaKhachHang.Enabled = true;
            txtTenKhachHang.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            txtSDT.ReadOnly = true;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenKhachHang.Text != "" && txtDiaChi.Text != "" && txtSDT.Text != "")
            {
                string tennv = txtTenKhachHang.Text;
                string diachi = txtDiaChi.Text;
                string sdt = txtSDT.Text;

                string query = "insert into khachhang (tenkh,diachikh,sdt) values (N'" + tennv + "',N'" + diachi + "','" + sdt + "')";
                ExecQuery(query);
                Form_KhachHang form_KhachHang = new Form_KhachHang();
                form_KhachHang.LoadKhachHang();
                form_KhachHang.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Xin hãy nhập đủ các trường");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtTenKhachHang.ReadOnly = false;
            txtDiaChi.ReadOnly = false;
            txtSDT.ReadOnly = false;

            txtTenKhachHang.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtTenKhachHang.Focus();
            btnSua.Hide();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (txtMaKhachHang.Text != "")
            {
                string makh = txtMaKhachHang.Text;
                string tenkh = txtTenKhachHang.Text;
                string diachi = txtDiaChi.Text;
                string sdt = txtSDT.Text;

                string query = "update khachhang set tenkh=N'" + tenkh + "',diachikh=N'" + diachi + "',sdt='" + sdt + "' where makh='" + makh + "'";
                ExecQuery(query);
                Form_KhachHang form_KhachHang = new Form_KhachHang();
                form_KhachHang.LoadKhachHang();
                form_KhachHang.Show();
                Close();
            }

        }

        private void btnQuayve_Click(object sender, EventArgs e)
        {
            Form_KhachHang form_KhachHang = new Form_KhachHang();
            form_KhachHang.LoadKhachHang();
            form_KhachHang.Show();
            Close();
        }
    }
}
