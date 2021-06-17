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
    public partial class Form_HoaDon : Form
    {
        public static string maHoaDon;
        public Form_HoaDon()
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
        public void LoadHoaDon()
        {
            DataTable dt = new DataTable();
            dt = Connect("select * from hoadon");
            dgvHoaDon.DataSource = dt;
            int j = 1;
            foreach (DataGridViewRow i in dgvHoaDon.Rows)
            {
                if (i != null)
                {
                    i.Cells[0].Value = j;
                    j++;
                }
            }
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


        private void Form_HoaDon_Load(object sender, EventArgs e)
        {
            LoadHoaDon();
        }

        private void xemChiTiếtHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            maHoaDon = dgvHoaDon.SelectedRows[0].Cells["sohd"].Value.ToString();
            Form_ChiTietHoaDon f_chitiethoadon = new Form_ChiTietHoaDon(true);

            f_chitiethoadon.Show();
            Hide();
        }

        private void dgvHoaDon_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //lấy thông tin hit là số dòng, số cột tại tọa độ e.X,e.Y
                var hit = dgvHoaDon.HitTest(e.X, e.Y);

                //clear dòng được chọn trước đó để mỗi lần kích phải chuột chỉ chọn 1 dòng
                dgvHoaDon.ClearSelection();

                //khi nhấn phải chuột trên dòng nào thì dòng đó được chọn
                dgvHoaDon.Rows[hit.RowIndex].Selected = true;

                //show menu context tại tọa độ đư kích phải chuột
                cmsHoaDon.Show(dgvHoaDon, new Point(e.X, e.Y));
            }
        }

        private void dgvHoaDon_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dgvHoaDon.CurrentRow.Selected = true;
            }
            catch { }
        }

        private void btnQuayVe_Click(object sender, EventArgs e)
        {
            Form_Menu f_menu = new Form_Menu();
            f_menu.Show();
            Hide();
        }
    }
}
