using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using jkuat_ip_telephony_dal;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Net;
using System.Security.Policy;
using System.Collections.Specialized;

namespace jkuat_ip_telephony_ui
{
    public partial class login_form : Form
    {
        DateTime startDate = DateTime.Now;
        public string TAG;

        // Timers namespace rather than Threading
        System.Timers.Timer elapsed_timer = new System.Timers.Timer(); // Doesn't require any args
        private int _TimeCounter = 0;
        DateTime _startDate = DateTime.Now;

        public List<notificationdto> _lstnotificationdto = new List<notificationdto>();

        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        public login_form()
        {
            InitializeComponent();

            TAG = this.GetType().Name;

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadException);

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName;
            _notificationmessageEventname += notificationmessageHandler;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished login_form initialization", TAG));
        }

        private void login_form_Load(object sender, EventArgs e)
        {
            string title = "Login - Copyright ©  " + DateTime.Now.Year.ToString() + " - All Rights Reserved";
            this.Text = title;

            this.txtusername.Focus();

            //txtusername.Text = "admin";
            //txtpassword.Text = "admin";

            //initialize current running time timer
            elapsed_timer.Interval = 1000;
            elapsed_timer.Elapsed += elapsed_timer_Elapsed; // Uses an event instead of a delegate
            elapsed_timer.Start(); // Start the timer

            //app version
            var _buid_version = Application.ProductVersion;
            groupBox1.Text = "Build Version - " + _buid_version;

            populate_auto_complete_values();

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished login_form load", TAG));

        }
        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            this._notificationmessageEventname.Invoke(sender, new notificationmessageEventArgs(ex.Message, TAG));
        }

        private void ThreadException(object sender, ThreadExceptionEventArgs e)
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
                String dateTimenow = currentDate.ToString("dd-MM-yyyy HH:mm:ss tt");

                String _logtext = Environment.NewLine + "[ " + dateTimenow + " ]   " + args.message;

                _notificationdto._notification_message = _logtext;
                _notificationdto._created_datetime = dateTimenow;
                _notificationdto.TAG = args.TAG;

                Log.WriteToErrorLogFile(new Exception(args.message));

                _lstnotificationdto.Add(_notificationdto);

                var _lstmsgdto = from msgdto in _lstnotificationdto
                                 orderby msgdto._created_datetime descending
                                 select msgdto._notification_message;

                String[] _logflippedlines = _lstmsgdto.ToArray();

                txtlog.Lines = _logflippedlines;
                txtlog.ScrollToCaret();

                //}));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void elapsed_timer_Elapsed(object sender, EventArgs e)
        {
            try
            {
                _TimeCounter++;
                DateTime nowDate = DateTime.Now;
                TimeSpan t = nowDate - _startDate;
                Invoke(new MethodInvoker(delegate()
                {
                    lbltimelapsed.Text = string.Format("{0} : {1} : {2} : {3}", t.Days, t.Hours, t.Minutes, t.Seconds);
                }));

                DateTime currentDate = DateTime.Now;
                String dateTimenow = currentDate.ToString("dd-MM-yyyy HH:mm:ss tt");

                Invoke(new MethodInvoker(delegate()
                {
                    lblrunningtime.Text = dateTimenow;
                }));


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loginToolStripMenuItem.Text = "Loging in...";
            try
            {
                string email = "";
                string password = "";
                string msg = "";

                if (!string.IsNullOrEmpty(txtusername.Text))
                {
                    email = txtusername.Text;
                }
                else
                {
                    msg = "User Name cannot be null.";
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(msg, TAG));
                    errorProvider.SetError(txtusername, msg);
                }

                if (!string.IsNullOrEmpty(txtpassword.Text))
                {
                    password = txtpassword.Text;
                }
                else
                {
                    msg = "Password cannot be null.";
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(msg, TAG));
                    errorProvider.SetError(txtpassword, msg);
                }

                if (msg.Length > 0)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("validation error...", TAG));
                    loginToolStripMenuItem.Text = "Login";
                    this.txtusername.Focus();

                    return;
                }

                responsedto response_dto = new responsedto();

                //create_password_hash_from_api(email, password);
                response_dto = login_from_api(email, password);

                //response_dto = mysqlapisingleton.getInstance(_notificationmessageEventname).login(email, password);

                if (response_dto.isresponseresultsuccessful)
                {
                    if (response_dto.responsesuccessmessage == "success")
                    {
                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(response_dto.responsesuccessmessage, TAG));

                        if (chkremember.Checked)
                        {
                            SaveAutoCompleteUsers();
                            save_auto_complete_login();
                        }
                        else
                        {
                            delete_auto_complete_login();
                        }

                        main_form _main_form = new main_form(email) { Owner = this };
                        _main_form.Show();
                        this.Hide();

                    }
                    else
                    {
                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(response_dto.responseerrormessage, TAG));
                    }

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(response_dto.responsesuccessmessage, TAG));

                }
                else
                {
                    loginToolStripMenuItem.Text = "Login";
                    this.txtusername.Focus();
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(response_dto.responseerrormessage, TAG));

                    errorProvider.SetError(txtusername, response_dto.responseerrormessage);
                    errorProvider.SetError(txtpassword, response_dto.responseerrormessage);

                }

                //main_form _main__form = new main_form() { Owner = this };
                //_main__form.Show();
                //this.Hide();

            }
            catch (Exception ex)
            {
                loginToolStripMenuItem.Text = "Login";
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message.ToString(), TAG));
                Utils.ShowError(new Exception(ex.Message));
            }

            //loginToolStripMenuItem.Text = "login";

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            loginToolStripMenuItem_Click(sender, e);
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            exitToolStripMenuItem_Click(sender, e);
        }

        private bool SaveAutoCompleteUsers()
        {
            try
            {
                string auto_complete_users_filename = Utils.AUTO_COMPLETE_USERS_FILENAME;

                string userName = txtusername.Text.Trim();
                string password = txtpassword.Text.Trim();
                password = Utils.encrypt_string(password);

                if (File.Exists(auto_complete_users_filename))
                {
                    List<SBSystem_Exp> successfully_logged_users = SQLHelper.GetDataFromSBSystem_ExpXML(auto_complete_users_filename);

                    var exists = successfully_logged_users.Where(i => i.Name.Equals(userName)).FirstOrDefault();

                    if (exists == null)
                    {
                        XDocument doc = XDocument.Load(auto_complete_users_filename);
                        doc.Element("Systems").Add(
                            new XElement("System",
                            new XAttribute("Name", userName),
                            new XAttribute("Application", password)
                            ));
                        doc.Save(auto_complete_users_filename);
                    }
                }
                if (!File.Exists(auto_complete_users_filename))
                {
                    List<SBSystem_Exp> systems = new List<SBSystem_Exp>() { 
                        new SBSystem_Exp(
                            userName,
                            password
                        )};

                    var xml = new XElement("Systems", systems.Select(x => new XElement("System",
                                           new XAttribute("Name", x.Name),
                                           new XAttribute("Application", x.Application)
                                           )));
                    xml.Save(auto_complete_users_filename);
                }
                return true;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message.ToString(), TAG));
                return false;
            }
        }

        private string[] AutoComplete_Users()
        {
            try
            {
                string auto_complete_users_filename = Utils.AUTO_COMPLETE_USERS_FILENAME;

                List<string> logged_usernames = new List<string>();

                if (File.Exists(auto_complete_users_filename))
                {
                    List<SBSystem_Exp> successfully_logged_users = SQLHelper.GetDataFromSBSystem_ExpXML(auto_complete_users_filename);

                    foreach (var item in successfully_logged_users)
                    {
                        if (!logged_usernames.Contains(item.Name))
                        {
                            logged_usernames.Add(item.Name);
                        }
                    }
                }
                return logged_usernames.ToArray();
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message.ToString(), TAG));
                return null;
            }
        }

        private void save_auto_complete_login()
        {
            try
            {
                string auto_complete_login_filename = Utils.AUTO_COMPLETE_LOGIN_FILENAME;

                string system = "online_ip_telephony";
                string serverName = "mysql";
                string databaseName = "ip_telephony_db";
                string userName = txtusername.Text.Trim();
                string password = txtpassword.Text.Trim();
                password = Utils.encrypt_string(password);
                bool IntegratedSecurity = false;
                bool remember = chkremember.Checked;

                if (File.Exists(auto_complete_login_filename))
                {
                    List<SB_Login> successfully_logged_users = GetDataFromSB_LoginXML(auto_complete_login_filename);

                    //var exists = successfully_logged_users.Where(i => i.userName.Equals(userName) && i.databaseName.Equals(databaseName) && i.serverName.Equals(serverName) && i.system.Equals(system)).FirstOrDefault();

                    DataSet ds = new DataSet();

                    ds.ReadXml(auto_complete_login_filename);

                    int count = ds.Tables[0].Rows.Count;

                    for (int i = 0; i < count; i++)
                    {
                        ds.Tables[0].DefaultView.RowFilter = "userName = '" + userName + "' and databaseName = '" + databaseName + "' and serverName = '" + serverName + "' and system = '" + system + "'";

                        DataTable dt = (ds.Tables[0].DefaultView).ToTable();

                        if (dt.Rows.Count > 0)
                        {
                            ds.Tables[0].Rows[i].Delete();
                        }
                    }

                    //get data
                    string xmlData = ds.GetXml();

                    //save to file
                    ds.WriteXml(auto_complete_login_filename);

                    XDocument doc = XDocument.Load(auto_complete_login_filename);
                    doc.Element("Systems").Add(
                        new XElement("System",
                        new XAttribute("system", system),
                        new XAttribute("serverName", serverName),
                        new XAttribute("databaseName", databaseName),
                        new XAttribute("userName", userName),
                        new XAttribute("password", password),
                        new XAttribute("IntegratedSecurity", IntegratedSecurity.ToString()),
                        new XAttribute("remember", remember.ToString())
                        ));

                    doc.Save(auto_complete_login_filename);
                }

                if (!File.Exists(auto_complete_login_filename))
                {
                    List<SB_Login> systems = new List<SB_Login>() { 
                        new SB_Login(system, 
                           serverName,
                           databaseName,
                           userName,
                           password,
                           IntegratedSecurity.ToString(),
                           remember.ToString()                            
                            )};

                    var xml = new XElement("Systems", systems.Select(x => new XElement("System",
                            new XAttribute("system", x.system),
                            new XAttribute("serverName", x.serverName),
                            new XAttribute("databaseName", x.databaseName),
                            new XAttribute("userName", x.userName),
                            new XAttribute("password", x.password),
                            new XAttribute("IntegratedSecurity", x.IntegratedSecurity),
                            new XAttribute("remember", x.remember)
                                           )));

                    xml.Save(auto_complete_login_filename);
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private List<SB_Login> GetDataFromSB_LoginXML(string filename)
        {
            using (XmlReader xmlRdr = new XmlTextReader(filename))
            {
                return new List<SB_Login>(
                   (from sysElem in XDocument.Load(xmlRdr).Element("Systems").Elements("System")
                    select new SB_Login(
                       (string)sysElem.Attribute("system"),
                       (string)sysElem.Attribute("serverName"),
                       (string)sysElem.Attribute("databaseName"),
                       (string)sysElem.Attribute("userName"),
                       (string)sysElem.Attribute("password"),
                       (string)sysElem.Attribute("IntegratedSecurity"),
                       (string)sysElem.Attribute("remember")
                                )).ToList());
            }
        }

        private void populate_auto_complete_values()
        {
            try
            {
                List<SB_Login> auto_complete_from_xml = GetDataFromSB_LoginXML(Utils.AUTO_COMPLETE_LOGIN_FILENAME);

                List<string> saved_servers = new List<string>();
                List<string> saved_databases = new List<string>();

                SB_Login last_record = auto_complete_from_xml.Last();

                if (last_record == null)
                {
                    return;
                }

                foreach (var item in auto_complete_from_xml)
                {
                    if (!saved_servers.Contains(item.serverName))
                    {
                        saved_servers.Add(item.serverName);
                    }
                    if (!saved_databases.Contains(item.databaseName))
                    {
                        saved_databases.Add(item.databaseName);
                    }
                }

                if (bool.Parse(last_record.remember))
                {

                    txtusername.Text = last_record.userName;

                    string password = last_record.password;
                    password = Utils.decrypt_string(password);
                    txtpassword.Text = password;

                    chkremember.Checked = bool.Parse(last_record.remember);

                }

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private void delete_auto_complete_login()
        {
            try
            {
                string auto_complete_login_filename = Utils.AUTO_COMPLETE_LOGIN_FILENAME;

                string system = "online_ip_telephony";
                string serverName = "mysql";
                string databaseName = "ip_telephony_db";
                string userName = "";
                string password = "";
                password = Utils.encrypt_string(password);
                bool IntegratedSecurity = false;
                bool remember = chkremember.Checked;

                if (File.Exists(auto_complete_login_filename))
                {
                    List<SB_Login> successfully_logged_users = GetDataFromSB_LoginXML(auto_complete_login_filename);

                    //var exists = successfully_logged_users.Where(i => i.userName.Equals(userName) && i.databaseName.Equals(databaseName) && i.serverName.Equals(serverName) && i.system.Equals(system)).FirstOrDefault();

                    DataSet ds = new DataSet();

                    ds.ReadXml(auto_complete_login_filename);

                    int count = ds.Tables[0].Rows.Count;

                    for (int i = 0; i < count; i++)
                    {
                        ds.Tables[0].DefaultView.RowFilter = "userName = '" + userName + "' and databaseName = '" + databaseName + "' and serverName = '" + serverName + "' and system = '" + system + "'";

                        DataTable dt = (ds.Tables[0].DefaultView).ToTable();

                        if (dt.Rows.Count > 0)
                        {
                            ds.Tables[0].Rows[i].Delete();
                        }
                    }

                    //get data
                    string xmlData = ds.GetXml();

                    //save to file
                    ds.WriteXml(auto_complete_login_filename);

                    XDocument doc = XDocument.Load(auto_complete_login_filename);
                    doc.Element("Systems").Add(
                        new XElement("System",
                        new XAttribute("system", system),
                        new XAttribute("serverName", serverName),
                        new XAttribute("databaseName", databaseName),
                        new XAttribute("userName", userName),
                        new XAttribute("password", password),
                        new XAttribute("IntegratedSecurity", IntegratedSecurity.ToString()),
                        new XAttribute("remember", remember.ToString())
                        ));

                    doc.Save(auto_complete_login_filename);
                }

                if (!File.Exists(auto_complete_login_filename))
                {
                    List<SB_Login> systems = new List<SB_Login>() { 
                        new SB_Login(system, 
                           serverName,
                           databaseName,
                           userName,
                           password,
                           IntegratedSecurity.ToString(),
                           remember.ToString()                            
                            )};

                    var xml = new XElement("Systems", systems.Select(x => new XElement("System",
                            new XAttribute("system", x.system),
                            new XAttribute("serverName", x.serverName),
                            new XAttribute("databaseName", x.databaseName),
                            new XAttribute("userName", x.userName),
                            new XAttribute("password", x.password),
                            new XAttribute("IntegratedSecurity", x.IntegratedSecurity),
                            new XAttribute("remember", x.remember)
                                           )));

                    xml.Save(auto_complete_login_filename);
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private void create_password_hash_from_api(string email, string password)
        {
            try
            {
                string api_url = System.Configuration.ConfigurationManager.AppSettings["ENCRYPTION_API_URL"];

                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["email"] = email;
                    values["pass_word"] = password;
                    values["action"] = "create_hash";

                    var iresponse = client.UploadValues(api_url, values);

                    var responseString = Encoding.Default.GetString(iresponse);

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(responseString, TAG));

                    Console.WriteLine(responseString);

                }

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private responsedto login_from_api(string email, string password)
        {
            responsedto response_dto = new responsedto();

            try
            {
                string api_url = System.Configuration.ConfigurationManager.AppSettings["ENCRYPTION_API_URL"];

                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["email"] = email;
                    values["pass_word"] = password;
                    values["action"] = "verify_password";

                    var iresponse = client.UploadValues(api_url, values);

                    string responseString = Encoding.Default.GetString(iresponse);

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(responseString, TAG));

                    Console.WriteLine(responseString);

                    response_dto.responsesuccessmessage = responseString.Trim();
                    response_dto.isresponseresultsuccessful = true;

                    return response_dto;

                }

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);

                response_dto.responseerrormessage = ex.Message;
                response_dto.isresponseresultsuccessful = false;

                return response_dto;
            }
        }




    }
}
