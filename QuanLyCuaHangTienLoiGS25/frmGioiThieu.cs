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
    public partial class frmGioiThieu : Form
    {
        public frmGioiThieu()
        {
            InitializeComponent();
        }

        private void frmGioiThieu_Load(object sender, EventArgs e)
        {
            lbl1.Text ="Là thương hiệu độc lập đầu tiên tại Hàn Quốc được thành lập vào năm 1990,\n" +
                       "GS25 tự hào là đại diện tiêu biểu cho hệ thống cửa hàng tiện lợi của Hàn Quốc.";
            lbl2.Text = "Với phương châm Lifestyle Platform, GS25 đã phát triển một nền tảng sống thường \n" +
                        "nhật, tối ưu những dịch vụ tiện ích, văn hóa ẩm thực mới gần gũi mang đến một trải \n" +
                        "nghiệm sống hiện đại và chất lượng cho khách hàng. Luôn giữ vững được vị trí dẫn \n" +
                        "đầu kể từ khi thành lập.";
            lbl3.Text = "Mang đến giá trị số 1 tại Việt Nam";
            lbl4.Text = "GS25 là một thương hiệu hàng đầu, đáp ứng nhu cầu khách hàng với các sản phẩm và dịch\n" +
                        "vụ cao cấp. Chúng tôi đạt được lợi nhuận cao và tăng trưởng liên tục bằng cách cân bằng\n" +
                       "việc phát triển và tạo ra giá trị. Môi trường làm việc của chúng tôi luôn vui vẻ và hứng\n" +
                        "thú, tạo điều kiện tốt cho sự thành công của mọi nhân viên.";
        }
    }
}
