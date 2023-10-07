using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FrameworkCommon.Utils
{
    /// <summary>
    /// 将对象序列化和反序列化
    /// </summary>
    public static class SerializableHelper
    {
        /// <summary>
        /// 将XmlDocument转化为string
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public static string ConvertXmlToString(XmlDocument xmlDoc)
        {
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, null);
            writer.Formatting = Formatting.Indented;
            xmlDoc.Save(writer);
            StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
            stream.Position = 0;
            string xmlString = sr.ReadToEnd();
            sr.Close();
            stream.Close();
            return xmlString;
        }

        /// <summary>  
        /// 将字符串（符合xml格式）转换为XmlDocument  
        /// </summary>  
        /// <param name="xmlString">XML格式字符串</param>  
        /// <returns></returns>  
        public static XmlDocument ConvertStringToXmlDocument(string xmlString)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlString);
            return document;
        }

        /// <summary>
        /// 获取对象序列化的二进制版本
        /// </summary>
        /// <param name="pObj">对象实体</param>
        /// <returns>如果对象实体为Null，则返回结果为Null。</returns>
        public static byte[] GetBytes(object pObj)
        {
            if (pObj == null) { return null; }
            MemoryStream serializationStream = new MemoryStream();
            new BinaryFormatter().Serialize(serializationStream, pObj);
            serializationStream.Position = 0L;
            byte[] buffer = new byte[serializationStream.Length];
            serializationStream.Read(buffer, 0, buffer.Length);
            serializationStream.Close();
            return buffer;
        }

        /// <summary>
        /// 获取对象序列化的XmlDocument版本
        /// </summary>
        /// <param name="pObj">对象实体</param>
        /// <returns>如果对象实体为Null，则返回结果为Null。</returns>
        public static XmlDocument GetXmlDoc(object pObj)
        {
            if (pObj == null) { return null; }
            XmlSerializer serializer = new XmlSerializer(pObj.GetType());
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            serializer.Serialize((TextWriter)writer, pObj);
            XmlDocument document = new XmlDocument();
            document.LoadXml(sb.ToString());
            writer.Close();
            return document;
        }

        /// <summary>
        /// 从已序列化数据中(byte[])获取对象实体
        /// </summary>
        /// <typeparam name="T">要返回的数据类型</typeparam>
        /// <param name="binData">二进制数据</param>
        /// <returns>对象实体</returns>
        public static T GetObject<T>(byte[] binData)
        {
            if (binData == null) { return default(T); }
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream serializationStream = new MemoryStream(binData);
            return (T)formatter.Deserialize(serializationStream);
        }

        /// <summary>
        /// 从已序列化数据(XmlDocument)中获取对象实体
        /// </summary>
        /// <typeparam name="T">要返回的数据类型</typeparam>
        /// <param name="xmlDoc">已序列化的文档对象</param>
        /// <returns>对象实体</returns>
        public static T GetObject<T>(XmlDocument xmlDoc)
        {
            if (xmlDoc == null) { return default(T); }
            XmlNodeReader xmlReader = new XmlNodeReader(xmlDoc.DocumentElement);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(xmlReader);
        }

        /// <summary>
        /// 序列化对象并存储到指定位置
        /// </summary>
        /// <typeparam name="T">要序列化的对象名</typeparam>
        /// <param name="Obj">对象</param>
        /// <param name="FileName">要保存的文件名，包含后缀</param>
        /// <param name="path">保存路径</param>
        public static void Serializable<T>(T Obj, string FileName, string path)
        {
            if (Obj != null)
            {
                FileStream fs = new FileStream(Path.Combine(path, FileName).ToString(), FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, Obj);
                fs.Close();
            }
            else
            {
                throw new Exception("序列化的对象为null");
            }
        }

        /// <summary>
        /// 反序列化指定文件为指定对象
        /// </summary>
        /// <typeparam name="T">反序列化后得到的对象</typeparam>
        /// <param name="FileName">要反序列化的文件</param>
        /// <returns></returns>
        public static T Deserialization<T>(string FileName) where T : class
        {
            string FilePath = FileName;
            if (File.Exists(FilePath))
            {
                T Obj = null;
                FileStream fs = new FileStream(FilePath, FileMode.Open);
                if (fs != null && fs.Length > 0)
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    Obj = bf.Deserialize(fs) as T;
                    fs.Close();
                }
                return Obj;
            }
            else
            {
                Console.Write("没有找到指定路径的文件");
                return null;
            }
        }


    }
}
