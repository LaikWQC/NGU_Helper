using NGU_Helper.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGU_Helper.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Stat> Stats { get; set; }
    }
}
