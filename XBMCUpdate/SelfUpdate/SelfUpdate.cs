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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;
using NLog;
using XbmcUpdate.Runtime;
using XbmcUpdate.Tools;

namespace XbmcUpdate.SelfUpdate
{
    class SelfUpdate
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly string SelfUpdatePath = Application.StartupPath + "\\selfupdate\\";
        private static readonly string SelfUpdateTemp = Settings.TempFolder + "\\selfupdate\\";

        private static Version LatestBuild = new Version();
        private static string LatestBuildUrl = String.Empty;


        internal static bool DownloadUpdate()
        {
            bool updateDownloaded = false;
            try
            {
                Stopwatch selfUpdateStopwatch = new Stopwatch();
                selfUpdateStopwatch.Start();

                logger.Info( "Initiating Self-update. Local version:{0}", Settings.ApplicationVersion.ToString() );
                SelfUpdateCleanup();

                GetLatestBuildInfo();

                if( LatestBuild > Settings.ApplicationVersion )
                {
                    PrepUpdate();
                    updateDownloaded = true;
                }
                else
                {
                    logger.Info( "You have the most recent build of XBMCUpdate. No Update is necessary" );
                    updateDownloaded = false;
                }

                selfUpdateStopwatch.Stop();

                logger.Info( "Selfupdate preparation took {0}s", selfUpdateStopwatch.Elapsed.TotalSeconds );
            }
            catch( Exception e )
            {
                logger.Error( "An error has occurred while checking for Application update.{0}", e.Message );
                throw;
            }

            return updateDownloaded;
        }


        private static void PrepUpdate()
        {
            logger.Info( "New version of XBMCUpdate is available from the server. Initiating Download." );
            string zipDestination = string.Format( "{0}\\xbmcupdate{1}.zip", SelfUpdateTemp, LatestBuild.ToString() );
            DownloadManager download = new DownloadManager();
            download.Download( LatestBuildUrl, zipDestination );

            logger.Info( "Extracting update" );
            FastZip zipClient = new FastZip();
            zipClient.ExtractZip( zipDestination, SelfUpdatePath, @"+\.exe$;+\.pdb$;+\.dll$;-^nlog\.dll$" );
            logger.Info( "Update extracted to {0}", SelfUpdatePath );

            if( File.Exists( SelfUpdatePath + "\\selfupdate.exe" ) )
            {
                File.Copy( SelfUpdatePath + "\\selfupdate.exe", Application.StartupPath + "\\\\selfupdate.exe", true );
                //File.Delete( SelfUpdatePath + "\\selfupdate.exe" );
            }
            if( File.Exists( SelfUpdatePath + "\\selfupdate.pdb" ) )
            {
                File.Copy( SelfUpdatePath + "\\selfupdate.pdb", Application.StartupPath + "\\\\selfupdate.pdb", true );
                File.Delete( SelfUpdatePath + "\\selfupdate.pdb" );
            }
        }


        private static void GetLatestBuildInfo()
        {
            string page = HtmlClient.GetPage( Settings.SelfUpdateUrl );
            List<int> buildNumbers = new List<Int32>();

            logger.Info( "Trying to parse out the builds list from HTML string" );

            var matches = Regex.Matches( page, @"http:\/\/.*xbmcupdate_\d\.\d\.\d.zip", RegexOptions.IgnoreCase );

            foreach( var buildUrl in matches )
            {
                try
                {
                    string build = Regex.Match( buildUrl.ToString(), @"\d\.\d\.\d", RegexOptions.IgnoreCase ).Value;

                    if( !String.IsNullOrEmpty( build ) )
                    {
                        Version thisFile = new Version( build );

                        if( thisFile > LatestBuild )
                        {
                            LatestBuild = thisFile;
                            LatestBuildUrl = buildUrl.ToString();
                        }
                    }
                }
                catch( Exception e )
                {
                    logger.Error( "An error has occurred while parsing out XBMCUpdate Version. {0}", e.ToString() );
                }
            }

            logger.Info( "Latest build available from the server: {0}", LatestBuild.ToString() );
        }

        private static void SelfUpdateCleanup()
        {
            logger.Info( "Preforming Selfupdate Cleanup" );

            try
            {
                if( Directory.Exists( SelfUpdatePath ) )
                {
                    Directory.Delete( SelfUpdatePath, true );
                }
            }
            catch( Exception e )
            {
                logger.Error( "An error has occurred while preforming selfupdate cleanup.{0}", e.Message );
            }

            try
            {
                if( Directory.Exists( SelfUpdateTemp ) )
                {
                    Directory.Delete( SelfUpdateTemp, true );
                }
            }
            catch( Exception e )
            {
                logger.Error( "An error has occurred while preforming selfupdate temp cleanup.{0}", e.Message );
            }

            Directory.CreateDirectory( SelfUpdateTemp );
        }


    }
}
