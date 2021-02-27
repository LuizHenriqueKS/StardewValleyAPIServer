using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.core
{
    public interface IConverter
    {
        bool CanConvert(object val);
        object Convert(object val);
    }
}
