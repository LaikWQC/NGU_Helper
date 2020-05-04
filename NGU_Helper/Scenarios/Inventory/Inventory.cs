using NGU_Helper.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGU_Helper.Scenarios.Inventory
{
    public class Inventory
    {
        private List<InventoryItem> Outfit;
        private List<InventoryItem> Accessories;
        private InventoryItem _selectedSlot;

        public Inventory(int accessorySlots)
        {
            Outfit = InventoryItem.GetOufitItems();
            Accessories = InventoryItem.GetAccessoryItems(accessorySlots);
        }

        public void Equip(InventoryItem item)
        {
            item.Item = null;
            if (item.Type == ItemType.Accessory)
                _findSlot();
        }

        public void UnEquip(ItemViewModel item)
        {
            if (item.Type == ItemType.Accessory)
            {
                var slot = Accessories.First(x => x.Item == item);
                slot.Item = null;
                _findSlot();
            }
            else
            {
                Outfit.First(x => x.Type == item.Type).Item = null;
            }
        }

        private void _findSlot()
        {
            var slot = Accessories.FirstOrDefault(x => x.Item == null);
            if (slot != null)
                _selectedSlot = slot;
            if (_selectedSlot == null) _selectedSlot = Accessories.LastOrDefault();
        }

        private void _changeAccessorySlots(int accessorySlots)
        {
            if (accessorySlots < 0) accessorySlots = 0;
            var list = new List<InventoryItem>();
            var count = Accessories.Count;
            var equippedIndex = _selectedSlot == null ? -1 : Accessories.IndexOf(_selectedSlot);
            _selectedSlot = null;
            for (int i = 0; i < accessorySlots; i++) 
            {
                var accessory = new InventoryItem(ItemType.Accessory);
                list.Add(accessory);
                if (count > i) accessory.Item = Accessories[i].Item;
                if (equippedIndex == i) _selectedSlot = accessory;
            }
            Accessories = list;
            _findSlot();
        }
    }
}
