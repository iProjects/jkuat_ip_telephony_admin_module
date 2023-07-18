namespace jkuat_ip_telephony_ui
{
    partial class pdf_viewer_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pdf_viewer_form));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblstatusinfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.campusesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.departmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.extensionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_campuses = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_departments = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_extensions = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_exit = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControlReportsData = new System.Windows.Forms.TabControl();
            this.tabPage_campuses = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView_campuses = new System.Windows.Forms.DataGridView();
            this.Column_campus_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_campus_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage_departments = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView_departments = new System.Windows.Forms.DataGridView();
            this.Column_department_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_department_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage_extensions = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView_extensions = new System.Windows.Forms.DataGridView();
            this.tabPage_logs = new System.Windows.Forms.TabPage();
            this.txtlog = new System.Windows.Forms.RichTextBox();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.appNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.bindingSource_campuses = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource_departments = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource_extensions = new System.Windows.Forms.BindingSource(this.components);
            this.Column_extension_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_extension_number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_owner_assigned = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControlReportsData.SuspendLayout();
            this.tabPage_campuses.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_campuses)).BeginInit();
            this.tabPage_departments.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_departments)).BeginInit();
            this.tabPage_extensions.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_extensions)).BeginInit();
            this.tabPage_logs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_campuses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_departments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_extensions)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblstatusinfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 656);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1150, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblstatusinfo
            // 
            this.lblstatusinfo.Name = "lblstatusinfo";
            this.lblstatusinfo.Size = new System.Drawing.Size(72, 17);
            this.lblstatusinfo.Text = "lblstatusinfo";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.adminToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1150, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // adminToolStripMenuItem
            // 
            this.adminToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.campusesToolStripMenuItem,
            this.toolStripSeparator2,
            this.departmentsToolStripMenuItem,
            this.toolStripSeparator1,
            this.extensionsToolStripMenuItem});
            this.adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            this.adminToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.adminToolStripMenuItem.Text = "&Admin";
            // 
            // campusesToolStripMenuItem
            // 
            this.campusesToolStripMenuItem.Name = "campusesToolStripMenuItem";
            this.campusesToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.campusesToolStripMenuItem.Text = "&Campuses";
            this.campusesToolStripMenuItem.Click += new System.EventHandler(this.campusesToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(139, 6);
            // 
            // departmentsToolStripMenuItem
            // 
            this.departmentsToolStripMenuItem.Name = "departmentsToolStripMenuItem";
            this.departmentsToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.departmentsToolStripMenuItem.Text = "&Departments";
            this.departmentsToolStripMenuItem.Click += new System.EventHandler(this.departmentsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(139, 6);
            // 
            // extensionsToolStripMenuItem
            // 
            this.extensionsToolStripMenuItem.Name = "extensionsToolStripMenuItem";
            this.extensionsToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.extensionsToolStripMenuItem.Text = "&Extensions";
            this.extensionsToolStripMenuItem.Click += new System.EventHandler(this.extensionsToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_campuses,
            this.toolStripSeparator4,
            this.toolStripButton_departments,
            this.toolStripSeparator3,
            this.toolStripButton_extensions,
            this.toolStripSeparator5,
            this.toolStripButton_exit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1150, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
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
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControlReportsData);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.webBrowser);
            this.splitContainer1.Size = new System.Drawing.Size(1150, 607);
            this.splitContainer1.SplitterDistance = 331;
            this.splitContainer1.TabIndex = 3;
            // 
            // tabControlReportsData
            // 
            this.tabControlReportsData.Controls.Add(this.tabPage_campuses);
            this.tabControlReportsData.Controls.Add(this.tabPage_departments);
            this.tabControlReportsData.Controls.Add(this.tabPage_extensions);
            this.tabControlReportsData.Controls.Add(this.tabPage_logs);
            this.tabControlReportsData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlReportsData.Location = new System.Drawing.Point(0, 0);
            this.tabControlReportsData.Name = "tabControlReportsData";
            this.tabControlReportsData.SelectedIndex = 0;
            this.tabControlReportsData.Size = new System.Drawing.Size(331, 607);
            this.tabControlReportsData.TabIndex = 0;
            // 
            // tabPage_campuses
            // 
            this.tabPage_campuses.Controls.Add(this.groupBox1);
            this.tabPage_campuses.Location = new System.Drawing.Point(4, 22);
            this.tabPage_campuses.Name = "tabPage_campuses";
            this.tabPage_campuses.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_campuses.Size = new System.Drawing.Size(323, 581);
            this.tabPage_campuses.TabIndex = 0;
            this.tabPage_campuses.Text = "Campuses";
            this.tabPage_campuses.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView_campuses);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(317, 575);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // dataGridView_campuses
            // 
            this.dataGridView_campuses.AllowUserToAddRows = false;
            this.dataGridView_campuses.AllowUserToDeleteRows = false;
            this.dataGridView_campuses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_campuses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_campus_id,
            this.Column_campus_name});
            this.dataGridView_campuses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_campuses.Location = new System.Drawing.Point(3, 16);
            this.dataGridView_campuses.Name = "dataGridView_campuses";
            this.dataGridView_campuses.ReadOnly = true;
            this.dataGridView_campuses.Size = new System.Drawing.Size(311, 556);
            this.dataGridView_campuses.TabIndex = 0;
            this.dataGridView_campuses.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_campuses_DataError);
            // 
            // Column_campus_id
            // 
            this.Column_campus_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column_campus_id.DataPropertyName = "id";
            this.Column_campus_id.HeaderText = "#";
            this.Column_campus_id.Name = "Column_campus_id";
            this.Column_campus_id.ReadOnly = true;
            this.Column_campus_id.Width = 30;
            // 
            // Column_campus_name
            // 
            this.Column_campus_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_campus_name.DataPropertyName = "campus_name";
            this.Column_campus_name.HeaderText = "Name";
            this.Column_campus_name.Name = "Column_campus_name";
            this.Column_campus_name.ReadOnly = true;
            // 
            // tabPage_departments
            // 
            this.tabPage_departments.Controls.Add(this.groupBox2);
            this.tabPage_departments.Location = new System.Drawing.Point(4, 22);
            this.tabPage_departments.Name = "tabPage_departments";
            this.tabPage_departments.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_departments.Size = new System.Drawing.Size(323, 581);
            this.tabPage_departments.TabIndex = 1;
            this.tabPage_departments.Text = "Departments";
            this.tabPage_departments.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView_departments);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(317, 575);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // dataGridView_departments
            // 
            this.dataGridView_departments.AllowUserToAddRows = false;
            this.dataGridView_departments.AllowUserToDeleteRows = false;
            this.dataGridView_departments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_departments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_department_id,
            this.Column_department_name});
            this.dataGridView_departments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_departments.Location = new System.Drawing.Point(3, 16);
            this.dataGridView_departments.Name = "dataGridView_departments";
            this.dataGridView_departments.ReadOnly = true;
            this.dataGridView_departments.Size = new System.Drawing.Size(311, 556);
            this.dataGridView_departments.TabIndex = 1;
            this.dataGridView_departments.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_departments_DataError);
            // 
            // Column_department_id
            // 
            this.Column_department_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column_department_id.DataPropertyName = "id";
            this.Column_department_id.HeaderText = "#";
            this.Column_department_id.Name = "Column_department_id";
            this.Column_department_id.ReadOnly = true;
            this.Column_department_id.Width = 30;
            // 
            // Column_department_name
            // 
            this.Column_department_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_department_name.DataPropertyName = "department_name";
            this.Column_department_name.HeaderText = "Name";
            this.Column_department_name.Name = "Column_department_name";
            this.Column_department_name.ReadOnly = true;
            // 
            // tabPage_extensions
            // 
            this.tabPage_extensions.Controls.Add(this.groupBox3);
            this.tabPage_extensions.Location = new System.Drawing.Point(4, 22);
            this.tabPage_extensions.Name = "tabPage_extensions";
            this.tabPage_extensions.Size = new System.Drawing.Size(323, 581);
            this.tabPage_extensions.TabIndex = 2;
            this.tabPage_extensions.Text = "Extensions";
            this.tabPage_extensions.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView_extensions);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(323, 581);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // dataGridView_extensions
            // 
            this.dataGridView_extensions.AllowUserToAddRows = false;
            this.dataGridView_extensions.AllowUserToDeleteRows = false;
            this.dataGridView_extensions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_extensions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_extension_id,
            this.Column_extension_number,
            this.Column_owner_assigned});
            this.dataGridView_extensions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_extensions.Location = new System.Drawing.Point(3, 16);
            this.dataGridView_extensions.Name = "dataGridView_extensions";
            this.dataGridView_extensions.ReadOnly = true;
            this.dataGridView_extensions.Size = new System.Drawing.Size(317, 562);
            this.dataGridView_extensions.TabIndex = 1;
            this.dataGridView_extensions.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_extensions_DataError);
            // 
            // tabPage_logs
            // 
            this.tabPage_logs.Controls.Add(this.txtlog);
            this.tabPage_logs.Location = new System.Drawing.Point(4, 22);
            this.tabPage_logs.Name = "tabPage_logs";
            this.tabPage_logs.Size = new System.Drawing.Size(323, 581);
            this.tabPage_logs.TabIndex = 3;
            this.tabPage_logs.Text = "Logs";
            this.tabPage_logs.UseVisualStyleBackColor = true;
            // 
            // txtlog
            // 
            this.txtlog.BackColor = System.Drawing.Color.Black;
            this.txtlog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtlog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtlog.ForeColor = System.Drawing.Color.Lime;
            this.txtlog.Location = new System.Drawing.Point(0, 0);
            this.txtlog.Name = "txtlog";
            this.txtlog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtlog.Size = new System.Drawing.Size(323, 581);
            this.txtlog.TabIndex = 0;
            this.txtlog.Text = "";
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Size = new System.Drawing.Size(815, 607);
            this.webBrowser.TabIndex = 0;
            // 
            // appNotifyIcon
            // 
            this.appNotifyIcon.Text = "appNotifyIcon";
            this.appNotifyIcon.Visible = true;
            // 
            // Column_extension_id
            // 
            this.Column_extension_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column_extension_id.DataPropertyName = "id";
            this.Column_extension_id.HeaderText = "#";
            this.Column_extension_id.Name = "Column_extension_id";
            this.Column_extension_id.ReadOnly = true;
            this.Column_extension_id.Width = 50;
            // 
            // Column_extension_number
            // 
            this.Column_extension_number.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_extension_number.DataPropertyName = "extension_number";
            this.Column_extension_number.FillWeight = 59.88024F;
            this.Column_extension_number.HeaderText = "Extension Number";
            this.Column_extension_number.Name = "Column_extension_number";
            this.Column_extension_number.ReadOnly = true;
            // 
            // Column_owner_assigned
            // 
            this.Column_owner_assigned.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_owner_assigned.DataPropertyName = "owner_assigned";
            this.Column_owner_assigned.FillWeight = 140.1198F;
            this.Column_owner_assigned.HeaderText = "Owner Assigned";
            this.Column_owner_assigned.Name = "Column_owner_assigned";
            this.Column_owner_assigned.ReadOnly = true;
            // 
            // pdf_viewer_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 678);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "pdf_viewer_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pdf Viewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.pdf_viewer_form_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControlReportsData.ResumeLayout(false);
            this.tabPage_campuses.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_campuses)).EndInit();
            this.tabPage_departments.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_departments)).EndInit();
            this.tabPage_extensions.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_extensions)).EndInit();
            this.tabPage_logs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_campuses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_departments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_extensions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem campusesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem departmentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem extensionsToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_campuses;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButton_departments;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton_extensions;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox txtlog;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButton_exit;
        private System.Windows.Forms.ToolStripStatusLabel lblstatusinfo;
        private System.Windows.Forms.NotifyIcon appNotifyIcon;
        private System.Windows.Forms.TabControl tabControlReportsData;
        private System.Windows.Forms.TabPage tabPage_campuses;
        private System.Windows.Forms.DataGridView dataGridView_campuses;
        private System.Windows.Forms.TabPage tabPage_departments;
        private System.Windows.Forms.DataGridView dataGridView_departments;
        private System.Windows.Forms.TabPage tabPage_extensions;
        private System.Windows.Forms.DataGridView dataGridView_extensions;
        private System.Windows.Forms.TabPage tabPage_logs;
        private System.Windows.Forms.BindingSource bindingSource_campuses;
        private System.Windows.Forms.BindingSource bindingSource_departments;
        private System.Windows.Forms.BindingSource bindingSource_extensions;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_campus_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_campus_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_department_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_department_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_extension_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_extension_number;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_owner_assigned;

    }
}