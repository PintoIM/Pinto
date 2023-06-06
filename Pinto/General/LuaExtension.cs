using NLua;
using NLua.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PintoNS.General
{
    public class LuaExtension
    {
        public static readonly string[] BlockedNamespaces = { "System.Threading" };
        private MainForm mainForm;
        public string FilePath;
        public Lua Script;
        public string Name;
        public string Author;
        public string Version;
        public int Priority;
        private LuaFunction orgImport;

        public LuaExtension(string filePath, MainForm mainForm) 
        {
            this.mainForm = mainForm;
            FilePath = filePath;
            Script = new Lua();

            Script.NewTable("PintoLib");
            LuaTable pintoLib = Script.GetTable("PintoLib");
            pintoLib["MainForm"] = mainForm;
            pintoLib["WriteDebug"] = (Action<string>)Program.Console.WriteMessage;
            pintoLib["ControlInvoke"] = new Action<Control, Action>(
                (Control control, Action action) => control.Invoke(action));

            Script.LoadCLRPackage();
            orgImport = Script.GetFunction("import");
            Script["import"] = (Action<string[]>) ImportHook;
            Script.DoFile(filePath);

            LuaTable scriptInfo = (LuaTable)Script.GetFunction("ScriptInfo").Call().First();
            Name = (string)scriptInfo["name"];
            Author = (string)scriptInfo["author"];
            Version = (string)scriptInfo["version"];
            Priority = Script["ScriptPriority"] != null ? 
                (int)((long)Script.GetFunction("ScriptPriority").Call().First()) : 0;
            if (Priority < 0 || Priority > 2) throw new Exception("Invalid priority!");
        }

        private void ImportHook(params string[] args) 
        {
            if (args.Length > 0 && (BlockedNamespaces.Contains(args[0]) ||
                (args.Length > 1 && BlockedNamespaces.Contains(args[1])))) 
                throw new Exception($"The assembly \"{args[0]}\"" +
                    $"{(args.Length > 1 ? $" or namespace \"{args[1]}\"" : "")}" +
                    $" has been blocked from usage in extensions");

            orgImport.Call(args);
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
