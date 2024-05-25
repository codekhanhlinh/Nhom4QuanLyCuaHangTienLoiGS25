namespace QuanLyCuaHangTienLoiGS25
{
    partial class frmReportPhieuNhap
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
            this.crystalReportPhieuNhap = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crystalReportPhieuNhap
            // 
            this.crystalReportPhieuNhap.ActiveViewIndex = -1;
            this.crystalReportPhieuNhap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportPhieuNhap.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportPhieuNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportPhieuNhap.Location = new System.Drawing.Point(0, 0);
            this.crystalReportPhieuNhap.Name = "crystalReportPhieuNhap";
            this.crystalReportPhieuNhap.Size = new System.Drawing.Size(800, 450);
            this.crystalReportPhieuNhap.TabIndex = 0;
            this.crystalReportPhieuNhap.Load += new System.EventHandler(this.crystalReportPhieuNhap_Load);
            // 
            // frmReportPhieuNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.crystalReportPhieuNhap);
            this.Name = "frmReportPhieuNhap";
            this.Text = "Báo cáo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportPhieuNhap;
    }
}