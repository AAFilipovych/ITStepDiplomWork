using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ClassLibrary1
{
    public class Good
    {
        //ID товара
        public int Id { get; set; }
        //название товара
        public string Name { get; set; }
        //описание товара
        public string Describe { get; set; }
        //цена
        public decimal Price { get; set; }
        //Вес
        public decimal Weight { get; set; }
        //Металл
        public string Metall { get; set; }
        public string m_k { get; set; }
        public string proba { get; set; }
        //Категория
        public string Category { get; set; }
        public string c_k { get; set; }
        //драгоценные камни
        public string stone { get; set; }
        public string s_k { get; set; }
        //Фотографии
        public byte[] Photo1 { get; set; }
        public byte[] Photo2 { get; set; }
        public byte[] Photo3 { get; set; }
        public byte[] Photo4 { get; set; }
    }
}



