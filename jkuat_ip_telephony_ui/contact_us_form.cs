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
    public partial class contact_us_form : Form
    {
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        public contact_us_form(EventHandler<notificationmessageEventArgs> notificationmessageEventname_from_parent)
        {
            InitializeComponent();

            TAG = this.GetType().Name;

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadException);

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName; 
            _notificationmessageEventname = notificationmessageEventname_from_parent;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished contact_us_form initialization", TAG));
        }

        private void contact_us_form_Load(object sender, EventArgs e)
        {

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished contact_us_form load", TAG));
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

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsendmessage_Click(object sender, EventArgs e)
        {
            bool no_error = true;
            errorProvider.Clear();

            if (string.IsNullOrEmpty(txtemail.Text))
            {
                errorProvider.SetError(txtemail, "Email cannot be null!");
                no_error = false;
            }
            if (!string.IsNullOrEmpty(txtemail.Text))
            {
                string emailregex = System.Configuration.ConfigurationManager.AppSettings["EMAILREGEX"];
                System.Text.RegularExpressions.Regex email_regex = new System.Text.RegularExpressions.Regex(emailregex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                if (!email_regex.IsMatch(txtemail.Text))
                {
                    errorProvider.SetError(txtemail, "Please provide a valid Email e.g username@domain.com");
                    no_error = false;
                }
            }
            if (string.IsNullOrEmpty(txtmessage.Text))
            {
                errorProvider.SetError(txtmessage, "Message cannot be null!");
                no_error = false;
            }

            if (!no_error)
            {
                return;
            }


            string template = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {

                sb.Append("Message from [ " + txtemail.Text + " ] \n");
                sb.Append("Message [ " + txtmessage.Text + " ] ");

                template = sb.ToString();

                Console.WriteLine(template);

                if (Utils.IsConnectedToInternet())
                {
                    bool is_email_sent = Utils.SendEmail(template);

                    if (is_email_sent)
                    {
                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Message sent successfully.", TAG));
                    }
                    else
                    {
                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Message send failed.", TAG));
                    }

                }
                else
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Message send failed. Check your internet connection.", TAG));
                }
            }
            catch (Exception ex)
            {
                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Utils.LogEventViewer(ex);
                }));
            }
            finally
            {
                //_notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(template, TAG));
                //Log.WriteToErrorLogFile_and_EventViewer(new Exception(template));
            }


        }




    }
}
