using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PintoNS.General
{
    public class LuaExtension
    {
        public string FilePath;
        public Script Script;
        public string Name;
        public string Author;
        public string Version;

        public LuaExtension(string filePath, MainForm mainForm) 
        {
            FilePath = filePath;
            Script = new Script();

            Script.DoFile(filePath);
            LuaExtensionsHelper.PrepareScript(Script, mainForm);

            Table scriptInfo = Script.Call(Script.Globals["getScriptInfo"]).Table;
            Name = (string)scriptInfo["name"];
            Author = (string)scriptInfo["author"];
            Version = (string)scriptInfo["version"];

            Program.Console.WriteMessage($"[Extensions] Loaded extension \"{Name}\" by \"{Author}\" (version {Version})");
        }
    }
}
