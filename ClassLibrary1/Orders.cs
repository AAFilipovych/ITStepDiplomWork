using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;//подключение библиотеки сервисмодел
using System.Runtime.Serialization;//подключение сериализации

namespace ClassLibrary1
{
    [DataContract]
    public class Orders
    {
        [DataMember]
        public List<Order> list;
        public Orders()
        {
            list = new List<Order>();
        }
        public void Add(Order x)
        {
            list.Add(x);
        }
    }
}



