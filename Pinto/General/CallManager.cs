using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PintoNS.General
{
    public class CallManager
    {
        private AudioRecorderPlayer audioRecPlay = new AudioRecorderPlayer();
        private IPEndPoint remote;
        private UdpClient client;
        private NATUPNPLib.UPnPNATClass router;
        private NATUPNPLib.IStaticPortMapping routerMapping;

    }
}
