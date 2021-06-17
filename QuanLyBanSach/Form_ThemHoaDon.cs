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
    public partial class Form_ThemHoaDon : Form
    {
        public Form_ThemHoaDon()
        {
            InitializeComponent();
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
        public void LoadNhanVien()
        {
            DataTable dt = new DataTable();
            dt = Connect("select * from nhanvien");
            foreach (DataRow i in dt.Rows)
            {
                NhanVien nhanvien = new NhanVien()
                {
                    manv = i["manv"].ToString(),
                    tennv = i["tennv"].ToString()
                };
                cbNhanVien.Items.Add(nhanvien);
            }
        }
        public void LoadKhachHang()
        {
            DataTable dt = new DataTable();
            dt = Connect("select * from khachhang");
            foreach (DataRow i in dt.Rows)
            {
                KhachHang khachhang = new KhachHang()
                {
                    makh = i["makh"].ToString(),
                    tenkh = i["tenkh"].ToString()
                };
                cbKhachHang.Items.Add(khachhang);
            }
        }
        private void btnChonSach_Click(object sender, EventArgs e)
        {
            string manv = ((NhanVien)(cbNhanVien.SelectedItem)).manv;
            string makh = ((KhachHang)(cbKhachHang.SelectedItem)).makh;
            string ngayban = txtNgayBan.Text;
            string query = "insert into hoadon(ngayban,manv,makh) values('" + ngayban + "','" + manv + "','" + makh + "')";
            ExecQuery(query);
            Form_ChonSach form_ChonSach = new Form_ChonSach();
            form_ChonSach.Show();
            
        }

        private void Form_ThemHoaDon_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
            LoadKhachHang();
            txtNgayBan.Text = DateTime.Now.ToString();
            txtNgayBan.ReadOnly = true;
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            Form_Menu form_menu = new Form_Menu();
            form_menu.Show();
            Hide();
        }

        private void txtNgayBan_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
