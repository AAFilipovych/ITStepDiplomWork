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
    public class Groups
    {
        [DataMember]
        public List<Group> list;
        public Groups()
        {
            list = new List<Group>();
        }
        public Groups(Groups gr)
        {
            this.list = gr.list;
        }
        public void Add(Group x)
        {
            list.Add(x);
        }
    }
}
