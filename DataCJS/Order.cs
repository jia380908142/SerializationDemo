using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataCJS
{
    [DataContract]
    public class Order
    {
        private List<OrderItem> items;

        public Order()
        {
        }

        public Order(int id, DateTime orderDate, bool useCoupon)
        {
            this.ID = id;
            this.OrderDate = orderDate;
            this.UseCoupon = useCoupon;
            this.items = new List<OrderItem>();
        }

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public DateTime OrderDate { get; set; }

        [DataMember]
        public bool UseCoupon { get; set; }

        [DataMember]
        public List<OrderItem> Items
        {
            get { return items; }
            set { items = value; }
        }
    }

    [DataContract]
    public class OrderItem
    {
        public OrderItem(Product product, int count)
        {
            this.Product = product;
            this.Count = count;
        }

        [DataMember]
        public Product Product { get; set; }

        [DataMember]
        public int Count { get; set; }
    }

    [DataContract]
    public class Product
    {
        public Product(string name, double price)
        {
            this.Name = name;
            this.Price = price;
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public double Price { get; set; }
    }
}
