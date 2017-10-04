using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;//подключение библиотеки сервисмодел
using System.Runtime.Serialization;//подключение сериализации

namespace /*Diplom*/ClassLibrary1
{
    [DataContract]       
        public class Goods
    {       
        [DataMember]
        public List<Good> list;
       public Goods()
        {
            list = new List<Good>();
        }
        public void Add(Good x)
        {
            list.Add(x);
        }
    }
}
