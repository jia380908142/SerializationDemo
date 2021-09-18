using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
namespace Json.NetDemo
{
   
    class Program
    {
        static void Main(string[] args)
        {
            #region  序列化DataTable

            //序列化DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("Age", Type.GetType("System.Int32"));
            dt.Columns.Add("Name", Type.GetType("System.String"));
            dt.Columns.Add("Sex", Type.GetType("System.String"));
            dt.Columns.Add("IsMarry", Type.GetType("System.Boolean"));
            for (int i = 0; i < 4; i++)
            {
                DataRow dr = dt.NewRow();
                dr["Age"] = i + 1;
                dr["Name"] = "Name" + i;
                dr["Sex"] = i % 2 == 0 ? "男" : "女";
                dr["IsMarry"] = i % 2 > 0 ? true : false;
                dt.Rows.Add(dr);
            }
            string dejsonstr = JsonConvert.SerializeObject(dt, Formatting.Indented);
            Console.WriteLine("\r\n序列化DataTable得到Json字符串:\r\n");
            Console.WriteLine(dejsonstr);


            ///反序列化           
            Console.WriteLine("\r\n\r\n反序列化结果:");
            dt = JsonConvert.DeserializeObject<DataTable>(dejsonstr);
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t", dr[0], dr[1], dr[2], dr[3]);
            }

            #endregion

            #region 序列化List<Person>
            if (true)
            {
                var person1 = new Person()
                {
                    Name = "余欢水",
                    Age = 32,
                    Sex = SexType.男,
                    IsMarry = true,
                    Birthday = DateTime.Now.AddYears(-32).Date
                };
                var person2 = new Person()
                {
                    Name = "小风",
                    Age = 15,
                    Sex = SexType.男,
                    IsMarry = false,
                    Birthday = DateTime.Now.AddYears(-15).Date
                };
                var persons = new List<Person>() { person1, person2 };
                string json1 = JsonConvert.SerializeObject(persons, Formatting.Indented);
                Console.WriteLine("\r\n序列化Psrsons得到Json字符串:\r\n");
                Console.WriteLine(json1);



                ///反序列化           
                Console.WriteLine("\r\n\r\n反序列化结果:\r\n");
                var newPersons = JsonConvert.DeserializeObject<List<Person>>(json1);
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t\r\n", "姓名", "年龄", "性别", "婚否", "出生日期");
                newPersons.ForEach(item =>
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t", item.Name, item.Age, item.Sex, item.IsMarry, item.Birthday.ToString("yyyy/MM/dd"));
                });

            }
            #endregion

            #region 忽略某些属性
            //类似本问开头介绍的接口优化，实体中有些属性不需要序列化返回，可以使用该特性。首先介绍Json.Net序列化的模式:OptOut 和 OptIn.
           // OptOut 默认值, 类中所有公有成员会被序列化, 如果不想被序列化, 可以用特性JsonIgnore
           //OptIn 默认情况下, 所有的成员不会被序列化, 类中的成员只有标有特性JsonProperty的才会被序列化, 当类的成员很多, 但客户端仅仅需要一部分数据时, 很有用
            if (true)
            {
                var person1 = new Person1()
                {
                    Name = "余欢水",
                    Age = 32,
                    Sex = "男",
                    IsMarry = true,
                    Birthday = DateTime.Now.AddYears(-32).Date
                };
                string json1 = JsonConvert.SerializeObject(person1, Formatting.Indented);
                Console.WriteLine("\r\n忽略某些属性,仅需要姓名属性\r\n");


                Console.WriteLine(json1);
            }

            if (true)
            {
                var person1 = new Person11()
                {
                    Name = "余欢水",
                    Age = 32,
                    Sex = "男",
                    IsMarry = true,
                    Birthday = DateTime.Now.AddYears(-32).Date
                };
                string json1 = JsonConvert.SerializeObject(person1, Formatting.Indented);
                Console.WriteLine("\r\n忽略某些属性, 不需要是否结婚属性\r\n");

                Console.WriteLine(json1);
            }

            #endregion

            #region 默认值的处理 || 及日期处理
            // 序列化时想忽略默认值属性可以通过JsonSerializerSettings.DefaultValueHandling来确定，该值为枚举值.
            //在Birthday属性上添加 [JsonConverter(typeof(ChinaDateTimeConverter))]，ChinaDateTimeConverter继承了DateTimeConverterBase，进行了重写。
            if (true)
            {
                var person1 = new Person11()
                {
                    Name = "余欢水",
                    Age = 10,
                    Sex = "男",
                    IsMarry = true,
                    Birthday = DateTime.Now.AddYears(-32).Date
                };
                JsonSerializerSettings seting = new JsonSerializerSettings();
                seting.DefaultValueHandling = DefaultValueHandling.Ignore;
                string json1 = JsonConvert.SerializeObject(person1, Formatting.Indented, seting);
                Console.WriteLine("\r\n默认值的处理\r\n");


                Console.WriteLine(json1);
            }
            #endregion

