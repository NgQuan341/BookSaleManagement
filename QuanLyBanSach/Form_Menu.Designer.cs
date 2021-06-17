
namespace DE4QLHANGHOA_ADO
{
    partial class Form_Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnNhanVien = new System.Windows.Forms.Button();
            this.btnSach = new System.Windows.Forms.Button();
            this.btnHoaDon = new System.Windows.Forms.Button();
            this.btnKhachHang = new System.Windows.Forms.Button();
            this.btnNhap = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnNhanVien
            // 
            this.btnNhanVien.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.btnNhanVien.Location = new System.Drawing.Point(202, 217);
            this.btnNhanVien.Name = "btnNhanVien";
            this.btnNhanVien.Size = new System.Drawing.Size(443, 71);
            this.btnNhanVien.TabIndex = 0;
            this.btnNhanVien.Text = "Thông tin nhân viên";
            this.btnNhanVien.UseVisualStyleBackColor = true;
            this.btnNhanVien.Click += new System.EventHandler(this.btnNhanVien_Click);
            // 
            // btnSach
            // 
            this.btnSach.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.btnSach.Location = new System.Drawing.Point(202, 294);
            this.btnSach.Name = "btnSach";
            this.btnSach.Size = new System.Drawing.Size(443, 71);
            this.btnSach.TabIndex = 0;
            this.btnSach.Text = "Thông tin sách";
            this.btnSach.UseVisualStyleBackColor = true;
            this.btnSach.Click += new System.EventHandler(this.btnSach_Click);
            // 
            // btnHoaDon
            // 
            this.btnHoaDon.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.btnHoaDon.Location = new System.Drawing.Point(202, 448);
            this.btnHoaDon.Name = "btnHoaDon";
            this.btnHoaDon.Size = new System.Drawing.Size(443, 71);
            this.btnHoaDon.TabIndex = 0;
            this.btnHoaDon.Text = "Thông tin Hóa Đơn";
            this.btnHoaDon.UseVisualStyleBackColor = true;
            this.btnHoaDon.Click += new System.EventHandler(this.btnHoaDon_Click);
            // 
            // btnKhachHang
            // 
            this.btnKhachHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.btnKhachHang.Location = new System.Drawing.Point(202, 371);
            this.btnKhachHang.Name = "btnKhachHang";
            this.btnKhachHang.Size = new System.Drawing.Size(443, 71);
            this.btnKhachHang.TabIndex = 0;
            this.btnKhachHang.Text = "Thông tin Khách Hàng";
            this.btnKhachHang.UseVisualStyleBackColor = true;
            this.btnKhachHang.Click += new System.EventHandler(this.btnKhachHang_Click);
            // 
            // btnNhap
            // 
            this.btnNhap.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.btnNhap.Location = new System.Drawing.Point(202, 140);
            this.btnNhap.Name = "btnNhap";
            this.btnNhap.Size = new System.Drawing.Size(443, 71);
            this.btnNhap.TabIndex = 0;
            this.btnNhap.Text = "Nhập hóa đơn mới";
            this.btnNhap.UseVisualStyleBackColor = true;
            this.btnNhap.Click += new System.EventHandler(this.btnNhap_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label1.Location = new System.Drawing.Point(285, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(281, 39);
            this.label1.TabIndex = 1;
            this.label1.Text = "Quản lý bán sách";
            // 
            // Form_Menu
            // 
            this.ClientSize = new System.Drawing.Size(834, 675);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnKhachHang);
            this.Controls.Add(this.btnNhap);
            this.Controls.Add(this.btnHoaDon);
            this.Controls.Add(this.btnSach);
            this.Controls.Add(this.btnNhanVien);
            this.Name = "Form_Menu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNhanVien;
        private System.Windows.Forms.Button btnSach;
        private System.Windows.Forms.Button btnHoaDon;
        private System.Windows.Forms.Button btnKhachHang;
        private System.Windows.Forms.Button btnNhap;
        private System.Windows.Forms.Label label1;
    }
}
