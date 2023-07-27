namespace jkuat_ip_telephony_ui
{
    partial class departments_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(departments_form));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.dataGridView_departments = new System.Windows.Forms.DataGridView();
            this.Column_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_department_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_created_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btndownloadtemplate = new System.Windows.Forms.Button();
            this.btnuploadexcel = new System.Windows.Forms.Button();
            this.btndownloadexcel = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.bindingSource_departments = new System.Windows.Forms.BindingSource(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_departments)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_departments)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.webBrowser);
            this.groupBox2.Controls.Add(this.dataGridView_departments);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(547, 517);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(33, 281);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(192, 79);
            this.webBrowser.TabIndex = 2;
            this.webBrowser.Visible = false;
            // 
            // dataGridView_departments
            // 
            this.dataGridView_departments.AllowUserToAddRows = false;
            this.dataGridView_departments.AllowUserToDeleteRows = false;
            this.dataGridView_departments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_departments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_id,
            this.Column_department_name,
            this.Column_status,
            this.Column_created_date});
            this.dataGridView_departments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_departments.Location = new System.Drawing.Point(3, 16);
            this.dataGridView_departments.Name = "dataGridView_departments";
            this.dataGridView_departments.ReadOnly = true;
            this.dataGridView_departments.Size = new System.Drawing.Size(541, 498);
            this.dataGridView_departments.TabIndex = 1;
            // 
            // Column_id
            // 
            this.Column_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column_id.DataPropertyName = "id";
            this.Column_id.HeaderText = "#";
            this.Column_id.Name = "Column_id";
            this.Column_id.ReadOnly = true;
            this.Column_id.Width = 30;
            // 
            // Column_department_name
            // 
            this.Column_department_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column_department_name.DataPropertyName = "department_name";
            this.Column_department_name.HeaderText = "Name";
            this.Column_department_name.Name = "Column_department_name";
            this.Column_department_name.ReadOnly = true;
            // 
            // Column_status
            // 
            this.Column_status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column_status.DataPropertyName = "status";
            this.Column_status.HeaderText = "Status";
            this.Column_status.Name = "Column_status";
            this.Column_status.ReadOnly = true;
            this.Column_status.Width = 60;
            // 
            // Column_created_date
            // 
            this.Column_created_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_created_date.DataPropertyName = "created_date";
            this.Column_created_date.HeaderText = "Created Date";
            this.Column_created_date.Name = "Column_created_date";
            this.Column_created_date.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btndownloadtemplate);
            this.groupBox1.Controls.Add(this.btnuploadexcel);
            this.groupBox1.Controls.Add(this.btndownloadexcel);
            this.groupBox1.Controls.Add(this.btnclose);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 517);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(547, 53);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // btndownloadtemplate
            // 
            this.btndownloadtemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndownloadtemplate.Location = new System.Drawing.Point(12, 17);
            this.btndownloadtemplate.Name = "btndownloadtemplate";
            this.btndownloadtemplate.Size = new System.Drawing.Size(124, 23);
            this.btndownloadtemplate.TabIndex = 0;
            this.btndownloadtemplate.Text = "Download &Template";
            this.btndownloadtemplate.UseVisualStyleBackColor = true;
            this.btndownloadtemplate.Click += new System.EventHandler(this.btndownloadtemplate_Click);
            // 
            // btnuploadexcel
            // 
            this.btnuploadexcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnuploadexcel.Location = new System.Drawing.Point(312, 17);
            this.btnuploadexcel.Name = "btnuploadexcel";
            this.btnuploadexcel.Size = new System.Drawing.Size(116, 23);
            this.btnuploadexcel.TabIndex = 2;
            this.btnuploadexcel.Text = "&Upload Departments";
            this.btnuploadexcel.UseVisualStyleBackColor = true;
            this.btnuploadexcel.Click += new System.EventHandler(this.btnuploadexcel_Click);
            // 
            // btndownloadexcel
            // 
            this.btndownloadexcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndownloadexcel.Location = new System.Drawing.Point(159, 17);
            this.btndownloadexcel.Name = "btndownloadexcel";
            this.btndownloadexcel.Size = new System.Drawing.Size(130, 23);
            this.btndownloadexcel.TabIndex = 1;
            this.btndownloadexcel.Text = "&Download Departments";
            this.btndownloadexcel.UseVisualStyleBackColor = true;
            this.btndownloadexcel.Click += new System.EventHandler(this.btndownloadexcel_Click);
            // 
            // btnclose
            // 
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Location = new System.Drawing.Point(451, 17);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(75, 23);
            this.btnclose.TabIndex = 3;
            this.btnclose.Text = "&Close";
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "filename";
            // 
            // departments_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnclose;
            this.ClientSize = new System.Drawing.Size(547, 570);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "departments_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Departments";
            this.Load += new System.EventHandler(this.departments_form_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_departments)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_departments)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnuploadexcel;
        private System.Windows.Forms.Button btndownloadexcel;
        private System.Windows.Forms.DataGridView dataGridView_departments;
        private System.Windows.Forms.BindingSource bindingSource_departments;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_department_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_created_date;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btndownloadtemplate;
    }
}