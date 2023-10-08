using PintoNS.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PintoNS.General
{
    public struct PintoPluginInfo
    {
        public string Name;
        public string Author;
        public string Version;
    }

    public interface IPintoPluginListener
    {
    }

    public class PintoPluginListener : IPintoPluginListener
    {
        public virtual void OnConnected(NetworkManager networkManager)
        {
        }

        public virtual void OnDisconnected()
        {
        }
    }

    public interface IPintoPlugin
    {
        PintoPluginInfo GetInfo();
        IPintoPluginListener GetListener();
        bool OnLoad();
        void OnMainFormLoad(MainForm mainForm);
    }

    public struct PintoPluginHost
    {
        public string AssemblyPath;
        public Assembly Assembly;
        public IPintoPlugin Plugin;
    }
}
