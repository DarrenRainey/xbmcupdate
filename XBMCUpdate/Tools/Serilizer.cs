/*
 *   XBMCUpdate: Automatic Update Client for XBMC. (www.xbmc.org)
 * 
 *   Copyright (C) 2009  Keivan Beigi
 * 
 *   This program is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.
 *
 *   You should have received a copy of the GNU General Public License
 *   along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using NLog;
using XbmcUpdate.UpdateEngine;

/// <summary>
/// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
/// </summary>
/// <param name="characters">Unicode Byte Array to be converted to String</param>
/// <returns>String converted from Unicode Byte Array</returns>
namespace XbmcUpdate.Tools
{
    internal static class Serilizer
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static string UTF8ByteArrayToString(byte[] characters)
        {
            var encoding = new UTF8Encoding();
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
            var encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

        /// <summary>
        /// Serialize an object into an XML string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static string SerializeObject<T>(T obj)
        {
            try
            {
                var memoryStream = new MemoryStream();
                var xs = new XmlSerializer(obj.GetType());

                var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

                xmlTextWriter.Formatting = Formatting.Indented;
                xs.Serialize(xmlTextWriter, obj);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                return UTF8ByteArrayToString(memoryStream.ToArray());
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
        internal static T DeserializeObject<T>(string xml) where T : class
        {

            T response = null;

            if (!string.IsNullOrEmpty(xml))
            {
                try
                {
                    var xs = new XmlSerializer(typeof(T));
                    var memoryStream = new MemoryStream(StringToUTF8ByteArray(xml));
                    new XmlTextWriter(memoryStream, Encoding.UTF8);
                    response = xs.Deserialize(memoryStream) as T;
                }
                catch (Exception ex)
                {
                    Logger.Info("XML file is malformed. {0}", ex.Message);
                }
            }

            return response;
        }


        internal static void WriteToFile(string path, string content, bool append)
        {
            TextWriter tw = null;
            try
            {
                tw = new StreamWriter(path, append);
                tw.Write(content);
            }
            catch (Exception e)
            {
                Logger.Fatal("An error has occurred while try to write to '{0}'. {1}", path, e.ToString());
            }
            finally
            {
                if (tw != null)
                {
                    tw.Close();
                }
            }
        }


        internal static string ReadFile(string path)
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
                Logger.Fatal("An error has occurred while try to read '{0}'. {1}", path, e.ToString());
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