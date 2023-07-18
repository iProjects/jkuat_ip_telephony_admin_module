using System;
using System.Collections.Generic;
using System.Linq; 
using jkuat_ip_telephony_dal; 

namespace jkuat_ip_telephony_ui
{
    public class campus_model_builder
    {
        campus_model_report _ViewModel; 
        string connection;
        bool error = false;
        int _year;
        int _period; 
        bool _current;
        string fileLogo;
        string slogan;
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;

        public campus_model_builder(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            _notificationmessageEventname = notificationmessageEventname;
        }


        public campus_model_report get_campus_model_report()
        {
            try
            {
                Build();
                return _ViewModel;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        public void Build()
        {
            try
            {
                _ViewModel = new campus_model_report();
                _ViewModel.employername = "jkuat".ToUpper();
                _ViewModel.employeraddress = "juja";
                _ViewModel.employertelephone = "020999999";
                _ViewModel.CompanyLogo = "resources/jkuat_logo.png";
                _ViewModel.CompanySlogan = "technology for development";
                _ViewModel.PrintedOn = DateTime.Today;
                _ViewModel.campuses = this.get_campuses();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private List<print_campuses> get_campuses()
        {
            try
            {
                List<print_campuses> lst_campuses = new List<print_campuses>();

                List<campus_dto> campuses_dto = mysqlapisingleton.getInstance(_notificationmessageEventname).lst_get_all_campuses();

                foreach (campus_dto _dto in campuses_dto)
                {
                    print_campuses _campus = new print_campuses();
                    _campus.id = _dto.id;
                    _campus.campus_name = _dto.campus_name;
                    _campus.status = _dto.status;
                    _campus.created_date = _dto.created_date;

                    lst_campuses.Add(_campus);
                }
                return lst_campuses;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }


















    }
}
