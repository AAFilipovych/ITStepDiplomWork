using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Sale
    {
        //идентификационный номер
        public int Id { get; set; }
        //фамилия
        public string FirstName { get; set; }
        //имя
        public string SecondName { get; set; }
        //отчетсво
        public string ThirdName { get; set; }
        //телефон
        public long Phone { get; set; }
        //дата покупки
        public DateTime when_buy { get; set; }
        //перечень купленного в заказе
        public int id_buy { get; set; }
        public DateTime when_done { get; set; }
        public bool done { get; set; }
    }
}
