using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.util
{
    public class ClearScriptUtils
    {
        public static Type GetType(object value)
        {
            if (IsHostType(value))
            {
                PropertyInfo propInfo = (PropertyInfo)value.GetType().GetMember("Type")[0];
                Type type = (Type)propInfo.GetValue(value);
                return type;
            } else
            {
                return value.GetType();
            }
        }

        public static bool IsHostType(object value)
        {
            return value.ToString().StartsWith("HostType:");
        }
    }
}
