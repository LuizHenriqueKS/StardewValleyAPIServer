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
    public class ClientIO
    {
        private Socket socket;

        public ClientIO(Socket socket)
        {
            this.socket = socket;
        }

        public RequestModel ReadRequest()
        {
            String json = ReadString();
            return JsonConvert.DeserializeObject<RequestModel>(json);
        }

        public void SendResponse(ResponseModel response)
        {
            lock (socket)
            {
                String json = JsonConvert.SerializeObject(response);
                byte[] buffer = Encoding.ASCII.GetBytes(json);
                byte[] sizeBuffer = BitConverter.GetBytes(buffer.Length);
                socket.Send(sizeBuffer);
                socket.Send(buffer);
            }
        }

        public int ReadInt()
        {
            byte[] sizeBuffer = new byte[8];
            socket.Receive(sizeBuffer);
            return BitConverter.ToInt32(sizeBuffer, 0);
        }

        public String ReadString()
        {
            int size = ReadInt();
            byte[] buffer = new byte[size];
            socket.Receive(buffer);
            return BitConverter.ToString(buffer);
        }

    }
}
