using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using jkuat_ip_telephony_dal;
using CrystalDecisions.CrystalReports.Engine;

namespace jkuat_ip_telephony_ui
{
    public partial class crystal_reports_form : Form
    {
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;
        public crystal_reports_form(EventHandler<notificationmessageEventArgs> notificationmessageEventname_from_parent)
        {
            InitializeComponent();


            TAG = this.GetType().Name;

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadException);

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName; 
            _notificationmessageEventname = notificationmessageEventname_from_parent;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished crystal_reports_form initialization", TAG));

        }

        private void crystal_reports_form_Load(object sender, EventArgs e)
        {
            try
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished crystal_reports_form load", TAG));

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

        private void toolStripButton_campuses_Click(object sender, EventArgs e)
        {
            try
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating campuses crystal report...", TAG));
                string app_title = this.Text;
                this.Text = "generating campuses crystal report...";

                DataSet dataset = new DataSet();
                dataset = new campusesDataSet();
                DataTable table = dataset.Tables["campusesDataTable"];

                List<campus_dto> lst_campuses = fetch_campuses();
                DataRow row;
                int i;

                for (i = 0; i < lst_campuses.Count(); i++)
                {
                    row = table.NewRow();
                    row["Id"] = lst_campuses[i].id;
                    row["Campus Name"] = lst_campuses[i].campus_name;
                    row["Status"] = lst_campuses[i].status;
                    row["Created Date"] = lst_campuses[i].created_date;
                    table.Rows.Add(row);
                }

                ReportDocument crystal_report = new ReportDocument();
                crystal_report.Load(Environment.CurrentDirectory + @"\crystal_reports\campus_CrystalReport.rpt");
                crystal_report.SetDataSource(dataset.Tables["campusesDataTable"]);
                crystalReportViewer.ReportSource = crystal_report;
                crystalReportViewer.Refresh();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("report generated successfully.", TAG));
                this.Text = app_title;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private void toolStripButton_departments_Click(object sender, EventArgs e)
        {
            try
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating departments crystal report...", TAG));
                string app_title = this.Text;
                this.Text = "generating departments crystal report...";

                DataSet dataset = new DataSet();
                dataset = new departmentsDataSet();
                DataTable table = dataset.Tables["departmentsDataTable"];

                List<department_dto> lst_departments = fetch_departments();
                DataRow row;
                int i;

                for (i = 0; i < lst_departments.Count(); i++)
                {
                    row = table.NewRow();
                    row["Id"] = lst_departments[i].id;
                    row["Campus"] = lst_departments[i].campus_name;
                    row["Department Name"] = lst_departments[i].department_name;
                    row["Status"] = lst_departments[i].status;
                    row["Created Date"] = lst_departments[i].created_date;
                    table.Rows.Add(row);
                }

                ReportDocument crystal_report = new ReportDocument();
                crystal_report.Load(Environment.CurrentDirectory + @"\crystal_reports\department_CrystalReport.rpt");
                crystal_report.SetDataSource(dataset.Tables["departmentsDataTable"]);
                crystalReportViewer.ReportSource = crystal_report;
                crystalReportViewer.Refresh();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("report generated successfully.", TAG));
                this.Text = app_title;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private void toolStripButton_extensions_Click(object sender, EventArgs e)
        {
            try
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating extensions crystal report...", TAG));
                string app_title = this.Text;
                this.Text = "generating extensions crystal report...";

                DataSet dataset = new DataSet();
                dataset = new extensionsDataSet();
                DataTable table = dataset.Tables["extensionsDataTable"];

                List<extension_dto> lst_extensions = fetch_extensions();
                DataRow row;
                int i;

                for (i = 0; i < lst_extensions.Count(); i++)
                {
                    row = table.NewRow();
                    row["Id"] = lst_extensions[i].id;
                    row["Campus"] = lst_extensions[i].campus_name;
                    row["Department"] = lst_extensions[i].department_name;
                    row["Owner Assigned"] = lst_extensions[i].owner_assigned;
                    row["Extension Number"] = lst_extensions[i].extension_number;
                    row["Status"] = lst_extensions[i].status;
                    row["Created Date"] = lst_extensions[i].created_date;
                    table.Rows.Add(row);
                }

                ReportDocument crystal_report = new ReportDocument();
                crystal_report.Load(Environment.CurrentDirectory + @"\crystal_reports\extension_CrystalReport.rpt");
                crystal_report.SetDataSource(dataset.Tables["extensionsDataTable"]);
                crystalReportViewer.ReportSource = crystal_report;
                crystalReportViewer.Refresh();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("report generated successfully.", TAG));
                this.Text = app_title;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private void toolStripButton_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private List<campus_dto> fetch_campuses()
        {
            List<campus_dto> lst_campuses = new List<campus_dto>();
            lst_campuses = mysqlapisingleton.getInstance(_notificationmessageEventname).lst_get_all_campuses();
            return lst_campuses;
        }

        private List<department_dto> fetch_departments()
        {
            List<department_dto> lst_departments = new List<department_dto>();
            lst_departments = mysqlapisingleton.getInstance(_notificationmessageEventname).lst_get_all_departments();

            foreach (department_dto _dto in lst_departments)
            {
                _dto.campus_name = mysqlapisingleton.getInstance(_notificationmessageEventname).get_campus_given_id(_dto.campus_id).campus_name;
            }

            return lst_departments;
        }

        private List<extension_dto> fetch_extensions()
        {
            List<extension_dto> lst_extensions = new List<extension_dto>();
            lst_extensions = mysqlapisingleton.getInstance(_notificationmessageEventname).lst_get_all_extensions();

            foreach (extension_dto _dto in lst_extensions)
            {
                _dto.campus_name = mysqlapisingleton.getInstance(_notificationmessageEventname).get_campus_given_id(_dto.campus_id).campus_name;
                _dto.department_name = mysqlapisingleton.getInstance(_notificationmessageEventname).get_department_given_id(_dto.department_id).department_name;
            }

            return lst_extensions;
        }


    }
}
