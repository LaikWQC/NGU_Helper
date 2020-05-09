using NGU_Helper.Data;
using NGU_Helper.Utils;
using NGU_Helper.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
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
                _item = value;
                OnPropertyChanged(nameof(Image));
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
    }
}
