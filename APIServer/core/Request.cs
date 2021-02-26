using APIServer.enums;
using APIServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.core
{
    public class Request
    {
        public readonly APIClient Client;
        public readonly RequestModel Model;

        public Request(APIClient client, RequestModel model)
        {
            this.Client = client;
            this.Model = model;
        }

        public void Reply(ResponseType type, object result)
        {
            ResponseModel response = new ResponseModel();
            response.Type = type;
            response.Result = result;
            response.Id = Model.Id;
            Client.io.SendResponse(response);
        }

    }
}
