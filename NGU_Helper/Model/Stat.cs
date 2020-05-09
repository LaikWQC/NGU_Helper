using NGU_Helper.Utils.Enums;
using System;

namespace NGU_Helper.Model
{
    public class Stat
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public StatType StatType { get; set; }
        public double Value { get; set; }
    }
}
