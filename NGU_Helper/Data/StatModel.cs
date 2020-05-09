using NGU_Helper.Model;
using NGU_Helper.Utils.Enums;
using System;

namespace NGU_Helper.Data
{
    public class StatModel
    {
        public StatModel(Guid itemId)
        {
            ItemId = itemId;
        }

        public StatModel(Stat stat, int level)
        {
            Id = stat.Id;
            ItemId = stat.ItemId;
            BaseValue = stat.Value;
            Level = level;
            Type = new StatTypeBl(stat.StatType);
        }

        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public double BaseValue { get; set; }
        public double Value => BaseValue * (1 + (double)Level / 100);
        public int Level { get; set; }
        public StatTypeBl Type { get; set; }
        public string BaseValueText => $"{BaseValue}%";
        public string ValueText => $"{Value}%";
    }
}
