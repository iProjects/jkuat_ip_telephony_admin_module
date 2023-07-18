using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace jkuat_ip_telephony_dal
{
    public static class DBContract
    {
        public static String DATABASE_NAME = "ip_telephony_db";
        public static String SQLITE_DATABASE_NAME = "ip_telephony_db.sqlite3";

        public static String error = "error";
        public static String info = "info";
        public static String warn = "warn"; 

        public static String mssql = "mssql";
        public static String mysql = "mysql";
        public static String sqlite = "sqlite";
        public static String postgresql = "postgresql";
         
        public static DialogResult dialogresult;

        public static String CAMPUS_SELECT_ALL_QUERY = "SELECT * FROM " +
                                    DBContract.campus_entity_table.TABLE_NAME;

        public static String CAMPUS_SELECT_ALL_FILTER_QUERY = "SELECT * FROM " +
                            DBContract.campus_entity_table.TABLE_NAME +
                            " where " +
                            DBContract.campus_entity_table.STATUS +
                            " = " +
                            "'active'";

        public static String DEPARTMENT_SELECT_ALL_QUERY = "SELECT * FROM " +
                                    DBContract.department_entity_table.TABLE_NAME;

        public static String DEPARTMENT_SELECT_ALL_FILTER_QUERY = "SELECT * FROM " +
                            DBContract.department_entity_table.TABLE_NAME +
                            " where " +
                            DBContract.department_entity_table.STATUS +
                            " = " +
                            "'active'";

        public static String EXTENSION_SELECT_ALL_QUERY = "SELECT * FROM " +
                                    DBContract.extension_entity_table.TABLE_NAME;

        public static String EXTENSION_SELECT_ALL_FILTER_QUERY = "SELECT * FROM " +
                            DBContract.extension_entity_table.TABLE_NAME +
                            " where " +
                            DBContract.extension_entity_table.STATUS +
                            " = " +
                            "'active'";

        //playlist table
        public static class campus_entity_table
        {
            public static String TABLE_NAME = "tbl_campuses";
            //Columns of the table
            public static String ID = "id";
            public static String CAMPUS_NAME = "campus_name";
            public static String STATUS = "status";
            public static String CREATED_DATE = "created_date";

        }
        public static class department_entity_table
        {
            public static String TABLE_NAME = "tbl_departments";
            //Columns of the table
            public static String ID = "id";
            public static String DEPARTMENT_NAME = "department_name";
            public static String CAMPUS_ID = "campus_id";
            public static String STATUS = "status";
            public static String CREATED_DATE = "created_date";

        }
        public static class extension_entity_table
        {
            public static String TABLE_NAME = "tbl_extensions";
            //Columns of the table
            public static String ID = "id";
            public static String CAMPUS_ID = "campus_id";
            public static String DEPARTMENT_ID = "department_id";
            public static String OWNER_ASSIGNED = "owner_assigned";
            public static String EXTENSION_NUMBER = "extension_number";
            public static String STATUS = "status";
            public static String CREATED_DATE = "created_date";

        }
 



    }
}
