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
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
        }
        KetnoiSQL data= new KetnoiSQL();
        //private DataTable dt;
        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            cboGioiTinhNV.Items.Add("Nam");
            cboGioiTinhNV.Items.Add("Nữ");
            LoadNV();
            LoadDatafromDatabase();
            DieuChinhdgvNV();

        }
        void DieuChinhdgvNV()
        {
            DataGridView x = dgvNV;
            x.Columns[0].HeaderText = "Mã NV";
            x.Columns[1].HeaderText = "Họ tên";
            x.Columns[2].HeaderText = "Giới tính";
            x.Columns[3].HeaderText = "Năm sinh";
            x.Columns[4].HeaderText = "SĐT";
            x.Columns[5].HeaderText = "Email";
            x.Columns[6].HeaderText = "Địa chỉ";
            x.Columns[7].HeaderText = "Ngày vào làm";
            x.Columns[8].HeaderText = "Mã CV";

            {
                x.Columns[0].Width = 75;
                x.Columns[1].Width = 135;
                x.Columns[2].Width = 85;
                x.Columns[3].Width = 85;
                x.Columns[4].Width = 100;
                x.Columns[5].Width = 90;
                x.Columns[6].Width = 120;
                x.Columns[7].Width = 120;
                x.Columns[8].Width = 70;
            }
            //Căn giữa tiêu đề
            x.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //Căn giữa các nội dung trong từng cột
            x.Columns[0].DefaultCellStyle.Alignment=DataGridViewContentAlignment.MiddleCenter;
            x.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            x.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            x.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            x.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            x.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            x.DefaultCellStyle.SelectionForeColor = Color.Black;
            x.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            x.EnableHeadersVisualStyles = false; // Tắt việc sử dụng kiểu mặc định cho tiêu đề
            x.ColumnHeadersDefaultCellStyle.ForeColor = Color.DeepSkyBlue; // Thiết lập màu chữ cho tiêu đề cột
            x.GridColor = Color.LightSkyBlue;
        }

        private void LoadNV()
        {
            string str = "select * from NhanVien";
            SqlDataAdapter adapter = new SqlDataAdapter(str, data.GetConnect());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvNV.DataSource = dt;
        }

        private Dictionary<string, string> maCVtenCV = new Dictionary<string, string>();
        private DataTable dtChucVu;
        private void LoadDatafromDatabase()
        {
            dtChucVu = data.thongtinChucVu();
            // Đổ dữ liệu từ DataTable vào ComboBox và Dictionary
            foreach (DataRow row in dtChucVu.Rows)
            {
                string maCV = row["MaCV"].ToString();
                string tenCV = row["TenCV"].ToString();

                cboChucVu.Items.Add(maCV);
                maCVtenCV.Add(maCV, tenCV);
            }
        }

        private void cboChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboChucVu.SelectedIndex != -1)
            {
                string selectedValue = cboChucVu.SelectedItem.ToString();

                if (maCVtenCV.ContainsKey(selectedValue))
                {
                    string tenCV = maCVtenCV[selectedValue];
                    txtTenCV.Text = tenCV;
                }
            }
        }

        private void dgvNV_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                txtMaNV.Text = dgvNV.CurrentRow.Cells[0].Value.ToString();
                txtHoTenNV.Text = dgvNV.CurrentRow.Cells[1].Value.ToString();
                cboGioiTinhNV.Text = dgvNV.CurrentRow.Cells[2].Value.ToString();
                txtNamSinh.Text = dgvNV.CurrentRow.Cells[3].Value.ToString();
                txtSDTNV.Text = dgvNV.CurrentRow.Cells[4].Value.ToString();
                txtENV.Text= Text = dgvNV.CurrentRow.Cells[5].Value.ToString();
                txtDCNV.Text= Text = dgvNV.CurrentRow.Cells[6].Value.ToString();
                dtpNVL.Text= Text = dgvNV.CurrentRow.Cells[7].Value.ToString();
                cboChucVu.Text= Text = dgvNV.CurrentRow.Cells[8].Value.ToString();
            }
            catch { }
        }

        private void btnHoanTacDSNV_Click(object sender, EventArgs e)
        {
            LoadNV();
        }

        private void btnTimKiemNV_Click(object sender, EventArgs e)
        {
            string maNV=txtTimKiemMaNV.Text;
            string tenNV=txtTimKiemTenNV.Text;
            string timkiem = "select * from NhanVien where 1=1";
            if (chkMaNV.Checked)
            {
                timkiem += $"and MaNV Like '%{maNV}%'";
                txtTimKiemTenNV.Text = "";
            }
            else if (chkHoVaTenNV.Checked)
            {
                timkiem += $"and HoTenNV Like N'%{tenNV}%'";
                txtTimKiemMaNV.Text = "";
            }
            else if (chkMaNV.Checked && chkHoVaTenNV.Checked)
            {
                timkiem += $"and MaNV Like '%{maNV}%' and HoTenNV LIKE N'%{tenNV}%'";
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một lựa chọn để tìm kiếm!", "Thông báo");
            }
            SqlDataAdapter adapter= new SqlDataAdapter(timkiem, data.GetConnect());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvNV.DataSource = dt;
        }

        void Them()
        {
            try
            {
                int vitri = dgvNV.CurrentCell.RowIndex;
                string maNV = dgvNV.Rows[vitri].Cells[0].Value.ToString();
                string tenNV = dgvNV.Rows[vitri].Cells[1].Value.ToString();
                string gioitinh = dgvNV.Rows[vitri].Cells[2].Value.ToString();
                string namsinh = dgvNV.Rows[vitri].Cells[3].Value.ToString();
                string sdt = dgvNV.Rows[vitri].Cells[4].Value.ToString();
                string email = dgvNV.Rows[vitri].Cells[5].Value.ToString();
                string diachi= dgvNV.Rows[vitri].Cells[6].Value.ToString();
                string nvl= dgvNV.Rows[vitri].Cells[7].Value.ToString();
                string maCV= dgvNV.Rows[vitri].Cells[8].Value.ToString();

                // Kiểm tra và định dạng lại giá trị ngày tháng
                DateTime nvlDate;
                if (!DateTime.TryParse(nvl, out nvlDate))
                {
                    MessageBox.Show("Ngày vào làm không hợp lệ, vui lòng nhập theo định dạng yyyy-MM-dd.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                nvl = nvlDate.ToString("yyyy-MM-dd");

                string insert=@"insert into NhanVien values('" + maNV + "',N'" + tenNV + "',N'" + gioitinh 
                    + "','" + namsinh+"','"+sdt+"','"+email+"',N'"+diachi+"','"+nvl+"','"+maCV + "')";
                SqlCommand cmd= new SqlCommand(insert, data.GetConnect());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã thêm 1 nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNV();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnThemNV_Click(object sender, EventArgs e)
        {
            Them();
        }
        void Xoa()
        {
            try
            {
                int vitri = dgvNV.CurrentCell.RowIndex;
                string maNV = dgvNV.Rows[vitri].Cells[0].Value.ToString();
                string tenNV = dgvNV.Rows[vitri].Cells[1].Value.ToString();
                string gioitinh = dgvNV.Rows[vitri].Cells[2].Value.ToString();
                string namsinh = dgvNV.Rows[vitri].Cells[3].Value.ToString();
                string sdt = dgvNV.Rows[vitri].Cells[4].Value.ToString();
                string email = dgvNV.Rows[vitri].Cells[5].Value.ToString();
                string diachi = dgvNV.Rows[vitri].Cells[6].Value.ToString();
                string nvl = dgvNV.Rows[vitri].Cells[7].Value.ToString();
                string maCV = dgvNV.Rows[vitri].Cells[8].Value.ToString();

                // Kiểm tra số lượng hóa đơn liên kết với nhân viên
                string countQuery = "select count(*) from HoaDonBan where MaNV = @maNV";
                SqlCommand countCmd = new SqlCommand(countQuery, data.GetConnect());
                countCmd.Parameters.AddWithValue("@maNV", maNV);
                int count = (int)countCmd.ExecuteScalar();

                //Kiểm tra số lượng phiếu nhập với nhân viên
                string countQuery2 = "select count(*) from PhieuNhap where MaNV=@MaNV";
                SqlCommand countCmd1=new SqlCommand(countQuery2, data.GetConnect());
                countCmd1.Parameters.AddWithValue("@MaNV", maNV);
                int count1=(int)countCmd1.ExecuteScalar();

                if(count > 0 && count1 > 0)
                {
                    DialogResult result = MessageBox.Show("Nhân viên có mã " + (maNV.Substring(0, 6)) + " nằm trong " + count.ToString() + 
                        " hóa đơn và nằm trong "+count1.ToString()+" phiếu nhập." +
                        "\nBạn có muốn xóa nhân viên cùng hóa đơn và phiếu nhập liên quan đến nhân viên không?", 
                        "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        // Xóa chi tiết hóa đơn liên kết với từng hóa đơn
                        string deleteCTHoaDonQuery = "DELETE FROM CTHoaDonBan WHERE MaHDB IN (SELECT MaHDB FROM HoaDonBan WHERE MaNV = @maNV)";
                        SqlCommand deleteCTHoaDonCmd = new SqlCommand(deleteCTHoaDonQuery, data.GetConnect());
                        deleteCTHoaDonCmd.Parameters.AddWithValue("@maNV", maNV);
                        deleteCTHoaDonCmd.ExecuteNonQuery();

                        // Xóa hóa đơn liên kết
                        string deleteHoaDonQuery = "DELETE FROM HoaDonBan WHERE MaNV = @maNV";
                        SqlCommand deleteHoaDonCmd = new SqlCommand(deleteHoaDonQuery, data.GetConnect());
                        deleteHoaDonCmd.Parameters.AddWithValue("@maNV", maNV);
                        deleteHoaDonCmd.ExecuteNonQuery();

                        // Xóa chi tiết phiếu nhập liên kết với từng phiếu nhập
                        string deleteCTPhieuNhapQuery = "DELETE FROM CTPhieuNhap WHERE MaPN IN (SELECT MaPN FROM PhieuNhap WHERE MaNV = @maNV)";
                        SqlCommand deleteCTPhieuNhapCmd = new SqlCommand(deleteCTPhieuNhapQuery, data.GetConnect());
                        deleteCTPhieuNhapCmd.Parameters.AddWithValue("@maNV", maNV);
                        deleteCTPhieuNhapCmd.ExecuteNonQuery();

                        // Xóa phiếu nhập liên kết
                        string deletePhieuNhapQuery = "DELETE FROM PhieuNhap WHERE MaNV = @maNV";
                        SqlCommand deletePNCmd = new SqlCommand(deletePhieuNhapQuery, data.GetConnect());
                        deletePNCmd.Parameters.AddWithValue("@maNV", maNV);
                        deletePNCmd.ExecuteNonQuery();

                        // Xóa nhân viên
                        string deleteNVQuery = "DELETE FROM NhanVien WHERE MaNV = @maNV";
                        SqlCommand deleteNVCmd = new SqlCommand(deleteNVQuery, data.GetConnect());
                        deleteNVCmd.Parameters.AddWithValue("@maNV", maNV);
                        deleteNVCmd.ExecuteNonQuery();

                        MessageBox.Show("Đã xóa 1 nhân viên và " + count.ToString() + " hóa đơn và " + count1.ToString() + " phiếu nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadNV();
                    }
                }
                else if (count > 0)
                {
                    DialogResult result = MessageBox.Show("Nhân viên có mã " + (maNV.Substring(0, 6)) + " nằm trong " + count.ToString() + " hóa đơn.\nBạn có muốn xóa nhân viên và hóa đơn liên quan đến nhân viên không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Xóa chi tiết hóa đơn liên kết với từng hóa đơn
                        string deleteCTHoaDonQuery = "DELETE FROM CTHoaDonBan WHERE MaHDB IN (SELECT MaHDB FROM HoaDonBan WHERE MaNV = @maNV)";
                        SqlCommand deleteCTHoaDonCmd = new SqlCommand(deleteCTHoaDonQuery, data.GetConnect());
                        deleteCTHoaDonCmd.Parameters.AddWithValue("@maNV", maNV);
                        deleteCTHoaDonCmd.ExecuteNonQuery();

                        // Xóa hóa đơn liên kết
                        string deleteHoaDonQuery = "DELETE FROM HoaDonBan WHERE MaNV = @maNV";
                        SqlCommand deleteHoaDonCmd = new SqlCommand(deleteHoaDonQuery, data.GetConnect());
                        deleteHoaDonCmd.Parameters.AddWithValue("@maNV", maNV);
                        deleteHoaDonCmd.ExecuteNonQuery();

                        // Xóa nhân viên
                        string deleteNVQuery = "DELETE FROM NhanVien WHERE MaNV = @maNV";
                        SqlCommand deleteNVCmd = new SqlCommand(deleteNVQuery, data.GetConnect());
                        deleteNVCmd.Parameters.AddWithValue("@maNV", maNV);
                        deleteNVCmd.ExecuteNonQuery();

                        MessageBox.Show("Đã xóa 1 nhân viên và " + count.ToString() + " hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadNV();
                    }
                }
                else if (count1>0)
                {
                    DialogResult result = MessageBox.Show("Nhân viên có mã " + (maNV.Substring(0, 6)) + " nằm trong " + count1.ToString() + " phiếu nhập.\nBạn có muốn xóa nhân viên và phiếu nhập liên quan đến nhân viên không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Xóa chi tiết phiếu nhập liên kết với từng phiếu nhập
                        string deleteCTPhieuNhapQuery = "DELETE FROM CTPhieuNhap WHERE MaPN IN (SELECT MaPN FROM PhieuNhap WHERE MaNV = @maNV)";
                        SqlCommand deleteCTPhieuNhapCmd = new SqlCommand(deleteCTPhieuNhapQuery, data.GetConnect());
                        deleteCTPhieuNhapCmd.Parameters.AddWithValue("@maNV", maNV);
                        deleteCTPhieuNhapCmd.ExecuteNonQuery();

                        // Xóa phiếu nhập liên kết
                        string deletePhieuNhapQuery = "DELETE FROM PhieuNhap WHERE MaNV = @maNV";
                        SqlCommand deletePNCmd = new SqlCommand(deletePhieuNhapQuery, data.GetConnect());
                        deletePNCmd.Parameters.AddWithValue("@maNV", maNV);
                        deletePNCmd.ExecuteNonQuery();

                        // Xóa nhân viên
                        string deleteNVQuery = "DELETE FROM NhanVien WHERE MaNV = @maNV";
                        SqlCommand deleteNVCmd = new SqlCommand(deleteNVQuery, data.GetConnect());
                        deleteNVCmd.Parameters.AddWithValue("@maNV", maNV);
                        deleteNVCmd.ExecuteNonQuery();

                        MessageBox.Show("Đã xóa 1 nhân viên và " + count.ToString() + " phiếu nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadNV();
                    }
                }
                else 
                {
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Xóa nhân viên
                        string deleteNVQuery = "delete from NhanVien where MaNV = @maNV";
                        SqlCommand deleteNVCmd = new SqlCommand(deleteNVQuery, data.GetConnect());
                        deleteNVCmd.Parameters.AddWithValue("@maNV", maNV);
                        deleteNVCmd.ExecuteNonQuery();
                        MessageBox.Show("Đã xóa 1 nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadNV();
                    }
                }
              }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            Xoa();
        }
        void Sua()
        {
            try
            {
                int vitri = dgvNV.CurrentCell.RowIndex;
                string maNV = dgvNV.Rows[vitri].Cells[0].Value.ToString();
                string tenNV = dgvNV.Rows[vitri].Cells[1].Value.ToString();
                string gioitinh = dgvNV.Rows[vitri].Cells[2].Value.ToString();
                string namsinh = dgvNV.Rows[vitri].Cells[3].Value.ToString();
                string sdt = dgvNV.Rows[vitri].Cells[4].Value.ToString();
                string email = dgvNV.Rows[vitri].Cells[5].Value.ToString();
                string diachi = dgvNV.Rows[vitri].Cells[6].Value.ToString();
                string nvl = dgvNV.Rows[vitri].Cells[7].Value.ToString();
                string maCV = dgvNV.Rows[vitri].Cells[8].Value.ToString();

                // Kiểm tra và định dạng lại giá trị ngày tháng
                DateTime nvlDate;
                if (!DateTime.TryParse(nvl, out nvlDate))
                {
                    MessageBox.Show("Ngày vào làm không hợp lệ, vui lòng nhập theo định dạng yyyy-MM-dd.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                nvl = nvlDate.ToString("yyyy-MM-dd");

                string update = @"update NhanVien set MaNV= '" + maNV + "', HoTenNV= N'" + tenNV + "', GioiTinh= N'" + gioitinh + "', NamSinh= '" +namsinh+
                    "', SDT= '" + sdt + "', Email= '" + email + "', DiaChi= N'" + diachi + "', NgayVaoLam= '" + nvl 
                    + "', MaCV= '" + maCV + "' where MaNV= '"+ maNV +"'";
                SqlCommand cmd = new SqlCommand(update, data.GetConnect());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã sửa thông tin nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNV();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            Sua();
        }
    }
}
