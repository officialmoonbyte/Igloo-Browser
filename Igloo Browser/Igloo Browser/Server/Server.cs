using Igloo.Logger;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Igloo.Server
{
    public class TcpServer
    {

        TcpListener ServerListener;
        List<ServerClient> clientList = new List<ServerClient>();

        /// <summary>
        /// Starts multithreaded server thread.
        /// </summary>
        public void StartServer()
        {
            Thread clientHandlingThread = new Thread(new ThreadStart(ServerThread));
            clientHandlingThread.Start();
        }

        public void SendClient(string information)
        {
            //Sends everysingle connected client information.
            for (int i = 0; i < clientList.Count; i++)
            { clientList[i].SendClientInformation(information); }
        }

        /// <summary>
        /// Multithreaded server thread used to run the server
        /// </summary>
        private void ServerThread()
        {
            //Starts the server
            ServerListener = new TcpListener(IPAddress.Any, 6189);
            ServerListener.Start();

            //Starts the client thread
            new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    try
                    {
                        //Accepts tcp client
                        var ClientTrack = ServerListener.AcceptTcpClientAsync();

                        //Breaks out of the client loop if the client disconnects
                        while (ClientTrack.Result == null) { break; }

                        //Setup a new client
                        ServerClient client = new ServerClient(ClientTrack.Result, ServerListener);
                        clientList.Add(client);
                    }
                    catch { }
                }
            })).Start();
        }

        /// <summary>
        /// Stops the server
        /// </summary>
        public void StopServer()
        {
            ILogger.AddToLog("TCP Server", "Stopping listening on TCP server");
            ServerListener.Stop();
        }
    }
}
