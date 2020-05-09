using NGU_Helper.Data;
using NGU_Helper.Utils;
using NGU_Helper.Utils.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NGU_Helper.Scenarios.Inventory
{
    public class InventoryItem : ViewModelBase
    {
        public InventoryItem(ItemType type)
        {
            Type = type;
        }

        private ItemModel _item;
        public ItemModel Item 
        {
            get => _item;
            set
            {
                if (_item != null) _item.PropertyChanged -= OnItemPropertyChanged;
                _item = value;
                if (_item != null) _item.PropertyChanged += OnItemPropertyChanged;
                OnPropertyChanged(nameof(Item));
                OnPropertyChanged(nameof(Image));
                OnPropertyChanged(nameof(Tooltip));
            }
        }        

        public BitmapImage Image
        {
            get
            {
                if (Item != null)
                    return Item.Image;
                else
                    return ImageCreator.FreeSlotImage(Type);
            }
        }

        public ItemType Type { get; set; }

        public ToolTip Tooltip => Item?.Tooltip;

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName==nameof(Item.Tooltip))
            {
                OnPropertyChanged(nameof(Tooltip));
            }                
        }

        #region static
        public static List<InventoryItem> GetOufitItems()
        {
            return Enum.GetValues(typeof(ItemType)).Cast<ItemType>()
                .Where(x => x != ItemType.Accessory)
                .Select(x => new InventoryItem(x))
                .ToList();
        }

        public static List<InventoryItem> GetAccessoryItems(int count)
        {
            var list = new List<InventoryItem>();
            for (int i = 0; i < count; i++) 
            {
                list.Add(new InventoryItem(ItemType.Accessory));
            }
            return list;
        }
        #endregion
    }
}
