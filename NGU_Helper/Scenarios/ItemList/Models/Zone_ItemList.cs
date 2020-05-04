using NGU_Helper.Utils;
using System;
using System.Collections.Generic;

namespace NGU_Helper.Scenarios.ItemList.Models
{
    public class Zone_ItemList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public List<Item_ItemList> Items { get; set; }
        public string ItemsCount => $"({Items?.Count ?? 0})";
    }
}
