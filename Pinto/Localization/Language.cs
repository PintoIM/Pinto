using System.Collections.Generic;

namespace PintoNS.Localization
{
    public class Language
    {
        protected Dictionary<string, string> StringMap = new Dictionary<string, string>();

        public string GetString(string id) 
        {
            return StringMap.ContainsKey(id) ? StringMap[id] : id;
        }
    }
}
