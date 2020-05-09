using NGU_Helper.Model;
using NGU_Helper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGU_Helper.Data
{
    public class ZoneModel : ViewModelBase
    {
        public ZoneModel()
        {
            Items = new ItemCollection();
        }

        public ZoneModel(Zone zone)
        {
            Id = zone.Id;
            Name = zone.Name;
            Order = zone.Order;
            Items = new ItemCollection(zone.Items);
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public ItemCollection Items { get; set; }
        public string ItemsCount => $"({Items?.Count ?? 0})";

        public void AddItem(ItemModel item)
        {
            Items.Add(item);
            OnPropertyChanged(nameof(ItemsCount));
        }

        public void RemoveItem(ItemModel item)
        {
            Items.Remove(item);
            OnPropertyChanged(nameof(ItemsCount));
        }
    }
}
