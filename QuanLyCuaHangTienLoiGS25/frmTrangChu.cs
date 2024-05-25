using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangTienLoiGS25
{
    public partial class frmTrangChu : Form
    {
        public bool IsAdmin { get; set; }
        public bool IsAdmin2 { get; set; }
        public bool IsAdmin3 { get; set; }
        public bool IsAdmin4 { get; set; }
        //public Button btnNhanVien { get; set; }
        public frmTrangChu()
        {
            InitializeComponent();
        }
 
        private void frmTrangChu_Load(object sender, EventArgs e)
        {
            pnlHDB.Visible = false;
            pnlKH.Visible = false;
            pnlNV.Visible = false;
            pnlSP.Visible = false;
            pnlKho.Visible = false;
            pnlNCC.Visible = false;
            pnlPN.Visible = false;
            pnlTK.Visible = false;
            //btnNhanVienBH.Enabled = IsAdmin;
            //btnThongKe.Enabled = IsAdmin2;
            //btnKho.Enabled = IsAdmin3;
            //btnPhieuNhap.Enabled = IsAdmin3;
            //btnHDBan.Enabled = IsAdmin4;
        }

        private void ThoatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void DangXuatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                frmDangNhap frmDangNhap = new frmDangNhap();
                frmDangNhap.Show();
            }
            
        }

        private Form currentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock= DockStyle.Fill;
            pnlChild.Controls.Add(childForm);
            pnlChild.Tag=childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void btnHDBan_Click(object sender, EventArgs e)
        {
            pnlHDB.Height=btnHDBan.Height;
            pnlHDB.Visible = true;
            pnlKH.Visible = false;
            pnlNV.Visible = false;
            pnlSP.Visible = false;
            pnlKho.Visible = false;
            pnlNCC.Visible = false;
            pnlPN.Visible = false;
            pnlTK.Visible = false;
            OpenChildForm(new frmHoaDonBan_CTHoaDonBan());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pnlHDB.Visible = false;
            pnlKH.Visible = false;
            pnlNV.Visible = false;
            pnlSP.Visible = false;
            pnlKho.Visible = false;
            pnlNCC.Visible = false;
            pnlPN.Visible = false;
            pnlTK.Visible = false;
            if (currentFormChild!= null)
            {
                currentFormChild.Close();
            }    

        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            pnlKH.Height = btnKhachHang.Height;
            pnlKH.Visible = true;
            pnlHDB.Visible = false;
            pnlNV.Visible = false;
            pnlSP.Visible = false;
            pnlKho.Visible = false;
            pnlNCC.Visible = false;
            pnlPN.Visible = false;
            pnlTK.Visible = false;
            OpenChildForm(new frmKhachHang());
        }

        private void btnNhanVienBH_Click(object sender, EventArgs e)
        {
            pnlNV.Height=btnNhanVienBH.Height;
            pnlNV.Visible = true;
            pnlKH.Visible = false;
            pnlHDB.Visible = false;
            pnlSP.Visible = false;
            pnlKho.Visible = false;
            pnlNCC.Visible = false;
            pnlPN.Visible = false;
            pnlTK.Visible = false;
            OpenChildForm(new frmNhanVien());
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            pnlSP.Height=btnSanPham.Height;
            pnlSP.Visible = true;
            pnlKH.Visible = false;
            pnlNV.Visible = false;
            pnlHDB.Visible = false;
            pnlKho.Visible = false;
            pnlNCC.Visible = false;
            pnlPN.Visible = false;
            pnlTK.Visible = false;
            OpenChildForm(new frmSanPham_LoaiSanPham());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnKho_Click(object sender, EventArgs e)
        {
            pnlKho.Height = btnKho.Height;
            pnlKho.Visible = true;
            pnlKH.Visible = false;
            pnlNV.Visible = false;
            pnlSP.Visible = false;
            pnlHDB.Visible = false;
            pnlNCC.Visible = false;
            pnlPN.Visible = false;
            pnlTK.Visible = false;
            OpenChildForm(new frmKho());
        }

        private void btnNhaCC_Click(object sender, EventArgs e)
        {
            pnlNCC.Height = btnNhaCC.Height;
            pnlNCC.Visible = true;
            pnlKH.Visible = false;
            pnlNV.Visible = false;
            pnlSP.Visible = false;
            pnlKho.Visible = false;
            pnlHDB.Visible = false;
            pnlPN.Visible = false;
            pnlTK.Visible = false;
            OpenChildForm(new frmNhaCungCap());
        }

        private void btnPhieuNhap_Click(object sender, EventArgs e)
        {
            pnlPN.Height = btnPhieuNhap.Height;
            pnlPN.Visible = true;
            pnlKH.Visible = false;
            pnlNV.Visible = false;
            pnlSP.Visible = false;
            pnlKho.Visible = false;
            pnlNCC.Visible = false;
            pnlHDB.Visible = false;
            pnlTK.Visible = false;
            OpenChildForm(new frmPhieuNhap_CTPhieuNhap());
        }

        private void giớiThiệuToolStripMenuItem1_Click(object sender, EventArgs e)
        {        
            OpenChildForm( new frmGioiThieu());
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            pnlTK.Height = btnThongKe.Height;
            pnlTK.Visible = true;
            pnlKH.Visible = false;
            pnlNV.Visible = false;
            pnlSP.Visible = false;
            pnlKho.Visible = false;
            pnlNCC.Visible = false;
            pnlPN.Visible = false;
            pnlHDB.Visible = false;
            OpenChildForm(new frmThongKe());
        }
    }
}
