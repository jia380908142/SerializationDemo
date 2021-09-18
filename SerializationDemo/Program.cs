using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace SerializationDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            List<UserInfo> userList = new List<UserInfo>();
            userList.Add(new UserInfo() { ID = 1, Name = "张三", CreateTime = DateTime.Now });
            userList.Add(new UserInfo() { ID = 2, Name = "李四", CreateTime = DateTime.Now });
            userList.Add(new UserInfo() { ID = 2, Name = "王五" });

            //创建一个JavaScriptSerializer对象
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            //将用户列表序列化成JSON
            string serializedResult = serializer.Serialize(userList);

            Console.WriteLine(serializedResult);
            //将JOSN反序列化成用户列表
            List<UserInfo> deserializeResult = serializer.Deserialize<List<UserInfo>>(serializedResult);
            Console.WriteLine(deserializeResult[0].Name);
            Console.WriteLine(deserializeResult[0].CreateTime.Value.ToString("yyyy-MM-dd"));
            
            Console.ReadLine();

        }
    }
}
