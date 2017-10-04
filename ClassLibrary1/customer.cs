using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class customer
    {
        //ID товара
        public int Id { get; set; }
        //Фамилия
        public string FName { get; set; }
        //Имя
        public string SName { get; set; }
        //Отчество
        public string TName { get; set; }
        //телефон
        public long phone { get; set; }
        //другие данные
        public string data { get; set; }    
        //ссылка на базу паролей
        public int login_id { get; set; }
    }
}
