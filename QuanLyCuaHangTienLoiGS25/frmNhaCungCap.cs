using QuanLyCuaHangTienLoiGS25.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangTienLoiGS25
{
    public partial class frmNhaCungCap : Form
    {
        private SqlDataAdapter adapter;
        private DataTable dt;
        public frmNhaCungCap()
        {
            InitializeComponent();
        }
        KetnoiSQL data= new KetnoiSQL();
        private void frmNhaCungCap_Load(object sender, EventArgs e)
        { 
            loadNCC();
            DieuChinhdgvNCC();
        }
        void loadNCC()
        {
            string str = "select * from NhaCungCap";
            adapter = new SqlDataAdapter(str, data.GetConnect());
            dt = new DataTable();
            adapter.Fill(dt);

            bdSNCC.DataSource = dt;
            dgvNCC.DataSource = bdSNCC;
            bdNNCC.BindingSource = bdSNCC;
        }

        void DieuChinhdgvNCC()
        {
            DataGridView x = dgvNCC;
            x.Columns[0].HeaderText = "Mã NCC";
            x.Columns[1].HeaderText = "Tên NCC";
            x.Columns[2].HeaderText = "SĐT";
            x.Columns[4].HeaderText = "Địa chỉ";

            {
                x.Columns[0].Width = 80;
                x.Columns[1].Width = 260;
                x.Columns[2].Width = 120;
                x.Columns[3].Width = 150;
                x.Columns[4].Width = 250;
            }

            //Căn giữa tiêu đề
            x.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            x.DefaultCellStyle.SelectionForeColor = Color.Black;
            x.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            x.EnableHeadersVisualStyles = false; // Tắt việc sử dụng kiểu mặc định cho tiêu đề
            x.ColumnHeadersDefaultCellStyle.ForeColor = Color.DeepSkyBlue; // Thiết lập màu chữ cho tiêu đề cột
            x.GridColor = Color.LightSkyBlue;
            x.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void dgvNCC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvNCC_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                txtMaNCC.Text = dgvNCC.CurrentRow.Cells[0].Value.ToString();
                txtTenNCC.Text = dgvNCC.CurrentRow.Cells[1].Value.ToString();
                txtSDT.Text = dgvNCC.CurrentRow.Cells[2].Value.ToString();
                txtEmail.Text = dgvNCC.CurrentRow.Cells[3].Value.ToString();
                txtDiaChi.Text = dgvNCC.CurrentRow.Cells[4].Value.ToString();
            }
            catch { }
        }

        private void btnHoanTacNCC_Click(object sender, EventArgs e)
        {
            loadNCC();
        }

        private void btnTimNCC_Click(object sender, EventArgs e)
        {
            if(rdoTimMaNCC.Checked)
            {
                string maNCC = txtTimMaNCC.Text;
                string timkiem = "select * from NhaCungCap where MaNCC like'%'+@MaNCC+'%'";
                adapter = new SqlDataAdapter(timkiem, data.GetConnect());
                adapter.SelectCommand.Parameters.AddWithValue("@MaNCC", maNCC);
                dt.Clear();
                adapter.Fill(dt);
                bdSNCC.DataSource = dt;
                dgvNCC.DataSource = bdSNCC;
                bdNNCC.BindingSource = bdSNCC;
            }
            else if(rdoTimTenNCC.Checked)
            {
                string tenNCC = txtTimTenNCC.Text;
                string timkiem = "select * from NhaCungCap where TenNCC like'%'+@TenNCC+'%'";
                adapter = new SqlDataAdapter(timkiem, data.GetConnect());
                adapter.SelectCommand.Parameters.AddWithValue("@TenNCC", tenNCC);
                dt.Clear();
                adapter.Fill(dt);
                bdSNCC.DataSource = dt;
                dgvNCC.DataSource = bdSNCC;
                bdNNCC.BindingSource = bdSNCC;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 trong 2 để thực hiện tìm kiếm!", "Thông báo");
            }
        }

        void ThemNCC()
        {
            try
            {
                string maNCC = txtMaNCC.Text;
                string tenNCC = txtTenNCC.Text;
                string sdt = txtSDT.Text;
                string email = txtEmail.Text;
                string diachi = txtDiaChi.Text;

                string insertNCC = @"insert into NhaCungCap values('" + maNCC + "',N'" + tenNCC + "','" + sdt
                    + "','" + email + "',N'" + diachi + "')";
                SqlCommand cmmd = new SqlCommand(insertNCC, data.GetConnect());
                cmmd.ExecuteNonQuery();
                MessageBox.Show("Đã thêm 1 nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadNCC();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            ThemNCC();
        }

        void SuaNCC()
        {
            try
            {
                int vitri = dgvNCC.CurrentCell.RowIndex;
                string maNCC = dgvNCC.Rows[vitri].Cells[0].Value.ToString();
                string tenNCC = dgvNCC.Rows[vitri].Cells[1].Value.ToString();
                string sdt = dgvNCC.Rows[vitri].Cells[2].Value.ToString();
                string email = dgvNCC.Rows[vitri].Cells[3].Value.ToString();
                string diachi = dgvNCC.Rows[vitri].Cells[4].Value.ToString();

                string updateNCC = @"update NhaCungCap set MaNCC= '" + maNCC + "', TenNCC= N'" + tenNCC
                    + "',SDT= '" + sdt + "',Email= '" + email + "',DiaChi= N'" + diachi + "' where MaNCC= '" + maNCC + "'";
                SqlCommand cmmd = new SqlCommand(updateNCC, data.GetConnect());
                cmmd.ExecuteNonQuery();
                MessageBox.Show("Đã sửa thông tin nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadNCC();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSuaNCC_Click(object sender, EventArgs e)
        {
            SuaNCC();
        }

        void XoaNCC()
        {
            try
            {
                int vitri = dgvNCC.CurrentCell.RowIndex;
                //Biến maNCCTB lưu giá trị của textbox txtMaNCC.Text.
                string maNCCTB =txtMaNCC.Text;
                //vitri2 lưu giá trị của ô được chọn trong cột đầu tiên (dgvNCC.Rows[vitri].Cells[0].Value.ToString())
                string vitri2 = dgvNCC.Rows[vitri].Cells[0].Value.ToString();
                vitri2 = maNCCTB;

                /* Kiểm tra xem giá trị trong biến maNCCTB có rỗng hoặc null không.
                   Nếu rỗng hoặc null, hiển thị một hộp thoại thông báo yêu cầu người dùng chọn một dòng trước khi tiến hành xóa.*/
                if (string.IsNullOrEmpty(maNCCTB))
                {
                    MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string maNCC = dgvNCC.Rows[vitri].Cells[0].Value.ToString();
                string tenNCC = dgvNCC.Rows[vitri].Cells[1].Value.ToString();
                string sdt = dgvNCC.Rows[vitri].Cells[2].Value.ToString();
                string email = dgvNCC.Rows[vitri].Cells[3].Value.ToString();
                string diachi = dgvNCC.Rows[vitri].Cells[4].Value.ToString();

                // Kiểm tra số lượng phiếu nhập liên kết với nhà cung cấp
                string countQuery = "select count(*) from PhieuNhap where MaNCC = @maNCC";
                SqlCommand countCmd = new SqlCommand(countQuery, data.GetConnect());
                countCmd.Parameters.AddWithValue("@maNCC", maNCC);
                int count = (int)countCmd.ExecuteScalar();

               if (count > 0)
                {
                    DialogResult result = MessageBox.Show("Nhà cung cấp có mã " + (maNCC.Substring(0, 5)) + " nằm trong " + count.ToString() + " phiếu nhập.\nBạn có muốn xóa nhà cung cấp và phiếu nhập chứa nhà cung cấp không?", 
                        "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                        // Xóa chi tiết phiếu nhập có mã phiếu nhập tương ứng
                        string deleteCTPhieuNhap = @"delete from CTPhieuNhap where MaPN in (select MaPN from PhieuNhap where MaNCC = '" + maNCC + "')";
                        SqlCommand cmdCTPhieuNhap = new SqlCommand(deleteCTPhieuNhap, data.GetConnect());
                        cmdCTPhieuNhap.ExecuteNonQuery();

                        // Xóa phiếu nhập có mã NCC tương ứng
                        string deletePhieuNhap = @"delete from PhieuNhap where MaNCC = '" + maNCC + "'";
                        SqlCommand cmdPhieuNhap = new SqlCommand(deletePhieuNhap, data.GetConnect());
                        cmdPhieuNhap.ExecuteNonQuery();

                        // Xóa nhà cung cấp
                        string deleteNCC = @"delete from NhaCungCap where MaNCC = '" + maNCC + "'";
                        SqlCommand cmdNCC = new SqlCommand(deleteNCC, data.GetConnect());
                        cmdNCC.ExecuteNonQuery();

                        MessageBox.Show("Đã xóa 1 nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadNCC();
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhà cung cấp này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        string deleteNCC = @"delete from NhaCungCap where MaNCC = '" + maNCC + "'";
                        SqlCommand cmdNCC = new SqlCommand(deleteNCC, data.GetConnect());
                        cmdNCC.ExecuteNonQuery();

                        MessageBox.Show("Đã xóa 1 nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadNCC();
                    }
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnXoaNCC_Click(object sender, EventArgs e)
        {
            XoaNCC();
        }
    }
}
