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

namespace QuanLyCuaHangTienLoiGS25
{
    public partial class frmKhachHang : Form
    {
        public frmKhachHang()
        {
            InitializeComponent();
        }
        KetnoiSQL data= new KetnoiSQL();
        private SqlDataAdapter adapter;
        private DataTable dt;

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            cboGioiTinh.Items.Add("Nam");
            cboGioiTinh.Items.Add("Nữ");
            loadKH();
            DieuChinhdgvKH();
        }

        private void loadKH()
        {
            string str = "select * from KhachHang";
            adapter = new SqlDataAdapter(str, data.GetConnect());
            dt = new DataTable();
            adapter.Fill(dt);
            bdSKH.DataSource = dt;
            dgvKH.DataSource = bdSKH;
            bdNKH.BindingSource = bdSKH;
        }

        void DieuChinhdgvKH()
        {
            DataGridView x = dgvKH;
            x.Columns[0].HeaderText = "Mã khách hàng";
            x.Columns[1].HeaderText = "Họ và tên";
            x.Columns[2].HeaderText = "Giới tính";
            x.Columns[3].HeaderText = " SĐT";
            x.Columns[4].HeaderText = " Email";
            x.Columns[5].HeaderText = "Điểm tích lũy";

            {
                x.Columns[0].Width = 120;
                x.Columns[1].Width = 160;
                x.Columns[2].Width = 90;
                x.Columns[3].Width = 100;
                x.Columns[4].Width = 200;
                x.Columns[5].Width = 120;
            }

            //Căn giữa tiêu đề
            x.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //Căn giữa các nội dung trong từng cột
            x.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            x.DefaultCellStyle.SelectionForeColor = Color.Black;
            x.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            x.EnableHeadersVisualStyles = false; // Tắt việc sử dụng kiểu mặc định cho tiêu đề
            x.ColumnHeadersDefaultCellStyle.ForeColor = Color.DeepSkyBlue; // Thiết lập màu chữ cho tiêu đề cột
            x.GridColor = Color.LightSkyBlue;
        }

