using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace ClassLibrary1
{
    public class ParamContext : DbContext
    {
        public ParamContext()
            : base("DBConnection4")
        {

        }

        public DbSet<proba> probas { get; set; }
        public DbSet<Stone> stones { get; set; }
    }
}
