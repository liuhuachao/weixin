using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Weixin.Code
{
    interface ISerialize
    {
        /// <summary>  
        /// xml 转换为 model  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="xml"></param>  
        /// <returns></returns>  
        T XmlToModel<T>(string xml,string typeName);

        /// <summary>  
        /// model 转换为xml  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="model"></param>  
        /// <returns></returns>  
        string ModelToXml<T>(T model);

        /// <summary>  
        /// xml 转换为Table  
        /// </summary>  
        /// <param name="xml"></param>  
        /// <returns></returns>  
        DataTable XmlToTable(string xml);

        /// <summary>  
        /// 获取对应XML节点的值  
        /// </summary>  
        /// <param name="?"></param>  
        /// <returns></returns>  
        string XmlAnalysis(string stringRoot, string xml);
    }  
}