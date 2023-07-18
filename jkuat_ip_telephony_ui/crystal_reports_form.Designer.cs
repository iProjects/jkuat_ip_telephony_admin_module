namespace jkuat_ip_telephony_ui
{
    partial class crystal_reports_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(crystal_reports_form));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.crystalReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.toolStripButton_campuses = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_departments = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_extensions = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_exit = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 628);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(969, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_campuses,
            this.toolStripSeparator1,
            this.toolStripButton_departments,
            this.toolStripSeparator2,
            this.toolStripButton_extensions,
            this.toolStripSeparator3,
            this.toolStripButton_exit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(969, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // crystalReportViewer
            // 
            this.crystalReportViewer.ActiveViewIndex = -1;
            this.crystalReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer.Location = new System.Drawing.Point(0, 25);
            this.crystalReportViewer.Name = "crystalReportViewer";
            this.crystalReportViewer.Size = new System.Drawing.Size(969, 603);
            this.crystalReportViewer.TabIndex = 2;
            // 
            // toolStripButton_campuses
            // 
            this.toolStripButton_campuses.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_campuses.Image")));
            this.toolStripButton_campuses.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_campuses.Name = "toolStripButton_campuses";
            this.toolStripButton_campuses.Size = new System.Drawing.Size(82, 22);
            this.toolStripButton_campuses.Text = "&Campuses";
            this.toolStripButton_campuses.Click += new System.EventHandler(this.toolStripButton_campuses_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_departments
            // 
            this.toolStripButton_departments.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_departments.Image")));
            this.toolStripButton_departments.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_departments.Name = "toolStripButton_departments";
            this.toolStripButton_departments.Size = new System.Drawing.Size(95, 22);
            this.toolStripButton_departments.Text = "&Departments";
            this.toolStripButton_departments.Click += new System.EventHandler(this.toolStripButton_departments_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_extensions
            // 
            this.toolStripButton_extensions.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_extensions.Image")));
            this.toolStripButton_extensions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_extensions.Name = "toolStripButton_extensions";
            this.toolStripButton_extensions.Size = new System.Drawing.Size(83, 22);
            this.toolStripButton_extensions.Text = "&Extensions";
            this.toolStripButton_extensions.Click += new System.EventHandler(this.toolStripButton_extensions_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_exit
            // 
            this.toolStripButton_exit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_exit.Image")));
            this.toolStripButton_exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_exit.Name = "toolStripButton_exit";
            this.toolStripButton_exit.Size = new System.Drawing.Size(46, 22);
            this.toolStripButton_exit.Text = "E&xit";
            this.toolStripButton_exit.Click += new System.EventHandler(this.toolStripButton_exit_Click);
            // 
            // crystal_reports_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 650);
            this.Controls.Add(this.crystalReportViewer);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "crystal_reports_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crystal Reports";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.crystal_reports_form_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer;
        private System.Windows.Forms.ToolStripButton toolStripButton_campuses;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton_departments;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton_extensions;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton_exit;
    }
}