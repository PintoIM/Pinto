using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;

namespace PintoNS.General
{
    public static class LastContacts
    {
        public static void ClearLastContacts() 
        {
            string path = Path.Combine(Program.DataFolder, "last_contacts.json");
            if (File.Exists(path))
                File.Delete(path);
        }

        public static void AddToLastContacts(string name) 
        {
            string path = Path.Combine(Program.DataFolder, "last_contacts.json");
            JArray data;

            if (File.Exists(path))
            {
                string fileData = File.ReadAllText(path);
                data = JsonConvert.DeserializeObject<JArray>(fileData);
            }
            else
                data = new JArray();

            data.Add(name);
            File.WriteAllText(path, data.ToString(Formatting.Indented));
        }

        public static void RemoveFromLastContacts(string name) 
        {
            string path = Path.Combine(Program.DataFolder, "last_contacts.json");
            if (!File.Exists(path)) return;

            string fileData = File.ReadAllText(path);
            JArray data = JsonConvert.DeserializeObject<JArray>(fileData);
            data.Remove(name);

            File.WriteAllText(path, data.ToString(Formatting.Indented));
        }

        public static string[] GetLastContacts()
        {
            string path = Path.Combine(Program.DataFolder, "last_contacts.json");
            if (!File.Exists(path)) return new string[0];

            string fileData = File.ReadAllText(path);
            JArray data = JsonConvert.DeserializeObject<JArray>(fileData);

            return data.ToObject<string[]>();
        }
    }
}
