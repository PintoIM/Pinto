using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PintoNS.General
{
    public class LuaExtension
    {
        public string FilePath;
        public Lua Script;
        public string Name;
        public string Author;
        public string Version;

        public LuaExtension(string filePath, MainForm mainForm) 
        {
            FilePath = filePath;
            Script = new Lua();

            Script.NewTable("PintoLib");
            LuaTable pintoLib = Script.GetTable("PintoLib");
            pintoLib["MainForm"] = mainForm;
            pintoLib["WriteDebug"] = (Action<string>)Program.Console.WriteMessage;

            Script.LoadCLRPackage();
            Script.DoFile(filePath);

            LuaTable scriptInfo = (LuaTable)Script.GetFunction("getScriptInfo").Call().First();
            Name = (string)scriptInfo["name"];
            Author = (string)scriptInfo["author"];
            Version = (string)scriptInfo["version"];

            Program.Console.WriteMessage($"[Extensions] Loaded extension \"{Name}\" by \"{Author}\" (version {Version})");
        }
    }
}
