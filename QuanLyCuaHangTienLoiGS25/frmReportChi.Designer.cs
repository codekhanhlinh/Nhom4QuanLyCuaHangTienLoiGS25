namespace QuanLyCuaHangTienLoiGS25
{
    partial class frmReportChi
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
            this.crystalReportChi = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crystalReportChi
            // 
            this.crystalReportChi.ActiveViewIndex = -1;
            this.crystalReportChi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportChi.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportChi.Location = new System.Drawing.Point(0, 0);
            this.crystalReportChi.Name = "crystalReportChi";
            this.crystalReportChi.Size = new System.Drawing.Size(800, 450);
            this.crystalReportChi.TabIndex = 0;
            // 
            // frmReportChi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.crystalReportChi);
            this.Name = "frmReportChi";
            this.Text = "frmReportChi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportChi;
    }
}