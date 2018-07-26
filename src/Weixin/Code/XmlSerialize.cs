using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Weixin.Code
{
    public class XmlSerialize : ISerialize
    {
        /// <summary>
        /// Xml转Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <summary>
        public T XmlToModel<T>(string xml, string typeName)
        {
            xml = xml.Replace("xml>", typeName + ">");            
            StringReader xmlReader = new StringReader(xml);
            XmlSerializer xmlSer = new XmlSerializer(typeof(T));
            return (T)xmlSer.Deserialize(xmlReader);
        }
        /// <summary>
        /// Model转Xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public string ModelToXml<T>(T model)
        {
            MemoryStream stream = new MemoryStream();
            XmlSerializer xmlSer = new XmlSerializer(typeof(T));
            xmlSer.Serialize(stream, model);

            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            return sr.ReadToEnd();
        }
        /// <summary>
        /// Xml转DataTable
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public System.Data.DataTable XmlToTable(string xml)
        {
            StringReader xmlReader = new StringReader(xml);
            DataSet ds = new DataSet();
            ds.ReadXml(xmlReader);
            return ds.Tables[0];
        }
        /// <summary>
        /// Xml解析(获取节点值)
        /// </summary>
        /// <param name="stringRoot"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public string XmlAnalysis(string stringRoot, string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc.DocumentElement.SelectSingleNode(stringRoot).InnerXml.Trim();
        }
    }  
}