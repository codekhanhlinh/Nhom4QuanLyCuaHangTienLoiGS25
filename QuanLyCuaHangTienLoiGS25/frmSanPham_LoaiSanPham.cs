using QuanLyCuaHangTienLoiGS25.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace QuanLyCuaHangTienLoiGS25
{
    public partial class frmSanPham_LoaiSanPham : Form
    {
        private SqlDataAdapter adapter;
        private DataTable dt;
        public frmSanPham_LoaiSanPham()
        {
            InitializeComponent();
        }
        KetnoiSQL data=new KetnoiSQL();
        private void frmSanPham_Load(object sender, EventArgs e)
        {  
            loadLSP();
            loadSP();
            DieuChinhdgv();
        }
        void loadSP()
        {
            string str = "select * from SanPham";
            adapter = new SqlDataAdapter(str, data.GetConnect());
            dt = new DataTable();
            adapter.Fill(dt);
            //dgvSP.DataSource = dt;
            bdSSP.DataSource = dt;
            dgvSP.DataSource = bdSSP;
            bdNSP.BindingSource = bdSSP;
        }

        void loadLSP()
        {
            string str = "select * from LoaiSP";
            adapter = new SqlDataAdapter(str, data.GetConnect());
            dt = new DataTable();
            adapter.Fill(dt);
            //dgvLSP.DataSource = dt;
            bdSLSP.DataSource = dt;
            dgvLSP.DataSource = bdSLSP;
            bdNLSP.BindingSource = bdSLSP;
        }

        void DieuChinhdgv()
        {
            DataGridView x = dgvLSP;
            DataGridView y= dgvSP;

            x.Columns[0].HeaderText = "Mã LSP";
            x.Columns[1].HeaderText = "Tên LSP";


            {
                x.Columns[0].Width = 80;
                x.Columns[1].Width = 180;
            }

            //Căn giữa tiêu đề
            x.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //x.DefaultCellStyle. = Color.Orange;
            x.DefaultCellStyle.SelectionForeColor = Color.Black;
            x.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            x.EnableHeadersVisualStyles = false; // Tắt việc sử dụng kiểu mặc định cho tiêu đề
            x.ColumnHeadersDefaultCellStyle.ForeColor = Color.DeepSkyBlue; // Thiết lập màu chữ cho tiêu đề cột
            x.GridColor = Color.LightSkyBlue;

            y.Columns[0].HeaderText = "Mã SP";
            y.Columns[1].HeaderText = "Tên SP";
            y.Columns[2].HeaderText = "Số lượng";
            y.Columns[3].HeaderText = "Đơn vị tính";
            y.Columns[4].HeaderText = "Đơn giá";
            y.Columns[5].HeaderText = " Khuyến mãi";
            y.Columns[6].HeaderText = "Mã LSP";

            {
                y.Columns[0].Width = 70;
                y.Columns[1].Width = 250;
                y.Columns[2].Width = 90;
                y.Columns[3].Width = 100;
                y.Columns[4].Width = 80;
                y.Columns[5].Width = 110;
                y.Columns[6].Width = 80;
            }

            //Căn giữa tiêu đề
            y.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            y.DefaultCellStyle.SelectionForeColor = Color.Black;
            y.DefaultCellStyle.SelectionBackColor = Color.Orange;
            y.EnableHeadersVisualStyles = false; // Tắt việc sử dụng kiểu mặc định cho tiêu đề
            y.ColumnHeadersDefaultCellStyle.ForeColor = Color.DeepSkyBlue; // Thiết lập màu chữ cho tiêu đề cột
            y.GridColor = Color.LightSkyBlue;


            y.Columns[0].DefaultCellStyle.Alignment= DataGridViewContentAlignment.MiddleCenter;
            y.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            y.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            y.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            y.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            y.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            
        }
        //1. Tab: LOẠI SẢN PHẨM
        void DemSanPhamTheoMaLoaiSP()
        {
            string maLSP = txtMaLSP.Text;
            string dem = "select count(*) from SanPham where MaLSP = @MaLSP";
            SqlCommand cmd = new SqlCommand(dem, data.GetConnect());
            cmd.Parameters.AddWithValue("@MaLSP", maLSP);
            int slsp = (int)cmd.ExecuteScalar();
            txtSoSP.Text = slsp.ToString();
        }
        private void btnDemSL_Click(object sender, EventArgs e)
        {
            DemSanPhamTheoMaLoaiSP();
        }

        void ThemLSP()
        {
            try
            {
                int vitri = dgvLSP.CurrentCell.RowIndex;
                string maLSP = dgvLSP.Rows[vitri].Cells[0].Value.ToString();
                string tenLSP = dgvLSP.Rows[vitri].Cells[1].Value.ToString();

                string insertLSP = @"insert into LoaiSP values('" + maLSP + "',N'" + tenLSP + "')";
                SqlCommand cmd = new SqlCommand(insertLSP, data.GetConnect());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã thêm 1 loại sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadLSP();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnThemLSP_Click(object sender, EventArgs e)
        {
            ThemLSP();
        }

        void SuaLSP()
        {
            try
            {
                int vitri = dgvLSP.CurrentCell.RowIndex;
                string maLSP = dgvLSP.Rows[vitri].Cells[0].Value.ToString();
                string tenLSP = dgvLSP.Rows[vitri].Cells[1].Value.ToString();

                string updateLSP = @"update LoaiSP set MaLSP= '" + maLSP + "', TenLSP= N'" + tenLSP + "' where MaLSP= '" + maLSP + "'";
                SqlCommand cmd = new SqlCommand(updateLSP, data.GetConnect());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã sửa thông tin loại sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadLSP();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnSuaLSP_Click(object sender, EventArgs e)
        {
            SuaLSP();
        }

        void XoaLSP()
        {
            try
            {
                int vitri = dgvLSP.CurrentCell.RowIndex;
                string maLSP = dgvLSP.Rows[vitri].Cells[0].Value.ToString();
                string tenLSP = dgvLSP.Rows[vitri].Cells[1].Value.ToString();

                // Kiểm tra số lượng sản phẩm thuộc loại sản phẩm
                string countQuery = "select count(*) from SanPham where MaLSP = @maLSP";
                SqlCommand countCmd = new SqlCommand(countQuery, data.GetConnect());
                countCmd.Parameters.AddWithValue("@maLSP", maLSP);
                int count = (int)countCmd.ExecuteScalar();


                if(count > 0 )
                {
                    DialogResult result = MessageBox.Show("LSP có mã " + (maLSP.Substring(0, 6)) + " có " + count.ToString() + " sản phẩm." +
                        "\nBạn có muốn xóa LSP này và số lượng sản phẩm thuộc LSP này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                        //Xóa các chi tiết phiếu nhập liên quan đến các sản phẩm đã bị xóa
                        string deleteCTPNQuery = "DELETE FROM CTPhieuNhap WHERE MaSP IN (SELECT MaSP FROM SanPham WHERE MaLSP = @maLSP)";
                        SqlCommand deleteCTPNCmd = new SqlCommand(deleteCTPNQuery, data.GetConnect());
                        deleteCTPNCmd.Parameters.AddWithValue("@maLSP", maLSP);
                        deleteCTPNCmd.ExecuteNonQuery();

                        // Xóa các sản phẩm thuộc loại sản phẩm
                        string deleteSPQuery = "DELETE FROM SanPham WHERE MaLSP = @maLSP";
                        SqlCommand deleteHoaDonCmd = new SqlCommand(deleteSPQuery, data.GetConnect());
                        deleteHoaDonCmd.Parameters.AddWithValue("@maLSP", maLSP);
                        deleteHoaDonCmd.ExecuteNonQuery();

                        // Xóa loại sản phẩm
                        string deleteLSPQuery = "delete from LoaiSP where MaLSP=@maLSP";
                        SqlCommand ccmd = new SqlCommand(deleteLSPQuery, data.GetConnect());
                        ccmd.Parameters.AddWithValue("@maLSP", maLSP);
                        ccmd.ExecuteNonQuery();
                        MessageBox.Show("Đã xóa 1 LSP và "+count.ToString()+" sản phẩm thuộc LSP này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadLSP();
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa LSP này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        string deleteLSPQuery = "delete from LoaiSP where MaLSP=@maLSP";
                        SqlCommand ccmd = new SqlCommand(deleteLSPQuery, data.GetConnect());
                        ccmd.Parameters.AddWithValue("@maLSP", maLSP);
                        ccmd.ExecuteNonQuery();
                        MessageBox.Show("Đã xóa 1 loại sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadLSP();
                    }
                }
 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoaLSP_Click(object sender, EventArgs e)
        {
            XoaLSP();
        }
        private void btnTimKiemNV_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdoTimMaLSP.Checked)
                {
                    string maLSP = txtTimMaLSP.Text;
                    string timkiem = "select * from LoaiSP where MaLSP like'%'+@MaLSP+'%'";
                    adapter = new SqlDataAdapter(timkiem, data.GetConnect());
                    adapter.SelectCommand.Parameters.AddWithValue("@MaLSP", maLSP);
                    dt.Clear();
                    adapter.Fill(dt);
                    bdSLSP.DataSource = dt;
                    dgvLSP.DataSource = bdSLSP;
                    bdNLSP.BindingSource = bdSLSP;
                }
                else if (rdoTimTenLSP.Checked)
                {
                    string tenLSP = txtTimTenLSP.Text;
                    string timkiem = "select * from LoaiSP where TenLSP like'%'+@TenLSP+'%'";
                    adapter = new SqlDataAdapter(timkiem, data.GetConnect());
                    adapter.SelectCommand.Parameters.AddWithValue("@TenLSP", tenLSP);
                    dt.Clear();
                    adapter.Fill(dt);
                    bdSLSP.DataSource = dt;
                    dgvLSP.DataSource = bdSLSP;
                    bdNLSP.BindingSource = bdSLSP;
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn 1 trong 2 để thực hiện tìm kiếm!", "Thông báo");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //2. Tab: SẢN PHẨM
        void ThemSP()
        {
            try
            {
                int vitri = dgvSP.CurrentCell.RowIndex;
                string maSP = dgvSP.Rows[vitri].Cells[0].Value.ToString();
                string tenSP = dgvSP.Rows[vitri].Cells[1].Value.ToString();
                string sl = dgvSP.Rows[vitri].Cells[2].Value.ToString();
                string dvt = dgvSP.Rows[vitri].Cells[3].Value.ToString();
                string dongia = dgvSP.Rows[vitri].Cells[4].Value.ToString();
                string khuyenmai = dgvSP.Rows[vitri].Cells[5].Value.ToString();
                string maLSP = dgvSP.Rows[vitri].Cells[6].Value.ToString();

                string insertSP = @"insert into SanPham values('" + maSP + "',N'" + tenSP + "','" + sl
                    + "',N'" + dvt + "','" + dongia + "',N'" + khuyenmai + "','" + maLSP + "')";
                SqlCommand cmd = new SqlCommand(insertSP, data.GetConnect());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã thêm 1 sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadSP();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnThemSP_Click(object sender, EventArgs e)
        {
            ThemSP();
        }

        void SuaSP()
        {
            try
            {
                int vitri = dgvSP.CurrentCell.RowIndex;
                string maSP = dgvSP.Rows[vitri].Cells[0].Value.ToString();
                string tenSP = dgvSP.Rows[vitri].Cells[1].Value.ToString();
                string sl = dgvSP.Rows[vitri].Cells[2].Value.ToString();
                string dvt = dgvSP.Rows[vitri].Cells[3].Value.ToString();
                string dongia = dgvSP.Rows[vitri].Cells[4].Value.ToString();
                string khuyenmai = dgvSP.Rows[vitri].Cells[5].Value.ToString();
                string maLSP = dgvSP.Rows[vitri].Cells[6].Value.ToString();

                string updateSP = @"update SanPham set MaSP= '" + maSP + "', TenSP= N'" + tenSP 
                    +"',SoLuong= '"+sl+"',Dvt= N'"+dvt+"',DonGia= '"+dongia+"',KhuyenMai= N'"+
                    "',MaLSP= '"+maLSP+ "' where MaSP= '" + maSP + "'";
                SqlCommand cmd = new SqlCommand(updateSP, data.GetConnect());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã sửa thông tin sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadSP();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnSuaSP_Click(object sender, EventArgs e)
        {
            SuaSP();

        }

        void XoaSP()
        {
            try
            {
                int vitri = dgvSP.CurrentCell.RowIndex;
                string maSP = dgvSP.Rows[vitri].Cells[0].Value.ToString();
                string tenSP = dgvSP.Rows[vitri].Cells[1].Value.ToString();
                string sl = dgvSP.Rows[vitri].Cells[2].Value.ToString();
                string dvt = dgvSP.Rows[vitri].Cells[3].Value.ToString();
                string dongia = dgvSP.Rows[vitri].Cells[4].Value.ToString();
                string khuyenmai = dgvSP.Rows[vitri].Cells[5].Value.ToString();
                string maLSP = dgvSP.Rows[vitri].Cells[6].Value.ToString();

                string deleteSP = @"delete from SanPham where MaSP='" + maSP + "'";
                SqlCommand cmd = new SqlCommand(deleteSP, data.GetConnect());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã xóa 1 sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadSP();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            XoaSP();
        }

        private void btnHoanTacLSP_Click(object sender, EventArgs e)
        {
            loadLSP();
        }

        private void btnHoanTacSP_Click(object sender, EventArgs e)
        {
            loadSP();
        }

        private void btnTimKiemSP_Click(object sender, EventArgs e)
        {
            if(rdoTimMaSP.Checked)
            {
                string maSP = txtTimMaSP.Text;
                string timkiem = "select * from SanPham where MaSP like'%'+@MaSP+'%'";
                adapter = new SqlDataAdapter(timkiem, data.GetConnect());
                adapter.SelectCommand.Parameters.AddWithValue("@MaSP", maSP);
                dt.Clear();
                adapter.Fill(dt);
                bdSSP.DataSource = dt;
                dgvSP.DataSource = bdSSP;
                bdNSP.BindingSource = bdSSP;
            }
            else if( rdoTimTenSP.Checked)                        
            {
                string tenSP = txtTimTenSP.Text;
                string timkiem = "select * from SanPham where TenSP like'%'+@TenSP+'%'";
                adapter = new SqlDataAdapter(timkiem, data.GetConnect());
                adapter.SelectCommand.Parameters.AddWithValue("@TenSP", tenSP);
                dt.Clear();
                adapter.Fill(dt);
                bdSSP.DataSource = dt;
                dgvSP.DataSource = bdSSP;
                bdNSP.BindingSource = bdSSP;
            }
            else 
            {
                MessageBox.Show("Vui lòng chọn 1 trong 2 để thực hiện tìm kiếm!", "Thông báo");
            }
        }
    }

}
