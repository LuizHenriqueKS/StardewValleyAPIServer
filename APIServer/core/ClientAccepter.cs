using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APIServer.core
{
    class ClientAccepter
    {
        private APIServer server;
        private Thread thread;
        private int lastClientId;

        public ClientAccepter(APIServer server)
        {
            this.server = server;
        }

        public void Start()
        {
            thread = new Thread(new ThreadStart(ListenSockets));
            thread.Start();
        }

        public void Stop()
        {
            thread.Interrupt();
        }

        private void ListenSockets()
        {
            Socket socket = server.SocketServer.AcceptSocket();
            APIClient client = BuildClient(socket);
            server.AddClient(client);
            client.Processor = new ClientProcessor(client);
            client.Processor.Start();
        }

        private APIClient BuildClient(Socket socket)
        {
            APIClient client = new APIClient(server, socket, ++lastClientId);
            return client;
        }

    }
}
