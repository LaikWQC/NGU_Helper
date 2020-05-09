using NGU_Helper.Data;
using NGU_Helper.Repo;
using NGU_Helper.Utils.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGU_Helper.Scenarios.Inventory
{
    public class Inventory
    {
        private InventoryItem _selectedSlot;
        private InventoryRepo _repo;

        private bool _needSave = false;
        private bool _canSave = true;

        public Inventory(int accessorySlots)
        {
            _repo = new InventoryRepo();

            Outfit = InventoryItem.GetOufitItems();
            Accessories = InventoryItem.GetAccessoryItems(accessorySlots);
            Refresh();
        }

        public List<InventoryItem> Outfit { get; private set; }
        public List<InventoryItem> Accessories { get; private set; }

        public void Refresh()
        {
            _restoreInventory();
            _findSlot();
        }

        public void Equip(ItemModel item)
        {
            if (item.Type.Type == ItemType.Accessory)
            {
                if(!_isEquiped(item))
                {
                    _selectedSlot.Item = item;
                    _findSlot();
                    _startSave();
                }
            }
            else
            {
                var fit = Outfit.First(x => x.Type == item.Type.Type);
                if (fit.Item?.Id != item.Id)
                {
                    fit.Item = item;
                    _startSave();
                }
            }
        }

        public void UnEquip(InventoryItem item)
        {
            item.Item = null;
            if (item.Type == ItemType.Accessory)
                _findSlot();
            _startSave();
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
            return Accessories.Any(x => x.Item?.Id == item.Id);
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

        private void _restoreInventory()
        {
            var list = _repo.GetInventory();
            foreach(var fit in Outfit)
            {
                var item = list.FirstOrDefault(x => x.Item.ItemType == fit.Type)?.Item;
                fit.Item = item != null ? new ItemModel(item) : null;
            }
            foreach(var accessory in Accessories)
            {
                var item = list.FirstOrDefault(x => x.Item.ItemType == accessory.Type 
                    && Accessories.IndexOf(accessory) == x.Slot)?.Item;
                accessory.Item = item != null ? new ItemModel(item) : null;
            }            
        }

        private void _startSave()
        {
            _needSave = true;
            if(_canSave)
            {
                _canSave = false;
                Task.Run(() => _save());
            }            
        }

        private void _save()
        {
            _needSave = false;
            _repo.SaveInventory(Outfit, Accessories);
            if (_needSave)
                _save();
            else
                _canSave = true;
        }
    }
}
