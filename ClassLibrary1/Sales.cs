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
    public class Sales
    {
        [DataMember]
        public List<Sale> list;
        public Sales()
        {
            list = new List<Sale>();
        }
        public void Add(Sale x)
        {
            list.Add(x);
        }
        public void removeat(int id)
        {
            list.RemoveAt(id);
        }
    }
}
