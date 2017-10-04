using System;
using System.Collections.Generic;
using System.Data.Entity;
 
namespace ClassLibrary1
{
    public class GoodContext : DbContext
    {
        public GoodContext()
            : base("DBConnection")
        {
                        
        }

        public DbSet<Good> goods { get; set; }
        public DbSet<Metall> metalls { get; set; }        
        public DbSet<Group> groups { get; set; }
       
    }
}
