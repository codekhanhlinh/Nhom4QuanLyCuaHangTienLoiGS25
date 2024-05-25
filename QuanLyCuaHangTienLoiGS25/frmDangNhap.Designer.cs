namespace QuanLyCuaHangTienLoiGS25
{
    partial class frmDangNhap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMK_DN = new System.Windows.Forms.TextBox();
            this.txtTenDN_DN = new System.Windows.Forms.TextBox();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnDN_DN = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblGS25 = new System.Windows.Forms.Label();
            this.tmrGS25chay = new System.Windows.Forms.Timer(this.components);
            this.picGoback = new System.Windows.Forms.PictureBox();
            this.picEyeClose = new System.Windows.Forms.PictureBox();
            this.picEyeOpen = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.grpDangNhap = new System.Windows.Forms.GroupBox();
            this.rdoKeToan = new System.Windows.Forms.RadioButton();
            this.rdoNhanVienKK = new System.Windows.Forms.RadioButton();
            this.rdoQuanLy = new System.Windows.Forms.RadioButton();
            this.rdoNhanVienBH = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.picGoback)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEyeClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEyeOpen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpDangNhap.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(116, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tên đăng nhập";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.SteelBlue;
            this.label2.Location = new System.Drawing.Point(116, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 28);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mật khẩu";
            // 
            // txtMK_DN
            // 
            this.txtMK_DN.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtMK_DN.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMK_DN.Location = new System.Drawing.Point(280, 157);
            this.txtMK_DN.Multiline = true;
            this.txtMK_DN.Name = "txtMK_DN";
            this.txtMK_DN.PasswordChar = '*';
            this.txtMK_DN.Size = new System.Drawing.Size(290, 41);
            this.txtMK_DN.TabIndex = 6;
            // 
            // txtTenDN_DN
            // 
            this.txtTenDN_DN.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtTenDN_DN.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenDN_DN.Location = new System.Drawing.Point(280, 94);
            this.txtTenDN_DN.Multiline = true;
            this.txtTenDN_DN.Name = "txtTenDN_DN";
            this.txtTenDN_DN.Size = new System.Drawing.Size(290, 41);
            this.txtTenDN_DN.TabIndex = 5;
            this.txtTenDN_DN.TextChanged += new System.EventHandler(this.txtTenDN_DN_TextChanged);
            // 
            // btnXoa
            // 
            this.btnXoa.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnXoa.BackColor = System.Drawing.Color.LightBlue;
            this.btnXoa.Font = new System.Drawing.Font("Bahnschrift SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnXoa.Location = new System.Drawing.Point(167, 249);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(138, 43);
            this.btnXoa.TabIndex = 7;
            this.btnXoa.Text = "Xóa hết";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnDN_DN
            // 
            this.btnDN_DN.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDN_DN.BackColor = System.Drawing.Color.LightBlue;
            this.btnDN_DN.Font = new System.Drawing.Font("Bahnschrift SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDN_DN.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnDN_DN.Location = new System.Drawing.Point(453, 249);
            this.btnDN_DN.Name = "btnDN_DN";
            this.btnDN_DN.Size = new System.Drawing.Size(138, 43);
            this.btnDN_DN.TabIndex = 8;
            this.btnDN_DN.Text = "Đăng nhập";
            this.btnDN_DN.UseVisualStyleBackColor = false;
            this.btnDN_DN.Click += new System.EventHandler(this.btnDN_DN_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel1.Location = new System.Drawing.Point(0, 612);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1029, 28);
            this.panel1.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel2.Location = new System.Drawing.Point(-1, -1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1030, 28);
            this.panel2.TabIndex = 10;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panel3.Location = new System.Drawing.Point(-1, 22);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1030, 10);
            this.panel3.TabIndex = 11;
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panel4.Location = new System.Drawing.Point(-1, 604);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1030, 10);
            this.panel4.TabIndex = 12;
            // 
            // lblGS25
            // 
            this.lblGS25.AutoSize = true;
            this.lblGS25.BackColor = System.Drawing.Color.Transparent;
            this.lblGS25.Font = new System.Drawing.Font("Bahnschrift Condensed", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGS25.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblGS25.Location = new System.Drawing.Point(875, 567);
            this.lblGS25.Name = "lblGS25";
            this.lblGS25.Size = new System.Drawing.Size(154, 34);
            this.lblGS25.TabIndex = 13;
            this.lblGS25.Text = "GS25 XIN CHÀO!";
            // 
            // tmrGS25chay
            // 
            this.tmrGS25chay.Tick += new System.EventHandler(this.tmrGS25chay_Tick);
            // 
            // picGoback
            // 
            this.picGoback.Image = global::QuanLyCuaHangTienLoiGS25.Properties.Resources.goback_50;
            this.picGoback.Location = new System.Drawing.Point(3, 33);
            this.picGoback.Name = "picGoback";
            this.picGoback.Size = new System.Drawing.Size(50, 50);
            this.picGoback.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picGoback.TabIndex = 16;
            this.picGoback.TabStop = false;
            this.picGoback.Click += new System.EventHandler(this.picGoback_Click);
            // 
            // picEyeClose
            // 
            this.picEyeClose.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.picEyeClose.Image = global::QuanLyCuaHangTienLoiGS25.Properties.Resources.closed_eye_30;
            this.picEyeClose.Location = new System.Drawing.Point(536, 164);
            this.picEyeClose.Name = "picEyeClose";
            this.picEyeClose.Size = new System.Drawing.Size(30, 30);
            this.picEyeClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picEyeClose.TabIndex = 15;
            this.picEyeClose.TabStop = false;
            this.picEyeClose.Click += new System.EventHandler(this.picEyeClose_Click);
            // 
            // picEyeOpen
            // 
            this.picEyeOpen.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.picEyeOpen.Image = global::QuanLyCuaHangTienLoiGS25.Properties.Resources.eye_30;
            this.picEyeOpen.Location = new System.Drawing.Point(536, 164);
            this.picEyeOpen.Name = "picEyeOpen";
            this.picEyeOpen.Size = new System.Drawing.Size(30, 30);
            this.picEyeOpen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picEyeOpen.TabIndex = 14;
            this.picEyeOpen.TabStop = false;
            this.picEyeOpen.Click += new System.EventHandler(this.picEyeOpen_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = global::QuanLyCuaHangTienLoiGS25.Properties.Resources.lggs25;
            this.pictureBox1.Location = new System.Drawing.Point(239, -3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(526, 277);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // grpDangNhap
            // 
            this.grpDangNhap.Controls.Add(this.rdoKeToan);
            this.grpDangNhap.Controls.Add(this.rdoNhanVienKK);
            this.grpDangNhap.Controls.Add(this.rdoQuanLy);
            this.grpDangNhap.Controls.Add(this.picEyeClose);
            this.grpDangNhap.Controls.Add(this.picEyeOpen);
            this.grpDangNhap.Controls.Add(this.rdoNhanVienBH);
            this.grpDangNhap.Controls.Add(this.label1);
            this.grpDangNhap.Controls.Add(this.txtTenDN_DN);
            this.grpDangNhap.Controls.Add(this.label2);
            this.grpDangNhap.Controls.Add(this.txtMK_DN);
            this.grpDangNhap.Controls.Add(this.btnXoa);
            this.grpDangNhap.Controls.Add(this.btnDN_DN);
            this.grpDangNhap.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDangNhap.ForeColor = System.Drawing.Color.SteelBlue;
            this.grpDangNhap.Location = new System.Drawing.Point(148, 215);
            this.grpDangNhap.Name = "grpDangNhap";
            this.grpDangNhap.Size = new System.Drawing.Size(731, 349);
            this.grpDangNhap.TabIndex = 17;
            this.grpDangNhap.TabStop = false;
            this.grpDangNhap.Text = "Đăng nhập với";
            // 
            // rdoKeToan
            // 
            this.rdoKeToan.AutoSize = true;
            this.rdoKeToan.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoKeToan.ForeColor = System.Drawing.Color.SteelBlue;
            this.rdoKeToan.Location = new System.Drawing.Point(480, 31);
            this.rdoKeToan.Name = "rdoKeToan";
            this.rdoKeToan.Size = new System.Drawing.Size(90, 28);
            this.rdoKeToan.TabIndex = 16;
            this.rdoKeToan.TabStop = true;
            this.rdoKeToan.Text = "Kế toán";
            this.rdoKeToan.UseVisualStyleBackColor = true;
            this.rdoKeToan.CheckedChanged += new System.EventHandler(this.rdoKeToan_CheckedChanged);
            // 
            // rdoNhanVienKK
            // 
            this.rdoNhanVienKK.AutoSize = true;
            this.rdoNhanVienKK.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoNhanVienKK.ForeColor = System.Drawing.Color.SteelBlue;
            this.rdoNhanVienKK.Location = new System.Drawing.Point(244, 31);
            this.rdoNhanVienKK.Name = "rdoNhanVienKK";
            this.rdoNhanVienKK.Size = new System.Drawing.Size(183, 28);
            this.rdoNhanVienKK.TabIndex = 2;
            this.rdoNhanVienKK.TabStop = true;
            this.rdoNhanVienKK.Text = "Nhân viên kiểm kho";
            this.rdoNhanVienKK.UseVisualStyleBackColor = true;
            this.rdoNhanVienKK.CheckedChanged += new System.EventHandler(this.rdoNhanVienKK_CheckedChanged);
            // 
            // rdoQuanLy
            // 
            this.rdoQuanLy.AutoSize = true;
            this.rdoQuanLy.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoQuanLy.ForeColor = System.Drawing.Color.SteelBlue;
            this.rdoQuanLy.Location = new System.Drawing.Point(623, 31);
            this.rdoQuanLy.Name = "rdoQuanLy";
            this.rdoQuanLy.Size = new System.Drawing.Size(90, 28);
            this.rdoQuanLy.TabIndex = 1;
            this.rdoQuanLy.TabStop = true;
            this.rdoQuanLy.Text = "Quản lý";
            this.rdoQuanLy.UseVisualStyleBackColor = true;
            this.rdoQuanLy.CheckedChanged += new System.EventHandler(this.rdoQuanLy_CheckedChanged);
            // 
            // rdoNhanVienBH
            // 
            this.rdoNhanVienBH.AutoSize = true;
            this.rdoNhanVienBH.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoNhanVienBH.ForeColor = System.Drawing.Color.SteelBlue;
            this.rdoNhanVienBH.Location = new System.Drawing.Point(7, 31);
            this.rdoNhanVienBH.Name = "rdoNhanVienBH";
            this.rdoNhanVienBH.Size = new System.Drawing.Size(184, 28);
            this.rdoNhanVienBH.TabIndex = 0;
            this.rdoNhanVienBH.TabStop = true;
            this.rdoNhanVienBH.Text = "Nhân viên bán hàng";
            this.rdoNhanVienBH.UseVisualStyleBackColor = true;
            this.rdoNhanVienBH.CheckedChanged += new System.EventHandler(this.rdoNhanVienBH_CheckedChanged);
            // 
            // frmDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1027, 637);
            this.Controls.Add(this.grpDangNhap);
            this.Controls.Add(this.picGoback);
            this.Controls.Add(this.lblGS25);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            this.Load += new System.EventHandler(this.frmDangNhap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picGoback)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEyeClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEyeOpen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpDangNhap.ResumeLayout(false);
            this.grpDangNhap.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMK_DN;
        private System.Windows.Forms.TextBox txtTenDN_DN;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnDN_DN;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblGS25;
        private System.Windows.Forms.Timer tmrGS25chay;
        private System.Windows.Forms.PictureBox picEyeOpen;
        private System.Windows.Forms.PictureBox picEyeClose;
        private System.Windows.Forms.PictureBox picGoback;
        private System.Windows.Forms.GroupBox grpDangNhap;
        private System.Windows.Forms.RadioButton rdoNhanVienKK;
        private System.Windows.Forms.RadioButton rdoQuanLy;
        private System.Windows.Forms.RadioButton rdoNhanVienBH;
        private System.Windows.Forms.RadioButton rdoKeToan;
    }
}