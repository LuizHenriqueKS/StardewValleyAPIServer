using APIServer.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.core
{
    public class APIClient
    {
        public readonly APIServer Server;
        public readonly int Id;
        public readonly Socket Socket;
        public readonly ClientIO io;
        public ClientProcessor Processor;

        private bool connected;

        public APIClient(APIServer server, Socket socket, int id)
        {
            this.Server = server;
            this.Socket = socket;
            this.Id = id;
            this.io = new ClientIO(socket);
        }

        public Request NextRequest()
        {
            RequestModel model = io.ReadRequest();
            Request request = new Request(this, model);
            return request;
        }

        public void Close()
        {
            connected = false;

            Server.RemoveClient(this);
        }

        public bool Connected
        {
            get => connected;
        }

    }
}
