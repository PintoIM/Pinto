using PintoNS.Localization.Builtin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoNS.Localization
{
    public class LocalizationManager
    {
        public static EnglishLanguage DefaultLanguage = new EnglishLanguage();
        public Language CurrentLanguage = DefaultLanguage;
        public readonly List<Language> Languages = new List<Language>(new Language[] { DefaultLanguage });
    }
}
