using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCJS
{
    class Program
    {
        static void Main(string[] args)
        {
            ///序列化
            Order order = new Order(1, DateTime.Now, true);
            order.Items.Add(new OrderItem(new Product("笔记本", 3000), 2));
            order.Items.Add(new OrderItem(new Product("无线键鼠", 126.9), 2));           
            string jsonStr = JsonHelper.Serialize<Order>(order);

            Console.WriteLine("\r\n序列化得到Json字符串:\r\n");         
            Console.WriteLine(jsonStr);

            ///反序列化
            Order dOrder = JsonHelper.Deserialize<Order>(jsonStr);
            Console.WriteLine("\r\n\r\n反序列化结果:");
            Console.WriteLine("\r\nOrderID: " + dOrder.ID.ToString());
            Console.WriteLine("\r\nOrderDate: " + dOrder.OrderDate.ToString("yyyy/MM/dd HH:mm:ss"));
            Console.WriteLine("\r\nUseCoupon: " + dOrder.UseCoupon.ToString());

            foreach (OrderItem item in dOrder.Items)
            {
                Console.WriteLine("\r\n==========================");
                Console.WriteLine("\r\nProduct name: " + item.Product.Name);
                Console.WriteLine("\r\nPrice: " + item.Product.Price.ToString());
                Console.WriteLine("\r\nCount: " + item.Count.ToString());
            }

            Console.ReadLine();
        }
    }
}
