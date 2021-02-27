using APIServer.converter.response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.core
{
    public class ResponseConverterManager
    {
        private static List<IConverter> converterList;

        public static object Convert(object val)
        {
            if (converterList == null)
            {
                LoadConvertList();
            }
            foreach (IConverter conv in converterList)
            {
                if (conv.CanConvert(val))
                {
                    return conv.Convert(val);
                }
            }
            return val;
        }

        private static void LoadConvertList()
        {
            converterList = new List<IConverter>();
            converterList.Add(new HostTypeConverter());
        }

    }
}
