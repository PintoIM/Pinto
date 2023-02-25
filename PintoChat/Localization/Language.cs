using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
