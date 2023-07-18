using System;
using System.Collections.Generic;
using System.Linq;
using jkuat_ip_telephony_dal;

namespace jkuat_ip_telephony_ui
{
    public class department_model_builder
    {
        department_model_report _ViewModel;
        string connection;
        bool error = false;
        int _year;
        int _period;
        bool _current;
        string fileLogo;
        string slogan;
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;

        public department_model_builder(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            _notificationmessageEventname = notificationmessageEventname;
        }


        public department_model_report get_department_model_report()
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
                _ViewModel = new department_model_report();
                _ViewModel.employername = "jkuat".ToUpper();
                _ViewModel.employeraddress = "juja";
                _ViewModel.employertelephone = "020999999";
                _ViewModel.CompanyLogo = "resources/jkuat_logo.png";
                _ViewModel.CompanySlogan = "technology for development";
                _ViewModel.PrintedOn = DateTime.Today;
                _ViewModel.departments = this.get_departments();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private List<print_departments> get_departments()
        {
            try
            {
                List<print_departments> lst_departments = new List<print_departments>();

                List<department_dto> departments_dto = mysqlapisingleton.getInstance(_notificationmessageEventname).lst_get_all_departments().ToList();

                foreach (department_dto _dto in departments_dto)
                {
                    print_departments _department = new print_departments();
                    _department.id = _dto.id;
                    _department.campus_id = _dto.campus_id;
                    _department.campus_name = mysqlapisingleton.getInstance(_notificationmessageEventname).get_campus_given_id(_dto.campus_id).campus_name;
                    _department.department_name = _dto.department_name;
                    _department.status = _dto.status;
                    _department.created_date = _dto.created_date;

                    lst_departments.Add(_department);
                }
                return lst_departments;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }


















    }
}
