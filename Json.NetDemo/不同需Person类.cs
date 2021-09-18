using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json.NetDemo
{

    //类似本问开头介绍的接口优化，实体中有些属性不需要序列化返回，可以使用该特性。首先介绍Json.Net序列化的模式:OptOut 和 OptIn
    //OptOut	默认值,类中所有公有成员会被序列化,如果不想被序列化,可以用特性JsonIgnore
    // OptIn 默认情况下, 所有的成员不会被序列化, 类中的成员只有标有特性JsonProperty的才会被序列化, 当类的成员很多, 但客户端仅仅需要一部分数据时, 很有用
   
    [JsonObject(MemberSerialization.OptIn)]
    public class Person1
    {      
        public int Age { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        public string Sex { get; set; }


        //自定义了BoolConvert类型，继承自JsonConverter。构造函数参数BooleanString可以让我们自定义将true false转换成相应字符串。
        [JsonConverter(typeof(BoolConvert))]
        [JsonProperty]
        public bool IsMarry { get; set; }

        [JsonProperty]
        public DateTime  Birthday { get; set; }
    }

    [JsonObject(MemberSerialization.OptOut)]
    public class Person11
    {

        [DefaultValue(10)] //默认值
        public int? Age { get; set; }
     
        public string Name { get; set; }

        [JsonProperty(PropertyName ="性别")]  //n自定义序列化的字段名称
        public string Sex { get; set; }

        [JsonIgnore]     
        public bool IsMarry { get; set; }

        //[JsonConverter(typeof(IsoDateTimeConverter))]
        //对于Dateime类型日期的格式化就比较麻烦了，系统自带的会格式化成iso日期标准，但是实际使用过程中大多数使用的可能是yyyy-MM-dd 或者yyyy-MM-dd HH:mm:ss两种格式的日期，解决办法是可以将DateTime类型改成string类型自己格式化好，然后在序列化。如果不想修改代码，可以采用下面方案实现。
        [JsonConverter(typeof(ChinaDateTimeConverter))]
        public DateTime? Birthday { get; set; }
    }
}
