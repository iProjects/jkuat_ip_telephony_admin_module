using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using jkuat_ip_telephony_dal;

namespace jkuat_ip_telephony_ui
{
    public partial class main_form : Form
    {
        string TAG;
        //Event declaration:
        //event for publishing messages to output
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        //list to hold messages
        List<notificationdto> _lstnotificationdto = new List<notificationdto>();
        /* to use a BackgroundWorker object to perform time-intensive operations in a background thread.
		You need to:
		1. Define a worker method that does the time-intensive work and call it from an event handler for the DoWork
		event of a BackgroundWorker.
		2. Start the execution with RunWorkerAsync. Any argument required by the worker method attached to DoWork
		can be passed in via the DoWorkEventArgs parameter to RunWorkerAsync.
		In addition to the DoWork event the BackgroundWorker class also defines two events that should be used for
		interacting with the user interface. These are optional.
		The RunWorkerCompleted event is triggered when the DoWork handlers have completed.
		The ProgressChanged event is triggered when the ReportProgress method is called. */
        BackgroundWorker bgWorker = new BackgroundWorker();
        // Timers namespace rather than Threading
        System.Timers.Timer running_timer = new System.Timers.Timer(); // Doesn't require any args
        // Timers namespace rather than Threading
        System.Timers.Timer elapsed_timer = new System.Timers.Timer(); // Doesn't require any args
        //task start time
        DateTime _task_start_time = DateTime.Now;
        //task end time
        DateTime _task_end_time = DateTime.Now;
        int _TimeCounter = 0;
        DateTime _startDate = DateTime.Now;

        public main_form()
        {
            InitializeComponent();

            TAG = this.GetType().Name;

            AppDomain.CurrentDomain.UnhandledException += new
UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadException);

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName; 
            _notificationmessageEventname += notificationmessageHandler;
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished main_form initialization", TAG));
        }

        private void main_form_Load(object sender, EventArgs e)
        {
            lblbuildversion.Text = string.Empty;
            lbldatetime.Text = string.Empty;
            lbltimelapsed.Text = string.Empty;

            //app version
            var _buid_version = Application.ProductVersion;
            lblbuildversion.Text = "build version - " + _buid_version;

            //initialize current running time timer
            running_timer.Interval = 1000;
            running_timer.Elapsed += running_timer_Elapsed; // Uses an event instead of a delegate
            running_timer.Start(); // Start the timer

            elapsed_timer.Interval = 1000;
            elapsed_timer.Elapsed += elapsed_timer_Elapsed; // Uses an event instead of a delegate
            elapsed_timer.Start(); // Start the timer

            //This allows the BackgroundWorker to be cancelled in between tasks
            bgWorker.WorkerSupportsCancellation = true;
            //This allows the worker to report progress between completion of tasks...
            //this must also be used in conjunction with the ProgressChanged event
            bgWorker.WorkerReportsProgress = true;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished main_form load", TAG));

        }

        void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            this._notificationmessageEventname.Invoke(sender, new notificationmessageEventArgs(ex.Message, TAG));
        }

        void ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            this._notificationmessageEventname.Invoke(sender, new notificationmessageEventArgs(ex.Message, TAG));
        }

        //Event handler declaration:
        public void notificationmessageHandler(object sender, notificationmessageEventArgs args)
        {
            try
            {
                /* Handler logic */

                //Invoke(new Action(() =>
                //{

                notificationdto _notificationdto = new notificationdto();

                DateTime currentDate = DateTime.Now;
                String dateTimenow = currentDate.ToString("dd-MM-yyyy HH:mm:ss");

                String _logtext = Environment.NewLine + "[ " + dateTimenow + " ]   " + args.message;

                _notificationdto._notification_message = _logtext;
                _notificationdto._created_datetime = dateTimenow;
                _notificationdto.TAG = args.TAG;

                Log.WriteToErrorLogFile(new Exception(args.message));

                Utils.LogEventViewer(new Exception(args.message));

                _lstnotificationdto.Add(_notificationdto);

                var _lstmsgdto = from msgdto in _lstnotificationdto
                                 orderby msgdto._created_datetime descending
                                 select msgdto._notification_message;

                String[] _logflippedlines = _lstmsgdto.ToArray();

                txtlog.Lines = _logflippedlines;
                txtlog.ScrollToCaret();

                appNotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                appNotifyIcon.BalloonTipText = _logtext;
                appNotifyIcon.Text = args.TAG;
                appNotifyIcon.BalloonTipTitle = args.TAG;
                //notifyIconntharene.ShowBalloonTip(2000);
                appNotifyIcon.BalloonTipClicked += new EventHandler(appNotifyIcon_BalloonTipClicked);

                //}));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        void appNotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(get_app_info(), TAG));
        }

        private void appNotifyIcon_Click(object sender, EventArgs e)
        {
            this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(get_app_info(), TAG));
        }

        private void appNotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(get_app_info(), TAG));
        }

        string get_app_info()
        {
            StringBuilder _sb = new StringBuilder();
            _sb.AppendLine("");
            _sb.AppendLine("ProductVersion: " + Application.ProductVersion);
            _sb.AppendLine("CompanyName: " + Application.CompanyName);
            _sb.AppendLine("ProductName: " + Application.ProductName);
            return _sb.ToString();
        }

        private void elapsed_timer_Elapsed(object sender, EventArgs e)
        {
            try
            {
                _TimeCounter++;
                DateTime nowDate = DateTime.Now;
                TimeSpan t = nowDate - _startDate;
                lbltimelapsed.Text = string.Format("{0} : {1} : {2} : {3}", t.Days, t.Hours, t.Minutes, t.Seconds);
            }
            catch (Exception ex)
            {
                //this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Console.WriteLine(ex.ToString());
            }
        }
        private void running_timer_Elapsed(object sender, EventArgs e)
        {
            try
            {
                //current running time
                DateTime currentDate = DateTime.Now;
                String dateTimenow = currentDate.ToString("dd(dddd)-MM(MMMM)-yyyy HH:mm:ss");
                lbldatetime.Text = dateTimenow;
            }
            catch (Exception ex)
            {
                //this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Console.WriteLine(ex.ToString());
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings_form settings = new settings_form();
            settings.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about_form about = new about_form();
            about.Show();
        }

        private void help_ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void contactUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contact_us_form contact_us = new contact_us_form();
            contact_us.Show();
        }

        private void campusesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            campuses_form campuses = new campuses_form(this._notificationmessageEventname);
            campuses.Show();
        }

        private void departmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            departments_form departments = new departments_form(this._notificationmessageEventname);
            departments.Show();
        }

        private void extensionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extensions_form extensions = new extensions_form(this._notificationmessageEventname);
            extensions.Show();
        }

        private void pdfReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pdf_viewer_form viewer = new pdf_viewer_form(this._notificationmessageEventname);
            viewer.Show();
        }

        private void crystalReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crystal_reports_form crystal_reports = new crystal_reports_form(this._notificationmessageEventname);
            crystal_reports.Show();
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

        private void toolStripButton_reports_Click(object sender, EventArgs e)
        {
            pdfReportsToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton_settings_Click(object sender, EventArgs e)
        {
            settingsToolStripMenuItem_Click(sender, e);
        }



    }
}
