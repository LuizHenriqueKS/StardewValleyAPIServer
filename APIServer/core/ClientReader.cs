using APIServer.exception;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APIServer.core
{
    public class ClientReader
    {
        public readonly APIClient Client;
        private Thread thread;

        public ClientReader(APIClient client)
        {
            this.Client = client;
        }

        public void Start()
        {
            thread = new Thread(new ThreadStart(ReadRequests));
            thread.Start();
        }

        public void Stop()
        {
            thread.Interrupt();
        }

        private void ReadRequests()
        {
            while (Client.Connected)
            {
                try
                {
                    Request request = Client.NextRequest();
                    if (request != null)
                    {
                        Client.Server.HandleRequest(request);
                    }
                }
                catch (JsonSerializationException ex)
                {
                    Log.Error("Error read request");
                }
                catch (SocketClosedException ex)
                {
                    Client.Close();
                }
                catch (SocketException ex)
                {
                    Client.Close();
                }
            }
        }

    }
}
