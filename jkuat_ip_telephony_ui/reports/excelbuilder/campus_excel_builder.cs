using System;
using System.IO;
using jkuat_ip_telephony_dal;

namespace jkuat_ip_telephony_ui
{
    public class campus_excel_builder
    {
        //private attributes 
        campus_model_report _model;
        CreateExcelDoc document;
        string Message;
        string sFileExcel;
        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        //constructor
        public campus_excel_builder(campus_model_report model, string FileName, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            _notificationmessageEventname = notificationmessageEventname;

            _model = model;
            sFileExcel = FileName;
        }

        public string get_campus_excel()
        {
            BuildExcel();
            document.Save(sFileExcel);
            return sFileExcel;
        }


        /*Build the document **/
        private void BuildExcel()
        {
            // step 1: creation of a document-object
            document = new CreateExcelDoc();

            try
            {
                //Add  Header 
                int row = 1;
                int col = 1;

                //AddDocHeader(ref row, ref col);

                //Add  Body
                AddDocBody(ref row, ref col);

                //Add Footer
                AddDocFooter(ref row, ref col);

            }
            catch (IOException ioe)
            {
                this.Message = ioe.Message;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }

        }

        /*Build the document**/
        private void AddDocHeader(ref int row, ref int col)
        {

            //createHeaders(int row, int col, string htext, string cell1, string cell2, int mergeColumns, string b, bool font, int size, string fcolor)

            row = 1;
            col = 2;

            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _model.employername, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _model.employeraddress, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Printed on: " + _model.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

        }


        private void AddDocBody(ref int row, ref int col)
        {
            //Add table headers
            AddBodytableHeaders(ref  row, ref  col);

            //Add table detail
            foreach (var d in _model.campuses)
            {
                AddBodyTableDetail(d, ref  row, ref  col);
            }

            //Add table footer
            //AddDocBodyTableTotals(ref  row, ref  col);

        }

        //table headers
        private void AddBodytableHeaders(ref int row, ref int col)
        {
            //row 1
            //row = row + 2;
            row = 1;
            col = 1;

            string cellrangeaddr1 = document.IntAlpha(col) + row;
            //document.createHeaders(row, col, "NO", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            //col++;
            //cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "CAMPUSNAME", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            //col++;
            //cellrangeaddr1 = document.IntAlpha(col) + row;
            //document.createHeaders(row, col, "STATUS", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            //col++;
            //cellrangeaddr1 = document.IntAlpha(col) + row;
            //document.createHeaders(row, col, "CREATED DATE", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");
            //col++;

        }

        //table details
        private void AddBodyTableDetail(print_campuses campus_model, ref int row, ref int col)
        {

            row++;
            col = 1;

            string cellrangeaddr1 = document.IntAlpha(col) + row;
            //document.createHeaders(row, col, campus_model.id, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            //col++;
            //cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, campus_model.campus_name, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            //col++;
            //cellrangeaddr1 = document.IntAlpha(col) + row;
            //document.createHeaders(row, col, campus_model.status, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            //col++;
            //cellrangeaddr1 = document.IntAlpha(col) + row;
            //document.createHeaders(row, col, campus_model.created_date, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

        }

        //table footer
        private void AddDocBodyTableTotals(ref int row, ref int col)
        {
            row++;
            col = 1;

            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Total Records", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _model.total_reords.ToString(), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

        }

        //document footer
        private void AddDocFooter(ref int row, ref int col)
        {


        }

    }
}
