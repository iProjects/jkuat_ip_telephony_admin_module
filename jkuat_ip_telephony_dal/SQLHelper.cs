using System;
using System.Collections.Generic; 
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Text; 
using System.Collections.Specialized; 
 
namespace jkuat_ip_telephony_dal
{
    public class SQLHelper
    {
        #region "public methods"
        static public List<SBSystem> GetDataFromXML()
        {
            return GetDataFromXML("Security/Systems.xml");
        }
        static public List<SBSystem> GetDataFromXML(string filename)
        {
            using (XmlReader xmlRdr = new XmlTextReader(filename))
            {
                return new List<SBSystem>(
                   (from sysElem in XDocument.Load(xmlRdr).Element("Systems").Elements("System")
                    select new SBSystem(
                       (string)sysElem.Attribute("Name"),
                       (string)sysElem.Attribute("Application"),
                       (string)sysElem.Attribute("Database"),
                       (string)sysElem.Attribute("Server"),
                       (string)sysElem.Attribute("AttachDB"),
                       (string)sysElem.Attribute("Metadata"),
                       (string)sysElem.Attribute("Version"),
                       (bool)sysElem.Attribute("Default")
                        )).ToList());
            }
        }
        static public List<SBSystem_Exp> GetDataFromSBSystem_ExpXML(string filename)
        {
            using (XmlReader xmlRdr = new XmlTextReader(filename))
            {
                return new List<SBSystem_Exp>(
                   (from sysElem in XDocument.Load(xmlRdr).Element("Systems").Elements("System")
                    select new SBSystem_Exp(
                       (string)sysElem.Attribute("Name"),
                       (string)sysElem.Attribute("Application")
                        )).ToList());
            }
        }
        static public List<SBSystem_DTO> GetDataFromSBSystem_DTOXML(string filename)
        {
            using (XmlReader xmlRdr = new XmlTextReader(filename))
            {
                return new List<SBSystem_DTO>(
                   (from sysElem in XDocument.Load(xmlRdr).Element("Systems").Elements("System")
                    select new SBSystem_DTO(
                       (string)sysElem.Attribute("Name"),
                       (string)sysElem.Attribute("Application")
                        )).ToList());
            }
        }
        static public void SaveXML(List<SBSystem> systems, string filename)
        {
            var xml = new XElement("Systems", systems.Select(x => new XElement("System",
                                                new XAttribute("Name", x.Name),
                                                new XAttribute("Application", x.Application),
                                                new XAttribute("Database", x.Database),
                                                new XAttribute("Server", x.Server),
                                                new XAttribute("AttachDB", x.AttachDB),
                                                new XAttribute("Metadata", x.Metadata),
                                                new XAttribute("Version", x.Version),
                                                new XAttribute("Default", x.Default))));
            xml.Save(filename);
        }
        static public void SaveXML(string info, string filename)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(info);
            xdoc.Save(filename);
        }
        static public SBSystem GetDataDefaultSystem()
        {
            var db = from dbs in GetDataFromXML()
                     where dbs.Default
                     select dbs;
            return db.FirstOrDefault();
        }
         
        static void showExceptionError(string msg, Exception ex)
        {
            Utils.LogEventViewer(ex); 
            msg += ex.Message;
            if (ex.InnerException != null)
                msg += "\n" + ex.InnerException.Message;
            System.Windows.Forms.MessageBox.Show(msg, Utils.APP_NAME, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }
        #endregion "public methods"
    }

}

