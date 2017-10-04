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
    class customers
    {
        [DataMember]
        public List<customer> list;
        public customers()
        {
            list = new List<customer>();
        }
        public void Add(customer x)
        {
            list.Add(x);
        }

    }
}
