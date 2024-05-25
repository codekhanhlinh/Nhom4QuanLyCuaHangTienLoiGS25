using QuanLyCuaHangTienLoiGS25.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyCuaHangTienLoiGS25
{
    public partial class frmHoaDonBan_CTHoaDonBan : Form
    {
        public frmHoaDonBan_CTHoaDonBan()
        {
            InitializeComponent();
            dgvCTHDB.CellClick += dgvCTHDB_CellClick;
        }
        KetnoiSQL data = new KetnoiSQL();
        private void frmHoaDonBan_Load(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnInHD.Enabled = true;
            btnLuu.Enabled=true;
            txtTenNhanVien.ReadOnly = true;
            txtSDTKH.ReadOnly = true;
            txtTenSP.ReadOnly = true;
            txtDonGia.ReadOnly = true;
            txtDVT.ReadOnly = true;
            txtThanhTien.ReadOnly = true;
            txtTongTienHDB.ReadOnly = true;
            txtTongTienHDB.Text = "0";
            LoadDataFromDatabase();
            cboMaNV.SelectedIndexChanged += new EventHandler(cboMaNV_SelectedIndexChanged);
            cboMaKH.SelectedIndexChanged += new EventHandler(cboMaKH_SelectedIndexChanged);
            cboMaSP.SelectedIndexChanged += new EventHandler(cboMaSP_SelectedIndexChanged);
            //LoadData();
            //Tab 2:
            loadds();
            loaddsCTHDB();
            ChinhDSHDB();
            ChinhDSCTHD();
        }
        private Dictionary<string, string> maNhanVienTenNhanVien = new Dictionary<string, string>();
        private Dictionary<string, string> maKhachHangTenKhachHang = new Dictionary<string, string>();
        private Dictionary<string, string> maSanPhamTenSanPham = new Dictionary<string, string>();
        private DataTable dtNhanVien, dtKhachHang, dtSanPham;
        private DataTable dtCTHDB;
        private void LoadDataFromDatabase()
        {
            dtNhanVien = data.thongtinNhanVien();
            dtKhachHang = data.thongtinKhachHang();
            dtSanPham = data.thongtinSanPham();

            // Đổ dữ liệu từ DataTable vào ComboBox và Dictionary
            foreach (DataRow row in dtNhanVien.Rows)
            {
                string maNV = row["MaNV"].ToString();
                string tenNV = row["HoTenNV"].ToString();

                cboMaNV.Items.Add(maNV);
                maNhanVienTenNhanVien.Add(maNV, tenNV);
            }

            foreach (DataRow row in dtKhachHang.Rows)
            {
                string maKH = row["MaKH"].ToString();
                string tenKH = row["HoTenKH"].ToString();
                string sdtKH = row["SDT"].ToString();

                cboMaKH.Items.Add(maKH);
                maKhachHangTenKhachHang.Add(maKH, tenKH);
                maKhachHangTenKhachHang.Add(maKH + "-sdt", sdtKH); // Thêm thông tin số điện thoại khách hàng với khóa khác nhau
            }

            foreach (DataRow row in dtSanPham.Rows)
            {
                string maSP = row["MaSP"].ToString();
                string tenSP = row["TenSP"].ToString();
                string dongiaSP = row["DonGia"].ToString();
                string dvtSP = row["Dvt"].ToString();

                cboMaSP.Items.Add(maSP);
                maSanPhamTenSanPham.Add(maSP, tenSP);
                maSanPhamTenSanPham.Add(maSP + "-dongia", dongiaSP);
                maSanPhamTenSanPham.Add(maSP + "-dvt", dvtSP);
            }
        }

        private void cboMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaKH.SelectedIndex != -1)
            {
                string selectedValue = cboMaKH.SelectedItem.ToString();
                if (maKhachHangTenKhachHang.ContainsKey(selectedValue))
                {
                    string tenKhachHang = maKhachHangTenKhachHang[selectedValue];
                    // Sử dụng khóa khác để lấy thông tin số điện thoại khách hàng
                    string sdtKhachHang = maKhachHangTenKhachHang[selectedValue + "-sdt"];
                    txtTenKH.Text = tenKhachHang;
                    txtSDTKH.Text = sdtKhachHang;
                }
            }
        }

        private void cboMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaNV.SelectedIndex != -1)
            {
                string selectedValue = cboMaNV.SelectedItem.ToString();

                if (maNhanVienTenNhanVien.ContainsKey(selectedValue))
                {
                    string tenNhanVien = maNhanVienTenNhanVien[selectedValue];
                    txtTenNhanVien.Text = tenNhanVien;
                }
            }
        }

        private void LoadData()
        {
            string maHDB = txtMaHDB.Text;
            string maSP = cboMaSP.Text;
            string tenSP=txtTenSP.Text;
            string soluong = txtSL.Text;
            string dvtSP = txtDVT.Text;
            string dongia = txtDonGia.Text;
            string thanhtien = txtThanhTien.Text;
            if (dtCTHDB == null)
            {
                dtCTHDB = new DataTable();
                dtCTHDB.Columns.Add("MaHDB", typeof(string));
                dtCTHDB.Columns.Add("MaSP", typeof(string));
                dtCTHDB.Columns.Add("TenSP",typeof(string));
                dtCTHDB.Columns.Add("SoLuong", typeof(string));
                dtCTHDB.Columns.Add("Dvt", typeof(string));
                dtCTHDB.Columns.Add("DonGia", typeof(string));
                dtCTHDB.Columns.Add("ThanhTien", typeof(string));
            }
            DataTable newDataTable = dtCTHDB.Clone(); // Tạo DataTable mới với cấu trúc giống DataTable cũ
            DataRow newRow = dtCTHDB.NewRow();
            newRow["MaHDB"] = maHDB;
            newRow["MaSP"] = maSP;
            newRow["TenSP"] = tenSP;
            newRow["SoLuong"] = soluong;
            newRow["Dvt"] = dvtSP;
            newRow["DonGia"] = dongia;
            newRow["ThanhTien"] = thanhtien;
            dtCTHDB.Rows.Add(newRow);

            // Xóa các dòng trống
            ClearEmptyRows();

            dgvCTHDB.DataSource = dtCTHDB;

            // Thiết lập kích thước mong muốn cho các cột
            dgvCTHDB.Columns["MaHDB"].Width = 80;
            dgvCTHDB.Columns["MaSP"].Width = 70;
            dgvCTHDB.Columns["TenSP"].Width = 290;
            dgvCTHDB.Columns["SoLuong"].Width = 85;
            dgvCTHDB.Columns["Dvt"].Width = 95;
            dgvCTHDB.Columns["DonGia"].Width = 90;
            dgvCTHDB.Columns["ThanhTien"].Width = 100;

            // Đổi tên các cột trong DataGridView
            dgvCTHDB.Columns["MaHDB"].HeaderText = "Mã HDB";
            dgvCTHDB.Columns["MaSP"].HeaderText = "Mã SP";
            dgvCTHDB.Columns["TenSP"].HeaderText = "Tên SP";
            dgvCTHDB.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvCTHDB.Columns["Dvt"].HeaderText = "Đơn vị tính";
            dgvCTHDB.Columns["DonGia"].HeaderText = "Đơn giá";
            dgvCTHDB.Columns["ThanhTien"].HeaderText = "Thành tiền";

            //Căn giữa tiêu đề mỗi cột
            foreach (DataGridViewColumn column in dgvCTHDB.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }


        private void ClearEmptyRows()
        {
            for (int i = dgvCTHDB.Rows.Count - 2; i >= 0; i--)
            {
                DataGridViewRow row = dgvCTHDB.Rows[i];
                bool isEmptyRow = true;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!string.IsNullOrEmpty(cell.Value?.ToString()))
                    {
                        isEmptyRow = false;
                        break;
                    }
                }
                if (isEmptyRow)
                {
                    dgvCTHDB.Rows.Remove(row);
                }
            }
        }

        private void cboKH_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaSP.SelectedIndex != -1)
            {
                string selectedValue = cboMaSP.SelectedItem.ToString();
                if (maSanPhamTenSanPham.ContainsKey(selectedValue))
                {
                    string tenSanPham = maSanPhamTenSanPham[selectedValue];
                    string dongiaSP = maSanPhamTenSanPham[selectedValue + "-dongia"];
                    string dvtSP = maSanPhamTenSanPham[selectedValue + "-dvt"];
                    txtTenSP.Text = tenSanPham;
                    txtDonGia.Text = dongiaSP;
                    txtDVT.Text = dvtSP;
                }
            }
        }

        private void txtSL_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSL.Text) && !string.IsNullOrEmpty(txtDonGia.Text))
            {
                double soluong = 0;
                double dongia = 0;
                // Kiểm tra xem cả hai TextBox txtSL và txtDonGia có chứa giá trị không rỗng
                if (double.TryParse(txtSL.Text, out soluong) && double.TryParse(txtDonGia.Text, out dongia))
                {
                    double thanhTien = soluong * dongia;
                    txtThanhTien.Text = thanhTien.ToString();
                }
                else
                {
                    // Xử lý khi người dùng nhập không hợp lệ
                    txtThanhTien.Text = "0";
                }
            }
            else
            {
                txtThanhTien.Text = "0";
            }
        }
        private string maHDB;
        //private List<string> listMaHDB = new List<string>();
        //private List<decimal> listThanhTien = new List<decimal>();

        private void ThemHDB()
        {
            string maHDB = txtMaHDB.Text;
            string ngaylap = dtpNgayLap.Text;
            string maNV = cboMaNV.Text;
            string maKH = cboMaKH.Text;
            string tongtienHDB = txtTongTienHDB.Text;
            string loaithanhtoan = cboLoaiTT.Text;
            string tienkhachdua = txtTienKhachDua.Text;
            string tientrakhach = txtTienTraLaiKH.Text;

            DateTime nLDate;
            if (!DateTime.TryParse(ngaylap, out nLDate))
            {
                MessageBox.Show("Ngày lập không hợp lệ, vui lòng nhập theo định dạng yyyy-MM-dd.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ngaylap = nLDate.ToString("yyyy-MM-dd");

            string insertHDB = @"insert into HoaDonBan values('" + maHDB + "','" + ngaylap + "','" + tongtienHDB + "',N'" + loaithanhtoan +
                "',N'" + tienkhachdua + "',N'" + tientrakhach + "','" + maKH + "','" + maNV + "')";
            SqlCommand cmd = new SqlCommand(insertHDB, data.GetConnect());
            cmd.ExecuteNonQuery();
            MessageBox.Show("Đã thêm 1 hóa đơn bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnThem_Click_1(object sender, EventArgs e)
        {
            maHDB = txtMaHDB.Text;
            while (dgvCTHDB.Rows.Count > 1)
            {
                dgvCTHDB.Rows.RemoveAt(0);
            }
            ThemHDB();
            loadds();
            txtTongTienHDB.Text = "0";
        }

        private void ThemCTHDB()
        {
            try
            {
                string maHDB = txtMaHDB.Text;
                string maSP = cboMaSP.Text;
                string tenSP = txtTenSP.Text;
                string soluong = txtSL.Text;
                string dvtSP = txtDVT.Text;
                string dongia = txtDonGia.Text;
                string thanhtien = txtThanhTien.Text;

                // Kiểm tra thông tin đầy đủ
                if (string.IsNullOrEmpty(maHDB) || string.IsNullOrEmpty(maSP) || string.IsNullOrEmpty(soluong) || string.IsNullOrEmpty(dvtSP) 
                    || string.IsNullOrEmpty(dongia) || string.IsNullOrEmpty(thanhtien))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string insertCTHDB = @"insert into CTHoaDonBan values('" + maHDB + "','" + maSP + "','" + soluong +
                        "',N'" + dvtSP + "','" + dongia + "','" + thanhtien + "')";
                    SqlCommand cmd = new SqlCommand(insertCTHDB, data.GetConnect());
                    cmd.ExecuteNonQuery();
                    LoadData();
                    MessageBox.Show("Đã thêm 1 chi tiết hóa đơn bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TinhTongTien();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnThemCTHD_Click(object sender, EventArgs e)
        {
            ThemCTHDB();
            loaddsCTHDB();
            cboMaSP.Text = "";
            txtTenSP.Text = "";
            txtSL.Text = "";
            txtDVT.Text = "";
            txtDonGia.Text = "";
            txtThanhTien.Text = "";
           
        }

        private void txtTongTienHDB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTongTienHDB.Text))
                {
                    decimal tongtien = decimal.Parse(txtTongTienHDB.Text);
                    string bangchu = ChuyenSoSangChu(tongtien.ToString()); // Chuyển đổi số thành chuỗi
                    lblBangChu.Text = bangchu;
                }
            }
            catch (FormatException)
            {
                // Xử lý ngoại lệ định dạng không hợp lệ ở đây
                lblBangChu.Text = "Định dạng không hợp lệ";
            }
        }
        private void TinhTongTien()
        {
            //string maHDB = txtMaHDB.Text;
            //string thanhtien = txtThanhTien.Text;
            //// Cập nhật giá trị vào danh sách tạm thời
            //listMaHDB.Add(maHDB);
            //listThanhTien.Add(decimal.Parse(thanhtien));

            //// Tính tổng tiền
            //decimal tongtienHDB = listThanhTien.Sum();

            //txtTongTienHDB.Text = tongtienHDB.ToString();

            try
            {
                string maHDB = txtMaHDB.Text;

                // Tính tổng tiền mới của hóa đơn bán
                decimal tongTien = 0;
                DataRow[] rows = dtCTHDB.Select("MaHDB = '" + maHDB + "'");
                foreach (DataRow row in rows)
                {
                    decimal thanhTien = Convert.ToDecimal(row["ThanhTien"]);
                    tongTien += thanhTien;
                }

                txtTongTienHDB.Text = tongTien.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        //void TinhTongTienSauUpdate()
        //{
        //    try
        //    {
        //        string maHDB = txtMaHDB.Text;

        //        // Tính tổng tiền mới của hóa đơn bán
        //        decimal tongTien = 0;
        //        DataRow[] rows = dtCTHDB.Select("MaHDB = '" + maHDB + "'");
        //        foreach (DataRow row in rows)
        //        {
        //            decimal thanhTien = Convert.ToDecimal(row["ThanhTien"]);
        //            tongTien += thanhTien;
        //        }

        //        txtTongTienHDB.Text = tongTien.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Thông báo");
        //    }
        //}
        private void txtTienKhachDua_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTienKhachDua.Text) && !string.IsNullOrEmpty(txtTongTienHDB.Text))
            {
                double tienkhachdua = 0, tongtienHDB = 0;
                if (double.TryParse(txtTienKhachDua.Text, out tienkhachdua) && double.TryParse(txtTongTienHDB.Text, out tongtienHDB))
                {
                    double tientrakhach = tienkhachdua - tongtienHDB;
                    txtTienTraLaiKH.Text = tientrakhach.ToString();
                }
                else
                {
                    txtTienTraLaiKH.Text = "0";
                }
            }
            else
            {
                txtTienTraLaiKH.Text = "0";
            }
        }

        private void grpThongTinChung_Enter(object sender, EventArgs e)
        {
            string[] dsloaiTT = { "Tiền mặt", "Ví ZaloPay", "Ví MoMo", "Thanh toán ngân hàng" };
            foreach (string item in dsloaiTT)
            {
                cboLoaiTT.Items.Add(item);
            }
            for (int i = 0; i < cboLoaiTT.Items.Count - 1; i++)
            {
                for (int j = cboLoaiTT.Items.Count - 1; j > i; j--)
                {
                    if (cboLoaiTT.Items[j].ToString() == cboLoaiTT.Items[i].ToString())
                    {
                        cboLoaiTT.Items.RemoveAt(j);
                    }
                }
            }
        }

        private void LamtrongdgvsaukhiLuu()
        {
            dtCTHDB.Clear(); // Xóa dữ liệu trong DataTable dtCTHDB
            dgvCTHDB.DataSource = dtCTHDB; // Gán lại dữ liệu trống cho DataGridView
        }
        void LuuThongTinHDB()
        {
            try
            {
                string maHDB = txtMaHDB.Text;
                string ngaylap = dtpNgayLap.Text;
                string maNV = cboMaNV.Text;
                string maKH = cboMaKH.Text;
                string tongtienHDB = txtTongTienHDB.Text;
                string loaithanhtoan = cboLoaiTT.Text;
                string tienkhachdua = txtTienKhachDua.Text;
                string tientrakhach = txtTienTraLaiKH.Text;

                DateTime nLDate;
                if (!DateTime.TryParse(ngaylap, out nLDate))
                {
                    MessageBox.Show("Ngày vào làm không hợp lệ, vui lòng nhập theo định dạng yyyy-MM-dd.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ngaylap = nLDate.ToString("yyyy-MM-dd");
                string saveHDB = @"update HoaDonBan set NgayLap= '" + ngaylap + "',TongTienHDB= '" + tongtienHDB + "',LoaiTT= N'" + loaithanhtoan + "',TienKhachDua= '" +
                    tienkhachdua + "',TienTraLaiKH= '" + tientrakhach + "'where MaHDB= '" + maHDB + "'";
                SqlCommand cmd = new SqlCommand(saveHDB, data.GetConnect());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã lưu thông tin hóa đơn bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamtrongdgvsaukhiLuu(); // Làm trống thông tin trong DataGridView dgvCTHDB
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            LuuThongTinHDB();
            loadds();
            txtMaHDB.Text = "";
            cboMaNV.Text = "";
            txtTenNhanVien.Text = "";
            cboMaKH.Text = "";
            txtTenKH.Text = "";
            txtSDTKH.Text = "";
            cboLoaiTT.Text = "";
            txtTienKhachDua.Text = "0";
            txtTienTraLaiKH.Text = "0";
        }
        void SuaThongTinCTHDB()
        {
            try
            {
                string maHDB = txtMaHDB.Text;
                string maSP = cboMaSP.Text;
                string soluong = txtSL.Text;
                string dvtSP = txtDVT.Text;
                string dongia = txtDonGia.Text;
                string thanhtien = txtThanhTien.Text;

                string updateCTDHDB=@"update CTHoaDonBan set SoLuong= '" + soluong + "',DonGia= '" + dongia + "',ThanhTien= '"+
                    thanhtien + "'where MaHDB= '" + maHDB + "'and MaSP= '" + maSP + "'";
                SqlCommand cmd = new SqlCommand(updateCTDHDB, data.GetConnect());
                cmd.ExecuteNonQuery();

                DataRow[] rows = dtCTHDB.Select("MaHDB = '" + maHDB + "'");
                if (rows.Length > 0)
                {
                    DataRow row = rows[0];
                    row["MaSP"] = maSP;
                    row["SoLuong"] = soluong;
                    row["Dvt"] = dvtSP;
                    row["DonGia"] = dongia;
                    row["ThanhTien"] = thanhtien;
                }

                // Cập nhật lại dgvCTHDB
                dgvCTHDB.Refresh();

                MessageBox.Show("Đã sửa thông tin chi tiết hóa đơn bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }
        private void btnSuaCTHD_Click(object sender, EventArgs e)
        {
            SuaThongTinCTHDB();
            TinhTongTien();
            loaddsCTHDB();
        }
        private void dgvCTHDB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvCTHDB.Rows[e.RowIndex];

                txtMaHDB.Text = selectedRow.Cells["MaHDB"].Value.ToString();
                cboMaSP.Text = selectedRow.Cells["MaSP"].Value.ToString();
                txtSL.Text = selectedRow.Cells["SoLuong"].Value.ToString();
                txtDVT.Text = selectedRow.Cells["Dvt"].Value.ToString();
                txtDonGia.Text = selectedRow.Cells["DonGia"].Value.ToString();
                txtThanhTien.Text = selectedRow.Cells["ThanhTien"].Value.ToString();
            }
        }
        void XoaCTHD()
        {
            try
            { 
                int vitri = dgvCTHDB.CurrentCell.RowIndex;
                string maHDB = dgvCTHDB.Rows[vitri].Cells[0].Value.ToString();
                string maSP= dgvCTHDB.Rows[vitri].Cells[1].Value.ToString();

                string deleteCTHDB = @"delete from CTHoaDonBan where MaHDB='" + maHDB + "' and MaSP='" + maSP + "'";
                SqlCommand cmd = new SqlCommand(deleteCTHDB, data.GetConnect());
                cmd.ExecuteNonQuery();
                dgvCTHDB.Rows.RemoveAt(vitri);

            }
            catch
            {
                MessageBox.Show("Xóa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void btnXoaCTHD_Click(object sender, EventArgs e)
        {
            XoaCTHD();
            TinhTongTien();
            MessageBox.Show("Đã xóa 1 chi tiết hóa đơn bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loaddsCTHDB();
            cboMaSP.Text = "";
            txtTenSP.Text = "";
            txtSL.Text = "";
            txtDVT.Text = "";
            txtDonGia.Text = "";
            txtThanhTien.Text = "0";
            
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            try
            {                   
                frmInHoaDon frmInHD = new frmInHoaDon();
                if (dgvCTHDB.Rows.Count > 0)
                {
                    frmInHD.Data = (DataTable)dgvCTHDB.DataSource;
                    string maHDB = txtMaHDB.Text;
                    string ngaylap = dtpNgayLap.Text;
                    string tenNV = txtTenNhanVien.Text;
                    string maKH = cboMaKH.Text;
                    string tenKH = txtTenKH.Text;
                    string sdtKH = txtSDTKH.Text;
                    string tongtien = txtTongTienHDB.Text;
                    string loaiTT = cboLoaiTT.Text;
                    string tienKHdua = txtTienKhachDua.Text;
                    string tientraKH = txtTienTraLaiKH.Text;

                    frmInHD.MaHDB = maHDB;
                    frmInHD.NgayLap = ngaylap;
                    frmInHD.TenNV = tenNV;
                    frmInHD.MaKH = maKH;
                    frmInHD.TenKH = tenKH;
                    frmInHD.SoDT = sdtKH;
                    frmInHD.Tong = tongtien;
                    frmInHD.LoaiTT = loaiTT;
                    frmInHD.TienKHDua = tienKHdua;
                    frmInHD.TienTraKH = tientraKH;

                    frmInHD.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để in hóa đơn!", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {

        }
        void Click_gdvCTHDB()
        {
            if(dgvCTHDB.SelectedRows.Count >0)
            {
                int vitri = dgvCTHDB.SelectedRows[0].Index;
                string maHDB = dgvCTHDB.Rows[vitri].Cells[1].Value.ToString();
                string maSP = dgvCTHDB.Rows[vitri].Cells[2].Value.ToString();
                string soluong = dgvCTHDB.Rows[vitri].Cells[3].Value.ToString();
                string dvtSP = dgvCTHDB.Rows[vitri].Cells[4].Value.ToString();
                string dongia = dgvCTHDB.Rows[vitri].Cells[5].Value.ToString();
                string thanhtien = dgvCTHDB.Rows[vitri].Cells[6].Value.ToString();

                txtMaHDB.Text = maHDB;
                cboMaSP.Text = maSP;
                txtSL.Text= soluong;
                txtDVT.Text= dvtSP;
                txtDonGia.Text= dongia;
                txtThanhTien.Text= thanhtien;
            }
            else
            {
                MessageBox.Show("", "Thông báo");
            }
        }

        public static string ChuyenSoSangChu(string sNumber)
        {
            int mLen, mDigit;
            //mLen: lưu trữ độ dài của chuỗi đầu vào (sNumber) trừ đi 1
            //mDigit sẽ lưu trữ giá trị của chữ số tại vị trí hiện tại trong chuỗi số
            string mTemp = ""; // Chuỗi chuyển đổi từ số -> chữ
            string[] mNumText; // mảng chuỗi, chứa các từ tương ứng với các số từ 0 -> 9

            sNumber = sNumber.Replace(",", ""); /* Xóa các dấu "," nếu có. Sử dụng Replace để thay tất cả
            dấu , thành bằng chuỗi rỗng */
            mNumText = "không;một;hai;ba;bốn;năm;sáu;bảy;tám;chín".Split(';'); //Split(';'): ký tự phân tách giữa các từ

            mLen = sNumber.Length - 1;  // Trừ 1 vì thứ tự đi từ 0

            for (int i = 0; i <= mLen; i++)
            {
                /*lấy giá trị của chữ số tại vị trí hiện tại (được chỉ định bởi biến i) 
                 * trong chuỗi sNumber và chuyển đổi thành số nguyên*/
                mDigit = Convert.ToInt32(sNumber.Substring(i, 1));


                mTemp = mTemp + " " + mNumText[mDigit];

                if (mLen == i)  // Chữ số cuối cùng không cần xét tiếp
                    break;

                switch ((mLen - i) % 9)
                {
                    case 0:
                        mTemp = mTemp + " tỷ";
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        break;
                    case 6:
                        mTemp = mTemp + " triệu";
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        break;
                    case 3:
                        mTemp = mTemp + " nghìn";
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        break;
                    default:
                        switch ((mLen - i) % 3)
                        {
                            case 2:
                                mTemp = mTemp + " trăm";
                                break;
                            case 1:
                                mTemp = mTemp + " mươi";
                                break;
                        }
                        break;
                }
            }

            //  Loại bỏ trường hợp chữ số hàng chục và hàng đơn vị đều là số 0,
            //  ví dụ: "không mươi không" sẽ được loại bỏ.
            mTemp = mTemp.Replace("không mươi không ", "");
            mTemp = mTemp.Replace("không mươi không", "");

            // Thay thế chuỗi "không mươi" bằng "linh".
            // Ví dụ: "không mươi năm" sẽ được thay thế thành "linh năm".
            mTemp = mTemp.Replace("không mươi ", "linh ");

            // Thay thế chuỗi "mươi không" bằng "mươi". 
            // Ví dụ: "ba mươi không" sẽ được thay thế thành "ba mươi".
            mTemp = mTemp.Replace("mươi không", "mươi");

            // Thay thế chuỗi "một mươi" bằng "mười".
            // Ví dụ: "một mươi hai" sẽ được thay thế thành "mười hai".
            mTemp = mTemp.Replace("một mươi", "mười");

            // Thay thế chuỗi "mươi bốn" bằng "mươi tư".
            // Ví dụ: "hai mươi bốn" sẽ được thay thế thành "hai mươi tư".
            mTemp = mTemp.Replace("mươi bốn", "mươi tư");

            // Thay thế chuỗi "linh bốn" bằng "linh tư".
            // Ví dụ: "mười linh bốn" sẽ được thay thế thành "mười linh tư".
            mTemp = mTemp.Replace("linh bốn", "linh tư");

            // Thay thế chuỗi "mươi năm" bằng "mươi lăm".
            // Ví dụ: "ba mươi năm" sẽ được thay thế thành "ba mươi lăm".
            mTemp = mTemp.Replace("mươi năm", "mươi lăm");

            // Thay thế chuỗi "mươi một" bằng "mươi mốt".
            // Ví dụ: "tám mươi một" sẽ được thay thế thành "tám mươi mốt".
            mTemp = mTemp.Replace("mươi một", "mươi mốt");

            // Thay thế chuỗi "mười năm" bằng "mười lăm".
            // Ví dụ: "mười năm" sẽ được thay thế thành "mười lăm".
            mTemp = mTemp.Replace("mười năm", "mười lăm");

            // Loại bỏ các ký tự trắng (space) ở đầu và cuối chuỗi.
            mTemp = mTemp.Trim();

            // Viết hoa ký tự đầu tiên
            mTemp = mTemp.Substring(0, 1).ToUpper() + mTemp.Substring(1) + " đồng";

            return mTemp;
        }
        //tab 2:
        private void loadds()
        {
            string str = "select * from HoaDonBan order by cast(substring(MaHDB, 4, len(MaHDB)) as int) asc";
            SqlDataAdapter adapter = new SqlDataAdapter(str, data.GetConnect());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvDSHDB.DataSource = dt;
        }

        private void loaddsCTHDB()
        {
            string str = "select*from CTHoaDonBan";
            SqlDataAdapter adapter = new SqlDataAdapter(str, data.GetConnect());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvDSCTHDB.DataSource = dt;
        }

        private void dgvDSHDB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDSHDB.Rows[e.RowIndex];
                string maHDB = row.Cells["MaHDB"].Value.ToString();

                //Lọc dgvDSCTHDB dựa trên maHDB được chọn
                (dgvDSCTHDB.DataSource as DataTable).DefaultView.RowFilter = $"[MaHDB] = '{maHDB}'";
            }
        }

        void ChinhDSHDB()
        {
            DataGridView x = dgvDSHDB;
            //Gán tiêu đề cho cột bên Danh sách Hóa Đơn Bán
            x.Columns[0].HeaderText = "Mã HDB";
            x.Columns[1].HeaderText = "Ngày lập";
            x.Columns[2].HeaderText = "Tổng tiền HDB";
            x.Columns[3].HeaderText = "Loại thanh toán";
            x.Columns[4].HeaderText = "Tiền khách đưa";
            x.Columns[5].HeaderText = "Tiền trả khách";
            x.Columns[6].HeaderText = "Mã KH";
            x.Columns[7].HeaderText = "Mã NV";

            //Điều chỉnh độ rộng cho từng cột
            {
                x.Columns[0].Width = 80;
                x.Columns[1].Width = 100;
                x.Columns[2].Width = 120;
                x.Columns[3].Width = 140;
                x.Columns[4].Width = 140;
                x.Columns[5].Width = 140;
                x.Columns[6].Width = 80;
                x.Columns[7].Width = 80;
            }
            //Căn giữa tiêu đề
            x.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Căn giữa nội dung các cột
            x.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            x.DefaultCellStyle.SelectionForeColor = Color.Black;
            x.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            x.EnableHeadersVisualStyles = false; // Tắt việc sử dụng kiểu mặc định cho tiêu đề
            x.ColumnHeadersDefaultCellStyle.ForeColor = Color.DeepSkyBlue; // Thiết lập màu chữ cho tiêu đề cột
            x.GridColor = Color.LightSkyBlue;
        }

        void ChinhDSCTHD()
        {
            DataGridView y = dgvDSCTHDB;
            //Gán tiêu đề cho cột bên Danh sách CT Hóa đơn bán
            y.Columns[0].HeaderText = "Mã HDB";
            y.Columns[1].HeaderText = "Mã sản phẩm";
            y.Columns[2].HeaderText = "Số lượng";
            y.Columns[3].HeaderText = "Đơn vị tính";
            y.Columns[4].HeaderText = "Đơn giá";
            y.Columns[5].HeaderText = "Thành tiền";


            //Điều chỉnh độ rộng cho từng cột
            {
                y.Columns[0].Width = 80;
                y.Columns[1].Width = 150;
                y.Columns[2].Width = 100;
                y.Columns[3].Width = 100;
                y.Columns[4].Width = 100;
                y.Columns[5].Width = 100;
            }

            //Căn giữa tiêu đề
            y.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Căn giữa nội dung các cột
            y.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            y.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            y.DefaultCellStyle.SelectionForeColor = Color.Black;
            y.DefaultCellStyle.SelectionBackColor = Color.Orange;
            y.EnableHeadersVisualStyles = false; // Tắt việc sử dụng kiểu mặc định cho tiêu đề
            y.ColumnHeadersDefaultCellStyle.ForeColor = Color.Orange; // Thiết lập màu chữ cho tiêu đề cột
            y.GridColor = Color.LightSkyBlue;
        }

        private void btnTimKiemMaHD_Click(object sender, EventArgs e)
        {
            string maHDB=txtTimMaHDB.Text;
            string selectTim = "select * from HoaDonBan where MaHDB like'%'+@MaHDB+'%'";
            SqlDataAdapter adapter=new SqlDataAdapter(selectTim, data.GetConnect());
            adapter.SelectCommand.Parameters.AddWithValue("@MaHDB", maHDB);
            DataTable dt=new DataTable();
            dt.Clear();
            adapter.Fill(dt);
            dgvDSHDB.DataSource = dt;

            string selectTimCT = "select * from CTHoaDonBan where MaHDB like'%'+@MaHDB+'%'";
            SqlDataAdapter adapterCT = new SqlDataAdapter(selectTimCT, data.GetConnect());
            adapterCT.SelectCommand.Parameters.AddWithValue("@MaHDB", maHDB);
            DataTable dtCT = new DataTable();
            dtCT.Clear();
            adapterCT.Fill(dtCT);
            dgvDSCTHDB.DataSource = dtCT;
        }

        private void btnHienThiDS_Click(object sender, EventArgs e)
        {
            loadds();
            loaddsCTHDB();
        }

        void XoaTTDS()
        {
            try
            {
                int vitri = dgvDSHDB.CurrentRow.Index;
                string maHDB = dgvDSHDB.Rows[vitri].Cells["MaHDB"].Value.ToString();

                // Xóa các bản ghi tương ứng từ bảng CTHoaDonBan trước
                string deleteCTHD = @"DELETE FROM CTHoaDonBan WHERE MaHDB=@MaHDB";
                SqlCommand cmdCTHD = new SqlCommand(deleteCTHD, data.GetConnect());
                cmdCTHD.Parameters.AddWithValue("@MaHDB", maHDB);
                cmdCTHD.ExecuteNonQuery();

                // Tiếp theo, xóa bản ghi đã chọn từ bảng HoaDonBan
                string deleteHDB = @"DELETE FROM HoaDonBan WHERE MaHDB=@MaHDB";
                SqlCommand cmdHDB = new SqlCommand(deleteHDB, data.GetConnect());
                cmdHDB.Parameters.AddWithValue("@MaHDB", maHDB);
                cmdHDB.ExecuteNonQuery();

                // Làm mới DataGridViews
                loadds();
                loaddsCTHDB();
            }
            catch
            {
                MessageBox.Show("Xóa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnXoaDS_Click(object sender, EventArgs e)
        {
            DialogResult tb = MessageBox.Show("Nếu bạn xóa hóa đơn này thì chi tiết hóa đơn cũng sẽ bị xóa theo\nBạn có muốn xóa không?", 
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (tb == DialogResult.Yes)
            {
                XoaTTDS();
                MessageBox.Show("Đã xóa hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Không thực hiện xóa
            }
        }
    }
}
