using PintoNS.Localization.Builtin;
using System.Collections.Generic;

namespace PintoNS.Localization
{
    public class LocalizationManager
    {
        public static EnglishLanguage DefaultLanguage = new EnglishLanguage();
        public Language CurrentLanguage = DefaultLanguage;
        public readonly List<Language> Languages = new List<Language>(new Language[] { DefaultLanguage });
    }
}
