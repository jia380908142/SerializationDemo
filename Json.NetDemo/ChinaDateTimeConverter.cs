using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json.NetDemo
{
   // 对于Dateime类型日期的格式化就比较麻烦了，系统自带的会格式化成iso日期标准，但是实际使用过程中大多数使用的可能是yyyy-MM-dd 或者yyyy-MM-dd HH:mm:ss两种格式的日期，解决办法是可以将DateTime类型改成string类型自己格式化好，然后在序列化。如果不想修改代码，可以采用下面方案实现。
   //但是IsoDateTimeConverter日期格式不是我们想要的，我们可以继承该类实现自己的日期
    public class ChinaDateTimeConverter : DateTimeConverterBase
    {
        private static IsoDateTimeConverter dtConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
          return   dtConverter.ReadJson(reader, objectType, existingValue, serializer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            dtConverter.WriteJson(writer, value, serializer);
        }
    }
}
