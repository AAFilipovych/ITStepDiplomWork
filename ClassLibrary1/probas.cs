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
    public class probas
    {
        [DataMember]
        public List<proba> list;
        public probas()
        {
            list = new List<proba>();
        }
        public void Add(proba x)
        {
            list.Add(x);
        }
    }
}