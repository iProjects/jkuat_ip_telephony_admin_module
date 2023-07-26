using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using jkuat_ip_telephony_dal;
using Microsoft.Win32;
using System.IO;
using System.Net;
using System.Diagnostics;

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
        string logged_in_user;
        DateTime _startTime = DateTime.Now;
        string _template;
        string current_action = string.Empty;
        int max_msg_length = 0;

        public main_form(string email)
        {
            InitializeComponent();

            logged_in_user = email;

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
            current_action = "load";
            _task_start_time = DateTime.Now;

            lblloggedinuser.Text = string.Empty;
            lblbuildversion.Text = string.Empty;
            lblrunningtime.Text = string.Empty;
            lbltimelapsed.Text = string.Empty;

            //logged in user
            lblloggedinuser.Text = "Logged in User - " + logged_in_user;

            //app version
            var _buid_version = Application.ProductVersion;
            lblbuildversion.Text = "Build Version - " + _buid_version;

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

            //this assigns event handlers for the backgroundWorker
            bgWorker.DoWork += bgWorker_DoWork;
            bgWorker.RunWorkerCompleted += bgWorker_WorkComplete;
            /* When you wish to have something occur when a change in progress
                occurs, (like the completion of a specific task) the "ProgressChanged"
                event handler is used. Note that ProgressChanged events may be invoked
                by calls to bgWorker.ReportProgress(...) only if bgWorker.WorkerReportsProgress
                is set to true. */
            bgWorker.ProgressChanged += bgWorker_ProgressChanged;

            //tell the backgroundWorker to raise the "DoWork" event, thus starting it.
            //Check to make sure the background worker is not already running.
            if (!bgWorker.IsBusy)
                bgWorker.RunWorkerAsync();

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

                Invoke(new System.Action(() =>
                {
                    lbltimelapsed.Text = string.Format("{0} : {1} : {2} : {3}", t.Days, t.Hours, t.Minutes, t.Seconds);
                }));

                Invoke(new MethodInvoker(delegate()
                {
                    lbltimelapsed.Text = string.Format("{0} : {1} : {2} : {3}", t.Days, t.Hours, t.Minutes, t.Seconds);
                }));

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

                Invoke(new System.Action(() =>
                {
                    lblrunningtime.Text = dateTimenow;
                }));

                Invoke(new MethodInvoker(delegate()
                {
                    lblrunningtime.Text = dateTimenow;
                }));

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
            campuses_form campuses = new campuses_form(this._notificationmessageEventname, logged_in_user);
            campuses.Show();
        }

        private void departmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            departments_form departments = new departments_form(this._notificationmessageEventname, logged_in_user);
            departments.Show();
        }

        private void extensionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extensions_form extensions = new extensions_form(this._notificationmessageEventname, logged_in_user);
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
            toolStripButton_campuses.Enabled = false;
            campusesToolStripMenuItem_Click(sender, e);
            toolStripButton_campuses.Enabled = true;
        }

        private void toolStripButton_departments_Click(object sender, EventArgs e)
        {
            toolStripButton_departments.Enabled = false;
            departmentsToolStripMenuItem_Click(sender, e);
            toolStripButton_departments.Enabled = true;
        }

        private void toolStripButton_extensions_Click(object sender, EventArgs e)
        {
            toolStripButton_extensions.Enabled = false;
            extensionsToolStripMenuItem_Click(sender, e);
            toolStripButton_extensions.Enabled = true;
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

        private void main_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            NotifyMessage(Utils.APP_NAME, "Exiting...");

            if (e.CloseReason == CloseReason.ApplicationExitCall)
            {
                try
                {
                    CloseAllOpenForms();
                    collect_admin_info_in_background_worker_thread();
                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Utils.LogEventViewer(ex);
                }
            }
            if (e.CloseReason == CloseReason.UserClosing)
            {
                try
                {
                    CloseAllOpenForms();
                    collect_admin_info_in_background_worker_thread();
                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Utils.LogEventViewer(ex);
                }
            }
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                try
                {
                    CloseAllOpenForms();
                    collect_admin_info_in_background_worker_thread();
                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Utils.LogEventViewer(ex);
                }
            }
            if (e.CloseReason == CloseReason.FormOwnerClosing)
            {
                try
                {
                    CloseAllOpenForms();
                    collect_admin_info_in_background_worker_thread();
                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Utils.LogEventViewer(ex);
                }
            }
            if (e.CloseReason == CloseReason.MdiFormClosing)
            {
                try
                {
                    CloseAllOpenForms();
                    collect_admin_info_in_background_worker_thread();
                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Utils.LogEventViewer(ex);
                }
            }
            if (e.CloseReason == CloseReason.None)
            {
                try
                {
                    CloseAllOpenForms();
                    collect_admin_info_in_background_worker_thread();
                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Utils.LogEventViewer(ex);
                }
            }
            if (e.CloseReason == CloseReason.TaskManagerClosing)
            {
                try
                {
                    CloseAllOpenForms();
                    collect_admin_info_in_background_worker_thread();
                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Utils.LogEventViewer(ex);
                }
            }
        }

        private void CloseAllOpenForms()
        {
            try
            {
                try
                {
                    string registryPath = "SOFTWARE\\" + Application.CompanyName + "\\" + Application.ProductName + "\\" + Application.ProductVersion;
                    RegistryKey MyReg = Registry.CurrentUser.CreateSubKey(registryPath, RegistryKeyPermissionCheck.ReadWriteSubTree);

                    DateTime currentDate = DateTime.Now;
                    String dateTimenow = currentDate.ToString("dd-MM-yyyy HH:mm:ss tt");

                    MyReg.SetValue("Last Usage Date", dateTimenow);

                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Log.WriteToErrorLogFile_and_EventViewer(ex);
                }

                List<Form> openForms = new List<Form>();
                foreach (Form f in Application.OpenForms)
                {
                    openForms.Add(f);
                }

                foreach (Form f in openForms)
                {
                    if (f.Name != "main_form")
                        f.Close();
                }

                Application.Exit();
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private void CollectAdminAppInfo()
        {
            string template = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {
                DateTime nowDate = DateTime.Now;
                TimeSpan t = nowDate - _startTime;

                WriteToCurrentUserRegisteryonAppClose(t.ToString());

                string loggederror = string.Empty;
                loggederror = Utils.ReadLogFile();
                string macaddrress = Utils.GetMACAddress();
                string ipAddresses = Utils.GetFormattedIpAddresses();
                DateTime _endTime = DateTime.Now;
                string _totalusagetime = this.ReadFromCurrentUserRegisteryTotalUsageTime();

                //if greater than zero then truncate
                if (max_msg_length > 0)
                {
                    string truncated_string = truncate_string_recursively(loggederror);

                    int original_length = loggederror.Length;
                    int truncated_length = truncated_string.Length;

                    loggederror = truncated_string;
                }

                sb.Append("User [ " + logged_in_user + " ] ");
                sb.Append("was logged in from [ " + this._startTime.ToString() + " ] ");
                sb.Append("to [ " + _endTime.ToString() + " ] ");
                sb.Append("total elapsed time [ " + lbltimelapsed.Text + " ] ");
                sb.Append("machine name [ " + FQDN.GetFQDN() + " ] ");
                sb.Append("MAC [ " + macaddrress + " ] ");
                sb.Append("ip addresses [ " + ipAddresses + " ] ");
                sb.Append("Total Usage Time [ " + _totalusagetime + " ] ");
                sb.Append("Template [ " + _template + " ] ");
                sb.Append("Logged Errors " + " [ " + loggederror + " ] ");

                template = sb.ToString();

                Console.WriteLine(template);

                if (Utils.IsConnectedToInternet())
                {
                    bool is_email_sent = Utils.SendEmail(template);
                }
            }
            catch (Exception ex)
            {
                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                }));
                Utils.LogEventViewer(ex);
            }
            finally
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(template, TAG));
                Log.WriteToErrorLogFile_and_EventViewer(new Exception(template));
            }
        }

        private String truncate_string_recursively(string string_to_truncate)
        {
            try
            {
                string truncated_string = string_to_truncate;
                if (truncated_string.Length > max_msg_length)
                {
                    int half = truncated_string.Length / 2;
                    truncated_string = truncated_string.Substring(half);
                }
                if (truncated_string.Length > max_msg_length)
                {
                    truncated_string = truncate_string_recursively(truncated_string);
                }

                int truncated_length = truncated_string.Length;
                Console.WriteLine(truncated_length);

                return truncated_string;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.LogEventViewer(ex);
                return string_to_truncate;
            }
        }
        private bool CollectAdminExtraInfo()
        {
            try
            {
                //ExecuteIPConfigCommands();

                //FindComputersConectedToHost();

                //GetClientExtraInfo();

                //GetHostNameandMac();

                return true;
            }
            catch (Exception ex)
            {
                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                }));
                Utils.LogEventViewer(ex);
                return false;
            }
        }
        private void collect_admin_info_in_background_worker_thread()
        {
            try
            {
                current_action = "collect";

                _task_start_time = DateTime.Now;

                //This allows the BackgroundWorker to be cancelled in between tasks
                bgWorker.WorkerSupportsCancellation = true;
                //This allows the worker to report progress between completion of tasks...
                //this must also be used in conjunction with the ProgressChanged event
                bgWorker.WorkerReportsProgress = true;

                //this assigns event handlers for the backgroundWorker
                bgWorker.DoWork += bgWorker_DoWork;
                bgWorker.RunWorkerCompleted += bgWorker_WorkComplete;
                /* When you wish to have something occur when a change in progress
                    occurs, (like the completion of a specific task) the "ProgressChanged"
                    event handler is used. Note that ProgressChanged events may be invoked
                    by calls to bgWorker.ReportProgress(...) only if bgWorker.WorkerReportsProgress
                    is set to true. */
                bgWorker.ProgressChanged += bgWorker_ProgressChanged;

                //tell the backgroundWorker to raise the "DoWork" event, thus starting it.
                //Check to make sure the background worker is not already running.
                if (!bgWorker.IsBusy)
                    bgWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.LogEventViewer(ex);
            }
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //this is the method that the backgroundworker will perform on in the background thread without blocking the UI.
                /* One thing to note! A try catch is not necessary as any exceptions will terminate the
                backgroundWorker and report the error to the "RunWorkerCompleted" event */
                if (current_action.Equals("load"))
                {
                    try
                    {
                        max_msg_length = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MAX_MSG_LENGTH"]);

                        Write_To_Current_User_Registery_on_App_first_start();

                        Write_To_Local_Machine_Registery_on_App_first_start();

                        LogIn();

                        WriteToCurrentUserRegistery();

                        try
                        {
                            string url = System.Configuration.ConfigurationManager.AppSettings["WEB_VERSION_URL"];

                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                            if (response.StatusDescription.Equals("OK"))
                            {
                                Invoke(new System.Action(() =>
                                {
                                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(string.Format("loading [{0}]", url), TAG));
                                }));

                                webBrowser.Invoke(new System.Action(() =>
                                {
                                    this.webBrowser.Navigate(url);
                                }));

                            }
                        }
                        catch (Exception ex)
                        {
                            Invoke(new System.Action(() =>
                            {
                                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                            }));
                            Log.WriteToErrorLogFile_and_EventViewer(ex);
                        }

                    }
                    catch (Exception ex)
                    {
                        Invoke(new System.Action(() =>
                        {
                            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                        }));
                        Log.WriteToErrorLogFile_and_EventViewer(ex);
                    }
                }
                else if (current_action.Equals("collect"))
                {
                    try
                    {
                        CollectAdminExtraInfo();
                        CollectAdminAppInfo();
                    }
                    catch (Exception ex)
                    {
                        Invoke(new System.Action(() =>
                        {
                            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                        }));
                        Log.WriteToErrorLogFile_and_EventViewer(ex);
                    }
                }
                else if (current_action.Equals("home"))
                {
                    try
                    {
                        string url = System.Configuration.ConfigurationManager.AppSettings["WEB_VERSION_URL"];

                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                        if (response.StatusDescription.Equals("OK"))
                        {
                            Invoke(new System.Action(() =>
                            {
                                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(string.Format("loading [{0}]", url), TAG));
                            }));

                            webBrowser.Invoke(new System.Action(() =>
                            {
                                this.webBrowser.Navigate(url);
                            }));

                        }
                    }
                    catch (Exception ex)
                    {
                        Invoke(new System.Action(() =>
                        {
                            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                        }));
                        Log.WriteToErrorLogFile_and_EventViewer(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                }));
                Utils.LogEventViewer(ex);
            }
        }

        /*This is how the ProgressChanged event method signature looks like:*/
        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Things to be done when a progress change has been reported
            /* The ProgressChangedEventArgs gives access to a percentage,
            allowing for easy reporting of how far along a process is*/
            int progress = e.ProgressPercentage;
        }

        private void bgWorker_WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                bgWorker.DoWork -= bgWorker_DoWork;
                bgWorker.RunWorkerCompleted -= bgWorker_WorkComplete;
                bgWorker.ProgressChanged -= bgWorker_ProgressChanged;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.LogEventViewer(ex);
            }
        }

        private bool NotifyMessage(string _Title, string _Text)
        {
            try
            {
                appNotifyIcon.Text = Utils.APP_NAME;
                //appNotifyIcon.Icon = new Icon("resources/favicon.ico");
                appNotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                //appNotifyIcon.ContextMenuStrip = contextMenuStripSystemNotification;
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

        private bool ExecuteIPConfigCommands()
        {
            try
            {
                System.Diagnostics.ProcessStartInfo sdpsinfo = new System.Diagnostics.ProcessStartInfo("ipconfig.exe", "-all");
                // The following commands are needed to
                //redirect the standard output.
                // This means that it will //be redirected to the
                // Process.StandardOutput StreamReader
                // u can try other console applications
                //such as  arp.exe, etc
                sdpsinfo.RedirectStandardOutput = true;
                // redirecting the standard output
                sdpsinfo.UseShellExecute = false;
                // without that console shell window
                sdpsinfo.CreateNoWindow = true;
                // Now we create a process,
                //assign its ProcessStartInfo and start it
                System.Diagnostics.Process p =
                new System.Diagnostics.Process();
                p.StartInfo = sdpsinfo;
                p.Start();
                // well, we should check the return value here...
                //  capturing the output into a string variable...
                string res = p.StandardOutput.ReadToEnd();
                _template += res;

                Debug.Write(res);
                Log.WriteToErrorLogFile_and_EventViewer(new Exception(res));

                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(res, TAG));
                }));


                return true;
            }
            catch (Exception ex)
            {
                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                }));
                Utils.LogEventViewer(ex);
                return false;
            }
        }

        private bool FindComputersConectedToHost()
        {
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo =
                    new System.Diagnostics.ProcessStartInfo("cmd", "/c netstat -a");
                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                // Get the output into a string
                string res = proc.StandardOutput.ReadToEnd();
                _template += res;

                Debug.Write(res);
                Log.WriteToErrorLogFile_and_EventViewer(new Exception(res));

                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(res, TAG));
                }));


                return true;
            }
            catch (Exception ex)
            {
                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                }));
                Utils.LogEventViewer(ex);
                return false;
            }
        }

        private bool GetHostNameandMac()
        {
            try
            {
                System.Diagnostics.ProcessStartInfo sdpsinfo = new System.Diagnostics.ProcessStartInfo("cmd.exe", "Getmac,Hostname");
                // The following commands are needed to
                //redirect the standard output.
                // This means that it will //be redirected to the
                // Process.StandardOutput StreamReader
                // u can try other console applications
                //such as  arp.exe, etc
                sdpsinfo.RedirectStandardOutput = true;
                // redirecting the standard output
                sdpsinfo.UseShellExecute = false;
                // without that console shell window
                sdpsinfo.CreateNoWindow = true;
                // Now we create a process,
                //assign its ProcessStartInfo and start it
                System.Diagnostics.Process p =
                new System.Diagnostics.Process();
                p.StartInfo = sdpsinfo;
                p.Start();
                // well, we should check the return value here...
                //  capturing the output into a string variable...
                string res = p.StandardOutput.ReadToEnd();
                _template += res;

                Debug.Write(res);
                Log.WriteToErrorLogFile_and_EventViewer(new Exception(res));

                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(res, TAG));
                }));

                return true;
            }
            catch (Exception ex)
            {
                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                }));

                Utils.LogEventViewer(ex);
                return false;
            }
        }

        private bool GetClientExtraInfo()
        {
            try
            {
                System.Diagnostics.ProcessStartInfo sdpsinfo =
 new System.Diagnostics.ProcessStartInfo
 ("cmd.exe", " NBTSTAT -n,WHOAMI, VER, TASKLIST, GPRESULT /r, NETSTAT,  Assoc, Arp -a");
                // The following commands are needed to
                //redirect the standard output.
                // This means that it will //be redirected to the
                // Process.StandardOutput StreamReader
                // u can try other console applications
                //such as  arp.exe, etc
                sdpsinfo.RedirectStandardOutput = true;
                // redirecting the standard output
                sdpsinfo.UseShellExecute = false;
                // without that console shell window
                sdpsinfo.CreateNoWindow = true;
                // Now we create a process,
                //assign its ProcessStartInfo and start it
                System.Diagnostics.Process p =
                new System.Diagnostics.Process();
                p.StartInfo = sdpsinfo;
                p.Start();
                // well, we should check the return value here...
                //  capturing the output into a string variable...
                string res = p.StandardOutput.ReadToEnd();
                _template += res;

                Debug.Write(res);
                Log.WriteToErrorLogFile(new Exception(res));
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(res, TAG));

                return true;
            }
            catch (Exception ex)
            {
                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                }));
                Utils.LogEventViewer(ex);
                return false;
            }
        }

        public void LogIn()
        {
            try
            {
                NotifyMessage("Log in Successfull.", "Welcome " + logged_in_user);
                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Log in Successfull, Welcome " + logged_in_user, TAG));
                }));

            }
            catch (Exception ex)
            {
                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                }));
            }
        }


        private bool WriteToCurrentUserRegistery()
        {
            try
            {
                string registryPath = "SOFTWARE\\" + Application.CompanyName + "\\" + Application.ProductName + "\\" + Application.ProductVersion;
                RegistryKey MyReg = Registry.CurrentUser.CreateSubKey(registryPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
                MyReg.SetValue("Company Name", Application.CompanyName);
                MyReg.SetValue("Application Name", Application.ProductName);
                MyReg.SetValue("Version", Application.ProductVersion);
                MyReg.SetValue("Launch Date", DateTime.Now.ToString());
                MyReg.SetValue("Developer", "Kevin Mutugi");
                MyReg.SetValue("Copyright", "Copyright ©  2015 - 2040");
                MyReg.SetValue("GUID", "baedce16-cf28-4a20-a5f3-4c698c242d99");
                MyReg.SetValue("TradeMark", "Soft Books Suite");
                MyReg.SetValue("Phone-Safaricom1", "+254717769329");
                MyReg.SetValue("Phone-Safaricom2", "+254740538757");
                MyReg.SetValue("Email", "kevin@softwareproviders.co.ke");
                MyReg.SetValue("Gmail", "kevinmk30@gmail.com");
                MyReg.SetValue("Company Website", "www.softwareproviders.co.ke");
                MyReg.SetValue("Company Email", "softwareproviders254@gmail.com");
                MyReg.Close();
                return true;
            }
            catch (Exception ex)
            {
                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                }));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                return false;
            }
        }

        private void Write_To_Current_User_Registery_on_App_first_start()
        {
            try
            {
                string registryPath = "SOFTWARE\\" + Application.CompanyName + "\\" + Application.ProductName + "\\" + Application.ProductVersion;

                DateTime currentDate = DateTime.Now;
                String dateTimenow = currentDate.ToString("dd-MM-yyyy HH:mm:ss tt");
                string keyvaluedata = string.Empty;

                using (RegistryKey MyReg = Registry.CurrentUser.OpenSubKey(registryPath, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl))
                {
                    if (MyReg != null)
                    {
                        keyvaluedata = string.Format("{0}", MyReg.GetValue("First Usage Time", "").ToString());
                    }
                }

                if (keyvaluedata.Length == 0)
                {
                    RegistryKey MyReg = Registry.CurrentUser.CreateSubKey(registryPath, RegistryKeyPermissionCheck.ReadWriteSubTree);

                    MyReg.SetValue("First Usage Time", dateTimenow);

                    string mac_address = Utils.GetMACAddress();
                    Console.WriteLine("Mac Address [ " + mac_address + " ]");
                    MyReg.SetValue("Mac Address", mac_address);

                    string computer_name = Utils.get_computer_name();
                    Console.WriteLine("Computer Name [ " + computer_name + " ]");
                    MyReg.SetValue("Computer Name", computer_name);

                    MyReg.Close();
                }

            }
            catch (Exception ex)
            {
                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                }));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private void Write_To_Local_Machine_Registery_on_App_first_start()
        {
            try
            {
                string registryPath = "SOFTWARE\\" + Application.CompanyName + "\\" + Application.ProductName + "\\" + Application.ProductVersion;

                DateTime currentDate = DateTime.Now;
                String dateTimenow = currentDate.ToString("dd-MM-yyyy HH:mm:ss tt");
                string keyvaluedata = string.Empty;

                using (RegistryKey MyReg = Registry.LocalMachine.OpenSubKey(registryPath, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl))
                {
                    if (MyReg != null)
                    {
                        keyvaluedata = string.Format("{0}", MyReg.GetValue("First Usage Time", "").ToString());
                    }
                }

                if (keyvaluedata.Length == 0)
                {
                    RegistryKey MyReg = Registry.LocalMachine.CreateSubKey(registryPath);

                    MyReg.SetValue("First Usage Time", dateTimenow);

                    string mac_address = Utils.GetMACAddress();
                    Console.WriteLine("Mac Address [ " + mac_address + " ]");
                    MyReg.SetValue("Mac Address", mac_address);

                    string computer_name = Utils.get_computer_name();
                    Console.WriteLine("Computer Name [ " + computer_name + " ]");
                    MyReg.SetValue("Computer Name", computer_name);

                    MyReg.Close();
                }

            }
            catch (Exception ex)
            {
                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                }));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private bool WriteToCurrentUserRegisteryonAppClose(string _totalLoggedinTime)
        {
            try
            {
                string _totalusagetime = this.ReadFromCurrentUserRegisteryTotalUsageTime();
                string _lastusagetime = this.ReadFromCurrentUserRegisteryLastUsageTime();

                TimeSpan ts = TimeSpan.Parse(_lastusagetime);
                TimeSpan _tust = TimeSpan.Parse(_totalLoggedinTime);
                TimeSpan _tts = _tust + ts;

                this.DeleteCurrentUserRegistery();

                string registryPath = "SOFTWARE\\" + Application.CompanyName + "\\" + Application.ProductName + "\\" + Application.ProductVersion;
                RegistryKey MyReg = Registry.CurrentUser.CreateSubKey(registryPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
                MyReg.SetValue("Last Usage Time", _totalLoggedinTime);
                MyReg.SetValue("Total Usage Time", _tts);
                MyReg.Close();
                return true;
            }
            catch (Exception ex)
            {
                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                }));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                return false;
            }
        }

        private bool DeleteCurrentUserRegistery()
        {
            try
            {

                string registryPath = "SOFTWARE\\" + Application.CompanyName + "\\" + Application.ProductName + "\\" + Application.ProductVersion;
                using (RegistryKey MyReg = Registry.CurrentUser.OpenSubKey(registryPath, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl))
                {
                    MyReg.DeleteValue("Last Usage Time");
                    MyReg.DeleteValue("Total Usage Time");
                }
                return true;
            }
            catch (Exception ex)
            {
                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                }));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                return false;
            }
        }

        private string ReadFromCurrentUserRegisteryStartDate()
        {
            try
            {
                string registryPath = "SOFTWARE\\" + Application.CompanyName + "\\" + Application.ProductName + "\\" + Application.ProductVersion;
                using (RegistryKey MyReg = Registry.CurrentUser.OpenSubKey(registryPath, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl))
                {
                    string keyvaluedata = string.Format("{0}", MyReg.GetValue("Launch Date", DateTime.Now.ToString()).ToString());
                    return keyvaluedata;
                }
            }
            catch (Exception ex)
            {
                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                }));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                return null;
            }
        }

        private string ReadFromCurrentUserRegisteryTotalUsageTime()
        {
            try
            {
                string registryPath = "SOFTWARE\\" + Application.CompanyName + "\\" + Application.ProductName + "\\" + Application.ProductVersion;
                using (RegistryKey MyReg = Registry.CurrentUser.OpenSubKey(registryPath, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl))
                {
                    string keyvaluedata = string.Format("{0}", MyReg.GetValue("Total Usage Time", 0).ToString());
                    return keyvaluedata;
                }
            }
            catch (Exception ex)
            {
                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                }));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                return null;
            }
        }

        private string ReadFromCurrentUserRegisteryLastUsageTime()
        {
            try
            {
                string registryPath = "SOFTWARE\\" + Application.CompanyName + "\\" + Application.ProductName + "\\" + Application.ProductVersion;
                using (RegistryKey MyReg = Registry.CurrentUser.OpenSubKey(registryPath, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl))
                {
                    string keyvaluedata = string.Format("{0}", MyReg.GetValue("Last Usage Time", 0).ToString());
                    return keyvaluedata;
                }
            }
            catch (Exception ex)
            {
                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                }));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                return null;
            }
        }





    }
}
