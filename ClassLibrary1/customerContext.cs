using System;
using System.Collections.Generic;
using System.Data.Entity;
 
namespace ClassLibrary1
{
    public class customerContext : DbContext
    {
        public customerContext()
            : base("DBConnection2")
        {

        }
        public DbSet<customer> users { get; set; }      
        public DbSet<PassStorage> pass { get; set; }  
    }
}