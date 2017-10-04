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
    public class Stones
    {
        [DataMember]
        public List<Stone> list;
        public Stones()
        {
            list = new List<Stone>();
        }
        public void Add(Stone x)
        {
            list.Add(x);
        }

    }
}
