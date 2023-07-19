using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace jkuat_ip_telephony_dal
{
    public sealed class mysqlapisingleton
    {
        // Because the _instance member is made private, the only way to get the single
        // instance is via the static Instance property below. This can also be similarly
        // achieved with a GetInstance() method instead of the property.
        private static mysqlapisingleton singleInstance;

        public static mysqlapisingleton getInstance(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            // The first call will create the one and only instance.
            if (singleInstance == null)
                singleInstance = new mysqlapisingleton(notificationmessageEventname);
            // Every call afterwards will return the single instance created above.
            return singleInstance;
        }

        private string CONNECTION_STRING = @"host=localhost;database=ip_telephony_db;user=sa;password=123456789;port=3306;";
        private const string db_name = "ip_telephony_db";//DBContract.DATABASE_NAME;
        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        private string TAG;

        private mysqlapisingleton(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            _notificationmessageEventname = notificationmessageEventname;
            try
            {
                TAG = this.GetType().Name;
                setconnectionstring();
                createdatabaseonfirstload();
                createtablesonfirstload();
            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
            }
        }

        private mysqlapisingleton()
        {

        }
        private void setconnectionstring()
        {
            try
            {
                mysqlconnectionstringdto _connectionstringdto = new mysqlconnectionstringdto();

                _connectionstringdto.datasource = System.Configuration.ConfigurationManager.AppSettings["mysql_datasource"];
                _connectionstringdto.database = System.Configuration.ConfigurationManager.AppSettings["mysql_database"];
                _connectionstringdto.userid = System.Configuration.ConfigurationManager.AppSettings["mysql_userid"];
                _connectionstringdto.password = System.Configuration.ConfigurationManager.AppSettings["mysql_password"];
                _connectionstringdto.port = System.Configuration.ConfigurationManager.AppSettings["mysql_port"];

                CONNECTION_STRING = buildconnectionstringfromobject(_connectionstringdto);

            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
            }
        }

        string buildconnectionstringfromobject(mysqlconnectionstringdto _connectionstringdto)
        {
            string CONNECTION_STRING = @"host=" + _connectionstringdto.datasource + ";" +
                "database=" + _connectionstringdto.database + ";" +
                "port=" + _connectionstringdto.port + ";" +
                "user=" + _connectionstringdto.userid + ";" +
                "password=" + _connectionstringdto.password;
            return CONNECTION_STRING;
        }
        void createdatabaseonfirstload()
        {
            mysqlconnectionstringdto _connectionstringdto = new mysqlconnectionstringdto();

            _connectionstringdto.datasource = System.Configuration.ConfigurationManager.AppSettings["mysql_datasource"];
            _connectionstringdto.database = System.Configuration.ConfigurationManager.AppSettings["mysql_database"];
            _connectionstringdto.userid = System.Configuration.ConfigurationManager.AppSettings["mysql_userid"];
            _connectionstringdto.password = System.Configuration.ConfigurationManager.AppSettings["mysql_password"];
            _connectionstringdto.port = System.Configuration.ConfigurationManager.AppSettings["mysql_port"];
            _connectionstringdto.new_database_name = System.Configuration.ConfigurationManager.AppSettings["mysql_database"];

            createdatabasegivenname(_connectionstringdto);
        }
        void createtablesonfirstload()
        {
            mysqlconnectionstringdto _connectionstringdto = new mysqlconnectionstringdto();

            _connectionstringdto.datasource = System.Configuration.ConfigurationManager.AppSettings["mysql_datasource"];
            _connectionstringdto.database = System.Configuration.ConfigurationManager.AppSettings["mysql_database"];
            _connectionstringdto.userid = System.Configuration.ConfigurationManager.AppSettings["mysql_userid"];
            _connectionstringdto.password = System.Configuration.ConfigurationManager.AppSettings["mysql_password"];
            _connectionstringdto.port = System.Configuration.ConfigurationManager.AppSettings["mysql_port"];
            _connectionstringdto.new_database_name = System.Configuration.ConfigurationManager.AppSettings["mysql_database"];

            createtables(_connectionstringdto);
        }
        public responsedto createdatabasegivenname(mysqlconnectionstringdto _connectionstringdto)
        {
            responsedto _responsedto = new responsedto();
            try
            {
                _connectionstringdto.database = "mysql";

                string CONNECTION_STRING = buildconnectionstringfromobject(_connectionstringdto);

                string query = "CREATE DATABASE " + _connectionstringdto.new_database_name + ";";

                using (var con = new MySqlConnection(CONNECTION_STRING))
                {
                    con.Open();
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                        _responsedto.isresponseresultsuccessful = true;
                        _responsedto.responsesuccessmessage = "successfully created database [ " + _connectionstringdto.new_database_name + " ] in " + DBContract.mysql + ".";
                        _responsedto.responseresultobject = _connectionstringdto;
                        return _responsedto;
                    }
                }
            }
            catch (Exception ex)
            {
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage = ex.Message;
                return _responsedto;
            }
        }
        public bool createdatabase()
        {
            try
            {
                string _default_database_name = utilzsingleton.getInstance(_notificationmessageEventname).getappsettinggivenkey("mysql_database", "nyax_playlist_db");
                string query = "CREATE DATABASE " + _default_database_name + ";";
                using (var con = new MySqlConnection(CONNECTION_STRING))
                {
                    con.Open();
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                return false;
            }
        }
        public responsedto createtables(mysqlconnectionstringdto _connectionstringdto)
        {
            responsedto _responsedto = new responsedto();
            responsedto _innerresponsedto = new responsedto();
            try
            {

                _connectionstringdto.database = _connectionstringdto.new_database_name;
                string CONNECTION_STRING = buildconnectionstringfromobject(_connectionstringdto);

                bool does_table_exist_in_db = checkiftableexists(CONNECTION_STRING, DBContract.campus_entity_table.TABLE_NAME, _connectionstringdto.new_database_name);

                //Create the table 
                string SQL_CREATE_CROPS_TABLE = " CREATE TABLE IF NOT EXISTS " + DBContract.campus_entity_table.TABLE_NAME + " (" +
                      DBContract.campus_entity_table.ID + " INT NOT NULL AUTO_INCREMENT PRIMARY KEY, " +
                      DBContract.campus_entity_table.CAMPUS_NAME + " TEXT, " +
                      DBContract.campus_entity_table.STATUS + " TEXT, " +
                      DBContract.campus_entity_table.CREATED_DATE + " TEXT " +
                      " ) ENGINE = InnoDB; ";

                _innerresponsedto = createtable(SQL_CREATE_CROPS_TABLE, CONNECTION_STRING);
                if (_innerresponsedto.isresponseresultsuccessful)
                    _responsedto.responsesuccessmessage += _innerresponsedto.responsesuccessmessage;
                else
                    _responsedto.responseerrormessage += _innerresponsedto.responseerrormessage;

                string successmsg = "successfully created tables in database [ " + _connectionstringdto.database + " ] - server [ " + DBContract.mysql + " ].";
                int msg_length = successmsg.Length;
                msg_length = msg_length + 1;
                int stars_printed = 0;
                string str_stars = "";
                string str_newline = Environment.NewLine;

                while (stars_printed != msg_length)
                {
                    str_stars += "*";
                    ++stars_printed;
                }

                _responsedto.responsesuccessmessage += str_newline;
                _responsedto.responsesuccessmessage += str_stars;
                _responsedto.responsesuccessmessage += str_newline;
                _responsedto.responsesuccessmessage += successmsg;
                _responsedto.responsesuccessmessage += str_newline;
                _responsedto.responsesuccessmessage += str_newline;
                _responsedto.responsesuccessmessage += str_stars;

                _responsedto.isresponseresultsuccessful = true;
                return _responsedto;

            }
            catch (Exception ex)
            {
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage += Environment.NewLine + ex.Message;
                return _responsedto;
            }
        }

        bool checkiftableexists(string CONNECTION_STRING, string table_name, string database_name)
        {
            try
            {
                string query = "SELECT * FROM information_schema.tables WHERE table_schema = '" + database_name + "' AND table_name = '" + table_name + "' LIMIT 1;";

                using (var con = new MySqlConnection(CONNECTION_STRING))
                {
                    con.Open();
                    //open a new command
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        //execute the query  
                        int _rows_affected = cmd.ExecuteNonQuery();
                        Console.WriteLine("_rows_affected [ " + _rows_affected + " ]");

                        var da = new MySqlDataAdapter(cmd);
                        var dt = new DataTable();
                        da.Fill(dt);
                        da.Dispose();

                        int _rows_count = dt.Rows.Count;
                        if (_rows_count > 0) return true;
                        else return false;
                    }
                }

            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                return false;
            }
        }

        public responsedto createtable(string query, string CONNECTION_STRING)
        {
            responsedto _responsedto = new responsedto();
            try
            {
                //setup the connection to the database
                using (var con = new MySqlConnection(CONNECTION_STRING))
                {
                    con.Open();
                    //open a new command
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        //execute the query  
                        int _rows_affected = cmd.ExecuteNonQuery();
                        Console.WriteLine("_rows_affected [ " + _rows_affected + " ]");

                        _responsedto.isresponseresultsuccessful = true;
                        string[] _conn_arr = CONNECTION_STRING.Split(new char[] { ';' });
                        _conn_arr.SetValue("", _conn_arr.Length - 1);
                        _conn_arr.SetValue("", _conn_arr.Length - 2);
                        string _sanitized_conn_arr = _conn_arr[0] + ";" + _conn_arr[1];
                        _responsedto.responsesuccessmessage += "successfully executed query [ " + query + " ] against connection [ " + _sanitized_conn_arr + " ]." + Environment.NewLine;
                        return _responsedto;
                    }
                }
            }
            catch (Exception ex)
            {
                _responsedto.isresponseresultsuccessful = false;
                Exception _exception = ex.GetBaseException();
                _responsedto.responseerrormessage += Environment.NewLine + "error executing query [ " + query + " ].";
                _responsedto.responseerrormessage += Environment.NewLine + Environment.NewLine + _exception.Message + Environment.NewLine;
                return _responsedto;
            }
        }

        public responsedto checkmysqlconnectionstate()
        {
            responsedto _responsedto = new responsedto();
            try
            {
                mysqlconnectionstringdto _connectionstringdto = getmysqlconnectionstringdto();

                _responsedto = checkconnectionasadmin(_connectionstringdto);

                return _responsedto;
            }
            catch (Exception ex)
            {
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage = ex.Message;
                return _responsedto;
            }
        }

        public mysqlconnectionstringdto getmysqlconnectionstringdto()
        {
            try
            {
                mysqlconnectionstringdto _connectionstringdto = new mysqlconnectionstringdto();

                _connectionstringdto.datasource = System.Configuration.ConfigurationManager.AppSettings["mysql_datasource"];
                _connectionstringdto.database = System.Configuration.ConfigurationManager.AppSettings["mysql_database"];
                _connectionstringdto.userid = System.Configuration.ConfigurationManager.AppSettings["mysql_userid"];
                _connectionstringdto.password = System.Configuration.ConfigurationManager.AppSettings["mysql_password"];
                _connectionstringdto.port = System.Configuration.ConfigurationManager.AppSettings["mysql_port"];

                return _connectionstringdto;

            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                return null;
            }
        }

        public responsedto checkconnectionasadmin(mysqlconnectionstringdto _connectionstringdto)
        {
            responsedto _responsedto = new responsedto();
            try
            {
                string CONNECTION_STRING = @"host=" + _connectionstringdto.datasource + ";" +
                "database=" + _connectionstringdto.database + ";" +
                "port=" + _connectionstringdto.port + ";" +
                "user=" + _connectionstringdto.userid + ";" +
                "password=" + _connectionstringdto.password;

                string query = DBContract.CAMPUS_SELECT_ALL_QUERY;

                int count = getrecordscountgiventable(DBContract.campus_entity_table.TABLE_NAME, CONNECTION_STRING);

                if (count != -1)
                {
                    _responsedto.isresponseresultsuccessful = true;
                    _responsedto.responsesuccessmessage = "connection to mysql successfull. records count [ " + count + " ]";
                    return _responsedto;
                }
                else
                {
                    _responsedto.isresponseresultsuccessful = true;
                    _responsedto.responseerrormessage = "connection to mysql failed.";
                    return _responsedto;
                }
            }
            catch (Exception ex)
            {
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage = ex.Message;
                return _responsedto;
            }
        }

        public int getrecordscountgiventable(string tablename, string CONNECTION_STRING)
        {
            string query = DBContract.CAMPUS_SELECT_ALL_QUERY;
            DataTable dt = getallrecordsglobal(query, CONNECTION_STRING);
            if (dt != null)
                return dt.Rows.Count;
            else
                return -1;
        }
        public bool isdbconnectionalive(string CONNECTION_STRING)
        {
            var con = new MySqlConnection(CONNECTION_STRING);
            con.Open();
            return true;
        }

        public bool isdbconnectionalive()
        {
            try
            {
                //setup the connection to the database
                var con = new MySqlConnection(CONNECTION_STRING);
                con.Open();
                return true;
            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                return false;
            }

        }

        public DataTable getallrecordsglobal(string query, string CONNECTION_STRING)
        {
            if (!isdbconnectionalive(CONNECTION_STRING)) return null;

            if (string.IsNullOrEmpty(query.Trim()))
                return null;
            using (var con = new MySqlConnection(CONNECTION_STRING))
            {
                con.Open();
                using (var cmd = new MySqlCommand(query, con))
                {
                    var da = new MySqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
        }

        public DataTable getallrecordsglobal(string query)
        {
            if (!isdbconnectionalive()) return null;

            if (string.IsNullOrEmpty(query.Trim()))
                return null;
            using (var con = new MySqlConnection(CONNECTION_STRING))
            {
                con.Open();
                using (var cmd = new MySqlCommand(query, con))
                {
                    var da = new MySqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
        }
        public DataTable getallrecordsglobal()
        {
            DataTable dt = getallrecordsglobal(DBContract.CAMPUS_SELECT_ALL_QUERY);
            return dt;
        }

        public List<campus_dto> lst_get_all_campuses()
        {
            List<campus_dto> lst_records = new List<campus_dto>();
            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                using (MySqlCommand cmd = new MySqlCommand(DBContract.CAMPUS_SELECT_ALL_QUERY, conn))
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        campus_dto dto = new campus_dto();
                        dto.id = reader.GetInt32(0).ToString();
                        dto.campus_name = reader.GetString(1);
                        dto.status = reader.GetString(2);
                        dto.created_date = reader.GetString(3);

                        lst_records.Add(dto);
                    }
                }
            }
            return lst_records;
        }

        public List<department_dto> lst_get_all_departments()
        {
            List<department_dto> lst_records = new List<department_dto>();
            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                using (MySqlCommand cmd = new MySqlCommand(DBContract.DEPARTMENT_SELECT_ALL_QUERY, conn))
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        department_dto dto = new department_dto();
                        dto.id = reader.GetInt32(0).ToString();
                        dto.campus_id = reader.GetInt32(1).ToString();
                        dto.department_name = reader.GetString(2);
                        dto.status = reader.GetString(3);
                        dto.created_date = reader.GetString(4);

                        lst_records.Add(dto);
                    }
                }
            }
            return lst_records;
        }

        public List<extension_dto> lst_get_all_extensions()
        {
            List<extension_dto> lst_records = new List<extension_dto>();
            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                using (MySqlCommand cmd = new MySqlCommand(DBContract.EXTENSION_SELECT_ALL_QUERY, conn))
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        extension_dto dto = new extension_dto();
                        dto.id = reader.GetInt32(0).ToString();
                        dto.campus_id = reader.GetInt32(1).ToString();
                        dto.department_id = reader.GetInt32(2).ToString();
                        dto.owner_assigned = reader.GetString(3);
                        dto.extension_number = reader.GetString(4);
                        dto.status = reader.GetString(5);
                        dto.created_date = reader.GetString(6);

                        lst_records.Add(dto);
                    }
                }
            }
            return lst_records;
        }

        public campus_dto get_campus_given_id(string id)
        {
            campus_dto campus = lst_get_all_campuses().Where(i => i.id == id).FirstOrDefault();
            return campus;
        }

        public campus_dto get_campus_given_name(string campus_name)
        {
            campus_dto campus = lst_get_all_campuses().Where(i => String.Equals(i.campus_name, campus_name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            return campus;
        }

        public department_dto get_department_given_id(string id)
        {
            department_dto department = lst_get_all_departments().Where(i => i.id == id).FirstOrDefault();
            return department;
        }

        public department_dto get_department_given_name(string department_name)
        {
            department_dto department = lst_get_all_departments().Where(i => String.Equals(i.department_name, department_name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            return department;
        }

        public department_dto get_department_given_campus_name_and_department_name(string campus_name, string department_name)
        {
            department_dto department = null;

            string query = "SELECT * FROM " + DBContract.department_entity_table.TABLE_NAME + " as departments " +
                "INNER JOIN " + DBContract.campus_entity_table.TABLE_NAME + " as campuses ON departments.campus_id = campuses.id " +
                "WHERE departments.department_name = @department_name AND campuses.campus_name = @campus_name";

            using (var con = new MySqlConnection(CONNECTION_STRING))
            {
                con.Open();
                //open a new command
                using (var cmd = new MySqlCommand(query, con))
                {
                    //here we are setting the parameter values that will be actually replaced in the query in Execute method                
                    cmd.Parameters.AddWithValue("@campus_name", campus_name);
                    cmd.Parameters.AddWithValue("@department_name", department_name);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        department = new department_dto();
                        department.id = reader.GetInt32(0).ToString();
                        department.campus_id = reader.GetInt32(1).ToString();
                        department.department_name = reader.GetString(2).ToString();
                        department.status = reader.GetString(3);
                        department.created_date = reader.GetString(4);

                    }
                }
            }

            return department;
        }

        public extension_dto get_extension_given_extension_number(string extension_number)
        {
            extension_dto extension = lst_get_all_extensions().Where(i => i.extension_number == extension_number).FirstOrDefault();
            return extension;
        }

        public int insertgeneric(string query, Dictionary<string, object> args)
        {
            int numberOfRowsAffected;
            //setup the connection to the database
            using (var con = new MySqlConnection(CONNECTION_STRING))
            {
                con.Open();
                //open a new command
                using (var cmd = new MySqlCommand(query, con))
                {
                    //set the arguments given in the query
                    foreach (var pair in args)
                    {
                        cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                    }
                    //execute the query and get the number of row affected
                    numberOfRowsAffected = cmd.ExecuteNonQuery();
                }
                return numberOfRowsAffected;
            }
        }

        public int insertgeneric(string query, Dictionary<string, object> args, string CONNECTION_STRING)
        {
            int numberOfRowsAffected;
            if (CONNECTION_STRING == null)
            {
                numberOfRowsAffected = insertgeneric(query, args);
                return numberOfRowsAffected;
            }
            else if (String.IsNullOrEmpty(CONNECTION_STRING))
            {
                numberOfRowsAffected = insertgeneric(query, args);
                return numberOfRowsAffected;
            }
            else
            {

                //setup the connection to the database
                using (var con = new MySqlConnection(CONNECTION_STRING))
                {
                    con.Open();
                    //open a new command
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        //set the arguments given in the query
                        foreach (var pair in args)
                        {
                            cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                        }
                        //execute the query and get the number of row affected
                        numberOfRowsAffected = cmd.ExecuteNonQuery();
                    }
                    return numberOfRowsAffected;
                }
            }
        }

        public responsedto create_campus_from_upload(string campus_name)
        {
            responsedto _responsedto = new responsedto();
            try
            {
                string query = "INSERT INTO " +
                DBContract.campus_entity_table.TABLE_NAME +
                " ( " +
                DBContract.campus_entity_table.CAMPUS_NAME + ", " +
                DBContract.campus_entity_table.STATUS + ", " +
                DBContract.campus_entity_table.CREATED_DATE +
                " ) VALUES(@campus_name, @status, @created_date)";

                //here we are setting the parameter values that will be actually replaced in the query in Execute method
                var args = new Dictionary<string, object>
			    {
				    {"@campus_name", campus_name},
                    {"@status", "active"},
				    {"@created_date", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss tt")}
			    };

                int numberOfRowsAffected = insertgeneric(query, args, CONNECTION_STRING);
                if (numberOfRowsAffected != 1)
                {
                    _responsedto.isresponseresultsuccessful = false;
                    _responsedto.responseerrormessage = "Record creation failed in " + DBContract.mysql + ".";
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_responsedto.responseerrormessage, TAG));
                    return _responsedto;
                }
                else
                {
                    _responsedto.isresponseresultsuccessful = true;
                    _responsedto.responsesuccessmessage = "Record created successfully in " + DBContract.mysql + ".";
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_responsedto.responsesuccessmessage, TAG));
                    return _responsedto;
                }

            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage = ex.Message;
                return _responsedto;
            }
        }

        public responsedto create_department_from_upload(string campus_id, string department_name)
        {
            responsedto _responsedto = new responsedto();
            try
            {
                string query = "INSERT INTO " +
                DBContract.department_entity_table.TABLE_NAME +
                " ( " +
                DBContract.department_entity_table.CAMPUS_ID + ", " +
                DBContract.department_entity_table.DEPARTMENT_NAME + ", " +
                DBContract.department_entity_table.STATUS + ", " +
                DBContract.department_entity_table.CREATED_DATE +
                " ) VALUES(@campus_id, @department_name, @status, @created_date)";

                //here we are setting the parameter values that will be actually replaced in the query in Execute method
                var args = new Dictionary<string, object>
			    {
				    {"@campus_id", campus_id},
                    {"@department_name", department_name},
                    {"@status", "active"},
				    {"@created_date", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss tt")}
			    };

                int numberOfRowsAffected = insertgeneric(query, args, CONNECTION_STRING);
                if (numberOfRowsAffected != 1)
                {
                    _responsedto.isresponseresultsuccessful = false;
                    _responsedto.responseerrormessage = "Record creation failed in " + DBContract.mysql + ".";
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_responsedto.responseerrormessage, TAG));
                    return _responsedto;
                }
                else
                {
                    _responsedto.isresponseresultsuccessful = true;
                    _responsedto.responsesuccessmessage = "Record created successfully in " + DBContract.mysql + ".";
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_responsedto.responsesuccessmessage, TAG));
                    return _responsedto;
                }

            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage = ex.Message;
                return _responsedto;
            }
        }

        public responsedto create_extension_from_upload(string campus_id, string department_id, string owner_assigned, string extension_number)
        {
            responsedto _responsedto = new responsedto();
            try
            {
                string query = "INSERT INTO " +
                DBContract.extension_entity_table.TABLE_NAME +
                " ( " +
                DBContract.extension_entity_table.CAMPUS_ID + ", " +
                DBContract.extension_entity_table.DEPARTMENT_ID + ", " +
                DBContract.extension_entity_table.OWNER_ASSIGNED + ", " +
                DBContract.extension_entity_table.EXTENSION_NUMBER + ", " +
                DBContract.extension_entity_table.STATUS + ", " +
                DBContract.extension_entity_table.CREATED_DATE +
                " ) VALUES(@campus_id, @department_id, @owner_assigned, @extension_number, @status, @created_date)";

                //here we are setting the parameter values that will be actually replaced in the query in Execute method
                var args = new Dictionary<string, object>
			    {
				    {"@campus_id", campus_id},
                    {"@department_id", department_id},
                    {"@owner_assigned", owner_assigned},
                    {"@extension_number", extension_number},
                    {"@status", "active"},
				    {"@created_date", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss tt")}
			    };

                int numberOfRowsAffected = insertgeneric(query, args, CONNECTION_STRING);
                if (numberOfRowsAffected != 1)
                {
                    _responsedto.isresponseresultsuccessful = false;
                    _responsedto.responseerrormessage = "Record creation failed in " + DBContract.mysql + ".";
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_responsedto.responseerrormessage, TAG));
                    return _responsedto;
                }
                else
                {
                    _responsedto.isresponseresultsuccessful = true;
                    _responsedto.responsesuccessmessage = "Record created successfully in " + DBContract.mysql + ".";
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_responsedto.responsesuccessmessage, TAG));
                    return _responsedto;
                }

            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage = ex.Message;
                return _responsedto;
            }
        }

        public responsedto login(string email, string password)
        {
            responsedto _responsedto = new responsedto();

            user_dto user = null;

            string query = "SELECT * FROM " + DBContract.user_entity_table.TABLE_NAME +
                " WHERE email = @email AND password = @password";

            using (var con = new MySqlConnection(CONNECTION_STRING))
            {
                con.Open();
                //open a new command
                using (var cmd = new MySqlCommand(query, con))
                {
                    //here we are setting the parameter values that will be actually replaced in the query in Execute method                  
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        user = new user_dto();
                        user.id = reader.GetInt32(0).ToString();
                        user.email = reader.GetString(1);
                        user.full_names = reader.GetString(2);
                        user.password = reader.GetString(3);
                        user.secretword = reader.GetString(4);
                        user.status = reader.GetString(5);
                        user.created_date = reader.GetString(6);
                    }
                }
            }

            if (user == null)
            {
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage = "Login failed. Check Email and Password.";
            }
            else
            {
                _responsedto.isresponseresultsuccessful = true;
                _responsedto.responsesuccessmessage = "Login successfull.";
            }

            return _responsedto;
        }








    }
}
