using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using jkuat_ip_telephony_dal;
using System.Data.OleDb;

namespace jkuat_ip_telephony_ui
{
    public partial class departments_form : Form
    {
        PDFGen pdf_generator;
        string msFolder = "";
        string _resourcesPath = null;
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        public departments_form(EventHandler<notificationmessageEventArgs> notificationmessageEventname_from_parent)
        {
            InitializeComponent();


            TAG = this.GetType().Name;

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadException);

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName; 
            _notificationmessageEventname = notificationmessageEventname_from_parent;

            //--- init the folder in which generated PDF's will be saved.
            msFolder = AppDomain.CurrentDomain.BaseDirectory;
            int n = msFolder.LastIndexOf(@"\");
            msFolder = msFolder.Substring(0, n + 1);

            SetResourcePath();

            pdf_generator = new PDFGen(_resourcesPath, _notificationmessageEventname);

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished departments_form initialization", TAG));
        }

        private void departments_form_Load(object sender, EventArgs e)
        {
            try
            {
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

                this.dataGridView_departments.AutoGenerateColumns = false;
                this.dataGridView_departments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                RefreshGrid();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished departments_form load", TAG));
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private void RefreshGrid()
        {
            try
            {
                this.bindingSource_departments.DataSource = mysqlapisingleton.getInstance(_notificationmessageEventname).lst_get_all_departments();
                this.dataGridView_departments.DataSource = bindingSource_departments;
                this.groupBox2.Text = bindingSource_departments.Count.ToString();
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("fetched [ " + bindingSource_departments.Count.ToString() + " ] departments.", TAG));

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Log.WriteToErrorLogFile_and_EventViewer(ex);
            this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
        }

        private void ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            Log.WriteToErrorLogFile_and_EventViewer(ex);
            this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
        }

        public string pathlookup(string folder)
        {
            try
            {
                string app_dir = Utils.get_application_path();
                var dir = System.IO.Path.Combine(app_dir, folder);


                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
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
                    System.IO.Directory.CreateDirectory(dir);
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
                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
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

        private void DoShowFile(string file_path)
        {
            this.webBrowser.Navigate(get_reports_uri(file_path));
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btndownloadexcel_Click(object sender, EventArgs e)
        {
            try
            {
                //create model
                department_model_builder model_builder = new department_model_builder(this._notificationmessageEventname);
                department_model_report model_report = model_builder.get_department_model_report();

                string current_file_name = "departments.xlsx";
                string report_path = get_reports_uri(current_file_name);
                Console.WriteLine("report_path [ " + report_path + " ].");

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating file [ " + report_path + " ]...", TAG));

                if (pdf_generator.show_departments_excel(model_report, report_path))
                {
                    DoShowFile(current_file_name);
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private void btnuploadexcel_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Select an Excel file";
            //openFileDialog1.FileName = "";
            //"Text files (*.txt)|*.txt|All files (*.*)|*.*"
            openFileDialog.Filter = "Excel Files |*.xlsx|Excel Files|*.xls";

            string strFileName = string.Empty;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                strFileName = openFileDialog.FileName;

                try
                {
                    string response = upload_data(strFileName);
                    if (response.Length > 0)
                    {
                        Utils.ShowError(new Exception(response));
                    }
                    else
                    {
                        MessageBox.Show("Upload completed successfully");
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(new Exception("Upload incomplete" + Environment.NewLine + ex.Message));
                    return;
                }
            }
        }

        private string upload_data(string strFileName)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                string query = null;
                string SourceConnectionString = "";
                string strFileType = System.IO.Path.GetExtension(strFileName).ToString().ToLower();

                //Check file type
                if (strFileType != ".xls" && strFileType != ".xlsx")
                {
                    throw new Exception("File Type not Excel");
                }

                //Connection String to Excel Workbook
                //Create a OLEDB connection for Excel file    
                if (strFileType.Trim() == ".xls")
                {
                    SourceConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFileName
                        + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                else if (strFileType.Trim() == ".xlsx")
                {
                    SourceConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                        + strFileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }

                query = "SELECT CAMPUSNAME, DEPARTMENTNAME FROM [Sheet1$]";

                using (var excel_Connection = new OleDbConnection(SourceConnectionString))

                // Creating a command object to read the values from Excel file   
                using (OleDbCommand excel_Command = new OleDbCommand(query, excel_Connection))
                {
                    excel_Connection.Open();

                    // Creating a Read object  
                    OleDbDataReader excel_Reader = excel_Command.ExecuteReader();

                    int created_record_count = 0;

                    // Looping through the values and displaying  
                    while (excel_Reader.Read())
                    {
                        string campus_name = excel_Reader["CAMPUSNAME"].ToString();
                        string department_name = excel_Reader["DEPARTMENTNAME"].ToString();

                        Console.WriteLine("campus_name [ " + campus_name + " ]");
                        Console.WriteLine("department_name [ " + department_name + " ]");

                        campus_dto campus = mysqlapisingleton.getInstance(_notificationmessageEventname).get_campus_given_name(campus_name);
                        department_dto department = mysqlapisingleton.getInstance(_notificationmessageEventname).get_department_given_name(department_name);

                        if (department == null)
                        {
                            if (campus != null)
                            {
                                department_name = Utils.ConvertFirstLetterToUpper(department_name);
                                responsedto response = mysqlapisingleton.getInstance(_notificationmessageEventname).create_department_from_upload(campus.id, department_name);

                                created_record_count++;

                                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("created department [ " + department_name + " ] for campus [ " + campus_name + " ].", TAG));
                            }
                            else
                            {
                                sb.AppendLine("campus [ " + campus_name + " ] does not exists.");
                                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("campus [ " + campus_name + " ] does not exists.", TAG));
                            }
                        }
                        else
                        {
                            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("department [ " + department_name + " ] exists.", TAG));
                        }
                    }

                    if (created_record_count > 0)
                    {
                        sb.AppendLine("[ " + created_record_count + " ] records created.");
                    }
                    else
                    {
                        sb.AppendLine("no records created.");
                    }
                }
            }
            catch (Exception ex)
            {
                //Utils.ShowError(ex);
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                throw ex;
            }
            finally
            {
                RefreshGrid();
            }

            return sb.ToString();
        }

        private void btndownloadtemplate_Click(object sender, EventArgs e)
        {
            try
            {
                CreateExcelDoc excel_app = new CreateExcelDoc();

                //creates the main header
                //createHeaders(int row, int col, string htext, string cell1, string cell2, int mergeColumns, string b, bool font, int size, string fcolor)
                excel_app.createHeaders(1, 1, "CAMPUSNAME", "A1", "A1", 0, "WHITE", true, 10, "n");
                //creates subheaders
                excel_app.createHeaders(1, 2, "DEPARTMENTNAME", "A2", "A2", 0, "WHITE", true, 10, "n");

                string current_file_name = "departments_template.xlsx";
                string report_path = get_reports_uri(current_file_name);
                Console.WriteLine("report_path [ " + report_path + " ].");

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating file [ " + report_path + " ]...", TAG));

                excel_app.Save(report_path);

                DoShowFile(report_path);

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                throw ex;
            }
        }



    }
}
