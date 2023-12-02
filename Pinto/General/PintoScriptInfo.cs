using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PintoNS.General
{
    public class PintoScriptInfo
    {
        public string Name { get; private set; }
        public string Author { get; private set; }
        public string Version { get; private set; }
        public string TestedPintoVersion { get; private set; }

        public PintoScriptInfo(string name, string author, string version, string testedPintoVersion) 
        {
            Name = name;
            Author = author;
            Version = version;
            TestedPintoVersion = testedPintoVersion;
        }
    }
}
