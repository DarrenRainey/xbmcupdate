using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;
using NLog;

namespace XbmcUpdate.Tools
{
    internal class DownloadManager
    {

        static Logger logger = LogManager.GetCurrentClassLogger();

        //Url for the file to be downloaded
        private string url;
        //location the downloaded file should be saved to
        private string localFile;
        // The thread inside which the download happens
        private Thread downloadThread;
        // The stream of data retrieved from the web server
        private Stream dlStream;
        // The stream of data that we write to the hard drive
        private Stream localStream;
        // The request to the web server for file information
        private HttpWebRequest webRequest;
        // The response from the web server containing information about the file
        private HttpWebResponse webResponse;
        //Size of the file
        private Int64 fileSize = 0;

        public Int64 BytesRead
        {
            get
            {
                if( localStream != null )
                {
                    try
                    {
                        return localStream.Length;
                    }
                    catch
                    {
                    }

                }
                return 0;
            }
        }

        public Int64 FileSize
        {
            get
            {
                return fileSize;
            }
        }


        public void Download( string fileUrl, string destinationFile )
        {
            url = fileUrl;
            localFile = destinationFile;

            StartDownload();
        }


        public void Stop()
        {
            // Close the web response and the streams
            webResponse.Close();
            dlStream.Close();
            localStream.Close();
            // Abort the thread that's downloading
            downloadThread.Abort();
            //Try to delete the incomplete file

            try
            {
                File.Delete( localFile );
            }
            catch( Exception e )
            {
                logger.Fatal( "Unable to delete incomplete file {0}. {1}", e.Message, localFile );
            }
        }

        public static long GetFileSize( string url )
        {
            long remoteSize = 0;

            HttpWebResponse resp = null;

            using( WebClient wcDownload = new WebClient() )
            {
                try
                {
                    // Create a request to the file
                    var req = (HttpWebRequest)WebRequest.Create( url );
                    // Set default authentication for retrieving the file
                    req.Credentials = CredentialCache.DefaultCredentials;
                    // Retrieve the response from the server
                    resp = (HttpWebResponse)req.GetResponse();
                    // Ask the server for the file size and store it
                    remoteSize = resp.ContentLength;

                    resp.Close();

                }
                catch( Exception e )
                {
                    logger.Fatal( "An error has occurred while retrieving server file size. {0}", e.Message );
                }
                finally
                {
                    resp.Close();
                }
            }

            return remoteSize;
        }

        private void StartDownload()
        {
            using( WebClient wcDownload = new WebClient() )
            {
                try
                {
                    // Create a request to the file we are downloading
                    webRequest = (HttpWebRequest)WebRequest.Create( url );
                    // Set default authentication for retrieving the file
                    webRequest.Credentials = CredentialCache.DefaultCredentials;
                    // Retrieve the response from the server
                    webResponse = (HttpWebResponse)webRequest.GetResponse();
                    // Ask the server for the file size and store it
                    fileSize = webResponse.ContentLength;
                    // Open the URL for download 
                    dlStream = wcDownload.OpenRead( url );
                    // Create a new file stream where we will be saving the data (local drive)
                    localStream = new FileStream( localFile, FileMode.Create, FileAccess.Write, FileShare.None );

                    // It will store the current number of bytes we retrieved from the server
                    int bytesSize = 0;
                    // A buffer for storing and writing the data retrieved from the server
                    byte[] downBuffer = new byte[2048];

                    // Loop through the buffer until the buffer is empty
                    while( ( bytesSize = dlStream.Read( downBuffer, 0, downBuffer.Length ) ) > 0 )
                    {
                        // Write the data from the buffer to the local hard drive
                        localStream.Write( downBuffer, 0, bytesSize );
                        // Invoke the method that updates the form's label and progress bar
                    }

                    logger.Info( "Download completed successfully" );
                }
                catch( Exception e )
                {
                    logger.Fatal( "An error has occurred while downloading file. {0}", e.Message );
                }
                finally
                {
                    // When the above code has ended, close the streams
                    localStream.Close();
                    dlStream.Close();
                    webResponse.Close();
                }
            }
        }
    }
}
