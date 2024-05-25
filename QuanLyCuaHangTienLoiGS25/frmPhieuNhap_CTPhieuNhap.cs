using QuanLyCuaHangTienLoiGS25.DAO;
using QuanLyCuaHangTienLoiGS25.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangTienLoiGS25
{
    public partial class frmPhieuNhap_CTPhieuNhap : Form
    {
        public frmPhieuNhap_CTPhieuNhap()
        {
            InitializeComponent();
        }
        KetnoiSQL data=new KetnoiSQL();
        private void frmPhieuNhap_CTPhieuNhap_Load(object sender, EventArgs e)
        {
            LoadDataFromDatabase();
            cboMaNV.SelectedIndexChanged += new EventHandler(cboMaNV_SelectedIndexChanged);
            cboMaNCC.SelectedIndexChanged += new EventHandler(cboMaNCC_SelectedIndexChanged);
            cboMaSP.SelectedIndexChanged += new EventHandler(cboMaSP_SelectedIndexChanged);
            loadDSPN();
            loadDSCTPN();
            DieuChinhdgvCTPN();
        }

        //Tab 1: TẠO PHIẾU NHẬP
        private void LoadData()
        {
            string maPN = txtMaPN.Text;
            string maSP = cboMaSP.Text;
            string tenSP = txtTenSP.Text;
            string slnhap = txtSLNhap.Text;
            string gianhap = txtGiaNhap.Text;
            string thanhtien = txtThanhTien.Text;
            if (dtCTPN == null)
            {
                dtCTPN = new DataTable();
                dtCTPN.Columns.Add("MaPN", typeof(string));
                dtCTPN.Columns.Add("MaSP", typeof(string));
                dtCTPN.Columns.Add("TenSP", typeof(string));
                dtCTPN.Columns.Add("SLNhap", typeof(string));
                dtCTPN.Columns.Add("GiaNhap", typeof(string));
                dtCTPN.Columns.Add("ThanhTien", typeof(string));
            }
            DataTable newDataTable = dtCTPN.Clone(); // Tạo DataTable mới với cấu trúc giống DataTable cũ
            DataRow newRow = dtCTPN.NewRow();
            newRow["MaPN"] = maPN;
            newRow["MaSP"] = maSP;
            newRow["TenSP"] = tenSP;
            newRow["SLNhap"] = slnhap;
            newRow["GiaNhap"] = gianhap;
            newRow["ThanhTien"] = thanhtien;
            dtCTPN.Rows.Add(newRow);

            // Xóa các dòng trống
            ClearEmptyRows();

            dgvCTPN.DataSource = dtCTPN;

            // Thiết lập kích thước mong muốn cho các cột
            dgvCTPN.Columns["MaPN"].Width = 80;
            dgvCTPN.Columns["MaSP"].Width = 70;
            dgvCTPN.Columns["TenSP"].Width = 110;
            dgvCTPN.Columns["SLNhap"].Width = 85;
            dgvCTPN.Columns["GiaNhap"].Width = 90;
            dgvCTPN.Columns["ThanhTien"].Width = 90;

            // Đổi tên các cột trong DataGridView
            dgvCTPN.Columns["MaPN"].HeaderText = "Mã PN";
            dgvCTPN.Columns["MaSP"].HeaderText = "Mã SP";
            dgvCTPN.Columns["TenSP"].HeaderText = "Tên SP";
            dgvCTPN.Columns["SLNhap"].HeaderText = "SL Nhập";
            dgvCTPN.Columns["GiaNhap"].HeaderText = "Giá nhập";
            dgvCTPN.Columns["ThanhTien"].HeaderText = "Thành tiền";

            //Căn giữa tiêu đề mỗi cột
            foreach (DataGridViewColumn column in dgvCTPN.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void ClearEmptyRows()
        {
            for (int i = dgvCTPN.Rows.Count - 2; i >= 0; i--)
            {
                DataGridViewRow row = dgvCTPN.Rows[i];
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
                    dgvCTPN.Rows.Remove(row);
                }
            }
        }
   
        private Dictionary<string, string> maNhanVienTenNhanVien = new Dictionary<string, string>();
        private Dictionary<string, string> maNCCTenNCC = new Dictionary<string, string>();
        private Dictionary<string, string> maSanPhamTenSanPham = new Dictionary<string, string>();
        private DataTable dtNhanVien, dtSanPham, dtNhaCC;

        private void LoadDataFromDatabase()
        {
            dtNhanVien = data.thongtinNhanVien();
            dtNhaCC=data.thongtinNCC();
            dtSanPham = data.thongtinSanPham();
            // Đổ dữ liệu từ DataTable vào ComboBox và Dictionary
            foreach (DataRow row in dtNhanVien.Rows)
            {
                string maNV = row["MaNV"].ToString();
                string tenNV = row["HoTenNV"].ToString();

                cboMaNV.Items.Add(maNV);
                maNhanVienTenNhanVien.Add(maNV, tenNV);
            }

            foreach (DataRow row in dtSanPham.Rows)
            {
                string maSP = row["MaSP"].ToString();
                string tenSP = row["TenSP"].ToString();

                cboMaSP.Items.Add(maSP);
                maSanPhamTenSanPham.Add(maSP, tenSP);
            }

            foreach (DataRow row in dtNhaCC.Rows)
            {
                string maNCC = row["MaNCC"].ToString();
                string tenNCC = row["TenNCC"].ToString();

                cboMaNCC.Items.Add(maNCC);
                maNCCTenNCC.Add(maNCC, tenNCC);
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
                    txtTenNV.Text = tenNhanVien;
                }
            }
        }

        private void cboMaNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaNCC.SelectedIndex != -1)
            {
                string selectedValue = cboMaNCC.SelectedItem.ToString();

                if (maNCCTenNCC.ContainsKey(selectedValue))
                {
                    string tenNCC = maNCCTenNCC[selectedValue];
                    txtTenNCC.Text = tenNCC;
                }
            }
        }

        private void cboMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaSP.SelectedIndex != -1)
            {
                string selectedValue = cboMaSP.SelectedItem.ToString();
                if (maSanPhamTenSanPham.ContainsKey(selectedValue))
                {
                    string tenSanPham = maSanPhamTenSanPham[selectedValue];
                    txtTenSP.Text = tenSanPham;
                }
            }
        }

        void ThemPN()
        {
            try
            {
                string maPN = txtMaPN.Text;
                string ngaynhap = dtpNgayNhap.Text;
                string trigiaPN=txtTriGiaPN.Text;
                string maNCC = cboMaNCC.Text;
                string maNV=cboMaNV.Text;

                DateTime nNDate;
                if (!DateTime.TryParse(ngaynhap, out nNDate))
                {
                    MessageBox.Show("Ngày nhập không hợp lệ, vui lòng nhập theo định dạng yyyy-MM-dd.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ngaynhap = nNDate.ToString("yyyy-MM-dd");
                string insertPN = @"insert into PhieuNhap values('" + maPN + "','"+ngaynhap+"','"+trigiaPN+"','"+maNCC
                    +"','" + maNV + "')";
                SqlCommand cmd = new SqlCommand(insertPN, data.GetConnect());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã tạo mã số phiếu nhập mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnThemPN_Click(object sender, EventArgs e)
        {
            ThemPN();
            loadDSPN();
        }

        void LuuPN()
        {
            try
            {
                string maPN = txtMaPN.Text;
                string ngaynhap=dtpNgayNhap.Text;
                string trigiaPN = txtTriGiaPN.Text;
                string maNCC=cboMaNCC.Text;
                string maNV = cboMaNV.Text;

                DateTime nNDate;
                if (!DateTime.TryParse(ngaynhap, out nNDate))
                {
                    MessageBox.Show("Ngày nhập không hợp lệ, vui lòng nhập theo định dạng yyyy-MM-dd.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ngaynhap = nNDate.ToString("yyyy-MM-dd");

                string updatePN = @"update PhieuNhap set MaPN= '" + maPN + "', NgayNhap= '" + ngaynhap
                    + "',TriGiaPN= '" + trigiaPN + "',MaNCC= '" + maNCC +"',MaNV= '"+maNV+ "' where MaPN= '" + maPN + "'";
                SqlCommand cmd = new SqlCommand(updatePN, data.GetConnect());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã lưu phiếu nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void LamtrongdgvsaukhiLuu()
        {
            dtCTPN.Clear(); // Xóa dữ liệu trong DataTable dtCTPN
            dgvCTPN.DataSource = dtCTPN; // Gán lại dữ liệu trống cho DataGridView
        }
        private void btnLuuPN_Click(object sender, EventArgs e)
        {
            LuuPN();
            loadDSPN();
            LamtrongdgvsaukhiLuu();
        }

        private void txtSLNhap_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSLNhap.Text) && !string.IsNullOrEmpty(txtGiaNhap.Text))
            {
                double soluong = 0;
                double gianhap = 0;
                // Kiểm tra xem cả hai TextBox txtSL và txtDonGia có chứa giá trị không rỗng
                if (double.TryParse(txtSLNhap.Text, out soluong) && double.TryParse(txtGiaNhap.Text, out gianhap))
                {
                    double thanhTien = soluong * gianhap;
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

        private void dgvCTPN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvCTPN.Rows[e.RowIndex];

                txtMaPN.Text = selectedRow.Cells["MaPN"].Value.ToString();
                cboMaSP.Text = selectedRow.Cells["MaSP"].Value.ToString();
                txtSLNhap.Text = selectedRow.Cells["SLNhap"].Value.ToString();
                txtGiaNhap.Text = selectedRow.Cells["GiaNhap"].Value.ToString();
                txtThanhTien.Text = selectedRow.Cells["ThanhTien"].Value.ToString();
            }
        }
        void ThemCTPN()
        {
            try
            {
                string maPN = txtMaPN.Text;
                string maSP = cboMaSP.Text;
                string tenSP = txtTenSP.Text;
                string slnhap = txtSLNhap.Text;
                string gianhap = txtGiaNhap.Text;
                string thanhtien = txtThanhTien.Text;
                string insertCTPN = @"insert into CTPhieuNhap values('" + maPN + "','" + maSP + "',N'" + tenSP + "','" + slnhap + "','" + gianhap +
                    "','" + thanhtien + "')";
                SqlCommand cmd = new SqlCommand(insertCTPN, data.GetConnect());
                cmd.ExecuteNonQuery();
                LoadData();
                MessageBox.Show("Đã thêm 1 chi tiết phiếu nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TinhTriGia();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnThemCTPN_Click(object sender, EventArgs e)
        {
            ThemCTPN();
            loadDSCTPN();
            cboMaSP.Text = "";
            txtTenSP.Text = "";
            txtSLNhap.Text = "";
            txtGiaNhap.Text = "";
            txtThanhTien.Text = "0";
        }

        void SuaCTPB()
        {
            try
            {
                string maPN = txtMaPN.Text;
                string maSP = cboMaSP.Text;
                string tenSP = txtTenSP.Text;
                string slnhap = txtSLNhap.Text;
                string gianhap = txtGiaNhap.Text;
                string thanhtien = txtThanhTien.Text;

                string updateCTPN = @"update CTPhieuNhap set MaPN= '" + maPN + "', MaSP= '" + maSP
                   + "',TenSP= '" + tenSP + "',SLNhap= '" + slnhap + "',GiaNhap= '" + gianhap+"',ThanhTien= '"+thanhtien
                   + "' where MaPN= '" + maPN + "'";
                SqlCommand cmd = new SqlCommand(updateCTPN, data.GetConnect());
                cmd.ExecuteNonQuery();

                DataRow[] rows = dtCTPN.Select("MaPN = '" + maPN + "'");
                if (rows.Length > 0)
                {
                    DataRow row = rows[0];
                    row["MaSP"] = maSP;
                    row["SLNhap"] = slnhap;
                    row["GiaNhap"] = gianhap;
                    row["ThanhTien"] = thanhtien;
                }

                MessageBox.Show("Đã sửa thông tin chi tiết phiếu nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnSuaCTPN_Click(object sender, EventArgs e)
        {
            SuaCTPB();
            TinhTriGia();
            loadDSCTPN();
        }
        void XoaCTPN()
        {
            try
            {
                int vitri = dgvCTPN.CurrentCell.RowIndex;
                string maPN = dgvCTPN.Rows[vitri].Cells[0].Value.ToString();

                string deleteCTPN = @"delete from CTPhieuNhap where MaPN='" + maPN + "'";
                SqlCommand cmd = new SqlCommand(deleteCTPN, data.GetConnect());
                cmd.ExecuteNonQuery();
                dgvCTPN.Rows.RemoveAt(vitri);
                MessageBox.Show("Đã xóa 1 chi tiết phiếu nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Xóa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnXoaCTPN_Click(object sender, EventArgs e)
        {
            XoaCTPN();
            TinhTriGia();
            loadDSCTPN();
        }

        //private List<string> listMaPN = new List<string>();
        //private List<decimal> listThanhTien = new List<decimal>();
        private DataTable dtCTPN;

        private void TinhTriGia()
        {
            try
            {
                string maPN = txtMaPN.Text;

                // Tính trị giá mới của phiếu nhập
                decimal trigia = 0;
                DataRow[] rows = dtCTPN.Select("MaPN = '" + maPN + "'");
                foreach (DataRow row in rows)
                {
                    decimal thanhTien = Convert.ToDecimal(row["ThanhTien"]);
                    trigia += thanhTien;
                }

                txtTriGiaPN.Text = trigia.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        //private void TinhTongTienSauUpdate()
        //{
        //    try
        //    {
        //        string maPN = txtMaPN.Text;

        //        // Tính trị giá mới của phiếu nhập
        //        decimal trigia = 0;
        //        DataRow[] rows = dtCTPN.Select("MaPN = '" + maPN + "'");
        //        foreach (DataRow row in rows)
        //        {
        //            decimal thanhTien = Convert.ToDecimal(row["ThanhTien"]);
        //            trigia += thanhTien;
        //        }

        //        txtTriGiaPN.Text = trigia.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Thông báo");
        //    }
        //}

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
        private void txtTriGiaPN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTriGiaPN.Text))
                {
                    decimal trigia = decimal.Parse(txtTriGiaPN.Text);
                    string bangchu = ChuyenSoSangChu(trigia.ToString()); // Chuyển đổi số thành chuỗi
                    lblBangChu.Text = bangchu;
                }
            }
            catch (FormatException)
            {
                // Xử lý ngoại lệ định dạng không hợp lệ ở đây
                lblBangChu.Text = "Định dạng không hợp lệ";
            }
        }

        //Tab2: DANH SÁCH PHIẾU NHẬP

        private void loadDSPN()
        {
            string str = "select * from PhieuNhap order by cast(substring(MaPN,3, len(MaPN)) as int) asc";
            SqlDataAdapter adapter = new SqlDataAdapter(str, data.GetConnect());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvDSPN.DataSource = dt;
        }

        private void btnHienThiDS_Click(object sender, EventArgs e)
        {
            loadDSPN();
            loadDSCTPN();
        }

        void loadDSCTPN()
        {
            string str = "select * from CTPhieuNhap";
            SqlDataAdapter adapter = new SqlDataAdapter(str, data.GetConnect());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvDSCTPN.DataSource = dt;
        }

        private void dgvDSPN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDSPN.Rows[e.RowIndex];
                string maPN = row.Cells["MaPN"].Value.ToString();

                //Lọc dgvDSCTPN dựa trên maPN được chọn:
                (dgvDSCTPN.DataSource as DataTable).DefaultView.RowFilter = $"[MaPN] = '{maPN}'";
            }
        }

        void DieuChinhdgvCTPN()
        {
            DataGridView x = dgvDSCTPN;
            x.Columns[0].HeaderText = "Mã PN";
            x.Columns[1].HeaderText = "Mã SP";
            x.Columns[2].HeaderText = "Tên SP";
            x.Columns[3].HeaderText = "SL Nhập";
            x.Columns[4].HeaderText = "Giá nhập";
            x.Columns[5].HeaderText = "Thành tiền";

            {
                x.Columns[0].Width = 80;
                x.Columns[1].Width = 70;
                x.Columns[2].Width = 350;
                x.Columns[3].Width = 85;
                x.Columns[4].Width = 90;
                x.Columns[5].Width = 100;
            }
            x.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            x.DefaultCellStyle.SelectionForeColor = Color.Black;
            x.DefaultCellStyle.SelectionBackColor = Color.Orange;
            x.EnableHeadersVisualStyles = false; // Tắt việc sử dụng kiểu mặc định cho tiêu đề
            x.ColumnHeadersDefaultCellStyle.ForeColor = Color.DeepSkyBlue; // Thiết lập màu chữ cho tiêu đề cột
            x.GridColor = Color.LightSkyBlue;

            DataGridView y = dgvDSPN;
            y.Columns[0].HeaderText = "Mã PN";
            y.Columns[1].HeaderText = "Ngày nhập";
            y.Columns[2].HeaderText = "Trị giá PN";
            y.Columns[3].HeaderText = "Mã NCC";
            y.Columns[4].HeaderText = "Mã NV";

            {
                y.Columns[0].Width = 80;
                y.Columns[1].Width = 100;
                y.Columns[2].Width = 100;
                y.Columns[3].Width = 80;
                y.Columns[4].Width = 80;
            }
            y.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            y.DefaultCellStyle.SelectionForeColor = Color.Black;
            y.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            y.EnableHeadersVisualStyles = false; // Tắt việc sử dụng kiểu mặc định cho tiêu đề
            y.ColumnHeadersDefaultCellStyle.ForeColor = Color.DeepSkyBlue; // Thiết lập màu chữ cho tiêu đề cột
            y.GridColor = Color.LightSkyBlue;

        }
        private void btnTimKiemMaPN_Click(object sender, EventArgs e)
        {
            string maPN = txtTimMaPN.Text;
            string selectTim = "select * from PhieuNhap where MaPN=@MaPN";
            SqlDataAdapter adapter = new SqlDataAdapter(selectTim, data.GetConnect());
            adapter.SelectCommand.Parameters.AddWithValue("@MaPN", maPN);
            DataTable dt = new DataTable();
            dt.Clear();
            adapter.Fill(dt);
            dgvDSPN.DataSource = dt;

            string selectTimCT = "select * from CTPhieuNhap where MaPN=@MaPN";
            SqlDataAdapter adapterCT = new SqlDataAdapter(selectTimCT, data.GetConnect());
            adapterCT.SelectCommand.Parameters.AddWithValue("@MaPN", maPN);
            DataTable dtCT = new DataTable();
            dtCT.Clear();
            adapterCT.Fill(dtCT);
            dgvDSCTPN.DataSource = dtCT;
        }

        void XoaDS()
        {
            try
            {
                int vitri = dgvDSPN.CurrentRow.Index;
                string maPN = dgvDSPN.Rows[vitri].Cells["MaPN"].Value.ToString();

                // Xóa các bản ghi tương ứng từ bảng CTPhieuNhap trước
                string deleteCTPN = @"DELETE FROM CTPhieuNhap WHERE MaPN=@MaPN";
                SqlCommand cmdCTPN = new SqlCommand(deleteCTPN, data.GetConnect());
                cmdCTPN.Parameters.AddWithValue("@MaPN", maPN);
                cmdCTPN.ExecuteNonQuery();

                // Tiếp theo, xóa bản ghi đã chọn từ bảng PhieuNhap
                string deletePN = @"DELETE FROM PhieuNhap WHERE MaPN=@MaPN";
                SqlCommand cmdPN = new SqlCommand(deletePN, data.GetConnect());
                cmdPN.Parameters.AddWithValue("@MaPN", maPN);
                cmdPN.ExecuteNonQuery();

                // Làm mới DataGridViews
                loadDSPN();
                loadDSCTPN();
            }
            catch
            {
                MessageBox.Show("Xóa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnInPN_Click_1(object sender, EventArgs e)
        {
            string lenh = "Select * from PhieuNhap";
            SqlDataAdapter adapter = new SqlDataAdapter(lenh, data.GetConnect());
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            rptPhieuNhap rptPN = new rptPhieuNhap();
            rptPN.SetDataSource(dt);
            frmReportPhieuNhap frmInPN = new frmReportPhieuNhap();
            frmInPN.crystalReportPhieuNhap.ReportSource = rptPN;
            frmInPN.ShowDialog();
        }

        private void btnXoaPN_Click(object sender, EventArgs e)
        {
            DialogResult tb = MessageBox.Show("Nếu bạn xóa phiếu nhập này thì chi tiết phiếu nhập cũng sẽ bị xóa theo\nBạn có muốn xóa không?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (tb == DialogResult.Yes)
            {
                XoaDS();
                MessageBox.Show("Đã xóa phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Không thực hiện xóa
            }
        }
    }
}
