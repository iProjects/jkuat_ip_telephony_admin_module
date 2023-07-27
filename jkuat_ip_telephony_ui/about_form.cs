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
    public partial class about_form : Form
    {
        public about_form()
        {
            InitializeComponent();
        }

        private void about_form_Load(object sender, EventArgs e)
        {
            lblbuildversion.Text = string.Empty;
            lblproductname.Text = string.Empty;

            var _buid_version = Application.ProductVersion;
            var _product_name = Application.ProductName;

            lblbuildversion.Text = "Build Version - " + _buid_version;
            lblproductname.Text =_product_name;

            this.Text = "About " + _product_name;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
