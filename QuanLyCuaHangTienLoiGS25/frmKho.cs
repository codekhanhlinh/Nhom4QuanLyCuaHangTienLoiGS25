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
    public partial class frmKho : Form
    {
        private SqlDataAdapter adapter;
        private DataTable dt;
        public frmKho()
        {
            InitializeComponent();
        }
        KetnoiSQL data= new KetnoiSQL();
        private void frmKho_Load(object sender, EventArgs e)
        {
            loadSPKho();
            DieuChinhdgvSPKho();
        }

        void loadSPKho()
        {
            string str = "select * from Kho";
            adapter = new SqlDataAdapter(str, data.GetConnect());
            dt = new DataTable();
            adapter.Fill(dt);
            bdSKho.DataSource = dt;
            dgvSPKho.DataSource = bdSKho;
            bdNKho.BindingSource = bdSKho;
        }

        void DieuChinhdgvSPKho()
        {
            DataGridView x = dgvSPKho;
            x.Columns[0].HeaderText = "Mã SP";
            x.Columns[1].HeaderText = "Mã LSP";
            x.Columns[2].HeaderText = "Số lượng còn";
            x.Columns[3].HeaderText = "Tình trạng";



            {
                x.Columns[0].Width = 80;
                x.Columns[1].Width = 100;
                x.Columns[2].Width = 120;
                x.Columns[3].Width = 100;
            }
            x.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            x.DefaultCellStyle.SelectionForeColor = Color.Black;
            x.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            x.EnableHeadersVisualStyles = false; // Tắt việc sử dụng kiểu mặc định cho tiêu đề
            x.ColumnHeadersDefaultCellStyle.ForeColor = Color.DeepSkyBlue; // Thiết lập màu chữ cho tiêu đề cột
            x.GridColor = Color.LightSkyBlue;

            x.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            x.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            x.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            x.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            x.CellFormatting += dgvSPKho_CellFormatting;
        }
        void ThemSPKho()
        {
            try
            {
                int vitri = dgvSPKho.CurrentCell.RowIndex;
                string maSP = dgvSPKho.Rows[vitri].Cells[0].Value.ToString();
                string maLSP = dgvSPKho.Rows[vitri].Cells[1].Value.ToString();
                string slhienco = dgvSPKho.Rows[vitri].Cells[2].Value.ToString();
                string tinhtrang = dgvSPKho.Rows[vitri].Cells[3].Value.ToString();

                string insertKho = @"insert into Kho values('" + maSP + "','" + maLSP + "','" + slhienco
                    + "',N'" + tinhtrang + "')";
                SqlCommand cmd = new SqlCommand(insertKho, data.GetConnect());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã thêm 1 sản phẩm kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadSPKho();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnThemSPKho_Click(object sender, EventArgs e)
        {
            ThemSPKho();
        }

        void SuaSPKho()
        {
            try
            {
                int vitri = dgvSPKho.CurrentCell.RowIndex;
                string maSP = dgvSPKho.Rows[vitri].Cells[0].Value.ToString();
                string maLSP = dgvSPKho.Rows[vitri].Cells[1].Value.ToString();
                string slhienco = dgvSPKho.Rows[vitri].Cells[2].Value.ToString();
                string tinhtrang = dgvSPKho.Rows[vitri].Cells[3].Value.ToString();

                string updateKho = @"update Kho set MaSP= '" + maSP + "', MaLSP= N'" + maLSP
                    + "',SLHienCo= '" + slhienco + "',TinhTrang= N'"+tinhtrang+ "' where MaSP= '" + maSP + "'";
                SqlCommand cmd = new SqlCommand(updateKho, data.GetConnect());
                cmd.ExecuteNonQuery();

                MessageBox.Show("Đã sửa thông tin sản phẩm kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadSPKho();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnSuaSPKho_Click(object sender, EventArgs e)
        {
            SuaSPKho();
        }

        void XoaSPKho()
        {
            try
            {
                int vitri = dgvSPKho.CurrentCell.RowIndex;
                string maSP = dgvSPKho.Rows[vitri].Cells[0].Value.ToString();
                string maLSP = dgvSPKho.Rows[vitri].Cells[1].Value.ToString();
                string slhienco = dgvSPKho.Rows[vitri].Cells[2].Value.ToString();
                string tinhtrang = dgvSPKho.Rows[vitri].Cells[3].Value.ToString();

                string deleteKho = @"delete from Kho where MaSP='" + maSP + "'";
                SqlCommand cmd = new SqlCommand(deleteKho, data.GetConnect());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã xóa 1 sản phẩm kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadSPKho();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnXoaSPKho_Click(object sender, EventArgs e)
        {
            XoaSPKho();
        }

        private void btnHoanTacKho_Click(object sender, EventArgs e)
        {
            loadSPKho() ;
        }

        private void btnTimKiemSPKho_Click(object sender, EventArgs e)
        {
            string maSP=txtTimMaSP.Text;
            string timkiem= "select * from Kho where MaSP like'%'+@MaSP+'%'";
            adapter = new SqlDataAdapter(timkiem, data.GetConnect());
            adapter.SelectCommand.Parameters.AddWithValue("@MaSP", maSP);
            dt.Clear();
            adapter.Fill(dt);
            bdSKho.DataSource = dt;
            dgvSPKho.DataSource = bdSKho;
            bdNKho.BindingSource = bdSKho;
        }

        private void dgvSPKho_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3) // Kiểm tra chỉ số của cột "Tình trạng"
            {
                if (e.Value != null)
                {
                    string tinhTrang = e.Value.ToString();

                    // Đặt màu cho ô dựa vào giá trị của "Tình trạng"
                    if (tinhTrang == "Gần hết"||tinhTrang=="gần hết")
                    {
                        e.CellStyle.BackColor = Color.Yellow; // Màu nền ô là màu vàng
                    }
                    else if (tinhTrang == "Hết"||tinhTrang=="hết")
                    {
                        e.CellStyle.BackColor = Color.Red; // Màu nền ô là màu đỏ
                    }
                    else if (tinhTrang =="")
                    {
                        e.CellStyle.BackColor = Color.White; // Màu nền ô là màu xanh lá cây
                    }
                }
            }
        }

    }
}
