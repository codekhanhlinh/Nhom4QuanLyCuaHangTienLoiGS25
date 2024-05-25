using QuanLyCuaHangTienLoiGS25.DAO;
using QuanLyCuaHangTienLoiGS25.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangTienLoiGS25
{
    public partial class frmThongKe : Form
    {
        public frmThongKe()
        {
            InitializeComponent();
        }
        private SqlDataAdapter adapter;
        private DataTable dt;

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            loaddsThu();
            loaddsChi();
            ChinhDSThu();
            TinhTongCong();
            TinhTongChi();
            FillComboBox();
        }
        KetnoiSQL data= new KetnoiSQL();
        private void FillComboBox()
        {
            // Thêm dữ liệu vào ComboBox Ngày
            for (int i = 1; i <= 31; i++)
            {
                cboNgay.Items.Add(i);
                cboNgayChi.Items.Add(i);
            }

            // Thêm dữ liệu vào ComboBox Tháng
            for (int i = 1; i <= 12; i++)
            {
                cboThang.Items.Add(i);
                cboThangChi.Items.Add(i);
            }

            // Thêm dữ liệu vào ComboBox Năm
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear; i >= 2022; i--)
            {
                cboNam.Items.Add(i);
                cboNamChi.Items.Add(i);
            }
        }

        void ChinhDSThu()
        {
            DataGridView x = dgvThu;
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
                x.Columns[0].Width = 90;
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

            x.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            x.DefaultCellStyle.SelectionForeColor = Color.Black;
            x.DefaultCellStyle.SelectionBackColor = Color.Salmon;
            x.EnableHeadersVisualStyles = false; // Tắt việc sử dụng kiểu mặc định cho tiêu đề
            x.ColumnHeadersDefaultCellStyle.ForeColor = Color.OrangeRed; // Thiết lập màu chữ cho tiêu đề cột
            x.GridColor = Color.LightSalmon;

            DataGridView y = dgvChi;
            y.Columns[0].HeaderText = "Mã PN";
            y.Columns[1].HeaderText = "Ngày nhập";
            y.Columns[2].HeaderText = "Trị giá PN";
            y.Columns[3].HeaderText = "Mã NCC";
            y.Columns[4].HeaderText = "Mã NV";

            {
                y.Columns[0].Width = 80;
                y.Columns[1].Width = 120;
                y.Columns[2].Width = 100;
                y.Columns[3].Width = 90;
                y.Columns[4].Width = 80;
            }
            y.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            y.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            y.DefaultCellStyle.SelectionForeColor = Color.Black;
            y.DefaultCellStyle.SelectionBackColor = Color.Salmon;
            y.EnableHeadersVisualStyles = false; // Tắt việc sử dụng kiểu mặc định cho tiêu đề
            y.ColumnHeadersDefaultCellStyle.ForeColor = Color.OrangeRed; // Thiết lập màu chữ cho tiêu đề cột
            y.GridColor = Color.LightSalmon;

        }

        //Tab1: Doanh thu
        private void loaddsThu()
        {
            string str = "select * from HoaDonBan order by cast(substring(MaHDB, 4, len(MaHDB)) as int) asc";
            adapter = new SqlDataAdapter(str, data.GetConnect());
            dt = new DataTable();
            adapter.Fill(dt);
            dgvThu.DataSource = dt;
        }

        void TinhTongCong()
        {
            float tongCong = 0;

            // Lấy dữ liệu từ DataGridView
            foreach (DataGridViewRow row in dgvThu.Rows)
            {
                float tienHDB = 0;
                if (row.Cells[2].Value != null && float.TryParse(row.Cells[2].Value.ToString(), out tienHDB))
                {
                    tongCong += tienHDB;
                }
            }

            // Gán giá trị tổng tiền HDB vào TextBox txtTongCong
            txtTongCong.Text = tongCong.ToString("N0");
        }

        private void HienThiHoaDonTheoNgayThangNam(string ngay, string thang, string nam)
        {
            string select = "select * from HoaDonBan where day(NgayLap) = @Ngay and month(NgayLap) = @Thang and year(NgayLap) = @Nam";
            SqlDataAdapter adapter = new SqlDataAdapter(select, data.GetConnect());
            adapter.SelectCommand.Parameters.AddWithValue("@Ngay", ngay);
            adapter.SelectCommand.Parameters.AddWithValue("@Thang", thang);
            adapter.SelectCommand.Parameters.AddWithValue("@Nam", nam);
            dt.Clear();
            adapter.Fill(dt);
        }

        private void HienThiHoaDonTheoThangNam(string thang, string nam)
        {
            string select = "select * from HoaDonBan where month(NgayLap) = @Thang and year(NgayLap) = @Nam";
            SqlDataAdapter adapter = new SqlDataAdapter(select, data.GetConnect());
            adapter.SelectCommand.Parameters.AddWithValue("@Thang", thang);
            adapter.SelectCommand.Parameters.AddWithValue("@Nam", nam);
            dt.Clear();
            adapter.Fill(dt);
        }
        private void btnThongKeThu_Click(object sender, EventArgs e)
        {
            string ngay = cboNgay.Text;
            string thang = cboThang.Text;
            string nam = cboNam.Text;
            if (chkNgay.Checked && chkThang.Checked && chkNam.Checked)
            {
                DemSLvaTongTientheoNgayThangNam();
                HienThiHoaDonTheoNgayThangNam(ngay, thang, nam);
            }
            else if (chkThang.Checked && chkNam.Checked)
            {
                DemSLvaTongTientheoThangNam();
                HienThiHoaDonTheoThangNam(thang, nam);
            }
            else if (chkNam.Checked)
            {
                DemSLvaTongTientheoNam();
            }
            else
            {
                MessageBox.Show("Vui lòng check theo yêu cầu để thống kê:\n 1.Theo Ngày, Tháng, Năm\n 2.Theo Tháng và Năm\n 3.Theo Năm", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
     
        private void DemSLvaTongTientheoThangNam()
        {
            string thang = cboThang.Text;
            string nam = cboNam.Text;
            string dem = "SELECT COUNT(*) AS SoLuong, SUM(TongTienHDB) AS TongTien FROM HoaDonBan WHERE DATEPART(month, NgayLap) = @Thang and year(NgayLap)=@Nam";
            SqlCommand cmd = new SqlCommand(dem, data.GetConnect());
            cmd.Parameters.AddWithValue("@Thang", thang);
            cmd.Parameters.AddWithValue("@Nam", nam);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int soLuong = Convert.ToInt32(reader["SoLuong"]);
                decimal tongTien = Convert.ToDecimal(reader["TongTien"]);
                lblSL.Text = soLuong.ToString();
                lblTongCong.Text = tongTien.ToString("N0");
                txtTongCong.Text=tongTien.ToString("N0");
            }
            reader.Close();
        }

        private void DemSLvaTongTientheoNam()
        {
            string nam = cboNam.Text;
            string dem = "SELECT COUNT(*) AS SoLuong, SUM(TongTienHDB) AS TongTien FROM HoaDonBan WHERE DATEPART(year, NgayLap) = @Nam";
            SqlCommand cmd = new SqlCommand(dem, data.GetConnect());
            cmd.Parameters.AddWithValue("@Nam", nam);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int soLuong = Convert.ToInt32(reader["SoLuong"]);
                decimal tongTien = Convert.ToDecimal(reader["TongTien"]);
                lblSL.Text = soLuong.ToString();
                lblTongCong.Text = tongTien.ToString("N0");
                txtTongCong.Text = tongTien.ToString("N0");
            }
            reader.Close();
        }

        private void DemSLvaTongTientheoNgayThangNam()
        {
            string ngay = cboNgay.Text;
            string thang = cboThang.Text;
            string nam = cboNam.Text;

            string dem = "SELECT COUNT(*) AS SoLuong, SUM(TongTienHDB) AS TongTien FROM HoaDonBan WHERE DAY(NgayLap) = @Ngay AND MONTH(NgayLap) = @Thang AND YEAR(NgayLap) = @Nam";
            SqlCommand cmd = new SqlCommand(dem, data.GetConnect());
            cmd.Parameters.AddWithValue("@Ngay", ngay);
            cmd.Parameters.AddWithValue("@Thang", thang);
            cmd.Parameters.AddWithValue("@Nam", nam);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int soLuong = Convert.ToInt32(reader["SoLuong"]);
                decimal tongTien = Convert.ToDecimal(reader["TongTien"]);
                lblSL.Text = soLuong.ToString();
                lblTongCong.Text = tongTien.ToString("N0");
                txtTongCong.Text = tongTien.ToString("N0");
            }
            reader.Close();
        }
        private void btnHienThiThu_Click(object sender, EventArgs e)
        {
            loaddsThu();
        }
        //Tab2: Khoản chi
        void loaddsChi()
        {
            string str = "select * from PhieuNhap";
            SqlDataAdapter adapter = new SqlDataAdapter(str, data.GetConnect());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvChi.DataSource = dt;
        }

        void TinhTongChi()
        {
            float tongChi = 0;

            // Lấy dữ liệu từ DataGridView
            foreach (DataGridViewRow row in dgvChi.Rows)
            {
                float tienPN = 0;
                if (row.Cells[2].Value != null && float.TryParse(row.Cells[2].Value.ToString(), out tienPN))
                {
                    tongChi += tienPN;
                }
            }

            // Gán giá trị tổng tiền HDB vào TextBox txtTongCong
            txtTongChi.Text = tongChi.ToString("N0");
        }

        void DemTongChitheoThangNam()
        {
            string thang = cboThangChi.Text;
            string nam = cboNamChi.Text;
            string dem = "SELECT SUM(TriGiaPN) AS TongTien FROM PhieuNhap WHERE DATEPART(month, NgayNhap) = @Thang and year(NgayNhap)=@Nam";
            SqlCommand cmd = new SqlCommand(dem, data.GetConnect());
            cmd.Parameters.AddWithValue("@Thang", thang);
            cmd.Parameters.AddWithValue("@Nam", nam);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                decimal tongTien = Convert.ToDecimal(reader["TongTien"]);
                lblChi.Text = tongTien.ToString("N0");
                txtTongChi.Text = tongTien.ToString("N0");
            }
            reader.Close();
        }

        void DemTongChitheoNam()
        {
            string nam = cboNamChi.Text;
            string dem = "SELECT SUM(TriGiaPN) AS TongTien FROM PhieuNhap WHERE DATEPART(year, NgayNhap) = @Nam";
            SqlCommand cmd = new SqlCommand(dem, data.GetConnect());
            cmd.Parameters.AddWithValue("@Nam", nam);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                decimal tongTien = Convert.ToDecimal(reader["TongTien"]);
                lblChi.Text = tongTien.ToString("N0");
                txtTongChi.Text = tongTien.ToString("N0");
            }
            reader.Close();
        }

        void DemTongChitheoNgayThangNam()
        {
            string ngay = cboNgayChi.Text;
            string thang = cboThangChi.Text;
            string nam = cboNamChi.Text;

            string dem = "SELECT SUM(TriGiaPN) AS TongTien FROM PhieuNhap WHERE DATEPART(day, NgayNhap) = @Ngay " +
                "and DATEPART(month,NgayNhap) =@Thang and year(NgayNhap) = @Nam";

            SqlCommand cmd = new SqlCommand(dem, data.GetConnect());
            cmd.Parameters.AddWithValue("@Ngay", ngay);
            cmd.Parameters.AddWithValue("@Thang", thang);
            cmd.Parameters.AddWithValue("@Nam", nam);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                decimal tongTien = Convert.ToDecimal(reader["TongTien"]);
                lblChi.Text = tongTien.ToString("N0");
                txtTongChi.Text = tongTien.ToString("N0");
            }
            reader.Close();
        }

        private void HienThiPNTheoNgayThangNam(string ngaychi, string thangchi, string namchi)
        {
            string select = "select * from PhieuNhap where day(NgayNhap) = @Ngay and month(NgayNhap) = @Thang and year(NgayNhap) = @Nam";
            SqlDataAdapter adapter = new SqlDataAdapter(select, data.GetConnect());
            adapter.SelectCommand.Parameters.AddWithValue("@Ngay", ngaychi);
            adapter.SelectCommand.Parameters.AddWithValue("@Thang", thangchi);
            adapter.SelectCommand.Parameters.AddWithValue("@Nam", namchi);
            DataTable dt= new DataTable();
            dt.Clear();
            adapter.Fill(dt);
            dgvChi.DataSource = dt;
        }

        private void HienThiPNTheoThangNam(string thangchi, string namchi)
        {
            string select = "select * from PhieuNhap where month(NgayNhap) = @Thang and year(NgayNhap) = @Nam";
            SqlDataAdapter adapter = new SqlDataAdapter(select, data.GetConnect());
            adapter.SelectCommand.Parameters.AddWithValue("@Thang", thangchi);
            adapter.SelectCommand.Parameters.AddWithValue("@Nam", namchi);
            DataTable dt = new DataTable();
            dt.Clear();
            adapter.Fill(dt);
            dgvChi.DataSource = dt;
        }
        private void btnHienThiChi_Click(object sender, EventArgs e)
        {
            loaddsChi();
        }

        private void btnThongKeChi_Click_1(object sender, EventArgs e)
        {
            string ngaychi = cboNgayChi.Text;
            string thangchi = cboThangChi.Text;
            string namchi = cboNamChi.Text;
            if (chkNgayChi.Checked && chkThangChi.Checked && chkNamChi.Checked)
            {
                DemTongChitheoNgayThangNam();
                HienThiPNTheoNgayThangNam(ngaychi, thangchi, namchi);
            }
            else if (chkThangChi.Checked && chkNamChi.Checked)
            {
                DemTongChitheoThangNam();
                HienThiPNTheoThangNam(thangchi, namchi);
            }
            else if (chkNamChi.Checked)
            {
                DemTongChitheoNam();
            }
            else
            {
                MessageBox.Show("Vui lòng check theo yêu cầu để thống kê:\n 1.Theo Ngày, Tháng, Năm\n 2.Theo Tháng và Năm\n 3.Theo Năm", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXemBCDT_Click(object sender, EventArgs e)
        {
            string lenh = "Select * from HoaDonBan";
            SqlDataAdapter adapter = new SqlDataAdapter(lenh, data.GetConnect());
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            rptDoanhThu rptDT= new rptDoanhThu();
            rptDT.SetDataSource(dt);
            frmReportDT frmDT = new frmReportDT();
            frmDT.crystalDT.ReportSource = rptDT;
            frmDT.ShowDialog();
        }

        private void btnXemBCChi_Click(object sender, EventArgs e)
        {
            string lenh = "Select * from PhieuNhap";
            SqlDataAdapter adapter = new SqlDataAdapter(lenh, data.GetConnect());
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            rptKhoanChi rptChi = new rptKhoanChi();
            rptChi.SetDataSource(dt);
            frmReportChi frmRPChi = new frmReportChi();
            frmRPChi.crystalReportChi.ReportSource = rptChi;
            frmRPChi.ShowDialog();
        }
    }
}
