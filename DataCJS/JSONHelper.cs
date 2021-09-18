using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace DataCJS
{
    public class JsonHelper
    {
        public static string Serialize<T>(T obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                string retVal = Encoding.UTF8.GetString(ms.ToArray());                    
                return retVal;
            }
        }

        public static T Deserialize<T>(string json) 
        {
            T obj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            ms.Dispose();
            return obj;
        }
    }
}
