using APIServer.core;
using APIServer.handler.command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APIServer
{
    public class APIServer
    {

        private int port;
        private string host;
        private TcpListener socketServer;
        private readonly ClientAccepter clientAccepter;
        private readonly Dictionary<int, APIClient> clientDict;
        private readonly Dictionary<Object, List<Request>> requestListDict;

        public readonly CommandManager CommandManager;
        public readonly EventManager EventManager;

        public APIServer()
        {
            clientDict = new Dictionary<int, APIClient>();
            clientAccepter = new ClientAccepter(this);
            requestListDict = new Dictionary<Object, List<Request>>();
            CommandManager = new CommandManager(this);
            EventManager = new EventManager(this);
        }

        public void Listen(string host, int port)
        {
            RequireNonListening();
            this.host = host;
            this.port = port;
            this.socketServer = new TcpListener(this.IPAddress, port);
            clientAccepter.Start();
        }

        public void LoadDefaultHandlers()
        {
            this.CommandManager.AddHandler(new PingCommandHandler());
        }

        public void FireEvent(APIEvent evt)
        {
            EventHandlerHelper helper = new EventHandlerHelper(this, evt);
            this.EventManager.Fire(helper);
        }

        public void HandleRequest(Request request)
        {
            CommandHandlerHelper helper = new CommandHandlerHelper(request.Client, request);
            this.CommandManager.Handle(helper);
        }

        public void AddClient(APIClient client)
        { 
            this.clientDict.Add(client.Id, client);
        }

        public void RemoveClient(APIClient client)
        {
            this.clientDict.Remove(client.Id);
        }

        public void AddRequest(Object key, Request request)
        {
            if (requestListDict.ContainsKey(key))
            {
                List<Request> list = requestListDict[key];
                list.Add(request);
                requestListDict.Add(key, list);
            } else
            {
                List<Request> list = new List<Request>();
                list.Add(request);
                requestListDict.Add(key, list);
            }
        }

        public List<Request> ListRequests(Object key)
        {
            List<Request> requestList;
            if (!requestListDict.TryGetValue(key, out requestList))
            {
                requestList = new List<Request>();
            }
            return requestList;
        }

        private void RequireNonListening(){
            if (this.Listening)
            {
                throw new AlreadyListeningException();
            }
        }

        public bool Listening
        {
            get => socketServer != null;
        }

        public TcpListener SocketServer
        {
            get => socketServer;
        }

        public int Port
        {
            get => this.port;
        }

        public IPAddress IPAddress
        {
            get => IPAddress.Parse(host);
        }

    }
}
