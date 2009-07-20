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
using System.Text.RegularExpressions;
using NLog;
using XbmcUpdate.Tools;

namespace XbmcUpdate.UpdateEngine
{
    internal static class ReleaseManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static readonly Dictionary<int, string> ReleaseUrls = new Dictionary<int, string>();


        internal static List<int> GetBuildList()
        {
            ReleaseUrls.Clear();

            string page = HtmlClient.GetPage(Settings.ReleaseUrl);
            var buildNumbers = new List<Int32>();

            Logger.Info("Trying to parse out the builds list from HTML string");

            MatchCollection matches = Regex.Matches(page, @"XBMC.{0,5}\d{5}.zip", RegexOptions.IgnoreCase);

            foreach (object buildFileName in matches)
            {
                string build = Regex.Match(buildFileName.ToString(), @"\d{5,6}", RegexOptions.IgnoreCase).Value;

                if (!String.IsNullOrEmpty(build))
                {
                    int buildNum = Convert.ToInt32(build);

                    if (!buildNumbers.Contains(buildNum))
                    {
                        buildNumbers.Add(buildNum);
                        ReleaseUrls.Add(buildNum, string.Format(@"{0}/{1}", Settings.ReleaseUrl, buildFileName));
                    }
                }
            }

            buildNumbers.Sort();

            Logger.Info("Total of {0} builds were found. Latest:{1}", buildNumbers.Count,
                        buildNumbers.Count != 0 ? buildNumbers[buildNumbers.Count - 1].ToString() : "NA");

            return buildNumbers;
        }


        internal static string GetBuildUrl(int buildNumber)
        {
            string responese = string.Empty;

            if (ReleaseUrls.ContainsKey(buildNumber))
            {
                responese = ReleaseUrls[buildNumber];
            }

            return responese;
        }
    }
}