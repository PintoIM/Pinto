using NLua;
using NLua.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PintoNS.General
{
    public class LuaExtension
    {
        private MainForm mainForm;
        public string FilePath;
        public Lua Script;
        public string Name;
        public string Author;
        public string Version;
        public int Priority;

        public LuaExtension(string filePath, MainForm mainForm) 
        {
            this.mainForm = mainForm;
            FilePath = filePath;
            Script = new Lua();

            Script.NewTable("PintoLib");
            LuaTable pintoLib = Script.GetTable("PintoLib");
            pintoLib["MainForm"] = mainForm;
            pintoLib["WriteDebug"] = (Action<string>)Program.Console.WriteMessage;

            Script.LoadCLRPackage();
            Script.DoFile(filePath);

            LuaTable scriptInfo = (LuaTable)Script.GetFunction("ScriptInfo").Call().First();
            Name = (string)scriptInfo["name"];
            Author = (string)scriptInfo["author"];
            Version = (string)scriptInfo["version"];
            Priority = Script["ScriptPriority"] != null ? 
                (int)((long)Script.GetFunction("ScriptPriority").Call().First()) : 0;
            if (Priority < 0 || Priority > 2) throw new Exception("Invalid priority!");
        }

        public void PrintLoadMessage() 
        {
            Program.Console.WriteMessage($"[Extensions] Loaded extension \"{Name}\" by" +
                $" \"{Author}\" (version {Version})");
        }

        public void PrintUnloadMessage()
        {
            Program.Console.WriteMessage($"[Extensions] Unloaded extension \"{Name}\" by" +
                $" \"{Author}\" (version {Version})");
        }
    }
}
