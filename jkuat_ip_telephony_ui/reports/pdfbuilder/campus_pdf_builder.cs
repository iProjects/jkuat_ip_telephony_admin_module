using System;
using System.IO;
using jkuat_ip_telephony_dal;
//--- Add the following to make itext work
using iTextSharp.text;
using iTextSharp.text.pdf;
using VVX;

namespace jkuat_ip_telephony_ui
{
    public class campus_pdf_builder
    {
        Document document;
        string Message;
        string sFilePDF;
        campus_model_report _model;

        Font hFont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        Font hfont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font hFont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font bfont1 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);//body
        Font bFont2 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);//body
        Font bFont3 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);//body
        Font tHFont = new Font(Font.TIMES_ROMAN, 9, Font.BOLD); //table Header
        Font tHfont1 = new Font(Font.TIMES_ROMAN, 11, Font.BOLD); //table Header
        Font tcFont = new Font(Font.HELVETICA, 8, Font.NORMAL);//table cell
        Font rms6Normal = new Font(Font.TIMES_ROMAN, 9, Font.NORMAL);
        Font rms10Bold = new Font(Font.HELVETICA, 10, Font.BOLD);
        Font rms6Bold = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font rms8Bold = new Font(Font.HELVETICA, 8, Font.BOLD);
        Font rms9Bold = new Font(Font.HELVETICA, 9, Font.BOLD);
        Font rms10Normal = new Font(Font.HELVETICA, 10, Font.NORMAL);
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        public campus_pdf_builder(campus_model_report model, string FileName, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (model == null)
                throw new ArgumentNullException("campus_model_report is null");
            _model = model;

            _notificationmessageEventname = notificationmessageEventname;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("initialized campus_pdf_builder", TAG));

            sFilePDF = FileName;

        }

        public string get_campus_pdf()
        {
            try
            {
                BuildPF();
                return sFilePDF;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        /*Build the document **/
        private void BuildPF()
        {
            try
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("building the pdf...", TAG));

                //step 1 creation of the document
                document = new Document(PageSize.A4.Rotate());

                // step 2: we create a writer that listens to the document
                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                //open the document
                document.Open();

                //add header
                AddDocHeader();

                //add body
                AddDocBody();

                //add footer
                AddDocFooter();

                //close the document
                document.Close();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("document generated successfullt.", TAG));
            }
            catch (DocumentException de)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(de.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(de);
            }
            catch (IOException ioe)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ioe.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ioe);
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }

        }

        private void AddDocHeader()
        {
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document headers...", TAG));

            Table pdf_table = new Table(5);
            pdf_table.WidthPercentage = 100;
            pdf_table.Padding = 1;
            pdf_table.Spacing = 1;
            pdf_table.Border = Table.NO_BORDER;

            Cell emptyCell = new Cell(new Phrase("", hFont2));
            emptyCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            emptyCell.Colspan = 2;
            emptyCell.Border = Cell.NO_BORDER;
            pdf_table.AddCell(emptyCell);

            //create the logo
            PDFGen pdfgen = new PDFGen(_notificationmessageEventname);
            Image img0 = pdfgen.DoGetImageFile(_model.CompanyLogo);
            img0.Alignment = Image.ALIGN_CENTER;
            Cell logoCell = new Cell(img0);
            logoCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            logoCell.Border = Cell.NO_BORDER;
            logoCell.Add(new Phrase(_model.CompanySlogan, new Font(Font.HELVETICA, 8, Font.ITALIC, Color.BLACK)));
            pdf_table.AddCell(logoCell);

            Cell emptyCell1 = new Cell(new Phrase("", hFont2));
            emptyCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
            emptyCell1.Colspan = 2;
            emptyCell1.Border = Cell.NO_BORDER;
            pdf_table.AddCell(emptyCell1);

            Cell organization_name = new Cell(new Phrase(_model.employername.ToUpper(), new Font(Font.TIMES_ROMAN, 12, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            organization_name.HorizontalAlignment = Cell.ALIGN_CENTER;
            organization_name.Colspan = 5;
            organization_name.Border = Cell.NO_BORDER;
            pdf_table.AddCell(organization_name);

            Cell report_title = new Cell(new Phrase("CAMPUSES REPORT", hFont1));
            report_title.HorizontalAlignment = Cell.ALIGN_CENTER;
            report_title.Colspan = 5;
            report_title.Border = Cell.NO_BORDER;
            pdf_table.AddCell(report_title);

            Cell PrintedonCell = new Cell(new Phrase("Printed on: " + _model.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PrintedonCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            PrintedonCell.Colspan = 5;
            PrintedonCell.Border = Cell.NO_BORDER;
            pdf_table.AddCell(PrintedonCell);

            document.Add(pdf_table);
        }

        private void AddDocBody()
        {

            //Add table headers
            AddTableHeaders();

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating table details...", TAG));

            //Add table details  
            foreach (var d in _model.campuses)
            {
                AddTableDetails(d);
            }

            //Add table totals
            //AddTableTotals();

        }

        //table headers
        private void AddTableHeaders()
        {
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating table headers...", TAG));

            Table pdf_table = new Table(4);
            pdf_table.WidthPercentage = 100;
            pdf_table.Padding = 1;
            pdf_table.Spacing = 1;

            Cell no = new Cell(new Phrase("No", tHFont));
            no.Border = Cell.RECTANGLE;
            no.HorizontalAlignment = Cell.ALIGN_CENTER;
            pdf_table.AddCell(no);

            Cell campus_name = new Cell(new Phrase("Name", tHFont));
            campus_name.Border = Cell.RECTANGLE;
            campus_name.HorizontalAlignment = Cell.ALIGN_CENTER;
            pdf_table.AddCell(campus_name);

            Cell status = new Cell(new Phrase("Status", tHFont));
            status.Border = Cell.RECTANGLE;
            status.HorizontalAlignment = Cell.ALIGN_CENTER;
            pdf_table.AddCell(status);

            Cell created_date = new Cell(new Phrase("Created \nDate", tHFont));
            created_date.Border = Cell.RECTANGLE;
            created_date.HorizontalAlignment = Cell.ALIGN_CENTER;
            pdf_table.AddCell(created_date);

            document.Add(pdf_table);
        }

        //table details 
        private void AddTableDetails(print_campuses campus_model)
        {
            
            Table pdf_table = new Table(4);
            pdf_table.WidthPercentage = 100;
            pdf_table.Padding = 1;
            pdf_table.Spacing = 1;

            Cell no = new Cell(new Phrase(campus_model.id.ToString(), rms8Bold));
            no.Border = Cell.RECTANGLE;
            no.HorizontalAlignment = Cell.ALIGN_CENTER;
            pdf_table.AddCell(no);

            Cell campus_name = new Cell(new Phrase(campus_model.campus_name.ToString(), rms8Bold));
            campus_name.Border = Cell.RECTANGLE;
            campus_name.HorizontalAlignment = Cell.ALIGN_CENTER;
            pdf_table.AddCell(campus_name);

            Cell status = new Cell(new Phrase(campus_model.status, rms8Bold));
            status.Border = Cell.RECTANGLE;
            status.HorizontalAlignment = Cell.ALIGN_CENTER;
            pdf_table.AddCell(status);

            Cell created_date = new Cell(new Phrase(campus_model.created_date, rms8Bold));
            created_date.Border = Cell.RECTANGLE;
            created_date.HorizontalAlignment = Cell.ALIGN_CENTER;
            pdf_table.AddCell(created_date);

            document.Add(pdf_table);

        }

        //table totals
        private void AddTableTotals()
        {
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating table totals...", TAG));

            Table pdf_table = new Table(4);
            pdf_table.WidthPercentage = 100;
            pdf_table.Padding = 1;
            pdf_table.Spacing = 1;

            Cell total_title = new Cell(new Phrase("TOTAL", tHfont1));
            total_title.Border = Cell.RECTANGLE;
            total_title.HorizontalAlignment = Cell.ALIGN_CENTER;
            total_title.Colspan = 3;
            pdf_table.AddCell(total_title);

            Cell total_value = new Cell(new Phrase(_model.total_reords.ToString(), tHfont1));
            total_value.Border = Cell.RECTANGLE;
            total_value.HorizontalAlignment = Cell.ALIGN_CENTER;
            pdf_table.AddCell(total_value);

            document.Add(pdf_table);
        }



        //document footer
        private void AddDocFooter()
        {
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document footer...", TAG));

            Table pdf_table = new Table(1);
            pdf_table.WidthPercentage = 100;
            pdf_table.Border = Table.NO_BORDER;

            Cell signature = new Cell(new Phrase("Signature.....................................................................................................", rms10Normal));
            signature.HorizontalAlignment = Cell.ALIGN_LEFT;
            signature.Border = Cell.NO_BORDER;
            pdf_table.AddCell(signature);

            document.Add(pdf_table);

        }



    }
}
