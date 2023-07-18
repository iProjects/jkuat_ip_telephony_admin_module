using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using jkuat_ip_telephony_dal;
//--- Add the following to make itext work
using iTextSharp.text;
using iTextSharp.text.pdf;
using VVX;

namespace jkuat_ip_telephony_ui
{
    public class PDFGen
    {

        #region "Properties"
        private bool bRet = false;
        private string resourcePath;
        private string sMsg = "";
        string connection;
        int _counter = 0;
        public string Message
        {
            get { return sMsg; }
            set { sMsg = value; }
        }
        public bool Success
        {
            get { return bRet; }
            set { bRet = value; }
        }
        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        #endregion "Properties"

        #region "Constructor"
        public PDFGen(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            _notificationmessageEventname = notificationmessageEventname;
        }
        public PDFGen(string ResourcePath, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            _notificationmessageEventname = notificationmessageEventname;

            resourcePath = ResourcePath;

        }
        #endregion "Constructor"

        #region "Helper methods"
        /// <summary>
        /// Safely attempts to insert an image file into the document
        /// </summary>
        /// <param name="document">iTextSharp Document in which it needs to be inserted</param>
        /// <param name="sFilename">the name of the file to be inserted</param>
        /// <returns>false if failed to do so</returns>
        private bool DoInsertImageFile(Document document, string sFilename, bool bInsertMsg)
        {
            bool bRet = false;

            try
            {
                if (VVX.File.Exists(sFilename) == false)
                {
                    string sMsg = "Unable to find '" + sFilename + "' in the current folder.\n\n"
                                + "Would you like to locate it?";
                    if (MsgBox.Confirm(sMsg))
                        sFilename = FileDialog.GetFilenameToOpen(FileDialog.FileType.Image);
                }

                Image img = null;
                if (VVX.File.Exists(sFilename))
                {
                    this.DoGetImageFile(sFilename, out img);
                }

                if (img != null)
                {
                    document.Add(img);
                    bRet = true;
                }
                else
                {
                    if (bInsertMsg)
                        document.Add(new Paragraph(sFilename + " not found"));
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }

            return bRet;
        }
        public Image DoGetImageFile(string sFilename)
        {
            bool bRet = false;
            Image img = null;

            try
            {
                if (VVX.File.Exists(sFilename) == false)
                {
                    string sMsg = "Unable to find '" + sFilename + "' in the current folder.\n\n"
                                + "Would you like to locate it?";
                    if (MsgBox.Confirm(sMsg))
                        sFilename = FileDialog.GetFilenameToOpen(FileDialog.FileType.Image);
                }

                if (VVX.File.Exists(sFilename))
                {
                    img = Image.GetInstance(sFilename);
                }

                bRet = (img != null);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }

            return img;
        }
        private bool DoGetImageFile(string sFilename, out Image img)
        {
            bool bRet = false;
            img = null;

            try
            {
                if (VVX.File.Exists(sFilename) == false)
                {
                    string sMsg = "Unable to find '" + sFilename + "' in the current folder.\n\n"
                                + "Would you like to locate it?";
                    if (MsgBox.Confirm(sMsg))
                        sFilename = FileDialog.GetFilenameToOpen(FileDialog.FileType.Image);
                }

                if (VVX.File.Exists(sFilename))
                {
                    img = Image.GetInstance(sFilename);
                }

                bRet = (img != null);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }

            return bRet;
        }
        private bool DoLocateImageFile(ref string sFilename)
        {
            bool bRet = false;

            try
            {
                if (VVX.File.Exists(sFilename) == false)
                {
                    string sMsg = "Unable to find '" + sFilename + "' in the current folder.\n\n"
                                + "Would you like to locate it?";

                    if (MsgBox.Confirm(sMsg))
                        sFilename = FileDialog.GetFilenameToOpen(FileDialog.FileType.Image);
                }

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }

            return bRet = VVX.File.Exists(sFilename);
        }
        #endregion "Helper methods"

        #region campuses
        public bool show_campuses_pdf(campus_model_report model, string sFilePDF)
        { 
            bRet = false;
            try
            { 
                campus_pdf_builder campus_pdf_builder = new campus_pdf_builder(model, sFilePDF, _notificationmessageEventname);
                campus_pdf_builder.get_campus_pdf();
                return true; 
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }

        }
        public bool show_campuses_excel(campus_model_report model, string sFilePDF)
        {
            bRet = false;
            try
            {
                campus_excel_builder campus_excel_builder = new campus_excel_builder(model, sFilePDF, _notificationmessageEventname);
                campus_excel_builder.get_campus_excel();
                return true;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }

        }
        #endregion

        #region departments
        public bool show_departments_pdf(department_model_report model, string sFilePDF)
        {
            bRet = false;
            try
            {
                department_pdf_builder department_pdf_builder = new department_pdf_builder(model, sFilePDF, _notificationmessageEventname);
                department_pdf_builder.get_department_pdf();
                return true;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }

        }
        public bool show_departments_excel(department_model_report model, string sFilePDF)
        {
            bRet = false;
            try
            {
                department_excel_builder department_excel_builder = new department_excel_builder(model, sFilePDF, _notificationmessageEventname);
                department_excel_builder.get_department_excel();
                return true;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }

        }
        #endregion

        #region extensions
        public bool show_extensions_pdf(extension_model_report model, string sFilePDF)
        {
            bRet = false;
            try
            {
                extension_pdf_builder extension_pdf_builder = new extension_pdf_builder(model, sFilePDF, _notificationmessageEventname);
                extension_pdf_builder.get_extension_pdf();
                return true;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }

        }
        public bool show_extensions_excel(extension_model_report model, string sFilePDF)
        {
            bRet = false;
            try
            {
                extension_excel_builder extension_excel_builder = new extension_excel_builder(model, sFilePDF, _notificationmessageEventname);
                extension_excel_builder.get_campus_excel();
                return true;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }

        }
        #endregion


    }
}