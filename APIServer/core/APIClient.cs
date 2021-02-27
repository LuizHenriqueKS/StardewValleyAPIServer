using APIServer.model;
using APIServer.util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
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

        private int lastEventId;
        private readonly List<ClientEventInfo> eventList;

        public APIClient(APIServer server, Socket socket, int id)
        {
            this.Server = server;
            this.Socket = socket;
            this.Id = id;
            this.io = new ClientIO(this, socket);
            this.connected = true;
            this.JSEngine = new JSEngine(this);
            this.eventList = new List<ClientEventInfo>();
            Log.Info($"New client: {Name}");
        }

        public ClientEventInfo AddEvent(Request request, object target, string eventName) 
        { 
            ClientEventInfo info = ClientEventInfoFactory.Create(lastEventId++, request, target, eventName);
            eventList.Add(info);
            info.EventInfo.GetAddMethod().Invoke(info.Target, info.DelegateArgs);
            return info;
        }

        public void RemoteAllEventListeners()
        {
            for (int i = eventList.Count() - 1; i >= 0; i--)
            {
                ClientEventInfo info = eventList[i];
                info.EventInfo.GetRemoveMethod().Invoke(info.Target, info.DelegateArgs);
                eventList.RemoveAt(i);
            }
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
                RemoteAllEventListeners();
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
