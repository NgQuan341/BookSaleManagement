using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DE4QLHANGHOA_ADO
{
    public partial class Form_Menu : Form
    {
        public Form_Menu()
        {
            InitializeComponent();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            Form_NhanVien form_NhanVien = new Form_NhanVien();
            form_NhanVien.LoadNhanVien();
            form_NhanVien.Show();
            Hide();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            Form_KhachHang form_KhachHang = new Form_KhachHang();
            form_KhachHang.LoadKhachHang();
            form_KhachHang.Show();
            Hide();
        }

        private void btnSach_Click(object sender, EventArgs e)
        {
            Form_Sach form_Sach = new Form_Sach();
            form_Sach.LoadSach();
            form_Sach.Show();
            Hide();
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            Form_ThemHoaDon form_themhoadon = new Form_ThemHoaDon();
            form_themhoadon.Show();
            Hide();
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            Form_HoaDon form_HoaDon = new Form_HoaDon();
            form_HoaDon.LoadHoaDon();
            form_HoaDon.Show();
            Hide();
        }
    }
}
