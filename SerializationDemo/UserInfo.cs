using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SerializationDemo
{
    public class UserInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }


        public UserInfo() { }
        public UserInfo(int id, string name,DateTime? createTime=null)
        {
            this.ID = id;
            this.Name = name;
            this.CreateTime = createTime;
        }
    }

}
