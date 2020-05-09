using System;

namespace NGU_Helper.Model
{
    public class EquipedInventoryItem
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public virtual Item Item { get; set; }
        public int? Slot { get; set; }
    }
}
