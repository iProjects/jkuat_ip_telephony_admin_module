using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using jkuat_ip_telephony_dal;
//--- Add the following to make this code work
using iTextSharp.text;
using VVX;
using System.Threading;

namespace jkuat_ip_telephony_ui
{
    public partial class pdf_viewer_form : Form
    {
        #region "Private Fields"
        private string msAppName = "JKUAT IP TELEPHONY Report.....";
        PDFGen pdf_generator;
        string current_file_name = "";
        string msFolder = "";
        string _resourcesPath = null;
        string TAG;
        List<notificationdto> _lstnotificationdto = new List<notificationdto>();
        //Event declaration:
        //event for publishing messages to output
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname_from_parent;
        #endregion "Private Fields"

        public pdf_viewer_form(EventHandler<notificationmessageEventArgs> notificationmessageEventname_from_parent)
        {
            InitializeComponent();

            TAG = this.GetType().Name;

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadException);

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName;
            _notificationmessageEventname += notificationmessageHandler;
            _notificationmessageEventname_from_parent = notificationmessageEventname_from_parent;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished pdf_viewer_form initialization", TAG));

            //--- init the folder in which generated PDF's will be saved.
            msFolder = AppDomain.CurrentDomain.BaseDirectory;
            int n = msFolder.LastIndexOf(@"\");
            msFolder = msFolder.Substring(0, n + 1);

            SetResourcePath();

            pdf_generator = new PDFGen(_resourcesPath, _notificationmessageEventname);
        }

