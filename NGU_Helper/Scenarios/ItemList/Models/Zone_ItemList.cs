using NGU_Helper.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NGU_Helper.Scenarios.ItemList.Models
{
    public class Zone_ItemList : ViewModelBase
    {
        public Zone_ItemList()
        {
            Items = new ObservableCollection<Item_ItemList>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public ObservableCollection<Item_ItemList> Items { get; set; }
        public string ItemsCount => $"({Items?.Count ?? 0})";

        public void AddItem(Item_ItemList item)
        {
            Items.Add(item);
            OnPropertyChanged(nameof(ItemsCount));
        }

        public void RemoveItem(Item_ItemList item)
        {
            Items.Remove(item);
            OnPropertyChanged(nameof(ItemsCount));
        }
    }
}
