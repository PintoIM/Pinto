using System;
using System.Net;
using System.Net.Sockets;

namespace PintoNS.Networking
{
    public class NetworkAddress
    {
        public string IP;
        public int Port;

        public NetworkAddress(string ip, int port)
        {
            IP = ip;
            Port = port;
        }

        public NetworkAddress(string str)
        {
            string[] stringSplitted = str.Split(':');
            IP = stringSplitted[0];
            Port = int.Parse(stringSplitted[1]);
        }

        public NetworkAddress(Socket socket)
        {
            IP = ((IPEndPoint)socket.RemoteEndPoint).Address.ToString();
            Port = ((IPEndPoint)socket.RemoteEndPoint).Port;
        }

        public override string ToString()
        {
            return $"{IP}:{Port}";
        }
    }
}