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
    public partial class Form_Sach : Form
    {
        public static string maSach;
        public Form_Sach()
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
        public void LoadSach()
        {
            DataTable dt = new DataTable();
            dt = Connect("select * from sach");
            dgvSach.DataSource = dt;
            int j = 1;
            foreach (DataGridViewRow i in dgvSach.Rows)
            {
                if (i != null)
                {
                    i.Cells[0].Value = j;
                    j++;
                }
            }
        }

        private void xemChiTiếtSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            maSach = dgvSach.SelectedRows[0].Cells["mas"].Value.ToString();
            Form_ChiTietSach f_chitietsach = new Form_ChiTietSach(true);

            f_chitietsach.Show();
            Hide();
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
        private void btnThem_Click(object sender, EventArgs e)
        {
            Form_ChiTietSach f_chitietsach = new Form_ChiTietSach(false);
            f_chitietsach.Show();
            Hide();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string masach = dgvSach.SelectedRows[0].Cells[1].Value.ToString(); //Cells[1]: ô mã hàng hóa
            DialogResult result = MessageBox.Show("Bạn có thực sự muốn xóa?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result.Equals(DialogResult.OK))
            {
                string query = "delete from sach where masach='" + masach + "'";
                ExecQuery(query);
                LoadSach();
            }
        }

        private void btnQuayVe_Click(object sender, EventArgs e)
        {
            Form_Menu f_menu = new Form_Menu();
            f_menu.Show();
            Hide();
        }

        private void Form_Sach_Load(object sender, EventArgs e)
        {
            LoadSach();
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
                cmsSach.Show(dgvSach, new Point(e.X, e.Y));
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
    }
}
