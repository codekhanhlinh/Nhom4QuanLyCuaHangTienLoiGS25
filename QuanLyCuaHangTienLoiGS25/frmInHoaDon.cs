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
    public partial class frmInHoaDon : Form
    {
        public frmInHoaDon()
        {
            InitializeComponent();
        }
        public string MaHDB { get; set; }
        public string NgayLap { get; set; }
        public string TenNV { get; set; }
        public string MaKH { get; set; }
        public string TenKH { get; set; } // Property tùy chỉnh cho tên khách hàng
        public  string SoDT { get; set; } // Property tùy chỉnh cho số điện thoại khách hàng
        public string Tong { get; set; }
        public string LoaiTT { get; set; }
        public string TienKHDua { get; set; }
        public string TienTraKH { get; set; }

        public DataTable Data { get; set; }

        private void frmInHoaDon_Load(object sender, EventArgs e)
        {
            string thongtinSP = "";
            string thongtinSL = "";
            string thongtinDvt = "";
            string thongtinDG = "";
            string thongtinTT = "";

            foreach (DataRow row in Data.Rows)
            {
                string tenSP = row["TenSP"].ToString();
                string soluong = row["SoLuong"].ToString();
                string dvt = row["Dvt"].ToString();
                string dongia = row["DonGia"].ToString();
                string thanhtien = row["ThanhTien"].ToString();

                thongtinSP += $"{tenSP}\n\n";
                thongtinSL += $"{soluong}\n\n";
                thongtinDvt += $"{dvt}\n\n";
                thongtinDG += $"{dongia}\n\n";
                thongtinTT += $"{thanhtien}\n\n";

            }
            DateTime datetimeNL = DateTime.Parse(NgayLap);
            DateTime ThoiGianIn = DateTime.Now;
            

            lblMaHD.Text = MaHDB;
            lblNgayLap.Text = datetimeNL.ToString("dd/MM/yyyy");
            lblTGian.Text = ThoiGianIn.ToString("HH:mm:ss");
            lblTenSP.Text = thongtinSP.ToString();
            lblSL.Text = thongtinSL.ToString();
            lblDVT.Text=thongtinDvt.ToString();
            lblDonGia.Text = thongtinDG.ToString();
            lblThanhTien.Text = thongtinTT.ToString();
            lblTenNV.Text = TenNV;
            lblMaKH.Text = MaKH;
            lblTenKH.Text = TenKH;
            lblSDTKH.Text = SoDT;
            lblTongTien.Text = Tong;
            lblLoaiTT.Text = LoaiTT;
            lblTienDua.Text = TienKHDua;
            lblTienTra.Text = TienTraKH;
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }
    }
}