        #region "General Purpose Helpers for this Form"
        //************************************************************
        /// <summary>
        /// Refreshes the window's Caption/Title bar
        /// </summary>
        private void DoUpdateCaption()
        {
            try
            {
                this.Text = this.msAppName;

                if (this.current_file_name.Length == 0)
                {
                    this.Text += "<...no PDF file created...>";
                    this.lblstatusinfo.Text = "<...no PDF file created...>";
                }
                else
                {
                    FileInfo fi = new FileInfo(get_reports_uri(this.current_file_name));
                    this.Text += @"...\" + fi.Name;
                    this.lblstatusinfo.Text = this.current_file_name;
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }
        private void DoPreProcess(object sender, EventArgs e)
        {
            this.lblstatusinfo.Text = string.Empty;
            string msg = "processing report...";
            this.lblstatusinfo.Text = msg;
            this.Text = msg;
        }
        public string pathlookup(string folder)
        {
            try
            {
                string app_dir = Utils.get_application_path();
                var dir = Path.Combine(app_dir, folder);


                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                return dir;

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                return null;
            }
        }
        private void DoPostProcess(object sender, EventArgs e)
        {
            try
            {
                string dir = pathlookup("reports");
                string sRet = Utils.build_file_path(dir, current_file_name);
                int pdfCount = Directory.GetFiles(dir, "*.pdf", SearchOption.TopDirectoryOnly).Length;
                int excelCount = Directory.GetFiles(dir, "*.xls", SearchOption.TopDirectoryOnly).Length;
                int _totalFiles = pdfCount + excelCount;
                this.lblstatusinfo.Text = current_file_name.ToString() + "     [  " + _totalFiles.ToString() + "  ] ";

                copy_to_user_temp();
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }
        private string get_reports_uri(string sFile)
        {
            string sRet;
            try
            {
                string dir = pathlookup("reports");
                sRet = Utils.build_file_path(dir, sFile);

                //check if directory exists.
                if (!System.IO.Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                return sRet;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
                return "";
            }
        }
        private void SetResourcePath()
        {
            string sRet = string.Empty;
            try
            {
                string dir = pathlookup("resources");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                else
                {
                    sRet = dir;
                }

                this._resourcesPath = sRet;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
                this._resourcesPath = Utils.build_file_path(msFolder, "resources");
            }
        }
        private void DoShowPDF(string sFilePDF)
        {
            this.DoUpdateCaption();
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("successfully generated file [ " + sFilePDF + " ]", TAG));
            this.webBrowser.Navigate(get_reports_uri(sFilePDF));
        }
        private void NavigateToHomePage()
        {
            try
            {
                string help_file = "index.html";

                string base_directory = AppDomain.CurrentDomain.BaseDirectory;
                string help_path = Path.Combine(base_directory, "help");
                string help_file_path = Path.Combine(help_path, help_file);

                FileInfo fi = new FileInfo(help_file_path);

                if (fi.Exists)
                    this.webBrowser.Navigate(fi.FullName);
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.LogEventViewer(ex);
            }
        }

        private void copy_to_user_temp()
        {
            try
            {
                string base_directory = AppDomain.CurrentDomain.BaseDirectory;
                string reports_path = Path.Combine(base_directory, "Reports");

                string temp_path = Path.GetTempPath();

                DirectoryInfo reports_dir_info = new DirectoryInfo(reports_path);
                DirectoryInfo temp_dir_info = new DirectoryInfo(temp_path);

                var files = reports_dir_info.GetFiles();

                foreach (var report_file_info in files)
                {
                    var _temp_file = Path.Combine(temp_path, report_file_info.Name);

                    System.IO.File.Copy(report_file_info.FullName, _temp_file, true);

                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private void lblstatusinfo_Click(object sender, EventArgs e)
        {
            try
            {
                string base_directory = AppDomain.CurrentDomain.BaseDirectory;
                string reports_path = Path.Combine(base_directory, "Reports");

                if (Directory.Exists(reports_path))
                {
                    string _filetoSelect = Path.Combine(reports_path, current_file_name);
                    // opens the folder in explorer and selects the displayed file
                    string args = string.Format("/Select, {0}", _filetoSelect);
                    ProcessStartInfo pfi = new ProcessStartInfo("Explorer.exe", args);
                    System.Diagnostics.Process.Start(pfi);
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }
        private bool NotifyMessage(string _Title, string _Text)
        {
            try
            {
                appNotifyIcon.Text = Utils.APP_NAME;
                appNotifyIcon.Icon = new Icon("Resources/favicon.ico");
                appNotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                appNotifyIcon.BalloonTipTitle = _Title;
                appNotifyIcon.BalloonTipText = _Text;
                appNotifyIcon.Visible = true;
                appNotifyIcon.ShowBalloonTip(900000);

                return true;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.LogEventViewer(ex);
                return false;
            }
        }

        #endregion "General Purpose Helpers for this Form"

        private void pdf_viewer_form_Load(object sender, EventArgs e)
        {
            try
            {
                NavigateToHomePage();
                InitializeControls();
                RefreshGrid();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished pdf_viewer_form load", TAG));
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }
        private void InitializeControls()
        {
            try
            {
                lblstatusinfo.Text = string.Empty;

                List<campus_dto> lst_campuses = mysqlapisingleton.getInstance(_notificationmessageEventname).lst_get_all_campuses();
                DataGridViewComboBoxColumn cbox_col_campuses = new DataGridViewComboBoxColumn();
                cbox_col_campuses.HeaderText = "Campus";
                cbox_col_campuses.Name = "cbox_col_campuses";
                cbox_col_campuses.DataSource = lst_campuses;
                // The display member is the name column in the column datasource  
                cbox_col_campuses.DisplayMember = "campus_name";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                cbox_col_campuses.DataPropertyName = "campus_id";
                // The value member is the primary key of the parent table  
                cbox_col_campuses.ValueMember = "id";
                cbox_col_campuses.MaxDropDownItems = 10;
                cbox_col_campuses.Width = 150;
                cbox_col_campuses.DisplayIndex = 1;
                cbox_col_campuses.MinimumWidth = 5;
                cbox_col_campuses.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                cbox_col_campuses.FlatStyle = FlatStyle.Flat;
                cbox_col_campuses.DefaultCellStyle.NullValue = "--- Select ---";
                cbox_col_campuses.ReadOnly = true;
                if (!this.dataGridView_departments.Columns.Contains("cbox_col_campuses"))
                {
                    dataGridView_departments.Columns.Add(cbox_col_campuses);
                }

                List<campus_dto> lst_extension_campuses = mysqlapisingleton.getInstance(_notificationmessageEventname).lst_get_all_campuses();
                DataGridViewComboBoxColumn cbox_col_extension_campuses = new DataGridViewComboBoxColumn();
                cbox_col_extension_campuses.HeaderText = "Campus";
                cbox_col_extension_campuses.Name = "cbox_col_extension_campuses";
                cbox_col_extension_campuses.DataSource = lst_extension_campuses;
                // The display member is the name column in the column datasource  
                cbox_col_extension_campuses.DisplayMember = "campus_name";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                cbox_col_extension_campuses.DataPropertyName = "campus_id";
                // The value member is the primary key of the parent table  
                cbox_col_extension_campuses.ValueMember = "id";
                cbox_col_extension_campuses.MaxDropDownItems = 10;
                cbox_col_extension_campuses.Width = 100;
                cbox_col_extension_campuses.DisplayIndex = 1;
                cbox_col_extension_campuses.MinimumWidth = 5;
                cbox_col_extension_campuses.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                cbox_col_extension_campuses.FlatStyle = FlatStyle.Flat;
                cbox_col_extension_campuses.DefaultCellStyle.NullValue = "--- Select ---";
                cbox_col_extension_campuses.ReadOnly = true;
                if (!this.dataGridView_extensions.Columns.Contains("cbox_col_extension_campuses"))
                {
                    dataGridView_extensions.Columns.Add(cbox_col_extension_campuses);
                }

                List<department_dto> lst_extension_departments = mysqlapisingleton.getInstance(_notificationmessageEventname).lst_get_all_departments();
                DataGridViewComboBoxColumn cbox_col_extension_departments = new DataGridViewComboBoxColumn();
                cbox_col_extension_departments.HeaderText = "Department";
                cbox_col_extension_departments.Name = "cbox_col_extension_departments";
                cbox_col_extension_departments.DataSource = lst_extension_departments;
                // The display member is the name column in the column datasource  
                cbox_col_extension_departments.DisplayMember = "department_name";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                cbox_col_extension_departments.DataPropertyName = "department_id";
                // The value member is the primary key of the parent table  
                cbox_col_extension_departments.ValueMember = "id";
                cbox_col_extension_departments.MaxDropDownItems = 10;
                cbox_col_extension_departments.Width = 100;
                cbox_col_extension_departments.DisplayIndex = 2;
                cbox_col_extension_departments.MinimumWidth = 5;
                cbox_col_extension_departments.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                cbox_col_extension_departments.FlatStyle = FlatStyle.Flat;
                cbox_col_extension_departments.DefaultCellStyle.NullValue = "--- Select ---";
                cbox_col_extension_departments.ReadOnly = true;
                if (!this.dataGridView_extensions.Columns.Contains("cbox_col_extension_departments"))
                {
                    dataGridView_extensions.Columns.Add(cbox_col_extension_departments);
                }

                this.dataGridView_campuses.AutoGenerateColumns = false;
                this.dataGridView_campuses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                this.dataGridView_departments.AutoGenerateColumns = false;
                this.dataGridView_departments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                this.dataGridView_extensions.AutoGenerateColumns = false;
                this.dataGridView_extensions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                this.bindingSource_campuses.DataSource = mysqlapisingleton.getInstance(_notificationmessageEventname).lst_get_all_campuses();
                this.dataGridView_campuses.DataSource = bindingSource_campuses;
                this.groupBox1.Text = bindingSource_campuses.Count.ToString();
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("fetched [ " + bindingSource_campuses.Count.ToString() + " ] campuses.", TAG));

                this.bindingSource_departments.DataSource = mysqlapisingleton.getInstance(_notificationmessageEventname).lst_get_all_departments();
                this.dataGridView_departments.DataSource = bindingSource_departments;
                this.groupBox2.Text = bindingSource_departments.Count.ToString();
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("fetched [ " + bindingSource_departments.Count.ToString() + " ] departments.", TAG));

                var lst_extesions = mysqlapisingleton.getInstance(_notificationmessageEventname).lst_get_all_extensions().OrderBy(i => i.campus_id).ToList();
                this.bindingSource_extensions.DataSource = lst_extesions;
                this.dataGridView_extensions.DataSource = bindingSource_extensions;
                this.groupBox3.Text = bindingSource_extensions.Count.ToString();
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("fetched [ " + bindingSource_extensions.Count.ToString() + " ] extensions.", TAG));

                tabControlReportsData.SelectedTab = tabControlReportsData.TabPages[tabControlReportsData.TabPages.IndexOf(tabPage_campuses)];
                tabControlReportsData.SelectedTab = tabControlReportsData.TabPages[tabControlReportsData.TabPages.IndexOf(tabPage_departments)];
                tabControlReportsData.SelectedTab = tabControlReportsData.TabPages[tabControlReportsData.TabPages.IndexOf(tabPage_extensions)];
                tabControlReportsData.SelectedTab = tabControlReportsData.TabPages[tabControlReportsData.TabPages.IndexOf(tabPage_logs)];
                tabControlReportsData.SelectedTab = tabControlReportsData.TabPages[tabControlReportsData.TabPages.IndexOf(tabPage_campuses)];
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Log.WriteToErrorLogFile_and_EventViewer(ex);
            this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
        }

        private void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            Log.WriteToErrorLogFile_and_EventViewer(ex);
            this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
        }

        //Event handler declaration:
        private void notificationmessageHandler(object sender, notificationmessageEventArgs args)
        {
            try
            {
                /* Handler logic */
                notificationdto _notificationdto = new notificationdto();

                DateTime currentDate = DateTime.Now;
                String dateTimenow = currentDate.ToString("dd-MM-yyyy HH:mm:ss tt");

                String _logtext = Environment.NewLine + "[ " + dateTimenow + " ]   " + args.message;

                _notificationdto._notification_message = _logtext;
                _notificationdto._created_datetime = dateTimenow;
                _notificationdto.TAG = args.TAG;

                _lstnotificationdto.Add(_notificationdto);
                Console.WriteLine(args.message);
                _notificationmessageEventname_from_parent.Invoke(this, new notificationmessageEventArgs(args.message, TAG));

                var _lstmsgdto = from msgdto in _lstnotificationdto
                                 orderby msgdto._created_datetime descending
                                 select msgdto._notification_message;

                String[] _logflippedlines = _lstmsgdto.ToArray();

                if (_logflippedlines.Length > 5000)
                {
                    _lstnotificationdto.Clear();
                }

                txtlog.Lines = _logflippedlines;
                txtlog.ScrollToCaret();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void RefreshGrid()
        {
            try
            {
                this.bindingSource_campuses.DataSource = mysqlapisingleton.getInstance(_notificationmessageEventname).lst_get_all_campuses();
                this.dataGridView_campuses.DataSource = bindingSource_campuses;
                this.groupBox1.Text = bindingSource_campuses.Count.ToString();

                this.bindingSource_departments.DataSource = mysqlapisingleton.getInstance(_notificationmessageEventname).lst_get_all_departments();
                this.dataGridView_departments.DataSource = bindingSource_departments;
                this.groupBox2.Text = bindingSource_departments.Count.ToString();

                this.bindingSource_extensions.DataSource = mysqlapisingleton.getInstance(_notificationmessageEventname).lst_get_all_extensions();
                this.dataGridView_extensions.DataSource = bindingSource_extensions;
                this.groupBox3.Text = bindingSource_extensions.Count.ToString();

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.LogEventViewer(ex);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void campusesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //create model
                campus_model_builder model_builder = new campus_model_builder(this._notificationmessageEventname);
                campus_model_report model_report = model_builder.get_campus_model_report();

                current_file_name = "campuses.pdf";

                DoPreProcess(sender, e);

                if (pdf_generator.show_campuses_pdf(model_report, get_reports_uri(current_file_name)))
                {
                    DoShowPDF(current_file_name);
                }
                this.DoPostProcess(sender, e);

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                Utils.ShowError(ex);
                return;
            }
        }

        private void departmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //create model
                department_model_builder model_builder = new department_model_builder(this._notificationmessageEventname);
                department_model_report model_report = model_builder.get_department_model_report();

                current_file_name = "departments.pdf";

                DoPreProcess(sender, e);

                if (pdf_generator.show_departments_pdf(model_report, get_reports_uri(current_file_name)))
                {
                    DoShowPDF(current_file_name);
                }
                this.DoPostProcess(sender, e);

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                Utils.ShowError(ex);
                return;
            }
        }

        private void extensionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //create model
                extension_model_builder model_builder = new extension_model_builder(this._notificationmessageEventname);
                extension_model_report model_report = model_builder.get_extension_model_report();

                current_file_name = "extensions.pdf";

                DoPreProcess(sender, e);

                if (pdf_generator.show_extensions_pdf(model_report, get_reports_uri(current_file_name)))
                {
                    DoShowPDF(current_file_name);
                }
                this.DoPostProcess(sender, e);

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                Utils.ShowError(ex);
                return;
            }
        }

        private void toolStripButton_campuses_Click(object sender, EventArgs e)
        {
            campusesToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton_departments_Click(object sender, EventArgs e)
        {
            departmentsToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton_extensions_Click(object sender, EventArgs e)
        {
            extensionsToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton_exit_Click(object sender, EventArgs e)
        {
            exitToolStripMenuItem_Click(sender, e);
        }

        private void dataGridView_extensions_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(e.Exception.ToString(), TAG));
        }

        private void dataGridView_departments_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(e.Exception.ToString(), TAG));
        }

        private void dataGridView_campuses_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(e.Exception.ToString(), TAG));
        }




    }
}
