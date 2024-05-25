using QuanLyCuaHangTienLoiGS25.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangTienLoiGS25
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
            frmTrangChu frmTC = new frmTrangChu();
            frmTC.IsAdmin = isAdmin;
            frmTC.IsAdmin2 = isAdmin2;
            frmTC.IsAdmin3= isAdmin3;
            frmTC.IsAdmin4 = isAdmin4;
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtTenDN_DN.Text = "";
            txtTenDN_DN.Focus();
            txtMK_DN.Text = "";
        }
        KetnoiSQL data = new KetnoiSQL();
        private void txtTenDN_DN_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnDN_DN_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDN_DN.Text;
            string matKhau = txtMK_DN.Text;

            //Kiểm tra người dùng có nhập thông tin đầy đủ hay không 
            if (string.IsNullOrEmpty(tenDangNhap) && string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Bạn chưa nhập Tên đăng nhập và Mật khẩu!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (string.IsNullOrEmpty(tenDangNhap))
            {
                MessageBox.Show("Bạn chưa nhập Tên đăng nhập!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Bạn chưa nhập Mật khẩu!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string query = "SELECT * FROM QuyenDangNhap WHERE TenQuyen = @TenQuyen AND MatKhau = @MatKhau";
            SqlParameter[] parameters =
            {
                new SqlParameter("@TenQuyen", SqlDbType.NVarChar) { Value = tenDangNhap },
                new SqlParameter("@MatKhau", SqlDbType.NVarChar) { Value = matKhau }
            };

            DataTable result = new DataTable();
            bool dangNhapThanhCong = false;

            using (SqlConnection connection = data.GetConnect())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }
            if (result.Rows.Count > 0)
            {
                dangNhapThanhCong = true;
                if ((tenDangNhap == "Userbanhang" && rdoNhanVienBH.Checked))
                {
                    frmTrangChu frmTC = new frmTrangChu();
                    frmTC.IsAdmin = false;
                    frmTC.IsAdmin2= false;
                    frmTC.IsAdmin3 = false;
                    frmTC.IsAdmin4 = true;
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    frmTC.ShowDialog();
                }
                else if ((tenDangNhap == "Userkiemkho" && rdoNhanVienKK.Checked))
                {
                    frmTrangChu frmTC = new frmTrangChu();
                    frmTC.IsAdmin = false;
                    frmTC.IsAdmin2 = false;
                    frmTC.IsAdmin3 = true;
                    frmTC.IsAdmin4 = false;
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    frmTC.ShowDialog();
                }
                else if (tenDangNhap == "Quanly" && rdoQuanLy.Checked)
                {
                    frmTrangChu frmTC = new frmTrangChu();
                    frmTC.IsAdmin = true;
                    frmTC.IsAdmin2 = true;
                    frmTC.IsAdmin3 = true;
                    frmTC.IsAdmin4 = true;
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    frmTC.ShowDialog();
                }
                else if (tenDangNhap == "Ketoan" && rdoKeToan.Checked)
                {
                    frmTrangChu frmTC = new frmTrangChu();
                    frmTC.IsAdmin = true;
                    frmTC.IsAdmin2 = true;
                    frmTC.IsAdmin3 = false;
                    frmTC.IsAdmin4 = false;
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    frmTC.ShowDialog();
                }
                else 
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (!dangNhapThanhCong)
            {
                MessageBox.Show("Đăng nhập không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            tmrGS25chay.Enabled = true;
        }

        private void tmrGS25chay_Tick(object sender, EventArgs e)
        {
            if (lblGS25.Left + lblGS25.Width < 0)
            {
                lblGS25.Left = this.ClientSize.Width;
            }
            else
            {
                lblGS25.Left -= 5;
            }
        }

        private void picEyeClose_Click(object sender, EventArgs e)
        {
            if (txtMK_DN.PasswordChar == '*')
            {
                picEyeOpen.BringToFront();
                txtMK_DN.PasswordChar = '\0';
            }
        }

        private void picEyeOpen_Click(object sender, EventArgs e)
        {
            if (txtMK_DN.PasswordChar == '\0')
            {
                picEyeClose.BringToFront();
                txtMK_DN.PasswordChar = '*';
            }
        }

        private void picGoback_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmChonDN frmDKDN = new frmChonDN();
            frmDKDN.Show();
        }

        private bool isAdmin = false;  //Biến lưu trạng thái quyền quản lý
        private bool isAdmin2 = false; //Biến lưu trạng thái quyền quản lý và kế toán
        private bool isAdmin3 = false; //Biến lưu trạng thái quyền nhân viên kiểm kho
        private bool isAdmin4 = false; //Biến lưu trạng thái quyền nhân viên bán hàng
        private void rdoNhanVienBH_CheckedChanged(object sender, EventArgs e)
        {
            // Xử lý khi RadioButton Nhân viên bán hàng được chọn
            if (rdoNhanVienBH.Checked)
            {
                isAdmin = false;
                isAdmin2 = false;
                isAdmin3 = false;
                isAdmin4 = true;
            }
        }

        private void rdoNhanVienKK_CheckedChanged(object sender, EventArgs e)
        {
            // Xử lý khi RadioButton Kiểm kho  được chọn
            if (rdoNhanVienKK.Checked)
            {
                isAdmin = false;
                isAdmin2 = false;
                isAdmin3 = true;
                isAdmin4 = false;
            }

        }

        private void rdoQuanLy_CheckedChanged(object sender, EventArgs e)
        {
            // Xử lý khi RadioButton Quản lý được chọn
            if (rdoQuanLy.Checked)
            {
                isAdmin = true;
                isAdmin2 = true;
                isAdmin3 = true;
                isAdmin4 = true;
            }
        }

        private void rdoKeToan_CheckedChanged(object sender, EventArgs e)
        {
            // Xử lý khi RadioButton Kế toán được chọn
            if (rdoNhanVienKK.Checked)
            {
                isAdmin = false;
                isAdmin2 = true;
                isAdmin3 = false;
                isAdmin4 = false;
            }

        }
    }
}
