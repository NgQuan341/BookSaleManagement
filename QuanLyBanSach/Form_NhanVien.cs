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
    public partial class Form_NhanVien : Form
    {
        public static string maNhanVien;
        
        public Form_NhanVien()
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
        public void LoadNhanVien()
        {
            DataTable dt = new DataTable();
            dt = Connect("select * from nhanvien");
            dgvNhanVien.DataSource = dt;
            int j = 1;
            foreach (DataGridViewRow i in dgvNhanVien.Rows)
            {
                if (i != null)
                {
                    i.Cells[0].Value = j;
                    j++;
                }
            }
        }


       
        // chọn một hàng
        private void dgvNhanVien_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dgvNhanVien.CurrentRow.Selected = true;
            }
            catch { }
        }
        //Hàm hiện menu strip để chọn chức năng
        private void dgvNhanVien_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //lấy thông tin hit là số dòng, số cột tại tọa độ e.X,e.Y
                var hit = dgvNhanVien.HitTest(e.X, e.Y);

                //clear dòng được chọn trước đó để mỗi lần kích phải chuột chỉ chọn 1 dòng
                dgvNhanVien.ClearSelection();

                //khi nhấn phải chuột trên dòng nào thì dòng đó được chọn
                dgvNhanVien.Rows[hit.RowIndex].Selected = true;

                //show menu context tại tọa độ đư kích phải chuột
                cmsNhanVien.Show(dgvNhanVien, new Point(e.X, e.Y));
            }
        }
        // hàm sự kiện click vào context menu strip
        private void cmsNhanVien_Click(object sender, EventArgs e)
        {
            maNhanVien = dgvNhanVien.SelectedRows[0].Cells["manv"].Value.ToString();
            Form_ChiTietNhanVien f_chitietnv = new Form_ChiTietNhanVien(true);

            f_chitietnv.Show();
            Hide();
        }
        // nút mở form thêm nhanvien
        private void btnThem_Click(object sender, EventArgs e)
        {
            Form_ChiTietNhanVien f_chitietnv = new Form_ChiTietNhanVien(false);
            f_chitietnv.Show();
            Hide();
        }
        // hàm thực thi lệnh query insert update delete
        public void ExecQuery(string query)
        {
            Connect constr = new Connect();
            SqlConnection con = new SqlConnection(constr.connectString);
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            com.ExecuteNonQuery();
            con.Close();
        }
        // nút xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string manv = dgvNhanVien.SelectedRows[0].Cells[1].Value.ToString(); //Cells[1]: ô mã hàng hóa
            DialogResult result = MessageBox.Show("Bạn có thực sự muốn xóa?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result.Equals(DialogResult.OK))
            {
                string query = "delete from nhanvien where manv='" + manv + "'";
                ExecQuery(query);
                LoadNhanVien();
            }
        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnQuayVe_Click(object sender, EventArgs e)
        {
            Form_Menu f_menu = new Form_Menu();
            f_menu.Show();
            Hide();
        }

        private void Form_NhanVien_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
        }
    }
}
