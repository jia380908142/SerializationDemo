using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json.NetDemo
{
    public enum SexType
    {
        男,
        女,
        其他
    }
    public class Person
    {
        public int Age { get; set; }
      
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SexType Sex { get; set; }
        
        public bool IsMarry { get; set; }

        public DateTime Birthday { get; set; }
    }
}
