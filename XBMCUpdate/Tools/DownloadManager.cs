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
using NLog;

namespace XbmcUpdate.Tools
{
    internal class DownloadManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        //Url for the file to be downloaded
        // The stream of data retrieved from the web server
        private Stream _dlStream;
        private string _localFile;
        // The stream of data that we write to the hard drive
        private Stream _localStream;
        private string _url;
        // The request to the web server for file information
        private HttpWebRequest _webRequest;
        // The response from the web server containing information about the file
        private HttpWebResponse _webResponse;
        //Size of the file

        internal long BytesRead { get; private set; }

        internal long FileSize { get; private set; }


        internal void Download(string fileUrl, string destinationFile)
        {
            _url = fileUrl;
            _localFile = destinationFile;

            StartDownload();
        }


        internal void Stop()
        {
            // Close the web response and the streams
            _webResponse.Close();
            _dlStream.Close();
            _localStream.Close();
            // Abort the thread that's downloading
            //Try to delete the incomplete file

            try
            {
                File.Delete(_localFile);
            }
            catch (Exception e)
            {
                Logger.Fatal("Unable to delete incomplete file {0}. {1}", e.Message, _localFile);
            }
        }

        internal static long GetFileSize(string url)
        {
            long remoteSize = 0;

            HttpWebResponse resp = null;

            try
            {
                // Create a request to the file
                var req = (HttpWebRequest)WebRequest.Create(url);
                // Set default authentication for retrieving the file
                req.Credentials = CredentialCache.DefaultCredentials;
                // Retrieve the response from the server
                resp = (HttpWebResponse)req.GetResponse();
                // Ask the server for the file size and store it
                remoteSize = resp.ContentLength;

                resp.Close();
            }
            catch (Exception e)
            {
                Logger.Fatal("An error has occurred while retrieving server file size. {0}", e.Message);
            }
            finally
            {
                if (resp != null) resp.Close();
            }

            return remoteSize;
        }

        private void StartDownload()
        {
            using (var wcDownload = new WebClient())
            {
                try
                {
                    BytesRead = 0;

                    // Create a request to the file we are downloading
                    _webRequest = (HttpWebRequest)WebRequest.Create(_url);
                    // Set default authentication for retrieving the file
                    _webRequest.Credentials = CredentialCache.DefaultCredentials;
                    // Retrieve the response from the server
                    _webResponse = (HttpWebResponse)_webRequest.GetResponse();
                    // Ask the server for the file size and store it
                    FileSize = _webResponse.ContentLength;
                    // Open the URL for download 
                    _dlStream = wcDownload.OpenRead(_url);
                    // Create a new file stream where we will be saving the data (local drive)
                    _localStream = new FileStream(_localFile, FileMode.Create, FileAccess.Write, FileShare.None);

                    // It will store the current number of bytes we retrieved from the server
                    int bytesSize;
                    // A buffer for storing and writing the data retrieved from the server
                    var downBuffer = new byte[2048];

                    // Loop through the buffer until the buffer is empty
                    while ((bytesSize = _dlStream.Read(downBuffer, 0, downBuffer.Length)) > 0)
                    {
                        // Write the data from the buffer to the local hard drive
                        _localStream.Write(downBuffer, 0, bytesSize);
                        BytesRead = _localStream.Length;
                        // Invoke the method that updates the form's label and progress bar
                    }

                    Logger.Info("Download completed successfully");
                }
                catch (Exception e)
                {
                    Logger.Fatal("An error has occurred while downloading file. {0}", e.Message);
                }
                finally
                {
                    BytesRead = _localStream.Length;
                    // When the above code has ended, close the streams
                    _localStream.Close();
                    _dlStream.Close();
                    _webResponse.Close();
                }
            }
        }
    }
}