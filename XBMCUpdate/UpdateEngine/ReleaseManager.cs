using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using NLog;
using XbmcUpdate.Runtime;

namespace XbmcUpdate.Managers
{
    class ReleaseManager
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static Dictionary<int, string> releaseUrls = new Dictionary<int, string>();

        private static string GetPage( string strURL )
        {

            string strResponse = String.Empty;

            try
            {

                logger.Trace( "Attempting to connect to '{0}'", strURL );

                WebRequest myWebRequest = WebRequest.Create( strURL );

                WebResponse myWebResponse = myWebRequest.GetResponse();

                Stream ReceiveStream = myWebResponse.GetResponseStream();

                Encoding encode = System.Text.Encoding.GetEncoding( "utf-8" );

                StreamReader readStream = new StreamReader( ReceiveStream, encode );

                strResponse = readStream.ReadToEnd();

                readStream.Close();

                myWebResponse.Close();

                logger.Trace( "Successfully downloaded build list" );

            }
            catch( System.Net.WebException webEx )
            {
                logger.Error( "Unable to connect to remote server on '{0}'. {1}", strURL, webEx.Message );
                throw;
            }
            catch( Exception e )
            {
                logger.Fatal( "An error has occurred while downloading build list from the server. {0}", e.Message );
                throw;

            }

            return strResponse;
        }

        internal static List<int> GetBuildList()
        {
            releaseUrls.Clear();

            string page = GetPage( Settings.ReleaseUrl );
            List<int> buildNumbers = new List<Int32>();

            logger.Info( "Trying to parse out the builds list from HTML string." );

            var matches = Regex.Matches( page, @"XBMC.{0,5}\d{5}.zip", RegexOptions.IgnoreCase );

            foreach( var buildFileName in matches )
            {
                string build = Regex.Match( buildFileName.ToString(), @"\d{5,6}", RegexOptions.IgnoreCase ).Value;

                if( !String.IsNullOrEmpty( build ) )
                {
                    int buildNum = Convert.ToInt32( build );

                    if( !buildNumbers.Contains( buildNum ) )
                    {
                        buildNumbers.Add( buildNum );
                        releaseUrls.Add( buildNum, string.Format( @"{0}/{1}", Settings.ReleaseUrl, buildFileName ) );
                    }
                }
            }

            buildNumbers.Sort();

            logger.Info( "Total of {0} builds were found. Latest:{1}", buildNumbers.Count, buildNumbers.Count != 0 ? buildNumbers[buildNumbers.Count - 1].ToString() : "NA" );

            return buildNumbers;
        }


        internal static string GetBuildUrl( int buildNumber )
        {
            string responese = string.Empty;

            if( releaseUrls.ContainsKey( buildNumber ) )
            {
                responese = releaseUrls[buildNumber];
            }

            return responese;
        }


    }



}



