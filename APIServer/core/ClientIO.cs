using APIServer.exception;
using APIServer.model;
using APIServer.util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.core
{
    public class ClientIO
    {
        private APIClient client;
        private Socket socket;

        public ClientIO(APIClient client, Socket socket)
        {
            this.client = client;
            this.socket = socket;
        }

        public RequestModel ReadRequest()
        {
            String json = ReadString();
            return JSON.Parse<RequestModel>(json);
        }

        public void SendResponse(ResponseModel response)
        {
            lock (socket)
            {
                try
                {
                    String json = JSON.Stringify(response);
                    Log.Debug($"{client.Name}.SendResponse: {json}");
                    byte[] buffer = Encoding.UTF8.GetBytes(json);
                    byte[] sizeBuffer = BitConverter.GetBytes(buffer.Length);
                    socket.Send(sizeBuffer);
                    socket.Send(buffer);
                } catch (SocketException)
                {

                }
            }
        }

        public int ReadInt()
        {
            byte[] sizeBuffer = new byte[4];
            int size = socket.Receive(sizeBuffer);
            if (size == 0) {
                client.Close();
                throw new SocketClosedException();
            }
            int result = BitConverter.ToInt32(sizeBuffer, 0);
            Log.Debug($"{client.Name}.ReadInt: {result}");
            return result;
        }

        public String ReadString()
        {
            int bufferSize = ReadInt();
            byte[] buffer = new byte[bufferSize];
            int size = socket.Receive(buffer);
            if (size == 0)
            {
                client.Close();
                throw new SocketClosedException();
            }
            string result = Encoding.UTF8.GetString(buffer);
            Log.Debug($"{client.Name}.ReadString: {result}");
            return result;
        }

    }
}
