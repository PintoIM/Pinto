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
    public static class Settings
    {
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
            JObject obj = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(file));

            foreach (JProperty property in obj.Children())
            {
                FieldInfo field = type.GetField(property.Name);
                field.SetValue(null, property.Value.ToObject(field.FieldType));
            }
        }
    }
}
