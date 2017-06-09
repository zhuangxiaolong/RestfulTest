using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RestfulClient
{
    public class JsonDeserializer 
    {
        public static object Deserialize(string content, Type objectType)
        {
            var serializer = new JsonSerializer();
            var jsonTextReader = new JsonTextReader(new StringReader(content));

            var method = typeof(JsonSerializer).GetMethods().First(m => m.Name == "Deserialize" && m.IsGenericMethod);
            var generic = method.MakeGenericMethod(objectType);

            return generic.Invoke(serializer, new object[] { jsonTextReader });
        }

    }
}
