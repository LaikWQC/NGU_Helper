using NGU_Helper.Data;
using NGU_Helper.Repo;
using NGU_Helper.Scenarios.Calculating;
using NGU_Helper.Scenarios.Calculating.Criterias;
using NGU_Helper.Scenarios.Tooltip;
using NGU_Helper.Utils;
using NGU_Helper.Utils.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGU_Helper.Scenarios.Inventory
{
    public class Inventory : ViewModelBase
    {
        private InventoryItem _selectedSlot;
        private readonly InventoryRepo _repo;
        private readonly Calculator _calculator;

        private bool _needSave = false;
        private bool _canSave = true;

        public ResultList Result { get; set; }

        public Inventory(int accessorySlots)
        {
            _repo = new InventoryRepo();
            _calculator = Calculator.Get();
            _calculator.SetInventory(this);

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

        /// <summary>
        /// получает список одетых вещей из БД
        /// </summary>
        private void _restoreInventory()
        {
            var list = _repo.GetInventory();
            foreach (var fit in Outfit)
            {
                var item = list.FirstOrDefault(x => x.Item.ItemType == fit.Type)?.Item;
                fit.Item = item != null ? new ExItemModel(item) : null;
            }
            foreach (var accessory in Accessories)
            {
                var item = list.FirstOrDefault(x => x.Item.ItemType == accessory.Type
                    && Accessories.IndexOf(accessory) == x.Slot)?.Item;
                accessory.Item = item != null ? new ExItemModel(item) : null;
            }
            _calculateStats();
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
            _startSave();
            _calculateStats();
        }

        #region Equip/Unequip
        public void Equip(ExItemModel item)
        {
            if(!_isEquiped(item))
            {
                if (item.Type.Type == ItemType.Accessory)
                {
                    _selectedSlot.Item = item;
                    _findSlot();                   
                }
                else
                {
                    var fit = Outfit.First(x => x.Type == item.Type.Type);
                    fit.Item = item;
                }
                _startSave();
                _calculateStats();
            }            
        }

        public void UnEquip(InventoryItem item)
        {
            item.Item = null;
            if (item.Type == ItemType.Accessory)
                _findSlot();
            _startSave();
            _calculateStats();
        }

        private void _findSlot()
        {
            bool isHighlighted = false;
            if (_selectedSlot != null)
            {
                isHighlighted = _selectedSlot.IsHighlighted;
                _selectedSlot.IsHighlighted = false;
            }

            var slot = Accessories.FirstOrDefault(x => x.Item == null);
            if (slot != null)
                _selectedSlot = slot;
            if (_selectedSlot == null) _selectedSlot = Accessories.LastOrDefault();

            if (_selectedSlot != null) _selectedSlot.IsHighlighted = isHighlighted;
        }

        private bool _isEquiped(ItemModel item)
        {
            if (item.Type.Type == ItemType.Accessory)
                return Accessories.Any(x => x.Item?.Id == item.Id);
            else
                return Outfit.Any(x => x.Item?.Id == item.Id);
        }

        public void HighLightItemSlot(ItemModel item, bool isHighlight)
        {
            var type = item.Type.Type;
            if (type == ItemType.Accessory)
                _selectedSlot.IsHighlighted = isHighlight;
            else
                Outfit.First(x => x.Type == type).IsHighlighted = isHighlight;
        }
        #endregion        

        #region Save
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
        #endregion

        #region Calculation
        public List<ItemModel> GetEquiped()
        {
            var list = new List<ItemModel>();
            list.AddRange(Outfit.Where(x => x.Item != null).Select(x => x.Item));
            list.AddRange(Accessories.Where(x => x.Item != null).Select(x => x.Item));
            return list;
        }

        private void _calculateStats()
        {
            Result = _calculator.Calculate();
            OnPropertyChanged(nameof(Result));
        }
        #endregion
    }
}
