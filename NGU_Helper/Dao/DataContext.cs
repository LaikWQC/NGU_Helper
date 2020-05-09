using NGU_Helper.Model;
using System.Data.Entity;

namespace NGU_Helper.Dao
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Stat> Stats { get; set; }
        public DbSet<EquipedInventoryItem> Inventory { get; set; }
    }
}
