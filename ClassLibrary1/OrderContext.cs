using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace ClassLibrary1
{
    public class OrderContext : DbContext
    {
        public OrderContext()
            : base("DBConnection1")
        {

        }      
        public DbSet<Order> orders { get; set; }
        public DbSet<Sale> sales { get; set; }        
    }
}