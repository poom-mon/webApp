using System;
using System.Xml;
 
    public class cXML
    {
        public XmlNodeList ExecuteXML(String queryXML, String ptPathXml)
        {
            try
            {
                string xmlPath = ptPathXml;//Server.MapPath(ptPathXml);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNodeList itemNodes = xmlDoc.SelectNodes(queryXML);
                return itemNodes;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
 