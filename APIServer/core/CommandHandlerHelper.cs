using APIServer.enums;
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
    public class CommandHandlerHelper
    {
        public readonly APIServer Server;
        public readonly APIClient Client;
        public readonly Request Request;
        public readonly Command Command;

        public CommandHandlerHelper(APIClient client, Request request)
        {
            this.Server = client.Server;
            this.Client = client;
            this.Request = request;
            this.Command = request.Model.Command;
        }

        public void Reply(ResponseType type, object result)
        {
            this.Request.Reply(type, result);
        }

    }
}