        private void dgvKH_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                txtMaKH.Text = dgvKH.CurrentRow.Cells[0].Value.ToString();
                txtHoTenKH.Text = dgvKH.CurrentRow.Cells[1].Value.ToString();
                cboGioiTinh.Text = dgvKH.CurrentRow.Cells[2].Value.ToString();
                txtSDTKH.Text = dgvKH.CurrentRow.Cells[3].Value.ToString();
                txtEmailKH.Text = dgvKH.CurrentRow.Cells[4].Value.ToString();
                txtDTL.Text = dgvKH.CurrentRow.Cells[5].Value.ToString();
            }
            catch { }
        }

        private void btnHoanTac_Click(object sender, EventArgs e)
        {
            loadKH();
        }

        private void btnTimKiemKH_Click(object sender, EventArgs e)
        {
            if(rdoMaKH.Checked)
            {
                string maKH = txtTimMaKH.Text;
                string timkiem="select * from KhachHang where MaKH like'%'+@MaKH+'%'";
                adapter = new SqlDataAdapter(timkiem, data.GetConnect());
                adapter.SelectCommand.Parameters.AddWithValue("@MaKH", maKH);
                dt.Clear();
                adapter.Fill(dt);
                bdSKH.DataSource = dt;
                dgvKH.DataSource = bdSKH;
                bdNKH.BindingSource = bdSKH;
            }
            else if (rdoHoTenKH.Checked)
            {
                string tenKH = txtTimHoTenKH.Text;
                string timkiem = "select * from KhachHang where HoTenKH like'%'+@HoTenKH+'%'";
                adapter=new SqlDataAdapter(timkiem, data.GetConnect());
                adapter.SelectCommand.Parameters.AddWithValue("@HoTenKH", tenKH);
                dt.Clear();
                adapter.Fill(dt);
                bdSKH.DataSource = dt;
                dgvKH.DataSource = bdSKH;
                bdNKH.BindingSource = bdSKH;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 trong 2 để thực hiện tìm kiếm!", "Thông báo");
            }
        }
        void Them()
        {
            try
            {
                int vitri = dgvKH.CurrentCell.RowIndex;
                string maKH = dgvKH.Rows[vitri].Cells[0].Value.ToString();
                string tenKH = dgvKH.Rows[vitri].Cells[1].Value.ToString();
                string gioitinh = dgvKH.Rows[vitri].Cells[2].Value.ToString();
                string sdt = dgvKH.Rows[vitri].Cells[3].Value.ToString();
                string email = dgvKH.Rows[vitri].Cells[4].Value.ToString();
                string diemtl = dgvKH.Rows[vitri].Cells[5].Value.ToString();

                string insert = @"insert into KhachHang values ('" + maKH + "', N'" + tenKH + "', N'" + gioitinh + "','" + sdt +
                    "','" + email + "','" + diemtl + "')";
                SqlCommand cmd = new SqlCommand(insert, data.GetConnect());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã thêm 1 khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadKH();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            Them();
        }

        void Xoa()
        {
            try{ 
            int vitri = dgvKH.CurrentCell.RowIndex;
            string maKH = dgvKH.Rows[vitri].Cells[0].Value.ToString();
            string tenKH = dgvKH.Rows[vitri].Cells[1].Value.ToString();
            string gioiTinh = dgvKH.Rows[vitri].Cells[2].Value.ToString();
            string sdt = dgvKH.Rows[vitri].Cells[3].Value.ToString();
            string email = dgvKH.Rows[vitri].Cells[4].Value.ToString();
            string diemTL = dgvKH.Rows[vitri].Cells[5].Value.ToString();

            // Kiểm tra số lượng hóa đơn liên kết với khách hàng
            string countQuery = "SELECT COUNT(*) FROM HoaDonBan WHERE MaKH = @maKH";
            SqlCommand countCmd = new SqlCommand(countQuery, data.GetConnect());
            countCmd.Parameters.AddWithValue("@maKH", maKH);
            int count = (int)countCmd.ExecuteScalar();

            if (count > 0)
            {
                DialogResult result = MessageBox.Show("Khách hàng có mã " + (maKH.Substring(0, 6)) + " có " + count.ToString() + " hóa đơn.\nBạn có muốn xóa khách hàng và hóa đơn liên quan đến khách hàng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Lấy danh sách mã hóa đơn liên quan đến khách hàng
                    List<string> maHDBList = new List<string>();
                    string getMaHDBQuery = "SELECT MaHDB FROM HoaDonBan WHERE MaKH = @maKH";
                    SqlCommand getMaHDBCmd = new SqlCommand(getMaHDBQuery, data.GetConnect());
                    getMaHDBCmd.Parameters.AddWithValue("@maKH", maKH);
                    using (SqlDataReader reader = getMaHDBCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            maHDBList.Add(reader["MaHDB"].ToString());
                        }
                    }

                    // Xóa chi tiết hóa đơn liên quan đến từng hóa đơn
                    string deleteCTHoaDonQuery = "DELETE FROM CTHoaDonBan WHERE MaHDB = @maHDB";
                    foreach (string maHDB in maHDBList)
                    {
                        SqlCommand deleteCTHoaDonCmd = new SqlCommand(deleteCTHoaDonQuery, data.GetConnect());
                        deleteCTHoaDonCmd.Parameters.AddWithValue("@maHDB", maHDB);
                        deleteCTHoaDonCmd.ExecuteNonQuery();
                    }

                    // Xóa hóa đơn liên kết
                    string deleteHoaDonQuery = "DELETE FROM HoaDonBan WHERE MaKH = @maKH";
                    SqlCommand deleteHoaDonCmd = new SqlCommand(deleteHoaDonQuery, data.GetConnect());
                    deleteHoaDonCmd.Parameters.AddWithValue("@maKH", maKH);
                    deleteHoaDonCmd.ExecuteNonQuery();

                    // Xóa khách hàng
                    string deleteKhachHangQuery = "DELETE FROM KhachHang WHERE MaKH = @maKH";
                    SqlCommand deleteKhachHangCmd = new SqlCommand(deleteKhachHangQuery, data.GetConnect());
                    deleteKhachHangCmd.Parameters.AddWithValue("@maKH", maKH);
                    deleteKhachHangCmd.ExecuteNonQuery();

                    MessageBox.Show("Đã xóa 1 khách hàng và " + count.ToString() + " hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadKH();
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Xóa khách hàng
                    string deleteKhachHangQuery = "DELETE FROM KhachHang WHERE MaKH = @maKH";
                    SqlCommand deleteKhachHangCmd = new SqlCommand(deleteKhachHangQuery, data.GetConnect());
                    deleteKhachHangCmd.Parameters.AddWithValue("@maKH", maKH);
                    deleteKhachHangCmd.ExecuteNonQuery();

                    MessageBox.Show("Đã xóa 1 khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadKH();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
}

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Xoa();
        }


        void Sua()
        {
            try
            {
                int vitri = dgvKH.CurrentCell.RowIndex;
                string maKH = dgvKH.Rows[vitri].Cells[0].Value.ToString();
                string tenKH = dgvKH.Rows[vitri].Cells[1].Value.ToString();
                string gioitinh = dgvKH.Rows[vitri].Cells[2].Value.ToString();
                string sdt = dgvKH.Rows[vitri].Cells[3].Value.ToString();
                string email = dgvKH.Rows[vitri].Cells[4].Value.ToString();
                string diemtl = dgvKH.Rows[vitri].Cells[5].Value.ToString();

                string update = @"update KhachHang set MaKH= '" + maKH + "', HoTenKH= N'" + tenKH + "', GioiTinh= N'" + gioitinh+
                    "', SDT= '" + sdt + "', Email= '" + email + "', DiemTichLuy= '" + diemtl +"' where MaKH= '" + maKH + "'";
                SqlCommand cmd = new SqlCommand(update, data.GetConnect());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã sửa thông tin khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadKH();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            Sua();
        }
    }
}
