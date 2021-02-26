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
        public ClientReader Processor;
        public JSEngine JSEngine;

        private bool connected;

        public APIClient(APIServer server, Socket socket, int id)
        {
            this.Server = server;
            this.Socket = socket;
            this.Id = id;
            this.io = new ClientIO(this, socket);
            this.connected = true;
            this.JSEngine = new JSEngine(this);
            Log.Info($"New client: {Name}");
        }

        public Request NextRequest()
        {
            RequestModel model = io.ReadRequest();
            if (model == null)
            {
                return null;
            }
            Request request = new Request(this, model);
            return request;
        }

        public void Close()
        {
            if (connected)
            {
                connected = false;
                Server.RemoveClient(this);
                Log.Info($"Closed client: {Name}");
            }
        }

        public bool Connected
        {
            get => connected;
        }

        public string Name
        {
            get => $"Client#{Id}";
        }

    }
}
