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
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using NLog;

namespace XbmcUpdate.SelfUpdate
{
    internal class Update
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly List<FileInfo> _updateFileInfo;
        private readonly List<string> _updateFiles;

        internal Update()
        {
            _updateFiles = new List<string>();
            _updateFileInfo = new List<FileInfo>();

            foreach (string currentFile in Directory.GetFiles(Program.UpdatePath))
            {
                var currentFileInfo = new FileInfo(currentFile);
                _updateFiles.Add(currentFileInfo.Name.ToLower());
                _updateFileInfo.Add(currentFileInfo);
            }
        }


        internal void ShutDownApp()
        {
            Logger.Info("Verifying if any of the files that need to be updated are being used by other processes");
            Process[] allProcesses = Process.GetProcesses();

            foreach (Process currentProcess in allProcesses)
            {

                //Don't kill yourself, there is so much to live for!
                if (Process.GetCurrentProcess().Id != currentProcess.Id)
                {
                    try
                    {
                        if (_updateFiles.Contains(currentProcess.MainModule.ModuleName.ToLower()))
                        {
                            Logger.Info("Process:{0} is associated with file:{1}. Terminating process.",
                                        currentProcess.ProcessName, currentProcess.MainModule.FileName);
                            currentProcess.CloseMainWindow();
                            currentProcess.WaitForExit(2000);
                            if (!currentProcess.HasExited)
                            {
                                Logger.Warn("Process {0} failed to exit gracefully. Forcing it to terminate",
                                            currentProcess.ProcessName);
                                currentProcess.Kill();
                                currentProcess.WaitForExit(2000);
                            }

                            if (!currentProcess.HasExited)
                            {
                                Logger.Warn("Process STILL Running! WHAT THE HELL! - Kill it with FIRE!. {0}",
                                            currentProcess.ProcessName);
                            }
                            else
                            {
                                Logger.Info("Process {0} has been terminated successfully", currentProcess.ProcessName);
                            }
                        }
                    }
                    catch (Win32Exception e)
                    {
                        if (e.Message.ToLower() != "access is denied" &&
                            e.Message.ToLower() != "unable to enumerate the process modules.")
                        {
                            Logger.Error(currentProcess + " " + e.Message);
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Error(currentProcess + " " + e.Message);
                    }
                }
            }
        }


        internal void CopyUpdate()
        {
            Logger.Info("Copying update to target directory");


            foreach (FileInfo currentFile in _updateFileInfo)
            {
                if (!currentFile.Name.ToLower().Contains("selfupdate"))
                {
                    try
                    {
                        File.Copy(currentFile.ToString(), Application.StartupPath + "\\" + currentFile.Name, true);
                        Logger.Info("File '{0}' Updated successfully", currentFile.Name);
                    }
                    catch (Exception e)
                    {
                        Logger.Fatal("An error has occurred while copying file {0}. {1}", currentFile, e.ToString());
                    }
                }
            }
        }


        internal void CleanUp()
        {
            Logger.Info("Cleaning Up {0}", Program.UpdatePath);

            try
            {
                Directory.Delete(Program.UpdatePath, true);
            }
            catch (Exception e)
            {
                Logger.Error("An error has occurred while preforming cleanup.{0}", e.ToString());
            }
        }
    }
}