using NGU_Helper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGU_Helper.Data
{
    public class ItemsDao
    {
        public void CreateItem(Item item)
        {
            using (var context = new DataContext())
            {                
                context.Items.Add(item);
                context.SaveChanges();                
            }
        }

        public void EditItem(Item model)
        {
            using (var context = new DataContext())
            {
                var item = context.Items.Find(model.Id);
                if (item == null) return;
                item.Name = model.Name;
                item.Level = model.Level;
                item.ItemType = model.ItemType;
                item.ImageUrl = model.ImageUrl;
                context.SaveChanges();
            }
        }

        public void DeleteItem(Guid id)
        {
            using (var context = new DataContext())
            {
                var item = context.Items.Find(id);
                context.Items.Remove(item);
                context.SaveChanges();
            }
        }
    }
}
