using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Drawing;
using System.IO;


namespace ClassLibrary1
{
    public class ParamsInitializer : DropCreateDatabaseAlways<ParamContext>
    // public class ParamsInitializer : DropCreateDatabaseIfModelChanges<ParamContext>
    //public class ParamsInitializer : CreateDatabaseIfNotExists<ParamContext>
    {
        protected override void Seed(ParamContext dbp)
        {

            dbp.probas.Add(new proba { number = "375", metall = 1 });
            dbp.probas.Add(new proba { number = "500", metall = 1 });
            dbp.probas.Add(new proba { number = "583", metall = 1 });
            dbp.probas.Add(new proba { number = "585", metall = 1 });
            dbp.probas.Add(new proba { number = "750", metall = 1 });
            dbp.probas.Add(new proba { number = "958", metall = 1 });


            dbp.probas.Add(new proba { number = "375", metall = 2 });
            dbp.probas.Add(new proba { number = "800", metall = 2 });
            dbp.probas.Add(new proba { number = "830", metall = 2 });
            dbp.probas.Add(new proba { number = "925", metall = 2 });


            dbp.stones.Add(new Stone { Name = "Без камней", s_k = "" });
            dbp.stones.Add(new Stone { Name = "Рубин", s_k = "rubin" });
            dbp.stones.Add(new Stone { Name = "Агат", s_k = "agat" });
            dbp.stones.Add(new Stone { Name = "Изумруд", s_k = "izumrud" });
            dbp.stones.Add(new Stone { Name = "Бриллиант", s_k = "briliant" });
            dbp.stones.Add(new Stone { Name = "Жемчуг", s_k = "jemchug" });

            base.Seed(dbp);
        }


    }
}
