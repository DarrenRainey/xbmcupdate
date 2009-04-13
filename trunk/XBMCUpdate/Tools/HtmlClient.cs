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
                myWebResponse.Close();
                readStream.Close();
                ReceiveStream.Close();
            }

            return strResponse;
        }
    }
}
