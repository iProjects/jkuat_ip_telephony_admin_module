using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jkuat_ip_telephony_dal
{
    public class department_dto
    {
        public int id { get; set; }
        public int campus_id { get; set; }
        public string campus_name { get; set; }
        public string department_name { get; set; }
        public string status { get; set; }
        public string created_date { get; set; }
    }
}
