using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Luxlead.WaterMark
{
    ///
    /// 将Json格式数据转化成对象
    ///
    public class JsonHelper
    {
        ///  
        /// 生成Json格式 
        ///   
        public static string GetJson<T>(T obj)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, obj);
                string szJson = Encoding.UTF8.GetString(stream.ToArray()); return szJson;
            }
        }
        ///  
        /// 获取Json的Model 
        ///   
        public static T ParseFromJson<T>(string szJson)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }
        }
    }
}