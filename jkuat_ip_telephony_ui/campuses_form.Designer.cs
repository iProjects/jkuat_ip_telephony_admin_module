namespace jkuat_ip_telephony_ui
{
    partial class campuses_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(campuses_form));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btndownloadtemplate = new System.Windows.Forms.Button();
            this.btnuploadexcel = new System.Windows.Forms.Button();
            this.btndownloadexcel = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.dataGridView_campuses = new System.Windows.Forms.DataGridView();
            this.Column_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_campus_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_created_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource_campuses = new System.Windows.Forms.BindingSource(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_campuses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_campuses)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btndownloadtemplate);
            this.groupBox1.Controls.Add(this.btnuploadexcel);
            this.groupBox1.Controls.Add(this.btndownloadexcel);
            this.groupBox1.Controls.Add(this.btnclose);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 514);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(528, 47);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btndownloadtemplate
            // 
            this.btndownloadtemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndownloadtemplate.Location = new System.Drawing.Point(12, 16);
            this.btndownloadtemplate.Name = "btndownloadtemplate";
            this.btndownloadtemplate.Size = new System.Drawing.Size(124, 23);
            this.btndownloadtemplate.TabIndex = 5;
            this.btndownloadtemplate.Text = "Download &Template";
            this.btndownloadtemplate.UseVisualStyleBackColor = true;
            this.btndownloadtemplate.Click += new System.EventHandler(this.btndownloadtemplate_Click);
            // 
            // btnuploadexcel
            // 
            this.btnuploadexcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnuploadexcel.Location = new System.Drawing.Point(308, 16);
            this.btnuploadexcel.Name = "btnuploadexcel";
            this.btnuploadexcel.Size = new System.Drawing.Size(110, 23);
            this.btnuploadexcel.TabIndex = 4;
            this.btnuploadexcel.Text = "&Upload Campuses";
            this.btnuploadexcel.UseVisualStyleBackColor = true;
            this.btnuploadexcel.Click += new System.EventHandler(this.btnuploadexcel_Click);
            // 
            // btndownloadexcel
            // 
            this.btndownloadexcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndownloadexcel.Location = new System.Drawing.Point(159, 16);
            this.btndownloadexcel.Name = "btndownloadexcel";
            this.btndownloadexcel.Size = new System.Drawing.Size(126, 23);
            this.btndownloadexcel.TabIndex = 3;
            this.btndownloadexcel.Text = "&Download Campuses";
            this.btndownloadexcel.UseVisualStyleBackColor = true;
            this.btndownloadexcel.Click += new System.EventHandler(this.btndownloadexcel_Click);
            // 
            // btnclose
            // 
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Location = new System.Drawing.Point(441, 16);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(75, 23);
            this.btnclose.TabIndex = 1;
            this.btnclose.Text = "&Close";
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.webBrowser);
            this.groupBox2.Controls.Add(this.dataGridView_campuses);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(528, 514);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(37, 215);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(192, 79);
            this.webBrowser.TabIndex = 1;
            this.webBrowser.Visible = false;
            // 
            // dataGridView_campuses
            // 
            this.dataGridView_campuses.AllowUserToAddRows = false;
            this.dataGridView_campuses.AllowUserToDeleteRows = false;
            this.dataGridView_campuses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_campuses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_id,
            this.Column_campus_name,
            this.Column_status,
            this.Column_created_date});
            this.dataGridView_campuses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_campuses.Location = new System.Drawing.Point(3, 16);
            this.dataGridView_campuses.Name = "dataGridView_campuses";
            this.dataGridView_campuses.ReadOnly = true;
            this.dataGridView_campuses.Size = new System.Drawing.Size(522, 495);
            this.dataGridView_campuses.TabIndex = 0;
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
            // Column_campus_name
            // 
            this.Column_campus_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column_campus_name.DataPropertyName = "campus_name";
            this.Column_campus_name.HeaderText = "Name";
            this.Column_campus_name.Name = "Column_campus_name";
            this.Column_campus_name.ReadOnly = true;
            this.Column_campus_name.Width = 200;
            // 
            // Column_status
            // 
            this.Column_status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column_status.DataPropertyName = "status";
            this.Column_status.HeaderText = "Status";
            this.Column_status.Name = "Column_status";
            this.Column_status.ReadOnly = true;
            this.Column_status.Width = 70;
            // 
            // Column_created_date
            // 
            this.Column_created_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_created_date.DataPropertyName = "created_date";
            this.Column_created_date.HeaderText = "Created Date";
            this.Column_created_date.Name = "Column_created_date";
            this.Column_created_date.ReadOnly = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "filename";
            // 
            // campuses_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnclose;
            this.ClientSize = new System.Drawing.Size(528, 561);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "campuses_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Campuses";
            this.Load += new System.EventHandler(this.campuses_form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_campuses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_campuses)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnuploadexcel;
        private System.Windows.Forms.Button btndownloadexcel;
        private System.Windows.Forms.DataGridView dataGridView_campuses;
        private System.Windows.Forms.BindingSource bindingSource_campuses;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_campus_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_created_date;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btndownloadtemplate;
    }
}