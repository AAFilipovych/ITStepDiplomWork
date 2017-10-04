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
    public class Metalls
    {
        [DataMember]
        public List<Metall> list;
        public Metalls()
        {
            list = new List<Metall>();
        }
        public void Add(Metall x)
        {
            list.Add(x);
        }
    }
}
