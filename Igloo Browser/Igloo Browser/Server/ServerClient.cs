namespace Igloo.Server
{
    class ServerClient
    {
        //Local values for the server
        TcpClient LocalClientConnection;
        TcpListener LocalServerListener;

        /// <summary>
        /// Sets all local values of the ServerClient
        /// </summary>
        public ServerClient(TcpClient client, TcpListener ServerListener)
        {
            LocalClientConnection = client;
            LocalServerListener = ServerListener;
            ServerConsole();
        }

        /// <summary>
        /// Sends the client information.
        /// </summary>
        public void SendClientInformation(string Information)
        {
            try
            {
                //Sends client information
                LocalClientConnection.Client.Send(Encoding.UTF8.GetBytes(Information));
            }
            catch { }
        }

        /// <summary>
        /// Connection to the server console.
        /// </summary>
        private void ServerConsole()
        {
            new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    //Check if the client stream is available
                    NetworkStream clientStream; try { clientStream = LocalClientConnection.GetStream(); }
                    catch { Console.WriteLine("Client has disconnected!"); break; }

                    //Gets the client request
                    string ClientResult = Receiver(clientStream);

                    if (ClientResult != null)
                    {
                        //Split the client result.
                        string[] CommandArgs = ClientResult.Split(' ');

                        //Check if the command is equal to forward
                        if (CommandArgs[0] == "newpage")
                        {
                            BrowserWindow window = new BrowserWindow();
                            Program.InvokeOnUI.Invoke((MethodInvoker)delegate ()
                            {
                                window.Show();
                            });
                        }
                    }
                }
            })).Start();

        }

        //Receive a byte from the server and transcribe the byte into a string.
        private string Receiver(NetworkStream m)
        {
            try
            {
                //Checks if the NetworkStream can read from the socket request.
                if (m.CanRead)
                {
                    if (LocalClientConnection.Connected)
                    {
                        //Sets the byte value and then read the network stream.
                        byte[] bytes = new byte[LocalClientConnection.ReceiveBufferSize];
                        m.Read(bytes, 0, LocalClientConnection.ReceiveBufferSize);

                        //Encodes the bytes into a string, and than trim the string.
                        string ReceivedBytes = Encoding.UTF8.GetString(bytes);
                        ReceivedBytes = ReceivedBytes.Trim('\0');

                        //List in console, client response
                        ILogger.AddToLog("INFO", "Got client response! " + ReceivedBytes);

                        //Flushes the Network Stream
                        m.Flush();

                        //Return bytes
                        return ReceivedBytes;
                    }
                    else
                    {
                        Console.WriteLine("Client disconnected!");
                        LocalClientConnection.Close();
                        LocalClientConnection.Dispose();
                        return null;
                    }
                }
                else
                {
                    //Return error if Network Stream does not support reading.
                    Console.WriteLine("[Error] Client does not support reading!");
                }
            }
            catch
            {
                //Display a error, Client Disconnect.
                Console.WriteLine("[Server] Client disconnected!");
                LocalClientConnection.Close();
            }

            //Return null (method should not get up to this point, unless there is an error.
            return null;
        }
    }
}
