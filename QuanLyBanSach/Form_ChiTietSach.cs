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
    public partial class Form_ChiTietSach : Form
    {
        public Form_ChiTietSach(bool check)
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
                txtMaSach.Hide();
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
        public void LoadChiTiet()
        {

            DataTable dtNhanVien = new DataTable();
            string query = "Select * from sach where masach='" + Form_Sach.maSach + "'";

            dtNhanVien = Connect(query);
            DataRow dr = dtNhanVien.Rows[0];
            txtMaSach.Text = dr[0].ToString();
            txtTenSach.Text = dr[1].ToString();
            txtTacGia.Text = dr[2].ToString();
            txtNXB.Text = dr[3].ToString();
            txtSoLuong.Text = dr[4].ToString();
            txtDonGia.Text = dr[5].ToString();


            txtMaSach.ReadOnly = true;
            txtTenSach.ReadOnly = true;
            txtTacGia.ReadOnly = true;
            txtNXB.ReadOnly = true;
            txtSoLuong.ReadOnly = true;
            txtDonGia.ReadOnly = true;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenSach.Text != "" && txtTacGia.Text != "" && txtNXB.Text != "" && txtSoLuong.Text != "" && txtDonGia.Text != "")
            {
                string tensach = txtTenSach.Text;
                string tacgia = txtTacGia.Text;
                string nxb = txtNXB.Text;
                int soluong = System.Convert.ToInt32(txtSoLuong.Text);
                double dongia = System.Convert.ToDouble(txtDonGia.Text);
                    

                string query = "insert into sach (tensach,tacgia,nxb,soluong,dongia) values (N'" + tensach + "',N'" + tacgia + "','" + nxb + "','" + soluong + "','" + dongia + "')";
                ExecQuery(query);
                Form_Sach form_Sach = new Form_Sach();
                form_Sach.LoadSach();
                form_Sach.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Xin hãy nhập đủ các trường");
            }


        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtTenSach.ReadOnly = false;
            txtTacGia.ReadOnly = false;
            txtNXB.ReadOnly = false;
            txtSoLuong.ReadOnly = false;
            txtDonGia.ReadOnly = false;

            txtTenSach.Text = "";
            txtTacGia.Text = "";
            txtNXB.Text = "";
            txtSoLuong.Text = "";
            txtDonGia.Text = "";
            btnSua.Hide();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (txtMaSach.Text != "")
            {
                string masach = txtMaSach.Text;
                string tensach = txtTenSach.Text;
                string tacgia = txtTacGia.Text;
                string nxb = txtNXB.Text;
                int soluong = System.Convert.ToInt32(txtSoLuong.Text);
                double dongia = System.Convert.ToDouble(txtDonGia.Text);

                string query = "update sach set tensach=N'" + tensach + "',tacgia=N'" + tacgia + "',nxb=N'" + nxb + "',soluong='" + soluong + "',dongia='" + dongia + "' where masach='" + masach + "'";
                ExecQuery(query);
                Form_Sach form_Sach = new Form_Sach();
                form_Sach.LoadSach();
                form_Sach.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Xin hãy nhập đủ các trường");
            }
        }

        private void btnQuayve_Click(object sender, EventArgs e)
        {
            Form_Sach form_Sach = new Form_Sach();
            form_Sach.LoadSach();
            form_Sach.Show();
            Close();
        }
    }
}
