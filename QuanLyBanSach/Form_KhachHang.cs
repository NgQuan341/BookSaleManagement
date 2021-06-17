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
    public partial class Form_KhachHang : Form
    {
        public static string maKhachHang;
        public Form_KhachHang()
        {
            InitializeComponent();
        }
        //Hàm kết nối database đổ ra dữ liệu dạng bảng
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
        //Hàm đổ dữ liệu bảng nhanvien
        public void LoadKhachHang()
        {
            DataTable dt = new DataTable();
            dt = Connect("select * from khachhang");
            dgvKhachHang.DataSource = dt;
            int j = 1;
            foreach (DataGridViewRow i in dgvKhachHang.Rows)
            {
                if (i != null)
                {
                    i.Cells[0].Value = j;
                    j++;
                }
            }
        }

        private void Form_KhachHang_Load(object sender, EventArgs e)
        {
            LoadKhachHang();
        }

        private void dgvKhachHang_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dgvKhachHang.CurrentRow.Selected = true;
            }
            catch { }
        }

        private void dgvKhachHang_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //lấy thông tin hit là số dòng, số cột tại tọa độ e.X,e.Y
                var hit = dgvKhachHang.HitTest(e.X, e.Y);

                //clear dòng được chọn trước đó để mỗi lần kích phải chuột chỉ chọn 1 dòng
                dgvKhachHang.ClearSelection();

                //khi nhấn phải chuột trên dòng nào thì dòng đó được chọn
                dgvKhachHang.Rows[hit.RowIndex].Selected = true;

                //show menu context tại tọa độ đư kích phải chuột
                cmsKhachHang.Show(dgvKhachHang, new Point(e.X, e.Y));
            }
        }

        private void cmsKhachHang_Click(object sender, EventArgs e)
        {
            maKhachHang = dgvKhachHang.SelectedRows[0].Cells["makh"].Value.ToString();
            Form_ChiTietKhachHang f_chitietkh = new Form_ChiTietKhachHang(true);

            f_chitietkh.Show();
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string makh = dgvKhachHang.SelectedRows[0].Cells[1].Value.ToString(); //Cells[1]: ô mã hàng hóa
            DialogResult result = MessageBox.Show("Bạn có thực sự muốn xóa?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result.Equals(DialogResult.OK))
            {
                string query = "delete from khachhang where makh='" + makh + "'";
                ExecQuery(query);
                LoadKhachHang();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Form_ChiTietKhachHang f_chitietkh = new Form_ChiTietKhachHang(false);
            f_chitietkh.Show();
            Hide();
        }

        private void btnQuayVe_Click(object sender, EventArgs e)
        {
            Form_Menu f_menu = new Form_Menu();
            f_menu.Show();
            Hide();
        }

        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
