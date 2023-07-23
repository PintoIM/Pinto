using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PintoNS.General
{
    [AttributeUsage(AttributeTargets.Field)]
    public class OptionsDisplayAttribute : Attribute 
    {
        public string DisplayName;
        public string Category;
        public string HelpInfo;
        public bool Hidden;
        public int NumMin = int.MinValue;
        public int NumMax = int.MaxValue;
    }

    public static class Settings
    {
        #region General
        [OptionsDisplay(DisplayName = "Automatically check for updates on start-up", Category = "General",
            HelpInfo = "When you start Pinto!, it will automatically check for updates," +
            " if this option is disabled, Pinto! will not check for updates on start-up," +
            " you can still manually check for update by going to Help > Check for Updates\n" +
            "It is recommended that you keep this option enabled")]
        public static bool AutoCheckForUpdates = true;
        #endregion
        #region Hidden
        [OptionsDisplay(Hidden = true)]
        public static bool DoNotShowSysTrayNotice = false;
        #endregion

        public static void Export(string file) 
        {
            Type type = typeof(Settings);
            JObject obj = new JObject();

            foreach (FieldInfo field in type.GetFields()) 
            {
                obj[field.Name] = new JValue(field.GetValue(null));
            }

            File.WriteAllText(file, obj.ToString(Formatting.Indented));
        }

        public static void Import(string file)
        {
            Type type = typeof(Settings);
            if (!File.Exists(file)) Export(file);
            JObject obj = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(file));

            foreach (JProperty property in obj.Children())
            {
                FieldInfo field = type.GetField(property.Name);
                if (field == null) continue;
                field.SetValue(null, property.Value.ToObject(field.FieldType));
            }
        }
    }
}