            #region 空值的处理
            //序列化时需要忽略值为NULL的属性，可以通过JsonSerializerSettings.NullValueHandling来确定，另外通过JsonSerializerSettings设置属性是对序列化过程中所有属性生效的，想单独对某一个属性生效可以使用[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
            if (true)
            {
                var person1 = new Person11()
                {
                    Name = "余欢水",
                    Age = 32,
                    Sex = "男",
                    IsMarry = true,
                    Birthday = null
                };
                JsonSerializerSettings seting = new JsonSerializerSettings();
                seting.NullValueHandling = NullValueHandling.Ignore;
                string json1 = JsonConvert.SerializeObject(person1, Formatting.Indented, seting);
                Console.WriteLine("\r\n空值的处理\r\n");


                Console.WriteLine(json1);
            }

            #endregion


            #region n自定义序列化的字段名称
            //在属性Sex属性上加 [JsonProperty(PropertyName = "性别")]
            if (true)
            {
                var person1 = new Person11()
                {
                    Name = "余欢水",
                    Age = 32,
                    Sex = "男",
                    IsMarry = true,
                    Birthday = DateTime.Now.AddYears(-32).Date
                };
                string json1 = JsonConvert.SerializeObject(person1, Formatting.Indented);
                Console.WriteLine("\r\n自定义序列化的字段名称\r\n");


                Console.WriteLine(json1);
            }

            #endregion
          

            #region 动态决定属性是否序列化            
            //其它的都不变，在Sex属性上加上了JsonConverter(typeof(StringEnumConverter))表示将枚举值转换成对应的字符串,而StringEnumConverter是Newtonsoft.Json内置的转换类型,
            if (true)
            {
                var person = new Person()
                {
                    Name = "余欢水",
                    Age = 32,
                    Sex = SexType.男,
                    IsMarry = true,
                    Birthday = DateTime.Now.AddYears(-32).Date
                };
                JsonSerializerSettings jsetting = new JsonSerializerSettings();
                jsetting.ContractResolver = new LimitPropsContractResolver(new string[] { "Age", "IsMarry" });
                string json1=JsonConvert.SerializeObject(person, Formatting.Indented, jsetting);
                Console.WriteLine("\r\n动态决定属性是否序列化:\r\n");
                Console.WriteLine(json1);
            }
            #endregion

            #region 枚举值的自定义格式化问题            
            //其它的都不变，在Sex属性上加上了JsonConverter(typeof(StringEnumConverter))表示将枚举值转换成对应的字符串,而StringEnumConverter是Newtonsoft.Json内置的转换类型,
            if (true)
            {
                var person = new Person()
                {
                    Name = "余欢水",
                    Age = 32,
                    Sex = SexType.男,
                    IsMarry = true,
                    Birthday = DateTime.Now.AddYears(-32).Date
                };
                string json1 = JsonConvert.SerializeObject(person, Formatting.Indented);
                Console.WriteLine("\r\n枚举值的自定义格式化问题:\r\n");
                Console.WriteLine(json1);
            }
            #endregion


            #region 自定义bool类型转换
            //在属性IsMarry上添加  [JsonConverter(typeof(BoolConvert))]
            //自定义了BoolConvert类型，继承自JsonConverter。构造函数参数BooleanString可以让我们自定义将true false转换成相应字符串。
            if (true)
            {
                var person = new Person1()
                {
                    Name = "余欢水",
                    Age = 32,
                    Sex = "男",
                    IsMarry = true,
                    Birthday = DateTime.Now.AddYears(-32).Date
                };
                string json1 = JsonConvert.SerializeObject(person, Formatting.Indented);
                Console.WriteLine("\r\n自定义bool类型转换:\r\n");
                Console.WriteLine(json1);
            }
            #endregion


            #region 全局序列化设置
            //在属性IsMarry上添加  [JsonConverter(typeof(BoolConvert))]
            //自定义了BoolConvert类型，继承自JsonConverter。构造函数参数BooleanString可以让我们自定义将true false转换成相应字符串。
            if (true)
            {
                var person = new Person11()
                {
                    Name = "余欢水",
                    Age = null,
                    Sex = "男",
                    IsMarry = true,
                    Birthday = DateTime.Now.AddYears(-32)
                };            
                JsonSerializerSettings setting = new JsonSerializerSettings();
                JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
                {
                    //日期类型默认格式化处理
                    setting.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                    setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";

                    //空值处理
                    setting.NullValueHandling = NullValueHandling.Ignore;

                    //高级用法九中的Bool类型转换 设置
                    //setting.Converters.Add(new BoolConvert("是,否"));

                    return setting;
                });
                string json1 = JsonConvert.SerializeObject(person, Formatting.Indented);
                Console.WriteLine("\r\n全局序列化设置:\r\n");
                Console.WriteLine(json1);
            }
            #endregion


            Console.ReadLine();
        }

    }
}
