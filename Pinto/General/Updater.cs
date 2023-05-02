using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PintoNS.Forms.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS.General
{
    public static class Updater
    {
        public const string UPDATE_URL = "https://github.com/PintoIM/Pinto/raw/main/version.json";

        public static async Task<JObject> GetVersionInformation() 
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("User-Agent", "PintoClient");

                string responseRaw = await httpClient.GetStringAsync(UPDATE_URL);
                JObject response = JsonConvert.DeserializeObject<JObject>(responseRaw);
                MessageBox.Show(response.ToString());
                return response;
            }
            catch (Exception ex)
            {
                Program.Console.WriteMessage($"[Updater] Unable to download version information: {ex}");
                return null;
            }
        }

        public static async Task<bool> IsLatest() 
        {
            Program.Console.WriteMessage($"[Updater] Checking for updates...");
            try
            {
                JObject information = await GetVersionInformation();
                return Program.VERSION.Equals(information["latest"].Value<string>(), 
                    StringComparison.InvariantCultureIgnoreCase);
            }
            catch (Exception ex) 
            {
                Program.Console.WriteMessage($"[Updater] Unable to check for updates: {ex}");
                MsgBox.ShowNotification(null, 
                    "Unable to check for updates!" +
                    " Check the console for more information," +
                    " you can also perform a re-check by going into the \"Help\" menu",
                    "Error",
                    MsgBoxIconType.ERROR);
                return true;
            }
        }

        public static async Task<byte[]> GetUpdateFile() 
        {
            Program.Console.WriteMessage($"[Updater] Downloading update file...");
            try
            {
                JObject information = await GetVersionInformation();
                string updateURL = information["update_url"].Value<string>();

                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("User-Agent", "PintoClient");

                return await httpClient.GetByteArrayAsync(updateURL);
            }
            catch (Exception ex)
            {
                Program.Console.WriteMessage($"[Updater] Unable to download the update file: {ex}");
                MsgBox.ShowNotification(null,
                    "Unable to download the update file!" +
                    " Check the console for more information," +
                    " you can perform a re-check by going into the \"Help\" menu",
                    "Error",
                    MsgBoxIconType.ERROR);
                return null;
            }
        }
    }
}