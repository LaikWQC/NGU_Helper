using NGU_Helper.Utils.Enums;
using System;

namespace NGU_Helper.Model
{
    public class Criteria
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsActiveForTooltip { get; set; }
        public CriteriaType Type { get; set; }
        public StatType? StatType { get; set; }
        public string Formula { get; set; }
    }
}
