using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System;
using NLog;
using XbmcUpdate.Managers;

/// <summary>
/// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
/// </summary>
/// <param name="characters">Unicode Byte Array to be converted to String</param>
/// <returns>String converted from Unicode Byte Array</returns>
namespace XbmcUpdate.Tools
{
    class Serilizer
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        private static string UTF8ByteArrayToString(byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        /// <summary>
        /// Converts the String to UTF8 Byte array and is used in De serialization
        /// </summary>
        /// <param name="pXmlString"></param>
        /// <returns></returns>
        private static Byte[] StringToUTF8ByteArray(string pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

        /// <summary>
        /// Serialize an object into an XML string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(T obj)
        {
            try
            {
                string xmlString = null;
                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer xs = new XmlSerializer(typeof(T));
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                xs.Serialize(xmlTextWriter, obj);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                xmlString = UTF8ByteArrayToString(memoryStream.ToArray());
                return xmlString;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Reconstruct an object from an XML string
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static VersionInfo DeserializeObject(string xml)
        {
            VersionInfo response = new VersionInfo();

            if (!string.IsNullOrEmpty(xml))
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(VersionInfo));
                    MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(xml));
                    XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                    response = (VersionInfo)xs.Deserialize(memoryStream);
                }
                catch (System.Exception ex)
                {
                    logger.Info("XML file is malformed. {0}", ex.Message);
                    response = null;
                }
            }

            return response;
        }


        public static void WriteToFile(string path, string content, bool append)
        {
            TextWriter tw = null;
            try
            {
                tw = new StreamWriter(path, append);
                tw.Write(content);
            }
            catch (Exception e)
            {
                logger.Fatal("An error has occurred while try to write to '{0}'. {1}", path, e.ToString());
            }
            finally
            {
                if (tw != null)
                {
                    tw.Close();
                }
            }

        }


        public static string ReadFile(string path)
        {
            TextReader tr = null;
            string content = String.Empty;
            try
            {
                tr = new StreamReader(path);
                content = tr.ReadToEnd();
            }
            catch (Exception e)
            {
                logger.Fatal("An error has occurred while try to read '{0}'. {1}", path, e.ToString());
            }
            finally
            {
                if (tr != null)
                {
                    tr.Close();
                }
            }

            return content;
        }
    }
}