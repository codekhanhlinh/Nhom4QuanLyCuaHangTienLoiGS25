namespace QuanLyCuaHangTienLoiGS25
{
    partial class frmReportDT
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
            this.crystalDT = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crystalDT
            // 
            this.crystalDT.ActiveViewIndex = -1;
            this.crystalDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalDT.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalDT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalDT.Location = new System.Drawing.Point(0, 0);
            this.crystalDT.Name = "crystalDT";
            this.crystalDT.Size = new System.Drawing.Size(800, 450);
            this.crystalDT.TabIndex = 0;
            // 
            // frmreportDT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.crystalDT);
            this.Name = "frmreportDT";
            this.Text = "FrmreportDT";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer crystalDT;
    }
}