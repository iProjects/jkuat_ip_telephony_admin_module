using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jkuat_ip_telephony_dal
{
    public class user_dto
    {
        public string id { get; set; }
        public string email { get; set; }
        public string full_names { get; set; }
        public string pass_word { get; set; }
        public string secret_word { get; set; }
        public string status { get; set; }
        public string created_date { get; set; }
    }
}
