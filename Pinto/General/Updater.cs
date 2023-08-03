using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PintoNS.General;
using System;
using System.Net.Cache;
using System.Net;
using System.Threading.Tasks;

namespace PintoNS.General
{
    public static class Updater
    {
        public const string UPDATE_URL = "https://github.com/PintoIM/Pinto/raw/main/version.json";

        public static async Task<JObject> GetVersionInformation() 
        {
            try
            {
                WebClient webClient = new WebClient
                {
                    CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore),
                };
                webClient.Headers["User-Agent"] = "PintoClient";

                string responseRaw = await Task.Factory.StartNew(() =>
                {
                    return webClient.DownloadString(UPDATE_URL);
                });
                JObject response = JsonConvert.DeserializeObject<JObject>(responseRaw);
                
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
                return Constants.VERSION_STRING.Equals(information["latest"].Value<string>(), 
                    StringComparison.InvariantCultureIgnoreCase);
            }
            catch (Exception ex) 
            {
                Program.Console.WriteMessage($"[Updater] Unable to check for updates: {ex}");
                MsgBox.Show(null, 
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

                WebClient webClient = new WebClient();
                webClient.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
                webClient.Headers["User-Agent"] = "PintoClient";

                byte[] file = await webClient.DownloadDataTaskAsync(updateURL);
                Program.Console.WriteMessage($"[Updater] Downloaded update file");
                
                return file;
            }
            catch (Exception ex)
            {
                Program.Console.WriteMessage($"[Updater] Unable to download the update file: {ex}");
                MsgBox.Show(null,
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