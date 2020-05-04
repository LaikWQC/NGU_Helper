using NGU_Helper.Utils;
using NGU_Helper.Utils.Enums;
using System;

namespace NGU_Helper.Scenarios.ItemList.Models
{
    public class Stat_ItemList
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public double Value { get; set; }
        public StatTypeBl Type { get; set; }
        public string ValueText => $"{Value}%";
    }
}
