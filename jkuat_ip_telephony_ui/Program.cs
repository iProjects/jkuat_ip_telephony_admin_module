﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace jkuat_ip_telephony_ui
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new main_form());
        }
    }
}
