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
    public class GoodsInitializer : DropCreateDatabaseAlways<GoodContext>
    // public class GoodsInitializer : DropCreateDatabaseIfModelChanges<GoodContext>
    //public class GoodsInitializer : CreateDatabaseIfNotExists<GoodContext>
    {
        protected override void Seed(GoodContext db)
        {
            db.metalls.Add(new Metall { Name = "Золото", m_k = "Gold" });
            db.metalls.Add(new Metall { Name = "Серебро", m_k = "Silver" });

            db.groups.Add(new Group { Name = "Кольца", c_k = "Rings" });
            db.groups.Add(new Group { Name = "Серьги", c_k = "Sergi" });
            db.groups.Add(new Group { Name = "Крестики", c_k = "Cross" });
            db.groups.Add(new Group { Name = "Цепочки", c_k = "Zep" });
            db.groups.Add(new Group { Name = "Кулоны", c_k = "Kulon" });

            string path = Directory.GetCurrentDirectory()+"\\rings\\";
            
            
            for (int i = 1; i < 10; ++i)
                db.goods.Add(new Good { Name = "g"+i.ToString(), Describe = "d"+i.ToString(), Price = 1, Weight = 1, Metall = "Золото", m_k = "Gold", proba = "750", Category = "Кольца", c_k = "Rings", stone="Рубин",s_k="rubin", Photo1 = ImageToByteArray(Image.FromFile(path+ "gold\\" + i+".jpeg")), Photo2 = ImageToByteArray(Image.FromFile(path + "gold\\" + i + ".jpeg")), Photo3 = ImageToByteArray(Image.FromFile(path + "gold\\" + i + ".jpeg")), Photo4 = ImageToByteArray(Image.FromFile(path + "gold\\" + i + ".jpeg")) });
            for (int i=1;i<=12;++i)
            db.goods.Add(new Good { Name = "s" + i.ToString(), Describe = "d" + i.ToString(), Price = 2, Weight = 2, Metall = "Серебро", m_k = "Silver", proba = "925", Category = "Кольца", c_k = "Rings",stone="Жемчуг",s_k="jemchug", Photo1 = ImageToByteArray(Image.FromFile(path + "silver\\" + i + ".jpeg")), Photo2 = ImageToByteArray(Image.FromFile(path + "silver\\" + i + ".jpeg")), Photo3 = ImageToByteArray(Image.FromFile(path + "silver\\" + i + ".jpeg")), Photo4 = ImageToByteArray(Image.FromFile(path + "silver\\" + i + ".jpeg")) });
            
            base.Seed(db);
           
        }
        

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }

      
    }
}
