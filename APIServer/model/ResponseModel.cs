using APIServer.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.model
{
    public class ResponseModel
    {
        public int Id;
        public ResponseType Type;
        public dynamic Result;
    }
}
