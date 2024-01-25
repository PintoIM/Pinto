using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PintoNS.Networking
{
    /// <summary>
    /// It shits out NetClientHandlers
    /// </summary>
    public class NetClientHandlerFactory
    {
        public static async Task<NetClientHandler> Create(MainForm instance,
            string ip, int port, Action<string> changeConnectionStatus)
        {
            TcpClient tcpClient = new TcpClient();
            tcpClient.ReceiveTimeout = 45000;

            changeConnectionStatus.Invoke("Connecting...");
            try { await TaskEx.Run(() => tcpClient.ConnectAsync(ip, port).Wait(5000)); }
            catch (AggregateException ex) { throw ex.InnerException; }

            if (!tcpClient.Connected)
                throw new PintoConnectionException("Timed out");

            changeConnectionStatus.Invoke("Handshaking...");
            NetClientHandler netHandler = new NetClientHandler(instance, tcpClient);
            await TaskEx.Run(() => netHandler.Handshake());
            return netHandler;
        }
    }
}
