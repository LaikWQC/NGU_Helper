using NGU_Helper.Data;
using NGU_Helper.Utils.Enums;
using System.Collections.Generic;
using System.Linq;

namespace NGU_Helper.Scenarios.Inventory
{
    public class Inventory
    {
        private InventoryItem _selectedSlot;

        public Inventory(int accessorySlots)
        {
            Outfit = InventoryItem.GetOufitItems();
            Accessories = InventoryItem.GetAccessoryItems(accessorySlots);
            _findSlot();
        }

        public List<InventoryItem> Outfit { get; private set; }
        public List<InventoryItem> Accessories { get; private set; }

        public void Equip(ItemModel item)
        {
            if (item.Type.Type == ItemType.Accessory)
            {
                if(!_isEquiped(item))
                {
                    _selectedSlot.Item = item;
                    _findSlot();
                }
            }
            else
            {
                Outfit.First(x => x.Type == item.Type.Type).Item = item;
            }
        }

        public void UnEquip(InventoryItem item)
        {
            item.Item = null;
            if (item.Type == ItemType.Accessory)
                _findSlot();
        }

        private void _findSlot()
        {
            var slot = Accessories.FirstOrDefault(x => x.Item == null);
            if (slot != null)
                _selectedSlot = slot;
            if (_selectedSlot == null) _selectedSlot = Accessories.LastOrDefault();
        }

        private bool _isEquiped(ItemModel item)
        {
            return Accessories.Any(x => x.Item == item);
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
