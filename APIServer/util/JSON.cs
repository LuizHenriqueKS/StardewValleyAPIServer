using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.util
{
    public class JSON
    {
        public static string Stringify(object obj)
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return JsonConvert.SerializeObject(obj, serializerSettings);
        }

        public static T Parse<T>(string json) 
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
