using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APIServer.core
{
    public class ClientProcessor
    {
        public readonly APIClient Client;
        private Thread thread;

        public ClientProcessor(APIClient client)
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
                Request request = Client.NextRequest();
                if (request != null)
                {
                    Client.Server.HandleRequest(request);
                }
            }
        }

    }
}
