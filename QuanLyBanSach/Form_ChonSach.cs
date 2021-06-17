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
    public partial class Form_ChonSach : Form
    {
        public static string maSach,soHoaDon;
        public static int tongtien = 0;
        public static bool thanhtoan = false;
        public Form_ChonSach()
        {
            InitializeComponent();
            thanhtoan = false;
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
        public void LoadHoaDon()
        {
            DataTable dt = new DataTable();
            string query = "select * from log_sohd";
            dt = Connect(query);
            DataRow dr = dt.Rows[0];
            soHoaDon = dr[0].ToString();
            string query1 = "select tensach,soluongban,dongiaban from chitiethd,sach where sohd=" + soHoaDon + " and chitiethd.masach=sach.masach";
            dt = Connect(query1);
            dgvChiTietHoaDon.DataSource = dt;
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
        public void LoadChiTietSach()
        {
            DataTable dt = new DataTable();
            dt = Connect("select * from sach");
            dgvSach.DataSource = dt;
            int k = 1;
            foreach (DataGridViewRow i in dgvSach.Rows)
            {
                if (i != null)
                {
                    i.Cells[0].Value = k;
                    k++;
                }
            }
        }

        private void Form_ChonSach_Load(object sender, EventArgs e)
        {
            LoadChiTietSach();
            LoadHoaDon();
        }

        private void Form_ChonSach_FormClosing(object sender, FormClosingEventArgs e)
        {
            ExecQuery("delete from log_sohd");
        }

        private void btnQuayve_Click(object sender, EventArgs e)
        {
            if (thanhtoan)
            {
                Close();
            }
            else
            {
                DataTable dt = new DataTable();
                string query = "select * from log_sohd";
                dt = Connect(query);
                DataRow dr = dt.Rows[0];
                string sohd = dr[0].ToString();
                ExecQuery("delete from chitiethd where sohd='" + sohd + "'");
                ExecQuery("delete from hoadon where sohd='" + sohd + "'");
                ExecQuery("delete from log_sohd");
            }

        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            thanhtoan = true;
            DataTable dt = new DataTable();
            string query = "select khachhang.tenkh from hoadon,khachhang where sohd = '"+soHoaDon+"' and hoadon.makh=khachhang.makh";
            dt = Connect(query);
            DataRow dr = dt.Rows[0];
            string tenkhachhang = dr[0].ToString();
            
            MessageBox.Show("Tên Khách Hàng:"+tenkhachhang+"\n"+"Tổng tiền là:" + tongtien);
        }

        private void cmsChon_Click(object sender, EventArgs e)
        {
            maSach = dgvSach.SelectedRows[0].Cells["mas"].Value.ToString(); 
            DataTable dt = new DataTable();
            dt = Connect("select * from sach where masach='"+maSach+"'");
            DataRow dr = dt.Rows[0];
            int soluongcon = Convert.ToInt32(dr[4]);
            if (soluongcon == 0)
            {
                MessageBox.Show("Trong Kho đã hết số lượng loại sách này");
            }
            else
            {
                txtTenSach.Text = dr[1].ToString();
                txtDonGia.Text = dr[5].ToString();
                txtTenSach.ReadOnly = true;
                txtDonGia.ReadOnly = true;
                txtTongTien.ReadOnly = true;
                txtSoLuong.Text = "";
                txtTongTien.Text = "0";

            }
            
        }

        private void dgvSach_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //lấy thông tin hit là số dòng, số cột tại tọa độ e.X,e.Y
                var hit = dgvSach.HitTest(e.X, e.Y);

                //clear dòng được chọn trước đó để mỗi lần kích phải chuột chỉ chọn 1 dòng
                dgvSach.ClearSelection();

                //khi nhấn phải chuột trên dòng nào thì dòng đó được chọn
                dgvSach.Rows[hit.RowIndex].Selected = true;

                //show menu context tại tọa độ đư kích phải chuột
                cmsChon.Show(dgvSach, new Point(e.X, e.Y));
            }
        }

        private void dgvSach_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dgvSach.CurrentRow.Selected = true;
            }
            catch { }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = Connect("select * from sach where masach='" + maSach + "'");
            DataRow dr = dt.Rows[0];
            string soluongban = txtSoLuong.Text;
            string dongiaban = txtDonGia.Text;
            int soluongcon = Convert.ToInt32(dr[4]);
            int soluongmua = Convert.ToInt32(soluongban);
            if(soluongcon < soluongmua)
            {
                MessageBox.Show("Số lượng mua vượt quá số lượng tồn kho");
            }
            else
            {
                int conlai = soluongcon - soluongmua;

                ExecQuery("insert into chitiethd(sohd,masach,soluongban,dongiaban) values(" + soHoaDon + "," + maSach + "," + soluongban + "," + dongiaban + ")");
                ExecQuery("update sach set soluong = " + conlai + " where masach=" + maSach + "");
                MessageBox.Show("đã thêm thành công");
                LoadHoaDon();
                LoadChiTietSach();
                tongtien += Convert.ToInt32(txtTongTien.Text);

            }
            
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string query = "select * from log_sohd";
            dt = Connect(query);
            DataRow dr = dt.Rows[0];
            string sohd = dr[0].ToString();
            ExecQuery("delete from chitiethd where sohd='" + sohd + "'");
            ExecQuery("delete from hoadon where sohd='" + sohd + "'");
            ExecQuery("delete from log_sohd");
            Close();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = Connect("select * from sach where tensach like '%"+ txtSearch.Text +"%'");
            dgvSach.DataSource = dt;
            int k = 1;
            foreach (DataGridViewRow i in dgvSach.Rows)
            {
                if (i != null)
                {
                    i.Cells[0].Value = k;
                    k++;
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            if (txtSoLuong.Text == "")
            {
                int tong = 0;
                txtTongTien.Text = tong.ToString();
            }
            else
            {
                double tong = System.Convert.ToInt32(txtSoLuong.Text) * System.Convert.ToInt32(txtDonGia.Text);
                txtTongTien.Text = tong.ToString();
            }
            
        }
    }
}
