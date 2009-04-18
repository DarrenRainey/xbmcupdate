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
using System.Net;
using System.Text;
using NLog;

namespace XbmcUpdate.Tools
{
    class HtmlClient
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        internal static string GetPage( string strUrl )
        {
            string strResponse = String.Empty;
            WebResponse myWebResponse = null;
            StreamReader readStream = null;
            Stream ReceiveStream = null;

            try
            {
                logger.Trace( "Attempting to connect to '{0}'", strUrl );
                WebRequest myWebRequest = WebRequest.Create( strUrl );

                myWebResponse = myWebRequest.GetResponse();
                ReceiveStream = myWebResponse.GetResponseStream();

                Encoding encode = System.Text.Encoding.GetEncoding( "utf-8" );

                readStream = new StreamReader( ReceiveStream, encode );

                strResponse = readStream.ReadToEnd();

                logger.Trace( "Successfully downloaded html page {0}", strUrl );
            }
            catch( System.Net.WebException webEx )
            {
                logger.Error( "Unable to connect to remote server on '{0}'. {1}", strUrl, webEx.Message );
                throw;
            }
            catch( Exception e )
            {
                logger.Fatal( "An error has occurred while downloading build list from the server. {0}", e.Message );
                throw;
            }
            finally
            {

                if( myWebResponse != null )
                    myWebResponse.Close();
                if( readStream != null )
                    readStream.Close();
                if( ReceiveStream != null )
                    ReceiveStream.Close();
            }

            return strResponse;
        }
    }
}
