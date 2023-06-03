using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PintoNS.General
{
    public class LuaExtensionsHelper
    {
        public class LuaNumber 
        {
            public double Number;
            public bool IsShort;
            public bool IsInt;
            public bool IsFloat;

            public LuaNumber(double number, bool isShort, bool isInt, bool isFloat)
            {
                Number = number;
                IsShort = isShort;
                IsInt = isInt;
                IsFloat = isFloat;
            }
        }

        public static void PrepareScript(Script script, MainForm mainForm) 
        {
            Table pintoLib = new Table(script);
            pintoLib["mainForm"] = mainForm;
            pintoLib["writeDebug"] = (Action<string>)Program.Console.WriteMessage;
            pintoLib["castType"] = (Func<ScriptExecutionContext, CallbackArguments, DynValue>)CastType;
            pintoLib["getStaticClass"] = (Func<ScriptExecutionContext, CallbackArguments, DynValue>)GetStaticClass;
            pintoLib["getNewClass"] = (Func<ScriptExecutionContext, CallbackArguments, DynValue>)GetNewClass;

            script.Globals["pintoLib"] = pintoLib;
            script.Globals["luaNumber"] = new CallbackFunction((ScriptExecutionContext context, CallbackArguments args) =>
            {
                return DynValue.FromObject(script, new LuaNumber(args[0].Number,
                    args[1].Boolean, args[2].Boolean, args[3].Boolean));
            });
        }

        // CastType(object, target type name)
        private static DynValue CastType(ScriptExecutionContext context, CallbackArguments args) 
        {
            Type targetType = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic)
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.FullName.Equals(args[1].String));
            return DynValue.FromObject(context.OwnerScript, args[0].ToObject().CastToReflected(targetType));
        }

        // GetStaticClass(type name)
        private static DynValue GetStaticClass(ScriptExecutionContext context, CallbackArguments args)
        {
            Type type = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic)
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.FullName.Equals(args[0].String));
            return UserData.CreateStatic(type);
        }

        // GetNewClass(type name, type constructor params)
        private static DynValue GetNewClass(ScriptExecutionContext context, CallbackArguments args)
        {
            string typeName = args[0].String;
            object[] typeArgs = new object[args.Count - 1];
            Type type = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic)
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.FullName.Equals(typeName));

            for (int i = 1; i < args.Count; i++)
            {
                DynValue arg = args[i];
                object typeArg = arg.ToObject();

                if (typeArg is LuaNumber)
                {
                    LuaNumber number = (LuaNumber)typeArg;
                    string nrStr = number.Number.ToString();

                    if (number.IsShort)
                        typeArg = short.Parse(nrStr);
                    else if (number.IsInt)
                        typeArg = int.Parse(nrStr);
                    else if (number.IsFloat)
                        typeArg = float.Parse(nrStr);
                    else
                        typeArg = number.Number;
                }

                typeArgs[i - 1] = typeArg;
            }

            return DynValue.FromObject(context.OwnerScript, Activator.CreateInstance(type, typeArgs));
        }
    }
}
