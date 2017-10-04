using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
   public class Transfer
    {
        
        //цена
        public decimal PriceUP { get; set; }
        public decimal PriceLOW { get; set; }
        //Вес
        public decimal WeightUP { get; set; }
        public decimal WeightLOW { get; set; }
        public string Category { get; set; }
        //Металл
        public string Metall { get; set; }
        public string Proba { get; set; }
        public string Stone { get; set; }
    }
}
