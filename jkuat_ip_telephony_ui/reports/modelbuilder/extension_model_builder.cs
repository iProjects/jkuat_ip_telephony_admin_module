using System;
using System.Collections.Generic;
using System.Linq;
using jkuat_ip_telephony_dal;

namespace jkuat_ip_telephony_ui
{
    public class extension_model_builder
    {
        extension_model_report _ViewModel;
        string connection;
        bool error = false;
        int _year;
        int _period;
        bool _current;
        string fileLogo;
        string slogan;
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;

        public extension_model_builder(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            _notificationmessageEventname = notificationmessageEventname;
        }


        public extension_model_report get_extension_model_report()
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
                _ViewModel = new extension_model_report();
                _ViewModel.employername = "jkuat".ToUpper();
                _ViewModel.employeraddress = "juja";
                _ViewModel.employertelephone = "020999999";
                _ViewModel.CompanyLogo = "resources/jkuat_logo.png";
                _ViewModel.CompanySlogan = "technology for development";
                _ViewModel.PrintedOn = DateTime.Today;
                _ViewModel.extensions = this.get_extensions();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private List<print_extensions> get_extensions()
        {
            try
            {
                List<print_extensions> lst_extensions = new List<print_extensions>();

                List<extension_dto> extensions_dto = mysqlapisingleton.getInstance(_notificationmessageEventname).lst_get_all_extensions().ToList();

                int counter = 0;

                foreach (extension_dto _dto in extensions_dto)
                {
                    counter++;

                    print_extensions _extension = new print_extensions();
                    //_extension.id = _dto.id;
                    _extension.id = counter.ToString();
                    _extension.campus_id = _dto.campus_id;
                    _extension.campus_name = _dto.campus_name;
                    _extension.campus_name = mysqlapisingleton.getInstance(_notificationmessageEventname).get_campus_given_id(_dto.campus_id).campus_name;
                    _extension.department_id = _dto.department_id;
                    _extension.department_name = mysqlapisingleton.getInstance(_notificationmessageEventname).get_department_given_id(_dto.department_id).department_name;
                    _extension.owner_assigned = _dto.owner_assigned;
                    _extension.extension_number = _dto.extension_number;
                    _extension.status = _dto.status;
                    _extension.created_date = _dto.created_date;

                    lst_extensions.Add(_extension);
                }
                return lst_extensions;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }


















    }
}
