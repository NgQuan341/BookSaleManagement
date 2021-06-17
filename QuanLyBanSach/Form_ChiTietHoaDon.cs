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
    public partial class Form_ChiTietHoaDon : Form
    {
        public Form_ChiTietHoaDon(bool check)
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

            DataTable dtHoaDon = new DataTable();
            string query = "select hoadon.sohd,nhanvien.tennv,khachhang.tenkh,hoadon.ngayban from hoadon,khachhang,nhanvien where sohd ="+ Form_HoaDon.maHoaDon + " and hoadon.makh=khachhang.makh and hoadon.manv=nhanvien.manv";
                dtHoaDon = Connect(query);
            DataRow dr = dtHoaDon.Rows[0];
            txtMaHoaDon.Text = dr[0].ToString();
            txtTenNhanVien.Text = dr[1].ToString();
            txtTenKhachHang.Text = dr[2].ToString();
            txtNgayBan.Text = System.Convert.ToDateTime(dr[3]).ToString("dd/MM/yyyy");
            txtMaHoaDon.ReadOnly = true;
            txtTenNhanVien.ReadOnly=true;
            txtTenKhachHang.ReadOnly=true;
            txtNgayBan.ReadOnly=true;


            string query1 = "select tensach,soluongban,dongiaban from chitiethd,sach where sohd="+ Form_HoaDon.maHoaDon + " and chitiethd.masach=sach.masach";
            dtHoaDon = Connect(query1);
            dgvChiTietHoaDon.DataSource = dtHoaDon;
            int j = 1;
            foreach (DataGridViewRow i in dgvChiTietHoaDon.Rows)
            {
                if (i != null)
                {
                    i.Cells[0].Value = j;
                    j++;
                }
            }

        }

        private void Form_ChiTietHoaDon_Load(object sender, EventArgs e)
        {
            LoadChiTiet();
        }

        private void btnQuayve_Click(object sender, EventArgs e)
        {
            Form_HoaDon form_HoaDon = new Form_HoaDon();
            form_HoaDon.LoadHoaDon();
            form_HoaDon.Show();
            Close();
        }
    }
}
