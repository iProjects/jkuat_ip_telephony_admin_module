using System;
using System.Collections.Generic;
using System.Linq;

namespace jkuat_ip_telephony_ui
{
    public class extension_model_report
    {
        public int Year { get; set; }
        public int Period { get; set; }
        public DateTime PrintedOn { get; set; }
        public string employername { get; set; }
        public string employeraddress { get; set; }
        public string employertelephone { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanySlogan { get; set; }
        public List<print_extensions> extensions { get; set; }
        public DateTime PeriodDate { get; set; }
        public string ReportName
        {
            get
            {
                return "";
            }
        }
        public decimal total_reords
        {
            get
            {
                return extensions.Count;
            }
        }
    }

    public class print_extensions
    {

        public string id { get; set; }
        public string campus_id { get; set; }
        public string campus_name { get; set; }
        public string department_id { get; set; }
        public string department_name { get; set; }
        public string owner_assigned { get; set; }
        public string extension_number { get; set; }
        public string status { get; set; }
        public string created_date { get; set; } 

    }
}
