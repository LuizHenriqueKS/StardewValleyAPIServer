using APIServer.core;
using APIServer.handler.command;
using APIServer.handler.events;
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
        private bool listening;
        private TcpListener socketServer;
        private readonly ClientAccepter clientAccepter;
        private readonly Dictionary<int, APIClient> clientMap;
        private readonly Dictionary<Object, List<Request>> requestListDict;

        public readonly CommandManager CommandManager;
        public readonly EventManager EventManager;

        public APIServer()
        {
            clientMap = new Dictionary<int, APIClient>();
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
            this.socketServer.Start();
            clientAccepter.Start();
            listening = true;
        }

        public void LoadDefaultHandlers()
        {
            this.CommandManager.AddHandler(new PingCommandHandler());
            this.CommandManager.AddHandler(new RunJSCommandHandler());

            this.CommandManager.AddHandler(new ListenButtonPressedEventCommandHandler());

            this.EventManager.AddHandler(new ButtonPressedEventHandler());
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
            this.clientMap.Add(client.Id, client);
        }

        public void RemoveClient(APIClient client)
        {
            this.clientMap.Remove(client.Id);
            this.RemoveReferences(client);
        }

        public void RemoveReferences(APIClient client)
        {
            foreach(List<Request> list in requestListDict.Values)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    Request req = list[i];
                    if (req.Client.Id == client.Id)
                    {
                        list.RemoveAt(i);
                    }
                }
            }
        }

        public void AddRequest(Object key, Request request)
        {
            if (requestListDict.ContainsKey(key))
            {
                List<Request> list = requestListDict[key];
                list.Add(request);
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
            get => listening;
        }

        public TcpListener SocketServer
        {
            get => socketServer;
        }

        public String Host
        {
            get => this.host;
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
